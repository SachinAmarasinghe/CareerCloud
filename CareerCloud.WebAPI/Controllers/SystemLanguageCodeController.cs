using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }

        [HttpGet]
        [Route("languageCode/{companyId}")]
        [ProducesResponseType(typeof(SystemLanguageCodePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSystemLanguageCode(string companyId)
        {
            var poco = _logic.Get(Guid.Parse(companyId));
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SystemLanguageCodePoco>), 200)]
        public ActionResult GetAllSystemLanguageCode()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("languageCode")]
        [ProducesResponseType(201)]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("languageCode")]
        [ProducesResponseType(200)]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("languageCode")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
