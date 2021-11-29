using Campaign.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignAPI.Controllers.Orders
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("create_order")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(
            [FromBody] CreateOrderCommand request)
        {
            var response = await _mediator.Send(request);
            return response.IsError ? BadRequest(response) : Ok(response);
        }
    }
}
