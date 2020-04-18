using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieRank.Services;

namespace MovieRank.Controllers
{   [Route("setup")]
    public class SetupController : Controller
    {
        private readonly ISetupService setupService;
        public SetupController(ISetupService setupService)
        {
            this.setupService = setupService;
        }

        [HttpPost]
        [Route("createtable/{dynamoDbTableName}")]
        public async Task<IActionResult> CreateDynamoDbTable(string dynamoDbTableName)
        {
            await setupService.CreateDynamoDbTable(dynamoDbTableName);
            return Ok();
        }

        [HttpDelete]
        [Route("deletetable/{dynamoDbTableName}")]
        public async Task<IActionResult> DeleteTable(string dynamoDbTableName)
        {
            await setupService.DeleteDynamoDbTableAsync(dynamoDbTableName);
            return Ok();
        }
    }
}