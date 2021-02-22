<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="Tayana.backend.news.List" %>

<%@ Register TagPrefix="uc1" TagName="webusercontrol" Src="~/backend/WebUserControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>新聞列表</h5>
                    <asp:HyperLink ID="newNews" CssClass="btn btn-sm btn-rounded btn-primary px-2 py-1" NavigateUrl="~/backend/news/info.aspx?id=0" Text="新增新聞" runat="server"></asp:HyperLink>
                </div>
                <div class="card-block">
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>編號</th>
                                            <th>主標題</th>
                                            <th>標題圖片</th>
                                            <th>置頂設定</th>
                                            <th>刪除</th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" Text='<%#Eval("編號") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:HyperLink CssClass="mb-1" NavigateUrl='<%#"~/backend/news/info.aspx?id="+Eval("Id") %>' Text='<%# Eval("標題") %>' runat="server">
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Image Width="80" runat="server" ImageUrl='<%#"~/upload/images/sm/sm-" +Eval("圖片") %>' />
                                            </td>
                                            <td>
                                                <div class="switch d-inline m-r-10">
                                                    <asp:CheckBox ID="isTop" Checked='<%#Eval("置頂") %>' runat="server" />
                                                    <asp:Label CssClass="cr" AssociatedControlID="isTop" runat="server" />
                                                </div>
                                                <asp:Literal ID="Id" Text='<%#Eval("Id") %>' Visible="False" runat="server"></asp:Literal>
                                            </td>
                                            <td>
                                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-rounded btn-outline-danger py-1" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) return false;">
                                                    刪除
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <uc1:webusercontrol runat="server" ID="WebUserControl" />
                        </table>
                        <asp:Button ID="save" CssClass="btn btn-primary btn-block" runat="server" Text="儲存" OnClick="Save_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>