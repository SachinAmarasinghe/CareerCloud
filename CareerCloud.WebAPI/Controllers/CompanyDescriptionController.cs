using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;
        public CompanyDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("description/{companyId}")]
        [ProducesResponseType(typeof(CompanyDescriptionPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyDescription(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyDescriptionPoco>), 200)]
        public ActionResult GetAllCompanyDescription()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("description")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("description")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("description")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
