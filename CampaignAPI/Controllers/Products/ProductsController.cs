using Campaign.Application.Commands;
using Campaign.Application.Queries;
using Campaign.Domain.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CampaignAPI.Controllers.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Route("create_product")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] CreateProductCommand request)
        {
            var response = await _mediator.Send(request);
            return response.IsError ? BadRequest(response) : Ok(response);
        }

        [Route("get_product_info/{productCode}")]
        [HttpGet]
        public async Task<IActionResult> GetProductInfo(
            [FromRoute] string productCode)
        {
            var response = await _mediator.Send(new GetProductByCodeQuery(productCode));
            return response.IsError ? BadRequest(response) : Ok(response);
        }


    }
}
