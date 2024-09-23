using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        private bool fx_checkFileForErrors()
        {
            bool hasError = false;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            List<string> lstStudentID = new List<string>();
            List<string> lstName = new List<string>();
            List<string> lstEmail = new List<string>();
            List<string> lstGender = new List<string>();
            List<string> lstBirthdate = new List<string>();

            DataTable dt = new DataTable();
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("StudentID");
            dtResult.Columns.Add("Name");
            dtResult.Columns.Add("Email");
            dtResult.Columns.Add("Gender");

            dtResult.Columns.Add("Birthdate");
            dtResult.Columns.Add("type");
            dtResult.Columns.Add("description");
            dtResult.Columns.Add("resolution");

            DataRow drResult = dtResult.NewRow();

            string sql = "";

            #region fetchData from database for faster search

            sql = @"SELECT StudentID FROM Students;";
            dt = clsCRUD.fetchData(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lstStudentID.Add(dt.Rows[i]["StudentID"].ToString());
            }
            dt.Dispose();
            #endregion
            #region open excel file and check for errors

            ExcelPackage pck = new ExcelPackage(fuUpload.PostedFile.InputStream);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (pck)
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets["MASS CRUD"];

                if (ws != null)
                {
                    bool hasRows = false;

                    //loop through the rows until blank is met.
                    int row = 7;
                    while (ws.Cells[row, 2].Text.ToString() != "")
                    {
                        hasRows = true;

                        string Name, Email, Gender, Birthdate;

                        Name = ws.Cells[row, 2].Text.ToString();
                        Email = ws.Cells[row, 3].Text.ToString();
                        Gender = ws.Cells[row, 4].Text.ToString();
                        Birthdate = ws.Cells[row, 5].Text.ToString();

                        if (!lstName.Contains(Name))
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = row.ToString();
                            drResult["type"] = "ERROR";
                            drResult["Name"] = Name;
                            drResult["description"] = "Invalid Name";
                            drResult["resolution"] = "CHECK THE NAME IF VALID";
                            dtResult.Rows.Add(drResult);
                            hasError = true;
                        }

                        if (!lstEmail.Contains(Email))
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = row.ToString();
                            drResult["type"] = "ERROR";
                            drResult["Email"] = Email;
                            drResult["description"] = "Invalid Email Format";
                            drResult["resolution"] = "CHECK THE EMAIL VALUE IF EXISTING/VALID";
                            dtResult.Rows.Add(drResult);
                            hasError = true;
                        }
                        if (!lstGender.Contains(Gender))
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = row.ToString();
                            drResult["type"] = "ERROR";
                            drResult["Gender"] = Gender;
                            drResult["description"] = "INVALID GENDER";
                            drResult["resolution"] = "PLEASE INPUT THE RIGHT FORMAT";
                            dtResult.Rows.Add(drResult);
                            hasError = true;
                        }
                        if (!lstBirthdate.Contains(Birthdate))
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = row.ToString();
                            drResult["type"] = "ERROR";
                            drResult["row_value"] = Birthdate;
                            drResult["description"] = "INVALID BIRTHDATE FORMAT";
                            drResult["resolution"] = "CHECK THE BIRTHDATE IF EXISTING/VALID";
                            dtResult.Rows.Add(drResult);
                            hasError = true;
                        }
                        if (hasRows == false)
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = "FILE";
                            drResult["type"] = "ERROR";
                            drResult["row_value"] = "";
                            drResult["description"] = "NO ROWS TO PROCESS";
                            drResult["resolution"] = "MAKE SURE THAT THE FILE HAS A DATA";
                            dtResult.Rows.Add(drResult);
                            hasError = true;
                        }
                        else
                        {
                            drResult = dtResult.NewRow();
                            drResult["StudentID"] = "FILE";
                            drResult["type"] = "ERROR";
                            drResult["row_value"] = "";
                            drResult["description"] = "CANNOT FIND THE EXCEL SHEET: MASS CRUD";
                            drResult["resolution"] = "MAKE SURE THAT THE FILE HAS A SHEET NAMED: MASS CRUD";
                            dtResult.Rows.Add(drResult);
                            hasError = true;

                        }
                    }
                    #endregion

                    gvUploadResult.DataSource = dtResult;
                    gvUploadResult.DataBind();


                    return hasError;
                }
            }
            return false;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            bool hasError = fx_checkFileForErrors();
            if ((hasError))
            {
                lblErrorFound.Visible = true;
                lblSuccess.Visible = false;
                //lnkDownloadValidateFile.Visible = false;

                gvUploadResult.Visible = true;
                gvUploadResultSuccess.Visible = false;
            }
            else
            {
                fx_uploadData();
                lblErrorFound.Visible = false;
                lblSuccess.Visible = true;
                clsUtil.toast("Successfully Uploaded", "success", this);
            }

        }
        //Get session StudentID
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

        private void fx_uploadData()
        {
            int studentID;

            // Check if the StudentID exists in the session and can be parsed
            if (Session["StudentID"] == null || !int.TryParse(Session["StudentID"].ToString(), out studentID))
            {
                clsUtil.toast("Invalid Student ID.", "warning", this);
                return;
            }

            if (fuUpload.HasFile)
            {
                for (int i = 0; i < fuUpload.PostedFiles.Count; i++)
                {
                    HttpPostedFile pFile = fuUpload.PostedFiles[i];
                    string fileName = Path.GetFileName(pFile.FileName);
                    string contentType = pFile.ContentType;

                    if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")  // Check if it's an Excel file
                    {
                        using (Stream stream = pFile.InputStream)
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                            using (ExcelPackage package = new ExcelPackage(stream))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];  // Get the first worksheet
                                int rowCount = worksheet.Dimension.Rows;
                                int colCount = worksheet.Dimension.Columns;

                                for (int row = 2; row <= rowCount; row++)  // Start from row 2, assuming row 1 is header
                                {
                                    string excelData = worksheet.Cells[row, 2].Text.ToString(); // Example of reading the first column
                                                                                                 // Process the data here or store it in the database
                                }

                                // Save the file content in the database if needed
                                byte[] fileData;
                                using (BinaryReader br = new BinaryReader(pFile.InputStream))
                                {
                                    fileData = br.ReadBytes((int)pFile.InputStream.Length);
                                }

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
                                    clsUtil.toast("Successfully Uploaded and Processed Excel File!", "success", this);
                                }
                            }
                        }
                    }
                    else
                    {
                        clsUtil.toast("Please upload an Excel file.", "warning", this);
                    }
                }
            }
        }
    }
}