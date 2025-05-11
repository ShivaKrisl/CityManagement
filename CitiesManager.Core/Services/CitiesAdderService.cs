using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.DTOs;
using CitiesManager.Core.Entity_Classes;
using CitiesManager.Core.Services_Interfaces;
using CitiesManager.Core.ValidationHelpers;

namespace CitiesManager.Core.Services
{
    public class CitiesAdderService : ICitiesAdder
    {
        private readonly ICitiesRepository _citiesRepository;

        private readonly ICitiesGetterService _citiesGetterService;

        public CitiesAdderService(ICitiesRepository citiesRepository, ICitiesGetterService citiesGetterService)
        {
            _citiesRepository = citiesRepository;
            _citiesGetterService = citiesGetterService;
        }

        /// <summary>
        /// This method is used to add a new city to the database.
        /// </summary>
        /// <param name="cityAddRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CityResponse> AddCity(CityAddRequest cityAddRequest)
        {
            if(cityAddRequest == null)
            {
                throw new ArgumentNullException(nameof(cityAddRequest), "CityAddRequest cannot be null");
            }

            bool isValid = ValidateRequest.IsModelValid(cityAddRequest);
            if (!isValid)
            {
                throw new ArgumentException(nameof(cityAddRequest), ValidateRequest.ErrorMessage);
            }

            // check if city name duplicated
            CityResponse? cityResponse = await _citiesGetterService.GetCityByName(cityAddRequest.CityName);

            if (cityResponse != null)
            {
                throw new ArgumentException(nameof(cityAddRequest), "City name already exists");
            }

            City city = cityAddRequest.ToCity();
            city.CityId = Guid.NewGuid();

            city = await _citiesRepository.AddCity(city);

            return city.ToCityResponse();
        }
    }
}
