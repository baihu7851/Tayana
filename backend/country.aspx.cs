using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Tayana.backend
{
    public partial class Country : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = (((FormsIdentity)Page.User.Identity).Ticket.UserData).Split(',');
            if ((Convert.ToInt32(userData[1]) & 16) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Show();
        }

        protected void Repeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            var id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName != "Del") return;
            var cmdText = $"UPDATE 新聞 SET 刪除 = 1 WHERE (Id = {id})";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            Show();
        }

        protected void NewCountry_Click(object sender, EventArgs e)
        {
            const string cmdText = "INSERT INTO 國家 (國名) VALUES (@國名)";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            sqlCommand.Parameters.AddWithValue("@國名", countryName.Text);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            countryName.Text = "";
            Show();
        }

        private void Show()
        {
            var page = 0;
            const int onePage = 10;
            var pageNumber = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]);
            var cmdText = new SqlCommand($@"
                WITH Page AS
                (
                    select ROW_NUMBER() over(order by Id DESC) as 編號, Id, 國名 from 國家 WHERE (刪除 = 0)
                )
                SELECT * FROM Page WHERE 編號 >={ (pageNumber - 1) * onePage + 1 }AND 編號<={ pageNumber * onePage}", _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(cmdText);
            sqlData.Fill(table);
            Repeater.DataSource = table;
            Repeater.DataBind();
            _sql.Open();
            var count = new SqlCommand("SELECT count(*) FROM 國家 WHERE (刪除 = 0)", _sql);
            var countData = count.ExecuteReader();
            if (countData.Read())
            {
                page = Convert.ToInt32(countData[0]);
            }
            WebUserControl.TotalItems = page;
            WebUserControl.limit = onePage;
            WebUserControl.Targetpage = "country.aspx";
            WebUserControl.ShowPageControls();
            _sql.Close();
        }
    }
}