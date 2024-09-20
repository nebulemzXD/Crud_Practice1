using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Crud_Practice1
{
    public partial class _Default : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("INSERT INTO Students (VALUES '" + txtName.Text + "', '" + txtEmail.Text + "','" + txtGender.Text + "', '" + txtBirthdate.Text + "')", con);
        //    }

        //}
    }
}