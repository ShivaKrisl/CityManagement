using CitiesManager.Core.DTOs;

namespace CitiesManager.Core.Services_Interfaces
{
    public interface ICitiesUpdateService
    {

        public Task<CityResponse>? UpdateCity(Guid cityId, CityAddRequest cityRequest);

    }
}
