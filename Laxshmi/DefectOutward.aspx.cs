
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

public partial class Laxshmi_DefectOutward : System.Web.UI.Page
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
            cmd.Parameters.AddWithValue("@Mode", "GetInventoryReportlist");
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
            //if (txtInwardno.Text == null || txtInwardno.Text == "")
            //{
            //    cmd.Parameters.AddWithValue("@InwardNo", DBNull.Value);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@InwardNo", txtInwardno.Text);
            //}
            if (txtReason.Text == null || txtReason.Text == "")
            {
                cmd.Parameters.AddWithValue("@DefectType", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DefectType", txtReason.Text);
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

    //Search Reason List methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetReasonList(string prefixText, int count)
    {
        return AutoFillReason(prefixText);
    }

    public static List<string> AutoFillReason(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT DefectType from tbl_LM_Defects where  " + "DefectType like '%' + @Search + '%' ";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["DefectType"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void txtReason_TextChanged(object sender, EventArgs e)
    {
        GridView();
    }

    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in GVfollowup.Rows)
        {
            // Find the CheckBox in each row and set its Checked property
            CheckBox chkRow = (CheckBox)row.FindControl("chkSelect");
            if (chkRow != null)
            {
                chkRow.Checked = chkAll.Checked;
            }
        }
    }

    protected void btnouward_Click(object sender, EventArgs e)
    {
        try
        {
            int totalDefectQty = 0;
            foreach (GridViewRow row in GVfollowup.Rows)
            {

                CheckBox chkRow = (CheckBox)row.FindControl("chkSelect");
                if (chkRow != null && chkRow.Checked)
                {
                    Label lbldDefectQty = (Label)row.FindControl("lbldDefectQty");
                    totalDefectQty += int.Parse(lbldDefectQty.Text);

                   // this.ModalPopupHistory.Show();

                }

            }
            //txtoutqantity.Text = totalDefectQty.ToString();
            //txtcustomerdefeted.Text = txtCustomerName.Text;
            //this.ModalPopupHistory.Show();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Laxshmi/DefectOutward.aspx");
    }


}