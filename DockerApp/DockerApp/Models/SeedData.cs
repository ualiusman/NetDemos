using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
namespace DockerApp.Models{

    public static class SeedData{
        public static void EnsurePopulated(IApplicationBuilder builder){
            var scope = builder.ApplicationServices.CreateScope();
var context = scope.ServiceProvider.GetService<ProductDbContext>();
            EnsurePopulated(context);
        }


        public static void EnsurePopulated(ProductDbContext context){
            System.Console.WriteLine("Applying Migratinos..");

            context.Database.Migrate();

            if(!context.Products.Any()){
                System.Console.WriteLine("Seeding product data..");
                context.Products.AddRange(
                new Product("Kayak", "Watersports", 275),
                new Product("Lifejacket", "Watersports", 48.95m),
                new Product("Soccer Ball", "Soccer", 19.50m),
                new Product("Corner Flags", "Soccer", 34.95m),
                new Product("Stadium", "Soccer", 79500),
                new Product("Thinking Cap", "Chess", 16),
                new Product("Unsteady Chair", "Chess", 29.95m),
                new Product("Human Chess Board", "Chess", 75),
                new Product("Bling-Bling King", "Chess", 1200)
                );
                context.SaveChanges();
            }else{
                System.Console.WriteLine("Seed Data is not required..");
            }
        }
    }
}