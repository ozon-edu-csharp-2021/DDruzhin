using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseApi.Domain.AggregationModels.MerchPackAggregate.Entities;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPackRequest;
using OzonEdu.MerchandiseApi.Infrastructure.Commands.MerchPacksInfoRequest;
using OzonEdu.MerchandiseApi.Models;

namespace OzonEdu.MerchandiseApi.Controllers
{
    [ApiController]
    [Route("v1/api/merchandise")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MerchandiseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<ActionResult<MerchPackResponse>> RequestMerch(string workerEmail, int merchType, IEnumerable<long> items,
            CancellationToken token)
        {
            var merchPackRequestCommand = new MerchPackRequestCommand
            {
                Worker = workerEmail,
                MerchItems = items,
                MerchType = merchType
            };
            var merchItem = await _mediator.Send(merchPackRequestCommand,token);
            if (merchItem is null)
            {
                return NotFound();
            }

            return Ok(merchItem);
        }


        [HttpGet("{workerEmail}")]
        public async Task<ActionResult<List<MerchPackResponse>>> RequestMerchInfo(string workerEmail, CancellationToken token)
        {
            var merchPacksInfoRequestCommand = new MerchPacksInfoRequestCommand
            {
                Worker = workerEmail
            };
            var merchItems = await _mediator.Send(merchPacksInfoRequestCommand, token);

            if (merchItems is null)
            {
                return NotFound();
            }
            if (!merchItems.Any())
            {
                return NoContent();
            }

            return Ok(merchItems);
        }
    }
}