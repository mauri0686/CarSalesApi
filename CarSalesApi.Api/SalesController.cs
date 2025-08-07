using CarSalesApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApi.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        /// <summary>
        /// Creates a new sale entry in the system.
        /// </summary>
        /// <param name="request">The details of the sale to be created, including the car type and distribution center.</param>
        /// <returns>Returns an IActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleRequest request)
        {
            _saleService.CreateSale(request);
            return Ok();
        }

        /// <summary>
        /// Retrieves the total sales volume across all distribution centers.
        /// </summary>
        /// <returns>Returns an IActionResult containing the total sales volume.</returns>
        [HttpGet("total-volume")]
        public IActionResult GetTotalSalesVolume()
        {
            var total = _saleService.GetTotalSalesVolume();
            return Ok(total);
        }

        /// <summary>
        /// Retrieves the sales volume for a specific distribution center.
        /// </summary>
        /// <param name="centerId">The identifier of the distribution center whose sales volume is to be retrieved.</param>
        /// <returns>Returns an IActionResult containing the sales volume for the specified distribution center.</returns>
        [HttpGet("volume-by-center/{centerId}")]
        public IActionResult GetSalesVolumeByCenter(int centerId)
        {
            var total = _saleService.GetSalesVolumeByCenter(centerId);
            return Ok(total);
        }

        /// <summary>
        /// Retrieves the percentage distribution of sales by car type for each distribution center.
        /// </summary>
        /// <returns>Returns an IActionResult containing a dictionary where the keys represent distribution center IDs, and the values contain nested dictionaries of car types and their respective sales percentages.</returns>
        [HttpGet("percentage-by-center")]
        public IActionResult GetPercentageByCenter()
        {
            var result = _saleService.GetPercentageByCenter();
            return Ok(result);
        }
    }
}