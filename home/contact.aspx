<%@ Page Title="" Language="C#" MasterPageFile="~/home/Site.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="Tayana.home.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--遮罩開始--%>
    <div class="bannermasks">
        <img src="images/contact.jpg" alt="" width="967" height="371" />
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
                <p><span>CONTACT</span></p>
                <ul>
                    <li><a href="#">contacts</a></li>
                </ul>
            </div>
        </div>
        <%--左邊選單結束--%>
        <%--右邊選單開始--%>
        <div id="crumb"><a href="index.aspx">Home</a> >> <a href="#"><span class="on1">Contact</span></a></div>
        <div class="right">
            <div class="right1">
                <div class="title"><span>Contact</span></div>
                <%--內容開始--%>
                <%--表單--%>
                <div class="from01">
                    <p>Please Enter your contact information<span class="span01">*Required</span></p>
                    <br />
                    <table id="table" runat="server">
                        <tr>
                            <td class="from01td01">Name :</td>
                            <td><span>*</span>
                                <asp:TextBox ID="Name" runat="server" required="required" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Email :</td>
                            <td><span>*</span>
                                <asp:TextBox ID="Email" TextMode="Email" runat="server" required="required" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Phone :</td>
                            <td><span>*</span>
                                <asp:TextBox ID="Phone" runat="server" required="required" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Country :</td>
                            <td><span>*</span>
                                <asp:DropDownList ID="Country" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"><span>*</span>Brochure of interest *Which Brochure would you like to view?</td>
                        </tr>
                        <tr>
                            <td class="from01td01"></td>
                            <td>
                                <asp:DropDownList ID="Yachts" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01">Comments:</td>
                            <td>
                                <asp:TextBox ID="Comments" runat="server" TextMode="MultiLine" Rows="2" Columns="20" Width="330" Height="150"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="from01td01"></td>
                            <td class="f_right">
                                <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="images/buttom03.gif" BorderWidth="0" OnClick="ImageButton_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <%--表單--%>
                <div class="box1">
                    <span class="span02">Contact with us</span><br />
                    Thanks for your enjoying our web site as an introduction to the Tayana world and our range of yachts.
                    As all the designs in our range are semi-custom built, we are glad to offer a personal service to all our
                    potential customers.
                    If you have any questions about our yachts or would like to take your interest a stage further, please
                    feel free to contact us.
                </div>
                <div class="list03">
                    <p>
                        <span>TAYANA HEAD OFFICE</span><br />
                        NO.60 Haichien Rd. Chungmen Village Linyuan Kaohsiung Hsien 832 Taiwan R.O.C<br />
                        tel. +886(7)641 2422<br />
                        fax. +886(7)642 3193<br />
                    </p>
                </div>
                <div class="box4">
                    <h4>Location</h4>
                    <p>
                        <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d14733.37422493212!2d120.3089536!3d22.603642450000002!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1szh-TW!2stw!4v1605089595472!5m2!1szh-TW!2stw" width="695" height="518" style="border: 0;" aria-hidden="false" tabindex="0"></iframe>
                    </p>
                </div>
                <%--內容結束--%>
            </div>
        </div>
        <%--右邊選單結束--%>
    </div>
</asp:Content>