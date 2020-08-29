using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class _Default : System.Web.UI.Page
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
           
            hyperlinkRegistration.Text = username;
            hyperlinkRegistration.Enabled = false;
            hyperlinkRegistration.NavigateUrl = "~/Profile.aspx";
            hyperlinkLogin.Text = "Log out";
            hyperlinkLogin.NavigateUrl = "~/SingOut.aspx";
        }
        else
        {
            Response.Redirect("MainPage.aspx", true);
        }
    }
}