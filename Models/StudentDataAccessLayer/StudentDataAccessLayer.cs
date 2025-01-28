using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApplicationWithDapper.Models.StudentDataAccessLayer
{
    public class StudentDataAccessLayer
    {
        //string connectionString = ConnectionString.CName;
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public StudentDataAccessLayer(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = configuration.GetConnectionString("WebApplicationWithDapperContext");
        }
        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> lstStudent = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Student student = new Student();
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"]?.ToString() ?? string.Empty;
                    student.LastName = rdr["LastName"]?.ToString() ?? string.Empty;
                    student.Email = rdr["Email"]?.ToString() ?? string.Empty;
                    student.Mobile = rdr["Mobile"]?.ToString() ?? string.Empty;
                    student.Address = rdr["Address"]?.ToString() ?? string.Empty;

                    lstStudent.Add(student);
                }
                con.Close();
            }
            return lstStudent;
        }
        public void AddStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void UpdateStudent(Student student)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", student.Id);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Mobile", student.Mobile);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Student GetStudentData(int? id)
        {
            Student student = new Student();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Student WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    student.Id = Convert.ToInt32(rdr["Id"]);
                    student.FirstName = rdr["FirstName"]?.ToString() ?? string.Empty;
                    student.LastName = rdr["LastName"]?.ToString() ?? string.Empty;
                    student.Email = rdr["Email"]?.ToString() ?? string.Empty;
                    student.Mobile = rdr["Mobile"]?.ToString() ?? string.Empty;
                    student.Address = rdr["Address"]?.ToString() ?? string.Empty;
                }
            }
            return student;
        }

        public void DeleteStudent(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteStudent", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
