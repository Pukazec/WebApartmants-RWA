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
    public partial class TagsUpdate : System.Web.UI.Page
    {

        private IList<TaggedApartmant> _listOfAllTaggedApartmants;
        private IList<Tag> _listOfAllTags;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["apartmantId"] == null)
            {
                Response.Redirect("Apartmants.aspx");
            }

            int apartmantId = int.Parse(Session["apartmantId"].ToString());

            _listOfAllTaggedApartmants = ((IRepo)Application["database"]).LoadTaggedApartmant(apartmantId);

            foreach (TaggedApartmant t in _listOfAllTaggedApartmants)
            {
                Button btn = new Button
                {
                    Text = t.Tag.NameEng,
                    ID = t.Id.ToString(),
                    CssClass = "btn btn-outline-warning m-1"
                };
                btn.Click += Btn_Click;
                btn.CommandArgument = t.Id.ToString();

                pnlTags.Controls.Add(btn);
            }

            if (!IsPostBack)
            {
                _listOfAllTags = ((IRepo)Application["database"]).LoadTags();
                _listOfAllTags.Insert(0, new Tag { Id = 0, NameEng = "-select tags-" });

                AppendTags();
            }
        }

        private void AppendTags()
        {
            ddlTagsToAdd.DataSource = _listOfAllTags;
            ddlTagsToAdd.DataValueField = "Id";
            ddlTagsToAdd.DataTextField = "NameEng";
            ddlTagsToAdd.DataBind();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int taggedApartmantId = int.Parse(button.CommandArgument);

            try
            {
                ((IRepo)Application["database"]).DeleteTaggedApartmant(taggedApartmantId);

                Response.Redirect(Request.Url.LocalPath);
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void ddlTagsToAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tagId = int.Parse(ddlTagsToAdd.SelectedItem.Value);
            string value = ddlTagsToAdd.SelectedItem.Value;
            lblError.Text = value;
            lblError.Visible = false;
            lblError.Visible = true;

            _listOfAllTaggedApartmants = ((IRepo)Application["database"]).LoadTaggedApartmant(int.Parse(Session["apartmantId"].ToString()));


            foreach (var tag in _listOfAllTaggedApartmants)
            {
                if (tag.TagId == tagId)
                {
                    return;
                }
            }

            TaggedApartmant ta = new TaggedApartmant
            {
                TagId = tagId,
                ApartmantId = int.Parse(Session["apartmantId"].ToString())
            };

            try
            {
                ((IRepo)Application["database"]).AddTaggedApartmant(ta);
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
                            lblError.Visible = true; ;
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnTagCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Apartmants.aspx");
        }
    }
}