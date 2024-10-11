using Autofac;
using Autofac.Extensions.DependencyInjection;
using Kutuphane.Bussiness.DependencyResolvers.AutoFac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new BussinessModule());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((ctx, cb) =>
                    {
                        //  Please specify the condition that is true only when
                        //    the application is running on your development environment.
                        //    Notice that excludes the case that the environment is "Development".
                        if (!ctx.HostingEnvironment.IsDevelopment())
                        {
                            //  This call inserts "StaticWebAssetsFileProvider" into
                            //    the static file middleware.
                            StaticWebAssetsLoader.UseStaticWebAssets(
                                ctx.HostingEnvironment,
                                ctx.Configuration);
                        }
                    });
                });
    }
}
