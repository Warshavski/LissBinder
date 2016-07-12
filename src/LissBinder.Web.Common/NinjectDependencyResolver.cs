using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using Ninject;

namespace Escyug.LissBinder.Web.Common
{
    /*
     * This tells ASP.NET Web API to ask Ninject for
     * all dependencies required at run time by the 
     * dependent objects. This is the key that allows you
     * to push dependencies up to the constructor on 
     * the controllers. Without this resolver, ASP.NET 
     * won't use your configured Ninject container for dependencies.
     */

    public sealed class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _container;

        public NinjectDependencyResolver(IKernel container)
        {
            _container = container;
        }

        public IKernel Container
        {
            get { return _container; }
        }

        // delegate to the Ninject container to get object instances for the requested service types
        public object GetService(Type serviceType)
        {
            // prevent Ninject from blowing up if it is asked for a dependency that it can't provide because the dependency - or one of its dependencies - was never registered
            // we simply want to return null if we haven't explicitly registered a given type.
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}
