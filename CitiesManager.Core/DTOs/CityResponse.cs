using System.ComponentModel.DataAnnotations;
using CitiesManager.Core.Entity_Classes;

namespace CitiesManager.Core.DTOs
{
    public class CityResponse
    {
        [Key]
        [Required]
        public Guid CityId { get; set; }

        [Required]
        public string? CityName { get; set; }

    }

    public static class CityExtensions
    {
        public static CityResponse ToCityResponse(this City city)
        {
            return new CityResponse
            {
                CityId = city.CityId,
                CityName = city.CityName
            };
        }
    }

}
