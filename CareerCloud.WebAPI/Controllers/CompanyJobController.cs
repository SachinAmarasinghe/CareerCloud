using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("job/{companyId}")]
        [ProducesResponseType(typeof(CompanyJobPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJob(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyJobPoco>), 200)]
        public ActionResult GetAllCompanyJob()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("job")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("job")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("job")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
