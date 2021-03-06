<%@ Page Title="" Language="C#" MasterPageFile="~/home/Site.Master" AutoEventWireup="true" CodeBehind="new_list.aspx.cs" Inherits="Tayana.home.NewList" %>

<%@ Register TagPrefix="uc1" TagName="webusercontrol" Src="~/backend/WebUserControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/pagination.css" rel="stylesheet" type="text/css" />
    <link href="css/page.css" rel="stylesheet" type="text/css" />
    <%--<style>
        .box2_list > ul {
            display: flex;
            flex-direction: column;
        }
        .True {
            order: -1;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--遮罩開始--%>
    <div class="bannermasks">
        <img src="images/banner02_masks.png" />
    </div>
    <%--遮罩結束--%>
    <%--換圖開始--%>
    <div class="banner">
        <ul>
            <li>
                <img src="images/newbanner.jpg" alt="Tayana Yachts" />
            </li>
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
                <div class="box2_list">
                    <asp:Repeater ID="Repeater" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class='<%#"list01 "+Eval("置頂") %>' runat="server">
                                <ul>
                                    <li>
                                        <div>
                                            <p>
                                                <asp:Image ID="imgNewsImges" runat="server" ImageUrl='<%#"~/upload/images/sm/sm-"+Eval("圖片") %>' />
                                            </p>
                                        </div>
                                    </li>
                                    <li>
                                        <asp:Label ID="txtNewDate" runat="server" Text='<%#Eval("日期", "{0:d}") %>'></asp:Label><br />
                                        <asp:HyperLink ID="hlNewsH1" runat="server" Text='<%#Eval("標題") %>' NavigateUrl='<%#"new_view.aspx?id="+Eval("Id") %>'></asp:HyperLink><br />
                                    </li>
                                    <li>
                                        <asp:Literal ID="txtNewsH2" runat="server" Text='<%#Eval("副標題") %>'></asp:Literal>
                                    </li>
                                </ul>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    <uc1:webusercontrol runat="server" ID="WebUserControl" />
                </div>
                <div></div>
                <%--內容結束--%>
            </div>
        </div>
        <%--右邊選單結--%>
    </div>
</asp:Content>