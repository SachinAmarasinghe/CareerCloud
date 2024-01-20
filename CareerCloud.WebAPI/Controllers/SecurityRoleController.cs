using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }

        [HttpGet]
        [Route("profile/{companyId}")]
        [ProducesResponseType(typeof(SecurityRolePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SecurityRolePoco>), 200)]
        public ActionResult GetAllSecurityRole()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("role")]
        [ProducesResponseType(201)]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("role")]
        [ProducesResponseType(200)]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("role")]
        [ProducesResponseType(204)]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
