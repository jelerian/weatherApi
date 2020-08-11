using AutoMapper;
using WeatherService.Exceptions;
using WeatherService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherService.Repo
{
    public class Location
    {
        public string city { set; get; }
        public string state { set; get; }

    }
    public class smallRecord
    {
        public int id { set; get; }
        public Location location { set; get; }

        public DateTime date { set; get; }
    }

    public class weatherRepo : IWeatherRepo
    {
        readonly List<Model.Weather> weather = new List<Model.Weather>();

        IMapper Mapper { get; }

        public weatherRepo(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<Model.Weather> Get()
        {
            weather.Sort(delegate (Model.Weather x, Model.Weather y)
            {
                if (x.id > y.id)
                    return 1;
                else if (x.id < y.id)
                    return -1;
                return 0;
            });
            return weather.AsQueryable();
        }

        public List<Model.Weather> GetAll()
        {
            weather.Sort(delegate (Model.Weather x, Model.Weather y)
            {
                if (x.id > y.id)
                    return 1;
                else if (x.id < y.id)
                    return -1;
                return 0;
            });
            return weather;
        }

        public List<Model.Weather> GetByDate(DateTime date)
        {
            DateTime temp = Convert.ToDateTime(date.ToString("yyyy-MM-dd"));
            List<Model.Weather> result = weather.Where(w => (w.date == temp)).ToList();

            return result;
        }

        public bool Create(Model.Weather p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            if (weather.Any(x => x.id == p.id))
            {
                return false;
            }

            weather.Add(p);
            return true;
        }

        public void Delete(int id)
        {
            var p = weather.FirstOrDefault(x => x.id == id);
            if (p == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Model.Weather)}' with the key '{id}' not found");
            }

            weather.RemoveAll(x => x.id == p.id);
        }

        public void Update(Model.Weather p)
        {

            if (p == null)
                throw new ArgumentNullException(nameof(p));

            var stored = weather.FirstOrDefault(x => x.id == p.id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Model.Weather)}' with the key '{p.id}' not found");
            }

            weather.RemoveAll(x => x.id == stored.id);
            weather.Add(Mapper.Map<Model.Weather>(p));
        }

        public smallRecord UpdateByLocation(Model.Weather p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            smallRecord rec = new smallRecord();
            rec.location = new Location();

            var stored = weather.FirstOrDefault(x => x.location.lat == p.location.lat && x.location.lon == p.location.lon
            && x.date == p.date);
            if (stored == null)
            {
                rec.id = -1;
            }
            else
            {
                rec.id = p.id;
                rec.date = p.date;
                rec.location.city = p.location.city;
                rec.location.state = p.location.state;
            }

            weather.RemoveAll(x => x.id == stored.id);
            weather.Add(p);
            return rec;
        }

        public void EraseContents()
        {
            weather.Clear();
        }
    }
}
