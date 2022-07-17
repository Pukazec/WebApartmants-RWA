<%@ Page Title="Log in" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WebApartmants.LogIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container py-4">
        <!-- PANEL PORUKA -->
        <asp:Panel ID="PanelPrint" CssClass="container mt-5" runat="server" Visible="False">
            <div class='alert alert-danger' role='alert'>
                <asp:Label ID="lbErrorLogin" meta:resourcekey="lblErrorLogin" runat="server" Text="Check the entered data again!"></asp:Label>
            </div>
        </asp:Panel>
        <!-- // -->

        <asp:Panel ID="PanelForm" runat="server" Visible="True">
            <!-- FORM -->
            <fieldset class="p-4">
                <legend runat="server" meta:resourcekey="legendLogin">Login</legend>
                <div class="mb-3">
                    <asp:Label ID="lblUsername" meta:resourcekey="lblUsername" class="form-label" runat="server" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:Label ID="lblPassword" meta:resourcekey="lblPassword" class="form-label" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" meta:resourcekey="btnLogin" class="btn btn-primary" runat="server" Text="Log in" OnClick="BtnLogin_Click"/>
            </fieldset>
            <!-- // -->
        </asp:Panel>
    </div>
</asp:Content>