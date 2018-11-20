namespace WebApi
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nest;
    using Services;
    using WebApi.Config;

    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            var logger = LoggerFactory.CreateLogger<Startup>();

            logger.LogDebug($"EnvironmentName = {env.EnvironmentName}");
        }

        public IConfiguration Configuration { get; }
        public ILoggerFactory LoggerFactory { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var logger = LoggerFactory.CreateLogger<Startup>();

            var elasticsearchSection = Configuration.GetSection("Elasticsearch");
            var elasticsearchSettings = elasticsearchSection.Get<ElasticsearchSettings>();

            services.AddTransient<IElasticClient>(impl =>
            {
                logger.LogDebug($"Elasticsearch > Endpoint = {elasticsearchSettings.Endpoint}, DefaultIndex = {elasticsearchSettings.DefaultIndex}");

                var connectionStettings = new ConnectionSettings(new Uri(elasticsearchSettings.Endpoint))
                    .DefaultIndex(elasticsearchSettings.DefaultIndex)
                    .DisableDirectStreaming()
                    .EnableDebugMode();

                return new ElasticClient(connectionStettings);
            });

            services.AddTransient<IDataService, DataService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddLogging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}