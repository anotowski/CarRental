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
        private readonly IReturnService _returnService;
        private readonly IMapper _mapper;

        public CarRentalController(ICarRentalService carRentalService,
            IRentService rentService,
            IReturnService returnService,
            IMapper mapper)
        {
            _carRentalService = carRentalService;
            _rentService = rentService;
            _returnService = returnService;
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
        public async Task<IActionResult> RentCar([FromBody] RentCarRequest rentRequest)
        {
            var rentBookingNumber = await _rentService.RentCar(rentRequest.CarPlateNumber,
                rentRequest.CustomerEmail,
                rentRequest.CustomerDateOfBirth);

            return Ok(rentBookingNumber);
        }

        [HttpPost]
        [Route("return", Name = "Return")]
        public async Task<IActionResult> ReturnCar([FromBody] ReturnCarRequest returnCarRequest)
        {
            var returnCarResponseDto = await _returnService.ReturnCar(returnCarRequest.BookingNumber,
                returnCarRequest.ReturnDate,
                returnCarRequest.CurrentMileage);

            return Ok(_mapper.Map<ReturnCarResponse>(returnCarResponseDto));
        }
    }
}
