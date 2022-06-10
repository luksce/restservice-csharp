using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RESTService.Services.Interface;
using RESTService.Models.Dto;

namespace RESTService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _queueService;

        private readonly ILogger<QueueController> _logger;

        public QueueController(IQueueService queueService, ILogger<QueueController> logger)
        {
            _queueService = queueService;
            _logger = logger;
        }

        // GET: api/<QueueController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<QueueController>
        [HttpPost("AddQueueItem")]
        public Return AddQueueItem([FromBody] List<CoinDto> listCoins)
        {
            return _queueService.AddItemRange(listCoins);
        }

        [HttpGet("GetQueueItem")]
        public string GetQueueItem()
        {
            return _queueService.GetListItem();
        }


    }
}
