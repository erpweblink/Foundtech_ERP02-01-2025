
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Activities.Expressions;
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

        DataTable Dt = Cls_Main.Read_Table("SELECT OH.PdfFilePath, PD.ProjectCode, PD.ProjectName, PH.CustomerName,COUNT(*) AS TotalRecords, " +
        " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY,SUM(CAST(OutwardQty AS INT)) AS OutwardQty " +
        " FROM[tbl_NewProductionDTLS] AS PD " +
        " INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
        " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode WHERE PD.Stage = 'PlazmaCutting' AND PD.Status < 2 " +
        " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName, OH.PdfFilePath " +
        " ORDER BY PD.ProjectCode desc ");
        MainGridLoad.DataSource = Dt;
        MainGridLoad.DataBind();

    }


    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Rowwarehouse")
        {

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

           // txtoutwardqty.Text = txtoutwardqty.Text;
            txtpending.Text = (A - B).ToString();
            txtoutwardqty.Text = txtpending.Text;
            this.ModalPopupHistory.Show();
        }
        if (e.CommandName == "DrawingFiles")
        {
            string rowIndex = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");

            string JobNo = ((Label)row.FindControl("jobno")).Text;
            string ProdName = ((Label)row.FindControl("ProductName")).Text;
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_DrawingDetails AS PD where JobNo='" + JobNo + "'");
            if (Dt.Rows.Count > 0)
            {
                rptImages.DataSource = Dt;
                rptImages.DataBind();
                lblJobNumb.Text = JobNo;
                lblProdName.Text = ProdName;
                this.ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Data not found..!!')", true);
            }

        }
        if (e.CommandName == "ViewDetails")
        {
            Response.Redirect("SubProducts.aspx?Id=" + objcls.encrypt(e.CommandArgument.ToString()) + "");
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

                Label JobNo = e.Row.FindControl("JobNo") as Label;

                if (JobNo != null)
                {
                    DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_DrawingDetails AS PD where JobNo='" + JobNo.Text + "'");

                    LinkButton btndrawings = e.Row.FindControl("btndrawings") as LinkButton;

                    if (btndrawings != null)
                    {

                        if (Dt.Rows.Count > 0)
                        {
                            btndrawings.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            btndrawings.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }

                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btnwarrehouse = e.Row.FindControl("btnwarrehouse") as LinkButton;

                if (btnEdit != null)
                {
                    string empcode = Session["UserCode"].ToString();
                    DataTable Dt = new DataTable();
                    SqlDataAdapter Sd = new SqlDataAdapter("Select ID from tbl_UserMaster where UserCode='" + empcode + "'", con);
                    Sd.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        string id = Dt.Rows[0]["ID"].ToString();
                        DataTable Dtt = new DataTable();
                        SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'PlazmaCutting.aspx' AND PagesView = '1'", con);
                        Sdd.Fill(Dtt);
                        if (Dtt.Rows.Count > 0)
                        {
                            btnEdit.Enabled = false;
                            btnwarrehouse.Enabled = false;
                        }
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
                    string fileName = dt.Rows[0]["FileName"].ToString();
                    string fileExtension = Path.GetExtension(fileName);

                     if (fileExtension == ".pdf")
 {
     //Old Code 
     Response.Redirect("~/Drawings/" + dt.Rows[0]["FileName"].ToString());
 }
 else
 {
     //New Code by Nikhil 04-01-2025
     string filePath = Server.MapPath("~/Drawings/" + fileName);

     if (File.Exists(filePath))
     {
         byte[] fileBytes = File.ReadAllBytes(filePath);
         string base64File = Convert.ToBase64String(fileBytes);
         string safeBase64File = base64File.Replace("'", @"\'");
         string script = "downloadDWGFile('" + safeBase64File + "', '" + fileName + "');";
         ScriptManager.RegisterStartupScript(this, this.GetType(), "DownloadDWG", script, true);

     }
 }
                }
                else
                {
                    //lblnotfound.Text = "File Not Found or Not Available !!";
                }

                // To convert the dwg file in .png by Nikhil 04-01-2025

                //string filePath = Server.MapPath("~/Drawings/" + fileName);

                //// Path where the converted PNG image will be stored
                //string imagePath = "~/Images/" + Path.GetFileNameWithoutExtension(fileName) + ".png";
                //string fullImagePath = Server.MapPath(imagePath);

                //// Check if the image already exists to avoid re-conversion
                //if (!File.Exists(fullImagePath))
                //{
                //    string convertCommand = "convert \"" + filePath + "\" \"" + fullImagePath + "\""; // Concatenate file paths correctly

                //    // Setup the process to run ImageMagick
                //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                //    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //    startInfo.FileName = "convert"; // ImageMagick command
                //    startInfo.Arguments = "\"" + filePath + "\" \"" + fullImagePath + "\"";

                //    try
                //    {
                //        // Start the process to convert the file
                //        System.Diagnostics.Process.Start(startInfo);

                //        // Optionally, wait for the process to finish before redirecting (if required)
                //        System.Threading.Thread.Sleep(2000); // Sleep for 2 seconds to ensure the image is created before redirecting


                //        string iframeSrc = "~/Images/" + Path.GetFileNameWithoutExtension(fileName) + ".png";
                //        ClientScript.RegisterStartupScript(this.GetType(), "setIframeSrc", "document.getElementById('yourIframeID').src = '" + iframeSrc + "';", true);

                //        this.ModalPopupExtender1.Show();

                //        Response.Redirect(imagePath);
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                // End Code 

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
    //            SqlCommand Cmd2 = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET  RevertQty= @RevertQty WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
    //            Cmd2.Parameters.AddWithValue("@StageNumber", 0);
    //            Cmd2.Parameters.AddWithValue("@JobNo", txtjobno.Text);
    //            Cmd2.Parameters.AddWithValue("@RevertQty", txtoutwardqty.Text);
    //            Cmd2.ExecuteNonQuery();
    //            Cls_Main.Conn_Close();

    //            Double qty = Convert.ToDouble(txtinwardqty.Text) - Convert.ToDouble(txtoutwardqty.Text);
    //            Cls_Main.Conn_Open();
    //            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,InwardQTY=@InwardQTY,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
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

    //            DataTable Dt = Cls_Main.Read_Table("SELECT TOP 1 * FROM tbl_NewProductionDTLS AS PD where JobNo='" + txtjobno.Text + "'and StageNumber<1 order by StageNumber desc");
    //            if (Dt.Rows.Count > 0)
    //            {
    //                int StageNumber = Convert.ToInt32(Dt.Rows[0]["StageNumber"].ToString());
    //                Cls_Main.Conn_Open();
    //                SqlCommand Cmd1 = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
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
        SqlCommand cmdselect = new SqlCommand("select Remark from  tbl_NewProductionDTLS  WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
        cmdselect.Parameters.AddWithValue("@StageNumber", 0);
        cmdselect.Parameters.AddWithValue("@JobNo", txtjobno.Text);
        Object Remarks = cmdselect.ExecuteScalar();
        Cls_Main.Conn_Close();
        if (Remarks != null)
        {
            txtRemarks.Text = Remarks.ToString();
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
        DataTable dtpt = Cls_Main.Read_Table("SELECT * FROM tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 AND stages=2");
        if (dtpt.Rows.Count > 0)
        {
            GVRequest.DataSource = dtpt;
            GVRequest.DataBind();

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
                //Label ProjectCode = e.Row.FindControl("lblProjectCode") as Label;
                //GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                //if (GVPurchase == null)
                //{

                //    return;
                //}

                //if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                //{
                //    var data = GetData(string.Format("SELECT * FROM tbl_NewProductionDTLS  AS Pd" +
                //        " Inner Join tbl_NewOrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                //        " WHERE Pd.Stage = 'PlazmaCutting' AND Pd.ProjectCode='{0}'", ProjectCode.Text));
                //    if (data != null && data.Rows.Count > 0)
                //    {
                //        GVPurchase.DataSource = data;
                //        GVPurchase.DataBind();
                //    }
                //    else
                //    {
                //        GVPurchase.Visible = false;
                //    }
                //}

                Label JobNo = e.Row.FindControl("lblProjectCode") as Label;

                if (JobNo != null)
                {
                    DataTable Dts = Cls_Main.Read_Table("SELECT PdfFilePath FROM tbl_NewOrderAcceptanceHdr  where ProjectCode ='" + JobNo.Text + "'");

                    LinkButton btndrawings = e.Row.FindControl("btnPdfFile") as LinkButton;

                    if (btndrawings != null)
                    {

                        if (Dts.Rows.Count > 0)
                        {
                            string fileName = Dts.Rows[0]["PdfFilePath"].ToString();

                            if (fileName != "")
                            {
                                btndrawings.ForeColor = System.Drawing.Color.Blue;
                            }
                            else
                            {
                                btndrawings.ForeColor = System.Drawing.Color.Red;
                                btndrawings.Enabled = false;
                            }
                        }
                        else
                        {
                            btndrawings.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                }

            }
        }
        catch
        {
            throw;
        }

    }


    // Search Filters added by Nikhil 04-01-2025

    //Search Company Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
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
                com.CommandText = "SELECT DISTINCT [ID],[Companyname] FROM [tbl_CompanyMaster] where Companyname like  @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["Companyname"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        if (txtCustName.Text != "" || txtCustName.Text != null)
        {
            string company = txtCustName.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT OH.PdfFilePath,PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY ,SUM(CAST(OutwardQty AS INT)) AS OutwardQty  FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode Where PD.Stage = 'PlazmaCutting' AND PD.Status < 2 AND PH.CustomerName = '" + company + "' " +
            " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName,OH.PdfFilePath " +
            " ORDER BY PD.ProjectCode desc  ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PlazmaCutting.aspx");
    }

    //Search OA.  Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCponoList(string prefixText, int count)
    {
        return AutoFillCponoName(prefixText);
    }

    public static List<string> AutoFillCponoName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT Distinct(ProjectCode) AS Code FROM [tbl_NewProductionHDR] where ProjectCode like @Search +'%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["Code"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }
    protected void txtjobno_TextChanged(object sender, EventArgs e)
    {
        if (txtProjCode.Text != "" || txtProjCode.Text != null)
        {
            string Cpono = txtProjCode.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter(" SELECT OH.PdfFilePath,PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY ,SUM(CAST(OutwardQty AS INT)) AS OutwardQty  FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode Where PD.Stage = 'PlazmaCutting' AND PD.Status < 2 AND PH.ProjectCode = '" + Cpono + "' " +
            " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName,OH.PdfFilePath " +
            " ORDER BY PD.ProjectCode desc  ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
        }
    }

    //Search GST WIse Company methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetGSTList(string prefixText, int count)
    {
        return AutoFillGSTName(prefixText);
    }

    public static List<string> AutoFillGSTName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT DISTINCT ProjectName FROM [tbl_NewProductionHDR] where ProjectName like @Search +'%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["ProjectName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtGST_TextChanged(object sender, EventArgs e)
    {
        if (txtGST.Text != "" || txtGST.Text != null)
        {
            string GST = txtGST.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter(" SELECT OH.PdfFilePath,PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY ,SUM(CAST(OutwardQty AS INT)) AS OutwardQty FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode Where PD.Stage = 'PlazmaCutting' AND PD.Status < 2 AND PH.ProjectName = '" + GST + "' " +
            " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName,OH.PdfFilePath " +
            " ORDER BY PD.ProjectCode desc ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
        }
    }

    protected void MainGridLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PdfDownload")
        {
            string fileName = Path.GetFileName(e.CommandArgument.ToString());
            Response.Redirect("~/PDF_Files/" + fileName);
        }
        if (e.CommandName == "ViewDetails")
        {
            string Page = "PlazmaCutting";
            string encryptedValue = objcls.encrypt(e.CommandArgument.ToString());
            Response.Redirect("ProdListPerProjCode2.aspx?ID=" + Page + "&EncryptedValue=" + encryptedValue);
        }
    }
}


