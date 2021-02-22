using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tayana.backend.Utils;

namespace Tayana.backend.dealers
{
    public partial class Info : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = ((FormsIdentity)Page.User.Identity).Ticket.UserData.Split(',');
            if ((Convert.ToInt32(userData[1]) & 8) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/backend/dealers/list.aspx");
            }
            Global.Id = Request.QueryString["id"];
            Show();
        }

        protected void CountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dealersCountry"] = countryList.SelectedValue == "0" ? "" : countryList.SelectedValue;
            ShowArea();
        }

        protected void AreaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["dealersArea"] = areaList.SelectedValue == "0" ? "" : areaList.SelectedValue;
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var sqlCommand = new SqlCommand(Global.CmdText, _sql);
            sqlCommand.Parameters.AddWithValue("@Pid", Convert.ToInt32(areaList.SelectedValue));
            sqlCommand.Parameters.AddWithValue("@名稱", inputName.Value);
            sqlCommand.Parameters.AddWithValue("@資訊", information.Text);
            if (dealerImgFile.HasFile)
            {
                var imgName = dealerImgFile.PostedFile.FileName;
                var fileType = imgName.Substring(imgName.LastIndexOf('.') + 1);
                var newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var path = Server.MapPath("~/upload/images/");
                var imgPath = Server.MapPath("~/upload/images/") + newImgName;
                var directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                dealerImgFile.PostedFile.SaveAs(imgPath);
                Tools.GenerateThumbnailImage(newImgName, @"C:\Users\work\Desktop\Project\Tayana\upload\images", @"C:\Users\work\Desktop\Project\Tayana\upload\images\sm", "sm-", 59);
                dealerImgName.Text = newImgName;
            }
            sqlCommand.Parameters.AddWithValue("@圖片", dealerImgName.Text);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            Response.Redirect("~/backend/dealers/list.aspx");
        }

        private void Show()
        {
            if (Global.Id == "0")
            {
                Global.CmdText = "INSERT INTO 代理商 (Pid, 名稱, 圖片, 資訊) VALUES (@Pid, @名稱, @圖片, @資訊)";
                dealerName.InnerText = "新增代理商";
                save.Text = "新增";
            }
            else
            {
                Global.CmdText = $"UPDATE 代理商 SET Pid = @Pid, 名稱 = @名稱, 圖片 = @圖片, 資訊 = @資訊 WHERE (id = {Global.Id})";
                var cmdText = $@"
                SELECT 代理商.名稱, 代理商.圖片, 代理商.資訊, 地區.Id AS 地區Id, 地區.地區名 AS 地區, 國家.Id AS 國家Id, 國家.國名 AS 國家
                FROM 代理商 INNER JOIN 地區 ON 代理商.Pid = 地區.Id INNER JOIN 國家 ON 地區.Pid = 國家.Id WHERE(代理商.Id = {Global.Id})";
                var sqlCommand = new SqlCommand(cmdText, _sql);
                _sql.Open();
                var sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    dealerName.InnerText = sqlData["名稱"].ToString();
                    inputName.Value = sqlData["名稱"].ToString();
                    dealerImg.ImageUrl = $"~/upload/images/sm/sm-{sqlData["圖片"]}";
                    dealerImgName.Text = sqlData["圖片"].ToString();
                    information.Text = sqlData["資訊"].ToString();
                    Session["dealersCountry"] = sqlData["國家Id"];
                    Session["dealersArea"] = sqlData["地區Id"];
                }
                _sql.Close();
            }
            ShowCountry();
            ShowArea();
        }

        private void ShowCountry()
        {
            countryList.Items.Clear();
            var cmdText = new SqlCommand("SELECT * FROM 國家 WHERE (刪除 = 0) ORDER BY 國名", _sql);
            _sql.Open();
            var sqlData = cmdText.ExecuteReader();
            while (sqlData.Read())
            {
                countryList.Items.Add(new ListItem(sqlData["國名"].ToString(), sqlData["Id"].ToString()));
            }
            if (Session["dealersCountry"] != null)
            {
                try
                {
                    countryList.SelectedValue = Session["dealersCountry"].ToString();
                }
                catch
                {
                    // ignored
                }
            }
            _sql.Close();
        }

        private void ShowArea()
        {
            areaList.Items.Clear();
            var select = Session["dealersCountry"] == null ? "" : $"AND 國家.Id = {Session["dealersCountry"]}";
            var cmdText = $"SELECT 地區.Id, 地區.地區名 FROM 國家 INNER JOIN 地區 ON 國家.Id = 地區.Pid WHERE (地區.刪除 = 0) AND 1=1 {select}";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            _sql.Open();
            var sqlData = sqlCommand.ExecuteReader();
            while (sqlData.Read())
            {
                areaList.Items.Add(new ListItem(sqlData["地區名"].ToString(), sqlData["Id"].ToString()));
            }
            if (Session["dealersArea"] != null)
            {
                try
                {
                    areaList.SelectedValue = Session["dealersArea"].ToString();
                }
                catch
                {
                    // ignored
                }
            }
            _sql.Close();
        }

        public class Global
        {
            public static string CmdText;
            public static string Id;
        }
    }
}