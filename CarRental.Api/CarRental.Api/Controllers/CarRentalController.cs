using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Api.Models.Requests;
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
        private readonly IRentService _rentService;
        private readonly IMapper _mapper;

        public CarRentalController(ICarRentalService carRentalService,
            IRentService rentService,
            IMapper mapper)
        {
            _carRentalService = carRentalService;
            _rentService = rentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("list", Name = "ListAll")]
        public async Task<IActionResult> ListAll()
        {
            var availableCarsDto = await _carRentalService.ListCars();

            return Ok(_mapper.Map<List<CarInformationResponse>>(availableCarsDto));
        }

        [HttpPost]
        [Route("rent", Name = "Rent")]
        public async Task<IActionResult> RentCar([FromBody] RentRequest rentRequest)
        {
            var rentBookingNumber = await _rentService.RentCar(rentRequest.CarPlateNumber,
                rentRequest.CustomerEmail,
                rentRequest.CustomerDateOfBirth);

            return Ok(rentBookingNumber);
        }


    }
}
