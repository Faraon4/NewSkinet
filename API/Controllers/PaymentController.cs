using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            
        }       

        [Authorize]
        [HttpPost("{basketId}")] 
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePyymentIntenr(string basketId)
        {
            return await _paymentService.CreateOrUpdatePaymenyIntent(basketId);
        }
    }
}