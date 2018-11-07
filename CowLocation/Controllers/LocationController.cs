using CowLocation.Dto;
using CowLocation.InterService;
using CowLocation.Storage;
using Microsoft.AspNetCore.Mvc;

namespace CowLocation.Controllers
{
    /// <summary>
    /// Concern: To receive and handle request from client.
    /// </summary>
    public class LocationController : Controller
    {
        private readonly IStorage _storage;
        //private readonly MasterData _masterDataService;
        private readonly IMasterData _masterDataService;

        // Concern: Constructor receives dependencies
        public LocationController(IStorage storage, IMasterData masterDataService)
        {
            _storage = storage;
            _masterDataService = masterDataService;
        }

        /// <summary>
        /// API to receive ear tag and GRP coordinates from cow
        /// </summary>
        /// <param name="dto">Input data</param>
        /// <returns></returns>
        [HttpPost("/Location/Create/")]
        public IActionResult LocationCreate([FromBody] LocationCreate dto)
        {
            try
            {
                _storage.LocationCreateUpdate(dto.EarTag, dto.Latitude, dto.Longitude);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// API to report latest position of a specifike cow
        /// </summary>
        /// <param name="earTag">Ear tag of the cow</param>
        /// <returns>LocationReport that contains name and GPS coordinates of the cow.</returns>
        [HttpGet("/Location/Report/{earTag}/")]
        public IActionResult LocationReport(string earTag)
        {
            var cowName = _masterDataService.GetCowName(earTag);    //Her brewak point-> husk
            var location = _storage.LocationRead(earTag);

            var dto = new LocationReport
            {
                Name = cowName,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };

            return Ok(dto);
        }
    }
}