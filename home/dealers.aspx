<%@ Page Title="" Language="C#" MasterPageFile="~/home/Site.Master" AutoEventWireup="true" CodeBehind="dealers.aspx.cs" Inherits="Tayana.home.Dealers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--遮罩開始--%>
    <div class="bannermasks">
        <img src="images/DEALERS.jpg" width="967" height="371" />
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
                <p><span>DEALERS</span></p>
                <ul id="ulDealers" runat="server">
                </ul>
            </div>
        </div>
        <%--左邊選單結束--%>
        <%--右邊選單開始--%>
        <div id="crumb">
            <a href="index.aspx">Home</a> >>
            <a href="#">Dealers</a> >>
            <a href="#"><span id="dealersCrumb" class="on1" runat="server"></span>
            </a>
        </div>
        <div class="right">
            <div class="right1">
                <div class="title"><span id="dealersRigh1" runat="server"></span></div>
                <%--內容開始--%>
                <div class="box2_list">
                    <asp:Repeater ID="dealersRepeater" runat="server">
                        <HeaderTemplate>
                            <ul>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="list02">
                                    <ul>
                                        <li class="list02li">
                                            <div>
                                                <p>
                                                    <asp:Image runat="server" ImageUrl='<%#"~/upload/images/sm/sm-"+Eval("圖片") %>' BorderWidth="0" />
                                                </p>
                                            </div>
                                        </li>
                                        <li class="list02li02">
                                            <asp:Label runat="server" Text='<%#Eval("名稱") %>'></asp:Label><br />
                                            <asp:Literal runat="server" Text='<%#Eval("資訊") %>'></asp:Literal><br />
                                        </li>
                                    </ul>
                                </div>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div class="pagenumber">
                    </div>
                </div>
                <%--內容結束--%>
            </div>
        </div>
        <%--右邊選單結--%>
    </div>
</asp:Content>