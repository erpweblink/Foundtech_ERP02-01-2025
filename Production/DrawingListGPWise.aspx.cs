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

public partial class Production_DrawingListGPWise : System.Web.UI.Page
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
                    //hideJobCode.Value = Id;
                    Session["ProjectCode"] = Id;
                    Session["Stage"] = Page.ToUpper();
                    FillGrid();
                }
            }
            if (Request["__EVENTTARGET"] == btnhist.ClientID)
            {
                RefreshPage();
            }
        }

    }



    private void FillGrid()
    {
        DataTable Dt = Cls_Main.Read_Table(" SELECT Discription, Length, Width, Stage, t1.Status, Count(JobNo) AS JobCounts " +
           " ,STUFF((SELECT ',' + CAST(JobNo AS NVARCHAR) FROM tbl_NewProductionDTLS t2 WHERE t2.Discription = t1.Discription " +
           " AND t2.Length = t1.Length AND t2.Width = t1.Width AND t2.Stage = t1.Stage FOR XML PATH('')), 1, 1, '') AS JobNoList, " +
           " Sum(Cast(TotalQTY as int)) AS TotalQty " +
           " FROM tbl_NewProductionDTLS AS t1 " +
           " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON t1.OANumber = OH.Pono " +
           " WHERE t1.Stage = '" + Session["Stage"].ToString() + "' AND t1.ProjectCode = '" + Session["ProjectCode"].ToString() + "' " +
           " GROUP BY Discription,Length,Width,Stage,t1.Status " +
           " ORDER BY t1.Status desc ");

        GVPurchase.DataSource = Dt;
        GVPurchase.DataBind();


    }

    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            string rowIndex = e.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");


            string lblDiscription = ((Label)row.FindControl("lblDiscription")).Text;
            string lblLength = ((Label)row.FindControl("lblLength")).Text;
            string lblWidth = ((Label)row.FindControl("lblWidth")).Text;
            string lblTotalQty = ((Label)row.FindControl("lblTotalQty")).Text;
            string lblJobNoList = ((Label)row.FindControl("lblJobNoList")).Text;

            DataTable Dt = Cls_Main.Read_Table("SELECT FilePath,Remark,FileName,MAX(Id) AS ID " +
                    " FROM tbl_DrawingDetails where Discription='" + lblDiscription + "' AND Length = '" + lblLength + "' AND Width = '" + lblWidth + "' " +
                    " Group by FilePath,Remark,FileName ");

            if (Dt.Rows.Count > 0)
            {
                grdgrid.DataSource = Dt;
                grdgrid.DataBind();
            }
            txtcustomername.Text = lblDiscription;
            txtProductname.Text = lblLength;
            txttotalqty.Text = lblWidth;
            txtjobno.Text = lblTotalQty;
            txtoutwardqty.Text = lblJobNoList;

            this.ModalPopupHistory.Show();
        }
        if (e.CommandName == "DrawingFiles")
        {
            string rowIndex = e.CommandArgument.ToString();
            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVPurchase");

            //string JobNo = ((Label)row.FindControl("jobno")).Text;
            //string ProdName = ((Label)row.FindControl("ProductName")).Text;
            string lblDiscription = ((Label)row.FindControl("lblDiscription")).Text;
            string lblLength = ((Label)row.FindControl("lblLength")).Text;
            string lblWidth = ((Label)row.FindControl("lblWidth")).Text;
            if (lblDiscription != null && lblLength != null && lblWidth != null)
            {
                DataTable Dt = Cls_Main.Read_Table("SELECT FilePath,Remark,FileName,MAX(Id) AS ID " +
                    " FROM tbl_DrawingDetails where Discription='" + lblDiscription + "' AND Length = '" + lblLength + "' AND Width = '" + lblWidth + "' " +
                    " Group by FilePath,Remark,FileName ");

                rptImages.DataSource = Dt;
                rptImages.DataBind();
                lblJobNumb.Text = lblDiscription;
                lblProdName.Text = lblLength;
                lblWid.Text = lblWidth;
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


                Label lblDiscription = e.Row.FindControl("lblDiscription") as Label;
                Label lblLength = e.Row.FindControl("lblLength") as Label;
                Label lblWidth = e.Row.FindControl("lblWidth") as Label;
                if (lblDiscription != null && lblLength != null && lblWidth != null)
                {
                    DataTable Dt = Cls_Main.Read_Table("SELECT FilePath,Remark,FileName,MAX(Id) AS ID " +
                        " FROM tbl_DrawingDetails where Discription='" + lblDiscription.Text + "' AND Length = '" + lblLength.Text + "' AND Width = '" + lblWidth.Text + "' " +
                        " Group by FilePath,Remark,FileName ");

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

                Label Status = e.Row.FindControl("lblProdStatus") as Label;

                if (Status != null)
                {

                    if (Status.Text == "2")
                    {
                        Status.Text = "Completed";
                        Status.ForeColor = System.Drawing.Color.Green;
                    }
                    else if (Status.Text != "1")
                    {
                        Status.Text = "In-Process";
                        Status.ForeColor = System.Drawing.Color.Orange;
                    }
                    else
                    {
                        Status.Text = "Pending";
                        Status.ForeColor = System.Drawing.Color.Red;
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
                string CmdText = "select FilePath from tbl_DrawingDetails where Id='" + id + "'";

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string fileName = Path.GetFileName(dt.Rows[0]["FilePath"].ToString());
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
        DataTable Dts = Cls_Main.Read_Table("SELECT FilePath,FileName FROM tbl_DrawingDetails WHERE Id='" + id + "'");

        if (Dts.Rows.Count > 0)
        {
            DataTable Dt = Cls_Main.Read_Table("DELETE tbl_DrawingDetails WHERE FileName='" + Dts.Rows[0]["FileName"].ToString() + "' AND FilePath = '" + Dts.Rows[0]["FilePath"].ToString() + "'");
            string path = Server.MapPath("~/Drawings/" + Dts.Rows[0]["FileName"].ToString());
            File.Delete(path);
        }

        string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        string url = "DrawingListGPWise.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Image deleted successfully..!!', '" + url + "');", true);

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string[] jobNos = txtoutwardqty.Text.Split(',');
            string UniqueID = GenerateUniqueEncryptedValue();
            foreach (var item in jobNos)
            {
                DataTable Dts = Cls_Main.Read_Table("SELECT *,ProjectCode, Discription, Width, Length FROM tbl_NewProductionDTLS where JobNo='" + item + "' AND Stage = 'Drawing'");

                if (Dts.Rows.Count >= 0)
                {
                    string Width = Dts.Rows[0]["Width"].ToString();
                    string Length = Dts.Rows[0]["Length"].ToString();
                    if (Width == "")
                    {
                        Width = "0.000";
                    }
                    if (Length == "")
                    {
                        Length = "0.000";
                    }
                    string jobno = Dts.Rows[0]["JobNo"].ToString();
                    string Oano = Dts.Rows[0]["OANumber"].ToString();
                    string ProjCode = Dts.Rows[0]["ProjectCode"].ToString();
                    string Discr = Dts.Rows[0]["Discription"].ToString();
                    Cls_Main.Conn_Open();

                    // Loop through the Request.Files to process the uploaded files
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFile file = Request.Files[i];
                        if (file != null && file.ContentLength > 0)
                        {
                            // Get the file name and save path
                            string fileName = Path.GetFileName(file.FileName);
                            string FileName = UniqueID + "_" + fileName;

                            string savePath = Server.MapPath("~/Drawings/" + FileName);

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
                            string insertQuery = "INSERT INTO tbl_DrawingDetails (JobNo, FileName,FilePath, Remark,CreatedBy,CreatedOn,OaNumber,ProjectCode,Discription,Length,Width) " +
                                " VALUES (@JobNo, @FileName,@FilePath, @Remark,@CreatedBy,@CreatedOn,@oano,@projCode,@Discr,@Leng,@Wid)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, Cls_Main.Conn))
                            {
                                // Add parameters to prevent SQL injection
                                cmd.Parameters.AddWithValue("@JobNo", jobno);  // Ensure JobNo is correctly set
                                cmd.Parameters.AddWithValue("@FileName", fileName);
                                cmd.Parameters.AddWithValue("@FilePath", savePath);
                                cmd.Parameters.AddWithValue("@Remark", remark);
                                cmd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                                cmd.Parameters.AddWithValue("@oano", Oano);
                                cmd.Parameters.AddWithValue("@projCode", ProjCode);
                                cmd.Parameters.AddWithValue("@Discr", Discr);
                                cmd.Parameters.AddWithValue("@Leng", Length);
                                cmd.Parameters.AddWithValue("@Wid", Width);
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
                    Cmd.Parameters.AddWithValue("@JobNo", Dts.Rows[0]["JobNo"].ToString());
                    Cmd.Parameters.AddWithValue("@InwardQTY", Dts.Rows[0]["TotalQty"].ToString());
                    Cmd.Parameters.AddWithValue("@OutwardQTY", Dts.Rows[0]["TotalQty"].ToString());
                    Cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text);
                    Cmd.Parameters.AddWithValue("@Status", 2);
                    Cmd.Parameters.AddWithValue("@OutwardBy", Session["UserCode"].ToString());
                    Cmd.Parameters.AddWithValue("@OutwardDate", DateTime.Now);
                    Cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();

                    DataTable Dt = Cls_Main.Read_Table("SELECT TOP 1 * FROM tbl_NewProductionDTLS AS PD where JobNo='" + Dts.Rows[0]["JobNo"].ToString() + "'and StageNumber>0 ");
                    if (Dt.Rows.Count > 0)
                    {
                        int StageNumber = Convert.ToInt32(Dt.Rows[0]["StageNumber"].ToString());

                        Cls_Main.Conn_Open();
                        SqlCommand Cmd1 = new SqlCommand("UPDATE [tbl_NewProductionDTLS] SET InwardQTY=@InwardQTY,InwardBy=@InwardBy,InwardDate=@InwardDate,Status=@Status WHERE StageNumber=@StageNumber AND JobNo=@JobNo", Cls_Main.Conn);
                        Cmd1.Parameters.AddWithValue("@StageNumber", StageNumber);
                        Cmd1.Parameters.AddWithValue("@JobNo", Dts.Rows[0]["JobNo"].ToString());
                        Cmd1.Parameters.AddWithValue("@Status", 1);
                        Cmd1.Parameters.AddWithValue("@InwardQTY", Dts.Rows[0]["TotalQty"].ToString());
                        Cmd1.Parameters.AddWithValue("@InwardBy", Session["UserCode"].ToString());
                        Cmd1.Parameters.AddWithValue("@InwardDate", DateTime.Now);
                        Cmd1.ExecuteNonQuery();
                        Cls_Main.Conn_Close();
                    }
                }
            }
            string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
            string url = "DrawingListGPWise.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!', '" + url + "');", true);
        }
        catch
        {

        }

    }

    protected void btncancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Drawing.aspx");
    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDiscription(string prefixText, int count)
    {
        return AutoFillGetDiscription(prefixText);
    }

    public static List<string> AutoFillGetDiscription(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = " select DISTINCT Discription from tbl_NewProductionDTLS " +
                   " where Discription like @Search + '%' AND Stage = '" + HttpContext.Current.Session["Stage"].ToString() + "' AND ProjectCode = '" + HttpContext.Current.Session["ProjectCode"].ToString() + "'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Discription = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Discription.Add(sdr["Discription"].ToString());
                    }
                }
                con.Close();
                return Discription;
            }

        }
    }

    protected void txtSerachDisc_TextChanged(object sender, EventArgs e)
    {
        if (txtSerachDisc.Text != "")
        {
            string Cpono = txtSerachDisc.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter(" SELECT Discription, Length, Width, Stage, t1.Status, Count(JobNo) AS JobCounts " +
           " ,STUFF((SELECT ',' + CAST(JobNo AS NVARCHAR) FROM tbl_NewProductionDTLS t2 WHERE t2.Discription = t1.Discription " +
           " AND t2.Length = t1.Length AND t2.Width = t1.Width AND t2.Stage = t1.Stage FOR XML PATH('')), 1, 1, '') AS JobNoList, " +
           " Sum(Cast(TotalQTY as int)) AS TotalQty " +
           " FROM tbl_NewProductionDTLS AS t1 " +
           " INNER JOIN tbl_NewOrderAcceptanceHdr AS OH ON t1.OANumber = OH.Pono " +
           " WHERE t1.Stage = '" + Session["Stage"].ToString() + "' AND t1.ProjectCode = '" + Session["ProjectCode"].ToString() + "' AND t1.Discription = '" + Cpono + "' " +
           " GROUP BY Discription,Length,Width,Stage,t1.Status ", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void btnDetails_Click(object sender, EventArgs e)
    {
        string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        Response.Redirect("ProdListPerProjCode.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue);
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

    private void RefreshPage()
    {
        string url = Request.Url.ToString();

        Response.Redirect(Request.Url.ToString());
    }
}