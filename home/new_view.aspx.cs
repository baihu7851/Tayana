using System;
using System.Data.SqlClient;

namespace Tayana.home
{
    public partial class NewView : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Show();
        }

        private void Show()
        {
            var id = Request.QueryString["id"];
            var cmdText = $"SELECT 內文 FROM 新聞 WHERE (Id = {id})";
            var command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var dataReader = command.ExecuteReader();
            if (dataReader.Read())
            {
                view.InnerHtml = dataReader["內文"].ToString();
            }
            _sql.Close();
        }
    }
}