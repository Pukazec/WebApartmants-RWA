<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="WebApartmants.Registration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <!-- FORM -->
        <asp:Panel ID="PanelForm" runat="server" Visible="False">
            <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100" Visible="false"></asp:Label>
            <fieldset class="p-4">
                <legend runat="server" meta:resourcekey="legendRegistration">Registration</legend>
                <div class="mb-3">
                    <asp:Label ID="lblUsername" for="txtUsername" class="form-label" meta:resourcekey="lblUsername" runat="server" Text="User name"></asp:Label>
                    <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1"
                        runat="server" 
                        meta:resourcekey="rfvUsername" 
                        ControlToValidate="txtUsername" 
                        Display="Dynamic" 
                        ForeColor="Red">* Username missing!
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator 
                        ID="ForbidenValidator" 
                        ClientValidationFunction="UsernameValidation"
                        runat="server"
                        ControlToValidate="txtUsername" 
                        Display="Dynamic"
                        ForeColor="Red"
                        meta:resourcekey="rfvBannedUsername" 
                        OnServerValidate="ForbidenValidator_ServerValidate">* Forbiden username
                    </asp:CustomValidator>
                    <asp:CustomValidator 
                        ID="UsernamenameExistsValidator"
                        runat="server"
                        ControlToValidate="txtUsername" 
                        Display="Dynamic"
                        ForeColor="Red" 
                        ClientValidationFunction="CheckUserValidation"
                        OnServerValidate="UsernamenameExistsValidator_ServerValidate">* User with the same urername already exists
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblAddress" for="txtAddress" class="form-label" meta:resourcekey="lbltxtAddress" runat="server" Text="Address"></asp:Label>
                    <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2"
                        runat="server" 
                        meta:resourcekey="rfvAddress" 
                        ControlToValidate="txtAddress" 
                        Display="Dynamic" 
                        ForeColor="Red">* Address missing!
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblEmail" for="txtEmail" class="form-label" meta:resourcekey="lbltxtEmail" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" type="email" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator3"
                        runat="server" 
                        meta:resourcekey="rfvEmail" 
                        ControlToValidate="txtEmail" 
                        Display="Dynamic" 
                        ForeColor="Red">* Email missing!
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator 
                        ID="EmailExistsValidator"
                        runat="server"
                        ControlToValidate="txtEmail" 
                        Display="Dynamic"
                        ForeColor="Red" 
                        ClientValidationFunction="CheckEmailValidation"
                        OnServerValidate="EmailExistsValidator_ServerValidate">* User with the same email already exists
                    </asp:CustomValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPhone" for="txtPhone" class="form-label" meta:resourcekey="lbltxtPhone" runat="server" Text="Phone"></asp:Label>
                    <asp:TextBox ID="txtPhone" TextMode="Phone" type="tel" class="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator4"
                        runat="server" 
                        meta:resourcekey="rfvPhone" 
                        ControlToValidate="txtPhone" 
                        Display="Dynamic" 
                        ForeColor="Red">* Phone missing!
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPassword" for="txtPassword" class="form-label" meta:resourcekey="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator5"
                        runat="server" 
                        meta:resourcekey="rfvPassword" 
                        ControlToValidate="txtPassword" 
                        Display="Dynamic" 
                        ForeColor="Red">* Password missing!
                    </asp:RequiredFieldValidator>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblConfirmedPassword" for="txtConfirmedPassword" class="form-label" meta:resourcekey="lblConfirmedPassword" runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox ID="txtConfirmedPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator6"
                        runat="server" 
                        meta:resourcekey="rfvConfirmedPassword" 
                        ControlToValidate="txtConfirmedPassword" 
                        Display="Dynamic" 
                        ForeColor="Red">* Password confirmation missing!
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator 
                        ID="CompareValidator" 
                        runat="server"
                        ControlToCompare="txtPassword" 
                        ControlToValidate="txtConfirmedPassword"
                        meta:resourcekey="rfvComparePassword"
                        Display="Dynamic"
                        ForeColor="Red">* Passwords not matching</asp:CompareValidator>
                    </div>
                <div class="mb-3">
                <asp:Button ID="btnSend" runat="server" class="btn btn-warning" meta:resourcekey="btnSend" Text="Register" OnClick="btnSend_Click"/>
                </div>

            </fieldset>
        </asp:Panel>
        <!-- // -->

        <!-- PANEL PORUKA -->
        <asp:Panel ID="PanelPrint" CssClass="container mt-5" runat="server" Visible="False">
            <div class='alert alert-success' role='alert'>
                <asp:Label ID="lblSuccessLogin" meta:resourcekey="lblSuccessLogin" runat="server" Text="Registration successful."></asp:Label>
            </div>
        </asp:Panel>
        <!-- // -->
        
    </div>
</asp:Content>
