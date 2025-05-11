using CitiesManager.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services_Interfaces
{
    public interface ICitiesAdder
    {
        public Task<CityResponse> AddCity(CityAddRequest cityAddRequest);
    }
}
