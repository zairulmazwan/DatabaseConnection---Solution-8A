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
    public class ViewDataModel : PageModel
    {
        [BindProperty]
        public List<Student> StudentRec { get; set; }
        public IActionResult OnGet()
        {
            string DbConnection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DatabaseConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Student ORDER BY StudentName"; // try order by Name

                SqlDataReader reader = command.ExecuteReader();

                StudentRec = new List<Student>();
                // Call Read before accessing data.
                while (reader.Read())//keep reading while there is a record
                {
                    Student rec = new Student(); //a local var to hold a record temporarily
                    rec.Id = reader.GetInt32(0);
                    rec.StudentID = reader.GetString(1); //make sure the data type is matched
                    rec.StudentName = reader.GetString(2);
                    rec.StudentLName = reader.GetString(3);
                    rec.StudentCourse = reader.GetString(4);
                    rec.StudentStatus = reader.GetString(5);
                    rec.StudentYear = reader.GetInt32(6);
                    rec.StudentLevel = reader.GetInt32(7);
                     
                    StudentRec.Add(rec); //the temporary var of rec which consists of a record is added to the the list

                }

                // Call Close when done reading.
                reader.Close();
            }
                return Page();//return to the page after the result is obtained
        }
    }
}
