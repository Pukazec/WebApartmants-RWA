<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="TagsUpdate.aspx.cs" Inherits="WebApartmants.TagsUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Add tags</h5>
                        <asp:Button OnClick="btnTagCancel_Click" class="btn-close" runat="server" />
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:Label ID="lblTags" runat="server" class="form-label" Text="Tags"></asp:Label>
                            <asp:DropDownList ID="ddlTagsToAdd" runat="server" OnSelectedIndexChanged="ddlTagsToAdd_SelectedIndexChanged" AutoPostBack="true" />
                            <asp:Label ID="lblError" runat="server" CssClass="form-label" ></asp:Label>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblAddedTags" runat="server" class="form-label" Text="Added tags:"></asp:Label>
                            <asp:Panel ID="pnlTags" runat="server" class="container"></asp:Panel>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnTagCancel" runat="server" class="btn btn-outline-secondary" Text="Back" OnClick="btnTagCancel_Click" />
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
