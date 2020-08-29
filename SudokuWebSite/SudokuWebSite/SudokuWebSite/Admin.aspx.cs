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

public partial class Admin : System.Web.UI.Page
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

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(DatabaseConnectionString))
        {
            connection.Open();
            string title = txtTitle.Text;
            string enddate = txtEndDate.Text +":00.000";
            int countrySelect = ddlCountry.SelectedIndex+1;
            string country = countrySelect.ToString();
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = string.Format("INSERT INTO Tournaments(title,start_date,end_date,country_id) VALUES ('{0}','{1}','{2}','{3}')",
                title, sqlFormattedDate, enddate, country);
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
        txtEndDate.Text = String.Empty;
        txtTitle.Text = String.Empty;
    }
}