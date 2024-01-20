using Microsoft.AspNetCore.Mvc;
using CareerCloud.Pocos;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillController()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        [HttpGet]
        [Route("skill/{applicantEducationId}")]
        [ProducesResponseType(typeof(ApplicantSkillPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid applicantEducationId)
        {
            var poco = _logic.Get(applicantEducationId);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ApplicantSkillPoco>), 200)]
        public ActionResult GetAllApplicantSkill()
        {
            var pocos = _logic.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("skill")]
        [ProducesResponseType(201)]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Add(poco);
            return Created($"skill/{poco[0].Id}", poco);
        }

        [HttpPut]
        [Route("skill")]
        [ProducesResponseType(200)]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("skill")]
        [ProducesResponseType(204)]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Delete(poco);
            return NoContent();
        }
    }

    
}
