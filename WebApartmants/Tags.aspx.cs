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
    public partial class Tags : System.Web.UI.Page
    {
        private IList<Tag> _listOfAllTags;
        private IList<TagType> _listOfAllTagTypes;


        protected void Page_Load(object sender, EventArgs e)
        {
            _listOfAllTags = ((IRepo)Application["database"]).LoadTags();
            _listOfAllTagTypes = ((IRepo)Application["database"]).LoadTagTypes();

            if (!IsPostBack)
            {
                LoadData();
                AppendTagTypes();
            }
        }

        //-------------------------------------------------------------------------------- Data load --------------------------------------------------------------------------------

        private void AppendTagTypes()
        {
            ddlTagType.DataSource = _listOfAllTagTypes;
            ddlTagType.DataValueField = "Id";
            ddlTagType.DataTextField = "Name";
            ddlTagType.DataBind();
        }

        private void LoadData()
        {
            rptTags.DataSource = _listOfAllTags;
            rptTags.DataBind();
        }

        //-------------------------------------------------------------------------------- Add tag --------------------------------------------------------------------------------

        protected void btnAddTag_Click(object sender, EventArgs e)
        {
            pnlTagsList.Visible = false;
            pnlAddPanel.Visible = true;
        }

        //-------------------------------------------------------------------------------- Canceling --------------------------------------------------------------------------------

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlTagsList.Visible = true;
            pnlAddPanel.Visible = false;
        }

        //-------------------------------------------------------------------------------- Add apartmant --------------------------------------------------------------------------------

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Tag t = new Tag
                {
                    Name = txtName.Text,
                    NameEng = txtNameEng.Text,
                    TagType = new TagType(
                        int.Parse(ddlTagType.SelectedItem.Value),
                        ddlTagType.SelectedItem.Text)
                };
                
                InsertIntoDataBase(t);
            }
        }
        private void InsertIntoDataBase(Tag t)
        {
            try
            {
                ((IRepo)Application["database"]).AddTag(t);

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


        //-------------------------------------------------------------------------------- Delete tag --------------------------------------------------------------------------------

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            pnlDelete.Visible = true;
            pnlTagsList.Visible = false;
            Button btn = (Button)sender;
            hfTagToDeleteId.Value = btn.CommandArgument;
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {            
            int tagId = int.Parse(hfTagToDeleteId.Value);

            try
            {
                ((IRepo)Application["database"]).DeleteTag(tagId);

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnDeleteCancel_Click(object sender, EventArgs e)
        {
            pnlDelete.Visible = false;
            pnlTagsList.Visible = true;
        }

        protected void btnDelete_DataBinding(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int tagId = int.Parse(btn.CommandArgument);
            bool deletable = ((IRepo)Application["database"]).DeletableTag(tagId);

            if (deletable)
            {
                btn.Enabled = true;
                btn.CssClass = "btn btn-danger";
            }
            else
            {
                btn.Enabled = false;
                btn.CssClass = "btn btn-secondary";
            }
        }

        protected void NameExistsValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            foreach (Tag tag in _listOfAllTags)
            {
                if (tag.Name == args.Value)
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
    }
}