using Microsoft.Extensions.Configuration;
using samplewebapp_ASAP.Net.Model;
using System.Data;
using System.Data.SqlClient;

namespace SampleWebApp.Model
{
    public class DAL
    {
        public List<User> GetUsers(IConfiguration _configuration)
        {
            List<User> users = new List<User>();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        User user = new User();
                        user.Id = Convert.ToString(dt.Rows[i]["Id"]);
                        user.FirstName = Convert.ToString(dt.Rows[i]["FirstName"]);
                        user.LastName = Convert.ToString(dt.Rows[i]["LastName"]);
                        user.Designation = Convert.ToString(dt.Rows[i]["Designation"]);
                        user.DateOfJoin = Convert.ToDateTime(dt.Rows[i]["DateOfJoin"]);
                        user.Salary = Convert.ToDecimal(dt.Rows[i]["Salary"]);
                        user.Gender = Convert.ToString(dt.Rows[i]["Gender"]);
                        user.State = Convert.ToString(dt.Rows[i]["State"]);
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public int AddUser(User user, IConfiguration _configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO TblUsers VALUES('" + user.FirstName + "', '" + user.LastName + "', '" + user.Designation + "', '" + user.DateOfJoin + "', '" + user.Salary + "', '" + user.Gender + "', '" + user.State + "')", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

        public User GetUser(string id, IConfiguration _configuration)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers WHERE ID = '" + id + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    user.Id = Convert.ToString(dt.Rows[0]["Id"]);
                    user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                    user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                    user.Designation = Convert.ToString(dt.Rows[0]["Designation"]);
                    user.DateOfJoin = Convert.ToDateTime(dt.Rows[0]["DateOfJoin"]);
                    user.Salary = Convert.ToDecimal(dt.Rows[0]["Salary"]);
                    user.Gender = Convert.ToString(dt.Rows[0]["Gender"]);
                    user.State = Convert.ToString(dt.Rows[0]["State"]);
                }
            }
            return user;
        }

        public int UpdateUser(User user, IConfiguration _configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("Update TblUsers SET FirstName = '" + user.FirstName + "', LastName = '" + user.LastName + "', Designation = '" + user.Designation + "', DateOfJoin = '" + user.DateOfJoin + "', Salary = '" + user.Salary + "', Gender = '" + user.Gender + "', State = '" + user.State + "' WHERE ID = '" + user.Id + "'", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

        public int DeleteUser(string id, IConfiguration _configuration)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM TblUsers WHERE ID = '" + id + "'", con);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
            return i;
        }

    }
}
