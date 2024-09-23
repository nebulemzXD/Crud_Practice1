using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace Crud_Practice1
{
    public class clsCRUD
    {
        public static void executeQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void executeQuery(string query, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public static void executeSP(string query, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
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

        public static DataTable fetchDataSP(string spname, SqlParameter[] sqlParam)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = spname;
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter param in sqlParam)
                    {
                        cmd.Parameters.Add(param);
                    }
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
    }
}