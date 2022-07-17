using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApartmants
{
    public partial class ImagesUpdate : Page
    {
        private IList<ApartmantPicture> _listOfAllPictures;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["apartmantId"] == null)
            {
                Response.Redirect("Apartmants.aspx");
            }

            int apartmantId = int.Parse(Session["apartmantId"].ToString());

            _listOfAllPictures = ((IRepo)Application["database"]).LoadPictures(apartmantId);

            foreach (ApartmantPicture ap in _listOfAllPictures)
            {
                ImageButton img = new ImageButton();
                img.ID = ap.Id.ToString();
                img.ToolTip = ap.Name;
                img.CommandArgument = ap.IsRepresentative.ToString();
                img.CssClass = "mg-thumbnail m-1";
                img.Height = 150;
                img.ImageUrl = "data:image/jpg;base64," + ap.Bytes.ToString();
                img.Click += Img_Click;

                if (ap.IsRepresentative)
                {
                    img.CssClass = "rounded";
                }

                pnlImages.Controls.Add(img);
            }
        }

        //-------------------------------------------------------------------------------- Validators --------------------------------------------------------------------------------

        protected void validationFuImage_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg" };
            string ext = Path.GetExtension(fuImage.PostedFile.FileName);
            args.IsValid = false;

            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    args.IsValid = true;
                    return;
                }
            }
        }

        //-------------------------------------------------------------------------------- Adding new image --------------------------------------------------------------------------------

        protected void btnAddNewImage_Click(object sender, EventArgs e)
        {
            pnlAddImage.Visible = true;
            pnlUploadImage.Visible = false;
        }

        //-------------------------------------------------------------------------------- Insert image --------------------------------------------------------------------------------

        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            if (Page.IsValid && fuImage.HasFile)
            {
                ApartmantPicture p = new ApartmantPicture
                {
                    Name = txtImageName.Text,
                    Bytes = Convert.ToBase64String(fuImage.FileBytes),
                    ApartmentId = int.Parse(Session["apartmantId"].ToString()),
                    Guid = Guid.NewGuid(),
                };


                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// staaaaaaaaaaa /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                try
                {
                    ((IRepo)Application["database"]).AddPicture(p);
                    Response.Redirect(Request.Url.LocalPath);
                }
                catch (SqlException ex)
                {
                    if (ex.Errors.Count > 0)
                    {
                        switch (ex.Errors[0].Number)
                        {
                            case 2627: // Constraint violation
                                lblError.Text = "Error: Tag alredy exists!";
                                lblError.Visible = true;
                                break;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //-------------------------------------------------------------------------------- Update image panel --------------------------------------------------------------------------------

        private void Img_Click(object sender, ImageClickEventArgs e)
        {
            int imgId = int.Parse((sender as ImageButton).ID);

            ApartmantPicture ap = _listOfAllPictures.FirstOrDefault(pic => pic.Id == imgId);
            imgShown.ToolTip = ap.Id.ToString();
            imgShown.CommandArgument = ap.IsRepresentative.ToString();
            imgShown.CssClass = "mg-thumbnail m-1";
            imgShown.Height = 150;
            imgShown.ImageUrl = "data:image/jpg;base64," + ap.Bytes.ToString();

            txtImageNameUpdate.Text = ap.Name;
            pnlEditImage.Visible = true;
            pnlUploadImage.Visible = false;
        }

        //-------------------------------------------------------------------------------- Update image --------------------------------------------------------------------------------

        protected void btnUpdateImage_Click(object sender, EventArgs e)
        {
            ApartmantPicture p = _listOfAllPictures.SingleOrDefault(pic => pic.Id == int.Parse(imgShown.ID));
            p.Name = txtImageNameUpdate.Text;

            try
            {
                ((IRepo)Application["database"]).UpdateImage(p);
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblError.Text = "Error: Tag alredy exists!";
                            lblError.Visible = true;
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnSetRepresentative_Click(object sender, EventArgs e)
        {
            int imgId = int.Parse(imgShown.ToolTip);
            int apartmantId = int.Parse(Session["apartmantId"].ToString());

            try
            {
                ((IRepo)Application["database"]).SetRepresentative(imgId, apartmantId);
                Response.Redirect(Request.Url.LocalPath);
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627: // Constraint violation
                            lblError.Text = "Error: Tag alredy exists!";
                            lblError.Visible = true;
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Apartmants.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.LocalPath);
        }
    }
}