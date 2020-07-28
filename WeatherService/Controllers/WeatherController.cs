using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherService.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherService.Model;
using System.Text.Json;

namespace WeatherService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class WeatherController : ControllerBase
    {
        Repo.IWeatherRepo WeatherRepo { get; }
        IMapper Mapper { get; }
        ILogger Logger { get; }

        public WeatherController(IWeatherRepo weatherRepo, IMapper mapper, ILogger<WeatherController> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            WeatherRepo = weatherRepo ?? throw new ArgumentNullException(nameof(weatherRepo));
        }

        /// <summary>
        /// Get all weather
        /// </summary>
        /// <response code="200">List of all weather</response>
        [HttpGet]
        public string Get()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(WeatherRepo.GetAll());
        }

        /// <summary>
        /// Get all weather, filtered by date
        /// </summary>
        /// <param name="date">Weather for a given date</param>
        [HttpGet]
        [Route("{date}")]
        public string GetByDate(DateTime date)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(WeatherRepo.GetByDate(date));
        }

        /// <summary>
        /// Create a new Weather entry
        /// </summary>
        /// <param name="json">New Weather data</param>
        /// <response code="201">The Weather entry was added successfully</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult Create(string json)
        {
            Weather newWeather = JsonSerializer.Deserialize<Model.Weather>(json);
            Dictionary<string, object> values = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            newWeather.location = JsonSerializer.Deserialize<Model.Location>(values["location"].ToString());
            if (newWeather.temperature.Count() != 24)
                throw new Exception(newWeather.temperature.Count() + " temperature data points provided. Expected 24.");
            for(int i = 0; i < newWeather.temperature.Count(); i++)
            {
                newWeather.temperature[i] = (float)(((int)(newWeather.temperature[i] * 10)) / 10.0);
            }
            bool created= WeatherRepo.Create(newWeather);
            if (!created)
                return BadRequest();
            Weather createdWeather = WeatherRepo.GetById(newWeather.id);

            Logger.LogInformation("New Weather was created: {@Weather}", createdWeather);
            if (createdWeather != null)
                return Created(this.Url.ToString(), createdWeather);
            else
                return BadRequest();
        }

/*        /// <summary>
        /// Update a Weather
        /// </summary>
        /// <param name="id">Id of the Weather to update</param>
        /// <param name="WeatherDto">Weather data</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(int id, [FromBody] Dto.UpdateWeather WeatherDto)
        {
            var Weather = WeatherRepo.GetById(id);
            Mapper.Map(WeatherDto, Weather);
            WeatherRepo.Update(Weather);
            return Ok();
        }
*/
        /// <summary>
        /// Clear all weather data
        /// </summary>
        /// Clear the data
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public IActionResult Delete()
        {
            WeatherRepo.EraseContents();
            return Ok();
        }

 /*       /// <summary>
        /// Example of an exception handling
        /// </summary>
        [HttpGet("ThrowAnException")]
        public IActionResult ThrowAnException()
        {
            throw new Exception("Example exception");
        }
*/
    }
}
