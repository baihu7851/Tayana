<%@ Page Title="" Language="C#" MasterPageFile="~/backend/Site.Master" AutoEventWireup="true" CodeBehind="info.aspx.cs" Inherits="Tayana.backend.users.Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header">
                    <h5 id="userName" runat="server"></h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="InputEmail" Text="信箱"></asp:Label>
                                <input runat="server" required="required" type="email" class="form-control" id="InputEmail" placeholder="信箱" />
                            </div>
                        </div>
                        <div id="Div1" runat="server" class="col-md-6" visible="false">
                            <div class="form-group">
                                <label>密碼</label>
                                <input class="form-control" disabled="disabled" value="********">
                            </div>
                        </div>
                        <div id="password" runat="server" class="col-md-6" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="InputPassword" Text="密碼"></asp:Label>
                                <input id="InputPassword" runat="server" type="password" required="required" minlength="6" maxlength="10" class="form-control" placeholder="密碼" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="InputName" Text="名稱"></asp:Label>
                                <input id="InputName" runat="server" type="text" required="required" class="form-control" placeholder="名稱" />
                            </div>
                        </div>
                        <div id="confirmation" runat="server" class="col-md-6" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="txtConfirmation" Text="確認密碼"></asp:Label>
                                <input id="txtConfirmation" runat="server" type="password" required="required" minlength="6" maxlength="10" equalto="input['#password']" class="form-control" placeholder="再次輸入密碼" />
                            </div>
                        </div>
                        <div id="Div2" runat="server" class="col-md-6" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" AssociatedControlID="changePassword" Text="更改密碼"></asp:Label>
                                <asp:LinkButton ID="changePassword" CssClass="btn btn-primary btn-block" OnClick="ChangePassword_Click" runat="server">更改密碼</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <div class="form-group">
                                <div class="switch d-inline m-r-10">
                                    <input type="checkbox" id="chkAll" checked="">
                                    <label for="chkAll" class="cr"></label>
                                </div>
                                <label>全選</label><br />
                                <ul>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="userPage" checked="" value="1" />
                                            <asp:Label runat="server" AssociatedControlID="userPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>使用者權限</label>
                                    </li>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="yachtsPage" checked="" value="2" />
                                            <asp:Label runat="server" AssociatedControlID="yachtsPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>遊艇權限</label><br />
                                    </li>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="newsPage" checked="" value="4" />
                                            <asp:Label runat="server" AssociatedControlID="newsPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>新聞權限</label><br />
                                    </li>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="dealersPage" checked="" value="8" />
                                            <asp:Label runat="server" AssociatedControlID="dealersPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>代理商權限</label><br />
                                    </li>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="countryPage" checked="" value="16" />
                                            <asp:Label runat="server" AssociatedControlID="countryPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>國家權限</label><br />
                                    </li>
                                    <li>
                                        <div class="switch d-inline m-r-10">
                                            <input runat="server" type="checkbox" id="areaPage" checked="" value="32" />
                                            <asp:Label runat="server" AssociatedControlID="areaPage" CssClass="cr"></asp:Label>
                                        </div>
                                        <label>地區權限</label><br />
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>頭像</label>
                                <div>
                                    <asp:Image ID="userImg" onerror="this.src='/errorImg/user.jpg'" runat="server" /><br />
                                    <asp:Label ID="userImgName" runat="server" Visible="false"></asp:Label>
                                    <asp:FileUpload ID="userImgFile" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="save" CssClass="btn btn-block btn-primary" runat="server" Text="儲存" OnClick="Save_Click" />
                </div>
            </div>
        </div>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        $('#chkAll').click(function () {
            $('li div input[type="checkbox"]').prop('checked', this.checked);
        });
        $('li div input[type="checkbox"]').change(function () {
            if ($('li div input[type="checkbox"]').length === $('li div input[type="checkbox"]:checked').length) {
                $('#chkAll').prop('checked', true);
            } else {
                $('#chkAll').prop('checked', false);
            }
        });
    </script>
</asp:Content>