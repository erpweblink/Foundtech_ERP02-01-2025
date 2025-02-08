using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_ProdListPerProjCode : System.Web.UI.Page
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
                    lblPageName.Text = Page;
                    int currentYear = 25;

                    for (int i = 0; i < 4; i++)
                    {
                        int year = currentYear + (i * 25);
                        DropDownList1.Items.Add(new ListItem(year.ToString(), year.ToString()));
                    }
                    DropDownList1.Items.Add(new ListItem("ALL", "ALL"));
                    DropDownList1.SelectedValue = currentYear.ToString();
                    FillGrid();
                }
            }
        }

    }
    private void showCount()
    {
        DataTable Dt = Cls_Main.Read_Table("SELECT Count(*) AS Count FROM [tbl_NewProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "'");
        if (Dt.Rows.Count > 0)
        {
            lblCount.Text = Dt.Rows[0]["Count"].ToString();
        }

        DataTable Dts = Cls_Main.Read_Table("SELECT CustomerName,ProjectName FROM [tbl_NewProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "'");
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
            DataTable Dt = Cls_Main.Read_Table("SELECT TOP " + DropDownList1.SelectedValue + " * FROM tbl_NewProductionDTLS  AS Pd " +
                " Inner Join tbl_NewOrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                " WHERE Pd.Stage = '" + Session["Stage"].ToString() + "' AND Pd.ProjectCode='" + Session["ProjectCode"].ToString() + "'");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }
        else
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_NewProductionDTLS  AS Pd" +
                " Inner Join tbl_NewOrderAcceptanceHdr AS OH on Pd.OANumber = OH.Pono " +
                " WHERE Pd.Stage = '" + Session["Stage"].ToString() + "' AND Pd.ProjectCode='" + Session["ProjectCode"].ToString() + "'");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }

    }

    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Rowwarehouse")
        {
            DivWarehouse.Visible = true;
           // divtable.Visible = false;

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
    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    public void GetRequestdata(string jobno)
    {
        DataTable dtpt = Cls_Main.Read_Table("SELECT * FROM tbl_InventoryRequest WHERE JobNo='" + jobno + "' AND IsDeleted=0 AND stages=1");
        if (dtpt.Rows.Count > 0)
        {
            GVRequest.DataSource = dtpt;
            GVRequest.DataBind();

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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable Dts = Cls_Main.Read_Table("SELECT Top 1 ProjectCode, Discription, Width, Length FROM tbl_NewProductionDTLS where JobNo='" + txtjobno.Text.Trim() + "'");

            if(Dts.Rows.Count >= 0)
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
                DataTable Dta = Cls_Main.Read_Table("select JobNo,OANumber,ProjectCode,Discription,Length,Width FROM tbl_NewProductionDTLS WHERE ProjectCode = '" + Dts.Rows[0]["ProjectCode"].ToString() + "' " +
                    " AND Discription = '" + Dts.Rows[0]["Discription"].ToString() + "' AND Width = '" + Width +"'" +
                    " AND Length = '" + Length + "' AND Stage = 'Drawing'");
                if(Dta.Rows.Count > 0)
                {
                    foreach (DataRow row in Dta.Rows)
                    {
                        string jobno = row["JobNo"].ToString();
                        string Oano = row["OANumber"].ToString();
                        string ProjCode = row["ProjectCode"].ToString();
                        string Discr = row["Discription"].ToString();
                        string Leng = row["Length"].ToString();
                        string Wid = row["Width"].ToString();
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
                                    cmd.Parameters.AddWithValue("@Leng", Leng);
                                    cmd.Parameters.AddWithValue("@Wid", Wid);
                                    // Execute the insert query
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // Close the connection (if not managed by Cls_Main)
                        Cls_Main.Conn_Close();
                    }

                }
                else
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

                }
               
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
               // Cmd.ExecuteNonQuery();
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
                  //Cmd1.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                }
            }

            string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
            string url = "ProdListPerProjCode.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully And Send to the Next..!!', '" + url + "');", true);
        }
        catch
        {

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

        string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        string url = "ProdListPerProjCode.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully..!!', '" + url + "');", true);
    }
    protected void btncancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("Drawing.aspx");
    }
    protected void GVRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {

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
    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void lblBtn_Click(object sender, EventArgs e)
    {
        string encryptedValue = objcls.encrypt(Session["ProjectCode"].ToString());
        Response.Redirect("DrawingListGPWise.aspx?ID=" + Session["Stage"].ToString() + "&EncryptedValue=" + encryptedValue);
    }
}