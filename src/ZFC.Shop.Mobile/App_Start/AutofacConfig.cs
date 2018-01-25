using Autofac;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Web.Mvc;
using ZFC.Shop.Entity;
using ZFC.Shop.Utility;
using ZFC.Shop.Service;

namespace ZFC.Shop.Mobile
{
    public static class AutofacConfig
    {
        public static void Run()
        {
            ConfigService.RegisterTables();

            RegitsterType();
        }

        private static void RegitsterType()
        {
            var builder = new ContainerBuilder();

            var assemblies = ConfigService.GetAssemblys().ToArray();

            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies).Where(t => baseType.IsAssignableFrom(t)).AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();

            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(currentAssembly).PropertiesAutowired();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}