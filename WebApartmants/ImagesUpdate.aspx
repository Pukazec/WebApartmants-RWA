<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ImagesUpdate.aspx.cs" Inherits="WebApartmants.ImagesUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlUploadImage" runat="server">
        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Add pictures</h5>
                    <asp:Button OnClick="btnCancel_Click" class="btn-close" runat="server" />
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Panel ID="pnlImages" runat="server" CssClass="container"></asp:Panel>
                    </div>
                    <div class="mb-3">
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAddNewImage" runat="server" class="btn btn-outline-primary" Text="Add new image" OnClick="btnAddNewImage_Click" />
                    <asp:Button runat="server" class="btn btn-outline-secondary" Text="Back" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Edit image -------------------------------------------------------------------------------->

    <asp:Panel ID="pnlEditImage" runat="server" Visible="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Edit image</h5>
                        <asp:Button OnClick="btnBack_Click" class="btn-close" runat="server" CausesValidation="false" />
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:ImageButton ID="imgShown" runat="server" Enabled="false" />
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblImageNameUpdate" runat="server" class="form-label" Text="Name:"></asp:Label>
                            <asp:TextBox ID="txtImageNameUpdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                runat="server"
                                ControlToValidate="txtImageNameUpdate"
                                Display="Dynamic"
                                ForeColor="Red">* Name missing!
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSetRepresentative" CssClass="btn btn-outline-info" Text="Set representative" runat="server" OnClick="btnSetRepresentative_Click" CausesValidation="false" />
                        <asp:Button ID="btnUpdateImage" CssClass="btn btn-outline-success" Text="Save" runat="server" OnClick="btnUpdateImage_Click" />
                        <asp:Button ID="btnBack" runat="server" class="btn btn-outline-secondary" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Add image -------------------------------------------------------------------------------->


    <asp:Panel ID="pnlAddImage" runat="server" Visible="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">New image</h5>
                        <asp:Button OnClick="btnBack_Click" class="btn-close" runat="server" CausesValidation="false" />
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:Label ID="lblImageName" runat="server" class="form-label" Text="Name:"></asp:Label>
                            <asp:TextBox ID="txtImageName" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                runat="server"
                                ControlToValidate="txtImageName"
                                Display="Dynamic"
                                ForeColor="Red">* Name missing!
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblUploadImg" runat="server" class="form-label" Text="Upload image:"></asp:Label>
                            <asp:FileUpload ID="fuImage" CssClass="btn" runat="server" />
                            <br />
                            <asp:CustomValidator
                                ID="validationFuImage"
                                runat="server"
                                ControlToValidate="fuImage"
                                Display="Dynamic"
                                ForeColor="Red"
                                OnServerValidate="validationFuImage_ServerValidate">* File must be image!
                            </asp:CustomValidator>
                        </div>
                        <div class="mb-3">
                            <asp:Label ID="lblError" runat="server" class="form-label" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddImage" CssClass="btn btn-outline-success" Text="Add image" runat="server" OnClick="btnAddImage_Click" />
                        <asp:Button runat="server" class="btn btn-outline-secondary" Text="Back" OnClick="btnBack_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
