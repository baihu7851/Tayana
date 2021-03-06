<%@ Page Title="" Language="C#" MasterPageFile="~/home/Site.Master" AutoEventWireup="true" CodeBehind="Yachts_Specification.aspx.cs" Inherits="Tayana.home.YachtsSpecification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="css/jquery.ad-gallery.css" />
    <style type="text/css">
        img,
        div,
        input {
            behavior: url("");
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.topbuttom').click(function () {
                $('html, body').scrollTop(0);
            });
        });
    </script>
    <script type="text/javascript" src="Scripts/jquery.ad-gallery.js"></script>
    <script type="text/javascript">
        $(function () {
            var galleries = $('.ad-gallery').adGallery();
            galleries[0].settings.effect = 'fade';
            if ($('.banner input[type=hidden]').val() === "0") {
                $(".bannermasks").hide();
                $(".banner").hide();
                $("#crumb").css("top", "125px");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--換圖開始--%>
    <!--遮罩-->
    <div class="bannermasks">
        <img src="images/banner01_masks.png" alt="" />
    </div>
    <!--遮罩結束-->
    <div class="banner1">
        <input type="hidden" name="HiddenField1"
            id="HiddenField1" value="1" />
        <div id="gallery" class="ad-gallery">
            <div class="ad-image-wrapper">
            </div>
            <div class="ad-controls">
            </div>
            <div class="ad-nav">
                <div class="ad-thumbs">
                    <ul id="adTumb" class="ad-thumb-list" runat="server">
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <%--換圖結束--%>
    <div class="conbg">
        <%--左邊選單開始--%>
        <div class="left">
            <div class="left1">
                <p><span>YACHTS</span></p>
                <ul id="ulYachts" runat="server">
                </ul>
            </div>
        </div>
        <%--左邊選單結束--%>
        <%--右邊選單開始--%>
        <div id="crumb1">
            <a href="index.aspx">Home</a> >>
            <a href="#">Yachts</a> >>
            <a href="Yachts_Specification.aspx">
                <span id="crumbName" class="on1" runat="server"></span>
            </a>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title"><span id="rightName" runat="server"></span></div>
                <%--內容開始--%>
                <%--次選單--%>
                <div class="menu_y">
                    <ul>
                        <li class="menu_y00">YACHTS</li>
                        <li><a id="yo" runat="server" class="menu_yli01">OverView</a></li>
                        <li><a id="yl" runat="server" class="menu_yli02">Layout & deck plan</a></li>
                        <li><a id="ys" runat="server" class="menu_yli03">Specification</a></li>
                    </ul>
                </div>
                <%--次選單--%>
                <div id="view" runat="server"></div>
                <%--內容結束--%>
            </div>
        </div>
        <%--右邊選單結束--%>
    </div>
</asp:Content>