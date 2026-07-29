using ABCPharmacy.Api.DTOs;
using ABCPharmacy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ABCPharmacy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        private readonly ILogger<MedicinesController> _logger;


        public MedicinesController(
            IMedicineService medicineService,
            ILogger<MedicinesController> logger)
        {
            _medicineService = medicineService;
            _logger = logger;
        }



        // GET: api/medicines
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medicines =
                await _medicineService.GetAllAsync();


            return Ok(medicines);
        }



        // GET: api/medicines/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(
            int id)
        {
            var medicine =
                await _medicineService
                .GetByIdAsync(id);



            if (medicine == null)
            {
                return NotFound(
                    new
                    {
                        message = "Medicine not found"
                    });
            }


            return Ok(medicine);
        }




        // GET: api/medicines/search?name=para
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(
                    new
                    {
                        message = "Search name is required"
                    });
            }


            var result =
                await _medicineService
                .SearchAsync(name);


            return Ok(result);
        }




        // POST: api/medicines
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateMedicineDto medicineDto)
        {
            try
            {
                await _medicineService
                    .AddAsync(medicineDto);



                return Ok(
                    new
                    {
                        message =
                        "Medicine added successfully"
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error while adding medicine");


                return BadRequest(
                    new
                    {
                        message = ex.Message
                    });
            }
        }
    }
}