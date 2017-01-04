using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using System;

namespace WebAPIArchitectureTemplate
{
    public static class UnityConfig
    {
        private static IUnityContainer _container;
        public static void RegisterComponents(IUnityContainer unityContainer)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            _container = unityContainer;
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unityContainer);
        }

        internal static IUnityContainer GetConfiguredContainer() => _container;
    }
}