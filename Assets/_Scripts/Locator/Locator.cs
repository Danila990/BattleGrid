using System;
using UnityEngine;

namespace ServiceLocator
{
    [DefaultExecutionOrder(-999)]
    public static class Locator
    {
        private static IContainer _sceneContainer = new Container();
        private static IContainer _globalContainer = new Container();
        private static IInjector _injector = new Injector();
        private static bool _isProjectBuild = false;

        public static T Get<T>(Type type) where T : class
        {
            T getObject = _sceneContainer.Get<T>(type);
            if (getObject == null) 
                getObject = _globalContainer.Get<T>(type);

            return getObject;
        }

        public static T Get<T>() where T : class => Get<T>(typeof(T));

        public static void Inject(object obj) => _injector.Inject(obj);
        public static void InjectMono(object obj) => _injector.InjectMono(obj);
        public static void InjectMono(MonoBehaviour obj) => _injector.InjectMono(obj);


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoBuildScene()
        {
            BuildProject();
            BuildScene();
        }

        private static void BuildScene()
        {
            _sceneContainer.Clear();

            IBuilder builder = new Builder(_sceneContainer);
            Scope scope = GameObject.FindAnyObjectByType<Scope>();
            if (scope == null)
            {
                Debug.Log("Отсутствует Scene Scope");
                return;
            }

            foreach (var service in _sceneContainer.Services)
                _injector.InjectMono(service);

            _injector.InjectAllMonoBehaviour();
        }

        private static void BuildProject()
        {
            if (_isProjectBuild) return;

            _isProjectBuild = true;
            IBuilder builder = new Builder(_globalContainer);
            Scope scope = Resources.Load<Scope>(nameof(Scope));
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
