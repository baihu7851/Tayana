<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" ValidateRequest="False" Inherits="Tayana.backend.dealers.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5 id="dealerName" runat="server"></h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="inputName" Text="名稱"></asp:Label>
                                <input runat="server" required="required" type="text" class="form-control" id="inputName" placeholder="名稱" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>頭像</label>
                                <div>
                                    <asp:Image ID="dealerImg" runat="server" /><br />
                                    <asp:Label ID="dealerImgName" runat="server" Visible="false"></asp:Label>
                                    <asp:FileUpload ID="dealerImgFile" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>篩選國家</label>
                                <asp:DropDownList ID="countryList" CssClass="form-control" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="CountryList_SelectedIndexChanged">
                                    <asp:ListItem Value="0">全部國家</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>選擇地區</label>
                                <asp:DropDownList ID="areaList" CssClass="form-control" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="AreaList_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Enabled="false">選擇地區</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-group">
                                <label>代理商資料</label><br />
                                <asp:TextBox ID="information" CssClass="ckeditor" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="save" CssClass="btn btn-block btn-primary" runat="server" Text="儲存" OnClick="Save_Click" />
        </div>
    </div>
</asp:Content>