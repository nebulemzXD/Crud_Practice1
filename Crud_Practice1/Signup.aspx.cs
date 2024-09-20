using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud_Practice1
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegions();

            }
        }

        private void BindRegions()
        {
            // Fetch regions from database
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Region_Code, Region_Name FROM Region", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlRegions.DataSource = reader;
                ddlRegions.DataTextField = "Region_Name";
                ddlRegions.DataValueField = "Region_Code";
                ddlRegions.DataBind();
                ddlRegions.Items.Insert(0, new ListItem("Select Region", ""));
            }
        }

        protected void ddlRegions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedRegion_Code;
            selectedRegion_Code = ddlRegions.SelectedValue;

                BindProvinces(selectedRegion_Code);
        }
        private void BindProvinces(string regionCode)
        {
            // Fetch provinces based on selected region from database
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Province_Code, Province_Name FROM Province WHERE Region_Code = @Region_Code", conn);
                cmd.Parameters.AddWithValue("@Region_Code", regionCode);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlProvinces.DataSource = reader;
                ddlProvinces.DataTextField = "Province_Name";
                ddlProvinces.DataValueField = "Province_Code";
                ddlProvinces.DataBind();
                ddlProvinces.Items.Insert(0, new ListItem("Select Province", ""));
            }
        }

        protected void ddlProvinces_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProvince_Code;
            selectedProvince_Code = ddlProvinces.SelectedValue;
            BindCities(selectedProvince_Code);
        }

        private void BindCities(string provinceCode)
        {
            using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSTUDENTINFO"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT City_Code, City_Name FROM City WHERE County = @County", conn);
                cmd.Parameters.AddWithValue("@County", provinceCode);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCities.DataSource = reader;
                ddlCities.DataTextField = "City_Name";
                ddlCities.DataValueField = "City_Code";
                ddlCities.DataBind();
                ddlCities.Items.Insert(0, new ListItem("Select City", ""));
            }
        }

        protected void ddlCities_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCounty;
            selectedCounty = ddlCities.SelectedValue;
        }
    }
}