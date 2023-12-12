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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string connectionString = "Server=SACHIN-ZENBOOK\\SQL2K22HUMBER; Database=JOB_PORTAL_DB; Integrated Security=true;";
        public void Add(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "Insert into Company_Locations(Id,Company,Country_Code,State_Province_Code,Street_Address,City_Town,Zip_Postal_Code) " +
                                        "values (@Id,@Company,@CountryCode,@StateProvinceCode,@StreetAddress,@CityTown,@ZipPostalCode)";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
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

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Company_Locations", conn);
                SqlDataReader r = cmd.ExecuteReader();

                var companyLocations = new List<CompanyLocationPoco>();

                while (r.Read())
                {
                    var poco = new CompanyLocationPoco
                    {
                        Id = (Guid)r["Id"],
                        Company = (Guid)r["Company"],
                        CountryCode = r["Country_Code"] as string,
                        Province = r["State_Province_Code"] as string,
                        Street = r["Street_Address"] as string,
                        City = r["City_Town"] as string,
                        PostalCode = r["Zip_Postal_Code"] as string
                    };

                    companyLocations.Add(poco);
                }

                r.Close();
                return companyLocations;
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "DELETE FROM Company_Locations WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                foreach (var poco in items)
                {
                    var cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "UPDATE Company_Locations " +
                        "SET Country_Code = @CountryCode, State_Province_Code = @StateProvinceCode, " +
                        "Street_Address = @Street, City_Town = @City, Zip_Postal_Code = @PostalCode " +
                        "WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@CountryCode", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@StateProvinceCode", poco.Province);
                    cmd.Parameters.AddWithValue("@Street", poco.Street);
                    cmd.Parameters.AddWithValue("@City", poco.City);
                    cmd.Parameters.AddWithValue("@PostalCode", poco.PostalCode);
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
