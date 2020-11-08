using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransferSubAPI.Interfaces;
using TransferSubAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace TransferSubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        // GET api/transfer
        [HttpGet]
        public ActionResult<IEnumerable<TransferLog>> Get()
        {
            return Ok(_transferService.GetTransferLogs());
        }    
    }
}
