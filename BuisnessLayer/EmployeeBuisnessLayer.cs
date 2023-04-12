using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer
{
    public class EmployeeBuisnessLayer
    {
        string Connection = constring.Cs;
        public IEnumerable<Employee> GetEmployees(string e, int stateId)
        {
            List<Employee> emp = new List<Employee>();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                SqlCommand cmd = new SqlCommand("spSearch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                if(string.IsNullOrEmpty(e))
                {
                    e = "";
                }
                cmd.Parameters.AddWithValue("@SearchTerm", e);
                cmd.Parameters.AddWithValue("@StateId", stateId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.EmployeeGender = rdr["EmployeeGender"].ToString();
                    employee.EmployeeDesignation = rdr["EmployeeDesignation"].ToString();
                    employee.Email_id = rdr["Email_id"].ToString();
                    employee.StateName = rdr["StateName"].ToString();
                    emp.Add(employee);
                }
                con.Close();
            }
            return emp;
        }
        public void AddEmployee(Employee employee)
        {
            using(SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spAddEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeGender",employee.EmployeeGender);
                cmd.Parameters.AddWithValue("@EmployeeDesignation",employee.EmployeeDesignation);
                cmd.Parameters.AddWithValue("@Email_id", employee.Email_id);
                cmd.Parameters.AddWithValue("@StateId", employee.StateId);

                cmd.ExecuteNonQuery();
            }
        }
        public void Updateemployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spUpdateEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@EmployeeGender", employee.EmployeeGender);
                cmd.Parameters.AddWithValue("@EmployeeDesignation", employee.EmployeeDesignation);
                cmd.Parameters.AddWithValue("@Email_id", employee.Email_id);
                cmd.Parameters.AddWithValue("@StateId", employee.StateId);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void DeleteEmployee(int id)
        {
            using(SqlConnection con = new SqlConnection(Connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID",id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public Employee GetEmployeeDetails(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                SqlCommand cmd = new SqlCommand("Select * from Employee where EmployeeID ="+id, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.EmployeeName = rdr["EmployeeName"].ToString();
                    employee.EmployeeGender = rdr["EmployeeGender"].ToString();
                    employee.EmployeeDesignation = rdr["EmployeeDesignation"].ToString();
                    employee.Email_id = rdr["Email_id"].ToString();
                }
                con.Close();
            }
            return employee;
        }
        public List<States> GetStates()
        {
            List<States> states = new List<States>();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                string qry = "SELECT StateId, StateName from States order BY StateName";
                using(SqlCommand cmd = new SqlCommand(qry,con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        States st = new States();
                        st.StateId = Convert.ToInt32(reader["StateId"]);
                        st.StateName = reader["StateName"].ToString();
                        states.Add(st);
                    }
                }
            }return states;
        }
        public int CheckEmail(Employee employee)
        {
            using(SqlConnection con = new SqlConnection(Connection))
            {
                int result = 0;
                SqlCommand cmd = new SqlCommand("sp_checkemail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                // cmd.Parameters.AddWithValue("@Id", emp.Id);

                cmd.Parameters.AddWithValue("@Email_id", employee.Email_id);

                con.Open();
                result = (int)cmd.ExecuteScalar();
                con.Close();
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }
    }
}
