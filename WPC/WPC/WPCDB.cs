using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WPC
{
    public static class WPCDB
    {
        public static SqlConnection GetConnection()
        {
            string connStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\VisualStudio\WPC\WPC\WPC.mdf;Integrated Security=True;User Instance=True";
            SqlConnection conn = new SqlConnection(connStr);
            return conn;
        }
        public static void AddEmployee(string firstName, string lastName, string department, string phone, string email)
        {
            string insStmt = "INSERT INTO EMPLOYEE (FirstName, LastName, Department, Phone, Email)" + "VALUES(@firstName, @lastName, @department, @phone, @email)";
            SqlConnection conn = GetConnection();
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@firstName", firstName);
            insCmd.Parameters.AddWithValue("@lastName", lastName);
            insCmd.Parameters.AddWithValue("@department", department);
            insCmd.Parameters.AddWithValue("@phone", phone);
            insCmd.Parameters.AddWithValue("@email", email);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) {throw ex;}
            finally { conn.Close(); }
        }
        public static List<Employee> GetEmployee()
        {
            List<Employee> employeeList = new List<Employee>();
            SqlConnection conn = GetConnection();
            string selStmt = "SELECT * FROM EMPLOYEE ORDER BY LastName,FirstName";
            SqlCommand selCmd = new SqlCommand(selStmt, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = selCmd.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee();
                    employee.EmployeeNumber = (int)reader["EmployeeNumber"];
                    employee.FirstName = reader["FirstName"].ToString();
                    employee.LastName = reader["LastName"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.Phone = reader["Phone"].ToString();
                    employee.Email = reader["Email"].ToString();
                    employeeList.Add(employee);

                }
                reader.Close();

            }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
            return employeeList;
        }
    }
}
