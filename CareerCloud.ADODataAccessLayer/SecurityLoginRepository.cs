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
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Security_Logins(Id,Login,Password,Created_Date,Password_Update_Date,Agreement_Accepted_Date,Is_Locked,Is_Inactive,Email_Address,Phone_Number,Full_Name,Force_Change_Password,Prefferred_Language) " +
                                        "values (@Id,@Login,@Password,@CreatedDate,@PasswordUpdateDate,@AgreementAcceptedDate,@IsLocked,@IsInactive,@EmailAddress,@PhoneNumber,@FullName,@ForceChangePassword,@PrefferredLanguage)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", poco.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdateDate", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementAcceptedDate", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@EmailAddress", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", poco.FullName);
                    cmd.Parameters.AddWithValue("@ForceChangePassword", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@PrefferredLanguage", poco.PrefferredLanguage);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Security_Logins", conn);
                SqlDataReader r = cmd.ExecuteReader();

                var securityLogins = new List<SecurityLoginPoco>();

                while (r.Read())
                {
                    var poco = new SecurityLoginPoco
                    {
                        Id = (Guid)r["Id"],
                        Login = r["Login"] as string,
                        Password = r["Password"] as string,
                        Created = (DateTime)r["Created_Date"],
                        PasswordUpdate = r["Password_Update_Date"] as DateTime?,
                        AgreementAccepted = r["Agreement_Accepted_Date"] as DateTime?,
                        IsLocked = (bool)r["Is_Locked"],
                        IsInactive = (bool)r["Is_Inactive"],
                        EmailAddress = r["Email_Address"] as string,
                        PhoneNumber = r["Phone_Number"] as string,
                        FullName = r["Full_Name"] as string,
                        ForceChangePassword = (bool)r["Force_Change_Password"],
                        PrefferredLanguage = r["Prefferred_Language"] as string
                    };

                    securityLogins.Add(poco);
                }

                r.Close();
                return securityLogins;
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM Security_Logins WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE Security_Logins " +
                        "SET Login = @Login, Password = @Password, Created_Date = @CreatedDate, " +
                        "Password_Update_Date = @PasswordUpdateDate, Agreement_Accepted_Date = @AgreementAcceptedDate, " +
                        "Is_Locked = @IsLocked, Is_Inactive = @IsInactive, Email_Address = @EmailAddress, " +
                        "Phone_Number = @PhoneNumber, Full_Name = @FullName, " +
                        "Force_Change_Password = @ForceChangePassword, Prefferred_Language = @PrefferredLanguage " +
                        "WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Password", poco.Password);
                    cmd.Parameters.AddWithValue("@CreatedDate", poco.Created);
                    cmd.Parameters.AddWithValue("@PasswordUpdateDate", poco.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@AgreementAcceptedDate", poco.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@IsLocked", poco.IsLocked);
                    cmd.Parameters.AddWithValue("@IsInactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@EmailAddress", poco.EmailAddress);
                    cmd.Parameters.AddWithValue("@PhoneNumber", poco.PhoneNumber);
                    cmd.Parameters.AddWithValue("@FullName", poco.FullName);
                    cmd.Parameters.AddWithValue("@ForceChangePassword", poco.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@PrefferredLanguage", poco.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
