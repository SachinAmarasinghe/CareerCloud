using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>

    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";

        public void Add(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items) {  
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Educations(Id,Applicant, Major,Certificate_Diploma,Start_Date,Completion_Date,Completion_Percent) " +
                                        "values (@Id,@Applicant, @Major, @CertificateDiploma,@StartDate,@CompletionDate,@CompletionPercent)";

                    cmd.Parameters.AddWithValue("@Id",poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@CertificateDiploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", poco.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@CompletionPercent", poco.CompletionPercent);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = (new SqlCommand("Select * from Applicant_Educations", conn)).ExecuteReader();

                var ApplicantEducations = new List<ApplicantEducationPoco>();
                while (r.Read())
                {
                    ApplicantEducations.Add(new ApplicantEducationPoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        Major = (string)r["Major"],
                        CertificateDiploma = (string)r["Certificate_Diploma"],
                        StartDate = (DateTime)r["Start_Date"],
                        CompletionDate = (DateTime)r["Completion_Date"],
                        CompletionPercent = (byte)r["Completion_Percent"],
                        TimeStamp = (byte[])r["Time_Stamp"]

                    });
                }
                return ApplicantEducations;
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Delete from Applicant_Educations where Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var poco in items) { 
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "Update Applicant_Educations set" +
                        " Applicant = @Applicant," +
                        " Major = @Major," +
                        " Certificate_Diploma = @CertificateDiploma," +
                        " Start_Date = @StartDate," +
                        " Completion_Date = @CompletionDate," +
                        " Completion_Percent = @CompletionPercent" +
                        " where id= @Id";


                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@CertificateDiploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@StartDate", poco.StartDate);
                    cmd.Parameters.AddWithValue("@CompletionDate", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@CompletionPercent", poco.CompletionPercent);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.ExecuteNonQuery();
                }
                
            }
        }
    }
}