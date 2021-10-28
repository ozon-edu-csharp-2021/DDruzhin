using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseApi.Models;
using OzonEdu.MerchandiseApi.Services.Interfaces;

namespace OzonEdu.MerchandiseApi.Controllers
{
    [ApiController]
    [Route("v1/api/merchandise")]
    public class MerchandiseController : ControllerBase
    {
        private readonly IMerchandiseService _merchandiseService;

        public MerchandiseController(IMerchandiseService merchandiseService)
        {
            _merchandiseService = merchandiseService;
        }
        
        [HttpPost]
        public async Task<ActionResult<MerchItem>> RequestMerch(long workerId, MerchType merchType,
            CancellationToken token)
        {
            var merchItem = await _merchandiseService.RequestMerch(workerId, merchType, token);
            if (merchItem is null)
            {
                return NotFound();
            }

            return Ok(merchItem);
        }


        [HttpGet("{workerId:long}")]
        public async Task<ActionResult<List<MerchItem>>> RequestMerchInfo(long workerId, CancellationToken token)
        {
            var merchItems = await _merchandiseService.RequestMerchInfo(workerId, token);

            if (merchItems is null)
            {
                return NotFound();
            }
            if (merchItems.Count == 0)
            {
                return NoContent();
            }

            return Ok(merchItems);
        }
    }
}