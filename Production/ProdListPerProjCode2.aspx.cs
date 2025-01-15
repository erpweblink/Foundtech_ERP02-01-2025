using DocumentFormat.OpenXml.Office2013.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_ProdListPerProjCode2 : System.Web.UI.Page
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
                if (Request.QueryString["ID"] != null)
                {
                    string Page = Request.QueryString["ID"].ToString();
                    string Id = objcls.Decrypt(Request.QueryString["EncryptedValue"].ToString());
                    Session["ProjectCode"] = Id;
                    Session["Stage"] = Page.ToUpper();
                    lblPageName.Text = Page;
                    int currentYear = 25;

                    for (int i = 0; i < 4; i++)
                    {
                        int year = currentYear + (i * 25);
                        DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem(year.ToString(), year.ToString()));
                    }
                    DropDownList1.Items.Add(new System.Web.UI.WebControls.ListItem("ALL", "ALL"));
                    DropDownList1.SelectedValue = currentYear.ToString();
                    FillGrid();
                }
            }
        }

    }
    private void showCount()
    {
        DataTable Dt = Cls_Main.Read_Table("SELECT Count(*) AS Count FROM [tbl_ProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "'");
        if (Dt.Rows.Count > 0)
        {
            lblCount.Text = Dt.Rows[0]["Count"].ToString();
        }

        DataTable Dts = Cls_Main.Read_Table("SELECT CustomerName,ProjectName FROM [tbl_ProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "'");
        if (Dts.Rows.Count > 0)
        {
            txtProjectCode.Text = Session["ProjectCode"].ToString();
            txtProjectName.Text = Dts.Rows[0]["ProjectName"].ToString();
            txtCustoName.Text = Dts.Rows[0]["CustomerName"].ToString();
        }
    }
    private void FillGrid()
    {
        showCount();
        lblPageName.Text = Session["Stage"].ToString();
        if (DropDownList1.SelectedValue != "ALL")
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT TOP " + DropDownList1.SelectedValue + " * FROM tbl_ProductionDTLS  AS Pd " +
                " Inner Join tbl_OrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                " WHERE Pd.Stage = '" + Session["Stage"].ToString() + "' AND Pd.ProjectCode='" + Session["ProjectCode"].ToString() + "'" +
                " ORDER BY PD.Status ASC ");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }
        else
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionDTLS  AS Pd" +
                " Inner Join tbl_OrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                " WHERE Pd.Stage = '" + Session["Stage"].ToString() + "' AND Pd.ProjectCode='" + Session["ProjectCode"].ToString() + "'" +
                " ORDER BY PD.Status ASC ");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }

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
            if (Session["Stage"].ToString() != "PLAZMACUTTING")
            {
                btnsendtoback.Visible = true;
            }
            else
            {
                btnsendtoback.Visible = false;
            }
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
                LinkButton btnWare = e.Row.FindControl("btnwarrehouse") as LinkButton;
                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btndtls = e.Row.FindControl("btnShowDtls") as LinkButton;

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

                if (Session["Stage"].ToString() != "PLAZMACUTTING")
                {
                    btnWare.Visible = false;
                    btnEdit.Style["margin-left"] = "28px";
                    btndtls.Visible = false;
                }
                else
                {
                    btnWare.Visible = true;
                }

                Label JobNo = e.Row.FindControl("JobNo") as Label;
                Label ProdName = e.Row.FindControl("Productname") as Label;
                if (JobNo != null)
                {
                    DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_DrawingDetails where JobNo='" + JobNo.Text + "'");

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
                            btndrawings.Enabled = false;
                        }
                    }
                }
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
    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
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
    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
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
                    int number = 0;
                    if (Session["Stage"].ToString() == "PLAZMACUTTING")
                    {
                        number = 1;
                    }else if(Session["Stage"].ToString() == "BENDING")
                    {
                        number = 2;
                    }
                     else if(Session["Stage"].ToString() == "FABRICATION")
                    {
                        number = 3;
                    }
                     else if(Session["Stage"].ToString() == "PAINTING")
                    {
                        number = 4;
                    }
                     else if(Session["Stage"].ToString() == "QUALITY")
                    {
                        number = 5;
                    }
                     else if(Session["Stage"].ToString() == "DISPATCH")
                    {
                        number = 6;
                    }
                   
                    Cls_Main.Conn_Open();
                    SqlCommand cmd = new SqlCommand("DB_Foundtech.ManageProductionDetails", Cls_Main.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "UpdateSendToNext");
                    cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                    cmd.Parameters.AddWithValue("@StageNumber", number);
                    cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(txtinwardqty.Text));
                    cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(txtoutwardqty.Text));
                    cmd.Parameters.AddWithValue("@PendingQty", Convert.ToDouble(txtpending.Text));
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
                    cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                    Cls_Main.Conn_Dispose();

                    string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
                    string url = "ProdListPerProjCode2.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!', '" + url + "');", true);
                   
                }
                else
                {
                    string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
                    string url = "ProdListPerProjCode2.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please check Outward Quantity is Greater then Inward Quantity..!!', '" + url + "');", true);
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
                    int number = 0;
                    if (Session["Stage"].ToString() == "PLAZMACUTTING")
                    {
                        number = 1;
                    }
                    else if (Session["Stage"].ToString() == "BENDING")
                    {
                        number = 2;
                    }
                    else if (Session["Stage"].ToString() == "FABRICATION")
                    {
                        number = 3;
                    }
                    else if (Session["Stage"].ToString() == "PAINTING")
                    {
                        number = 4;
                    }
                    else if (Session["Stage"].ToString() == "QUALITY")
                    {
                        number = 5;
                    }
                    else if (Session["Stage"].ToString() == "DISPATCH")
                    {
                        number = 6;
                    }

                    Cls_Main.Conn_Open();
                    SqlCommand cmd = new SqlCommand("DB_Foundtech.ManageProductionDetails", Cls_Main.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "UpdateSendToBack");
                    cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                    cmd.Parameters.AddWithValue("@StageNumber", number);
                    cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(txtinwardqty.Text));
                    cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(txtoutwardqty.Text));
                    cmd.Parameters.AddWithValue("@PendingQty", Convert.ToDouble(txtpending.Text));
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
                    cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                    Cls_Main.Conn_Dispose();

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
                string CmdText = "select FileName from tbl_DrawingDetails where Id='" + id + "'";

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

            }
        }
    }

    protected void LinkButtonTrash_Click(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument).ToString();

        DataTable Dt = Cls_Main.Read_Table("DELETE tbl_DrawingDetails WHERE Id='" + id + "'");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Image deleted successfully..!!');", true);

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

        GetRequestdata(hdnJobid.Value);
        //string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        //string url = "ProdListPerProjCode.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully..!!', '" + url + "');", true);

    }

    protected void btncancle_Click(object sender, EventArgs e)
    {
        string Page = Session["Stage"].ToString();
        string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        Response.Redirect("ProdListPerProjCode2.aspx?ID=" + Page + "&EncryptedValue=" + encryptedValue);
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
            DataTable dtpt = Cls_Main.Read_Table("select SUM(CAST(InwardQty AS FLOAT)) AS Quantity from tbl_InwardData WHERE RowMaterial='" + txtRMC.Text.Trim() + "' AND TRY_CAST(Thickness AS FLOAT)='" + txtThickness.Text.Trim() + "' AND TRY_CAST(Width AS FLOAT)='" + txtwidth.Text.Trim() + "' AND TRY_CAST(Length AS FLOAT)='" + txtlength.Text.Trim() + "' AND IsDeleted=0");
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

    protected void lblBtn_Click(object sender, EventArgs e)
    {
        string pageName = Session["Stage"].ToString();
        if (pageName == "QUALITY")
        {
            pageName = "Packing";
        }
        Response.Redirect(pageName + ".aspx");
    }

}