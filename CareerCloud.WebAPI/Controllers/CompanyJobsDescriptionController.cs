using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobsDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("jobDescription/{companyId}")]
        [ProducesResponseType(typeof(CompanyJobDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobsDescription(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobDescriptionPoco>), 200)]
        public ActionResult GetAllCompanyJobDescription()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("jobDescription")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("jobDescription")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("jobDescription")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
