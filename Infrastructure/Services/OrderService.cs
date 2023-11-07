using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IBasketRepository _basketRepo;
        private readonly IGenericRepository<DeliveryMethod> _dmRepo;
        private readonly IGenericRepository<Product> _productRepo;
        public OrderService(IGenericRepository<Order> orderRepo,IGenericRepository<DeliveryMethod> dmRepo, IGenericRepository<Product> productRepo, IBasketRepository basketRepo)
        {
            _productRepo = productRepo;
            _dmRepo = dmRepo;
            _basketRepo = basketRepo;
            _orderRepo = orderRepo;

        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // 1. Get basket from the basket repo --> we do not trust in the basket !!! 

            // we can trust the quantity and the product , but we cannot trust the price

            // Better explanation what is is meant:
            // If we go to the shop , we see sticker with price on the shelves
            // We can change it , but when we are at the counter and want to pay
            // the shop-worker it not trusting us , but is trusting the bar-sign 
            // that is scanning and see the actual price
            // WE ARE DOING THE SAME THING HERE

            // 2. Get items from the product repo

            // 3. Get delivery method from the repo
            // 4. Calculate subtotal
            // 5. create order
            // 6. save to db
            // 7. return order

            // In order to perform all this, we need to inject the repos for working: basket,order, deliverymethod, product 


            // We start to solve all points
            
            // 1
             var basket = await _basketRepo.GetBasketAsync(basketId);

             // 2
             var items = new List<OrderItem>();
             foreach (var item in basket.Items)
             {
                var productItem = await _productRepo.GetByIdAsync(item.Id);
                var itemOrderd = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                                                        // we get the price from db from item from basket
                var orderItem = new OrderItem(itemOrderd, productItem.Price, item.Quantity);
                items.Add(orderItem);
             }

             // 3
             var deliveryMethod = await _dmRepo.GetByIdAsync(deliveryMethodId);

             // 4 
             var subtotal = items.Sum(item => item.Price * item.Quantity);

             // 5
             var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);

             // 6 TODO:

             // 7 
             return order;
            



        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetORderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}