[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Escyug.LissBinder.Web.Api.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Escyug.LissBinder.Web.Api.NinjectWebCommon), "Stop")]

// WebApi2Book.Web.Api.App_Start -> WebApi2Book.Web.Api
// common practice to use this namespace for files in the App_Start folder
namespace Escyug.LissBinder.Web.Api
{
    // move using directives outside of the namespace. (just personal preference)
    using System;
    using System.Web;
    using System.Web.Http;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using WebActivatorEx;

    using Escyug.LissBinder.Web.Api;
    using Escyug.LissBinder.Web.Common;

    /*
     * Make sure a DI container is created during 
     * application start-up and remains in memory
     * until the application shuts down. (You can think 
     * of the container as the object that contains the dependencies.)
     */
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// register dependency resolver with Web API configuratio. In doing so, 
        /// we have directed the framework to hit our configured Ninject 
        /// container instance to resolve any dependencies that are needed.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            //bootstrapper.Initialize(CreateKernel);

            IKernel container = null;
            bootstrapper.Initialize(() =>
            {
                container = CreateKernel();
                return container;
            });

            var resolver = new NinjectDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// configure the container bindings using the NinjectConfigurator class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var containerConfigurator = new NinjectConfigurator();
            containerConfigurator.Configure(kernel);
        }
    }
}

/*
 * It's important to note that the registration of our dependency resolver with Web API
 * and configuration of container bindings by the NinjectConfigurator.Configure method
 * are both called (the former directly, the letter indirectly) from the Start method, 
 * which is called during application start-up. In this way, all of this setup is completed
 * before the application accepts and processes anu HTTP requests, and thus before any of the
 * controllers - which rely on dependencies being injected into them - are ever created.
 */