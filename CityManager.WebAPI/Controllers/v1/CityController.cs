using Asp.Versioning;
using CitiesManager.Core.DTOs;
using CitiesManager.Core.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManager.WebAPI.Controllers.v1
{

    /// <summary>
    /// Controller for managing cities in the database.
    /// </summary>
    [ApiVersion("1.0")]
    public class CityController : CommonControllerBase
    {

        private readonly ICitiesAdder _citiesAdder;
        private readonly ICitiesGetterService _citiesGetterService;
        private readonly ICitiesUpdateService _citiesUpdateService;
        private readonly ICitiesDeleteService _citiesDeleteService;

        /// <summary>
        /// Constructor for CityController.
        /// </summary>
        /// <param name="citiesAdder"></param>
        /// <param name="citiesGetterService"></param>
        /// <param name="citiesUpdateService"></param>
        /// <param name="citiesDeleteService"></param>
        public CityController(ICitiesAdder citiesAdder, ICitiesGetterService citiesGetterService, ICitiesUpdateService citiesUpdateService, ICitiesDeleteService citiesDeleteService)
        {
            _citiesAdder = citiesAdder;
            _citiesGetterService = citiesGetterService;
            _citiesUpdateService = citiesUpdateService;
            _citiesDeleteService = citiesDeleteService;
        }


        /// <summary>
        /// Get all the cities (City name and Id) from the database.
        /// </summary>
        /// <returns>List of cities with city name and city ID</returns>

        //[Produces("application/xml")] for getting response as application/xml
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _citiesGetterService.GetCities();
            return Ok(cities);
        }


        /// <summary>
        /// Get a city by its ID.
        /// </summary>
        /// <param name="cityId">Id(Guid) of city you want to retrieve</param>
        /// <returns>City if found else NotFound error</returns>
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

        /// <summary>
        /// Get a city by its name.
        /// </summary>
        /// <param name="name">Name of city to be searched</param>
        /// <returns>City if found else NotFound error</returns>
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

        /// <summary>
        /// Add a new city to the database.
        /// </summary>
        /// <param name="cityAddRequest">City request that should have valid unique city name</param>
        /// <returns>City object if created</returns>
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

        /// <summary>
        /// Delete a city by its ID.
        /// </summary>
        /// <param name="cityId">Id(Guid) of the city to be deleted</param>
        /// <returns>No content if city is deleted else NotFound error</returns>
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

        /// <summary>
        /// Update a city by its ID.
        /// </summary>
        /// <param name="cityId">Id the city to be updated</param>
        /// <param name="cityAddRequest">City request object that has new valid unique city name </param>
        /// <returns>City object if updated success else NotFound error</returns>
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
