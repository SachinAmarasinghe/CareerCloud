using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("loginsLog/{companyId}")]
        [ProducesResponseType(typeof(SecurityLoginsLogPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginLog(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginsLogPoco>), 200)]
        public ActionResult GetAllSecurityLoginLog()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("loginsLog")]
        [ProducesResponseType(201)]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("loginsLog")]
        [ProducesResponseType(200)]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("loginsLog")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
