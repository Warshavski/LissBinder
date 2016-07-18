using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using Ninject;
using Ninject.Web.Common;

using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.QueryProcessors;
using Escyug.LissBinder.Data.SqlServer.QueryProcessors;

using Escyug.LissBinder.Web.Models.Repositories;

namespace Escyug.LissBinder.Web.Api
{
    /*
     * This is where we bind or relate interfaces 
     * to concrete implementations so that the
     * dependencies can be resolved at run time.
     * For example, if a class requires an IDateTime object, 
     * the bindings tell the container to provide a DateTimeAdapter object.
     */
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        // bind interfaces to concrete implementation
        // make it share for whole application
        private void AddBindings(IKernel container)
        {
            // COMMON SECTION
            //--------------------------------------------
            ConfigureDbContext(container);


            // QUERY PROCESSORS SECTION
            //--------------------------------------------
            container.Bind<IAddBindingQueryProcessor>().
                To<AddBindingQueryProcessor>().
                InRequestScope();
            container.Bind<IAddPharmacyDrugsQueryProcessor>().
                To<AddPharmacyDrugsQueryProcessor>().
                InRequestScope();
            container.Bind<IDeletePharmacyDrugsQueryProcessor>().
                To<DeletePharmacyDrugsQueryProcessor>().
                InRequestScope();
            container.Bind<IDictionaryDrugsByNameQueryProcessor>().
                To<DictionaryDrugsByNameQueryProcessor>().
                InRequestScope();
            container.Bind<IPharmacyByUserQueryProcessor>().
                To<PharmacyByUserQueryProcessor>().
                InRequestScope();
            container.Bind<IPharmacyDrugsByNameQueryProcessor>().
                To<PharmacyDrugsByNameQueryProcessor>().
                InRequestScope();
            container.Bind<IUserByLoginQueryProcessor>().
                To<UserByLoginQueryProcessor>().
                InRequestScope();


            // REPOSITORY SECTION
            //--------------------------------------------
            container.Bind<IDictionaryDrugsRepository>().
                To<DictionaryDrugsRepository>().
                InRequestScope();
            container.Bind<IPharmacyDrugsRepository>().
                To<PharmacyDrugsRepository>().
                InRequestScope();
            container.Bind<IPharmacyRepository>().
                To<PharmacyRepository>().
                InRequestScope();
            container.Bind<IUserRepository>().
                To<UserRepository>().
                InRequestScope();
        }

        private void ConfigureDbContext(IKernel container)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            var context = new DbContext(connectionString);

            container.Bind<DbContext>().ToConstant(context);
        }

        // configurate log4net (logger framework)
        private void ConfigureLog4net(IKernel container)
        {
            //XmlConfigurator.Configure();

            //var logManager = new LogManagerAdapter();
            //container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}