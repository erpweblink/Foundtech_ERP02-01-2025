using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Laxshmi_LaxshmiDashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserCode"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                CustomerCount();
                MaterialCount();
                InwardCount();
                OutwardCount();
                InventoryCount();
            }
        }
    }

    protected void CustomerCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_CompanyMaster where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tblTaxInvoiceHdr where CreatedBy='" + Session["UserCode"].ToString() + "'  AND IsDeleted=0 and Status>=2 AND CONVERT(nvarchar(10),CreatedOn,103)=CONVERT(nvarchar(10),GETDATE(),103)", Cls_Main.Conn);

        //    count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        lblcustomers.Text = count.ToString();
        Cls_Main.Conn_Close();
    }

    protected void MaterialCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_ComponentMaster where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblMaterial.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void InwardCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_inwarddata where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblInwardQuantity.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void OutwardCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_Outwarddata ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblOuwardQuantity.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void InventoryCount()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (ddlType.SelectedItem.Text == "ToDay")
            {
                cmd.Parameters.AddWithValue("@Mode", "GetToDayList");
            }
            if (ddlType.SelectedItem.Text == "Month")
            {
                cmd.Parameters.AddWithValue("@Mode", "GetMonthList");

            }
            if (ddlType.SelectedItem.Text == "Year")
            {
                cmd.Parameters.AddWithValue("@Mode", "GetYearList");
            }


            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Columns.Count > 0)
            {
                GVCustomerList.DataSource = dt;
                GVCustomerList.DataBind();
            }


        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);

        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        InventoryCount();
    }

    protected void GVCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowOutwardList")
        {
            Response.Redirect("../Laxshmi/OutwardReport.aspx");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetMonthList");
            cmd.Parameters.AddWithValue("@Month", ddlMonth.SelectedValue);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Columns.Count > 0)
            {
                GVCustomerList.DataSource = dt;
                GVCustomerList.DataBind();
            }


        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);

        }
    }
}