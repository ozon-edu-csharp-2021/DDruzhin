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
        public async Task<ActionResult<MerchPackResponse>> RequestMerchPack(string workerEmail, int merchType, IEnumerable<long> items,
            CancellationToken token)
        {
            var merchPackRequestCommand = new MerchPackRequestCommand
            {
                Worker = workerEmail,
                MerchItems = items,
                MerchType = merchType
            };
            var merchPack = await _mediator.Send(merchPackRequestCommand,token);
            if (merchPack is null)
            {
                return NotFound();
            }

            return Ok(merchPack);
        }


        [HttpGet("{workerEmail}")]
        public async Task<ActionResult<List<MerchPackResponse>>> RequestMerchPacksInfo(string workerEmail, CancellationToken token)
        {
            var merchPacksInfoRequestCommand = new MerchPacksInfoRequestCommand
            {
                Worker = workerEmail
            };
            var merchPacks = await _mediator.Send(merchPacksInfoRequestCommand, token);

            if (merchPacks is null)
            {
                return NotFound();
            }
            if (!merchPacks.Any())
            {
                return NoContent();
            }

            return Ok(merchPacks);
        }
    }
}