using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Production_DispatchPage : System.Web.UI.Page
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

        }
    }

    private void RefreshPage()
    {
        string url = Request.Url.ToString();

        Response.Redirect(Request.Url.ToString());
    }

    private void FillGrid()
    {
        DataTable dt = Cls_Main.Read_Table(" SELECT OANumber,Stage,RowMaterial,RawMateReqQTY,(Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))) AS ReceivedQty, " +
            " (Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)) AS SentQTy," +
            " ((Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))-(Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int))))AS RemainingQTy, " +
            " ProjectCode,ProjectName,Count(ID) AS JobCounts, TRY_CAST(SUM(CAST(TotalQTY AS INT)) AS INT) AS TotalQuantity, " +

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
            " WHERE ProjectCode = '" + Session["ProjectCode"].ToString() + "' and Stage='" + Session["Stage"].ToString() + "' " +
            " GROUP BY OANumber, Stage, RowMaterial, RawMateReqQTY, RawMateRemainingReqQty, ProjectCode, ProjectName " +
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
                Label lblRemainingSet = e.Row.FindControl("lblRemainReqQty") as Label;

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

            string TotalSet = ((Label)row.FindControl("lblReqQty")).Text;
            string InwardSet = ((Label)row.FindControl("lblRemainReqQty")).Text;
            string RemainreqQty = ((Label)row.FindControl("lblRemainReqQty")).Text;
            string JobNoList = ((Label)row.FindControl("lblJobNoList")).Text;

            txtSetQty.Text = TotalSet;
            txtReqQuantity.Text = InwardSet;
            txtJobNos.Text = JobNoList;

            this.ModalPopupExtender1.Show();
        }

    }


    //Send  Next step production
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string url = Request.Url.ToString();
            string[] jobNos = txtJobNos.Text.Split(',');

            if (txtEnteredQty.Text != null && txtEnteredQty.Text != "")
            {
                int InwardSet = Convert.ToInt32(txtReqQuantity.Text);
                int EnteredQtny = Convert.ToInt32(txtEnteredQty.Text);
                if (InwardSet >= EnteredQtny)
                {
                    foreach (var item in jobNos)
                    {
                        Cls_Main.Conn_Open();
                        SqlCommand cmd = new SqlCommand("DB_Foundtech.QualityManageProductionDetails", Cls_Main.Conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Mode", "UpdateSendToNext");
                        cmd.Parameters.AddWithValue("@JobNo", item);
                        cmd.Parameters.AddWithValue("@StageNumber", 6);

                        int FinalQty = 0;
                        int totalQty = 0;
                        int RemainRequQty = 0;
                        DataTable dt = Cls_Main.Read_Table("Select * from tbl_NewProductionDtls where JobNo ='" + item + "' and StageNumber = 6");
                        if (dt.Rows.Count > 0)
                        {
                            totalQty = Convert.ToInt32(dt.Rows[0]["TotalQTY"].ToString());
                            int InwardQty = Convert.ToInt32(dt.Rows[0]["TotalQty"].ToString());
                            int ReqQty = Convert.ToInt32(dt.Rows[0]["RawMateReqQTY"].ToString());
                            RemainRequQty = Convert.ToInt32(dt.Rows[0]["rawmateremainingreqqty"].ToString());

                            int subQty = InwardQty / ReqQty;
                            for (int i = 0; i < EnteredQtny; i++)
                            {
                                FinalQty += subQty;
                            }

                        }

                        cmd.Parameters.AddWithValue("@InwardQty", Convert.ToDouble(totalQty));
                        cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToDouble(FinalQty));
                        cmd.Parameters.AddWithValue("@PendingQty", "0");
                        cmd.Parameters.AddWithValue("@RemainingSentQty", RemainRequQty - EnteredQtny);
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

    protected void txtEnteredQty_TextChanged(object sender, EventArgs e)
    {
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

    protected void GroupRecords_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }


    public void GetExcellDataCustomerWise()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand(" SELECT OANumber, Stage, RowMaterial, RawMateReqQTY, (Sum(CAST(InwardQty as int)) / (SUM(CAST(TotalQTY AS INT)) / Cast(RawMateReqQTY as int))) AS ReceivedQty, " +
            " (Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)) AS SentQTy," +
            " ((Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))-(Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int))))AS RemainingQTy, " +
            " ProjectCode,ProjectName,Count(ID) AS JobCounts, TRY_CAST(SUM(CAST(TotalQTY AS INT)) AS INT) AS TotalQuantity, " +

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
            " WHERE ProjectCode = '" + Session["ProjectCode"].ToString() + "' and Stage='" + Session["Stage"].ToString() + "' " +
            " GROUP BY OANumber, Stage, RowMaterial, RawMateReqQTY, RawMateRemainingReqQty, ProjectCode, ProjectName " +
            " order by SentQTy desc, RemainingQTy desc", con))
            {
                DataTable Dt = new DataTable();
                con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dt.Columns.Add("ProductStatus");
                    GroupRecords.DataSource = dt;
                    GroupRecords.DataBind();
                    con.Close();
                    Response.Clear();
                    System.DateTime now = System.DateTime.Today;
                    string filename = dt.Rows[0]["ProjectCode"].ToString() + " Report " + now.ToString("dd/MM/yyyy");
                    Response.AddHeader("content-disposition", "attachment; filename = '" + filename + "'.xls");
                    Response.ContentType = "application/vnd.xls";
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    GroupRecords.RenderControl(htmlWrite);
                    Response.Write(stringWrite.ToString());
                    Response.Flush();
                }
            }

        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
       // GetExcellDataCustomerWise();
        Report();
    }


    public void Report()
    {
        try
        {
            using (SqlCommand cmd = new SqlCommand(" SELECT RowMaterial AS ProductName, RawMateReqQTY AS TotalSet, (Sum(CAST(InwardQty as int)) / (SUM(CAST(TotalQTY AS INT)) / Cast(RawMateReqQTY as int))) AS InwardSet, " +
             " (Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)) AS OutwardSet," +
             " ((Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))-(Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int))))AS RemainingSet, " +
             " ProjectCode, Case When RawMateReqQTY = (Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)) then 'Completed' "+
             " When ((Sum(CAST(InwardQty as int))/(SUM(CAST(TotalQTY AS INT)) /Cast(RawMateReqQTY as int))-(Cast(RawMateReqQTY as Int)-Cast(RawMateRemainingReqQty as int)))) != 0 then 'In-Process' "+
             " else 'Pending' end ProductStatus " +

             " FROM tbl_NewProductionDTLS AS t1 " +
             " WHERE ProjectCode = '" + Session["ProjectCode"].ToString() + "' and Stage='" + Session["Stage"].ToString() + "' " +
             " GROUP BY RowMaterial, RawMateReqQTY, RawMateRemainingReqQty, ProjectCode, ProjectName " +
             " order by OutwardSet desc ", con))
            {
                DataTable Dt = new DataTable();
                con.Open();
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    ReportDataSource obj1 = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(obj1);
                    ReportViewer1.LocalReport.ReportPath = "RDLC_Reports\\DispatchReport.rdlc";
                    ReportViewer1.LocalReport.Refresh();

                    //-------- Print PDF directly without showing ReportViewer ----
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    byte[] bytePdfRep = ReportViewer1.LocalReport.Render("EXCEL", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment; filename="+dt.Rows[0]["ProjectCode"].ToString()+"_Reports.xls");


                    Response.BinaryWrite(bytePdfRep);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();


                }
            }
        }
        catch (Exception ex)
        {

        }
    }

}