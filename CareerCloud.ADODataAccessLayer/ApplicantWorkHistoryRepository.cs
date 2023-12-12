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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Work_History(Id,Applicant,Company_Name,Country_Code,Location,Job_Title,Job_Description,Start_Month,Start_Year,End_Month,End_Year)" 
                        + "values (@Id,@Applicant,@CompanyName,@CountryCode,@Location,@JobTitle,@JobDescription,@StartMonth,@StartYear,@EndMonth,@EndYear)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", poco.EndYear);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Applicant_Work_History", conn);
                SqlDataReader r = cmd.ExecuteReader();

                var ApplicantWorkHistoryList = new List<ApplicantWorkHistoryPoco>();

                while (r.Read())
                {
                    var poco = new ApplicantWorkHistoryPoco
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        CompanyName = (string)r["Company_Name"],
                        CountryCode = (string)r["Country_Code"],
                        Location = (string)r["Location"],
                        JobTitle = (string)r["Job_Title"],
                        JobDescription = (string)r["Job_Description"],
                        StartMonth = (short)r["Start_Month"],
                        StartYear = (int)r["Start_Year"],
                        EndMonth = (short)r["End_Month"],
                        EndYear = (int)r["End_Year"]
                    };

                    ApplicantWorkHistoryList.Add(poco);
                }

                r.Close();
                return ApplicantWorkHistoryList;
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM Applicant_Work_History WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE Applicant_Work_History " +
                        "SET Company_Name = @CompanyName, Country_Code = @CountryCode, Location = @Location, " +
                        "Job_Title = @JobTitle, Job_Description = @JobDescription, Start_Month = @StartMonth, " +
                        "Start_Year = @StartYear, End_Month = @EndMonth, End_Year = @EndYear " +
                        "WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@CompanyName", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@JobTitle", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@JobDescription", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@StartMonth", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@StartYear", poco.StartYear);
                    cmd.Parameters.AddWithValue("@EndMonth", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@EndYear", poco.EndYear);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
