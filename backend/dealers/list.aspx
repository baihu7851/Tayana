<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="list.aspx.cs" Inherits="Tayana.backend.dealers.List" %>

<%@ Register TagPrefix="uc1" TagName="webusercontrol" Src="~/backend/WebUserControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>代理商列表</h5>
                    <asp:HyperLink ID="newNews" CssClass="btn btn-sm btn-rounded btn-primary px-2 py-1" NavigateUrl="~/backend/dealers/info.aspx?id=0" Text="新增代理商" runat="server"></asp:HyperLink>
                </div>
                <div class="card-block">
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>編號</th>
                                            <th>代理商名稱</th>
                                            <th>頭像</th>
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
                                                <asp:HyperLink CssClass="mb-1" NavigateUrl='<%#"~/backend/dealers/info.aspx?id="+Eval("Id") %>' Text='<%#Eval("名稱") %>' runat="server"></asp:HyperLink>
                                            </td>
                                            <td>
                                                <asp:Image runat="server" onerror="this.src='../defaultdata/avatar-2.jpg'" ImageUrl='<%#"~/upload/images/sm/sm-" +Eval("圖片") %>' />
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>