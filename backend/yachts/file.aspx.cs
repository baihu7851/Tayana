using System;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Tayana.backend.Utils;

namespace Tayana.backend.yachts
{
    public partial class File : System.Web.UI.Page
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
            switch (Request.QueryString["id"])
            {
                case null:
                    Response.Redirect("~/backend/yachts/list.aspx");
                    break;

                case "0":
                    var cmdText = "SELECT top 1 Id FROM 船 ORDER BY Id DESC";
                    var sqlCommand = new SqlCommand(cmdText, _sql);
                    _sql.Open();
                    var sqlDataReader = sqlCommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        Global.Id = sqlDataReader["id"].ToString();
                    }
                    _sql.Close();
                    break;

                default:
                    Global.Id = Request.QueryString["id"];
                    break;
            }
            Show();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            if (interiors.HasFile)
            {
                foreach (var httpPostedFile in interiors.PostedFiles)
                {
                    if (httpPostedFile.ContentType.Contains("image"))
                    {
                        SaveInterior(httpPostedFile);
                    }
                }
            }
            if (filed.HasFile)
            {
                foreach (var httpPostedFile in filed.PostedFiles)
                {
                    SaveFile(httpPostedFile);
                }
            }
            Response.Redirect("~/backend/yachts/list.aspx");
        }

        protected void Prev_Click(object sender, EventArgs e)
        {
            if (interiors.HasFile)
            {
                foreach (var httpPostedFile in interiors.PostedFiles)
                {
                    if (httpPostedFile.ContentType.Contains("image"))
                    {
                        SaveInterior(httpPostedFile);
                    }
                }
            }
            if (filed.HasFile)
            {
                foreach (var httpPostedFile in filed.PostedFiles)
                {
                    SaveFile(httpPostedFile);
                }
            }
            Response.Redirect($"~/backend/yachts/info.aspx?id={Global.Id}");
        }

        private void Show()
        {
            var id = Global.Id;
            var cmdText = $"SELECT * FROM 船 WHERE (Id = {id})";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var sqlData = sqlCommand.ExecuteReader();
            if (sqlData.Read())
            {
                yachName.InnerText = sqlData["船名"] + " " + sqlData["船號"];
            }
            sqlData.Close();
            _sql.Close();
        }

        private void SaveInterior(HttpPostedFile interior)
        {
            var cmdText = $"INSERT INTO 船_相片集 (Pid, 船圖) VALUES ({Global.Id}, @船圖)";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            var imgName = interior.FileName;
            var fileType = imgName.Substring(imgName.LastIndexOf('.') + 1);
            var newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
            var path = Server.MapPath("~/upload/images/");
            var imgPath = Server.MapPath("~/upload/images/") + newImgName;
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            interior.SaveAs(imgPath);
            Tools.GenerateThumbnailImage( newImgName, WebConfigurationManager.AppSettings["source"], WebConfigurationManager.AppSettings["target"], "sm-",59);
            sqlCommand.Parameters.AddWithValue("@船圖", newImgName);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
        }

        private void SaveFile(HttpPostedFile file)
        {
            var cmdText = $"INSERT INTO 船_檔案集 (Pid, 檔案) VALUES ({Global.Id}, @檔案)";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            var fileName = file.FileName;
            var fileType = fileName.Substring(fileName.LastIndexOf('.') + 1);
            var newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
            var path = Server.MapPath("~/upload/File/");
            var filePath = Server.MapPath("~/upload/File/") + newFileName;
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            file.SaveAs(filePath);
            sqlCommand.Parameters.AddWithValue("@檔案", newFileName);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
        }

        public class Global
        {
            public static string Id;
        }
    }
}