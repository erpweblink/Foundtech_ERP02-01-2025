
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
        DataTable Dt = Cls_Main.Read_Table("SELECT OH.PdfFilePath, PD.ProjectCode, PD.ProjectName, PH.CustomerName, COUNT(*) AS TotalRecords, " +
                       " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY,SUM(CAST(OutwardQty AS INT)) AS OutwardQty " +
                       " FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo=PD.JobNo " +
                       " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode Where PD.Stage = 'Dispatch' and PD.Status < 2 " +
                       " GROUP BY  PD.ProjectCode, PD.ProjectName, PH.CustomerName, OH.PdfFilePath " +
                       " ORDER BY PD.ProjectCode desc ");
        MainGridLoad.DataSource = Dt;
        MainGridLoad.DataBind();
    }

    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Rowwarehouse")
        {
            this.ModalPopupExtender1.Show();
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
                        SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'Dispatch.aspx' AND PagesView = '1'", con);
                        Sdd.Fill(Dtt);
                        if (Dtt.Rows.Count > 0)
                        {
                            btnEdit.Enabled = false;
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
        SqlCommand cmdselect = new SqlCommand("select Remark from  tbl_NewProductionDTLS  WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
        cmdselect.Parameters.AddWithValue("@StageNumber", 4);
        cmdselect.Parameters.AddWithValue("@JobNo", txtjobno.Text);
        Object Remarks = cmdselect.ExecuteScalar();
        Cls_Main.Conn_Close();
        if (Remarks != null)
        {
            txtRemarks.Text = Remarks.ToString();
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
                //GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                //if (GVPurchase == null)
                //{

                //    return;
                //}

                //if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                //{
                //    var data = GetData(string.Format("SELECT * FROM tbl_NewProductionDTLS  AS Pd" +
                //        " Inner Join tbl_NewOrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                //        " WHERE Pd.Stage = 'Dispatch' AND Pd.ProjectCode='{0}'", ProjectCode.Text));
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
            " SUM(CAST(TotalQTY AS INT)) AS TotalQTY,SUM(CAST(InwardQTY AS INT)) AS InwardQTY ,SUM(CAST(OutwardQty AS INT)) AS OutwardQty "+
            " FROM tbl_NewProductionDTLS AS PD INNER JOIN tbl_NewProductionHDR AS PH ON PH.JobNo = PD.JobNo "+
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode "+
            " Where PD.Stage = 'Dispatch' AND PD.Status < 2 AND PH.CustomerName = '" + company + "' " +
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
        Response.Redirect("Dispatch.aspx");
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
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode " +
            " Where PD.Stage = 'Dispatch' AND PD.Status < 2 AND PH.ProjectCode = '" + Cpono + "' " +
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
            " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON OH.ProjectCode = PD.ProjectCode " +
            " Where PD.Stage = 'Dispatch' AND PD.Status < 2 AND PH.ProjectName = '" + GST + "' " +
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
            string Page = "Dispatch";
            string encryptedValue = objcls.encrypt(e.CommandArgument.ToString());
            Response.Redirect("DispatchPage.aspx?ID=" + Page + "&EncryptedValue=" + encryptedValue);
        }
    }
}


