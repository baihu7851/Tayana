using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Tayana.backend
{
    public partial class Area : System.Web.UI.Page
    {
        private readonly SqlConnection _sql = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["TayanaConnectionString"].ConnectionString);

        protected void Page_Init(object sender, EventArgs e)
        {
            var userData = (((FormsIdentity)Page.User.Identity).Ticket.UserData).Split(',');
            if ((Convert.ToInt32(userData[1]) & 32) == 0)
            {
                Response.Redirect("~/backend/signin.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var cmdText = new SqlCommand("SELECT * FROM 國家 ORDER BY 國名", _sql);
            _sql.Open();
            var sqlData = cmdText.ExecuteReader();
            while (sqlData.Read())
            {
                countryList.Items.Add(new ListItem(sqlData["國名"].ToString(), sqlData["Id"].ToString()));
            }
            _sql.Close();
            Show();
        }

        protected void Repeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Del") return;
            var id = Convert.ToInt32(e.CommandArgument);
            var cmdText = $"UPDATE 地區 SET 刪除 = 1 WHERE (Id = {id})";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            countryList.SelectedIndex = 0;
            Show();
        }

        protected void NewArea_Click(object sender, EventArgs e)
        {
            if (countryList.SelectedIndex == 0) return;
            const string cmdText = "INSERT INTO 地區 (Pid, 地區名) VALUES (@Pid, @地區名)";
            var sqlCommand = new SqlCommand(cmdText, _sql);
            sqlCommand.Parameters.AddWithValue("@Pid", countryList.SelectedValue);
            sqlCommand.Parameters.AddWithValue("@地區名", areaName.Text);
            _sql.Open();
            sqlCommand.ExecuteNonQuery();
            _sql.Close();
            areaName.Text = "";
            Show();
        }

        protected void CountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["Country"] = countryList.SelectedIndex;
            Show();
        }

        private void Show()
        {
            var select = "";
            if (Session["Country"] != null)
            {
                countryList.SelectedIndex = (int)Session["Country"];
                select = countryList.SelectedValue == "0" ? "" : $"AND 國家.Id = {countryList.SelectedValue}";
            }
            var page = 0;
            const int onePage = 10;
            var pageNumber = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"]);
            var cmdText = new SqlCommand($@"
                WITH Page AS
                (
                    select ROW_NUMBER() over(order by 地區.Id DESC) as 編號, 地區.Id, 地區.地區名
                    FROM 地區 INNER JOIN 國家 ON 地區.Pid = 國家.Id WHERE (地區.刪除 = 0) AND 1=1 {select}
                )
                SELECT * FROM Page WHERE 編號 >={ (pageNumber - 1) * onePage + 1 }AND 編號<={ pageNumber * onePage}", _sql);
            var table = new DataTable();
            var sqlData = new SqlDataAdapter(cmdText);
            sqlData.Fill(table);
            Repeater.DataSource = table;
            Repeater.DataBind();
            _sql.Open();
            var count = new SqlCommand("SELECT count(*) FROM 地區 WHERE (刪除 = 0)", _sql);
            var countData = count.ExecuteReader();
            if (countData.Read())
            {
                page = Convert.ToInt32(countData[0]);
            }
            WebUserControl.TotalItems = page;
            WebUserControl.limit = onePage;
            WebUserControl.Targetpage = "area.aspx";
            WebUserControl.ShowPageControls();
            _sql.Close();
        }
    }
}