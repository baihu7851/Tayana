using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace Tayana.home
{
    public partial class YachtsOverView : Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
        public string Id { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Id = GetId();
            yo.HRef = $"Yachts_OverView.aspx?id={Id}";
            yl.HRef = $"Yachts_Layout.aspx?id={Id}";
            ys.HRef = $"Yachts_Specification.aspx?id={Id}";
            ShowData();
            ShowImg();
        }

        private string GetId()
        {
            string id = null;
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }
            else
            {
                var command = new SqlCommand("SELECT TOP 1 * FROM 船 WHERE 刪除 = 0 ORDER BY Id DESC", _sql);
                _sql.Open();
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    id = dataReader["Id"].ToString();
                }
                _sql.Close();
            }
            return id;
        }

        private void ShowData()
        {
            var id = Request.QueryString["id"] ?? "1";
            var cmdText = "SELECT Id, 船名, 船號, 新船 FROM 船 Where 刪除 = 0  ORDER BY Id DESC";
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

            cmdText = $"SELECT 船名, 船號, 概觀 FROM 船 WHERE (Id = {Id})";
            command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                crumbName.InnerText = reader["船名"] + " " + reader["船號"];
                rightName.InnerText = reader["船名"] + " " + reader["船號"];
                view.InnerHtml = reader["概觀"].ToString();
            }
            _sql.Close();

            cmdText = $"SELECT Pid, 檔案 FROM 船_檔案集 WHERE (Pid = {Id})";
            command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var downloadReader = command.ExecuteReader();
            while (downloadReader.Read())
            {
                var download = downloadReader["檔案"].ToString();
                downloadsLink.InnerHtml += $"<li><a href = '../../upload/File/{download}' target = '_blank' >{download}</ a></ li> ";
            }
            _sql.Close();
        }

        private void ShowImg()
        {
            var cmdText = $"SELECT 船.船名, 船.船號, 船.概觀, 船_相片集.船圖 FROM 船 LEFT JOIN 船_相片集 ON 船.Id = 船_相片集.Pid WHERE (船.Id = {Id})";
            var command = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var interiorReader = command.ExecuteReader();
            while (interiorReader.Read())
            {
                adTumb.InnerHtml += $"<li><a href='../../upload/images/{interiorReader["船圖"]}'><img src = '../../upload/images/sm/sm-{interiorReader["船圖"]}' class='image0' /></a></li>";
            }
            _sql.Close();
        }
    }
}