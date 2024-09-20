using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

namespace Crud_Practice1
{
    public partial class ExcelUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fuUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fuUpload.FileName).ToLower();
                if (fileExtension == ".xlsx" || fileExtension == ".xls")
                {
                    try
                    {
                        using (ExcelPackage package = new ExcelPackage(fuUpload.FileContent))
                        {
                            // Check if there are any worksheets
                            if (package.Workbook.Worksheets.Count == 0)
                            {
                                throw new Exception("The uploaded file does not contain any worksheets.");
                            }

                            // Get the first worksheet in the Excel file
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            // Check if the worksheet has any data
                            if (worksheet.Dimension == null)
                            {
                                throw new Exception("The worksheet is empty.");
                            }

                            // List to hold students data
                            List<Student> students = new List<Student>();

                            // Read data starting from the second row (assuming the first row is the header)
                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                if (worksheet.Dimension.End.Column < 4)
                                {
                                    throw new Exception("The worksheet does not have enough columns.");
                                }

                                Student student = new Student
                                {
                                    Name = worksheet.Cells[row, 1].Text,
                                    Gender = worksheet.Cells[row, 2].Text,
                                    Email = worksheet.Cells[row, 3].Text,
                                    Birthdate = DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime birthdate) ? birthdate : DateTime.MinValue
                                };

                                // Validate the student object if necessary
                                if (string.IsNullOrEmpty(student.Name) || student.Birthdate == DateTime.MinValue)
                                {
                                    throw new Exception($"Invalid data at row {row}: Name or Birthdate is missing.");
                                }

                                students.Add(student);
                            }

                            // Insert students into the database
                            InsertStudentsIntoDatabase(students);

                            // Display success message
                            lblMessage.Text = $"{students.Count} students uploaded successfully.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display error message
                        lblMessage.Text = $"Error: {ex.Message}";
                    }
                }
                else
                {
                    lblMessage.Text = "Please upload a valid Excel file (.xlsx or .xls).";
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload.";
            }
        }

        private void InsertStudentsIntoDatabase(List<Student> students)
        {
            string connectionString = "your_connection_string_here"; // Update with your actual connection string
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (Student student in students)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Gender, Email, Birthdate) VALUES (@Name, @Gender, @Email, @Birthdate)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Birthdate", student.Birthdate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public class Student
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public string Email { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
}