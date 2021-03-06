using System;
using System.Data.SqlClient;
using System.Text;
using System.Web.Security;

namespace Tayana.backend
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var newNavbar = new StringBuilder();
            var userData = ((FormsIdentity)Page.User.Identity).Ticket.UserData.Split(',');
            userName.InnerText = Page.User.Identity.Name;
            userImg.Src = "~/upload/images/" + userData[0];
            var sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            const string cmdText = "SELECT * FROM 權限";
            var sqlCommand = new SqlCommand(cmdText, sql);
            sql.Open();
            var sqlData = sqlCommand.ExecuteReader();
            while (sqlData.Read())
            {
                for (var i = 1; i <= Convert.ToInt32(userData[1]); i *= 2)
                {
                    if ((Convert.ToInt32(userData[1]) & i) != Convert.ToInt32(sqlData["編號"])) continue;
                    newNavbar.Append("<li class='nav-item'>");
                    newNavbar.Append($"<a href='/backend/{sqlData["頁面"]}.aspx' class='nav-link'>");
                    newNavbar.Append("<span class='pcoded-micon'>");
                    newNavbar.Append($"<i class='feather icon-{sqlData["圖示"]}'></i>");
                    newNavbar.Append("</span>");
                    newNavbar.Append($"<span class='pcoded-mtext'>{sqlData["名稱"]}</span>");
                }
            }
            nav.InnerHtml = newNavbar.ToString();
            sql.Close();
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/backend/signin.aspx");
        }
    }
}