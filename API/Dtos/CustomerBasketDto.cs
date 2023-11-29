using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } 
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

        // This 3 properties we copy from the CustomerBasket class itself
        public int? DeliveryMethodId { get; set; }

        public string ClientSecret { get; set; } 
        public string PaymentIntentId { get; set; } 
   
    }
}