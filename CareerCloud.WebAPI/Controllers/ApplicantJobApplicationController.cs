using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _logic;
        public ApplicantJobApplicationController()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }


        [HttpGet]
        [Route("jobApplication/{applicantEducationId}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantJobApplication(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicantJobApplicationPoco>), 200)]
        public ActionResult GetAllApplicantJobApplication()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("jobApplication")]
        [ProducesResponseType(201)]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"skill/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("jobApplication")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("jobApplication")]
        [ProducesResponseType(204)]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
