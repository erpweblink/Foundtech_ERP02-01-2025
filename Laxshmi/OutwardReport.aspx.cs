﻿
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

public partial class Laxshmi_OutwardReport : System.Web.UI.Page
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
                if (Request.QueryString["CustName"] != null)
                {
                    txtCustomerName.Text = Request.QueryString["CustName"].ToString();
                    txtRowMaterial.Text = Request.QueryString["ProductName"].ToString();
                    txtCustomerName.ReadOnly = true;
                    txtRowMaterial.ReadOnly = true;
                    txtInwardno.ReadOnly = true;
                    txtDeliveryNoteno.ReadOnly = true;
                    txtReferenceNo.ReadOnly = true;
                    txtfromdate.ReadOnly = true;
                    txttodate.ReadOnly = true;
                    btnSearchData.Enabled = false;
                    GridView();
                }
                else
                {
                    GridView();
                }

            }
        }
    }

    void GridView()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetOutwardReportlist");
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
            if (txtDeliveryNoteno.Text == null || txtDeliveryNoteno.Text == "")
            {
                cmd.Parameters.AddWithValue("@DeliveryNoteno", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DeliveryNoteno", txtDeliveryNoteno.Text);
            }
            if (txtReferenceNo.Text == null || txtReferenceNo.Text == "")
            {
                cmd.Parameters.AddWithValue("@ReferenceNo", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text);
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
        //Response.Redirect(Request.RawUrl);
        Response.Redirect("../Laxshmi/OutwardReport.aspx");
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
                    cmd.Parameters.AddWithValue("@Mode", "GetOutwardReportlist");
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
                    if (txtDeliveryNoteno.Text == null || txtDeliveryNoteno.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNoteno", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNoteno", txtDeliveryNoteno.Text);
                    }
                    if (txtReferenceNo.Text == null || txtReferenceNo.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@ReferenceNo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ReferenceNo", txtReferenceNo.Text);
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
                    ReportViewer1.LocalReport.ReportPath = "RDLC_Reports\\OutwardReport.rdlc";
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
                com.CommandText = "select DISTINCT CompanyName from tbl_LM_Outwarddata where  " + "CompanyName like  '%'+ @Search + '%' ";

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
                com.CommandText = "select DISTINCT RowMaterial from tbl_LM_Outwarddata where  " + "RowMaterial like  '%'+ @Search + '%'";

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
                com.CommandText = "select DISTINCT OutwardNo from tbl_LM_Outwarddata where  " + "OutwardNo like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["OutwardNo"].ToString());
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


    //Search Delivery Note Number methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDeliveryNotenoList(string prefixText, int count)
    {
        return AutoFillDeliveryNoteno(prefixText);
    }

    public static List<string> AutoFillDeliveryNoteno(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT DeliveryNoteno from tbl_LM_Outwarddata where  " + "DeliveryNoteno like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["DeliveryNoteno"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    //Search Referenace No List methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetReferenceNoList(string prefixText, int count)
    {
        return AutoFillReferenceNo(prefixText);
    }

    public static List<string> AutoFillReferenceNo(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT ReferenceNo from tbl_LM_Outwarddata where  " + "ReferenceNo like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["ReferenceNo"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }


    protected void txtReferenceNo_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    protected void txtDeliveryNoteno_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    protected void GVfollowup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string rowIndex = e.CommandArgument.ToString();

            GridViewRow row = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            GridView gvPurchase = (GridView)row.FindControl("GVfollowup");


            string OutwardQty = ((Label)row.FindControl("lblOutwardQty")).Text;
            string CustomerName = ((Label)row.FindControl("lblCompanyName")).Text;
            string VehicleNo = ((Label)row.FindControl("lblVehicleNo")).Text;
            string RowMaterial = ((Label)row.FindControl("lblRowMaterial")).Text;
            string OutwardNo = ((Label)row.FindControl("lblInwardNo")).Text;
            string Weight = ((Label)row.FindControl("lblWeight")).Text;
            string DeliveryNoteno = ((Label)row.FindControl("lblDeliveryNoteno")).Text;
            string DeliveryNotedate = ((Label)row.FindControl("lblDeliveryNotedate")).Text;
            string ReferenceNo = ((Label)row.FindControl("lbldReferenceNo")).Text;
            string ReferenceDate = ((Label)row.FindControl("lblReferenceDate")).Text;

            if (ReferenceDate != "" && ReferenceDate != "01-01-1900")
            {
                //DateTime ffff1 = Convert.ToDateTime(ReferenceDate);
                //txtReferenceDate.Text = ffff1.ToString("yyyy-MM-dd");
                txtReferenceDate.Text = ReferenceDate;
            }
            if (DeliveryNotedate != "" && DeliveryNotedate!= "01-01-1900")
            {
                //DateTime ffff2 = Convert.ToDateTime(DeliveryNotedate);
                //txtDeliverynotedate.Text = ffff2.ToString("yyyy-MM-dd");
                txtDeliverynotedate.Text = DeliveryNotedate;
            }



            txtInwardnopop.Text = OutwardNo;
            txtcustomernamepop.Text = CustomerName;
            txtrowmaterialpop.Text = RowMaterial;
            txtcustomernamepop.Text = CustomerName;
            txtVehicleno.Text = VehicleNo;
            txtWeight.Text = Weight;
            TextBox1.Text = DeliveryNoteno;
            // txtDeliverynotedate.Text = DeliveryNotedate;
            txtrefrenceno.Text = ReferenceNo;
            // txtReferenceDate.Text = ReferenceDate;
            txtoutwardqty.Text = OutwardQty;

            txtinwardqty.Text = OutwardQty;

            this.ModalPopupHistory.Show();
        }
    }

    protected void GVfollowup_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {

            double outtotal = Convert.ToDouble(txtoutwardqty.Text);

            if (outtotal <= Convert.ToDouble(txtinwardqty.Text))
            {

                Cls_Main.Conn_Open();
                SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", Cls_Main.Conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "UpdateOutwardQTY");
                cmd.Parameters.AddWithValue("@OutwardNo", txtInwardnopop.Text);
                cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToString(outtotal));
                cmd.Parameters.AddWithValue("@Description", txtRemarks.Text);
                cmd.Parameters.AddWithValue("@Vehicleno", txtVehicleno.Text);
                cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                cmd.Parameters.AddWithValue("@DeliveryNotedate", txtDeliverynotedate.Text);
                cmd.Parameters.AddWithValue("@DeliveryNoteno", TextBox1.Text);
                cmd.Parameters.AddWithValue("@ReferenceNo", txtrefrenceno.Text);
                cmd.Parameters.AddWithValue("@ReferenceDate", txtReferenceDate.Text);

                cmd.ExecuteNonQuery();
                Cls_Main.Conn_Close();
                Cls_Main.Conn_Dispose();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Updated Successfully ..!!');window.location='OutwardReport.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Outward quantity not matched ..!!');", true);
                this.ModalPopupHistory.Show();
            }
        }
        catch (Exception ex)
        {

        }

    }

}