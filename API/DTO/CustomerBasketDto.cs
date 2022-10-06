using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.DTO
{
    public class CustomerBasketDto
    {
        public List<BasketItemDto> Items { get; set; }

        [Required]
        public string Id { get; set; }

        public int? DeliveryMethodId  { get; set; }

        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}