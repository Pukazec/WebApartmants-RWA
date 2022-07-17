using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApartmants
{
    public partial class Apartmants : System.Web.UI.Page
    {
        private IList<Apartment> _listOfAllApartmants;
        private IList<Status> _listOfAllStatuses;
        private IList<City> _listOfAllCities;
        private IList<Owner> _listOfAllOwners;
        private IList<User> _listOfAllUsers;
        private IList<ApartmantReservation> _listOfAllExistingUserReservations;
        private IList<ApartmantReservation> _listOfAllAnonimusUserReservations;

        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllApartmants = ((IRepo)Application["database"]).LoadApartments();
            _listOfAllStatuses = ((IRepo)Application["database"]).LoadStatuses();
            _listOfAllStatuses.Insert(0, new Status { Id = 0, Name = "-odaberite status", NameEng = "-select status" });
            _listOfAllCities = ((IRepo)Application["database"]).LoadCities();
            _listOfAllCities.Insert(0, new City { Id = 0, Name = "-select city-" });
            _listOfAllOwners = ((IRepo)Application["database"]).LoadOwners();
            _listOfAllOwners.Insert(0, new Owner { Id = 0, Name = "-select owner-" });
            _listOfAllUsers = ((IRepo)Application["database"]).LoadUsers();
            _listOfAllUsers.Insert(0, new User { IdUser = 0, UserName = "-select user-" });

            if (!IsPostBack)
            {
                LoadData(_listOfAllApartmants);
                AppendCity();
                AppendStatus();
                AppendOwners();

                AppendUsers();
            }
        }

        //-------------------------------------------------------------------------------- Data load --------------------------------------------------------------------------------

        private void AppendUsers()
        {
            ddlUsers.DataSource = _listOfAllUsers;
            ddlUsers.DataValueField = "Id";
            ddlUsers.DataTextField = "UserName";
            ddlUsers.DataBind();
        }


        private void AppendOwners()
        {
            ddlOwner.DataSource = _listOfAllOwners;
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataTextField = "Name";
            ddlOwner.DataBind();
            ddlUpdateOwner.DataSource = _listOfAllOwners;
            ddlUpdateOwner.DataValueField = "Id";
            ddlUpdateOwner.DataTextField = "Name";
            ddlUpdateOwner.DataBind();
        }

        private void AppendCity()
        {
            ddlCity.DataSource = _listOfAllCities;
            ddlCity.DataValueField = "Id";
            ddlCity.DataTextField = "Name";
            ddlCity.DataBind();
            ddlAddApartmantCity.DataSource = _listOfAllCities;
            ddlAddApartmantCity.DataValueField = "Id";
            ddlAddApartmantCity.DataTextField = "Name";
            ddlAddApartmantCity.DataBind();
            ddlUpdateApartmantCity.DataSource = _listOfAllCities;
            ddlUpdateApartmantCity.DataValueField = "Id";
            ddlUpdateApartmantCity.DataTextField = "Name";
            ddlUpdateApartmantCity.DataBind();
        }

        private void AppendStatus()
        {
            ddlStatus.DataSource = _listOfAllStatuses;
            ddlStatus.DataValueField = "Id";
            ddlStatus.DataTextField = "NameEng";
            ddlStatus.DataBind();
        }

        private void LoadData<T>(T listOfAllApartmants)
        {
            rptApartmants.DataSource = listOfAllApartmants;
            rptApartmants.DataBind();
        }

        //-------------------------------------------------------------------------------- Filter --------------------------------------------------------------------------------

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int statusIndex = ddlStatus.SelectedIndex;
            int cityIndex = ddlCity.SelectedIndex;
            _listOfAllApartmants = ((IRepo)Application["database"]).LoadApartments();


            if (statusIndex == 0 && cityIndex == 0)
            {
                LoadData(_listOfAllApartmants);
                return;
            }

            if (statusIndex != 0 && cityIndex != 0)
            {
                var filtered = _listOfAllApartmants.Where(s => s.StatusId == statusIndex && s.CityId == cityIndex);
                LoadData(filtered);
                return;
            }

            if (statusIndex != 0)
            {
                var filtered = _listOfAllApartmants.Where(s => s.StatusId == statusIndex);
                LoadData(filtered);
            }
            if (cityIndex != 0)
            {
                var filtered = _listOfAllApartmants.Where(c => c.CityId == cityIndex);
                LoadData(filtered);
            }
        }

        //-------------------------------------------------------------------------------- Add apartmant --------------------------------------------------------------------------------

        protected void btnUploadApartmant_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Apartment a = new Apartment
                {
                    Name = txtName.Text,
                    NameEng = txtNameEng.Text,
                    MaxAdults = int.Parse(numMaxAdults.Text),
                    MaxChildren = int.Parse(numMaxChildren.Text),
                    TotalRooms = int.Parse(numTotalRooms.Text),
                    Address = txtAddress.Text,
                    City = new City(int.Parse(ddlAddApartmantCity.SelectedItem.Value), ddlAddApartmantCity.SelectedItem.Text),
                    Price = decimal.Parse(numPrice.Text),
                    BeachDistance = int.Parse(numBeachDistance.Text),
                    Owner = new Owner(int.Parse(ddlOwner.SelectedItem.Value), ddlOwner.SelectedItem.Text),
                    Guid = Guid.NewGuid(),
                };

                InsertIntoDatabase(a);
            }
        }

        private void InsertIntoDatabase(Apartment a)
        {
            try
            {
                ((IRepo)Application["database"]).AddApartmant(a);
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Error: Tag alredy exists!";
                            lblResult.Visible = true;
                            txtName.Text = "";
                            txtNameEng.Text = "";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnAddApartmant_Click(object sender, EventArgs e)
        {
            pnlApartmants.Visible = false;
            pnlAddApartmant.Visible = true;
        }

        //-------------------------------------------------------------------------------- Validators --------------------------------------------------------------------------------

        protected void ApartmantNameExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (Apartment apartment in _listOfAllApartmants)
            {
                if (apartment.Name == args.Value)
                {
                    args.IsValid = false;
                    break;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }

        protected void CityNullValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (int.Parse(args.Value) == 0)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;

        }

        protected void MaxAdultsTooSmall_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (int.Parse(args.Value) < 0)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }

        protected void PriceUpdateTooSmall_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (decimal.Parse(args.Value) <= 0)
            {
                args.IsValid = false;
                return;
            }

            args.IsValid = true;
        }


        //-------------------------------------------------------------------------------- Update apartmant --------------------------------------------------------------------------------

        protected void btnUpdateApartmant_Click(object sender, EventArgs e)
        {
            int apartmantId = int.Parse(hfApartmantId.Value);
            Apartment a = _listOfAllApartmants.SingleOrDefault(ap => ap.Id == apartmantId);
            a.Name = txtNameUpdate.Text;
            a.NameEng = txtNameEngUpdate.Text;
            a.MaxAdults = int.Parse(numMaxAdultsUpdate.Text);
            a.MaxChildren = int.Parse(numMaxChildrenUpdate.Text);
            a.TotalRooms = int.Parse(numTotalRoomsUpdate.Text);
            a.Address = txtAddressUpdate.Text;
            a.City = new City(int.Parse(ddlUpdateApartmantCity.SelectedItem.Value), ddlUpdateApartmantCity.SelectedItem.Text);
            a.Price = decimal.Parse(numPriceUpdate.Text);
            a.BeachDistance = int.Parse(numBeachDistanceUpdate.Text);
            a.Owner = new Owner(int.Parse(ddlUpdateOwner.SelectedItem.Value), ddlUpdateOwner.SelectedItem.Text);

            UpdateApartmant(a);
        }

        private void UpdateApartmant(Apartment a)
        {
            try
            {
                ((IRepo)Application["database"]).UpdateApartmant(a);

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Error: Tag alredy exists!";
                            lblResult.Visible = true;
                            txtName.Text = "";
                            txtNameEng.Text = "";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnOpenApartmant_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int selectedApartmanId = int.Parse(btn.CommandArgument);
            var selectedApartmant = _listOfAllApartmants.SingleOrDefault(a => a.Id == selectedApartmanId);

            txtNameUpdate.Text = selectedApartmant.Name;
            txtNameEngUpdate.Text = selectedApartmant.NameEng;
            txtAddressUpdate.Text = selectedApartmant.Address;
            ddlUpdateApartmantCity.SelectedIndex = selectedApartmant.CityId;
            ddlUpdateOwner.SelectedIndex = selectedApartmant.OwnerId;
            numMaxAdultsUpdate.Text = selectedApartmant.MaxAdults.ToString();
            numMaxChildrenUpdate.Text = selectedApartmant.MaxChildren.ToString();
            numBeachDistanceUpdate.Text = selectedApartmant.BeachDistance.ToString();
            numTotalRoomsUpdate.Text = selectedApartmant.TotalRooms.ToString();
            numPriceUpdate.Text = selectedApartmant.Price.ToString();
            hfApartmantId.Value = selectedApartmant.Id.ToString();

            pnlUpdateApartmant.Visible = true;
            pnlApartmants.Visible = false;
        }

        //-------------------------------------------------------------------------------- Add/update images --------------------------------------------------------------------------------

        protected void btnUploadImg_Click(object sender, EventArgs e)
        {
            Session["apartmantId"] = (sender as Button).CommandArgument.ToString();
            Response.Redirect("ImagesUpdate.aspx");
        }

        //-------------------------------------------------------------------------------- Add/update tags --------------------------------------------------------------------------------

        protected void btnUpdateTags_Click(object sender, EventArgs e)
        {
            Session["apartmantId"] = (sender as Button).CommandArgument.ToString();
            Response.Redirect("TagsUpdate.aspx");
        }
        
        //-------------------------------------------------------------------------------- Add/update reservation --------------------------------------------------------------------------------

        protected void cbExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (cbExisting.Checked)
            {
                pnlExistingUser.Visible = true;
                pnlAnonimusUser.Visible = false;
                return;
            }

            pnlAnonimusUser.Visible = true;
            pnlExistingUser.Visible = false;
        }

        protected void btnUploadReservation_Click(object sender, EventArgs e)
        {
            int apartmantId = int.Parse((sender as Button).CommandArgument);
            hfApartmantId.Value = apartmantId.ToString();

            _listOfAllExistingUserReservations = ((IRepo)Application["database"]).LoadApartmantReservationE(apartmantId);
            _listOfAllAnonimusUserReservations = ((IRepo)Application["database"]).LoadApartmantReservationA(apartmantId);

            rptExistingReservations.DataSource = _listOfAllExistingUserReservations.Concat(_listOfAllAnonimusUserReservations.ToList()).ToList();
            rptExistingReservations.DataBind();

            pnlReservation.Visible = true;
            pnlAddApartmant.Visible = false;
            pnlApartmants.Visible = false;
            pnlUpdateApartmant.Visible = false;
        }

        protected void btnAddReservation_Click(object sender, EventArgs e)
        {
            ApartmantReservation ar = new ApartmantReservation
            {
                Details = tbDetails.Text,
                ApartmentId = int.Parse(hfApartmantId.Value)
            };
            if (cbExisting.Checked)
            {
                ar.UserId = int.Parse(ddlUsers.SelectedItem.Value);
                AddExisting(ar);
                return;
            }

            ar.UserName = txtUserName.Text;
            ar.UserEmail = txtEmail.Text;
            ar.UserPhone = txtPhone.Text;
            ar.UserAddress = txtAddressAnonimus.Text;
            AddAnonimus(ar);
        }

        private void AddAnonimus(ApartmantReservation ar)
        {
            try
            {
                ((IRepo)Application["database"]).AddReservationAnonimusUser(ar);
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Error: Tag alredy exists!";
                            lblResult.Visible = true;
                            txtName.Text = "";
                            txtNameEng.Text = "";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void AddExisting(ApartmantReservation ar)
        {
            try
            {
                ((IRepo)Application["database"]).AddReservationExistingUser(ar);
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblResult.Text = "Error: Tag alredy exists!";
                            lblResult.Visible = true;
                            txtName.Text = "";
                            txtNameEng.Text = "";
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }


        //-------------------------------------------------------------------------------- Delete apartmant --------------------------------------------------------------------------------

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            pnlDelete.Visible = true;
            pnlApartmants.Visible = false;
            Button btn = (Button)sender;
            hfTagToDeleteId.Value = btn.CommandArgument;
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            int apartmantId = int.Parse(hfTagToDeleteId.Value);

            try
            {
                ((IRepo)Application["database"]).DeleteApartmant(apartmantId);

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //-------------------------------------------------------------------------------- Canceling --------------------------------------------------------------------------------

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtNameUpdate.Text = "";
            txtNameEng.Text = "";
            txtNameEngUpdate.Text = "";
            txtAddress.Text = "";
            txtAddressUpdate.Text = "";
            ddlAddApartmantCity.SelectedIndex = 0;
            ddlUpdateApartmantCity.SelectedIndex = 0;
            ddlOwner.SelectedIndex = 0;
            ddlUpdateOwner.SelectedIndex = 0;
            numMaxAdults.Text = "";
            numMaxAdultsUpdate.Text = "";
            numMaxChildren.Text = "";
            numMaxChildrenUpdate.Text = "";
            numBeachDistance.Text = "";
            numBeachDistanceUpdate.Text = "";
            numTotalRooms.Text = "";
            numTotalRoomsUpdate.Text = "";
            numPrice.Text = "";
            numPriceUpdate.Text = "";

            pnlApartmants.Visible = true;
            pnlUpdateApartmant.Visible = false;
            pnlAddApartmant.Visible = false;
            pnlReservation.Visible = false;
            pnlDelete.Visible = false;
        }
    }
}