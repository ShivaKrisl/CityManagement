using Asp.Versioning;
using CitiesManager.Core.DTOs;
using CitiesManager.Core.Services_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManager.WebAPI.Controllers.v2
{

    /// <summary>
    /// Controller for managing cities in the database.
    /// </summary>
    [ApiVersion("2.0")]
    public class CityController : CommonControllerBase
    {
        private readonly ICitiesGetterService _citiesGetterService;

        /// <summary>
        /// Constructor for CityController.
        /// </summary>
        /// <param name="citiesGetterService"></param>
        public CityController(ICitiesGetterService citiesGetterService)
        {
           _citiesGetterService = citiesGetterService;
        }


        /// <summary>
        /// Get all the city names from the database.
        /// </summary>
        /// <returns>List of cities with city name</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _citiesGetterService.GetCities();
            if(cities == null || cities.Count == 0)
            {
                return NotFound("No cities found.");
            }
            List<string>? cityNames = cities.Select(c => c.CityName).ToList();
            return Ok(cityNames);
        }

    }
}
