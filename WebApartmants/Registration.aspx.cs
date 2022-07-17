using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApartmants
{
    public partial class Registration : System.Web.UI.Page
    {
        private IList<User> _listOfAllUsers;

        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllUsers = ((IRepo)Application["database"]).LoadUsers();

            if (!IsPostBack)
            {
                if (Session["user"] != null)
                {
                    Response.Redirect("Apartmants.aspx");
                }

                PanelForm.Visible = true;
                PanelPrint.Visible = false;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            try
            {
                if (IsPostBack && ViewState["user"] != null)
                {
                    User u = (User)ViewState["user"];
                    ((IRepo)Application["database"]).AddUser(u);
                    Session["user"] = u;

                    var method = Request.HttpMethod.ToLower();
                    Response.Redirect("Apartmants.aspx");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Errors.Count > 0)
                {
                    switch (ex.Errors[0].Number)
                    {
                        case 2627:
                            lblResult.Text = "Error: User with the same username alredy exists!";
                            lblResult.Visible = true;
                            txtUsername.Text = "";
                            break;
                    }
                }
            }
            catch (Exception) { 
            }

            base.OnPreRender(e);
        }

        protected void ForbidenValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.ToLower() == "admin")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void UsernamenameExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (User user in _listOfAllUsers)
            {
                if (user.UserName == args.Value)
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

        protected void EmailExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (User user in _listOfAllUsers)
            {
                if (user.Email == args.Value)
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

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                User u = new User
                {
                    UserName = txtUsername.Text,
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    PhoneNumber = txtPhone.Text,
                    Password = Cryptography.HashPassword(txtPassword.Text)
                };
                ViewState["user"] = u;
            }
        }
    }
}