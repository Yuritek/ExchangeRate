using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RequestToSite;

namespace ExchangeRates
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRequestToCbr, RequestToCbr>();
            services.AddScoped<IChartFactory, ChartFactory>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
