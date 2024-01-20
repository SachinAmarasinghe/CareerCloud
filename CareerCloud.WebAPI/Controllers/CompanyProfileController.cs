using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        [HttpGet]
        [Route("profile/{companyId}")]
        [ProducesResponseType(typeof(CompanyProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyProfilePoco>), 200)]
        public ActionResult GetAllCompanyProfile()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
