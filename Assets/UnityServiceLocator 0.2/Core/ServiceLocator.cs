using System;
using UnityEngine;

namespace UnityServiceLocator
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

        public static void BuildScope(IServiceContext configurateScope, bool injectSceneMonoBehaviour = false)
        {
            _instance._sceneContainer.Clear();
            IServiceRegister builder = new ServiceRegister(_instance._sceneContainer);
            configurateScope.BuildContext(builder);

            foreach (var service in _instance._sceneContainer.Services)
                _instance._injector.InjectMono(service);

            if(injectSceneMonoBehaviour)
                _instance._injector.InjectAllMonoBehaviour();
        }

        private void BuildProject()
        {
            if (_isProjectBuild) return;

            _isProjectBuild = true;
            IServiceRegister builder = new ProjectServiceRegister(_globalContainer);
            IServiceContext context = Resources.Load<ProjectContext>(nameof(ProjectContext));
            if (context == null)
            {
                Debug.LogWarning($"{nameof(ProjectContext)} Отсутствует в папке Resources");
                return;
            }

            context.BuildContext(builder);
            foreach (var service in _globalContainer.Services)
                _injector.InjectMono(service);
        }
    }
}
