using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherService.Dto;

namespace WeatherService.Repo
{
    public interface IRepository<T> where T : class, Model.IEntity
    {
        bool Create(T e);
        void Delete(int id);
        IQueryable<T> Get();
        List<T> GetAll();
        List<T> GetByDate(DateTime date);
        smallRecord UpdateByLocation(Model.Weather weather);
        void Update(T e);
        void EraseContents();
    }
}
