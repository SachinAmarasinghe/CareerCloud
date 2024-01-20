using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeController()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet]
        [Route("countryCode/{countryCode}")]
        [ProducesResponseType(typeof(SystemCountryCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string countryCode)
        {
            var poco = _logic.Get(Guid.Parse(countryCode));
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SystemCountryCodePoco>), 200)]
        public ActionResult GetAllSystemCountryCode()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("countryCode")]
        [ProducesResponseType(201)]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Code}", poco);
        }

        [HttpPut]
        [Route("countryCode")]
        [ProducesResponseType(200)]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("countryCode")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
