using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using  Microsoft.EntityFrameworkCore;
using DockerApp.Models;

namespace DockerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .AddEnvironmentVariables()
            .Build();

            if ((config["INITDB"] ?? "false") == "true") {
                System.Console.WriteLine("Preparing Database...");
                var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
                
                var host = config["DBHOST"] ?? "localhost";
                var port = config["DBPORT"] ?? "3306";
                var password = config["DBPASSWORD"] ?? "uali";
                var connectionString = $"server={host};userid=root;pwd={password};port={port};database=products";
                optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
                var context = new ProductDbContext(optionsBuilder.Options);
                
                Models.SeedData.EnsurePopulated(context);
                System.Console.WriteLine("Database Preparation Complete");
            } else
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
