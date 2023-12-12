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
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Applicant_Resumes(Id,Applicant,Resume,Last_Updated) " +
                                        "values (@Id,@Applicant,@Resume,@LastUpdated)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@LastUpdated", poco.LastUpdated);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = (new SqlCommand("SELECT * FROM Applicant_Resumes", conn)).ExecuteReader();

                var ApplicantResumes = new List<ApplicantResumePoco>();
                while (r.Read())
                {
                    ApplicantResumes.Add(new ApplicantResumePoco()
                    {
                        Id = (Guid)r["Id"],
                        Applicant = (Guid)r["Applicant"],
                        Resume = (string)r["Resume"],
                        LastUpdated = r["Last_Updated"] as DateTime?
                    });
                }
                return ApplicantResumes;
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Delete from Applicant_Resumes where Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Update Applicant_Resumes set Applicant = @Applicant, Resume = @Resume, Last_Updated = @LastUpdated WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", poco.Resume);
                    cmd.Parameters.AddWithValue("@LastUpdated", poco.LastUpdated);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
