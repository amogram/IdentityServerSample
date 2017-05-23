using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TheLibrary.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "TheLibary.IdentityServer";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
