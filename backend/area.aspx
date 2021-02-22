<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="area.aspx.cs" Inherits="Tayana.backend.Area" %>

<%@ Register TagPrefix="uc1" TagName="webusercontrol" Src="~/backend/WebUserControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5>地區列表</h5>
                </div>
                <div class="card-block">
                    <div class="row">
                        <div class="col-md-6">
                            <h5>篩選地區</h5>
                            <asp:DropDownList ID="countryList" CssClass="form-control" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="CountryList_SelectedIndexChanged">
                                <asp:ListItem Value="0">全部國家</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <h5>新增地區</h5>
                            <div class="input-group mb-3">
                                <asp:TextBox ID="areaName" CssClass="form-control" placeholder="輸入地區" required="required" runat="server"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:Button CssClass="btn btn-primary" runat="server" Text="新增" OnClick="NewArea_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr>
                    <h5>現有地區</h5>
                    <div class="table-responsive">
                        <table class="table">
                            <asp:Repeater ID="Repeater" runat="server" OnItemCommand="Repeater_ItemCommand">
                                <HeaderTemplate>
                                    <thead>
                                        <tr>
                                            <th>編號</th>
                                            <th>地區名稱</th>
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
                                                <asp:Label ID="txtName" runat="server" Text='<%#Eval("地區名") %>'></asp:Label>
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