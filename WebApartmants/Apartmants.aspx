<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Apartmants.aspx.cs" Inherits="WebApartmants.Apartmants" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-------------------------------------------------------------------------------- Apartmants table -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlApartmants" runat="server">
        <div class="container">
            <div class="row gy-2 gx-3 mb-4 align-items-center">
                <div class="col-auto">
                    <asp:Label ID="lblStatus" for="ddlStatus" class="form-label" meta:resourcekey="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
                <div class="col-auto">
                    <asp:Label ID="lblCity" for="ddlCity" class="form-label" meta:resourcekey="lblCity" runat="server" Text="City"></asp:Label>
                    <asp:DropDownList ID="ddlCity" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md">
                    <asp:Repeater ID="rptApartmants" runat="server">
                        <HeaderTemplate>
                            <table id="tblApartmants" class="table table-striped">
                                <thead>
                                    <tr>
                                        <th scope="col">Name</th>
                                        <th scope="col">City</th>
                                        <th scope="col">City id</th>
                                        <th scope="col">Adults</th>
                                        <th scope="col">Children</th>
                                        <th scope="col">Rooms</th>
                                        <th scope="col">Pictures</th>
                                        <th scope="col">Price</th>
                                        <th scope="col">Updates</th>
                                        <th scope="col">Tags</th>
                                        <th scope="col">Images</th>
                                        <th scope="col">Reservations</th>
                                        <th scope="col">Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.Name)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.CityName)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.CityId)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.MaxAdults)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.MaxChildren)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.TotalRooms)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.NumOfPictures)) %></td>
                                <td><%#Eval(nameof(ApartmantsLibrary.Model.Apartment.Price)) %></td>
                                <td>
                                    <asp:Button
                                        ID="btnOpenApartmant" class="btn btn-outline-success"
                                        CommandArgument="<%# Eval(nameof(ApartmantsLibrary.Model.Apartment.Id)) %>"
                                        runat="server" Text="Update" OnClick="btnOpenApartmant_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdateTags" runat="server"
                                        CommandArgument="<%# Eval(nameof(ApartmantsLibrary.Model.Apartment.Id)) %>"
                                        CssClass="btn btn-outline-info" Text="Tags" OnClick="btnUpdateTags_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdateImg" runat="server"
                                        CommandArgument="<%# Eval(nameof(ApartmantsLibrary.Model.Apartment.Id)) %>"
                                        CssClass="btn btn-outline-primary" Text="Images" OnClick="btnUploadImg_Click" />
                                </td>
                                <td>
                                    <asp:Button
                                        ID="btnUploadReservation" class="btn btn-outline-warning"
                                        CommandArgument="<%# Eval(nameof(ApartmantsLibrary.Model.Apartment.Id)) %>"
                                        runat="server" Text="Reservations" OnClick="btnUploadReservation_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete"
                                        CommandArgument="<%#Eval(nameof(ApartmantsLibrary.Model.Apartment.Id)) %>"
                                        OnClick="btnDelete_Click"
                                        class="btn btn-danger"
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
            <div class="row gy-2 gx-3 mt-4 align-items-center">
                <div class="col-auto">
                    <asp:Button ID="btnAddApartmant" runat="server" class="btn btn-primary" Text="Add new apartmant" OnClick="btnAddApartmant_Click" />
                    <asp:Label ID="lblKliknijame" runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Add apartmant -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlAddApartmant" runat="server" Visible="false">
        <fieldset class="container p-4">
            <legend runat="server" meta:resourcekey="pnlAddApartmant">New apartmant</legend>
            <div class="mb-3">
                <asp:Label ID="lblName" for="txtName" class="form-label" meta:resourcekey="lblName" runat="server" Text="Apartmant name:"></asp:Label>
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
                    ID="ApartmantNameExistsValidator"
                    runat="server"
                    ControlToValidate="txtName"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckNValidation"
                    OnServerValidate="ApartmantNameExistsValidator_ServerValidate">
                        * Name already exists
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblEngName" for="txtNameEng" class="form-label" meta:resourcekey="lblNameEng" runat="server" Text="English name"></asp:Label>
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
                <asp:Label ID="lblAddress" for="txtAddress" class="form-label" meta:resourcekey="lblAddress" runat="server" Text="Address"></asp:Label>
                <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="AdressRFV"
                    runat="server"
                    meta:resourcekey="rfvAddress"
                    ControlToValidate="txtAddress"
                    Display="Dynamic"
                    ForeColor="Red">* Address missing!
                </asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblAddApartmantCity" for="ddlAddApartmantCity" class="form-label" meta:resourcekey="lblDdlCity" runat="server" Text="City"></asp:Label>
                <asp:DropDownList ID="ddlAddApartmantCity" class="form-select" runat="server" />
                <asp:CustomValidator
                    ID="CityNullValidator"
                    runat="server"
                    ControlToValidate="ddlAddApartmantCity"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckCValidation"
                    OnServerValidate="CityNullValidator_ServerValidate">* Select city!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblAddApartmantOwner" for="DdlOwner" class="form-label" meta:resourcekey="lblDdlOwner" runat="server" Text="Owner"></asp:Label>
                <asp:DropDownList ID="ddlOwner" class="form-select" runat="server" />
                <asp:CustomValidator
                    ID="OwnerNullValidator"
                    runat="server"
                    ControlToValidate="ddlOwner"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckCValidation"
                    OnServerValidate="CityNullValidator_ServerValidate">* Select city! 
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMaxAdults" for="numMaxAdults" class="form-label" meta:resourcekey="lblnumMaxAdults" runat="server" Text="Max adults"></asp:Label>
                <asp:TextBox ID="numMaxAdults" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="MaxAdultsRFV"
                    runat="server"
                    ControlToValidate="numMaxAdults"
                    Display="Dynamic"
                    ForeColor="Red">* Maximum number of adults missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="MaxAdultsTooSmall"
                    runat="server"
                    ControlToValidate="numMaxAdults"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMaxChildren" for="DdlMaxChildren" class="form-label" meta:resourcekey="lblDdlMaxChildren" runat="server" Text="Max children"></asp:Label>
                <asp:TextBox ID="numMaxChildren" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="numMaxChildren"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblBeachDistance" for="DdlBeachDistance" class="form-label" meta:resourcekey="lblDdlBeachDistance" runat="server" Text="Beach distance"></asp:Label>
                <asp:TextBox ID="numBeachDistance" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="BeachDistanceRFV"
                    runat="server"
                    ControlToValidate="numBeachDistance"
                    Display="Dynamic"
                    ForeColor="Red">* Beach distance missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="BeachDistanceTooSmall"
                    runat="server"
                    ControlToValidate="numBeachDistance"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTotalRooms" for="DdlTotalRooms" class="form-label" meta:resourcekey="lblDdlTotalRooms" runat="server" Text="Total rooms"></asp:Label>
                <asp:TextBox ID="numTotalRooms" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="TotalRoomsRFV"
                    runat="server"
                    ControlToValidate="numTotalRooms"
                    Display="Dynamic"
                    ForeColor="Red">* Number of total rooms missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="TotalRoomsTooSmall"
                    runat="server"
                    ControlToValidate="numTotalRooms"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPrice" for="DdlPrice" class="form-label" meta:resourcekey="lblDdlPrice" runat="server" Text="Price"></asp:Label>
                <asp:TextBox ID="numPrice" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="PriceRFV"
                    runat="server"
                    ControlToValidate="numPrice"
                    Display="Dynamic"
                    ForeColor="Red">* Price missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="PriceTooSmall"
                    runat="server"
                    ControlToValidate="numPrice"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="PriceUpdateTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Button ID="btnUploadApartmant" runat="server" class="btn btn-outline-success" Text="Add apartmant" OnClick="btnUploadApartmant_Click" />
                <asp:Button ID="btnCancel" runat="server" class="btn btn-outline-danger" meta:resourcekey="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </fieldset>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Update apartmant -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlUpdateApartmant" runat="server" Visible="false">
        <fieldset class="container p-4">
            <legend runat="server" meta:resourcekey="pnlUpdateApartmant">Edit</legend>
            <div class="mb-3">
                <asp:Label ID="lblNameUpdate" for="txtNameUpdate" class="form-label" meta:resourcekey="lblNameUpdate" runat="server" Text="Apartmant name:"></asp:Label>
                <asp:TextBox ID="txtNameUpdate" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="NameUpdateRFV"
                    runat="server"
                    meta:resourcekey="rfvNameUpdate"
                    ControlToValidate="txtNameUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Name missing!
                </asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblNameEngUpdate" for="txtNameEngUpdate" class="form-label" meta:resourcekey="lblNameEngUpadate" runat="server" Text="English name"></asp:Label>
                <asp:TextBox ID="txtNameEngUpdate" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="NameEngUpdateRFV"
                    runat="server"
                    meta:resourcekey="rfvNameEngUpdate"
                    ControlToValidate="txtNameEngUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* English name missing!
                </asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblAddresUpdate" for="txtAddressUpdate" class="form-label" meta:resourcekey="lblAddressUpdate" runat="server" Text="Address"></asp:Label>
                <asp:TextBox ID="txtAddressUpdate" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="AddressUpdateRFV"
                    runat="server"
                    meta:resourcekey="rfvAddressUpdate"
                    ControlToValidate="txtAddressUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Address missing!
                </asp:RequiredFieldValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblCityUpdate" for="ddlUpdateApartmantCity" class="form-label" meta:resourcekey="lblDdlCityUpdate" runat="server" Text="City"></asp:Label>
                <asp:DropDownList ID="ddlUpdateApartmantCity" class="form-select" runat="server" />
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="ddlUpdateApartmantCity"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckCValidation"
                    OnServerValidate="CityNullValidator_ServerValidate">* Select city!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblUpdateOwner" for="DdlUpdateOwner" class="form-label" meta:resourcekey="lblDdlUpdateOwner" runat="server" Text="Owner"></asp:Label>
                <asp:DropDownList ID="ddlUpdateOwner" class="form-select" runat="server" />
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="ddlUpdateOwner"
                    Display="Dynamic"
                    ForeColor="Red"
                    ClientValidationFunction="CheckCValidation"
                    OnServerValidate="CityNullValidator_ServerValidate">* Select city! 
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMaxAdultsUpdate" for="numMaxAdultsUpdate" class="form-label" meta:resourcekey="lblnumMaxAdultsUpdate" runat="server" Text="Max adults"></asp:Label>
                <asp:TextBox ID="numMaxAdultsUpdate" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="numMaxAdultsUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Maximum number of adults missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="numMaxAdultsUpdate"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblMaxChildrenUpdate" for="numMaxChildrenUpdate" class="form-label" meta:resourcekey="lblnumMaxChildrenUpdate" runat="server" Text="Max children"></asp:Label>
                <asp:TextBox ID="numMaxChildrenUpdate" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="numMaxChildrenUpdate"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblBeachDistanceUpdate" for="numBeachDistanceUpdate" class="form-label" meta:resourcekey="lblnumBeachDistanceUpdate" runat="server" Text="Beach disteance"></asp:Label>
                <asp:TextBox ID="numBeachDistanceUpdate" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="numBeachDistanceUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Beach distance missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="numBeachDistanceUpdate"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblTotalRoomsUpdate" for="numTotalRoomsUpdate" class="form-label" meta:resourcekey="lblnumTotalRoomsUpdate" runat="server" Text="Total rooms"></asp:Label>
                <asp:TextBox ID="numTotalRoomsUpdate" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="numTotalRoomsUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Number of total rooms missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    runat="server"
                    ControlToValidate="numTotalRoomsUpdate"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="MaxAdultsTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Label ID="lblPriceUpdate" for="numPriceUpdate" class="form-label" meta:resourcekey="lblnumPriceUpdate" runat="server" Text="Price"></asp:Label>
                <asp:TextBox ID="numPriceUpdate" TextMode="Number" class="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="PriceUpdateRFV"
                    runat="server"
                    ControlToValidate="numPriceUpdate"
                    Display="Dynamic"
                    ForeColor="Red">* Price missing!
                </asp:RequiredFieldValidator>
                <asp:CustomValidator
                    ID="PriceUpdateTooSmall"
                    runat="server"
                    ControlToValidate="numPriceUpdate"
                    Display="Dynamic"
                    ForeColor="Red"
                    OnServerValidate="PriceUpdateTooSmall_ServerValidate">* Number must be greater than 0!
                </asp:CustomValidator>
            </div>
            <div class="mb-3">
                <asp:Button ID="btnUpdateApartmant" runat="server" CssClass="btn btn-outline-success" Text="Update apartmant" OnClick="btnUpdateApartmant_Click" />
                <asp:Button ID="btnCancelUpdate" runat="server" class="btn btn-outline-danger" meta:resourcekey="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
            </div>
        </fieldset>
    </asp:Panel>

    <!-------------------------------------------------------------------------------- Apartmant reservations -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlReservation" runat="server" Visible="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">New reservation</h5>
                        <asp:Button OnClick="btnCancel_Click" class="btn-close" runat="server" />
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <asp:CheckBox ID="cbExisting" runat="server" OnCheckedChanged="cbExisting_CheckedChanged" Checked="true" AutoPostBack="true" />
                            <asp:Label ID="lblExisting" runat="server" class="form-label" Text="Existing user"></asp:Label>
                        </div>
                        <asp:Panel ID="pnlExistingUser" runat="server" Visible="true">
                            <div class="mb-3">
                                <asp:Label ID="lblUser" runat="server" class="form-label" Text="Users"></asp:Label>
                                <asp:DropDownList ID="ddlUsers" runat="server" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlAnonimusUser" runat="server" Visible="false">
                            <div class="mb-3">
                                <div class="mb-3">
                                    <asp:Label ID="lblUserName" runat="server" class="form-label" Text="Name"></asp:Label>
                                    <asp:TextBox ID="txtUserName" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        meta:resourcekey="rfvUsername"
                                        ControlToValidate="txtUsername"
                                        Display="Dynamic"
                                        ForeColor="Red">* Username missing!
                                    </asp:RequiredFieldValidator>

                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblUserEmail" runat="server" class="form-label" Text="Email"></asp:Label>
                                    <asp:TextBox ID="txtEmail" type="email" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3"
                                        runat="server"
                                        meta:resourcekey="rfvEmail"
                                        ControlToValidate="txtEmail"
                                        Display="Dynamic"
                                        ForeColor="Red">* Email missing!
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblUserPhone" runat="server" class="form-label" Text="Phone"></asp:Label>
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
                                    <asp:Label ID="lblUserAddress" runat="server" class="form-label" Text="Address"></asp:Label>
                                    <asp:TextBox ID="txtAddressAnonimus" class="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2"
                                        runat="server"
                                        meta:resourcekey="rfvAddress"
                                        ControlToValidate="txtAddress"
                                        Display="Dynamic"
                                        ForeColor="Red">* Address missing!
                                    </asp:RequiredFieldValidator>
                                </div>

                            </div>
                        </asp:Panel>
                        <div class="mb-3">
                            <asp:Label ID="lblAnonimusDetils" runat="server" class="form-label" Text="Details"></asp:Label>
                            <asp:TextBox ID="tbDetails" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddReservation" runat="server" class="btn btn-success" Text="Add reservation" OnClick="btnAddReservation_Click" />
                        <asp:Button ID="btnReservationCancel" runat="server" class="btn btn-danger" meta:resourcekey="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

        <!-------------------------------------------------------------------------------- Existing reservations -------------------------------------------------------------------------------->


        <div class="modal-dialog modal-xl modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Existing reservations</h5>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <asp:Repeater ID="rptExistingReservations" runat="server">
                            <HeaderTemplate>
                                <table id="tblExistingReservations" class="table table-striped">
                                    <thead>
                                        <tr>
                                            <th scope="col">Name</th>
                                            <th scope="col">Email</th>
                                            <th scope="col">Phone</th>
                                            <th scope="col">Address</th>
                                            <th scope="col">Details</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval(nameof(ApartmantsLibrary.Model.ApartmantReservation.UserName)) %></td>
                                    <td><%#Eval(nameof(ApartmantsLibrary.Model.ApartmantReservation.UserEmail)) %></td>
                                    <td><%#Eval(nameof(ApartmantsLibrary.Model.ApartmantReservation.UserPhone)) %></td>
                                    <td><%#Eval(nameof(ApartmantsLibrary.Model.ApartmantReservation.UserAddress)) %></td>
                                    <td><%#Eval(nameof(ApartmantsLibrary.Model.ApartmantReservation.Details)) %></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                    </table>
                            </FooterTemplate>
                        </asp:Repeater>


                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <!-------------------------------------------------------------------------------- Delete apartmant -------------------------------------------------------------------------------->
    <asp:Panel ID="pnlDelete" runat="server" Visible="false">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Delete apartman</h5>
                        <asp:Button OnClick="btnCancel_Click" class="btn-close" runat="server" />
                    </div>
                    <div class="modal-body">
                        Are your shure you want to delete this apartmant ?
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDeleteCancel" runat="server" class="btn btn-secondary" meta:resourcekey="btnDeleteCancel" Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:Button ID="btnDeleteConfirm" runat="server" class="btn btn-danger" meta:resourcekey="btnDeleteSend" Text="Delete" OnClick="btnDeleteConfirm_Click" />
                        <asp:HiddenField ID="hfTagToDeleteId" runat="server" />
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblResult" runat="server"></asp:Label>

    <asp:HiddenField ID="hfApartmantId" runat="server" />

</asp:Content>
