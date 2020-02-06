﻿using System.IO;
using Microsoft.AspNetCore.Hosting;
using Akh.Breed.Web.Helpers;

namespace Akh.Breed.Web.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel(opt => opt.AddServerHeader = false)
                .UseUrls("http://0.0.0.0:5000") 
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIIS()
                .UseIISIntegration()
                .UseStartup<Startup>();
        }
    }
}

