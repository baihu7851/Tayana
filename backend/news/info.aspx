<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" ValidateRequest="False" Inherits="Tayana.backend.news.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5 id="newsName" runat="server"></h5>
                </div>
                <div class="card-body">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="title" Text="標題"></asp:Label>
                        <input runat="server" required="required" type="text" class="form-control" id="title" placeholder="標題" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="subTitle" Text="副標題"></asp:Label>
                        <input runat="server" required="required" type="text" class="form-control" id="subTitle" placeholder="副標題" />
                    </div>
                    <div class="form-group">
                        <asp:Image ID="newsImg" runat="server" />
                        <br />
                        <asp:Label ID="newsImgName" runat="server" Visible="false"></asp:Label>
                        <asp:FileUpload ID="newsImgFile" runat="server" />
                    </div>
                    <div class="switch d-inline m-r-10">
                        <asp:Label runat="server" AssociatedControlID="isTop" Text="置頂"></asp:Label>
                        <input runat="server" type="checkbox" id="isTop" />
                        <asp:Label runat="server" AssociatedControlID="isTop" CssClass="cr"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="內文"></asp:Label>
                        <asp:TextBox ID="newsText" runat="server" CssClass="ckeditor" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <asp:Button ID="save" CssClass="btn btn-block btn-primary" runat="server" Text="儲存" OnClick="Save_Click" />
        </div>
    </div>
</asp:Content>