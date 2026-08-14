using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityServiceLocator
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InjectAttribute : PropertyAttribute { }

    public class ServiceInjector : IServiceInjector
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

        public void InjectAllScene()
        {
            var sceneMonoBehaviours = Object.FindObjectsByType<MonoBehaviour>();

            foreach (var monoBehaviour in sceneMonoBehaviours)
                Inject(monoBehaviour);
        }

        public IServiceInjector Inject(object obj)
        {
            if (!IsInjectable(obj)) return this;

            InjectFields(obj.GetType(), obj);
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

        private bool IsInjectable(object obj)
        {
            var members = obj.GetType().GetMembers(BINDING_FLAGS);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
        #endregion
    }
}