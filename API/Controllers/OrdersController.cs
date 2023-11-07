using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize] // we use this attribute , because we need email from the token that we can take it
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
            
        }
        private readonly IMapper _mapper;

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto, IMapper mapper)
        {
            // This is the loing version
            // we create extension method for not writing so long
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        
            // new one
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            // Check that the Address is coming from OrderAgregate class
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400,"Problem creating order"));
        
            return Ok(order);
        }
    }
}