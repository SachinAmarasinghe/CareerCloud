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
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Logins_Log(Id,Login,Source_IP,Logon_Date,Is_Succesful) " +
                                        "values (@Id,@Login,@SourceIP,@LogonDate,@IsSuccesful)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSuccesful", poco.IsSuccesful);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Security_Logins_Log", conn);
                SqlDataReader r = cmd.ExecuteReader();

                var securityLoginsLogs = new List<SecurityLoginsLogPoco>();

                while (r.Read())
                {
                    var poco = new SecurityLoginsLogPoco
                    {
                        Id = (Guid)r["Id"],
                        Login = (Guid)r["Login"],
                        SourceIP = r["Source_IP"] as string,
                        LogonDate = (DateTime)r["Logon_Date"],
                        IsSuccesful = (bool)r["Is_Succesful"]
                    };

                    securityLoginsLogs.Add(poco);
                }

                r.Close();
                return securityLoginsLogs;
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM Security_Logins_Log WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE Security_Logins_Log SET Login = @Login,Source_IP = @SourceIP,Logon_Date = @LogonDate, Is_Succesful = @IsSuccesful WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@SourceIP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@LogonDate", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@IsSuccesful", poco.IsSuccesful);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
