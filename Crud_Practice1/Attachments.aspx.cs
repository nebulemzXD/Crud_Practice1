using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Crud_Practice1
{
    public partial class Attachments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Get the StudentID from the query string
                string studentID = Request.QueryString["StudentID"];

                if (!string.IsNullOrEmpty(studentID))
                {
                    // Fetch and display attachments for the given StudentID
                    DisplayAttachments(studentID);
                }
            }
        }

        private void DisplayAttachments(string studentID)
        {
            // Your logic to retrieve and display attachments based on studentID
            // Sample code:
            DataTable dtAttachments = GetAttachmentsByStudentID(studentID);
            AttachmentsGridView.DataSource = dtAttachments;
            AttachmentsGridView.DataBind();
        }

        private DataTable GetAttachmentsByStudentID(string studentID)
        {
            // Fetch the connection string from the web.config
            string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Attachments WHERE StudentID = @StudentID", con))
                {

                    cmd.Parameters.AddWithValue("@StudentID", studentID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    return dt;
                }
            }
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

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            LinkButton lnkDownload = (LinkButton)sender;
            string attachmentID = lnkDownload.CommandArgument;

            // Your logic to handle the file download
            DownloadAttachment(attachmentID);
        }
    }
}
