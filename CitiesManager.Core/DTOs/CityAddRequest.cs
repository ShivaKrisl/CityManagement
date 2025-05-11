using CitiesManager.Core.Entity_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.DTOs
{
    public class CityAddRequest
    {

        [Required]
        public string? CityName { get; set; }


        public City ToCity()
        {
            return new City()
            {
                CityName = this.CityName?.ToLower(),
            };
        }

    }
}
