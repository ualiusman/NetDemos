using System.Linq;

namespace DockerApp.Models
{
    public interface IRepository{
        public IQueryable<Product> Products {get;}

    }
}