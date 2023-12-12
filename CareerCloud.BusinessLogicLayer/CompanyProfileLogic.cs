using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{

    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach (var poco in pocos)
            {
                if (poco.CompanyWebsite == null ||!(poco.CompanyWebsite.EndsWith(".ca", StringComparison.OrdinalIgnoreCase) || poco.CompanyWebsite.EndsWith(".com", StringComparison.OrdinalIgnoreCase) || poco.CompanyWebsite.EndsWith(".biz", StringComparison.OrdinalIgnoreCase)))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with thefollowing extensions – \".ca\",\".com\", \".biz\""));
                }

                if (poco.ContactPhone == null || !IsValidPhoneNumber(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Must correspond to a valid phonenumber (e.g. 416-555-1234)"));
                }

            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^\d{3}-\d{3}-\d{4}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
