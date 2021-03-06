using System;
using System.Data.SqlClient;
using System.IO;
using System.Web.Configuration;
using System.Web.Security;
using Tayana.backend.Utils;

namespace Tayana.backend.yachts
{
    public partial class Info : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = (((FormsIdentity)Page.User.Identity).Ticket.UserData).Split(',');
            if ((Convert.ToInt32(userData[1]) & 2) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/backend/yachts/list.aspx");
            }
            Global.Id = Request.QueryString["id"];
            Show();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("~/backend/yachts/list.aspx");
        }

        protected void Next_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect($"~/backend/yachts/file.aspx?id={Global.Id}");
        }

        private void Show()
        {
            var id = Global.Id;
            if (id == "0")
            {
                Global.CmdText = "INSERT INTO 船 (船名, 船號, 新船, 概觀, 規格, 佈局, 圖片) VALUES (@船名, @船號, @新船, @概觀, @規格, @佈局, @圖片)";
                yachName.InnerText = "新增船型";
                btnSave.Text = "新增";
            }
            else
            {
                Global.CmdText = $"UPDATE 船 SET 船名 = @船名, 船號 = @船號, 新船 = @新船, 概觀 = @概觀, 規格 = @規格, 佈局 = @佈局, 圖片 = @圖片 WHERE (Id = {id})";
                var sqlString = $"SELECT * FROM 船 WHERE (id = {id})";
                var sqlCommand = new SqlCommand(sqlString, _sql);
                _sql.Open();
                var sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    yachName.InnerText = sqlData["船名"] + " " + sqlData["船號"];
                    inputYachName.Value = sqlData["船名"] + " " + sqlData["船號"];
                    isNew.Checked = Convert.ToBoolean(sqlData["新船"]);
                    txtOverView.Text = sqlData["概觀"].ToString();
                    txtspecification.Text = sqlData["規格"].ToString();
                    txtLayout.Text = sqlData["佈局"].ToString();
                    yachtImgName.Text = sqlData["圖片"].ToString();
                    yachtImg.ImageUrl = $"~/upload/images/sm/sm-{sqlData["圖片"]}";
                }
                _sql.Close();
            }
        }

        private void Save()
        {
            var sqlCommand = new SqlCommand(Global.CmdText, _sql);
            var newName = inputYachName.Value.Split(' ');
            sqlCommand.Parameters.AddWithValue("@船名", newName[0]);
            sqlCommand.Parameters.AddWithValue("@船號", newName[1]);
            sqlCommand.Parameters.AddWithValue("@新船", isNew.Checked);
            sqlCommand.Parameters.AddWithValue("@概觀", txtOverView.Text);
            sqlCommand.Parameters.AddWithValue("@規格", txtspecification.Text);
            sqlCommand.Parameters.AddWithValue("@佈局", txtLayout.Text);

            if (yachtImgFile.HasFile)
            {
                var imgName = yachtImgFile.PostedFile.FileName;
                var fileType = imgName.Substring(imgName.LastIndexOf('.') + 1);
                var newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var path = Server.MapPath("~/upload/images/");
                var imgPath = Server.MapPath("~/upload/images/") + newImgName;
                var directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                yachtImgFile.PostedFile.SaveAs(imgPath);
                Tools.GenerateThumbnailImage(newImgName, WebConfigurationManager.AppSettings["source"], WebConfigurationManager.AppSettings["target"], "sm-", 59);
                yachtImgName.Text = newImgName;
            }
            sqlCommand.Parameters.AddWithValue("@圖片", yachtImgName.Text);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
        }

        public class Global
        {
            public static string CmdText;
            public static string Id;
        }
    }
}