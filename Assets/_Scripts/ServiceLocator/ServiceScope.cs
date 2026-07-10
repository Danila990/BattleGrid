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

        private void Awake()
        {
            if (_isAutoBuild)
            {
                PostAwake();
                Build();
                LateAwake();
            }
        }

        protected virtual void LateAwake() { }

        protected virtual void PostAwake() { }

        public void Build()
        {
            if (_isBuilded) return;

            _isBuilded = true;
            _container = new Container();
            _builder = new Builder(_container);
            _injector = new Injector(_container);
            Configurate(_builder);
            InjectContainer();
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

        public IInjector Inject(object obj) => _injector.Inject(obj);
        public IInjector InjectMono(object obj) => _injector.InjectMono(obj);
        public IInjector InjectMono(MonoBehaviour obj) => _injector.InjectMono(obj);


        private void OnDestroy()
        {
            _injector = null;
            _builder = null;
            _container = null;
        }
    }
}