<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Tayana.home.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Tayana | Tayana Yachts Official Website</title>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.cycle.all.2.74.js"></script>
    <script type="text/javascript">
        $(function () {
            // 先取得 #abgne-block-20110111 , 必要參數及輪播間隔
            var $block = $('#abgne-block-20110111'),
                timer, speed = 4000;
            // 幫 #abgne-block-20110111 .title ul li 加上 hover() 事件
            var $li = $('.title ul li', $block).hover(function () {
                // 當滑鼠移上時加上 .over 樣式
                $(this).addClass('over').siblings('.over').removeClass('over');
            }, function () {
                // 當滑鼠移出時移除 .over 樣式
                $(this).removeClass('over');
            }).click(function () {
                // 當滑鼠點擊時, 顯示相對應的 div.info
                // 並加上 .on 樣式
                $(this).addClass('on').siblings('.on').removeClass('on');
                var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().eq($(this).index());
                $('#abgne-block-20110111 .bd .banner ul:eq(0)').children().hide().eq($(this).index()).fadeIn(1000);
                if (thisLi.children('input[type=hidden]').val() === 1) {
                    thisLi.children('.new').show();
                }
            });
            // 幫 $block 加上 hover() 事件
            $block.hover(function () {
                // 當滑鼠移上時停止計時器
                clearTimeout(timer);
            }, function () {
                // 當滑鼠移出時啟動計時器
                timer = setTimeout(move, speed);
            });
            // 控制輪播
            function move() {
                var index = $('.title ul li.on', $block).index();
                index = (index + 1) % $li.length;
                $li.eq(index).click();
                timer = setTimeout(move, speed);
            }
            // 啟動計時器
            timer = setTimeout(move, speed);
            //相簿輪撥初始值設定
            $('.title ul li:eq(0)').addClass('on');
            var thisLi = $('#abgne-block-20110111 .bd .banner ul:eq(0) li:eq(0)');
            thisLi.addClass('on');
            if (thisLi.children('input[type=hidden]').val() === "True") {
                thisLi.children('.new').show();
            }
            //最新消息TOP
            $('.newstop').each(function () {
                if ($(this).nextAll('input[type=hidden]').val() === "True") {
                    $(this).show();
                }
            });
        });
    </script>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-30943877-1']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' === document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <link rel="shortcut icon" href="favicon.ico" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/reset.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="contain">
            <div class="sub">
                <p>
                    <a href="index.aspx">Home</a>
                </p>
            </div>
            <!--------------------------------選單開始---------------------------------------------------->
            <div id="logol">
                <a href="index.aspx">
                    <img src="images/logo001.gif" alt="Tayana" /></a>
            </div>
            <div class="menu">
                <ul>
                    <li class="menuli01"><a href="Yachts_OverView.aspx">Yachts</a></li>
                    <li class="menuli02"><a href="new_list.aspx">NEWS</a></li>
                    <li class="menuli03"><a href="company.aspx">COMPANY</a></li>
                    <li class="menuli04"><a href="dealers.aspx">DEALERS</a></li>
                    <li class="menuli05"><a href="contact.aspx">CONTACT</a></li>
                </ul>
            </div>
            <!--------------------------------選單開始結束---------------------------------------------------->
            <!--遮罩-->
            <div class="bannermasks">
                <img src="images/banner00_masks.png" alt="&quot;&quot;" />
            </div>
            <!--遮罩結束-->
            <!--------------------------------換圖開始---------------------------------------------------->
            <div id="abgne-block-20110111">
                <div class="bd">
                    <div class="banner">
                        <asp:Repeater ID="RepeaterBanner" runat="server">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="info">
                                    <a href="#" target="_blank">
                                        <img src='../upload/Images/<%#Eval("圖片") %>' onerror="this.src='/errorImg/yacht.jpg'" alt="" />
                                    </a>
                                    <!--文字開始-->
                                    <div class="wordtitle">
                                        <%#Eval("船名") %>
                                        <span><%#Eval("船號") %></span>
                                        <br />
                                        <p>SPECIFICATION SHEET</p>
                                    </div>
                                    <!--文字結束-->
                                    <!--新船型開始-->
                                    <div class="new" style="display: none">
                                        <img src="images/new01.png" alt="new" />
                                    </div>
                                    <!--新船型結束-->
                                    <input type="hidden" value='<%#Eval("新船") %>' />
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <!--小圖開始-->
                        <div class="bannerimg title" style="display: none">
                            <asp:Repeater ID="RepeaterBannerimg" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li>
                                        <div>
                                            <p class="bannerimg_p">
                                                <img src='../upload/Images/sm/sm-<%#Eval("圖片") %>' onerror="this.src='/errorImg/sm-yacht.jpg'" alt="" />
                                            </p>
                                        </div>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                        <!--小圖結束-->
                    </div>
                </div>
            </div>
            <!--------------------------------換圖結束---------------------------------------------------->
            <!--------------------------------最新消息---------------------------------------------------->
            <div class="news">
                <div class="newstitle">
                    <p class="newstitlep1">
                        <img src="images/news.gif" alt="news" />
                    </p>
                    <p class="newstitlep2">
                        <a href="new_list.aspx">More>></a>
                    </p>
                </div>
                <ul>
                    <asp:Repeater ID="RepeaterNews" runat="server">
                        <ItemTemplate>
                            <li>
                                <!--TOP第一則最新消息-->
                                <div class="news01">
                                    <!--TOP標籤-->
                                    <div class="newstop" style="display: none">
                                        <img src="images/new_top01.png" alt="&quot;&quot;" />
                                    </div>
                                    <!--TOP標籤結束-->
                                    <div class="news02p1">
                                        <p class="news02p1img">
                                            <img id="Repeater3_ctl01_Image1" src="../upload/Images/sm/sm-<%#Eval("圖片") %>" onerror="this.src='/errorImg/sm-yacht.jpg'" style="border-width: 0;" alt="" />
                                        </p>
                                    </div>
                                    <p class="news02p2">
                                        <span>
                                            <font color="#02a5b8"><%# Eval("日期", "{0:d}") %></font>
                                        </span>
                                        <span><a href="new_view.aspx?id=<%#Eval("Id") %>"><%#Eval("標題") %></a></span>
                                    </p>
                                    <input type="hidden" value='<%#Eval("置頂") %>' />
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!--------------------------------最新消息結束---------------------------------------------------->
            <!--------------------------------落款開始---------------------------------------------------->
            <div class="footer">
                <div class="footerp00">
                    <a href="http://www.tognews.com/" target="_blank">
                        <p>
                            <img src="images/tog.jpg" alt="TOG" />
                        </p>
                    </a>
                    <span>Visitor：
                    <div style="padding-top: 1px; float: right">
                        <img src='http://comp.hihosting.hinet.net/scripts/Count.exe?display=counter&df=89940168_1.dat&dd=A002&st=1&ft=0' height="13" />
                    </div>
                    </span>
                    <p class="footerp001">© 1973-2012 Tayana Yachts, Inc. All Rights Reserved</p>
                </div>
                <div class="footer01">
                    <span>No. 60, Hai Chien Road, Chung Men Li, Lin Yuan District, Kaohsiung City, Taiwan, R.O.C.</span><br />
                    <span>TEL：+886(7)641-2721</span> <span>FAX：+886(7)642-3193</span>
                </div>
            </div>

            <!--------------------------------落款結束---------------------------------------------------->
        </div>
    </form>
</body>
</html>