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
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Student StudentRec { get; set; }

        public IActionResult OnGet(int? id)
        {
            Console.WriteLine("studID : " + id);
            
            if (id == null)
            {
                return NotFound();
            }
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Student WHERE Id = @id"; // try order by Name
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();
                StudentRec = new Student();
                while (reader.Read())
                {
                    StudentRec.Id = reader.GetInt32(0);
                    StudentRec.StudentID = reader.GetString(1); //make sure the data type is matched
                    StudentRec.StudentName = reader.GetString(2);
                    StudentRec.StudentLName = reader.GetString(3);
                    StudentRec.StudentCourse = reader.GetString(4);
                    StudentRec.StudentStatus = reader.GetString(5);
                    StudentRec.StudentYear = reader.GetInt32(6);
                    StudentRec.StudentLevel = reader.GetInt32(7);
                }
            }

                return Page();
        }

        public IActionResult OnPost()
        {
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();
            Console.WriteLine("Id to be deleted : "+ StudentRec.Id);
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"DELETE Student WHERE Id = @StdID";
                command.Parameters.AddWithValue("@StdID", StudentRec.Id);
                command.ExecuteNonQuery();
            }

            ViewData["Message"] = "A record has been deleted!";
            return Page();
        }


    }
}
