using System;
using UnityEngine;

namespace GameCore.UnityServiceLocator
{
    public class ServiceLocator
    {
        private IServiceContainer _sceneContainer;
        private IServiceContainer _globalContainer;
        private IServiceInjector _injector;
        private bool _isProjectBuild = false;

        private static ServiceLocator _instance = new ServiceLocator();

        public ServiceLocator()
        {
            _sceneContainer = new ServiceContainer();
            _globalContainer = new ServiceContainer();
            _injector = new ServiceInjector();

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
            IServiceBuilder builder = new ServiceBuilder(_instance._sceneContainer);
            configurateScope.BuildContext(builder);

            foreach (var service in _instance._sceneContainer.Services)
                _instance._injector.InjectMono(service);

            _instance._injector.InjectAllMonoBehaviour();
        }

        private void BuildProject()
        {
            if (_isProjectBuild) return;

            _isProjectBuild = true;
            IServiceBuilder builder = new ServiceBuilder(_globalContainer);
            IServiceContext context = Resources.Load<ProjectContext>(nameof(ProjectContext));
            if (context == null)
            {
                Debug.LogError($"Отсутствует в папке Resources {nameof(ProjectContext)}");
                return;
            }

            context.BuildContext(builder);
            foreach (var service in _globalContainer.Services)
                _injector.InjectMono(service);
        }
    }
}
