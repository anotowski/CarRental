using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;

        public CarRentalController(ICarRentalService carRentalService)
        {
            _carRentalService = carRentalService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var carDtos = await _carRentalService.ListCars();

            return Ok(carDtos);
        }
    }
}
