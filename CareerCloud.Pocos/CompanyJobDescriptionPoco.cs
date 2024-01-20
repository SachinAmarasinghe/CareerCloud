
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Jobs_Descriptions")]
    public class CompanyJobDescriptionPoco:IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Key]
        public Guid Job { get; set; }

        [Column("Job_Name")]
        public string? JobName { get; set; }

        [Column("Job_Descriptions")]
        public string? JobDescriptions { get; set; }

        [Column("Time_Stamp")]
        public byte[]? TimeStamp { get; set; }

        public virtual CompanyProfilePoco CompanyProfiles { get; set; } = null!;
        public virtual ICollection<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
        public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
