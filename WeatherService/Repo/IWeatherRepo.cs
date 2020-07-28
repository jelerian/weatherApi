using System;
using System.Collections.Generic;
using WeatherService.Dto;

namespace WeatherService.Repo
{
    public interface IWeatherRepo : IRepository<Model.Weather>
    {
    }
}
