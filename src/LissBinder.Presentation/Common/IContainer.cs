using System;
using System.Linq.Expressions;

namespace Escyug.LissBinder.Presentation.Common
{
    /// <summary>
    /// Interface for IoC - container (Adapter)
    /// </summary>
    public interface IContainer
    {
        void Register<TService, TImplementation>()
            where TImplementation : TService;

        void Register<TService>();

        void RegisterInstance<T>(T instance);

        void RegisterInstance(string instanceName, string serviceName);

        TService Resolve<TService>();

        bool IsRegistered<TService>();

        void Register<TService, TArgument>(Expression<Func<TArgument, TService>> factory);
    }
}
