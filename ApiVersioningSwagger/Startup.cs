using ApiVersioningSwagger.SwaggerOptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ApiVersioningSwagger
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;


                //p.Conventions.Controller<Features.v1.Products.ProductsController>().HasApiVersion(new ApiVersion(1, 0));
                //p.Conventions.Controller<Features.v2.Products.ProductsController>().HasApiVersion(new ApiVersion(2, 0));
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen();

            services.AddTransient<Features.v1.Products.IProductRepository, Features.v1.Products.ProductRepository>();
            services.AddTransient<Features.v2.Products.IProductRepository, Features.v2.Products.ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseMvc()
                .UseApiVersioning()
                .UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
                }

                options.DocExpansion(DocExpansion.List);
            });
        }
    }
}
