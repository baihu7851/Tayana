using System;
using System.Data.SqlClient;

namespace Tayana.home
{
    public partial class Dealers : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Show();
        }

        private void Show()
        {
            var id = Request.QueryString["id"] ?? "1";
            var name = "";
            var cmdText = "SELECT * FROM 國家 Where (刪除 = 0)";
            var command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var leftReader = command.ExecuteReader();
            while (leftReader.Read())
            {
                name = leftReader["國名"].ToString();
                var listId = leftReader["Id"].ToString();
                ulDealers.InnerHtml += $"<li><a href='dealers.aspx?id={listId}'>{name}</a></li>";
            }
            _sql.Close();

            cmdText = $"SELECT * FROM 國家 WHERE (刪除 = 0) AND (id = {id})";
            command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var dataCountry = command.ExecuteReader();
            if (dataCountry.Read())
            {
                dealersCrumb.InnerText = dataCountry["國名"].ToString();
                dealersRigh1.InnerText = dataCountry["國名"].ToString();
            }
            _sql.Close();

            cmdText = $"SELECT * FROM 國家 INNER JOIN 地區 ON 國家.Id = 地區.Pid INNER JOIN 代理商 ON 代理商.Pid = 地區.Id WHERE (國家.Id = {id})";
            command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var dataReader = command.ExecuteReader();
            dealersRepeater.DataSource = dataReader;
            dealersRepeater.DataBind();
            _sql.Close();
        }
    }
}