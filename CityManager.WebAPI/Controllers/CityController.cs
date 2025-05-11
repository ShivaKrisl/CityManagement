using CitiesManager.Core.DTOs;
using CitiesManager.Core.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly ICitiesAdder _citiesAdder;
        private readonly ICitiesGetterService _citiesGetterService;
        private readonly ICitiesUpdateService _citiesUpdateService;
        private readonly ICitiesDeleteService _citiesDeleteService;

        public CityController(ICitiesAdder citiesAdder, ICitiesGetterService citiesGetterService, ICitiesUpdateService citiesUpdateService, ICitiesDeleteService citiesDeleteService)
        {
            _citiesAdder = citiesAdder;
            _citiesGetterService = citiesGetterService;
            _citiesUpdateService = citiesUpdateService;
            _citiesDeleteService = citiesDeleteService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _citiesGetterService.GetCities();
            return Ok(cities);
        }

        [HttpGet("{cityId:guid}")]
        public async Task<IActionResult> GetCityById(Guid cityId)
        {
            var city = await _citiesGetterService.GetCityById(cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCityByName(string? name)
        {
            var city = await _citiesGetterService.GetCityByName(name);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityAddRequest cityAddRequest)
        {
            var city = await _citiesAdder.AddCity(cityAddRequest);
            if (city == null)
            {
                return BadRequest("City could not be added.");
            }
            return CreatedAtAction(nameof(GetCityById), new { cityId = city.CityId }, city);
            // this will return the location of the newly created resource (action method is GetCityById and cityId is the parameter)
        }

        [HttpDelete("{cityId:guid}")]
        public async Task<IActionResult> DeleteCity(Guid cityId)
        {
            var result = await _citiesDeleteService.DeleteCity(cityId);
            if (result == false)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{cityId:guid}")]
        public async Task<IActionResult> UpdateCity(Guid cityId, CityAddRequest cityAddRequest)
        {
            var city = await _citiesUpdateService.UpdateCity(cityId, cityAddRequest);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

    }
}
