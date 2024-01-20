using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet]
        [Route("jobSkill/{companyId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobSkill(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobSkillPoco>), 200)]
        public ActionResult GetAllCompanyJobSkill()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("jobSkill")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("jobSkill")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("jobSkill")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
