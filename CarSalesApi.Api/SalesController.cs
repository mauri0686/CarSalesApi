using CarSalesApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesApi.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController(ISaleService saleService) : ControllerBase
    {
        /// <summary>
        /// Creates a new sale entry in the system.
        /// </summary>
        /// <param name="request">The details of the sale to be created, including the car type and distribution center.</param>
        /// <returns>Returns an IActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult CreateSale([FromBody] CreateSaleRequest request)
        {
            saleService.CreateSale(request);
            return Ok();
        }

        /// <summary>
        /// Retrieves the total sales volume across all distribution centers.
        /// </summary>
        /// <returns>Returns an IActionResult containing the total sales volume.</returns>
        [HttpGet("volume/total")]
        public IActionResult GetTotalSalesVolume()
        {
            var total = saleService.GetTotalSalesVolume();
            return Ok(total);
        }

        /// <summary>
        /// Retrieves the sales volume for a specific distribution center.
        /// </summary>
        /// <param name="centerId">The identifier of the distribution center whose sales volume is to be retrieved.</param>
        /// <returns>Returns an IActionResult containing the sales volume for the specified distribution center.</returns>
        [HttpGet("volume/center/{centerId}")]
        public IActionResult GetSalesVolumeByCenter(int centerId)
        {
            var total = saleService.GetSalesVolumeByCenter(centerId);
            return Ok(total);
        }

        /// <summary>
        /// Retrieves the percentage distribution of sales by car type for each distribution center.
        /// </summary>
        /// <returns>Returns an IActionResult containing a dictionary where the keys represent distribution center IDs, and the values contain nested dictionaries of car types and their respective sales percentages.</returns>
        [HttpGet("percentage/center")]
        public IActionResult GetPercentageByCenter()
        {
            var result = saleService.GetPercentageByCenter();
            return Ok(result);
        }
    }
}