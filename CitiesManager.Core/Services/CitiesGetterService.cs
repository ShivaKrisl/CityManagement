using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.DTOs;
using CitiesManager.Core.Entity_Classes;
using CitiesManager.Core.Services_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services
{
    public class CitiesGetterService : ICitiesGetterService
    {

        private readonly ICitiesRepository _citiesRepository;

        public CitiesGetterService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public async Task<List<CityResponse>?> GetCities()
        {
            List<City>? cities = await _citiesRepository.GetAllCities();
            if (cities == null)
            {
                return new List<CityResponse>();
            }
            List<CityResponse> cityResponses = cities.Select(c => c.ToCityResponse()).ToList();
            return cityResponses;
        }

        public async Task<CityResponse?> GetCityById(Guid cityId)
        {
            if(cityId == Guid.Empty)
            {
                throw new ArgumentException("City ID cannot be empty.");
            }

            City? city = await _citiesRepository.GetCityById(cityId);
            if(city == null)
            {
                return null;
            }
            CityResponse cityResponse = city.ToCityResponse();
            return cityResponse;
        }

        public async Task<CityResponse?> GetCityByName(string? cityName)
        {
            if(string.IsNullOrEmpty(cityName))
            {
                throw new ArgumentException("City name cannot be null or empty.");
            }

            City? city = await _citiesRepository.GetCityByName(cityName);
            if (city == null)
            {
                return null;
            }
            CityResponse cityResponse = city.ToCityResponse();
            return cityResponse;

        }
    }
}
