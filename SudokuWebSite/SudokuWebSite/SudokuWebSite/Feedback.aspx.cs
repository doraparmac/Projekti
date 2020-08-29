using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;

public partial class Feedback : System.Web.UI.Page
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        MailMessage feedBack = new MailMessage();
        feedBack.To.Add("dora.parmac@gmail.com");
        feedBack.From = new MailAddress("dora.parmac@gmail.com");
        feedBack.Subject = txtSubject.Text;
        feedBack.Body = "Sender Name: " + txtName.Text + "<br><br>Sender Email: " + txtMail.Text + "<br><br>" + txtMessage.Text;
        feedBack.IsBodyHtml = true;
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.mailtrap.io"; //Or Your SMTP Server Address 
        //OVA ADRESA NE RADI; KRIVI PORT?
        smtp.Port = 25;
        smtp.EnableSsl = false;
        smtp.Credentials = new System.Net.NetworkCredential("02897816bd1d9d", "916efa96f57424");
        //Or your Smtp Email ID and Password
        smtp.Send(feedBack);
        Label1.Text = "Thank you for your time!";
        txtName.Text = String.Empty;
        txtMail.Text = String.Empty;
        txtMessage.Text = String.Empty;
        txtSubject.Text = String.Empty;
    }
}