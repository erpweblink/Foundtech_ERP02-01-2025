using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SalesMarketing_OAList : System.Web.UI.Page
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


    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderAcceptance.aspx");
    }

    //Fill GridView
    private void FillGrid()
    {
        if (Session["Role"].ToString() == "Admin")
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE CP.IsDeleted = 0 ORDER BY CP.ID DESC");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }
        else
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE (CP.CreatedBy='" + Session["UserCode"].ToString() + "' OR CP.UserName='" + Session["UserCode"].ToString() + "') AND  CP.IsDeleted = 0 ORDER BY CP.ID DESC");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }

    }


    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            Response.Redirect("OrderAcceptance.aspx?Id=" + objcls.encrypt(e.CommandArgument.ToString()) + "");
        }
        if (e.CommandName == "Sendtoproduction")
        {
            SendtoProduction(Convert.ToInt32(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "RowDelete")
        {
            Cls_Main.Conn_Open();

            DataTable Dtt = new DataTable();
            SqlDataAdapter Sdd = new SqlDataAdapter("SELECT * FROM tbl_NewProductionDTLS WHERE Oanumber='" + e.CommandArgument.ToString() + "'", con);
            Sdd.Fill(Dtt);
            if (Dtt.Rows.Count == 0)
            {

                SqlCommand Cmd = new SqlCommand("DELETE [tbl_NewOrderAcceptanceHdr] WHERE pono=@ID", Cls_Main.Conn);
                Cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
                Cmd.ExecuteNonQuery();

                SqlCommand Cmd1 = new SqlCommand("DELETE [tbl_NewOrderAcceptanceDtls] WHERE pono=@ID", Cls_Main.Conn);
                Cmd1.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
                Cmd1.ExecuteNonQuery();

                SqlCommand Cmd2 = new SqlCommand("DELETE [tbl_NewProductionHDR] WHERE oanumber=@ID", Cls_Main.Conn);
                Cmd2.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
                Cmd2.ExecuteNonQuery();

                //SqlCommand Cmd = new SqlCommand("UPDATE [tbl_NewOrderAcceptanceHdr] SET IsDeleted=@IsDeleted,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID", Cls_Main.Conn);
                //Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument.ToString()));
                //Cmd.Parameters.AddWithValue("@IsDeleted", '1');
                //Cmd.Parameters.AddWithValue("@DeletedBy", Session["UserCode"].ToString());
                //Cmd.Parameters.AddWithValue("@DeletedOn", DateTime.Now);
                //  Cmd.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Acceptance Deleted Successfully..!!')", true);
                FillGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You cannot delete the records it is already in production..!!')", true);
            }

            Cls_Main.Conn_Close();
        }
        if (e.CommandName == "RowView")
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[SP_NewGetSubProductsByProjectCode]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Pono", e.CommandArgument.ToString());
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        ViewState["FinalExcel"] = dt;

                        connection.Close();

                        Response.Clear();
                        DateTime now = DateTime.Today;
                        string filename = "ProjectDetails_" + now.ToString("dd-MM-yyyy");
                        Response.AddHeader("content-disposition", "attachment; filename=" + filename + ".xls");
                        Response.ContentType = "application/vnd.ms-excel";

                        StringWriter stringWrite = new StringWriter();
                        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

                        htmlWrite.Write("<table border='1' cellpadding='5' cellspacing='0'>");

                        htmlWrite.Write("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            htmlWrite.Write("<th style='background-color: orange; color: white; text-align: center; vertical-align: middle;'>");
                            htmlWrite.Write(column.ColumnName);
                            htmlWrite.Write("</th>");
                        }
                        htmlWrite.Write("</tr>");

                        foreach (DataRow row in dt.Rows)
                        {
                            htmlWrite.Write("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                htmlWrite.Write("<td style='text-align: center; vertical-align: middle;'>");
                                htmlWrite.Write(row[column].ToString());
                                htmlWrite.Write("</td>");
                            }
                            htmlWrite.Write("</tr>");
                        }

                        htmlWrite.Write("</table>");

                        Response.Write(stringWrite.ToString());
                        Response.End();
                    }
                }
            }
            // Response.Redirect("Pdf_CustomerPurchase.aspx?Pono=" + objcls.encrypt(e.CommandArgument.ToString()) + " ");
            // Response.Write("<script>window.open ('Pdf_Quotation.aspx?Quotationno=" + (e.CommandArgument.ToString()) + "','_blank');</script>");
        }
        if (e.CommandName == "PdfFiles")
        {
            string fileName = Path.GetFileName(e.CommandArgument.ToString());
            Response.Redirect("~/PDF_Files/" + fileName);
        }
        if(e.CommandName == "RowClose")
        {
            Cls_Main.Conn_Open();
            SqlCommand Cmd = new SqlCommand("DELETE [tbl_NewOrderAcceptanceHdr] WHERE pono=@ID", Cls_Main.Conn);
            Cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
            Cmd.ExecuteNonQuery();

            SqlCommand Cmd1 = new SqlCommand("DELETE [tbl_NewOrderAcceptanceDtls] WHERE pono=@ID", Cls_Main.Conn);
            Cmd1.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
            Cmd1.ExecuteNonQuery();

            SqlCommand Cmd2 = new SqlCommand("DELETE [tbl_NewProductionHDR] WHERE oanumber=@ID", Cls_Main.Conn);
            Cmd2.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
            Cmd2.ExecuteNonQuery();

           SqlCommand Cmd3 = new SqlCommand("DELETE [tbl_NewProductionDTLS] WHERE oanumber=@ID", Cls_Main.Conn);
            Cmd3.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
            Cmd3.ExecuteNonQuery();

            Cls_Main.Conn_Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Closed Successfully..!!')", true);
            FillGrid();
        }
    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVPurchase.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Authorization
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
            LinkButton btnClose = e.Row.FindControl("btnCancel") as LinkButton;

            Label JobNo = e.Row.FindControl("Pono") as Label;

            if (JobNo != null)
            {
                DataTable Dts = Cls_Main.Read_Table("SELECT PdfFilePath FROM tbl_NewOrderAcceptanceHdr  where Pono ='" + JobNo.Text + "'");

                LinkButton btndrawings = e.Row.FindControl("btnPdf") as LinkButton;

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
            DataTable Dtas = new DataTable();
            SqlDataAdapter Sda = new SqlDataAdapter("Select * from tbl_NewProductionDTLS where Oanumber='" + JobNo.Text + "'", con);
            Sda.Fill(Dtas);
            if (Dtas.Rows.Count > 0)
            {
                btnClose.Visible = true;
            }


                string empcode = Session["UserCode"].ToString();
            DataTable Dt = new DataTable();
            SqlDataAdapter Sd = new SqlDataAdapter("Select ID from tbl_UserMaster where UserCode='" + empcode + "'", con);
            Sd.Fill(Dt);
            if (Dt.Rows.Count > 0)
            {
                string id = Dt.Rows[0]["ID"].ToString();
                DataTable Dtt = new DataTable();
                SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'OAList.aspx' AND PagesView = '1'", con);
                Sdd.Fill(Dtt);
                if (Dtt.Rows.Count > 0)
                {
                    btnCreate.Visible = false;
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }


        }
    }

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
                com.CommandText = "SELECT DISTINCT CustomerName FROM [tbl_NewOrderAcceptanceHdr] where " + "CustomerName like @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["CustomerName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        if (txtCustomerName.Text != "" || txtCustomerName.Text != null)
        {
            string company = txtCustomerName.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE CP.IsDeleted = 0 AND CustomerName='" + txtCustomerName.Text + "' ORDER BY CP.ID DESC", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("OAList.aspx");
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
                com.CommandText = "SELECT * FROM [tbl_NewOrderAcceptanceHdr] where " + "ProjectCode like @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["ProjectCode"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }
    protected void txtCpono_TextChanged(object sender, EventArgs e)
    {
        if (txtCpono.Text != "" || txtCpono.Text != null)
        {
            string Cpono = txtCpono.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT Distinct * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName Where ProjectCode='" + Cpono + "' ORDER BY CP.ID DESC", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(txtCpono.Text) && string.IsNullOrEmpty(txtfromdate.Text) && string.IsNullOrEmpty(txttodate.Text))
            {
                FillGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Search Record');", true);
            }
            else
            {
                if (Session["Role"].ToString() == "Admin")
                {
                    if (txtCpono.Text != "")
                    {
                        string Quono = txtCpono.Text;
                        DataTable dt = new DataTable();
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName where ProjectCode = '" + Quono + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
                        sad.Fill(dt);
                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }
                    if (txtCustomerName.Text != "")
                    {
                        string company = txtCustomerName.Text;

                        DataTable dt = new DataTable();
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName where CustomerName = '" + company + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
                        sad.Fill(dt);
                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }

                    if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        DataTable dt = new DataTable();

                        //SqlDataAdapter sad = new SqlDataAdapter(" select [Id],[JobNo],[DateIn],[CustName],[Subcustomer],[Branch],[MateName],[SrNo],[MateStatus],FinalStatus,[TestBy],[ModelNo],[otherinfo],[Imagepath],[CreatedBy],[CreatedDate],[UpdateBy],[UpdateDate] ,ProductFault,RepeatedNo,DATEDIFF(DAY, CreatedDate, getdate()) AS days FROM [tblInwardEntry] Where DateIn between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", Cls_Main.Conn);
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE  CP.IsDeleted = 0 AND CP.CreatedOn between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", Cls_Main.Conn);
                        sad.Fill(dt);

                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }

                }
                else
                {
                    if (txtCpono.Text != "")
                    {
                        string Quono = txtCpono.Text;

                        DataTable dt = new DataTable();
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE (CP.CreatedBy='" + Session["UserCode"].ToString() + "' OR CP.UserName='" + Session["UserCode"].ToString() + "') AND  ProjectCode = '" + Quono + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
                        sad.Fill(dt);
                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }
                    if (txtCustomerName.Text != "")
                    {
                        string company = txtCustomerName.Text;

                        DataTable dt = new DataTable();
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE (CP.CreatedBy='" + Session["UserCode"].ToString() + "' OR CP.UserName='" + Session["UserCode"].ToString() + "') AND  CustomerName = '" + company + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
                        sad.Fill(dt);
                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }

                    if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                    {
                        DataTable dt = new DataTable();

                        //SqlDataAdapter sad = new SqlDataAdapter(" select [Id],[JobNo],[DateIn],[CustName],[Subcustomer],[Branch],[MateName],[SrNo],[MateStatus],FinalStatus,[TestBy],[ModelNo],[otherinfo],[Imagepath],[CreatedBy],[CreatedDate],[UpdateBy],[UpdateDate] ,ProductFault,RepeatedNo,DATEDIFF(DAY, CreatedDate, getdate()) AS days FROM [tblInwardEntry] Where DateIn between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", Cls_Main.Conn);
                        SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName WHERE (CP.CreatedBy='" + Session["UserCode"].ToString() + "' OR CP.UserName='" + Session["UserCode"].ToString() + "') AND  CP.CreatedOn between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
                        sad.Fill(dt);

                        GVPurchase.EmptyDataText = "Not Records Found";
                        GVPurchase.DataSource = dt;
                        GVPurchase.DataBind();
                    }

                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
                com.CommandText = "SELECT DISTINCT ProjectName FROM [tbl_NewOrderAcceptanceHdr] where " + "ProjectName like @Search + '%' and IsDeleted=0";

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
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_NewOrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName where ProjectName = '" + GST + "' AND CP.IsDeleted = 0", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
    }

    protected void ImageButtonfile5_Click(object sender, ImageClickEventArgs e)
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
                string CmdText = "select fileName from tbl_NewOrderAcceptanceHdr where IsDeleted=0 AND ID='" + id + "'";

                SqlDataAdapter ad = new SqlDataAdapter(CmdText, con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    //Response.Write(dt.Rows[0]["Path"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["fileName"].ToString()))
                    {
                        Response.Redirect("~/PDF_Files/" + dt.Rows[0]["fileName"].ToString());
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

    public void SendtoProduction(Int32 ID)
    {
        DataTable Dt = Cls_Main.Read_Table("select *,OAD.ID as PID FROM [DB_Foundtech].[dbo].[tbl_NewOrderAcceptanceHdr] AS OAH INNER JOIN tbl_NewOrderAcceptanceDtls AS OAD ON OAD.Pono = OAH.Pono WHERE OAH.ID = '" + ID + "' AND OAH.IsDeleted=0");
        foreach (DataRow row in Dt.Rows)
        {
            string PID = null;
            PID = row["PID"].ToString();
            Cls_Main.Conn_Open();
            SqlCommand cmd = new SqlCommand("SP_NewProductionDept", Cls_Main.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", PID);
            cmd.Parameters.AddWithValue("@MainId", ID);
            cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
            cmd.Parameters.AddWithValue("@Mode", "InseartOAinProduction");
            cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();
            Cls_Main.Conn_Dispose();

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('OA Send to Production Successfully..!!');window.location='OAList.aspx'; ", true);


    }
}


