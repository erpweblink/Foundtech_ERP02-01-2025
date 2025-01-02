
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


public partial class Production_Dispatch : System.Web.UI.Page
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

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionDTLS AS PD INNER JOIN tbl_ProductionHDR AS PH ON PH.JobNo=PD.JobNo where PD.Stage='Dispatch' AND CONVERT(bigint,ISNULL(InwardQTY,0))>=CONVERT(bigint,ISNULL(OutwardQTY,0)) AND PD.Status<2");
        GVPurchase.DataSource = Dt;
        GVPurchase.DataBind();


    }



    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Rowwarehouse")
        {
            this.ModalPopupExtender1.Show();
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
                this.ModalPopupExtender2.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data not found..!!')", true);
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
                    cmd.Parameters.AddWithValue("@StageNumber", 6);
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
                    cmd.Parameters.AddWithValue("@StageNumber", 6);
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

}


