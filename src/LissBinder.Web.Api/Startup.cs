using System;
using System.Web.Http;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;

using Owin;

using Escyug.LissBinder.Web.Api.Providers;
using Escyug.LissBinder.Web.Models;

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

        /// <summary>
        /// Configure authorization in web api
        /// </summary>
        /// <param name="app"></param>
        /// <param name="config"></param>
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