using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoApi.Config;
using FluentValidation.AspNetCore;
using System.Reflection;
using CarDataContractValidator;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNet.OData.Extensions;

namespace AutoApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(f=> f.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(DataContractValidator))))
                .AddJsonOptions(opt => opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            services
                .AddDependencies()
                .AddAutoMapper()
                .ConfigureCors()
                .AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(
                
            rbuilder =>
            {
                rbuilder.Select().Expand().Filter().OrderBy().MaxTop(50).Count();
                rbuilder.MapODataServiceRoute("api/odata", "api/odata", ODataConfig.GetEdmModel());
            });
        }
    }
}
