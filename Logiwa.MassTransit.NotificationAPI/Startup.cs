using Logiwa.MassTransit.NotificationAPI.Services;
using Logiwa.MassTransit.Models;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logiwa.MassTransit.NotificationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddScoped<IMailService, MailService>();

            // MassTransit DI
            services.AddMassTransit(config =>
            {
                config.AddConsumer<OrderConsumer>(); //added Consumer
                config.UsingRabbitMq((context, cfg) =>
                {

                    cfg.Host(new Uri("rabbitmq://localhost/")); 
                    cfg.ReceiveEndpoint("LOrder", c =>          // queue
                       {
                           c.ConfigureConsumer<OrderConsumer>(context); 
                       });

                });

            });
            services.AddMassTransitHostedService(); //automatically start and stop bus
            // MassTransit DI

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logiwa.MassTransit.NotificationAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logiwa.MassTransit.NotificationAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
