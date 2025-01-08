using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_UserAuthorization : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    string Pages = "";
    string PagesView = "";
    string PageName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                BindRole();

                GridDiv.Visible = false;
            }
        }
    }

    protected void BindUser()
    {
        try
        {
            ddluser.Items.Clear();
            ddluser.Items.Add(new ListItem("--Select User--", ""));
            DataTable Dt = new DataTable();

            SqlDataAdapter Da = new SqlDataAdapter("select ID,Username from tbl_UserMaster where Role='" + ddlrole.SelectedItem.Text + "' AND isdeleted='0' AND status='1' ", con);
            Da.Fill(Dt);
            ddluser.DataTextField = "Username";
            ddluser.DataValueField = "ID";
            ddluser.DataSource = Dt;
            ddluser.DataBind();
            //ddluser.Items.Insert(0, "-- Select User --");
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void BindRole()
    {
        try
        {
            ddlrole.Items.Clear();
            ddlrole.Items.Add(new ListItem("--Select Role--", ""));
            DataTable Dt = new DataTable();

            SqlDataAdapter Da = new SqlDataAdapter("Select Id,RoleName From tbl_Role where  IsDeleted=0", con);
            Da.Fill(Dt);
            ddlrole.DataTextField = "RoleName";
            ddlrole.DataValueField = "RoleName";
            ddlrole.DataSource = Dt;
            ddlrole.DataBind();

            BindUser();
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindUser();
    }

    protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //con.Open();
            GridDiv.Visible = true;
            DataTable Dt = new DataTable();
            SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM [tblUserRoleAuthorization] where [UserID]='" + ddluser.SelectedItem.Value + "'", con);
            Da.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {

                DataTable Dttt = new DataTable();
                SqlDataAdapter Daaa = new SqlDataAdapter(@"select DISTINCT AP.ID,AP.MenuID,AP.MenuName,AP.PageName,ISNULL(UA.Pages,0),ISNULL(UA.PagesView,0)
from tblAuthPages AS AP
LEFT JOIN[tblUserRoleAuthorization]
AS UA ON AP.MenuID = UA.menuId AND UA.UserID = '" + ddluser.SelectedItem.Value + "'", con);
                Daaa.Fill(Dttt);
                gvUserAuthorization.EmptyDataText = "No Records Found";
                gvUserAuthorization.DataSource = Dttt;
                gvUserAuthorization.DataBind();

            }
            else
            {
                //con.Close();

                btnSubmit.Text = "Save";
                DataTable Dttt = new DataTable();
                SqlDataAdapter Daaa = new SqlDataAdapter("SELECT * FROM [tblAuthPages]", con);
                Daaa.Fill(Dttt);
                gvUserAuthorization.EmptyDataText = "No Records Found";
                gvUserAuthorization.DataSource = Dttt;
                //con.Open();
                gvUserAuthorization.DataBind();
                //con.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvUserAuthorization_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkpages = (CheckBox)e.Row.FindControl("chkPages");
            CheckBox chkpagesview = (CheckBox)e.Row.FindControl("chkPagesView");
            Label lblMenuName = (Label)e.Row.FindControl("lblMenuName");


            con.Open();
            int id = Convert.ToInt32(gvUserAuthorization.DataKeys[e.Row.RowIndex].Values[0]);

            SqlCommand cmd = new SqlCommand("select PageName,Pages,PagesView from tblUserRoleAuthorization where menuId='" + id + "' AND [UserID]='" + ddluser.SelectedItem.Value + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Pages = dr["Pages"].ToString();
                PagesView = dr["PagesView"].ToString();
                PageName = dr["PageName"].ToString();

                dr.Close();
                chkpages.Checked = Pages == "True" ? true : false;
                chkpagesview.Checked = PagesView == "True" ? true : false;
            }
            else
            {
                chkpages.Checked = Pages == "True" ? false : true;
               
            }
            con.Close();

        }
    }

    protected void gvUserAuthorization_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        int RowIndex = grv.RowIndex;
        CheckBox chkpages = (CheckBox)gvUserAuthorization.Rows[RowIndex].FindControl("chkPages");
        CheckBox chkpagesview = (CheckBox)gvUserAuthorization.Rows[RowIndex].FindControl("chkPagesView");
        chkpages.Checked = chkpages.Checked == true ? false : true;
        chkpagesview.Checked = chkpagesview.Checked == true ? false : true;

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSubmit.Text == "Save")
            {
                int userId = Convert.ToInt32(ddluser.SelectedItem.Value);
                con.Open();
                SqlCommand cmds = new SqlCommand("Delete tblUserRoleAuthorization where UserId= '" + userId + "'", con);
                cmds.ExecuteNonQuery();

                foreach (GridViewRow g1 in gvUserAuthorization.Rows)
                {
                    string menuname = (g1.FindControl("lblMenuName") as Label).Text;
                    string pagename = (g1.FindControl("lblPageName") as Label).Text;
                    string menu = (g1.FindControl("lblMenuId") as Label).Text;
                    bool pageChk = (g1.FindControl("chkPages") as CheckBox).Checked;
                    bool pageviewChk = (g1.FindControl("chkPagesView") as CheckBox).Checked;
                    DateTime Date = DateTime.Now;
                    SqlCommand cmd = new SqlCommand("SP_UAuthorization", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ddluser.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@UserName", ddluser.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@menuId", menu);
                    cmd.Parameters.AddWithValue("@MenuName", menuname);
                    cmd.Parameters.AddWithValue("@PageName", pagename);
                    cmd.Parameters.AddWithValue("@createdBy", Session["Username"].ToString());
                    cmd.Parameters.AddWithValue("@CreatedDate", Date);
                    cmd.Parameters.AddWithValue("@Pages", pageChk);
                    cmd.Parameters.AddWithValue("@PagesView", pageviewChk);
                    cmd.Parameters.AddWithValue("@Action", "Insert");
                    cmd.ExecuteNonQuery();

                }
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Pages Authorized Successfully..!!'); window.location='UserAuthorization.aspx';", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("UserAuthorization.aspx");
    }
}