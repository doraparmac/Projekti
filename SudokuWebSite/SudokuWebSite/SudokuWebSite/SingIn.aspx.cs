using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;

public partial class SingIn :  System.Web.UI.Page
{
    string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSingIn_Click(object sender, EventArgs e)
    {
        int userCount = 0;
        string password = "NULL";
        bool doLogin = false;
        if(txtPassword.Text.Length>0)
        {
            password = sha256(txtPassword.Text);
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong password!');", true);
        }
        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) from Players where username='" + txtUsername.Text + "'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                userCount += (int)command.ExecuteScalar();
            }
        }
        if(userCount>0 && txtPassword.Text.Length>0)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
            {
                connection.Open();
                string query = "SELECT password FROM Players where username='" + txtUsername.Text + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (password == reader.GetString(0))
                            {
                                doLogin = true;
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong Password!');", true);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Username does not exist!');", true);
        }
        if(doLogin)
        {
            string cookieValue = "1";

            HttpCookie cookie = new HttpCookie("logged", cookieValue);

            Response.Cookies.Add(cookie);
            string userID="NULL";
            using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
            {
                connection.Open();
                string query = "SELECT player_id FROM Players where username='" + txtUsername.Text + "'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userID = reader[0].ToString();
                        }
                    }
                }
            }
            string cookieValue2 = userID;

            HttpCookie cookie2 = new HttpCookie("logged_id", cookieValue2);

            Response.Cookies.Add(cookie2);
            Response.Redirect("MainPage.aspx");
        }
    }
    string sha256(string randomString)
    {
        var crypt = new SHA256Managed();
        string hash = String.Empty;
        byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(randomString));
        foreach (byte theByte in crypto)
        {
            hash += theByte.ToString("x2");
        }
        return hash;
    }
}