using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tayana.backend.Utils;

namespace Tayana.home
{
    public partial class Contact : Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            var cmdText = "INSERT INTO contact (contactName, contactEmail, contactPhone, contactCountry, contactYachts, contactComments) VALUES (@contactName, @contactEmail, @contactPhone, @contactCountry, @contactYachts, @contactComments)";
            var command = new SqlCommand(cmdText, _sql);
            command.Parameters.AddWithValue("@contactName", Name.Text);
            command.Parameters.AddWithValue("@contactEmail", Email.Text);
            command.Parameters.AddWithValue("@contactPhone", Phone.Text);
            command.Parameters.AddWithValue("@contactCountry", Country.SelectedValue);
            command.Parameters.AddWithValue("@contactYachts", Yachts.SelectedValue);
            command.Parameters.AddWithValue("@contactComments", Comments.Text);
            _sql.Open();
            command.ExecuteNonQuery();
            _sql.Close();
            Tools.ClearControls(Name, Email, Phone, Comments);
            Country.ClearSelection();
            Yachts.ClearSelection();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Show();
        }

        private void Show()
        {
            var command = new SqlCommand("SELECT 國名 FROM 國家 Where 刪除 = 0", _sql);
            _sql.Open();
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Country.Items.Add(dataReader["國名"].ToString());
            }
            _sql.Close();

            command = new SqlCommand("SELECT 船名, 船號, 新船 FROM 船 Where 刪除 = 0", _sql);
            _sql.Open();
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                var yachtsText = dataReader["船名"] + " " + dataReader["船號"];
                var yachtsValue = dataReader["船名"].ToString();
                if ((bool)dataReader["新船"])
                {
                    yachtsText += " (New Building) ";
                }
                Yachts.Items.Add(new ListItem(yachtsText, yachtsValue));
            }
            _sql.Close();
        }
    }
}