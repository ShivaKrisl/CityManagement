using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.DTOs;
using CitiesManager.Core.Entity_Classes;
using CitiesManager.Core.Services_Interfaces;
using CitiesManager.Core.ValidationHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services
{
    public class CitiesUpdateService : ICitiesUpdateService
    {

        private readonly ICitiesRepository _citiesRepository;
        private readonly ICitiesGetterService _citiesGetterService;
        public CitiesUpdateService(ICitiesRepository citiesRepository, ICitiesGetterService citiesGetterService)
        {
            _citiesRepository = citiesRepository;
            _citiesGetterService = citiesGetterService;
        }

        public async Task<CityResponse>? UpdateCity(Guid cityId, CityAddRequest cityRequest)
        {
            if(cityRequest == null)
            {
                throw new ArgumentNullException(nameof(cityRequest));
            }

            CityResponse? existingCity = await _citiesGetterService.GetCityById(cityId);
            if (existingCity == null)
            {
                throw new KeyNotFoundException($"City with ID {cityId} not found.");
            }

            bool isModelStateValid = ValidateRequest.IsModelValid(cityRequest);
            if (!isModelStateValid)
            {
                throw new ArgumentException(nameof(cityRequest), ValidateRequest.ErrorMessage);
            }

            if(cityId != existingCity.CityId)
            {
                throw new ArgumentException("City ID mismatch.");
            }

            // Check if the city name already exists
            CityResponse? existingCityByName = await _citiesGetterService.GetCityByName(cityRequest.CityName);
            if (existingCityByName != null && existingCityByName.CityId != cityId)
            {
                throw new ArgumentException($"City with name {cityRequest.CityName} already exists.");
            }

            City cityToUpdate = cityRequest.ToCity();
            cityToUpdate.CityId = cityId;

            City updatedCity = await _citiesRepository.UpdateCity(cityToUpdate);
           
            return updatedCity.ToCityResponse();

        }
    }
}
