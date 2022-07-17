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
    public partial class RegisterdUsers : System.Web.UI.Page
    {
        private IList<User> _listOfAllUsers;
        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllUsers = ((IRepo)Application["database"]).LoadUsers();
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            rptUsers.DataSource = _listOfAllUsers;
            rptUsers.DataBind();
        }
    }
}