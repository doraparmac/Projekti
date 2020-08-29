using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["logged"] != null)
        {
            Response.Cookies["logged"].Expires = DateTime.Now.AddDays(-1);
        }
        if (Request.Cookies["logged_id"] != null)
        {
            Response.Cookies["logged_id"].Expires = DateTime.Now.AddDays(-1);
        }
        Response.Redirect("MainPage.aspx");
    }
}