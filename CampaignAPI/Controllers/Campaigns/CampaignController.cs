using Campaign.Application.Commands;
using Campaign.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CampaignAPI.Controllers.Campaigns
{
    [Route("api/campaign")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        IMediator _mediator;
        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("create_campaign")]
        [HttpPost]
        public async Task<IActionResult> CreateCampaign(
                    [FromBody] CreateCampaignCommand request)
        {
            var response = await _mediator.Send(request);
            return response.IsError ? BadRequest(response) : Ok(response);
        }

        [Route("increase_time")]
        [HttpPost]
        public async Task<IActionResult> IncreaseTimeToCampaign(
            [FromBody] IncreaseTimeCommand request)
        {
            var response = await _mediator.Send(request);
            return response.IsError ? BadRequest(response) : Ok(response);
        }


        [Route("get_campaign_info/{campaignName}")]
        [HttpGet]
        public async Task<IActionResult> GetProductInfo(
                    [FromRoute] string campaignName)
        {
            var response = await _mediator.Send(new GetCampaignByCodeQuery(campaignName));
            return response.IsError ? BadRequest(response) : Ok(response);
        }
    }
}
