
using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.Entity_Classes;
using Microsoft.EntityFrameworkCore;

namespace CityManager.Infrastructure.Repository_Classes
{
    public class CitiesRepository : ICitiesRepository
    {

        private readonly ApplicationDbContext _db;

        public CitiesRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<City> AddCity(City city)
        {
            await _db.Cities.AddAsync(city);
            await _db.SaveChangesAsync();
            return city;
        }

        public async Task<List<City>> GetAllCities()
        {
            return await _db.Cities.ToListAsync();
        }

        public async Task<City?> GetCityById(Guid cityId)
        {
            City? city = await _db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            if (city == null)
            {
                return null;
            }
            return city;
        }

        public async Task<City?> GetCityByName(string? cityName)
        {
            City? city = await _db.Cities.FirstOrDefaultAsync(c => c.CityName.ToLower() == cityName.ToLower());
            return city;
        }

        public async Task<City> UpdateCity(City city)
        {
            City? existingCity = _db.Cities.FirstOrDefault(c => c.CityId == city.CityId);
            if (existingCity == null)
            {
                return city;
            }
            existingCity.CityName = city.CityName;
            //_db.Cities.Update(existingCity);
            await _db.SaveChangesAsync();
            return existingCity;
        }

        public async Task<bool> DeleteCity(Guid cityId)
        {
            City? city = await _db.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            if (city == null)
            {
                return false;
            }
            _db.Cities.Remove(city);
            await _db.SaveChangesAsync();
            return true;

        }

        

        
    }

}
