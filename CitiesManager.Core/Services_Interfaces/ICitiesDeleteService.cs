using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Services_Interfaces
{
    public interface ICitiesDeleteService
    {
        public Task<bool> DeleteCity(Guid cityId);
    }
}
