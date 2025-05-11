using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitiesManager.Core.Entity_Classes
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }

        [Required(ErrorMessage ="City Name should not be Null or empty")]
        public string? CityName { get; set; }
    }
}
