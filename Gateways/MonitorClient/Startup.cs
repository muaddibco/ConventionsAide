using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConventionsAide.HealthCheck.MonitorClient
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _environment = env;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var uiBuilder = services.AddHealthChecksUI(setup =>
            {
                //var webhookHandler = new LogglyWebhookHandler(_configuration, _environment);
                //setup.SetHeaderText($"{_environment.EnvironmentName} Environment Health Checks Monitor");
                //setup.AddWebhookNotification("LogglyWebhook",
                //    uri: webhookHandler.BuildUrl(),
                //    payload: webhookHandler.Payload,
                //    restorePayload: webhookHandler.RestorePayload,
                //    customMessageFunc: webhookHandler.CustomMessage,
                //    customDescriptionFunc: webhookHandler.CustomDescription);
            });

            if (_environment.IsDevelopment())
            {
                uiBuilder.AddInMemoryStorage();
            }
            else
            {
                uiBuilder.AddSqlServerStorage(_configuration["Secrets:HealthChecksDatabase"]);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecksUI(setup => { setup.UIPath = "/"; });
            });
        }
    }
}
