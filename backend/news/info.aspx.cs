using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Security;
using Tayana.backend.Utils;

namespace Tayana.backend.news
{
    public partial class Info : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = (((FormsIdentity)Page.User.Identity).Ticket.UserData).Split(',');
            if ((Convert.ToInt32(userData[1]) & 4) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/backend/news/list.aspx");
            }
            Global.Id = Request.QueryString["id"];
            Show(Global.Id);
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var sqlCommand = new SqlCommand(Global.CmdText, _sql);
            sqlCommand.Parameters.AddWithValue("@標題", title.Value);
            sqlCommand.Parameters.AddWithValue("@副標題", subTitle.Value);
            sqlCommand.Parameters.AddWithValue("@內文", newsText.Text);
            if (newsImgFile.HasFile)
            {
                var imgName = newsImgFile.PostedFile.FileName;
                var fileType = imgName.Substring(imgName.LastIndexOf('.') + 1);
                var newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var path = Server.MapPath("~/upload/images/");
                var imgPath = Server.MapPath("~/upload/images/") + newImgName;
                var directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                newsImgFile.PostedFile.SaveAs(imgPath);
                Tools.GenerateThumbnailImage(newImgName, @"C:\Users\work\Desktop\Project\Tayana\upload\images", @"C:\Users\work\Desktop\Project\Tayana\upload\images\sm", "sm-", 59);

                newsImgName.Text = newImgName;
            }
            sqlCommand.Parameters.AddWithValue("@圖片", newsImgName.Text);
            sqlCommand.Parameters.AddWithValue("@置頂", isTop.Checked ? 1 : 0);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            Response.Redirect("~/backend/news/list.aspx");
        }

        private void Show(string id)
        {
            if (id == "0")
            {
                Global.CmdText = "INSERT INTO 新聞 (標題, 副標題, 內文, 圖片, 置頂) VALUES (@標題, @副標題, @內文, @圖片, @置頂)";
                newsName.InnerText = "新增新聞";
                save.Text = "新增";
            }
            else
            {
                Global.CmdText = $"UPDATE 新聞 SET 標題 = @標題, 副標題 = @副標題, 內文 = @內文, 圖片 = @圖片, 置頂 = @置頂 WHERE(Id = {id})";
                var cmdText = $"SELECT * FROM 新聞 WHERE (id = {id})";
                var sqlCommand = new SqlCommand(cmdText, _sql);
                _sql.Open();
                var sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    newsName.InnerText = sqlData["標題"].ToString();
                    title.Value = sqlData["標題"].ToString();
                    subTitle.Value = sqlData["副標題"].ToString();
                    newsText.Text = sqlData["內文"].ToString();
                    newsImgName.Text = sqlData["圖片"].ToString();
                    //newsImg.ImageUrl = $"~/upload/images/sm/sm-{sqlData["圖片"]}";
                    isTop.Checked = Convert.ToBoolean(sqlData["置頂"]);
                }
                _sql.Close();
            }
        }

        public class Global
        {
            public static string CmdText;
            public static string Id;
        }
    }
}