using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class MainPage : System.Web.UI.Page
{
    string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = "Register";
        string isLogged = "NULL";
        if (Request.Cookies["logged"] != null)
        {
            isLogged = Request.Cookies["logged"].Value.ToString();
        }
        string playerID = "NULL";
        if (Request.Cookies["logged_id"] != null)
        {
            playerID = Request.Cookies["logged_id"].Value.ToString();
        }
        if (isLogged!="NULL")
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
            hyperlinkRegistration.Enabled = false;
            hyperlinkRegistration.NavigateUrl = "~/Profile.aspx";
            hyperlinkLogin.Text = "Log out";
            hyperlinkLogin.NavigateUrl = "~/SingOut.aspx";
        }
    }

    protected void btnHrv_Click(object sender, EventArgs e)
    {
        
    }
}