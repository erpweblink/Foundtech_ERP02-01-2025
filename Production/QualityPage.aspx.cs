using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;

public partial class Production_QualityPage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    bool allCompleted = true;

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
                    Session["Stage"] = Page;
                    //lblPageName.Text = Page;
                    FillGrid();
                    ViewState["TempDataGrid"] = "";
                }
            }
            if (Request["__EVENTTARGET"] == btnhist.ClientID)
            {
                RefreshPage();
            }
        }
    }

    private void RefreshPage()
    {
        string url = Request.Url.ToString();

        Response.Redirect(Request.Url.ToString());
    }

    private void FillGrid()
    {
        DataTable dt = Cls_Main.Read_Table(" SELECT  t7.FilePath,t1.OANumber,t1.Stage,RowMaterial,RawMateReqQTY,RawMateRemainingReqQty, " +
            " (Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))) AS ReceivedQty, " +
            " (Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)) AS SentQTy, " +
            " ((Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))-(Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int))))AS RemainingQTy, " +
            " t1.ProjectCode, t1.ProjectName,Count( t1.ID) AS JobCounts, " +
            " TRY_CAST(SUM(CAST(TotalQTY AS INT)) AS INT) AS TotalQuantity, " +

            " STUFF((SELECT ',' + CAST(JobNo AS NVARCHAR) " +
            " FROM tbl_NewProductionDTLS t2 " +
            " WHERE t2.RowMaterial = t1.RowMaterial  And t2.Stage = t1.Stage And t2.OANumber = t1.OANumber " +
            " FOR XML PATH('')), 1, 1, '') AS JobNoList, " +

            " STUFF((SELECT ',' + CAST(TotalQTY AS NVARCHAR) " +
            " FROM tbl_NewProductionDTLS AS t3 " +
            " WHERE t3.RowMaterial = t1.RowMaterial And t3.Stage = t1.Stage And t3.OANumber = t1.OANumber " +
            " FOR XML PATH('')), 1, 1, '') AS TotalQTYlist, " +

            " STUFF((SELECT ',' + CAST(InwardQTY AS NVARCHAR) " +
            " FROM tbl_NewProductionDTLS AS t4 " +
            " WHERE t4.RowMaterial = t1.RowMaterial And t4.Stage = t1.Stage And t4.OANumber = t1.OANumber " +
            " FOR XML PATH('')), 1, 1, '') AS InwardQTYlist, " +

            " STUFF((SELECT ',' + CAST(Discription AS NVARCHAR) " +
            " FROM tbl_NewProductionDTLS t5 " +
            " WHERE  t5.RowMaterial = t1.RowMaterial And t5.Stage = t1.Stage And t5.OANumber = t1.OANumber " +
            " FOR XML PATH('')), 1, 1, '') AS Disc " +

            " FROM tbl_NewProductionDTLS AS t1 " +
            " LEFT JOIN tbl_NewProductionHDR AS t7 ON t7.ProductName = t1.RowMaterial " +
            " WHERE  t1.ProjectCode = '" + Session["ProjectCode"].ToString() + "' and  t1.Stage='" + Session["Stage"].ToString() + "' " +
            " GROUP BY t1.OANumber, t1.Stage, RowMaterial,RawMateReqQTY,RawMateRemainingReqQty, t1.ProjectCode,t1.ProjectName,t7.FilePath  " +
            " order by SentQTy desc, RemainingQTy desc ");

        dt.Columns.Add("ProductStatus");

        GroupRecords.DataSource = dt;
        GroupRecords.DataBind();
    }

    protected void GroupRecords_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblReqQty = e.Row.FindControl("lblReqQty") as Label;
                Label lblSentQty = e.Row.FindControl("lblSentQty") as Label;
                Label lblProdStatus = e.Row.FindControl("lblProdStatus") as Label;
                Label lblRemainingSet = e.Row.FindControl("lblRemainingSet") as Label;

                if (lblReqQty != null || lblSentQty != null)
                {

                    if (lblReqQty.Text == lblSentQty.Text)
                    {
                        lblProdStatus.Text = "Completed";
                        lblProdStatus.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (lblRemainingSet.Text != "0")
                    {
                        lblProdStatus.Text = "In-Process";
                        lblProdStatus.ForeColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                        lblProdStatus.Text = "Pending";
                        lblProdStatus.ForeColor = System.Drawing.Color.Red;
                    }
                }
                LinkButton btnPdfFile = e.Row.FindControl("btnPdfFile") as LinkButton;
                Label lblFilePath = e.Row.FindControl("lblFilePath") as Label;
                if (lblFilePath.Text != "")
                {
                    btnPdfFile.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    btnPdfFile.ForeColor = System.Drawing.Color.Black;
                    btnPdfFile.Enabled = false;
                }

            }

        }

        catch
        {
        }
    }

    protected void GroupRecords_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PdfDownload")
        {
            string fileName = Path.GetFileName(e.CommandArgument.ToString());
            Response.Redirect("~/PDF_Files/" + fileName);
        }
        if (e.CommandName == "ViewDetails")
        {
            string rowIndex = e.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

            string TotalQty = ((Label)row.FindControl("lblTotalQuantity")).Text;
            string reqQty = ((Label)row.FindControl("lblReqQty")).Text;
            string RemainreqQty = ((Label)row.FindControl("lblRemainReqQty")).Text;
            string JobNoList = ((Label)row.FindControl("lblJobNoList")).Text;
            string QtyList = ((Label)row.FindControl("lblTotalQTYlist")).Text;
            string DiscList = ((Label)row.FindControl("lblDisclist")).Text;
            string InwardQtyList = ((Label)row.FindControl("lblInwardQtylist")).Text;

            int divVal = Convert.ToInt32(TotalQty) / Convert.ToInt32(reqQty);


            txtRequestedQty.Text = reqQty;
            txtRemainRequeQty.Text = RemainreqQty;
            txtinwardqty.Text = divVal.ToString();
            txttotalqty.Text = TotalQty;
            txtJobList.Text = JobNoList;
            txtoutwardqty.Text = TotalQty;

            // txtpending.Text = "0";
            txtoutwardqty.Text = TotalQty;

            string[] jobsNo = JobNoList.Split(',');
            string[] Qty = QtyList.Split(',');
            string[] Disc = DiscList.Split(',');
            string[] InwardQty = InwardQtyList.Split(',');
            DataTable Dt = new DataTable();
            Dt.Columns.AddRange(new DataColumn[7] { new DataColumn("Checkbox"), new DataColumn("JobNo"), new DataColumn("Qty"), new DataColumn("Discr"), new DataColumn("InwardQTYlist"), new DataColumn("PerSetQty"), new DataColumn("Status") });
            int rowCount = jobsNo.Length > Qty.Length ? jobsNo.Length : Qty.Length;

            for (int i = 0; i < rowCount; i++)
            {
                string jobNo = jobsNo.Length > i ? jobsNo[i] : string.Empty;
                string qty = Qty.Length > i ? Qty[i] : string.Empty;
                string disc = Disc.Length > i ? Disc[i] : string.Empty;
                string inwardQty = InwardQty.Length > i ? InwardQty[i] : string.Empty;

                int val = Convert.ToInt32(qty) / Convert.ToInt32(reqQty);

                Dt.Rows.Add("", jobNo, qty, disc, inwardQty, val.ToString(), "");
            }

            if (Dt.Rows.Count > 0)
            {
                grdgrid.DataSource = Dt;
                grdgrid.DataBind();

                btnSendtopro.Visible = allCompleted;
                ViewState["TempDataGrid"] = Dt;
            }
            AttachmentID.Visible = true;

            if (RemainreqQty == "0")
            {
                btnSendtopro.Visible = false;
                btnsendtoback.Visible = false;
            }

            this.ModalPopupHistory.Show();
        }

    }


    //Send  Next step production
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString();
            string[] jobNos = txtJobNos.Text.Split(',');
            if (AttachmentUpload.HasFile)
            {
                string fileName = Path.GetFileName(AttachmentUpload.PostedFile.FileName);
                byte[] fileContent;
                using (Stream fs = AttachmentUpload.PostedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        fileContent = br.ReadBytes((int)fs.Length);
                    }
                }

                lblfile1.Text = fileName;
                string[] pdffilename = lblfile1.Text.Split('.');
                string pdffilename1 = pdffilename[0];
                string filenameExt = pdffilename[1];

                string UniqueID = GenerateUniqueEncryptedValue();
                string FileName = UniqueID + "_" + pdffilename1;
                string filePath = Server.MapPath("~/PDF_Files/") + FileName + "." + filenameExt;

                // Save the file to the specified path
                System.IO.File.WriteAllBytes(filePath, fileContent);



                foreach (var item in jobNos)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(" UPDATE tbl_NewProductionHDR SET FilePath = @filePath,FileAddedBy = @createdby," +
                        " FileAddedDate = @createdon WHERE JobNo = @jobNo ", con);
                    cmd.Parameters.AddWithValue("@jobNo", item);
                    cmd.Parameters.AddWithValue("@filePath", filePath);
                    cmd.Parameters.AddWithValue("@createdby", Session["usercode"].ToString());
                    cmd.Parameters.AddWithValue("@createdon", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            if (txtEnteredQty.Text != null && txtEnteredQty.Text != "")
            {
                int TotalQtny = Convert.ToInt32(txtRemainRequeQty.Text);
                int EnteredQtny = Convert.ToInt32(txtEnteredQty.Text);
                if (TotalQtny >= EnteredQtny)
                {
                    foreach (var item in jobNos)
                    {
                        Cls_Main.Conn_Open();
                        SqlCommand cmd = new SqlCommand("DB_Foundtech.QualityManageProductionDetails", Cls_Main.Conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Mode", "UpdateSendToNext");
                        cmd.Parameters.AddWithValue("@JobNo", item);
                        cmd.Parameters.AddWithValue("@StageNumber", 5);

                        int FinalQty = 0;
                        int totalQty = 0;
                        DataTable dt = Cls_Main.Read_Table("Select * from tbl_NewProductionDtls where JobNo ='" + item + "' and StageNumber = 5");
                        if (dt.Rows.Count > 0)
                        {
                            totalQty = Convert.ToInt32(dt.Rows[0]["TotalQTY"].ToString());
                            int InwardQty = Convert.ToInt32(dt.Rows[0]["InwardQTY"].ToString());
                            int ReqQty = Convert.ToInt32(dt.Rows[0]["RawMateReqQTY"].ToString());

                            int subQty = InwardQty / ReqQty;
                            for (int i = 0; i < EnteredQtny; i++)
                            {
                                FinalQty += subQty;
                            }

                        }

                        cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(totalQty));
                        cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(FinalQty));
                        cmd.Parameters.AddWithValue("@PendingQty", "0");
                        cmd.Parameters.AddWithValue("@RemainingSentQty", TotalQtny - EnteredQtny);
                        cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                        cmd.Parameters.AddWithValue("@UserCode", Session["UserCode"].ToString());
                        cmd.ExecuteNonQuery();
                        Cls_Main.Conn_Close();
                        Cls_Main.Conn_Dispose();

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!', '" + url + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please check Outward Quantity is Greater then Inward Quantity..!!', '" + url + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please fill data...........!!', '" + url + "');", true);
            }
        }
        catch
        {

        }
    }

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
                    //  cmd.Parameters.AddWithValue("@JobNo", txtjobno.Text);
                    cmd.Parameters.AddWithValue("@StageNumber", 5);
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

            }
        }
    }


    protected void GroupRecords_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void lblCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSelect = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chkSelect.NamingContainer;

        Label lblQty = (Label)row.FindControl("lblQty");
        Label Desc = (Label)row.FindControl("lblDiscr");

        if (chkSelect.Checked)
        {
            int val = Convert.ToInt32(txtpending.Text) + Convert.ToInt32(lblQty.Text);
            txtpending.Text = val.ToString();
        }
        else
        {
            int val = Convert.ToInt32(txtpending.Text) - Convert.ToInt32(lblQty.Text);
            txtpending.Text = val.ToString();
        }
        this.ModalPopupHistory.Show();
    }

    protected void grdgrid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grdgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = e.Row.FindControl("lblStatus") as Label;
                Label lblQty = e.Row.FindControl("lblQty") as Label;
                Label lblInwardQty = e.Row.FindControl("lblInwardQty") as Label;
                CheckBox checkBox = e.Row.FindControl("lblCheckBox") as CheckBox;
                checkBox.Enabled = false;
                if (lblInwardQty != null)
                {
                    int InwardQty = Convert.ToInt32(lblInwardQty.Text);
                    int Qty = Convert.ToInt32(lblQty.Text);

                    // Update the status text based on the status code
                    if (InwardQty == Qty)
                    {
                        lblStatus.Text = "Completed";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        checkBox.Checked = true;
                    }
                    else
                    {
                        lblStatus.Text = "In-Process";
                        lblStatus.ForeColor = System.Drawing.Color.Orange;
                    }
                }

                if (lblStatus.Text != "Completed")
                {
                    allCompleted = false;
                }

            }
        }
        catch
        {
        }
    }

    public void SendModalPopUP(object sender, EventArgs e)
    {
        this.ModalPopupHistory.Show();
        txtReqQuantity.Text = txtRemainRequeQty.Text;
        if (txtReqQuantity.Text == "1")
        {
            txtEnteredQty.Text = txtReqQuantity.Text;
            txtEnteredQty.ReadOnly = true;
        }
        txtJobNos.Text = txtJobList.Text;
        this.ModalPopupExtender1.Show();
    }
    public void BackModalPopUP(object sender, EventArgs e)
    {
        this.ModalPopupHistory.Show();
        DataTable Dt = ViewState["TempDataGrid"] as DataTable;
        if (Dt.Rows.Count > 0)
        {
            GridSendBack.DataSource = Dt;
            GridSendBack.DataBind();
        }

        txtReqQuantity.Text = txtRemainRequeQty.Text;
        txtJobNos.Text = txtJobList.Text;
        this.ModalPopupExtender2.Show();
    }

    protected void txtEnteredQty_TextChanged(object sender, EventArgs e)
    {
        this.ModalPopupHistory.Show();
        TextBox txt = (TextBox)sender;
        if (txt.Text != "")
        {
            int enteredText = Convert.ToInt32(txt.Text);
            if (enteredText > Convert.ToInt32(txtReqQuantity.Text))
            {
                txt.Text = "";
            }
            if (enteredText <= 0)
            {
                txt.Text = "";
            }
        }

        this.ModalPopupExtender1.Show();
    }

    protected void GridSendBack_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void GridSendBack_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = e.Row.FindControl("lblStatus1") as Label;
                Label lblQty = e.Row.FindControl("lblQty1") as Label;
                Label lblInwardQty = e.Row.FindControl("lblInwardQty1") as Label;
                CheckBox checkBox = e.Row.FindControl("lblCheckBox1") as CheckBox;
                checkBox.Enabled = false;
                if (lblInwardQty != null)
                {
                    int InwardQty = Convert.ToInt32(lblInwardQty.Text);
                    int Qty = Convert.ToInt32(lblQty.Text);

                    // Update the status text based on the status code
                    if (InwardQty == Qty)
                    {
                        lblStatus.Text = "Completed";
                        lblStatus.ForeColor = System.Drawing.Color.Green;
                        checkBox.Checked = true;
                    }
                    else
                    {
                        lblStatus.Text = "In-Process";
                        lblStatus.ForeColor = System.Drawing.Color.Orange;
                    }
                }

                if (lblStatus.Text != "Completed")
                {
                    allCompleted = false;
                }

            }
        }
        catch
        {
        }

    }

    public static string GenerateUniqueEncryptedValue()
    {
        string uniqueString = Guid.NewGuid().ToString() + "_" + DateTime.UtcNow.Ticks.ToString();

        byte[] bytes = Encoding.UTF8.GetBytes(uniqueString);

        using (SHA256 sha256 = SHA256.Create())
        {
            // Compute the hash of the byte array
            byte[] hashBytes = sha256.ComputeHash(bytes);

            // Convert the hash bytes into a hex string
            StringBuilder hexString = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hexString.AppendFormat("{0:x2}", b);
            }

            return hexString.ToString();
        }
    }

}