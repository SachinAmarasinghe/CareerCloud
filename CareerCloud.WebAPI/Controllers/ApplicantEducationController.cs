using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }

        [HttpGet]
        [Route("education/{applicantEducationId}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicantEducationPoco>), 200)]
        public ActionResult GetAllApplicantEducation()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("education")]
        [ProducesResponseType(201)]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"education/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("education")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("education")]
        [ProducesResponseType(204)]
        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
