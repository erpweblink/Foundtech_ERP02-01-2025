
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Laxshmi_InwardReport : System.Web.UI.Page
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

                GridView();
            }
        }
    }

    void GridView()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetInwardReportlist");
            if (txtCustomerName.Text == null || txtCustomerName.Text == "")
            {
                cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CompanyName", txtCustomerName.Text);
            }
            if (txtRowMaterial.Text == null || txtRowMaterial.Text == "")
            {
                cmd.Parameters.AddWithValue("@RowMaterial", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RowMaterial", txtRowMaterial.Text);
            }
            if (txtInwardno.Text == null || txtInwardno.Text == "")
            {
                cmd.Parameters.AddWithValue("@InwardNo", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InwardNo", txtInwardno.Text);
            }
            if (txtDeliveryNo.Text == null || txtDeliveryNo.Text == "")
            {
                cmd.Parameters.AddWithValue("@DeliveryNo", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DeliveryNo", txtDeliveryNo.Text);
            }
            if (txtHSN.Text == null || txtHSN.Text == "")
            {
                cmd.Parameters.AddWithValue("@HSN", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@HSN", txtHSN.Text);
            }
            if (txtfromdate.Text == null || txtfromdate.Text == "" && txttodate.Text == null || txttodate.Text == "")
            {
                cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                cmd.Parameters.AddWithValue("@ToDate", txttodate.Text);    
            }              
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Columns.Count > 0)
            {
                GVfollowup.DataSource = dt;
                GVfollowup.DataBind();
            }


        }
        catch (Exception ex)
        {
            //throw ex;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);
        }
    }

    protected void btnSearchData_Click(object sender, EventArgs e)
    {
        GridView();
    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Report("EXCEL");
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        Report("PDF");
    }

    public void Report(string flag)
    {
        try
        {
            DataSet Dtt = new DataSet();
            string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand("[SP_Laxshmidetails]", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "GetInwardReportlist");
                    if (txtCustomerName.Text == null || txtCustomerName.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", txtCustomerName.Text);
                    }
                    if (txtRowMaterial.Text == null || txtRowMaterial.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@RowMaterial", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RowMaterial", txtRowMaterial.Text);
                    }
                    if (txtInwardno.Text == null || txtInwardno.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@InwardNo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InwardNo", txtInwardno.Text);
                    }
                    if (txtDeliveryNo.Text == null || txtDeliveryNo.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNo", txtDeliveryNo.Text);
                    }
                    if (txtHSN.Text == null || txtHSN.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@HSN", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HSN", txtHSN.Text);
                    }
                    if (txtfromdate.Text == null || txtfromdate.Text == "" && txttodate.Text == null || txttodate.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@FromDate", txtfromdate.Text);
                        cmd.Parameters.AddWithValue("@ToDate", txttodate.Text);
                    }
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(Dtt);

                        }
                    }
                }
            }

            if (Dtt.Tables.Count > 0)
            {
                if (Dtt.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource obj1 = new ReportDataSource("DataSet1", Dtt.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Add(obj1);
                    ReportViewer1.LocalReport.ReportPath = "RDLC_Reports\\InwardReport.rdlc";
                    ReportViewer1.LocalReport.Refresh();
                    //-------- Print PDF directly without showing ReportViewer ----
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    byte[] bytePdfRep = ReportViewer1.LocalReport.Render(flag, null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;

                    if (flag == "EXCEL")
                    {
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("content-disposition", "attachment; filename=InventoryReports.xls");
                    }
                    else
                    {
                        Response.ContentType = "application/vnd.pdf";
                        Response.AddHeader("content-disposition", "attachment; filename=InventoryReports.pdf");
                    }

                    Response.BinaryWrite(bytePdfRep);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.Reset();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Not Found...........!')", true);
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void GVfollowup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       // if (e.Row.RowType == DataControlRowType.Footer)
       // {
       //     decimal InwardQty = 0;
       //     decimal OutwardQty = 0;
       //     decimal DefectQty = 0;
       //     decimal RemainingQty = 0;


       //     // Loop through the data rows to calculate the totals
       //     foreach (GridViewRow row in GVfollowup.Rows)
       //     {
       //         if (row.RowType == DataControlRowType.DataRow)
       //         {
       //             // Calculate the total for each column
       //             InwardQty += Convert.ToDecimal((row.FindControl("lbInwardQty") as Label).Text);
       //             OutwardQty += Convert.ToDecimal((row.FindControl("lblOutwardQty") as Label).Text);
       //             DefectQty += Convert.ToDecimal((row.FindControl("lblDefectQty") as Label).Text);
       //             RemainingQty += Convert.ToDecimal((row.FindControl("lblRemainingQty") as Label).Text);

       //         }
       //     }

       //// Display the totals in the footer labels
       //(e.Row.FindControl("lblTotalInwardQty") as Label).Text = InwardQty.ToString();
       //     (e.Row.FindControl("lblTotalOutwardQty") as Label).Text = OutwardQty.ToString();
       //     (e.Row.FindControl("lblTotalDefectQty") as Label).Text = DefectQty.ToString();
       //     (e.Row.FindControl("lblTotalRemainingQty") as Label).Text = RemainingQty.ToString();
       // }
    }

    //Search Customers methods
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
                com.CommandText = "select DISTINCT CompanyName from tbl_LM_inwarddata where  " + "CompanyName like @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["CompanyName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    //Search Row Material methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetRowMaterialList(string prefixText, int count)
    {
        return AutoFillRowMaterial(prefixText);
    }

    public static List<string> AutoFillRowMaterial(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT RowMaterial from tbl_LM_inwarddata where  " + "RowMaterial like  '%'+ @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["RowMaterial"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }


    protected void txtRowMaterial_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }
    //Search Inward Number methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetInwardnoList(string prefixText, int count)
    {
        return AutoFillInwardNo(prefixText);
    }

    public static List<string> AutoFillInwardNo(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT InwardNo from tbl_LM_inwarddata where  " + "InwardNo like '%' + @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["InwardNo"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtInwardno_TextChanged(object sender, EventArgs e)
    {
        GridView();

    }


    //Search Delivery Number methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDeliveryNoList(string prefixText, int count)
    {
        return AutoFillDeliveryNo(prefixText);
    }

    public static List<string> AutoFillDeliveryNo(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT DeliveryNo from tbl_LM_inwarddata where  " + "DeliveryNo like '%' + @Search + '%' and IsDeleted=0";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["DeliveryNo"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    //Search HSN List methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetHSNList(string prefixText, int count)
    {
        return AutoFillHSN(prefixText);
    }

    public static List<string> AutoFillHSN(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT HSN from tbl_LM_inwarddata where  " + "HSN like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["HSN"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }


    protected void txtHSN_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    protected void txtDeliveryNo_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    //changes on 19112024
    protected void btnCreate_Click(object sender, EventArgs e)
    {
         divinwardform.Visible = true;
        divtabl.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        divinwardform.Visible = false;
        divtabl.Visible = true;
    }

    protected void txtWeight_TextChanged(object sender, EventArgs e)
    {
        if (txtinwardqantity.Text != "" && txtWeight.Text != "")
        {
            try
            {
                Double Totalweight = Convert.ToDouble(txtinwardqantity.Text) * Convert.ToDouble(txtWeight.Text);

                txtTotalWeight.Text = Convert.ToString(Totalweight);
            }
            catch
            {

            }
        }

    }
    protected void btnsavedata_Click(object sender, EventArgs e)
    {

        Cls_Main.Conn_Open();
        SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", Cls_Main.Conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@InwardNo", hdnInwardNo.Value);
        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
        cmd.Parameters.AddWithValue("@CompanyName", txtcustomer.Text);
        cmd.Parameters.AddWithValue("@RowMaterial", txtrowmetarial.Text);
        cmd.Parameters.AddWithValue("@InwardQty", txtinwardqantity.Text);
        cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());

        cmd.Parameters.AddWithValue("@DeliveryNo", txtDeliveryNo1.Text);
        cmd.Parameters.AddWithValue("@DeliveryDate", txtDeliveryDate.Text);
        cmd.Parameters.AddWithValue("@WODate", txtWODate.Text);
        cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
        cmd.Parameters.AddWithValue("@Vehicleno", txtVehicleno.Text);
        cmd.Parameters.AddWithValue("@HSN", txtHSN1.Text);
        cmd.Parameters.AddWithValue("@Totalweight", txtTotalWeight.Text);
        if (btnsavedata.Text == "Update")
        {
            cmd.Parameters.AddWithValue("@Mode", "UpdateInwarddata");
        }
        else
        {
            cmd.Parameters.AddWithValue("@Mode", "InseartInwarddata");
        }

        cmd.ExecuteNonQuery();
        Cls_Main.Conn_Close();
        Cls_Main.Conn_Dispose();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Record Successfully..!!');window.location='InwardReport.aspx';", true);
    }

    protected void GVfollowup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            divinwardform.Visible = true;
            divtabl.Visible = false;
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVfollowup.Rows[rowIndex];
            txtcustomer.Text = ((Label)row.FindControl("lblCompanyName")).Text;
            txtrowmetarial.Text = ((Label)row.FindControl("lblRowMaterial")).Text;
            hdnInwardNo.Value = ((Label)row.FindControl("lblInwardNo")).Text;
            txtinwardqantity.Text = ((Label)row.FindControl("lblInwardQty")).Text;
            string inwardno = ((Label)row.FindControl("lblInwardNo")).Text;
            DataTable Dt = Cls_Main.Read_Table("select * from tbl_LM_inwarddata where InwardNo='" + hdnInwardNo.Value + "'");
            txtDeliveryNo1.Text = Dt.Rows[0]["DeliveryNo"].ToString();
            DateTime ffff1 = Convert.ToDateTime(Dt.Rows[0]["DeliveryDate"].ToString());
            txtDeliveryDate.Text = ffff1.ToString("yyyy-MM-dd");
            DateTime ffff2 = Convert.ToDateTime(Dt.Rows[0]["WODate"].ToString());
            txtWODate.Text = ffff2.ToString("yyyy-MM-dd");

            txtWeight.Text = Dt.Rows[0]["Weight"].ToString();
            txtTotalWeight.Text = Dt.Rows[0]["TotalWeight"].ToString();
            txtVehicleno.Text = Dt.Rows[0]["VehicleNo"].ToString();
            txtHSN1.Text = Dt.Rows[0]["HSN"].ToString();
            btnsavedata.Text = "Update";
        }
        if (e.CommandName == "RowDelete")
        {
            Cls_Main.Conn_Open();
            SqlCommand Cmd = new SqlCommand("UPDATE [tbl_LM_inwarddata] SET IsDeleted=@IsDeleted,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID", Cls_Main.Conn);
            Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument.ToString()));
            Cmd.Parameters.AddWithValue("@IsDeleted", '1');
            Cmd.Parameters.AddWithValue("@DeletedBy", Session["UserCode"].ToString());
            Cmd.Parameters.AddWithValue("@DeletedOn", DateTime.Now);
            Cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Entry Deleted Successfully..!!')", true);
            GridView();
        }
    }
}