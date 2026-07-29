using ABCPharmacy.Api.DTOs;
using ABCPharmacy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCPharmacy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        private readonly ILogger<SalesController> _logger;
        public SalesController(
            ISaleService saleService,
            ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }
        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales =
                await _saleService
                .GetAllAsync();


            return Ok(sales);
        }
        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> CreateSale(
            [FromBody] CreateSaleDto saleDto)
        {
            try
            {
                await _saleService
                    .CreateSaleAsync(saleDto);



                return Ok(
                    new
                    {
                        message =
                        "Sale completed successfully"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while processing sale");


                return BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}