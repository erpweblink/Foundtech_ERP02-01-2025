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

public partial class Production_DrawingDetails : System.Web.UI.Page
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

        DataTable Dt = Cls_Main.Read_Table("SELECT OH.PdfFilePath, PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY " +
            " FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode Where PD.Stage = 'Drawing' " +
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
            string CustomerName = ((Label)row.FindControl("CustomerName")).Text;
            string JobNo = ((Label)row.FindControl("jobno")).Text;
            string Productname = ((Label)row.FindControl("Productname")).Text;

            DataTable Dt = Cls_Main.Read_Table("SELECT id, FileName FROM tbl_DrawingDetails where JobNo='" + JobNo + "'");

            if (Dt.Rows.Count > 0)
            {
                grdgrid.DataSource = Dt;
                grdgrid.DataBind();
            }
            txtcustomername.Text = CustomerName;
            txtProductname.Text = Productname;
            txttotalqty.Text = Total_Price;
            txtjobno.Text = JobNo;

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
        // GVPurchase.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;

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
                        SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'DrawingDetails.aspx' AND PagesView = '1'", con);
                        Sdd.Fill(Dtt);
                        if (Dtt.Rows.Count > 0)
                        {
                            btnEdit.Enabled = false;
                        }
                    }
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
            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET OutwardQTY=@OutwardQTY,OutwardBy=@OutwardBy,OutwardDate=@OutwardDate,Remark=@Remark,InwardQTY=@InwardQTY,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
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

            DataTable Dt = Cls_Main.Read_Table("SELECT TOP 1 * FROM tbl_NewProductionDTLS AS PD where JobNo='" + txtjobno.Text + "'and StageNumber>0 ");
            if (Dt.Rows.Count > 0)
            {
                int StageNumber = Convert.ToInt32(Dt.Rows[0]["StageNumber"].ToString());

                Cls_Main.Conn_Open();
                SqlCommand Cmd1 = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET InwardQTY=@InwardQTY,InwardBy=@InwardBy,InwardDate=@InwardDate,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
                Cmd1.Parameters.AddWithValue("@StageNumber", StageNumber);
                Cmd1.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                Cmd1.Parameters.AddWithValue("@Status", 1);
                Cmd1.Parameters.AddWithValue("@InwardQTY", txttotalqty.Text);
                Cmd1.Parameters.AddWithValue("@InwardBy", Session["UserCode"].ToString());
                Cmd1.Parameters.AddWithValue("@InwardDate", DateTime.Now);
                Cmd1.ExecuteNonQuery();
                Cls_Main.Conn_Close();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!');", true);
        }
        catch
        {

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

    protected void MainGridLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ProjectCode = e.Row.FindControl("lblProjectCode") as Label;
                //GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                //if (GVPurchase == null)
                //{

                //    return;
                //}

                //if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                //{
                //    var data = GetData(string.Format("SELECT * FROM tbl_NewProductionDTLS  AS Pd" +
                //        " Inner Join tbl_NewOrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                //        " WHERE Pd.Stage = 'Drawing' AND Pd.ProjectCode='{0}'", ProjectCode.Text));
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
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY  FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode " +
            " Where PD.Stage = 'Drawing' AND PH.CustomerName = '" + company + "' " +
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
        Response.Redirect("DrawingDetails.aspx");
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
            SqlDataAdapter sad = new SqlDataAdapter(" SELECT OH.PdfFilePath,PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, "+
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY  FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode" +
            " Where PD.Stage = 'Drawing' AND PH.ProjectCode = '" + Cpono + "' " +
            " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName,OH.PdfFilePath  " +
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
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY  FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo " +
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode " +
            " Where PD.Stage = 'Drawing' AND PH.ProjectName = '" + GST + "' " +
            " GROUP BY PD.ProjectCode, PD.ProjectName, PH.CustomerName,OH.PdfFilePath " +
            " ORDER BY PD.ProjectCode desc ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
        }
    }

    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void LinkButtonTrash_Click(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument).ToString();

        DataTable Dt = Cls_Main.Read_Table("DELETE tbl_DrawingDetails WHERE Id='" + id + "'");

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Image deleted successfully..!!');", true);

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
            string Page = "Drawing";
            string encryptedValue = objcls.encrypt(e.CommandArgument.ToString());
            Response.Redirect("DrawingListGPWise.aspx?ID=" + Page + "&EncryptedValue=" + encryptedValue);
        }
    }
}