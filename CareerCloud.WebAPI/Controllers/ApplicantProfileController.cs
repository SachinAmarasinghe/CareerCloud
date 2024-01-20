using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileController()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repo);
        }

        [HttpGet]
        [Route("profile/{applicantEducationId}")]
        [ProducesResponseType(typeof(ApplicantProfilePoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantProfile(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicantProfilePoco>), 200)]
        public ActionResult GetAllApplicantProfile()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(201)]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
        {
            _logic.Add(poco);
            return Created($"skill/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(204)]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }
}
