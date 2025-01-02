
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


public partial class Production_PlazmaCutting : System.Web.UI.Page
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

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionDTLS AS PD INNER JOIN tbl_ProductionHDR AS PH ON PH.JobNo=PD.JobNo where PD.Stage='PlazmaCutting' AND CONVERT(bigint,ISNULL(InwardQTY,0))>=CONVERT(bigint,ISNULL(OutwardQTY,0)) AND PD.Status<2 ORDER BY PD.ID DESC");
        GVPurchase.DataSource = Dt;
        GVPurchase.DataBind();

    }


    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Rowwarehouse")
        {

            DivWarehouse.Visible = true;
            divtable.Visible = false;
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
            hdnJobid.Value = ((Label)row.FindControl("jobno")).Text;
            GetRequestdata(hdnJobid.Value);

        }
        if (e.CommandName == "Edit")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
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
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Data not found..!!')", true);
            }

        }
    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVPurchase.PageIndex = e.NewPageIndex;
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
                    cmd.Parameters.AddWithValue("@StageNumber", 1);
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

    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    //protected void btnsendtoback_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (txtoutwardqty.Text != null && txtoutwardqty.Text != "" && txtpending.Text != "")
    //        {

    //            Cls_Main.Conn_Open();
    //            SqlCommand Cmd2 = new SqlCommand("UPDATE [tbl_ProductionDTLS] SET  RevertQty= @RevertQty WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
    //            Cmd2.Parameters.AddWithValue("@StageNumber", 0);
    //            Cmd2.Parameters.AddWithValue("@JobNo", txtjobno.Text);
    //            Cmd2.Parameters.AddWithValue("@RevertQty", txtoutwardqty.Text);
    //            Cmd2.ExecuteNonQuery();
    //            Cls_Main.Conn_Close();

    //            Double qty = Convert.ToDouble(txtinwardqty.Text) - Convert.ToDouble(txtoutwardqty.Text);
    //            Cls_Main.Conn_Open();
    //            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_ProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,InwardQTY=@InwardQTY,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
    //            Cmd.Parameters.AddWithValue("@StageNumber", 1);
    //            Cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
    //            if (qty == 0)
    //            {
    //                Cmd.Parameters.AddWithValue("@Status", 0);
    //            }
    //            else
    //            {
    //                Cmd.Parameters.AddWithValue("@Status", 1);
    //            }

    //            Cmd.Parameters.AddWithValue("@InwardQTY", qty);
    //            Cmd.Parameters.AddWithValue("@OutwardQTY", "0");
    //            Cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
    //            Cmd.Parameters.AddWithValue("@RevertQty", txtoutwardqty.Text);
    //            Cmd.Parameters.AddWithValue("@OutwardBy", Session["UserCode"].ToString());
    //            Cmd.Parameters.AddWithValue("@OutwardDate", DateTime.Now);
    //            Cmd.ExecuteNonQuery();
    //            Cls_Main.Conn_Close();

    //            DataTable Dt = Cls_Main.Read_Table("SELECT TOP 1 * FROM tbl_ProductionDTLS AS PD where JobNo='" + txtjobno.Text + "'and StageNumber<1 order by StageNumber desc");
    //            if (Dt.Rows.Count > 0)
    //            {
    //                int StageNumber = Convert.ToInt32(Dt.Rows[0]["StageNumber"].ToString());
    //                Cls_Main.Conn_Open();
    //                SqlCommand Cmd1 = new SqlCommand("UPDATE [tbl_ProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
    //                Cmd1.Parameters.AddWithValue("@StageNumber", StageNumber);
    //                Cmd1.Parameters.AddWithValue("@JobNo", txtjobno.Text);
    //                Cmd1.Parameters.AddWithValue("@Status", 1);
    //                Cmd1.Parameters.AddWithValue("@OutwardQTY", qty);
    //                Cmd1.Parameters.AddWithValue("@Remark", txtRemarks.Text);
    //                Cmd1.Parameters.AddWithValue("@OutwardBy", Session["UserCode"].ToString());
    //                Cmd1.Parameters.AddWithValue("@OutwardDate", DateTime.Now);
    //                Cmd1.ExecuteNonQuery();
    //                Cls_Main.Conn_Close();
    //            }
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send Back..!!');window.location='PlazmaCutting.aspx';", true);
    //            FillGrid();

    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "HideLabelerror('Please fill data...........!!');", true);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle the exception

    //    }
    //}

    public void GetRemarks()
    {
        Cls_Main.Conn_Open();
        SqlCommand cmdselect = new SqlCommand("select Remark from  tbl_ProductionDTLS  WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
        cmdselect.Parameters.AddWithValue("@StageNumber", 0);
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
        cmd.Parameters.AddWithValue("@Thickness", txtThickness.Text);
        cmd.Parameters.AddWithValue("@Width", txtwidth.Text);
        cmd.Parameters.AddWithValue("@Length", txtlength.Text);
        cmd.Parameters.AddWithValue("@NeedSize", DBNull.Value);
        cmd.Parameters.AddWithValue("@AvailableQty", txtAvilableqty.Text);
        cmd.Parameters.AddWithValue("@AvailableSize", txtAvailablesize.Text);
        cmd.Parameters.AddWithValue("@RowMaterial", txtRMC.Text);
        cmd.Parameters.AddWithValue("@JobNo", hdnJobid.Value);
        cmd.Parameters.AddWithValue("@Weight", Txtweight.Text);
        cmd.Parameters.AddWithValue("@stages", 2);
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully..!!');window.location='PlazmaCutting.aspx';", true);
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Request Deleted Successfully..!!')", true);

        }

        if (e.CommandName == "Edit")
        {
            Response.Redirect("../Store/ReturnInventory.aspx?ID='" + e.CommandArgument.ToString() + "'");
        }
    }

    protected void GVRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public void GetRequestdata(string jobno)
    {
        //DataTable dtpt = Cls_Main.Read_Table("select * from tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 ");

        DataTable dtpt = Cls_Main.Read_Table("SELECT * FROM tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 AND stages=2");
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


    protected void txtThickness_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtwidth_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtlength_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtneedqty_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    public void Getdata()
    {
        try
        {
            DataTable dtpt = Cls_Main.Read_Table("select SUM(CAST(InwardQty AS FLOAT)) AS Quantity from tbl_InwardData WHERE RowMaterial='" + txtRMC.Text.Trim() + "' AND Thickness='" + txtThickness.Text.Trim() + "' AND Width='" + txtwidth.Text.Trim() + "' AND Length='" + txtlength.Text.Trim() + "' AND IsDeleted=0");
            if (dtpt.Rows.Count > 0)
            {
                txtAvilableqty.Text = dtpt.Rows[0]["Quantity"] != DBNull.Value ? dtpt.Rows[0]["Quantity"].ToString() : "0";

            }
            else
            {

            }
            if (txtThickness.Text != "" && txtwidth.Text != "" && txtlength.Text != "")
            {
                double thickness = Convert.ToDouble(txtThickness.Text);
                double width = Convert.ToDouble(txtwidth.Text);
                double length = Convert.ToDouble(txtlength.Text);
                double Quantity = string.IsNullOrEmpty(txtneedqty.Text) ? 0 : Convert.ToDouble(txtneedqty.Text);

                // Ensure inputs are non-negative
                if (thickness <= 0 || width <= 0 || length <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please enter positive values for thickness, width, and length...!!');", true);

                }

                // Calculate weight in kilograms
                double weight = length / 1000 * width / 1000 * thickness * 7.85;
                double totalweight = weight * Quantity;
                // Display the calculated weight
                Txtweight.Text = totalweight.ToString();
            }

        }
        catch { }
    }


    protected void GVRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
            Label lblRequestNo = e.Row.FindControl("lblRequestNo") as Label;
            DataTable dtpt = Cls_Main.Read_Table("select * from tbl_InventoryRequest where Status=2 AND RequestNo='" + lblRequestNo.Text + "'");
            if (dtpt.Rows.Count > 0)
            {
                gvDetails.DataSource = dtpt;
                gvDetails.DataBind();

            }
        }
    }
}


