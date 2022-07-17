using ApartmantsLibrary.Dal;
using ApartmantsLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApartmants
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PanelForm.Visible = true;
                PanelPrint.Visible = false;
            }

            if (Session["user"] != null)
            {
                Response.Redirect("Apartmants.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                var username = txtUsername.Text;
                var password = Cryptography.HashPassword(txtPassword.Text);

                User user = ((IRepo)Application["database"]).AuthenticateUser(username, password);

                if (user == null)
                {
                    PanelForm.Visible = true;
                    PanelPrint.Visible = true;

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    Session["user"] = user;
                    Response.Redirect("Apartmants.aspx");
                }
            }
        }
    }
}