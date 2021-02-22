<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="Tayana.backend.yachts.List" %>

<%@ Register TagPrefix="uc1" TagName="WebUserControl" Src="~/backend/WebUserControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>船型列表</h5>
                    <asp:HyperLink ID="newUser" CssClass="btn btn-sm btn-rounded btn-primary px-2 py-1" NavigateUrl="~/backend/yachts/info.aspx?id=0" Text="新增船型" runat="server"></asp:HyperLink>
                </div>
                <div class="card-block">
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>編號</th>
                                            <th>船名</th>
                                            <th>圖片</th>
                                            <th>新船</th>
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
                                                <asp:HyperLink CssClass="mb-1" NavigateUrl='<%#"~/backend/yachts/info.aspx?id="+Eval("Id") %>' Text='<%# Eval("船名") +" "+ Eval("船號") %>' runat="server">
                                                </asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Image runat="server" onerror="this.src='/errorImg/sm-yacht.jpg'" ImageUrl='<%#"~/upload/images/sm/sm-" +Eval("圖片") %>' />
                                            </td>
                                            <td>
                                                <div class="switch d-inline m-r-10">
                                                    <asp:CheckBox ID="isNew" Checked='<%#Eval("新船") %>' runat="server" />
                                                    <asp:Label CssClass="cr" AssociatedControlID="isNew" runat="server" />
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Literal ID="Id" Text='<%#Eval("Id") %>' Visible="False" runat="server"></asp:Literal>
                                                <asp:LinkButton runat="server" CssClass="btn btn-sm btn-rounded btn-outline-danger py-1" CommandName="Del" CommandArgument='<%#Eval("Id") %>' OnClientClick="javascript:if(!window.confirm('你確定要刪除嗎?')) return false;">
                                                    刪除
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </tbody>
                                </ItemTemplate>
                            </asp:Repeater>
                            <uc1:WebUserControl runat="server" ID="WebUserControl" />
                        </table>
                        <asp:Button ID="save" CssClass="btn btn-primary btn-block" runat="server" Text="儲存" OnClick="Save_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>