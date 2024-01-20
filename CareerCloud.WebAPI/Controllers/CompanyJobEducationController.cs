using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationController()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }

        [HttpGet]
        [Route("jobEducation/{companyId}")]
        [ProducesResponseType(typeof(CompanyJobEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobEducationPoco>), 200)]
        public ActionResult GetAllCompanyJobEducation()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("jobEducation")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("jobEducation")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("jobEducation")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
