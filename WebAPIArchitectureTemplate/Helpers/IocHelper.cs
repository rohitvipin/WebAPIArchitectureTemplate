using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using WebAPIArchitectureTemplate.Services.Implementations;
using WebAPIArchitectureTemplate.Services.Interfaces;

namespace WebAPIArchitectureTemplate.Helpers
{
    public static class IocHelper
    {
        public static void Initialise(IUnityContainer unityContainer)
        {
            var container = BuildUnityContainer(unityContainer);

            var unityDependencyResolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(unityDependencyResolver);
        }

        private static IUnityContainer BuildUnityContainer(IUnityContainer container)
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();        
            container.RegisterType<IBlogService, BlogService>(new HierarchicalLifetimeManager());

            return container;
        }
    }
}
