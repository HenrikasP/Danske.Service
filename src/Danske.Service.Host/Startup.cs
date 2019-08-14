using System.IO.Compression;
using Danske.Service.Host.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Danske.Service.Host
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors()
                .AddMemoryCache()
                .ConfigureDependencyInjection()
                .Configure<GzipCompressionProviderOptions>
                    (options => options.Level = CompressionLevel.Fastest)
                .AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); })
                .AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new Info
                    {
                        Title = "Danske API",
                        Description = "Danske graph API"
                    });
                })
                .AddMvcCore()
                .AddApiExplorer()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter())
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Global
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            app.UseSwagger();

            app
                .UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
                .UseMvc()
                .UseMvcWithDefaultRoute()
                .UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Danske API V1"); })
                .UseResponseCompression();
            ;
        }
    }
}
