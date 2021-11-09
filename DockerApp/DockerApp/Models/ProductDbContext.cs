using Microsoft.EntityFrameworkCore;
using System;
namespace DockerApp.Models{
    public class ProductDbContext: DbContext{

        public ProductDbContext(DbContextOptions<ProductDbContext> options)
        :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            var envs = Environment.GetEnvironmentVariables();
            var host = envs["DBHOST"] ?? "localhost";
            var port = envs["DBPORT"] ?? "3306";
            var password = envs["DBPASSWORD"] ?? "uali";
            var connectionString = $"server={host};userid=root;pwd={password};" + $"port={port};database=products";
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }


        public DbSet<Product> Products {get; set;}
    }
}