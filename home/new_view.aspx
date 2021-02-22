<%@ Page Title="" Language="C#" MasterPageFile="~/home/Site.Master" AutoEventWireup="true" CodeBehind="new_view.aspx.cs" Inherits="Tayana.home.NewView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--遮罩開始--%>
    <div class="bannermasks">
        <img src="images/banner02_masks.png" alt="" />
    </div>
    <%--遮罩結束--%>
    <%--換圖開始--%>
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" /></li>
        </ul>
    </div>
    <%--換圖結束--%>
    <div class="conbg">
        <%--左邊選單開始--%>
        <div class="left">
            <div class="left1">
                <p><span>NEWS</span></p>
                <ul>
                    <li><a href="#">News & Events</a></li>
                </ul>
            </div>
        </div>
        <%--左邊選單結束--%>
        <%--右邊選單開始--%>
        <div id="crumb">
            <a href="index.aspx">Home</a> >>
            <a href="#">News</a> >>
            <a href="#"><span class="on1">News & Events</span>
            </a>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>News & Events</span></div>
                <%--內容開始--%>
                <div id="view" runat="server"></div>
                <div class="buttom001">
                    <a href="javascript:window.history.back();">
                        <img src="images/back.gif" width="55" height="28" /></a>
                </div>
                <%--內容結束--%>
            </div>
        </div>
        <%--右邊選單結--%>
    </div>
</asp:Content>