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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into System_Country_Codes(Code,Name) " +
                                        "values (@Code,@Name)";

                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataReader r = (new SqlCommand("Select * from System_Country_Codes", conn)).ExecuteReader();

                var SystemCountryCodes = new List<SystemCountryCodePoco>();
                while (r.Read())
                {
                    SystemCountryCodes.Add(new SystemCountryCodePoco()
                    {
                        Code = (string)r["Code"],
                        Name = r["Name"] as string
                    });
                }
                return SystemCountryCodes;
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM System_Country_Codes WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE System_Country_Codes SET Name = @Name WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
