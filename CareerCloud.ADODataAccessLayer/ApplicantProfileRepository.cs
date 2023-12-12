using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Profiles(Id,Login,Current_Salary,Current_Rate,Currency,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code) " +
                                        "values (@Id,@Login,@CurrentSalary,@CurrentRate,@Currency,@CountryCode,@StateProvinceCode,@StreetAddress,@CityTown,@ZipPostalCode)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.Country);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", poco.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", poco.Street);
                    cmd.Parameters.AddWithValue("@CityTown", poco.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", poco.PostalCode);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = (new SqlCommand("SELECT * FROM Applicant_Profiles", conn)).ExecuteReader();

                var applicantProfiles = new List<ApplicantProfilePoco>();
                while (r.Read())
                {
                    applicantProfiles.Add(new ApplicantProfilePoco()
                    {
                        Id = (Guid)r["Id"],
                        Login = (Guid)r["Login"],
                        CurrentSalary = (decimal)r["Current_Salary"],
                        Currency = (string)r["Currency"],
                        CurrentRate = (decimal)r["Current_Rate"],
                        Country = (string)r["Country_Code"],
                        Province = (string)r["State_Province_Code"],
                        Street = (string)r["Street_Address"],
                        City = (string)r["City_Town"],
                        PostalCode = (string)r["Zip_Postal_Code"],
                        TimeStamp = (byte[])r["Time_Stamp"]
                    });
                }
                return applicantProfiles;
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    // Construct the DELETE SQL statement based on the Id
                    cmd.CommandText = "DELETE FROM Applicant_Profiles WHERE Id = @Id";

                    // Set the parameter value for the Id
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    // Construct the UPDATE SQL statement based on the Id
                    cmd.CommandText = "UPDATE Applicant_Profiles SET" +
                        " Login = @Login," +
                        " Current_Salary = @CurrentSalary," +
                        " Currency = @Currency," +
                        " Current_Rate = @CurrentRate," +
                        " Country_Code = @CountryCode," +
                        " State_Province_Code = @StateProvinceCode," +
                        " Street_Address = @StreetAddress," +
                        " City_Town = @CityTown," +
                        " Zip_Postal_Code = @ZipPostalCode" +
                        " WHERE Id = @Id";

                    // Set the parameter values for the update
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@CurrentSalary", poco.CurrentSalary);
                    cmd.Parameters.AddWithValue("@Currency", poco.Currency);
                    cmd.Parameters.AddWithValue("@CurrentRate", poco.CurrentRate);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.Country);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", poco.Province);
                    cmd.Parameters.AddWithValue("@StreetAddress", poco.Street);
                    cmd.Parameters.AddWithValue("@CityTown", poco.City);
                    cmd.Parameters.AddWithValue("@ZipPostalCode", poco.PostalCode);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
