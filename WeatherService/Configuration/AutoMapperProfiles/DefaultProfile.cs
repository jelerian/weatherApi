using AutoMapper;

namespace WeatherService.Profiles
{
    /// <summary>
    /// Default profile for AutoMapper
    /// </summary>
    /// <remarks>See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#automapper</remarks>
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Model.Weather, Dto.Weather>();
            CreateMap<Dto.UpdateWeather, Model.Weather>();
            // For copy creation
            CreateMap<Model.Weather, Model.Weather>();
        }
    }
}
