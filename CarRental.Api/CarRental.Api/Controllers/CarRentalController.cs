using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Api.Models;
using CarRental.Api.Models.Responses;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Api.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarRentalController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;
        private readonly IMapper _mapper;

        public CarRentalController(ICarRentalService carRentalService,
            IMapper mapper)
        {
            _carRentalService = carRentalService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list", Name = "ListAll")]
        public async Task<IActionResult> ListAll()
        {
            var availableCarsDto = await _carRentalService.ListCars();

            return Ok(_mapper.Map<List<CarInformationResponse>>(availableCarsDto));
        }

        public async Task<IActionResult> BookCar()
        {

        }
    }
}
