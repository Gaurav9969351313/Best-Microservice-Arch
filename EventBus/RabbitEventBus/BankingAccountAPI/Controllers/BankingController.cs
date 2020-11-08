using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingAccountAPI.Interfaces;
using BankingAccountAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingAccountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankingController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public BankingController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET api/banking
        [HttpGet]
        public ActionResult<IEnumerable<Account>> Get()
        {
            return Ok(_accountService.GetAccounts());
        } 

        [HttpPost]
        public IActionResult Post([FromBody] AccountTransfer accountTransfer)
        {
            _accountService.Transfer(accountTransfer);
            return Ok(accountTransfer);
        }    
    }
}