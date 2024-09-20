using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud_Practice1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static DataTable fetchData(string query)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        public static void toast(string Msg, string MsgType, Page Child)
        {
            ScriptManager.RegisterClientScriptBlock(Child, typeof(Page), "sweetAlert", "toastMsg('" + Msg + "','" + MsgType + "'); ", true);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string sql = "";
            sql = "EXEC sp_AuthenticateUser @Name='" +txtName.Text+ "',@Password='" +txtPassword.Text+ "'";

            DataTable dt = new DataTable();
            //dt = clsDB.ts_getdata(sql);
            dt = fetchData(sql);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsActive"].ToString().Trim() == "X")
                {
                    Session["Name"] = dt.Rows[0]["Name"].ToString();
                    Session["Email"] = dt.Rows[0]["Email"].ToString();

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    toast("Employee Not Active!", "error", this);
                }
            }
            else
            {
               toast("Invalid Credentials!", "error", this);
            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }
    }
}