using Akka.Actor;
using API.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace API
{
    public class Startup
    {
        private static ILogger Logger = Shared.Logger.LoggerFactory.CreateLogger("Startup");

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddCors(options => CorsPolicies.GetCorsOptions(options));

                services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

                //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => AuthenticationPolicies.GetJwtBearerOptions(options));

                //services.AddAuthorization(options => AuthorizationPolicies.GetAuthorizationOptions(options));
                //services.AddSingleton<IAuthorizationHandler, RequiredClaimsHandler>();

                services.AddSingleton<ActorSystem>(options => ActorPolicies.GetActorSystemOptions(options));

                services.AddSingleton<IActorRef>(options => ActorPolicies.GetIActorRefOptions(options));

                services.AddSwaggerGen(options => SwaggerPolicies.GetSwaggerGenOptions(options));
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in ConfigureServices", ex);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                // Set LoggerFactory to use everywhere
                Shared.Logger.ConfigureLogger(loggerFactory);
                Shared.Logger.LoggerFactory = loggerFactory;

                app.UseHttpsRedirection();

                app.UseAuthentication();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui
                app.UseSwaggerUI(options => SwaggerPolicies.GetSwaggerUIOptions(options)); ;

                app.UseMvc();
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Configure", ex);
            }
        }
    }
}