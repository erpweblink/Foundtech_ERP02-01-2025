
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


public partial class Production_ProductionList : System.Web.UI.Page
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

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionHDR order by ID desc");
        GVPurchase.DataSource = Dt;
        GVPurchase.DataBind();
       
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
    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Sendtoproduction")
        {
            ViewState["ID"] = e.CommandArgument.ToString();
            this.ModalPopupHistory.Show();
           
        }
        //if (e.CommandName == "RowDelete")
        //{
        //    Cls_Main.Conn_Open();
        //    SqlCommand Cmd = new SqlCommand("UPDATE [tbl_OrderAcceptanceHdr] SET IsDeleted=@IsDeleted,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID", Cls_Main.Conn);
        //    Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument.ToString()));
        //    Cmd.Parameters.AddWithValue("@IsDeleted", '1');
        //    Cmd.Parameters.AddWithValue("@DeletedBy", Session["UserCode"].ToString());
        //    Cmd.Parameters.AddWithValue("@DeletedOn", DateTime.Now);
        //    Cmd.ExecuteNonQuery();
        //    Cls_Main.Conn_Close();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Acceptance Deleted Successfully..!!')", true);
        //    FillGrid();
        //}
        if (e.CommandName == "RowView")
        {
            Response.Redirect("Pdf_CustomerPurchase.aspx?Pono=" + objcls.encrypt(e.CommandArgument.ToString()) + " ");
            // Response.Write("<script>window.open ('Pdf_Quotation.aspx?Quotationno=" + (e.CommandArgument.ToString()) + "','_blank');</script>");
        }
    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVPurchase.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblStatus = e.Row.FindControl("Status") as Label;
                Label jobno = e.Row.FindControl("jobno") as Label;

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
             

                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
                gvDetails.DataSource = GetData(string.Format("select *,CONVERT(bigint,InwardQTY)-CONVERT(bigint,OutwardQTY) AS Pending from tbl_ProductionDTLS where JobNo='{0}'", jobno.Text));
                gvDetails.DataBind();

             
            }
        }
        catch(Exception ex)
        {

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
                com.CommandText = "SELECT DISTINCT [ID],[Companyname] FROM [tbl_CompanyMaster] where " + "Companyname like '%'+ @Search + '%' and IsDeleted=0";

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
        if (txtCustomerName.Text != "" || txtCustomerName.Text != null)
        {
            string company = txtCustomerName.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby WHERE  CustomerName='" + txtCustomerName.Text + "' ORDER BY CP.ID DESC", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProductionList.aspx");
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
                com.CommandText = "SELECT * FROM [tbl_ProductionHDR] where " + "OANumber like  '%'+ @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["OANumber"].ToString());
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
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby WHERE OANumber='" + Cpono + "' ORDER BY CP.ID DESC", Cls_Main.Conn);
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
            if (string.IsNullOrEmpty(txtCustomerName.Text) && string.IsNullOrEmpty(txtjobno.Text) && string.IsNullOrEmpty(txtfromdate.Text) && string.IsNullOrEmpty(txttodate.Text))
            {
                FillGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Search Record');", true);
            }
            else
            {
                if (txtjobno.Text != "")
                {
                    string Quono = txtjobno.Text;
                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby where OANumber = '" + Quono + "' ", Cls_Main.Conn);
                    sad.Fill(dt);
                    GVPurchase.EmptyDataText = "Not Records Found";
                    GVPurchase.DataSource = dt;
                    GVPurchase.DataBind();
                }
                if (txtGST.Text != "")
                {
                    string JOBno = txtGST.Text;
                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby where JobNo = '" + JOBno + "' ", Cls_Main.Conn);
                    sad.Fill(dt);
                    GVPurchase.EmptyDataText = "Not Records Found";
                    GVPurchase.DataSource = dt;
                    GVPurchase.DataBind();
                }
                if (txtCustomerName.Text != "")
                {
                    string company = txtCustomerName.Text;

                    DataTable dt = new DataTable();
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby where CustomerName = '" + company + "' ", Cls_Main.Conn);
                    sad.Fill(dt);
                    GVPurchase.EmptyDataText = "Not Records Found";
                    GVPurchase.DataSource = dt;
                    GVPurchase.DataBind();
                }

                if (!string.IsNullOrEmpty(txtfromdate.Text) && !string.IsNullOrEmpty(txttodate.Text))
                {
                    DataTable dt = new DataTable();

                    //SqlDataAdapter sad = new SqlDataAdapter(" select [Id],[JobNo],[DateIn],[CustName],[Subcustomer],[Branch],[MateName],[SrNo],[MateStatus],FinalStatus,[TestBy],[ModelNo],[otherinfo],[Imagepath],[CreatedBy],[CreatedDate],[UpdateBy],[UpdateDate] ,ProductFault,RepeatedNo,DATEDIFF(DAY, CreatedDate, getdate()) AS days FROM [tblInwardEntry] Where DateIn between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", Cls_Main.Conn);
                    SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby WHERE  CP.Createdon between'" + txtfromdate.Text + "' AND '" + txttodate.Text + "' ", Cls_Main.Conn);
                    sad.Fill(dt);

                    GVPurchase.EmptyDataText = "Not Records Found";
                    GVPurchase.DataSource = dt;
                    GVPurchase.DataBind();
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
                com.CommandText = "SELECT DISTINCT JobNo FROM [tbl_ProductionHDR] where " + "JobNo like '%'+ @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["JobNo"].ToString());
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
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM [tbl_ProductionHDR] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.Createdby where JobNo = '" + GST + "'", Cls_Main.Conn);
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
                string CmdText = "select fileName from tbl_OrderAcceptanceHdr where IsDeleted=0 AND ID='" + id + "'";

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
        if (Drawing.Checked == true)
        {
            InseartData("Drawing",0);
        }
        if (PlazmaCutting.Checked == true)
        {
            InseartData("PlazmaCutting",1);
        }
        if (Fabrication.Checked == true)
        {
            InseartData("Fabrication",2);
        }
        if (Bending.Checked == true)
        {
            InseartData("Bending",3);
        }
        if (Painting.Checked == true)
        {
            InseartData("Painting",4);
        }
        if (Packaging.Checked == true)
        {
            InseartData("Packaging",5);
        }
        if (Dispatch.Checked == true)
        {
            InseartData("Dispatch",6);
        }
        Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SP_ProductionDept", Cls_Main.Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@JobNo", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());      
        cmd.Parameters.AddWithValue("@Mode", "InseartInwardQTY");
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Production Plan Saved Successfully..!!');window.location='ProductionList.aspx'; ", true);
    }

    public void InseartData(string Stage,int num)
    {
        Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SP_ProductionDept", Cls_Main.Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@JobNo", ViewState["ID"].ToString());
        cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
        cmd.Parameters.AddWithValue("@Stage", Stage);
        cmd.Parameters.AddWithValue("@Num", num);
        cmd.Parameters.AddWithValue("@Mode", "InseartProduction");
        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Production Plan Saved Successfully..!!');window.location='ProductionList.aspx'; ", true);
    }
}


