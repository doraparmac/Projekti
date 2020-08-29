using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class LetsPlay :  System.Web.UI.Page
{
    string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
    string playerID = "NULL";
    string isLogged = "NULL";
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = "Register";
        if (Request.Cookies["logged"] != null)
        {
            isLogged = Request.Cookies["logged"].Value.ToString();
        }
        
        if (Request.Cookies["logged_id"] != null)
        {
            playerID = Request.Cookies["logged_id"].Value.ToString();
        }
        if (isLogged != "NULL")
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Players where player_id=" + playerID;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            username = reader[1].ToString();
                        }
                    }
                }
            }
            hyperlinkRegistration.Text = username;
            if (hyperlinkRegistration.Text == "admin")
            {
                btnDodaj.Visible = true;
            }
            else
            {
                btnDodaj.Visible = false;
            }
            hyperlinkRegistration.Text = username;
            hyperlinkRegistration.Enabled = false;
            hyperlinkRegistration.NavigateUrl = "~/Profile.aspx";
            hyperlinkLogin.Text = "Log out";
            hyperlinkLogin.NavigateUrl = "~/SingOut.aspx";
        }
        else
        {
            Response.Redirect("MainPage.aspx",true);
        }


        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string query = "SELECT tournament_id,title,end_date FROM Tournaments";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader.GetInt64(0).ToString();
                        string title = reader.GetString(1).ToString();
                        string date = reader.GetDateTime(2).ToString();
                        ddlTournaments.Items.Add(id+": "+title + " ["+ date+"]");
                    }
                }
            }
        }
    }

    protected void btnPlay_Click(object sender, EventArgs e)
    {
        if (isLogged != "NULL")
        {
            string cookieValue2 = ddlTournaments.SelectedItem.Text;
            int index = cookieValue2.IndexOf(":");
            if (index > 0)
                cookieValue2 = cookieValue2.Substring(0, index);

            HttpCookie cookie2 = new HttpCookie("tournament_id", cookieValue2);

            Response.Cookies.Add(cookie2);
            Response.Redirect("Match.aspx");
        }
        else
        {
            Response.Redirect("SingIn.aspx");
        }

    }



    protected void btnDodaj_Click(object sender, EventArgs e)
    {
        Response.Redirect("Admin.aspx", true);
    }

    protected void ddlTournaments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}