using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class HomeController : ApiController
    {
        String connectionString = "Server=RAED_COMPUTER\\SQLEXPRESS;Database=SchoolManagementSystem;Trusted_Connection=True;";
        [HttpPost]
        public string StudentSave(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            DateTime currentTime = DateTime.Now;
            currentTime.ToString("hh:mm:tt");
            SqlCommand command = new SqlCommand("StudentSave", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("Name", student.Name);
            command.Parameters.AddWithValue("Malayalam", student.Malayalam);
            command.Parameters.AddWithValue("Hindi", student.Hindi);
            command.Parameters.AddWithValue("English", student.English);
            command.Parameters.Add("@LastStudentId", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;
            command.Parameters.Add("@CreateTime", System.Data.SqlDbType.VarChar, 50).Direction = System.Data.ParameterDirection.Output;
            command.ExecuteNonQuery();
            string id = command.Parameters["@LastStudentId"].Value.ToString();
            string time = command.Parameters["@CreateTime"].Value.ToString();
            return "Inserted Sucessfully Id=" + id + " " + " CreatedTime=" + time;
        }
        [HttpPost]
        public Student StudentDetail(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentDetails", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Id", student.Id);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();


            student.Id = Convert.ToInt32(student.Id);
            //student.Id = Convert.ToInt32(reader["Id"]);
            student.Name = reader["Name"].ToString();
            student.Malayalam = Convert.ToInt32(reader["Malayalam"]);
            student.Hindi = Convert.ToInt32(reader["Hindi"]);
            student.English = Convert.ToInt32(reader["English"]);
            student.Total = Convert.ToInt32(reader["Total"]);
            student.Average = Convert.ToInt32(reader["Average"]);


            reader.Close();
            connection.Close();
            return student;

        }
        [HttpPost]
        public List<string> StudentList(Order order)
        {
            List<string> stud = new List<string>();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("StudentList", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("Order", order.Order);


            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                stud.Add(reader["Name"].ToString());





            }


            reader.Close();
            connection.Close();
            return stud;
        }
    }
}
