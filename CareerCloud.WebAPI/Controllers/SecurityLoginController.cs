using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        [HttpGet]
        [Route("login/{companyId}")]
        [ProducesResponseType(typeof(SecurityLoginPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginPoco>), 200)]
        public ActionResult GetAllSecurityLogin()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(201)]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("login")]
        [ProducesResponseType(200)]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("login")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
