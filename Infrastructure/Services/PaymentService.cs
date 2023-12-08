using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Entities.Product;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, 
                              IConfiguration config)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymenyIntent(string basketId)
        {
            // spelling inside [] should be the same as the one that we mentioned in the appsettings.json
           StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

           var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) {
                return null;
            }

           var shippingPrice = 0m; //0m => 0 money

           if(basket.DeliveryMethodId.HasValue)
           {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);

            shippingPrice = deliveryMethod.Price;
           }

           foreach(var item in basket.Items)
           {
                // Product need to be important somehow other way that it was before --> check the imports
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if(item.Price != productItem.Price)
                {
                    // we need to do this check , because we cannot trust the client
                    item.Price = productItem.Price;
                }
           }

           var service = new PaymentIntentService(); // Comming from stripe

           PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    // Amount is the math for doing the necesary calculations
                    Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice*100,
                    Currency ="usd",
                    PaymentMethodTypes = new List<string> {"card"}
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else 
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice*100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);    
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return basket;

        }
    }
}