using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        public string Id { get; set; } // we are not putting it in the db , we put it in Redis. It is string because of that and because that it will be generated by the Angular
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}