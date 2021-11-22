using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlace.Shared
{
    public class Menu
    {
        public List<Pizza> Pizzas = new List<Pizza>();

        public Pizza GetPizza(int id) 
            => Pizzas.SingleOrDefault(x => x.Id == id);
    }
}
