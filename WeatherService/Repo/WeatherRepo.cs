using AutoMapper;
using WeatherService.Exceptions;
using WeatherService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherService.Repo
{
    public class weatherRepo : IWeatherRepo
    {
        readonly List<Weather> weather = new List<Weather>();

        IMapper Mapper { get; }

        public weatherRepo(IMapper mapper)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IQueryable<Weather> Get()
        {
            weather.Sort(delegate (Weather x, Weather y)
            {
                if (x.id > y.id)
                    return 1;
                else if (x.id < y.id)
                    return -1;
                return 0;
            });
            return weather.AsQueryable();
        }

        public List<Weather> GetAll()
        {
            weather.Sort(delegate (Weather x, Weather y)
            {
                if (x.id > y.id)
                    return 1;
                else if (x.id < y.id)
                    return -1;
                return 0;
            });
            return weather;
        }

        public List<Weather> GetByDate(DateTime date)
        {
            DateTime temp = Convert.ToDateTime(date.ToString("yyyy-MM-dd"));
            List<Weather> result = weather.Where(w => (w.date == temp)).ToList();

            return result;
        }

        public bool Create(Weather p)
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
                throw new KeyNotFoundException($"An object of a type '{nameof(Weather)}' with the key '{id}' not found");
            }

            weather.RemoveAll(x => x.id == p.id);
        }

        public void Update(Weather p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            var stored = weather.FirstOrDefault(x => x.id == p.id);
            if (stored == null)
            {
                throw new KeyNotFoundException($"An object of a type '{nameof(Weather)}' with the key '{p.id}' not found");
            }

            weather.RemoveAll(x => x.id == stored.id);
            weather.Add(Mapper.Map<Weather>(p));
        }

        public void EraseContents()
        {
            weather.Clear();
        }

    }
}
