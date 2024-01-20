using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeController()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }

        [HttpGet]
        [Route("resume/{applicantEducationId}")]
        [ProducesResponseType(typeof(ApplicantResumePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicantResumePoco>), 200)]
        public ActionResult GetAllApplicantResume()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("resume")]
        [ProducesResponseType(201)]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"skill/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("resume")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("resume")]
        [ProducesResponseType(204)]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
