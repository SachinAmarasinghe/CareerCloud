﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Country_Codes")]
    public class SystemCountryCodePoco : IPoco
    {

        [Key]
        [Column("Code")]
        public string? Code { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Code")]
        public virtual ICollection<ApplicantProfilePoco>? ApplicantProfiles { get; set; }
        public virtual ICollection<ApplicantWorkHistoryPoco>? ApplicantWorkHistories { get; set; }
        Guid IPoco.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
