using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameCore.UnityServiceLocator
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class InjectAttribute : PropertyAttribute { }

    public interface IServiceInjector
    {
        public void InjectAllMonoBehaviour();
        public IServiceInjector Inject(object obj);
        public IServiceInjector InjectMono(object obj);
        public IServiceInjector InjectMono(MonoBehaviour obj);
    }

    public class ServiceInjector : IServiceInjector
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        public void InjectAllMonoBehaviour()
        {
            var mono = Object.FindObjectsByType<MonoBehaviour>();

            foreach (var monoBehaviour in mono)
            {
                if (!IsInjectable(monoBehaviour)) return;

                var injectableFields = monoBehaviour.GetType().GetFields(BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

                foreach (var injectableField in injectableFields)
                    if (injectableField.GetValue(monoBehaviour) != null)
                        continue;

                InjectMono(monoBehaviour);
            }
        }

        public IServiceInjector InjectMono(object obj)
        {
            if (obj is MonoBehaviour monoBehaviour )
                return InjectMono(monoBehaviour);

            Inject(obj);
            return this;
        }

        public IServiceInjector InjectMono(MonoBehaviour behavior)
        {
            var behaviours = behavior.GetComponentsInChildren<MonoBehaviour>();
            foreach (var mono in behaviours)
                Inject(mono);

            return this;
        }

        public IServiceInjector Inject(object obj)
        {
            if (!IsInjectable(obj)) return this;

            var type = obj.GetType();
            InjectFields(type, obj);
            InjectMethods(type, obj);
            InjectProperties(type, obj);

            return this;
        }

        #region InjectionLogick
        private void InjectFields(Type type, object instance)
        {
            var injectableFields = type.GetFields(BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields)
            {
                if (injectableField.GetValue(instance) != null)
                {
                    Debug.LogWarning($"[ServiceInjector] Field '{injectableField.Name}' of class '{type.Name}' is already set.");
                    continue;
                }

                var fieldType = injectableField.FieldType;
                var resolvedInstance = ServiceLocator.Get<object>(fieldType);
                if (resolvedInstance == null)
                    throw new Exception($"Failed to inject into field '{injectableField.Name}' of class '{type.Name}'.");

                injectableField.SetValue(instance, resolvedInstance);
            }
        }

        private void InjectMethods(Type type, object instance)
        {
            var injectableMethods = type.GetMethods(BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods)
            {
                var requiredParameters = injectableMethod.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .ToArray();
                var resolvedInstances = requiredParameters.Select(ServiceLocator.Get<object>).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null))
                    throw new Exception($"Failed to inject into method '{injectableMethod.Name}' of class '{type.Name}'.");

                injectableMethod.Invoke(instance, resolvedInstances);
            }
        }

        private void InjectProperties(Type type, object instance)
        {
            var injectableProperties = type.GetProperties(BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableProperty in injectableProperties)
            {
                var propertyType = injectableProperty.PropertyType;
                var resolvedInstance = ServiceLocator.Get<object>(propertyType);
                if (resolvedInstance == null)
                    throw new Exception($"Failed to inject into property '{injectableProperty.Name}' of class '{type.Name}'.");

                injectableProperty.SetValue(instance, resolvedInstance);
            }
        }

        private bool IsInjectable(object obj)
        {
            var members = obj.GetType().GetMembers(BINDING_FLAGS);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
        #endregion
    }
}