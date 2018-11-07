using MasterData.Dto;
using MasterData.Storage;
using Microsoft.AspNetCore.Mvc;

namespace MasterData.Controllers
{
    /// <summary>
    /// Concern: To receive and handle request from client.
    /// </summary>
    public class CowController : Controller
    {
        private readonly IStorage _storage;

        // Concern: Constructor receives dependencies
        public CowController(IStorage storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// API to receive data needed to create the "cow account"
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Ok if saved and BadRequest if not</returns>
        [HttpPost("/Cow/Create/")]
        public IActionResult CowCreate([FromBody] CowCreate dto)
        {
            try
            {                                                                   //Her brewak point-> husk 
                _storage.Store(dto.EarTag, dto.Name, dto.Birthday);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// API to get the data on a cow with a specifike ear tag
        /// </summary>
        /// <param name="earTag"></param>
        /// <returns>CowRead containing name and birthday</returns>
        [HttpGet("/Cow/Read/{earTag}/")]
        public ActionResult<CowRead> CowRead(string earTag)
        {
            try
            {
                var cow = _storage.Read(earTag);

                var dto = new CowRead
                {
                    Name = cow.Name,
                    Birthday = cow.Birthday
                };

                return Ok(dto);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
