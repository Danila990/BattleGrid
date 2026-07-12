using System;
using UnityEngine;

namespace UnityScope
{
    [DefaultExecutionOrder(-999)]
    public abstract class ServiceScope : MonoBehaviour
    {
        [SerializeField] private bool _isAutoBuild = true;

        private bool _isBuilded = false;
        private IBuilder _builder;

        private static IInjector _injector;
        private static IContainer _container;

        public void Build()
        {
            if (_isBuilded) return;

            _isBuilded = true;
            _container = new ServiceContainer();
            _builder = new ServiceBuilder(_container);
            _injector = new ServiceInjector(_container);
            Configurate(_builder);
            InjectContainer();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoBuild()
        {
            if (_isAutoBuild)
                Build();
        }

        private void InjectContainer()
        {
            foreach (var service in _container.Services)
                _injector.InjectMono(service);

            _injector.InjectAllMonoBehaviour();
        }

        public abstract void Configurate(IBuilder builder);

        public static IContainerGeter Get<T>(out T service) where T : class => _container.Get<T>(out service);
        public static T Get<T>(Type type) where T : class => Get<T>(type);
        public static T Get<T>() where T : class => Get<T>();

        public static IInjector Inject(object obj) => _injector.Inject(obj);
        public static IInjector InjectMono(object obj) => _injector.InjectMono(obj);
        public static IInjector InjectMono(MonoBehaviour obj) => _injector.InjectMono(obj);


        private void OnDestroy()
        {
            _injector = null;
            _builder = null;
            _container = null;
        }
    }
}