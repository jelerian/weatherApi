using System;
namespace WeatherService.Dto
{
    /// <summary>
    /// DTO for reading weather (-s)
    /// </summary>
    public class Location
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    };

    public class Weather
    {
        /// <summary>
        /// Weather id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// date
        /// </summary>
        public DateTime date { get; set;}

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
