using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class Laxshmi_LaxshmiDashboard : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["UserCode"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                int value = currentYear - 2024;
                string ToDays = "ToDay";
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add(new ListItem("ToDay", "ToDay"));
                for (int i = 0; i <= value; i++)
                {
                    int year = currentYear - i;
                    DropDownList1.Items.Add(new ListItem(year.ToString(), year.ToString()));
                }

                DropDownList1.SelectedValue = ToDays;

                MonthDp.Visible = false;
                MonthDp1.Visible = false;
                //CustName.Visible = false;
                CustomerCount();
                MaterialCount();
                InwardCount();
                OutwardCount();
                InventoryCount();
            }
        }
    }

    protected void CustomerCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_CompanyMaster where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tblTaxInvoiceHdr where CreatedBy='" + Session["UserCode"].ToString() + "'  AND IsDeleted=0 and Status>=2 AND CONVERT(nvarchar(10),CreatedOn,103)=CONVERT(nvarchar(10),GETDATE(),103)", Cls_Main.Conn);

        //    count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        lblcustomers.Text = count.ToString();
        Cls_Main.Conn_Close();
    }

    protected void MaterialCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_ComponentMaster where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblMaterial.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void InwardCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_inwarddata where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblInwardQuantity.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void OutwardCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_Outwarddata ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        lblOuwardQuantity.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void InventoryCount()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetToDayList");
            if (DropDownList1.SelectedItem.Text == "ToDay")
            {
                cmd.Parameters.AddWithValue("@SelectedValue", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SelectedValue", DropDownList1.SelectedItem.Text);
            }
            if (txtCustName.Text != "")
            {
                cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CustName", DBNull.Value);
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GVCustomerList.DataSource = dt;
                GVCustomerList.DataBind();
            }

        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);

        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedItem.Text != "ToDay")
        {
            MonthDp.Visible = false;
            DropDownList2.Items.Clear();
            int val = 0;
            string ToDays = "Months";
            int currentMonth = DateTime.Now.Month;
            if (Convert.ToInt32(DropDownList1.SelectedValue) < Convert.ToInt32(DateTime.Now.Year))
            {
                val = 12;
            }
            else if (currentMonth != 1)
            {
                val = currentMonth - 1;
            }

            DropDownList2.Items.Add(new ListItem("Months", "Months"));
            for (int i = 0; i <= val; i++)
            {
                if (DropDownList1.SelectedValue == "2024")
                {
                    if (i <= 1)
                    {
                        int month = 11 + i;
                        string monthText = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                        string monthValue = month.ToString("D2");
                        DropDownList2.Items.Add(new ListItem(monthText.ToString(), monthValue.ToString()));
                    }
                }
                else
                {
                    if (val != 12)
                    {
                        int month = currentMonth - i;
                        if (month <= 0) month = 12 + month;

                        string monthText = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                        string monthValue = month.ToString("D2");
                        DropDownList2.Items.Add(new ListItem(monthText.ToString(), monthValue.ToString()));
                    }
                    else
                    {
                        int month = 12 - i;
                        if (month != 0)
                        {
                            string monthText = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month);
                            string monthValue = month.ToString("D2");
                            DropDownList2.Items.Add(new ListItem(monthText.ToString(), monthValue.ToString()));
                        }

                    }
                }

            }
            DropDownList2.SelectedValue = ToDays;
            MonthDp1.Visible = true;
            // CustName.Visible = true;
        }
        else
        {
            MonthDp.Visible = false;
            MonthDp1.Visible = false;
            // CustName.Visible = false;
            GVCustomerList.DataSource = "";
            GVCustomerList.DataBind();
        }
        InventoryCount();
    }

    protected void GVCustomerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowOutwardList")
        {
            string[] values = e.CommandArgument.ToString().Split(',');

            Response.Redirect("../Laxshmi/OutwardReport.aspx?CustName=" + values[0] + "&ProductName=" + values[1] + "");
        }
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DropDownList2.SelectedValue == "Months")
            {
                InventoryCount();
            }
            else
            {
                string date = DropDownList1.SelectedValue + "-" + DropDownList2.SelectedValue;

                SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetMonthList");
                if (txtCustName.Text != "")
                {
                    cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustName", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@SelectedValue", date);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Columns.Count > 0)
                {
                    GVCustomerList.DataSource = dt;
                    GVCustomerList.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);

        }
    }

    [ScriptMethod()]
    [WebMethod]
    public static List<string> GetCompanyList(string prefixText, int count)
    {
        return AutoFillCompanyName(prefixText);
    }

    public static List<string> AutoFillCompanyName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "Select DISTINCT [Companyname] from [tbl_LM_Outwarddata] where Companyname like  @Search +'%' ";

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

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet Dtt = new DataSet();
            if (DropDownList2.SelectedValue == "Months")
            {
                SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetToDayList");
                if (DropDownList1.SelectedItem.Text == "ToDay")
                {
                    cmd.Parameters.AddWithValue("@SelectedValue", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SelectedValue", DropDownList1.SelectedItem.Text);
                }
                if (txtCustName.Text != "")
                {
                    cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustName", DBNull.Value);
                }
                using (SqlDataAdapter sdas = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sdas.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sdas.Fill(Dtt);
                    }
                }
            }
            else
            {
                string date = DropDownList1.SelectedValue + "-" + DropDownList2.SelectedValue;

                SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Mode", "GetMonthList");
                if (txtCustName.Text != "")
                {
                    cmd.Parameters.AddWithValue("@CustName", txtCustName.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CustName", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@SelectedValue", date);
                using (SqlDataAdapter sdas = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sdas.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        sdas.Fill(Dtt);
                    }
                }
            }

            if (Dtt.Tables.Count > 0)
            {
                if (Dtt.Tables[0].Rows.Count > 0)
                {
                    ReportDataSource obj1 = new ReportDataSource("DataSet1", Dtt.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Add(obj1);
                    ReportViewer1.LocalReport.ReportPath = "RDLC_Reports\\OutwardReportDashboard.rdlc";
                    ReportViewer1.LocalReport.Refresh();
                    //-------- Print PDF directly without showing ReportViewer ----
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    byte[] bytePdfRep = ReportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.Buffer = true;

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment; filename=OutwardQtyReports.xls");

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
        catch (Exception)
        {

            throw;
        }
    }

}