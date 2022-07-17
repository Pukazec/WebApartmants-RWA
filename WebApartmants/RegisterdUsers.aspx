<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="RegisterdUsers.aspx.cs" Inherits="WebApartmants.RegisterdUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-------------------------------------------------------------------------------- Users table -------------------------------------------------------------------------------->
    <div class="container">
        <div class="col-lg">
            <asp:Repeater ID="rptUsers" runat="server">
                <HeaderTemplate>
                    <table id="tblUsers" class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Name</th>
                                <th scope="col">Address</th>
                                <th scope="col">Phone number</th>
                                <th scope="col">Email</th>
                            </tr>
                        </thead>
                        <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.User.Id)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.User.UserName)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.User.Address)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.User.PhoneNumber)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.User.Email)) %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
        </div>
        <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
    </div>
</asp:Content>
