using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Crud_Practice1;
using System.Web.UI.WebControls;
using System.Web;
using System.Xml.Linq;
using Antlr.Runtime.Tree;
using Microsoft.Ajax.Utilities;
using System.Web.UI;
using System.IO;
using System.Net.Mail;
using System.ComponentModel;
using OfficeOpenXml;

namespace Crud_Practice1
{
    public partial class Students : System.Web.UI.Page
    {
        private int StudentID;
        private string Name;
        private string Email;
        private string Gender;
        private DateTime Birthdate;

        string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        void clear()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtGender.Text = "";
            txtBirthdate.Text = "";
        }

        //Bind GridView
        private void BindGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        con.Open();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        GridView1.DataSource = dt;
                        GridView1.DataBind();

                        con.Close();
                    }
                }
            }
        }

        //Create Data   
        protected void btnSubmit_Click1(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Email, Gender, Birthdate) VALUES ('" + txtName.Text + "', '" + txtEmail.Text + "','" + txtGender.Text + "', '" + txtBirthdate.Text + "')", con);
                int j = cmd.ExecuteNonQuery();
                if (j > 0)
                {
                    BindGrid();
                }
            }
            clear();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the GridView to edit mode for the row being edited
            GridView1.EditIndex = e.NewEditIndex;

            // Retrieve the student data from the GridView and store it in private fields
            GridViewRow row = GridView1.Rows[e.NewEditIndex];

            if (row != null)
            {
                if (GridView1.DataKeys[e.NewEditIndex] != null)
                {
                    StudentID = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
                }

                TextBox txtName = row.FindControl("txtName") as TextBox;
                TextBox txtEmail = row.FindControl("txtEmail") as TextBox;
                TextBox txtGender = row.FindControl("txtGender") as TextBox;
                TextBox txtBirthdate = row.FindControl("txtBirthdate") as TextBox;

                if (txtName != null) Name = txtName.Text;
                if (txtEmail != null) Email = txtEmail.Text;
                if (txtGender != null) Gender = txtGender.Text;
                if (txtBirthdate != null) Birthdate = Convert.ToDateTime(txtBirthdate.Text);

                // Rebind the GridView to display the edit controls
                BindGrid();
            }
            else
            {
                // Handle the case where row is null
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }


        private void UpdateStudent(int studentID, string name, string email, string gender, DateTime birthdate)
        {
            string query = "UPDATE Students SET Name = @Name, Email = @Email, Gender = @Gender, Birthdate = @Birthdate WHERE StudentID = @StudentID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Define parameters and their values
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Birthdate", birthdate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void DeleteStudent(int studentID)
        {
            string query = "DELETE FROM Students WHERE StudentID = @StudentID";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Define parameter and its value
                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    // Open the connection and execute the command
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void alert(string Msg, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "alert", "alert('" + Msg + "'); ", true);
        }

        public static void toast(string Msg, string MsgType, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "sweetAlert", "toastMsg('" + Msg + "','" + MsgType + "'); ", true);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int studentID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtName")).Text;
            string email = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEmail")).Text;
            string gender = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtGender")).Text;
            string birthdateText = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtBirthdate")).Text;

            // Validate the inputs
            if (string.IsNullOrEmpty(name))
            {
                toast("Name cannot be empty!", "warning", this);
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                toast("Email cannot be empty!", "warning", this);
                return;
            }

            if (string.IsNullOrEmpty(gender))
            {
                toast("Gender cannot be empty!", "warning", this);
                return;
            }
            if (!DateTime.TryParse(birthdateText, out DateTime birthdate))
            {
                // If the parsing fails, show an alert
                toast("Invalid birthdate format. Please enter a valid date.", "warning", this);
                return;
            }

            // Update the database with the new values
            UpdateStudent(studentID, name, email, gender, birthdate);

            // Exit edit mode and rebind the GridView
            GridView1.EditIndex = -1;
            BindGrid();
            alert("Student record updated successfully!", this.Page);

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int studentID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            // Delete the record from the database
            DeleteStudent(studentID);

            // Rebind the GridView
            BindGrid();
        }


        private DataTable GetStudentByID(int studentID)
        {
            DataTable dt = new DataTable();

            string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE StudentID =@StudentID", con))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
        }

        protected void btnStudentID_Command(object sender, CommandEventArgs e)
        {
            int studentID = Convert.ToInt32(e.CommandArgument);

            DataTable dt = GetStudentByID(studentID);

            if (dt.Rows.Count > 0)
            {
                // Assuming you have text fields in the modal to show the student information
                txtModalStudentID.Text = dt.Rows[0]["StudentID"].ToString();
                txtModalName.Text = dt.Rows[0]["Name"].ToString();
                txtModalEmail.Text = dt.Rows[0]["Email"].ToString();
                txtModalGender.Text = dt.Rows[0]["Gender"].ToString();
                txtModalBirthdate.Text = Convert.ToDateTime(dt.Rows[0]["Birthdate"]).ToString("yyyy-MM-dd");

                // Fetch and bind attachments
                BindAttachments(studentID);

                // Now, trigger the modal to show
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", "$('#student_modal').modal({ backdrop: 'static' });", true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    LinkButton btnStudentId = e.Row.FindControl("btnStudentID") as LinkButton;
            //    ScriptManager.GetCurrent(this).RegisterPostBackControl(btnStudentId);
            //}
        }

        protected void btnupload_Click1(object sender, EventArgs e)
        {
            int studentID;
            if (!int.TryParse(txtModalStudentID.Text, out studentID))
            {
                toast("Invalid Student ID.", "warning", this);
                return;
            }

            DataTable dt = GetStudentByID(studentID);  // Assuming you already have this method for student data

            if (fileUpload.HasFile)
            {

                for (int i = 0; i < fileUpload.PostedFiles.Count; i++)
                {
                    HttpPostedFile pFile = fileUpload.PostedFiles[i];
                    string fileName = Path.GetFileName(pFile.FileName);
                    string contentType = pFile.ContentType;

                    // Convert file to byte array
                    byte[] fileData;
                    using (Stream fs = pFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            fileData = br.ReadBytes((int)fs.Length);

                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
                            {
                                string query = "EXEC sp_t_update_attachment @FileName, @ContentType, @FileData, @StudentID";

                                using (SqlCommand cmd = new SqlCommand(query, conn))
                                {
                                    cmd.Parameters.AddWithValue("@FileName", fileName);
                                    cmd.Parameters.AddWithValue("@ContentType", contentType);
                                    cmd.Parameters.AddWithValue("@FileData", fileData);
                                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                                    conn.Open();
                                    cmd.ExecuteNonQuery();
                                }
                                toast("Successfully Uploaded File!", "success", this);
                            }
                        }
                    }
                }
            }
        }
        protected void btnAttachment_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string studentID = btn.CommandArgument;

            Response.Redirect($"Attachments.aspx?StudentID={studentID}");
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkDownload = (LinkButton)sender;
            string attachmentID = lnkDownload.CommandArgument;

            // Your logic to handle the file download
            DownloadAttachment(attachmentID);
        }

        private void DownloadAttachment(string attachmentID)
        {
            // Your logic to retrieve the file content from the database and send it to the user
            string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FileName, FileData FROM Attachments WHERE AttachmentID = @AttachmentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AttachmentID", attachmentID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string fileName = reader["FileName"].ToString();
                    byte[] fileData = (byte[])reader["FileData"];

                    Response.Clear();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.BinaryWrite(fileData);
                    Response.End();
                }
            }
        }


        private void BindAttachments(int studentID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT AttachmentID, FileName FROM Attachments WHERE StudentID = @StudentID", conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Repeater1.DataSource = reader;
                    Repeater1.DataBind();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            LinkButton btnDownloadStudent = e.Item.FindControl("lnkDownload") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDownloadStudent);

        }

        private void fx_uploadData()
        {

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            
        }
    }
    public static class ToastType
    {
        public const string success = "success";
        public const string error = "error";
        public const string info = "info";
        public const string warning = "warning";
    }
}
