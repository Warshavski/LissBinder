using System;
using System.Web.Http;

using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

//using Ninject.Web.Common.OwinHost;
//using Ninject.Web.WebApi.OwinHost;

using Owin;
using Ninject;
using System.Reflection;
using System.Configuration;
using Escyug.LissBinder.Data;
using Escyug.LissBinder.Web.Providers.Api;
using Escyug.LissBinder.Web.Models;
using Microsoft.AspNet.Identity;
using Escyug.LissBinder.Web.Models.Repositories;
using Escyug.LissBinder.Data.SqlServer.QueryProcessors;
using Escyug.LissBinder.Data.QueryProcessors;

[assembly: OwinStartup(typeof(Escyug.LissBinder.Web.Api.Startup))]
namespace Escyug.LissBinder.Web.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());
            
            ConfigureOAuth(app, config);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/account/auth"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider((UserManager<User>)config.DependencyResolver.GetService(typeof(UserManager<User>)))
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}