<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tags.aspx.cs" Inherits="WebApartmants.Tags" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-------------------------------------------------------------------------------- Tags table -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlTagsList" runat="server">
        <asp:Label ID="lblResult" runat="server" CssClass="alert alert-danger d-block w-100" Visible="false"></asp:Label>
        <div class="container">
            <div class="row ">
                <div class="col-md">
                    <asp:Repeater ID="rptTags" runat="server">
                        <HeaderTemplate>
                            <table id="tblTags" class="table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">Tagged appartmants</th>
                                        <th scope="col"></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval(nameof(ApartmantsLibrary.Model.Tag.Id)) %></th>
                                <td><%# Eval(nameof(ApartmantsLibrary.Model.Tag.Name)) %></td>
                                <td><%# Eval(nameof(ApartmantsLibrary.Model.Tag.TaggedApartmants)) %></td>
                                <td>
                                    <asp:Button ID="btnDelete"
                                        CommandArgument="<%#Eval(nameof(ApartmantsLibrary.Model.Tag.Id)) %>"
                                        OnClick="btnDelete_Click" OnDataBinding="btnDelete_DataBinding"
                                        Text="Delete" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                    </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row gy-2 gx-3 align-items-center">
                <div class="col-auto">
                    <asp:Button ID="btnAddTag" runat="server" class="btn btn-primary" Text="Add new tag" OnClick="btnAddTag_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Add tag -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlAddPanel" runat="server" Visible="false">
        <fieldset class="container p-4">
            <legend runat="server" meta:resourcekey="pnlAddTag">New tag</legend>
            <div class="mb-3">
                <asp:Label ID="lblName" for="txtName" class="form-label" meta:resourcekey="lblName" runat="server" Text="Tag name:"></asp:Label>
                <asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="NameRFV"
                    runat="server"
                    meta:resourcekey="rfvName"
                    ControlToValidate="txtName"
                    Display="Dynamic"
                    ForeColor="Red">* Name missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="NameExistsValidator"
                    runat="server"
                    ControlToValidate="txtName"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckNValidation"
                    OnServerValidate="NameExistsValidator_ServerValidate">* Tag already exists
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lbl" for="txtNameEng" class="form-label" meta:resourcekey="lblNameEng" runat="server" Text="English tag name:"></asp:Label>
                <asp:TextBox ID="txtNameEng" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="NameEngRFV"
                    runat="server"
                    meta:resourcekey="rfvNameEng"
                    ControlToValidate="txtNameEng"
                    Display="Dynamic"
                    ForeColor="Red">* English name missing!
                </asp:RequiredFieldValidator>

            </div>
            <div class="mb-3">
                <asp:Label ID="lblTypeName" for="DdlTypeName" class="form-label" meta:resourcekey="lblDdlTypeName" runat="server" Text="Tag type"></asp:Label>
                <asp:DropDownList ID="ddlTagType" class="form-select" runat="server" />
            </div>

            <div class="mb-3">
                <asp:Button ID="btnSend" runat="server" class="btn btn-success" meta:resourcekey="btnSend" Text="Add" OnClick="btnSend_Click" />
                <asp:Button ID="btnCancel" runat="server" class="btn btn-danger" meta:resourcekey="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </fieldset>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Delete tag -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlDelete" runat="server" Visible="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Delete tag</h5>
                        <asp:Button OnClick="btnDeleteCancel_Click" class="btn-close" runat="server" />
                    </div>
                    <div class="modal-body">
                        Are your shure you want to delete this tag ?
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDeleteCancel" runat="server" class="btn btn-secondary" meta:resourcekey="btnDeleteCancel" Text="Cancel" OnClick="btnDeleteCancel_Click" />
                        <asp:Button ID="btnDeleteConfirm" runat="server" class="btn btn-danger" meta:resourcekey="btnDeleteSend" Text="Delete" OnClick="btnDeleteConfirm_Click" />
                        <asp:HiddenField ID="hfTagToDeleteId" runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
</asp:Content>
