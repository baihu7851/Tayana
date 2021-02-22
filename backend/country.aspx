<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="country.aspx.cs" Inherits="Tayana.backend.Country" %>

<%@ Register TagPrefix="uc1" TagName="webusercontrol" Src="~/backend/WebUserControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>國家列表</h5>
                </div>
                <div class="card-block">
                    <h5>新增國家</h5>
                    <div class="input-group mb-3">
                        <asp:TextBox ID="countryName" CssClass="form-control" placeholder="輸入國家" required="required" runat="server"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="btnNew" CssClass="btn btn-primary" runat="server" Text="新增" OnClick="NewCountry_Click" />
                        </div>
                    </div>
                    <hr>
                    <h5>現有國家</h5>
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>編號</th>
                                            <th>國家名稱</th>
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
                                                <asp:Label ID="txtName" runat="server" Text='<%#Eval("國名") %>'></asp:Label>
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