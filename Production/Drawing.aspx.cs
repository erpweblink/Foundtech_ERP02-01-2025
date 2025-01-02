
using DocumentFormat.OpenXml.Bibliography;
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


public partial class Production_Drawing : System.Web.UI.Page
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
                DivWarehouse.Visible = false;
            }
        }
    }

    //Fill GridView
    private void FillGrid()
    {

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionDTLS AS PD INNER JOIN tbl_ProductionHDR AS PH ON PH.JobNo=PD.JobNo where PD.Stage='Drawing' AND PD.Status<=2 ORDER BY  PD.ID DESC");
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
           // string InwardQty = ((Label)row.FindControl("InwardQty")).Text;
           // string OutwardQty = ((Label)row.FindControl("OutwardQty")).Text;
           // string RevertQty = ((Label)row.FindControl("RevertQty")).Text;
            string CustomerName = ((Label)row.FindControl("CustomerName")).Text;
            string JobNo = ((Label)row.FindControl("jobno")).Text;
            string Productname = ((Label)row.FindControl("Productname")).Text;
            txtcustomername.Text = CustomerName;
            txtProductname.Text = Productname;       
            txttotalqty.Text = Total_Price;
            txtjobno.Text = JobNo;
           // txtoutwardqty.Text = OutwardQty;
         
            this.ModalPopupHistory.Show();
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
                string CmdText = "select filePath from tbl_ProductionDTLS where JobNo='" + id + "' AND StageNumber='0'";

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(dt.Rows[0]["Path"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["filePath"].ToString()))
                    {
                        Response.Redirect("~/Drawings/" + dt.Rows[0]["filePath"].ToString());
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
            Cls_Main.Conn_Open();

            // Loop through the Request.Files to process the uploaded files
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFile file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    // Get the file name and save path
                    string fileName = Path.GetFileName(file.FileName);
                    string savePath = Server.MapPath("~/Drawings/" + fileName);

                    // Save the file
                    file.SaveAs(savePath);

                    // Get the corresponding remark for this file
                    string remark = string.Empty;
                    string remarkKey = string.Format("fileRemarks_{0}", i); // Generate the key for the remark
                    if (Request.Form.AllKeys.Contains(remarkKey))
                    {
                        remark = Request.Form[remarkKey];
                    }

                    // Insert file details and remark into the database
                    string insertQuery = "INSERT INTO tbl_DrawingDetails (JobNo, FileName,FilePath, Remark,CreatedBy,CreatedOn) VALUES (@JobNo, @FileName,@FilePath, @Remark,@CreatedBy,@CreatedOn)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, Cls_Main.Conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text.Trim());  // Ensure JobNo is correctly set
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@FilePath", savePath);
                        cmd.Parameters.AddWithValue("@Remark", remark);
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        // Execute the insert query
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Close the connection (if not managed by Cls_Main)
            Cls_Main.Conn_Close();

            Cls_Main.Conn_Open();
            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_ProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,InwardQTY=@InwardQTY,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
            Cmd.Parameters.AddWithValue("@StageNumber", 0);
            Cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
            Cmd.Parameters.AddWithValue("@InwardQTY", txttotalqty.Text);
            Cmd.Parameters.AddWithValue("@OutwardQTY", txtoutwardqty.Text);
            Cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
            if (txttotalqty.Text == txtoutwardqty.Text)
            {
                Cmd.Parameters.AddWithValue("@Status", 2);
            }
            else
            {
                Cmd.Parameters.AddWithValue("@Status", 1);
            }
            Cmd.Parameters.AddWithValue("@OutwardBy", Session["UserCode"].ToString());
            Cmd.Parameters.AddWithValue("@OutwardDate", DateTime.Now);
            Cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();

            DataTable Dt = Cls_Main.Read_Table("SELECT TOP 1 * FROM tbl_ProductionDTLS AS PD where JobNo='" + txtjobno.Text + "'and StageNumber>0 ");
            if (Dt.Rows.Count > 0)
            {
                int StageNumber = Convert.ToInt32(Dt.Rows[0]["StageNumber"].ToString());

                Cls_Main.Conn_Open();
                SqlCommand Cmd1 = new SqlCommand("UPDATE [tbl_ProductionDTLS] SET InwardQTY=@InwardQTY,InwardBy=@InwardBy,InwardDate=@InwardDate,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
                Cmd1.Parameters.AddWithValue("@StageNumber", StageNumber);
                Cmd1.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                Cmd1.Parameters.AddWithValue("@Status", 1);
                Cmd1.Parameters.AddWithValue("@InwardQTY", txttotalqty.Text);
                Cmd1.Parameters.AddWithValue("@InwardBy", Session["UserCode"].ToString());
                Cmd1.Parameters.AddWithValue("@InwardDate", DateTime.Now);
                Cmd1.ExecuteNonQuery();
                Cls_Main.Conn_Close();
            }           
            FillGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!');", true);
        }
        catch
        {

        }

    }

    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //int rowIndex = e.NewEditIndex;
        //GridViewRow row = GVPurchase.Rows[rowIndex];
        //string data = row.Cells[0].Text;
        //LinkButton btnwarrehouse = row.FindControl("btnwarrehouse") as LinkButton;
        //LinkButton btnEdit = row.FindControl("btnEdit") as LinkButton;
        //Label JobNo = (Label)row.FindControl("JobNo");
        //Label TotalQuantity = (Label)row.FindControl("Total_Price");
        //Label CustomerName = (Label)row.FindControl("CustomerName");

        //if (btnwarrehouse != null && btnwarrehouse.CommandName == "Rowwarehouse")
        //{
        //    // this.ModalPopupExtender1.Show();
        //}
        //if (btnEdit != null && btnEdit.CommandName == "Edit")
        //{
        //    txtcustomername.Text = CustomerName.Text;
        //    txtinwardqty.Text = TotalQuantity.Text;
        //    txttotalqty.Text = TotalQuantity.Text;
        //    txtjobno.Text = JobNo.Text;
        //    //this.ModalPopupHistory.Show();
        //}




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
        cmd.Parameters.AddWithValue("@stages", 1);
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully..!!');window.location='Drawing.aspx';", true);
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

    protected void btncancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Drawing.aspx");
    }

    protected void GVRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    public void GetRequestdata(string jobno)
    {
        DataTable dtpt = Cls_Main.Read_Table("SELECT * FROM tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 AND stages=1");
        //DataTable dtpt = Cls_Main.Read_Table("select * from tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 ");
        if (dtpt.Rows.Count > 0)
        {
            GVRequest.DataSource = dtpt;
            GVRequest.DataBind();

        }
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


}


