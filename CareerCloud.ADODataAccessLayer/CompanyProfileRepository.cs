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
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Profiles(Id,Registration_Date,Company_Website,Contact_Phone,Contact_Name,Company_Logo) " +
                                        "values (@Id,@RegistrationDate,@CompanyWebsite,@ContactPhone,@ContactName,@CompanyLogo)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Company_Profiles", conn);
                SqlDataReader r = cmd.ExecuteReader();

                var companyProfiles = new List<CompanyProfilePoco>();

                while (r.Read())
                {
                    var poco = new CompanyProfilePoco
                    {
                        Id = (Guid)r["Id"],
                        RegistrationDate = (DateTime)r["Registration_Date"],
                        CompanyWebsite = r["Company_Website"] as string,
                        ContactPhone = r["Contact_Phone"] as string,
                        ContactName = r["Contact_Name"] as string,
                        CompanyLogo = r["Company_Logo"] as byte[]
                    };

                    companyProfiles.Add(poco);
                }

                r.Close();
                return companyProfiles;
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM Company_Profiles WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE Company_Profiles " +
                        "SET Registration_Date = @RegistrationDate, Company_Website = @CompanyWebsite, " +
                        "Contact_Phone = @ContactPhone, Contact_Name = @ContactName, Company_Logo = @CompanyLogo " +
                        "WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@RegistrationDate", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CompanyWebsite", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@ContactPhone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@ContactName", poco.ContactName);
                    cmd.Parameters.AddWithValue("@CompanyLogo", poco.CompanyLogo);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
