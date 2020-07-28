using System;
namespace WeatherService.Dto
{

    /// <summary>
    /// DTO for creating and updating Weather
    /// </summary>
    public class UpdateWeather
    {
        /// <summary>
        /// date
        /// </summary>
        public DateTime date
        {
            get { return date; }
            set { date = Convert.ToDateTime(value.ToString("yyyy-MM-dd")); }
        }

        /// <summary>
        /// location
        /// </summary>
        public Location location;

        /// <summary>
        /// temperature
        /// </summary>

        public float[] temperature { get; set; }
    }
}
