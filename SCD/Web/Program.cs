using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Prodest.Scd.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "1806";
            var requestPath = Environment.GetEnvironmentVariable("REQUEST_PATH");
            var url = $"http://*:{port}{requestPath}";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls(url)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
