<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" ValidateRequest="False" Inherits="Tayana.backend.yachts.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/ckeditor/ckeditor.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5 id="yachName" runat="server"></h5>
                </div>
                <div class="card-body">
                    <div id="smartwizard" class="sw-main sw-theme-default">
                        <ul class="nav nav-tabs step-anchor">
                            <li class="nav-item">
                                <asp:HyperLink CssClass="nav-link" runat="server">
                                    <h6>Step 1</h6>
                                    <p class="m-0">船型基本資料</p>
                                </asp:HyperLink>
                            </li>
                            <li class="nav-item active">
                                <asp:HyperLink CssClass="nav-link" runat="server" OnClick="Next_Click">
                                    <h6>Step 2</h6>
                                    <p class="m-0">相片及檔案</p>
                                </asp:HyperLink>
                            </li>
                        </ul>
                        <div class="sw-container tab-content">
                            <div id="step-1" class="tab-pane step-content" style="display: block">
                                <h5>船名</h5>
                                <input id="inputYachName" runat="server" type="text" required="required" class="form-control" placeholder="船名" />
                                <hr />
                                <h5>形象圖</h5>
                                <asp:Image ID="yachtImg" onerror="this.src='/errorImg/sm-yacht.jpg'" runat="server" />
                                <br />
                                <asp:Label ID="yachtImgName" runat="server" Visible="false"></asp:Label>
                                <asp:FileUpload ID="yachtImgFile" runat="server" />
                                <hr>

                                <div class="switch d-inline m-r-10">
                                    <input runat="server" type="checkbox" id="isNew" />
                                    <asp:Label CssClass="cr" AssociatedControlID="isNew" runat="server" Text=""></asp:Label>
                                    最新型號
                                </div>
                                <hr>

                                <h5>OverView</h5>
                                <asp:TextBox ID="txtOverView" runat="server" CssClass="ckeditor" TextMode="MultiLine"></asp:TextBox>
                                <hr>

                                <h5>Specification</h5>
                                <asp:TextBox ID="txtspecification" runat="server" CssClass="ckeditor" TextMode="MultiLine"></asp:TextBox>
                                <hr>

                                <h5>Layout</h5>
                                <asp:TextBox ID="txtLayout" runat="server" CssClass="ckeditor" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="btn-toolbar sw-toolbar sw-toolbar-bottom mt-3">
                        <div class="btn-group mr-2" role="group">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="直接儲存" OnClick="Save_Click" />
                            <asp:Button ID="btnNext" CssClass="btn btn-secondary" runat="server" Text="儲存並下一步" OnClick="Next_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>