using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Plumsail.Domain;
using Plumsail.Repository;

namespace Plumsail.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<AppDbContext>();
                if (!context.SomethingTypes.Any())
                {
                    context.SomethingTypes.AddRange(new List<SomethingType>
                    {
                        new SomethingType
                        {
                            Name = "Красный"
                        },
                        new SomethingType
                        {
                            Name = "Круглый"
                        },
                        new SomethingType
                        {
                            Name = "Острый"
                        },
                        new SomethingType
                        {
                            Name = "Глубокий"
                        }
                    });
                    context.SaveChanges();
                }
            }

            host.Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
