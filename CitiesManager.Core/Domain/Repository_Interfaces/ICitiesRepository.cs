using CitiesManager.Core.Entity_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Domain.Repository_Interfaces
{
    public interface ICitiesRepository
    {
        public Task<City> AddCity(City city);

        public Task<List<City>> GetAllCities();

        public Task<City?> GetCityByName(string? cityName);

        public Task<City?> GetCityById(Guid cityId);

        public Task<City> UpdateCity(City city);

        public Task<bool> DeleteCity(Guid cityId);
    }
}
