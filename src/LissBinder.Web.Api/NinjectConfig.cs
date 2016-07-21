using Escyug.LissBinder.Data.QueryProcessors;
using Escyug.LissBinder.Data.SqlServer.QueryProcessors;
using Escyug.LissBinder.Web.Models;
using Escyug.LissBinder.Web.Providers.Api;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


using Escyug.LissBinder.Web.Models.Repositories;
using System.Configuration;
using Escyug.LissBinder.Data;

namespace Escyug.LissBinder.Web.Api
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var container = new StandardKernel();
            //Create the bindings

            // COMMON SECTION
            //--------------------------------------------
            ConfigureDbContext(container);


            // QUERY PROCESSORS SECTION
            //--------------------------------------------
            container.Bind<IAddBindingQueryProcessor>().
                To<AddBindingQueryProcessor>();
            container.Bind<IAddUserQueryProcessor>().
                To<AddUserQueryProcessor>();
            container.Bind<IAddPharmacyDrugsQueryProcessor>().
                To<AddPharmacyDrugsQueryProcessor>();
            container.Bind<IDeletePharmacyDrugsQueryProcessor>().
                To<DeletePharmacyDrugsQueryProcessor>();
            container.Bind<IDictionaryDrugsByNameQueryProcessor>().
                To<DictionaryDrugsByNameQueryProcessor>();
            container.Bind<IPharmacyByUserQueryProcessor>().
                To<PharmacyByUserQueryProcessor>();
            container.Bind<IPharmacyDrugsByNameQueryProcessor>().
                To<PharmacyDrugsByNameQueryProcessor>();
            container.Bind<IUserByLoginQueryProcessor>().
                To<UserByLoginQueryProcessor>();


            // REPOSITORY SECTION
            //--------------------------------------------
            container.Bind<IDictionaryDrugsRepository>().
                To<DictionaryDrugsRepository>();
            container.Bind<IPharmacyDrugsRepository>().
                To<PharmacyDrugsRepository>();
            container.Bind<IPharmacyRepository>().
                To<PharmacyRepository>();
            container.Bind<IUserRepository>().
                To<UserRepository>();


            // AUTH SECTION
            //--------------------------------------------
            container.Bind<UserManager<User>>().
                To<IdentityUserManager>();
            container.Bind<AuthorizationServerProvider>().ToSelf();

            return container;
        }

        private static void ConfigureDbContext(IKernel container)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            var context = new DbContext(connectionString);

            container.Bind<DbContext>().ToConstant(context);
        }
    }
}