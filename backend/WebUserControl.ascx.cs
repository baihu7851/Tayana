using System;
using System.Text;

namespace Tayana.backend
{
    public partial class WebUserControl : System.Web.UI.UserControl
    {
        private int _totalitems;

        private int _limit;

        private string _targetpage;

        public int TotalItems
        {
            get
            {
                return _totalitems;
            }
            set
            {
                _totalitems = value;
            }
        }

        public int limit
        {
            get
            {
                return _limit;
            }
            set
            {
                _limit = value;
            }
        }

        public string Targetpage
        {
            get
            {
                return _targetpage;
            }
            set
            {
                _targetpage = value;
            }
        }

        public void ShowPageControls()
        {
            litPage.Text = "";//清空
            var page = 1;
            if (!string.IsNullOrEmpty(Request["page"]))
            {
                if (IsNumber(Request["page"]))
                {
                    page = Convert.ToInt16(Request["page"]);
                }
            }
            if (TotalItems == 0)
            {
                return;
            }
            if (limit == 0)
            {
                return;
            }
            Targetpage = Targetpage ?? System.IO.Path.GetFileName(Request.PhysicalPath);
            litPage.Text = GetPaginationString(page, TotalItems, limit, 2, Targetpage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowPageControls();
        }

        #region "判斷是否為數字"

        /// <summary>
        /// 判斷是否為數字
        /// </summary>
        /// <param name="inputData">輸入字串</param>
        /// <returns>bool</returns>
        private bool IsNumber(string inputData)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(inputData, "^[0-9]+$");
        }

        #endregion "判斷是否為數字"

        #region "產生分頁控制項"

        /// <summary>
        /// 產生分頁控制項
        /// </summary>
        /// <param name="page">目前第幾頁</param>
        /// <param name="totalitems">共有幾筆</param>
        /// <param name="limit">一頁幾筆</param>
        /// <param name="adjacents">不知道，傳2~5都OK</param>
        /// <param name="targetpage">連結文字，例:pagination.aspx?foo=bar</param>
        /// <returns></returns>
        public static string GetPaginationString(int page, int totalitems, int limit, int adjacents, string targetpage)
        {
            //defaults
            targetpage = targetpage.IndexOf('?') != -1 ? targetpage + "&" : targetpage + "?";
            //other vars
            var prev = page - 1;
            //previous page is page - 1
            var nextPage = page + 1;
            //nextPage page is page + 1
            var value = Convert.ToDouble((decimal)totalitems / limit);
            var lastpage = Convert.ToInt16(Math.Ceiling(value));
            //lastpage is = total items / items per page, rounded up.
            var lpm1 = lastpage - 1;
            //last page minus 1
            var counter = 0;
            // Now we apply our rules and draw the pagination object.
            // We're actually saving the code to a variable in case we want to draw it more than once.
            var paginationBuilder = new StringBuilder();
            if (lastpage > 1)
            {
                paginationBuilder.Append("<nav aria-label=\"Page navigation example\"><ul class=\"pagination\">");
                //paginationBuilder.Append(">共<span style=\"color:red\" >" + totalitems + "</span>筆資料");
                //previous button
                paginationBuilder.Append(page > 1 ? string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={prev}\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>") : $"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\" aria-label=\"Previous\"><span aria-hidden=\"true\">&laquo;</span></a></li>");

                //pages
                if (lastpage < 7 + (adjacents * 2))
                {
                    //not enough pages to bother breaking it up
                    for (counter = 1; counter <= lastpage; counter++)
                    {
                        //paginationBuilder.Append(counter == page ? string.Format($"<span class=\"current\">{counter}</span>") : string.Format($"<a href=\"{targetpage}page={counter}\">{counter}</a>"));
                        //paginationBuilder.Append(counter == page ? string.Format($"<li class=\"page-item\"><span class=\"page-link\">{counter}</span></li>") : string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={counter}\">{counter}</a></li>"));
                        paginationBuilder.Append(counter == page ? string.Format($"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\">{counter}</a></li>") : string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={counter}\">{counter}</a></li>"));
                    }
                }
                else if (lastpage >= 7 + (adjacents * 2))
                {
                    //enough pages to hide some
                    //close to beginning only hide later pages
                    if (page < 1 + (adjacents * 3))
                    {
                        for (counter = 1; counter <= (4 + (adjacents * 2)) - 1; counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format($"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\">{counter}</a></li>") : string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={counter}\">{counter}</a></li>"));
                        }
                        paginationBuilder.Append("...");
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={lpm1}\">{lpm1}</a></li>"));
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={lastpage}\">{lastpage}</a></li>"));
                    }
                    //in middle hide some front and some back
                    else if (lastpage - (adjacents * 2) > page & page > (adjacents * 2))
                    {
                        //paginationBuilder.Append(string.Format("<a href=\"{0}page=1\">1</a>", targetpage));
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page=1\">1</a></li>"));

                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page=2\">2</a></li>"));
                        paginationBuilder.Append("...");
                        for (counter = (page - adjacents); counter <= (page + adjacents); counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format($"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\">{counter}</a></li>") : string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={counter}\">{counter}</a></li>"));
                        }
                        paginationBuilder.Append("...");
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={lpm1}\">{lpm1}</a></li>"));
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={lastpage}\">{lastpage}</a></li>"));
                    }
                    else
                    {
                        //close to end only hide early pages
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page=1\">1</a></li>"));
                        paginationBuilder.Append(string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page=1\">1</a></li>"));
                        paginationBuilder.Append("...");
                        for (counter = (lastpage - (1 + (adjacents * 3))); counter <= lastpage; counter++)
                        {
                            paginationBuilder.Append(counter == page ? string.Format($"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\">{counter}</a></li>") : string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={counter}\">{counter}</a></li>"));
                        }
                    }
                }
                //nextPage button
                //paginationBuilder.Append(page < counter - 1 ? string.Format("<a href=\"{0}page={1}\">下一頁</a>", targetpage, nextPage) : "<span class=\"disabled page-link\">下一頁</span>");
                paginationBuilder.Append(page < counter - 1 ? string.Format($"<li class=\"page-item\"><a class=\"page-link\" href=\"{targetpage}page={nextPage}\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>") : $"<li class=\"page-item disabled\"><a class=\"page-link\" tabindex=\"-1\" aria-label=\"Next\"><span aria-hidden=\"true\">&raquo;</span></a></li>");

                paginationBuilder.Append("</ul></nav>\r\n");
            }
            return paginationBuilder.ToString();
        }

        #endregion "產生分頁控制項"
    }
}