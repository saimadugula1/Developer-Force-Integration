using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesForceLeadDtos;
using SalesForceLeadLogic;

namespace SalesForceLeadAPI.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/SalesForce")]
    public class SalesForceLeadController : Controller
    {
        private readonly ISalesForceProcessor _salesForceProcessor;
        private readonly ILogger<SalesForceLeadController> _logger;

        public SalesForceLeadController(ISalesForceProcessor salesForceProcessor, ILogger<SalesForceLeadController> logger)
        {
            _salesForceProcessor = salesForceProcessor;
            _logger = logger;
        }
        //// POST: api/SalesForceLead
        [EnableCors("")]
        [Route("CreateLead")]
        [ValidateModel]
        [HttpPost]
        public IActionResult CreateLead([FromBody] SalesForceLeadDto lead)
        {
            if (_salesForceProcessor.CreateSFObject(lead, SalesForceLeadLogic.Constants.Lead_SObjectTypeName, Startup.Configuration).Result)
            {
                _logger.LogInformation("Lead created successfully for" + lead.Company + lead.Email + lead.FirstName + lead.LastName);
                return Ok();
            }
            else
            {
                _logger.LogInformation("Lead creation failed for" + lead.Company + lead.Email + lead.FirstName + lead.LastName);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create a lead for " + lead.FirstName + lead.LastName);
            }
        }

        [EnableCors("")]
        [Route("CreateOrder")]
        [ValidateModel]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] SalesForceOrderDTO order)
        {
            if (_salesForceProcessor.CreateSFObject(order, SalesForceLeadLogic.Constants.Lead_SObjectTypeName, Startup.Configuration).Result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create an order ");
            }
        }

        [EnableCors("")]
        [Route("CreateOrderItem")]
        [ValidateModel]
        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] SalesForceOrderItemDTO orderItem)
        {
            if (_salesForceProcessor.CreateSFObject(orderItem, SalesForceLeadLogic.Constants.Lead_SObjectTypeName, Startup.Configuration).Result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create an orderItem ");
            }
        }
    }
}
