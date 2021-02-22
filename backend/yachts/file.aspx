<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="file.aspx.cs" Inherits="Tayana.backend.yachts.File" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                                <asp:HyperLink CssClass="nav-link" runat="server" OnClick="Prev_Click">
                                    <h6>Step 1</h6>
                                    <p class="m-0">船型基本資料</p>
                                </asp:HyperLink>
                            </li>
                            <li class="nav-item active">
                                <asp:HyperLink CssClass="nav-link" runat="server">
                                    <h6>Step 2</h6>
                                    <p class="m-0">相片及檔案</p>
                                </asp:HyperLink>
                            </li>
                        </ul>
                        <div class="sw-container tab-content">
                            <div id="step2" class="tab-pane step-content" style="display: block">
                                <h5>相片集</h5>
                                <asp:FileUpload ID="interiors" runat="server" AllowMultiple="True" />
                                <hr>
                                <h5>其他檔案</h5>
                                <asp:FileUpload ID="filed" runat="server" AllowMultiple="True" />
                                <hr>
                            </div>
                        </div>
                    </div>
                    <div class="btn-toolbar sw-toolbar sw-toolbar-bottom mt-3">
                        <div class="btn-group mr-2" role="group">
                            <asp:Button ID="btnPrev" CssClass="btn btn-secondary" runat="server" Text="儲存返回上一步" OnClick="Prev_Click" />
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="儲存" OnClick="Save_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>