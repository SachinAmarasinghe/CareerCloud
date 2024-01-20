using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }

        [HttpGet]
        [Route("location/{companyId}")]
        [ProducesResponseType(typeof(CompanyLocationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid companyId)
        {
            var poco = _logic.Get(companyId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CompanyLocationPoco>), 200)]
        public ActionResult GetAllCompanyLocation()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("location")]
        [ProducesResponseType(201)]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"workhistory/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("location")]
        [ProducesResponseType(200)]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("location")]
        [ProducesResponseType(204)]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
