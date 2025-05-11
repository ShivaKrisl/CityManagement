using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.Services_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services
{
    public class CitiesDeleteService : ICitiesDeleteService
    {
        private readonly ICitiesRepository _citiesRepository;

        public CitiesDeleteService(ICitiesRepository citiesRepository)
        {
            _citiesRepository = citiesRepository;
        }

        public async Task<bool> DeleteCity(Guid cityId)
        {
            if(cityId == Guid.Empty)
            {
                throw new ArgumentException("City ID cannot be empty.", nameof(cityId));
            }

            var city = await _citiesRepository.GetCityById(cityId);
            if (city == null)
            {
                return false;
            }
            await _citiesRepository.DeleteCity(cityId);
            return true;
        }
    }
}
