using AggregationApp.Data;
using AggregationApp.Interfaces;
using AggregationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElectricityController : Controller
    {
        private readonly DataContext _context;
        private readonly ILogger<ElectricityController> _logger;
        public ElectricityController(DataContext context, ILogger<ElectricityController> logger)
        {
            _context = context;
            _logger = logger;

        }

        [HttpGet]
        public async Task<ActionResult<AggregatedElectricity>> Get()
        {
            var response = await _context.AggregatedElectricities.ToListAsync();
            if(response.Count == 0)
            {
                _logger.LogWarning("Electricity data has not been processed yet or a problem occured during the data process");
                return Ok("Electricity data has not been processed yet");
            }

            else
            {
                _logger.LogInformation("AggregatedElectricities data has been retrieved from database");
                return Ok(response);
            }

        }
    }
}
