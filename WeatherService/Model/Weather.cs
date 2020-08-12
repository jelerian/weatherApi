using System;

namespace WeatherService.Model
{
    public class Location
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public string city { get; set; }
        public string state { get; set; }
    };

    public class Weather : IEntity
    {
        public Weather()
        {
        }

        public int id { get; set; }
        
        public DateTime date
        {
            get; set;
        }

        public Location location { get; set; }
        public float[] temperature { get; set; }

    }
}
