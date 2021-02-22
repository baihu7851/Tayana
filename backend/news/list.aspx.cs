using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Tayana.backend.news
{
    public partial class List : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = ((FormsIdentity)Page.User.Identity).Ticket.UserData.Split(',');
            if ((Convert.ToInt32(userData[1]) & 4) == 0)
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
            if (e.CommandName != "Del") return;
            var id = Convert.ToInt32(e.CommandArgument);
            var cmdText = $"UPDATE 新聞 SET 刪除 = 1 WHERE (Id = {id})";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            Show();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater.Items)
            {
                var id = ((Literal)item.FindControl("Id")).Text;
                var isNew = (CheckBox)item.FindControl("isTop");
                var cmdText = $"UPDATE 新聞 SET 置頂 = @置頂 WHERE (Id = {id})";
                var sqlCommand = new SqlCommand(cmdText, _sql);
                sqlCommand.Parameters.AddWithValue("@置頂", isNew.Checked ? 1 : 0);
                _sql.Open();
                sqlCommand.ExecuteNonQuery();
                _sql.Close();
            }
            Show();
        }

        private void Show()
        {
            var page = 0;
            const int onePage = 10;
            var pageNumber = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]);
            var cmdText = $@"
                WITH Page AS
                (
                    select ROW_NUMBER() over(order by Id DESC) as 編號, Id, 標題, 置頂, 圖片 from 新聞 WHERE (刪除 = 0)
                )
                SELECT * FROM Page WHERE 編號 >={ (pageNumber - 1) * onePage + 1 }AND 編號<={ pageNumber * onePage}";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(sqlCommand);
            sqlData.Fill(table);
            Repeater.DataSource = table;
            Repeater.DataBind();
            _sql.Open();
            var count = new SqlCommand("SELECT count(*) FROM 新聞 WHERE (刪除 = 0)", _sql);
            var countData = count.ExecuteReader();
            if (countData.Read())
            {
                page = Convert.ToInt32(countData[0]);
            }
            WebUserControl.TotalItems = page;
            WebUserControl.limit = onePage;
            WebUserControl.Targetpage = "list.aspx";
            WebUserControl.ShowPageControls();
            _sql.Close();
        }
    }
}