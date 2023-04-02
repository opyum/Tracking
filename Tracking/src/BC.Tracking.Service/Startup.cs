using Autofac;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Text.Json.Serialization;
using MQTTnet;
using MQTTnet.Client;

namespace Connexity.BC.Tracking.Service
{
    using Connexity.BC.Tracking.Domain;
    using Connexity.BC.Tracking.Service.Adapters;
    using Connexity.BC.Tracking.Service.Adapters.Spi;
    using Connexity.BC.Tracking.Service.Adapters.Spi.Configuration;
    
    using Connexity.BC.Tracking.Service.Adapters.Api;

    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServiceConfigOptions>(this.Configuration.GetSection("ServiceConfig"));
            services.Configure<RedisOptions>(this.Configuration.GetSection("Redis"));

            services
                .AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            services.AddHealthChecks();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton<IMqttClient>(sp =>
            {
                var factory = new MqttFactory();
                var mqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("localhost") // Replace with your Mosquitto server address
                    .Build();

                mqttClient.ConnectAsync(options).GetAwaiter().GetResult();
                return mqttClient;
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler(Constants.Routes.Errors);
            }


            app.UseRouting();


            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHealthChecks("/hc");
                });

        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<MediatorModule>();
            builder.RegisterModule<MappingModule>();
            builder.RegisterModule<DomainModule>();
            builder.RegisterModule<ApiModule>();
            builder.RegisterModule<SpiModule>();

        }
    }
}