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

public partial class Register :  System.Web.UI.Page
{
    string DatabaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string query = "SELECT title FROM Countries";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ddlCountry.Items.Add(reader.GetString(0));
                    }
                }
            }
        }
       
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        bool error = false;
        if (txtUsername.Text.Length < 1 || txtUsername.Text.Length > 25)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong username');", true);
        }
        else if (txtFirstName.Text.Length < 1 || txtFirstName.Text.Length > 25)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Enter First Name');", true);
        }
        else if (txtLastName.Text.Length < 1 || txtLastName.Text.Length > 25)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Enter Last Name');", true);
        }
        else if (txtPassword.Text.Length < 1 || txtPassword.Text.Length > 25)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Enter Password');", true);
        }
        else if (txtPassword.Text != txtConfirmPassword.Text)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong Password');", true);
        }
        else if (IsValidEmail(txtEmail.Text) is false)
        {
            error = true;
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong E-mail');", true);
        }
        if(!error)
        {
            InsertUser();
        }

    }
    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    void InsertUser()
    {
        int userCount = 0;
        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) from Players where username='"+txtUsername.Text+"'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                userCount += (int)command.ExecuteScalar();
            }
        }
        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string query = "SELECT COUNT(*) from Players where email='" + txtEmail.Text + "'";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                 userCount += (int)command.ExecuteScalar();
            }
        }
        if(userCount < 1)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
            {
                connection.Open();
                string username = txtUsername.Text;
                string email = txtEmail.Text;
                string firstname = txtFirstName.Text;
                string lastname = txtLastName.Text;
                string password = sha256(txtPassword.Text);
                int countrySelect = ddlCountry.SelectedIndex+1;
                string country = countrySelect.ToString();
                DateTime myDateTime = DateTime.Now;
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
                string query = string.Format("INSERT INTO Players(username,email,admin,first_name,last_name,country_id,password,created_at) VALUES ('{0}','{1}','0','{2}','{3}','{4}','{5}','{6}')",
                    username,email,firstname,lastname,country,password, sqlFormattedDate);
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Username or email already exists!');", true);
        }
        txtFirstName.Text = String.Empty;
        txtLastName.Text = String.Empty;
        txtEmail.Text = String.Empty;
        txtUsername.Text = String.Empty;
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

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}