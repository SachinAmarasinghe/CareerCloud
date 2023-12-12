using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (poco.StartDate.HasValue && poco.CompletionDate.HasValue && poco.CompletionDate.Value < poco.StartDate.Value)
                {
                    exceptions.Add(new ValidationException(109, $"CompletionDate for ApplicantEducation {poco.Id} cannot be earlier than StartDate."));
                }

                if (poco.StartDate.HasValue && poco.StartDate.Value > DateTime.Today)
                {
                    exceptions.Add(new ValidationException(108, $"StartDate for ApplicantEducation {poco.Id} cannot be greater than today."));
                }

                if (string.IsNullOrWhiteSpace(poco.Major) || poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, $"Major for ApplicantEducation {poco.Id} cannot be empty or less than 3 characters."));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
