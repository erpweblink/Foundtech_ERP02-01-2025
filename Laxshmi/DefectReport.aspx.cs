
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

public partial class Laxshmi_DefectReport : System.Web.UI.Page
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
            cmd.Parameters.AddWithValue("@Mode", "GetDefectReport");
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
            if (txtDelivery.Text == null || txtDelivery.Text == "")
            {
                cmd.Parameters.AddWithValue("@DeliveryNo", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DeliveryNo", txtDelivery.Text);
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
                    cmd.Parameters.AddWithValue("@Mode", "GetDefectReport");
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
                    if (txtDelivery.Text == null || txtDelivery.Text == "")
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DeliveryNo", txtDelivery.Text);
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
                    ReportViewer1.LocalReport.ReportPath = "RDLC_Reports\\InventoryReport.rdlc";
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
                        Response.AddHeader("content-disposition", "attachment; filename=DefectReport.xls");
                    }
                    else
                    {
                        Response.ContentType = "application/vnd.pdf";
                        Response.AddHeader("content-disposition", "attachment; filename=DefectReport.pdf");
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




    //Search Delivery Number methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetDeliveryList(string prefixText, int count)
    {
        return AutoFillDelivery(prefixText);
    }

    public static List<string> AutoFillDelivery(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT DEFDeleveryNo from tbl_LM_DefectOutward where  " + "DEFDeleveryNo like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["DEFDeleveryNo"].ToString());
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

}