using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;
using WebApiProject.Repository;

namespace WebApiProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    public class SensorDetailsNewController : ControllerBase
    {
        private readonly ISensorDetailsRepository _repository;
        public SensorDetailsNewController(ISensorDetailsRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This method will add sensor record to database.
        /// </summary>
        /// <param name= sensorDetails></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddSensor")]
        [MapToApiVersion("1.0")]
        public IActionResult AddRecord(SensorDetails sensorDetails)
        {
                if (ModelState.IsValid)
                {
                    var result = _repository.AddSensorRecords(sensorDetails);
                    return Ok(result);
                }
                return BadRequest(ModelState);
        }
    }
}