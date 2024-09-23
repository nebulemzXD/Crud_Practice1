﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Crud_Practice1
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear all sessions
            Session.Clear();

            // Optionally abandon the session
            Session.Abandon();

            // Redirect to the login page
            Response.Redirect("~/Login.aspx");
        }
    }
}