﻿
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Production_Packing : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
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
                FillGrid();

            }
        }
    }

    //Fill GridView
    private void FillGrid()
    {
        DataTable Dt = Cls_Main.Read_Table("SELECT  PD.ProjectCode, PD.ProjectName, PH.CustomerName, " +
                " COUNT(*) AS TotalRecords, SUM(CAST(OutwardQty AS INT)) AS OutwardQty " +
                " FROM tbl_ProductionDTLS AS PD INNER JOIN tbl_ProductionHDR AS PH ON PH.JobNo=PD.JobNo " +
                " Where PD.Stage = 'Packaging' and PD.Status < 2 " +
                " GROUP BY  PD.ProjectCode, PD.ProjectName, PH.CustomerName " +
                " ORDER BY PD.ProjectCode desc ");
        MainGridLoad.DataSource = Dt;
        MainGridLoad.DataBind();


    }



    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
 
        if (e.CommandName == "Rowwarehouse")
        {
            //this.ModalPopupExtender1.Show();
            DivWarehouse.Visible = true;
            divtable.Visible = false;
            string rowIndex = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");

            hdnJobid.Value = ((Label)row.FindControl("jobno")).Text;
            GetRequestdata(hdnJobid.Value);


        }
        if (e.CommandName == "Edit")
        {
            string rowIndex = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");

            string Total_Price = ((Label)row.FindControl("Total_Price")).Text;
            string InwardQty = ((Label)row.FindControl("InwardQty")).Text;
            string OutwardQty = ((Label)row.FindControl("OutwardQty")).Text;
            string RevertQty = ((Label)row.FindControl("RevertQty")).Text;
            string CustomerName = ((Label)row.FindControl("CustomerName")).Text;
            string JobNo = ((Label)row.FindControl("jobno")).Text;

            txtcustomername.Text = CustomerName;
            txtinwardqty.Text = InwardQty;
            txttotalqty.Text = Total_Price;
            txtjobno.Text = JobNo;
            txtoutwardqty.Text = OutwardQty;
            GetRemarks();
            int A, B;

            if (!int.TryParse(txtinwardqty.Text, out A))
            {
                A = 0;
            }

            if (!int.TryParse(txtoutwardqty.Text, out B))
            {
                B = 0;
            }

            txtoutwardqty.Text = txtoutwardqty.Text;
            txtpending.Text = (A - B).ToString();
            this.ModalPopupHistory.Show();
        }

        if (e.CommandName == "DrawingFiles")
        {
            string rowIndex = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");

            string JobNo = ((Label)row.FindControl("jobno")).Text;
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_DrawingDetails AS PD where JobNo='" + JobNo + "'");
            if (Dt.Rows.Count > 0)
            {
                rptImages.DataSource = Dt;
                rptImages.DataBind();
                this.ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data not found..!!')", true);
            }

        }
    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //GVPurchase.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblStatus = e.Row.FindControl("Status") as Label;

                if (lblStatus != null)
                {
                    string statusCode = lblStatus.Text;

                    // Update the status text based on the status code
                    switch (statusCode)
                    {
                        case "0":
                            lblStatus.Text = "Pending";
                            lblStatus.ForeColor = System.Drawing.Color.Orange;
                            break;
                        case "1":
                            lblStatus.Text = "In-Process";
                            lblStatus.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case "2":
                            lblStatus.Text = "Completed";
                            lblStatus.ForeColor = System.Drawing.Color.Green;
                            break;
                        default:
                            lblStatus.Text = "Unknown";
                            lblStatus.ForeColor = System.Drawing.Color.Gray;
                            break;
                    }
                }

            }
        }
        catch
        {

        }
    }



    protected void ImageButtonfile2_Click(object sender, ImageClickEventArgs e)
    {
        string id = ((sender as ImageButton).CommandArgument).ToString();

        Display(id);
    }

    public void Display(string id)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                string CmdText = "select FileName from tbl_DrawingDetails where ID='" + id + "'";

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(dt.Rows[0]["Path"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["FileName"].ToString()))
                    {
                        Response.Redirect("~/Drawings/" + dt.Rows[0]["FileName"].ToString());
                    }
                    else
                    {
                        //lblnotfound.Text = "File Not Found or Not Available !!";
                    }
                }
                else
                {
                    //lblnotfound.Text = "File Not Found or Not Available !!";
                }

            }
        }
    }


    //Send  Next step production
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtoutwardqty.Text != null && txtoutwardqty.Text != "" && txtpending.Text != "")
            {
                if (Convert.ToDouble(txtpending.Text) + 1 > Convert.ToDouble(txtoutwardqty.Text))
                {
                    Cls_Main.Conn_Open();
                    SqlCommand cmd = new SqlCommand("DB_Foundtech.ManageProductionDetails", Cls_Main.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "UpdateSendToNext");
                    cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                    cmd.Parameters.AddWithValue("@StageNumber", 5);
                    cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(txtinwardqty.Text));
                    cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(txtoutwardqty.Text));
                    cmd.Parameters.AddWithValue("@PendingQty", Convert.ToDouble(txtpending.Text));
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
                    cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                    Cls_Main.Conn_Dispose();

                    FillGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please check Outward Quantity is Greater then Inward Quantity..!!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please fill data...........!!');", true);
            }
        }
        catch
        {

        }
    }

    //Send  back step production
    protected void btnsendtoback_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtoutwardqty.Text != null && txtoutwardqty.Text != "" && txtpending.Text != "")
            {
                if (Convert.ToDouble(txtpending.Text) + 1 > Convert.ToDouble(txtoutwardqty.Text))
                {
                    Cls_Main.Conn_Open();
                    SqlCommand cmd = new SqlCommand("DB_Foundtech.ManageProductionDetails", Cls_Main.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "UpdateSendToBack");
                    cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                    cmd.Parameters.AddWithValue("@StageNumber", 5);
                    cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(txtinwardqty.Text));
                    cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(txtoutwardqty.Text));
                    cmd.Parameters.AddWithValue("@PendingQty", Convert.ToDouble(txtpending.Text));
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
                    cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                    Cls_Main.Conn_Dispose();

                    FillGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send Back..!!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please fill data...........!!');", true);
            }
        }
        catch
        {

        }
    }

    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

 

    public void GetRemarks()
    {
        Cls_Main.Conn_Open();
        SqlCommand cmdselect = new SqlCommand("select Remark from  tbl_ProductionDTLS  WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
        cmdselect.Parameters.AddWithValue("@StageNumber", 4);
        cmdselect.Parameters.AddWithValue("@JobNo", txtjobno.Text);
        Object Remarks = cmdselect.ExecuteScalar();
        Cls_Main.Conn_Close();
        if (Remarks != null)
        {
            txtRemarks.Text = Remarks.ToString();
        }
    }




    //Search Size Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetAvailablesizeList(string prefixText, int count)
    {
        return AutoFillGetAvailablesizeList(prefixText);
    }

    public static List<string> AutoFillGetAvailablesizeList(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select Size from tbl_InwardData where IsDeleted=0  AND " + "Size like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["Size"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtAvailablesize_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_InwardData WHERE RowMaterial='" + txtRMC.Text.Trim() + "' AND Size='" + txtAvailablesize.Text.Trim() + "' AND IsDeleted=0", Cls_Main.Conn);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            // txtAvailablesize.Text = dtpt.Rows[0]["Size"].ToString();
            txtAvilableqty.Text = dtpt.Rows[0]["InwardQty"].ToString();

        }
        else
        {
            txtAvilableqty.Text = "";
        }
    }

    protected void btnWarehousedata_Click(object sender, EventArgs e)
    {
        Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SP_InventoryDetails", Cls_Main.Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Mode", "InseartInventoryrequest");
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
        cmd.Parameters.AddWithValue("@NeedQty", txtneedqty.Text);
        cmd.Parameters.AddWithValue("@NeedSize", txtsize.Text);
        cmd.Parameters.AddWithValue("@AvailableQty", txtAvilableqty.Text);
        cmd.Parameters.AddWithValue("@AvailableSize", txtAvailablesize.Text);
        cmd.Parameters.AddWithValue("@RowMaterial", txtRMC.Text);
        cmd.Parameters.AddWithValue("@JobNo", hdnJobid.Value);
        cmd.Parameters.AddWithValue("@Weight", Txtweight.Text);
        cmd.Parameters.AddWithValue("@stages", 6);
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Record Successfully..!!');window.location='Drawing.aspx';", true);
    }

    protected void btncancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlazmaCutting.aspx");
    }


    protected void GVRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "RowDelete")
        {
            Cls_Main.Conn_Open();
            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_InventoryRequest] SET IsDeleted=@IsDeleted,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID", Cls_Main.Conn);
            Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.Parameters.AddWithValue("@IsDeleted", '1');
            Cmd.Parameters.AddWithValue("@DeletedBy", Session["UserCode"].ToString());
            Cmd.Parameters.AddWithValue("@DeletedOn", DateTime.Now);
            Cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Request Deleted Successfully..!!')", true);

        }
    }

    protected void GVRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public void GetRequestdata(string jobno)
    {
        //DataTable dtpt = Cls_Main.Read_Table("select * from tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 ");

        DataTable dtpt = Cls_Main.Read_Table("SELECT * FROM tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 AND stages=6");
        if (dtpt.Rows.Count > 0)
        {
            GVRequest.DataSource = dtpt;
            GVRequest.DataBind();

        }
    }


    //Search RMC Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetRMCList(string prefixText, int count)
    {
        return AutoFillCustomerName(prefixText);
    }

    public static List<string> AutoFillCustomerName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT RowMaterial from tbl_InwardData where IsDeleted=0  AND " + "RowMaterial like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["RowMaterial"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }


    private static DataTable GetData(string query)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    protected void MainGridLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ProjectCode = e.Row.FindControl("lblProjectCode") as Label;
                GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                if (GVPurchase == null)
                {

                    return;
                }

                if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                {
                    var data = GetData(string.Format("SELECT * FROM tbl_ProductionDTLS  AS Pd" +
                        " Inner Join tbl_OrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                        " WHERE Pd.Stage = 'Packaging' AND Pd.ProjectCode='{0}'", ProjectCode.Text));
                    if (data != null && data.Rows.Count > 0)
                    {
                        GVPurchase.DataSource = data;
                        GVPurchase.DataBind();
                    }
                    else
                    {
                        GVPurchase.Visible = false;
                    }
                }

            }
        }
        catch
        {
            throw;
        }

    }
}


