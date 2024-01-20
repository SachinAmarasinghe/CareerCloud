using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {

        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }

        [HttpGet]
        [Route("loginsRole/{companyId}")]
        [ProducesResponseType(typeof(SecurityLoginsRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SecurityLoginsRolePoco>), 200)]
        public ActionResult GetAllSecurityLoginRole()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("loginsRole")]
        [ProducesResponseType(201)]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("loginsRole")]
        [ProducesResponseType(200)]
        public ActionResult PutSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("loginsRole")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
