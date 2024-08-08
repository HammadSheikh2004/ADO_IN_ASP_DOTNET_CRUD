using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System;
using ADO.NET_Crud.Models;
using ADO.NET_Crud.DatabaseConnectivity;

namespace ADO.NET_Crud.DataAccess
{
    public class StudentDataAccess
    {
        private readonly IWebHostEnvironment _web;

        string cs = Connection.MyConn;

        public StudentDataAccess(IWebHostEnvironment web)
        {
            _web = web;
        }

        public void AddRecord(Student_Info student, IFormFile Std_Image)
        {
            
            try
            {
                var path = Path.Combine(_web.WebRootPath, "StudentImages");
                string fileName = Path.GetFileName(Std_Image.FileName);
                var fileExtension = Path.GetExtension(fileName);
                var ds = DateTime.Now.Millisecond;
                string imageName = "Std" + ds + fileExtension;
                var filePath = Path.Combine(path, imageName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    Std_Image.CopyTo(stream);
                }

                student.Std_Image = imageName;

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spAddStudentInfo", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@stdName", student.Std_Name);
                    cmd.Parameters.AddWithValue("@stdEmail", student.Std_Email);
                    cmd.Parameters.AddWithValue("@stdAge", student.age);
                    cmd.Parameters.AddWithValue("@stdImage", student.Std_Image);
                    cmd.Parameters.AddWithValue("@stdPhone", student.Std_Phone);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the student record.", ex);
            }
        }

        public List<Student_Info> GetData()
        {
            List<Student_Info> stdList = new List<Student_Info>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetStudent", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Student_Info student = new Student_Info();
                    student.Std_Id = Convert.ToInt32(reader["Std_Id"]);
                    student.Std_Name = reader["Std_Name"].ToString()!;
                    student.Std_Email = reader["Std_Email"].ToString()!;
                    student.age = Convert.ToInt32(reader["age"]);
                    student.Std_Image = reader["Std_Image"].ToString()!;
                    student.Std_Phone = reader["Std_Phone"].ToString()!;
                    stdList.Add(student);
                }
            }
            return stdList;
        }

        public Student_Info GetDataById(int id)
        {
            Student_Info student = new Student_Info();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from Student_Info where Std_Id = @id", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    student.Std_Id = Convert.ToInt32(reader["Std_Id"]);
                    student.Std_Name = reader["Std_Name"].ToString()!;
                    student.Std_Email = reader["Std_Email"].ToString()!;
                    student.age = Convert.ToInt32(reader["age"]);
                    student.Std_Image = reader["Std_Image"].ToString()!;
                    student.Std_Phone = reader["Std_Phone"].ToString()!;
                }
            }
            return student;
        }

        public void UpdateData(Student_Info student, IFormFile Std_Image)
        {
            try
            {
                var path = Path.Combine(_web.WebRootPath, "StudentImages");
                string fileName = Path.GetFileName(Std_Image.FileName);
                var fileExtension = Path.GetExtension(fileName);
                var ds = DateTime.Now.Millisecond;
                string imageName = "Std" + ds + fileExtension;
                var filePath = Path.Combine(_web.WebRootPath, "StudentImages", imageName);
                var directory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    Std_Image.CopyTo(stream);
                }

                student.Std_Image = imageName;

                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spUpdateStudentData", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", student.Std_Id);
                    cmd.Parameters.AddWithValue("@name", student.Std_Name);
                    cmd.Parameters.AddWithValue("@email", student.Std_Email);
                    cmd.Parameters.AddWithValue("@age", student.age);
                    cmd.Parameters.AddWithValue("@image", student.Std_Image);
                    cmd.Parameters.AddWithValue("@phone", student.Std_Phone);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the student record.", ex);
            }
        }

        public void DeleteData(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteStudentData", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
