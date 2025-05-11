using CitiesManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services_Interfaces
{
    public interface ICitiesGetterService
    {
        public Task<List<CityResponse>?> GetCities();

        public Task<CityResponse?> GetCityByName(string? cityName);

        public Task<CityResponse?> GetCityById(Guid cityId);

    }
}
