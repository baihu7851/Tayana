using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Tayana.backend.users
{
    public partial class Info : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected static string Md5Password(string str)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = (((FormsIdentity)Page.User.Identity).Ticket.UserData).Split(',');
            if ((Convert.ToInt32(userData[1]) & 1) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/backend/users/list.aspx");
            }
            Global.Id = Request.QueryString["id"];
            Show(Global.Id);
        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;
            Div2.Visible = false;
            password.Visible = true;
            confirmation.Visible = true;
            Global.CmdText = $"UPDATE 使用者 SET 密碼 = @密碼, 信箱 = @信箱, 暱稱 = @暱稱, 圖片 = @圖片, 權限 = @權限 WHERE(Id = {Global.Id})";
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            var sqlCommand = new SqlCommand(Global.CmdText, _sql);
            sqlCommand.Parameters.AddWithValue("@信箱", InputEmail.Value);
            sqlCommand.Parameters.AddWithValue("@暱稱", InputName.Value);
            if (password.Visible)
            {
                sqlCommand.Parameters.AddWithValue("@密碼", Md5Password(InputPassword.Value));
            }
            if (userImgFile.HasFile)
            {
                var imgName = userImgFile.PostedFile.FileName;
                var fileType = imgName.Substring(imgName.LastIndexOf('.') + 1);
                var newImgName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + fileType;
                var path = Server.MapPath("~/upload/images/");
                var imgPath = Server.MapPath("~/upload/images/") + newImgName;
                var directoryInfo = new DirectoryInfo(path);
                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }
                userImgFile.PostedFile.SaveAs(imgPath);
                userImgName.Text = newImgName;
            }
            sqlCommand.Parameters.AddWithValue("@圖片", userImgName.Text);
            var auth = 0;
            auth += userPage.Checked ? Convert.ToInt32(userPage.Value) : 0;
            auth += yachtsPage.Checked ? Convert.ToInt32(yachtsPage.Value) : 0;
            auth += newsPage.Checked ? Convert.ToInt32(newsPage.Value) : 0;
            auth += dealersPage.Checked ? Convert.ToInt32(dealersPage.Value) : 0;
            auth += countryPage.Checked ? Convert.ToInt32(countryPage.Value) : 0;
            auth += areaPage.Checked ? Convert.ToInt32(areaPage.Value) : 0;
            sqlCommand.Parameters.AddWithValue("@權限", auth);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            Response.Redirect("~/backend/users/list.aspx");
        }

        private void Show(string id)
        {
            if (id == "0")
            {
                Global.CmdText = "INSERT INTO 使用者 (密碼, 信箱, 暱稱, 圖片, 權限) VALUES (@密碼, @信箱, @暱稱, @圖片, @權限)";
                userName.InnerText = "新增使用者";
                password.Visible = true;
                confirmation.Visible = true;
                save.Text = "新增";
            }
            else
            {
                Global.CmdText = $"UPDATE 使用者 SET 信箱 = @信箱, 暱稱 = @暱稱, 圖片 = @圖片, 權限 = @權限 WHERE(Id = {id})";
                Div1.Visible = true;
                Div2.Visible = true;
                password.Visible = false;
                confirmation.Visible = false;
                var cmdText = $"SELECT * FROM 使用者 WHERE (id = {id})";
                var sqlCommand = new SqlCommand(cmdText, _sql);
                _sql.Open();
                var sqlData = sqlCommand.ExecuteReader();
                if (sqlData.Read())
                {
                    var auth = Convert.ToInt32(sqlData["權限"]);
                    userName.InnerText = sqlData["暱稱"].ToString();
                    InputEmail.Value = sqlData["信箱"].ToString();
                    InputName.Value = sqlData["暱稱"].ToString();
                    userImg.ImageUrl = $"~/upload/images/{sqlData["圖片"]}";
                    userPage.Checked = Convert.ToBoolean(auth & 1);
                    yachtsPage.Checked = Convert.ToBoolean(auth & 2);
                    newsPage.Checked = Convert.ToBoolean(auth & 4);
                    dealersPage.Checked = Convert.ToBoolean(auth & 8);
                    countryPage.Checked = Convert.ToBoolean(auth & 16);
                    areaPage.Checked = Convert.ToBoolean(auth & 32);
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