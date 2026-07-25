using System;
using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public class ServiceLocator
    {
        private IContainer _sceneContainer;
        private IContainer _globalContainer;
        private IInjector _injector;
        private bool _isProjectBuild = false;

        private static ServiceLocator _instance = new ServiceLocator();

        public ServiceLocator()
        {
            _sceneContainer = new Container();
            _globalContainer = new Container();
            _injector = new Injector();

            BuildProject();
        }

        public static T Get<T>(Type type) where T : class
        {
            T getObject = _instance._sceneContainer.Get<T>(type);
            if (getObject == null)
                getObject = _instance._globalContainer.Get<T>(type);

            return getObject;
        }

        public static T Get<T>() where T : class => Get<T>(typeof(T));

        public static void Inject(object obj) => _instance._injector.Inject(obj);
        public static void InjectMono(object obj) => _instance._injector.InjectMono(obj);
        public static void InjectMono(MonoBehaviour obj) => _instance._injector.InjectMono(obj);

        public static void BuildScope(IServiceContext configurateScope)
        {
            _instance._sceneContainer.Clear();

            IBuilder builder = new Builder(_instance._sceneContainer);
            configurateScope.Configurate(builder);

            foreach (var service in _instance._sceneContainer.Services)
                _instance._injector.InjectMono(service);

            _instance._injector.InjectAllMonoBehaviour();
        }

        private void BuildProject()
        {
            if (_isProjectBuild) return;

            _isProjectBuild = true;
            IBuilder builder = new Builder(_globalContainer);
            IServiceContext scope = Resources.Load<IServiceContext>(nameof(EntryPointScope));
            if (scope == null)
            {
                Debug.Log("Отсутствует Project Scope");
                return;
            }

            foreach (var service in _globalContainer.Services)
                _injector.InjectMono(service);
        }
    }
}
