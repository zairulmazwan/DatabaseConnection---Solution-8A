using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DatabaseConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DatabaseConnection.Pages.Students
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }
        public IActionResult OnGet(int? Id)
        {
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Student WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", Id);

                SqlDataReader reader = command.ExecuteReader();
                Student = new Student();
                while (reader.Read())
                {
                    Student.Id = reader.GetInt32(0);
                    Student.StudentID = reader.GetString(1);
                    Student.StudentName = reader.GetString(2);
                    Student.StudentLName = reader.GetString(3);
                    Student.StudentCourse = reader.GetString(4);
                    Student.StudentStatus = reader.GetString(5);
                    Student.StudentYear = reader.GetInt32(6);
                    Student.StudentLevel = reader.GetInt32(7);
                }

            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"UPDATE Student Set StudentID=@StdID, StudentName=@StdName, StudentLName=@StdLName, StudentCourse=@StdCourse, StudentStatus=@StdStatus, StudentYear=@StdYear, StudentLevel=@StdLevel WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", Student.Id);
                command.Parameters.AddWithValue("@StdID", Student.StudentID);
                command.Parameters.AddWithValue("@StdName", Student.StudentName);
                command.Parameters.AddWithValue("@StdLName", Student.StudentLName);
                command.Parameters.AddWithValue("@StdCourse", Student.StudentCourse);
                command.Parameters.AddWithValue("@StdStatus", Student.StudentStatus);
                command.Parameters.AddWithValue("@StdYear", Student.StudentYear);
                command.Parameters.AddWithValue("@StdLevel", Student.StudentLevel);

                SqlDataReader reader = command.ExecuteReader();
            }

                return RedirectToPage("/index");
        }
    }
}
