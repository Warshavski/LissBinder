using System.Configuration;

using Microsoft.AspNet.Identity;

using Ninject;

using Escyug.LissBinder.Data;
using Escyug.LissBinder.Data.QueryProcessors;
using Escyug.LissBinder.Data.SqlServer;
using Escyug.LissBinder.Data.SqlServer.QueryProcessors;

using Escyug.LissBinder.Web.Api.Providers;

using Escyug.LissBinder.Web.Models;
using Escyug.LissBinder.Web.Models.Repositories;
using Escyug.LissBinder.Web.Models.Services;

namespace Escyug.LissBinder.Web.Api
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var container = new StandardKernel();

            /* Create the bindings */

            // COMMON SECTION
            //--------------------------------------------
            ConfigureDbContext(container);


            // QUERY PROCESSORS SECTION
            //--------------------------------------------
            container.Bind<IUserQueryProcessor>().To<UserQueryProcessor>();
            container.Bind<IDictionaryQueryProcessor>().To<DictionaryQueryProcessor>();
            container.Bind<IPharmacyDrugsQueryProcessor>().To<PharmacyDrugsQueryProcessor>();
            container.Bind<IBindingsQueryProcessor>().To<BindingsQueryProcessor>();


            // REPOSITORY SECTION
            //--------------------------------------------
            container.Bind<IUserStore<User>>().To<UserRepository>();
            container.Bind<IDictionaryRepository>().To<DictionaryRepository>();
            container.Bind<IPharmacyDrugsRepository>().To<PharmacyDrugsRepository>();
            container.Bind<IBindingsRepository>().To<BindingsRepository>();


            // SERVICES SECTION
            //--------------------------------------------
            container.Bind<UserManager<User>>().To<UserService>();


            // AUTH SECTION
            //--------------------------------------------
            container.Bind<AuthorizationServerProvider>().ToSelf();

            return container;
        }

        private static void ConfigureDbContext(IKernel container)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["local"].ConnectionString;

            var context = new SqlContext(connectionString);

            container.Bind<DbContext>().ToConstant(context);
        }
    }
}