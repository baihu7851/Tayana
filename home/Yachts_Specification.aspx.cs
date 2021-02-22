using System;
using System.Data.SqlClient;

namespace Tayana.home
{
    public partial class YachtsSpecification : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var id = Request.QueryString["id"] ?? "1";
            yo.HRef = $"Yachts_OverView.aspx?id={id}";
            yl.HRef = $"Yachts_Layout.aspx?id={id}";
            ys.HRef = $"Yachts_Specification.aspx?id={id}";
            Show();
        }

        private void Show()
        {
            var id = Request.QueryString["id"] ?? "1";
            var cmdText = "SELECT Id, 船名, 船號, 新船 FROM 船";
            var command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var leftRepeater = command.ExecuteReader();
            while (leftRepeater.Read())
            {
                var name = leftRepeater["船名"] + " " + leftRepeater["船號"];
                var listId = leftRepeater["Id"].ToString();
                if ((bool)leftRepeater["新船"])
                {
                    name += " ( New Building ) ";
                }

                ulYachts.InnerHtml += $"<li><a href='Yachts_OverView.aspx?id={listId}'>{name}</a></li>";
            }
            _sql.Close();

            cmdText = $"SELECT 船.船名, 船.船號, 船.規格, 船_相片集.船圖 FROM 船 LEFT JOIN 船_相片集 ON 船.Id = 船_相片集.Pid WHERE (船.Id = {id})";
            command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var interiorReader = command.ExecuteReader();
            while (interiorReader.Read())
            {
                crumbName.InnerText = interiorReader["船名"] + " " + interiorReader["船號"];
                rightName.InnerText = interiorReader["船名"] + " " + interiorReader["船號"];
                view.InnerHtml = interiorReader["規格"].ToString();
                var interior = interiorReader["船圖"].ToString();
                adTumb.InnerHtml +=
                    $"<li><a href='../../upload/Images/{interior}'><img src = '../../upload/Images/s{interior}' class='image0' /></a></li>";
            }
            _sql.Close();
        }
    }
}