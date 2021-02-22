using System;
using System.Data;
using System.Data.SqlClient;

namespace Tayana.home
{
    public partial class Index : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            ShowYachts();
            ShowNews();
        }

        private void ShowNews()
        {
            var cmdText = "SELECT TOP 3 Id, 標題, 置頂, 圖片, 日期 FROM 新聞 WHERE (刪除 = 0) ORDER BY 置頂 DESC, Id DESC";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(sqlCommand);
            sqlData.Fill(table);
            RepeaterNews.DataSource = table;
            RepeaterNews.DataBind();
        }

        private void ShowYachts()
        {
            var cmdText = "SELECT Id, 船名, 船號, 圖片, 新船 FROM 船 WHERE (刪除 = 0) ORDER BY Id DESC";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(sqlCommand);
            sqlData.Fill(table);
            RepeaterBanner.DataSource = table;
            RepeaterBanner.DataBind();
            RepeaterBannerimg.DataSource = table;
            RepeaterBannerimg.DataBind();
        }
    }
}