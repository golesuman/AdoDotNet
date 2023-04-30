using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using adonet.Models;

namespace adonet.Controllers
{
    public class StudentController : Controller
    {
        private readonly string _connectionString = "Data Source=test.db;";

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                string sql = "INSERT INTO Students (Name, Roll, Address) VALUES (@Name, @Roll, @Address);";
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", student.Name);
                        command.Parameters.AddWithValue("@Roll", student.Roll);
                        command.Parameters.AddWithValue("@Address", student.Address);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(student);
        }

        public IActionResult ShowStudents()
        {
            List<Student> students = new();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                // Open the connection
                connection.Open();

                using var command = new SQLiteCommand("SELECT Name, Address, Roll FROM Students;", connection);
                // Execute the command and retrieve the data
                using var reader = command.ExecuteReader();
                Console.WriteLine("loading the students");
                // Console.WriteLine(reader.GetString(1));

                while (reader.Read())
                {
                    Student student = new Student()
                    {
                        Name = reader.GetString(0),
                        Address = reader.GetString(1),
                        Roll = (int)reader.GetInt64(2)
                    };

                    students.Add(student);

                }


            }
            return View(students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
