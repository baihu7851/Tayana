using System;
using System.Data;
using System.Data.SqlClient;

namespace Tayana.home
{
    public partial class NewList : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Show();
        }

        private void Show()
        {
            var page = 0;
            const int onePage = 10;
            var pageNumber = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]);
            var cmdText = $@"WITH Page AS
                (
                select ROW_NUMBER() over(order by 置頂 DESC, Id DESC) as 編號,
                Id, 標題, 副標題, 圖片, 置頂, 日期 FROM 新聞
                )
                SELECT * FROM Page WHERE 編號 >= { (pageNumber - 1) * onePage + 1 } AND 編號 <= { pageNumber * onePage}";
            var command = new SqlCommand(cmdText, _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(command);
            sqlData.Fill(table);
            Repeater.DataSource = table;
            Repeater.DataBind();

            var con = new SqlCommand("SELECT count(*) FROM 新聞 ", _sql);
            _sql.Open();
            var sqlDataReader = con.ExecuteReader();
            if (sqlDataReader.Read())
            {
                page = Convert.ToInt32(sqlDataReader[0]);
            }
            WebUserControl.TotalItems = page;
            WebUserControl.limit = onePage;
            WebUserControl.Targetpage = "new_list.aspx";
            WebUserControl.ShowPageControls();
            sqlDataReader.Close();
            _sql.Close();
        }
    }
}