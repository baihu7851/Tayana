using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Tayana.backend
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            var sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);
            const string cmdText = "SELECT * FROM [使用者] WHERE (信箱 = @信箱) AND (密碼 = @密碼)";
            var sqlCommand = new SqlCommand(cmdText, sql);
            sqlCommand.Parameters.AddWithValue("@信箱", email.Value);
            sqlCommand.Parameters.AddWithValue("@密碼", Md5Hash(password.Value));
            sql.Open();
            var sqlData = sqlCommand.ExecuteReader();
            if (sqlData.Read())
            {
                SetAuthTicket(sqlData["暱稱"].ToString(), $"{sqlData["圖片"]},{sqlData["權限"]}", rememberMe.Checked);
                sql.Close();
                Response.Redirect("~/backend/index.aspx");
            }
            else
            {
                sql.Close();
            }
        }

        private static string Md5Hash(string value)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            foreach (var i in data)
            {
                sBuilder.Append(i.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private void SetAuthTicket(string userName, string userData, bool remember)
        {
            const int userTime = 7;
            //宣告一個驗證票
            var ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddDays(userTime), remember, userData);
            //加密驗證票
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            //建立Cookie
            var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                //帳號保留時間
                Expires = DateTime.Now.AddDays(userTime)
            };
            //將Cookie寫入回應
            Response.Cookies.Add(authenticationCookie);
        }
    }
}