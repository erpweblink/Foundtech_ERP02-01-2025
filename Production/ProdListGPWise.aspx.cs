using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Authentication;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_ProdListGPWise : System.Web.UI.Page
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
                this.ModalPopupHistory.Hide();
                FillGrid();

            }
        }
    }

    //Fill GridView
    private void FillGrid()
    {
        DataTable Dts = Cls_Main.Read_Table("SELECT ProjectCode, ProjectName, CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQuantity AS INT)) AS TotalQuantitySum, SUM(CAST(CompletedQTY AS INT)) AS CompletedQuantitySum, " +
            " MAX(CAST(Stage AS INT)) AS MaxStage FROM tbl_NewProductionHDR GROUP BY ProjectCode, CustomerName,  ProjectName " +
            " ORDER BY ProjectCode desc; ");

        MainGridLoad.DataSource = Dts;
        MainGridLoad.DataBind();
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
                Label JobNo = e.Row.FindControl("jobno") as Label;

                if (JobNo != null)
                {
                    DataTable Dt = Cls_Main.Read_Table("SELECT FilePath FROM tbl_NewProductionHDR  where JobNo ='" + JobNo.Text + "'");

                    LinkButton btndrawings = e.Row.FindControl("btndrawings") as LinkButton;

                    if (btndrawings != null)
                    {

                        if (Dt.Rows.Count > 0)
                        {
                            string fileName = Dt.Rows[0]["FilePath"].ToString();

                            if (fileName != "")
                            {
                                btndrawings.ForeColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                btndrawings.ForeColor = System.Drawing.Color.Black;
                            }
                        }
                        else
                        {
                            btndrawings.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }


                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
                gvDetails.DataSource = GetData(string.Format("select *,CONVERT(bigint,InwardQTY)-CONVERT(bigint,OutwardQTY) AS Pending from tbl_NewProductionDTLS where JobNo='{0}'", JobNo.Text));
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DrawingFiles")
        {
            Response.Redirect("~/Drawings/" + e.CommandArgument.ToString());
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
                com.CommandText = "SELECT DISTINCT [ID],[Companyname] FROM [tbl_CompanyMaster] where Companyname like  @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Companyname = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Companyname.Add(sdr["Companyname"].ToString());
                    }
                }
                con.Close();
                return Companyname;
            }
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        if (txtCustomerName.Text != "" || txtCustomerName.Text != null)
        {
            string company = txtCustomerName.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT ProjectCode, ProjectName, CustomerName, COUNT(*) AS TotalRecords, " +
                " SUM(CAST(TotalQuantity AS INT)) AS TotalQuantitySum, SUM(CAST(CompletedQTY AS INT)) AS CompletedQuantitySum " +
                " FROM tbl_NewProductionHDR WHERE  CustomerName='" + txtCustomerName.Text + "' " +
                " GROUP BY ProjectCode, CustomerName,  ProjectName " +
                " ORDER BY ProjectCode desc; ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProdListGPWise.aspx");
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
        if (txtjobno.Text != "" || txtjobno.Text != null)
        {
            string Cpono = txtjobno.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT ProjectCode, ProjectName, CustomerName, COUNT(*) AS TotalRecords, " +
               " SUM(CAST(TotalQuantity AS INT)) AS TotalQuantitySum, SUM(CAST(CompletedQTY AS INT)) AS CompletedQuantitySum " +
               " FROM tbl_NewProductionHDR WHERE  ProjectCode='" + Cpono + "' " +
               " GROUP BY ProjectCode, CustomerName,  ProjectName " +
               " ORDER BY ProjectCode desc; ", Cls_Main.Conn);
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
            SqlDataAdapter sad = new SqlDataAdapter("SELECT ProjectCode, ProjectName, CustomerName, COUNT(*) AS TotalRecords, " +
               " SUM(CAST(TotalQuantity AS INT)) AS TotalQuantitySum, SUM(CAST(CompletedQTY AS INT)) AS CompletedQuantitySum " +
               " FROM tbl_NewProductionHDR WHERE  ProjectName='" + GST + "' " +
               " GROUP BY ProjectCode, CustomerName,  ProjectName " +
               " ORDER BY ProjectCode desc; ", Cls_Main.Conn);
            sad.Fill(dt);
            MainGridLoad.EmptyDataText = "Not Records Found";
            MainGridLoad.DataSource = dt;
            MainGridLoad.DataBind();
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


    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable Dt = Cls_Main.Read_Table("select JobNo AS PID FROM [tbl_NewProductionHDR]  WHERE ProjectCode = '" + ViewState["ID"].ToString() + "'");
        int count = Dt.Rows.Count;
        int StageCount = 0;
        foreach (DataRow row in Dt.Rows)
        {
            ViewState["JobNo"] = row["PID"].ToString();

            if (Drawing.Checked == true)
            {
                InseartData("Drawing", 0);
                StageCount = 1;
            }
            if (PlazmaCutting.Checked == true)
            {
                InseartData("PlazmaCutting", 1);
                StageCount += 1;
            }
            if (Bending.Checked == true)
            {
                InseartData("Bending", 2);
                StageCount += 1;
            }
            if (Fabrication.Checked == true)
            {
                InseartData("Fabrication", 3);
                StageCount += 1;
            }
            if (Painting.Checked == true)
            {
                InseartData("Painting", 4);
                StageCount += 1;
            }
            if (Packaging.Checked == true)
            {
                InseartData("Quality", 5);
                StageCount += 1;
            }
            if (Dispatch.Checked == true)
            {
                InseartData("Dispatch", 6);
                StageCount += 1;
            }
            Cls_Main.Conn_Open();
            SqlCommand cmd = new SqlCommand("SP_NewProductionDept", Cls_Main.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@JobNo", ViewState["JobNo"].ToString());
            cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
            cmd.Parameters.AddWithValue("@StageCount", StageCount);
            cmd.Parameters.AddWithValue("@Mode", "InseartInwardQTY");
            cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();
            Cls_Main.Conn_Dispose();
        }
        this.ModalPopupHistory.Hide();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Production Plan Saved Successfully..!!');window.location='ProdListGPWise.aspx'; ", true);
    }

    public void InseartData(string Stage, int num)
    {
        Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SP_NewProductionDept", Cls_Main.Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@JobNo", ViewState["JobNo"].ToString());
        cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
        cmd.Parameters.AddWithValue("@Stage", Stage);
        cmd.Parameters.AddWithValue("@Num", num);
        cmd.Parameters.AddWithValue("@Mode", "InseartProduction");
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
    }

    protected void MainGridLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int maxStage = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MaxStage"));

                LinkButton btnSendtopro = (LinkButton)e.Row.FindControl("btnSendtopro");

                // Check if the MaxStage is 7 and hide the button
                if (maxStage == 7)
                {
                    btnSendtopro.Visible = false;
                }

                //Label ProjectCode = e.Row.FindControl("lblProjectCode") as Label;
                //GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                //if (GVPurchase == null)
                //{

                //    return;
                //}

                //if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                //{
                //    var data = GetData(string.Format("SELECT * FROM tbl_NewProductionHDR WHERE ProjectCode='{0}'", ProjectCode.Text));
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


                string empcode = Session["UserCode"].ToString();
                DataTable Dt = new DataTable();
                SqlDataAdapter Sd = new SqlDataAdapter("Select ID from tbl_UserMaster where UserCode='" + empcode + "'", con);
                Sd.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string id = Dt.Rows[0]["ID"].ToString();
                    DataTable Dtt = new DataTable();
                    SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'ProdListGPWise.aspx' AND PagesView = '1'", con);
                    Sdd.Fill(Dtt);
                    if (Dtt.Rows.Count > 0)
                    {
                        MainGridLoad.Columns[8].Visible = false;
                        btnSendtopro.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in RowDataBound: " + ex.Message);
        }
    }


    protected void MainGridLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Sendtoproduction")
        {
            ViewState["ID"] = e.CommandArgument.ToString();
            this.ModalPopupHistory.Show();

        }
        if (e.CommandName == "SendMail")
        {
            string Customer = e.CommandArgument.ToString();
            if (Customer != null)
            {
                String Usermail = "", SerialNo = "", PoNo = "";
                string Id = "";
                SqlDataAdapter ad = new SqlDataAdapter("SELECT TOP 1 *  FROM tbl_NewOrderAcceptanceHdr WHERE CustomerName ='" + Customer + "'", con);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Id = dt.Rows[0]["ID"].ToString();
                    Usermail = dt.Rows[0]["EmailId"].ToString();
                    PoNo = dt.Rows[0]["PoNo"].ToString();
                    SerialNo = dt.Rows[0]["SerialNo"].ToString();
                    string url = "ProductionListForCust.aspx?ID=" + objcls.encrypt(Id) + "&name=" + objcls.encrypt(Customer);

                   SendMail(url, Usermail, Customer, PoNo, SerialNo);

                    // Response.Redirect(url);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please Register Mail First !!');", true);
                }
            }
        }
        if(e.CommandName == "ViewDetails")
        {
            Response.Redirect("ProdDTLSProjCodeWise.aspx?Id=" + objcls.encrypt(e.CommandArgument.ToString()) + "");
        }
    }

    protected void SendMail(string url, string mail, string CustomerName, string PoNo, string SerialNo)
    {
        try
        {
            // Force using TLS 1.2 for SMTP
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            string strMessage = "Hello, " + CustomerName + " <br /> Click the link below to track your OA report:<br /><br />";
            strMessage += "<a href='https://www.foundtecherp.com/Production/" + url + "'>View Report</a>";
            MailMessage message = new MailMessage();
            message.To.Add(mail);
            // Add CC recipients
            string erp = "cn.foundtechengg@gmail.com";
            string Gmail = "testing@weblinkservices.net";
            message.CC.Add(erp);
            message.CC.Add(Gmail);
            message.ReplyToList.Add(new MailAddress("cn.foundtechengg@gmail.com"));

            message.Subject = "Track Your Order";
            message.Body = GetEmailTemplate(CustomerName, "https://www.foundtecherp.com/Production/" + url, PoNo, SerialNo);
            message.From = new System.Net.Mail.MailAddress("testing@weblinkservices.net"); // Email-ID of Sender
            message.IsBodyHtml = true;

            SmtpClient SmtpMail = new SmtpClient();
            SmtpMail.Host = "smtpout.secureserver.net";
            SmtpMail.Port = 587;
            SmtpMail.Credentials = new System.Net.NetworkCredential("testing@weblinkservices.net", "Weblink@Testing#123"); // Username/password of network, if apply  
            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpMail.EnableSsl = true;
            SmtpMail.Timeout = 10000;
            SmtpMail.ServicePoint.MaxIdleTime = 0;
            SmtpMail.ServicePoint.SetTcpKeepAlive(true, 2000, 2000);
            message.BodyEncoding = Encoding.Default;
            message.Priority = MailPriority.High;
            SmtpMail.Send(message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Mail Send Successfully !!');", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Mail Not send !!');", true);
            throw ex;
        }
    }


    private string GetEmailTemplate(string user, string link, string PoNo, string SerialNo)
    {
        string template = @"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""utf-8"" />
            <title></title>
        </head>
        <body>
            <div width=""100%"" style=""min-width:100%!important;margin:0!important;padding:0!important"">
                <table width=""660"" border=""1"" cellpadding=""0"" cellspacing=""0"" align=""center"">
                    <tbody>
                        <tr>
                            <td width=""100%"" style=""min-width:100%"">
                                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""display:block"">
                                    <tbody>
                                        <tr>
                                            <td width=""100%"" align=""center"" style=""display:block;text-align:center;vertical-align:top;font-size:16;min-width:100%;background-color:#edece6"">
                                                <table align=""center"" width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""display:block;min-width:100%!important"" bgcolor=""#ffffff"">
                                                    <tbody>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td width=""100%"" align=""center"" style=""text-align:center;padding:10px 0px"">
                                                                <a href=""https://www.foundtechengg.com/"" target=""_blank""><img src=""https://www.foundtechengg.com/images/logo.png"" width=""40%"" style=""box-shadow: rgba(6, 24, 44, 0.4) 0px 0px 0px 2px, rgba(6, 24, 44, 0.65) 0px 4px 6px -1px, rgba(255, 255, 255, 0.08) 0px 1px 0px inset;""></a>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div style=""overflow:hidden;display:none;font-size:1px;color:#ffffff;line-height:1px;max-height:0px;max-width:0px;opacity:0"">&nbsp;</div>
                                                <table align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""display:block;min-width:100%;background-color:#edece6"">
                                                    <tbody>
                                                        <tr style=""background-color:#1a263a"">
                                                            <td>&nbsp;</td>
                                                            <td width=""660"" style=""padding:10px 0px 10px 0px;text-align:center""><a href=""#"" style=""text-decoration:none;font-size:14px;color:#ffffff""> Notification From Foundtech Engineering </a></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td valign=""middle"" align=""left"" width=""100%"" style=""padding:0px 21px 0px 21px"">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td width=""100%"">
                                                                <table cellpadding=""0"" cellspacing=""0"" border=""0"" align=""center"" style=""border-bottom:2px solid #b8b8b8; width:90%"" bgcolor=""#ffffff"">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td width=""100%"" align=""left"" valign=""top"" style=""padding:10px;"">
                                                                                <table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
                                                                                    <tbody>
                                                                                        <tr height=""40"">
                                                                                            <td style=""text-align:center;padding:0px 1px 1px 15px;font-size:14px;color:#333333;line-height:1.4!important;word-wrap:break-word"" valign=""top"">
                                                                                                <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left; color:black;"">
                                                                                                    Dear: <strong>{user}</strong>,
                                                                                                </p>
                                                                                                <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left;"">
                                                                                                   We hope this message finds you well. Thank you for choosing Foundtech Engineering for your recent purchase. We're happy to provide you with an update.
                                                                                                </p>
                                                                                                <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left;"">
                                                                                                     Your order <strong>{PoNo}</strong>, with the reference number <strong>{SerialNo}</strong>, is currently in the production stage.
                                                                                                </p>
                                                                                                <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left;"">
                                                                                                    To help you track the progress of your order, <a href=""{link}"" style=""color: #1a263a; text-decoration: underline;""><strong>Click Here</strong></a>.
                                                                                                </p>
                                                                                                   <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left;"">
                                                                                                    We appreciate your patience and trust in Foundtech  Engineering. Thank you for being a valued customer.
                                                                                                </p>
                                                                                                <p class=""pdata"" align=""center"" style=""font-size: 16px; text-align: left;"">
                                                                                                    Best regards, <br />
                                                                                            Foundtech  Engineering. <br/>
                                                                                            Mobile: 9552513956 / 8888884238 <br/>
                                                                                            <a href=""https://www.foundtechengg.com/"" >https://www.foundtechengg.com/</a>
                                                                                                </p>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </tbody>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                                <br />
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <br />
                                                <table width=""100%"">
                                                    <tbody>
                                                        <tr>
                                                            <td width=""100%"" bgcolor=""#1A263A"" style=""padding:0px 30px!important;background-color:#1a263a"">
                                                                <table width=""100%"" border=""0"" cellpadding=""0"" cellspacing=""0"" style=""color:#ffffff;text-align:center;font-size:14px"">
                                                                    <tbody>
                                                                        <tr><td height=""24px"">&nbsp;</td></tr>
                                                                        <tr>
                                                                            <td style=""padding:0px 10px 0px 10px;font-size:14px"">
                                                                                <!-- Add any additional content or links here -->
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style=""padding:5px 10px 24px 10px;text-decoration:none!important;color:#ffffff!important;font-size:14px"">
                                                                                <span>Gat No.-250, Opp.Agarwal Packaging Ltd. Kharabwadi Chakan, Pune 410501</span>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </body>
        </html>
    ";

        return template.Replace("{user}", user).Replace("{SerialNo}", SerialNo).Replace("{PoNo}", PoNo).Replace("{link}", link);
    }
}