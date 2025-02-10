using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Threading;

public partial class Production_ProdDTLSProjCodeWise : System.Web.UI.Page
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
                    string Id = objcls.Decrypt(Request.QueryString["ID"].ToString());
                    hideJobCode.Value = Id;
                    Session["ProjectCode"] = Id;

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
        DataTable Dt = Cls_Main.Read_Table("SELECT Count(*) AS Count FROM [tbl_NewProductionHDR] Where ProjectCode = '" + hideJobCode.Value + "'");
        if (Dt.Rows.Count > 0)
        {
            lblCount.Text = Dt.Rows[0]["Count"].ToString();
        }

        DataTable Dts = Cls_Main.Read_Table("SELECT CustomerName,ProjectName FROM [tbl_NewProductionHDR] Where ProjectCode = '" + hideJobCode.Value + "'");
        if (Dts.Rows.Count > 0)
        {
            txtProjectCode.Text = hideJobCode.Value.ToString();
            txtProjectName.Text = Dts.Rows[0]["ProjectName"].ToString();
            txtCustoName.Text = Dts.Rows[0]["CustomerName"].ToString();
        }
    }
    private void FillGrid()
    {
        showCount();
        if (DropDownList1.SelectedValue != "ALL")
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT TOP " + DropDownList1.SelectedValue + " * FROM tbl_NewProductionHDR Where ProjectCode = '" + hideJobCode.Value + "'");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }
        else
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_NewProductionHDR Where ProjectCode = '" + hideJobCode.Value + "'");
            GVPurchase.DataSource = Dt;
            GVPurchase.DataBind();
        }
       
        
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
        if (e.CommandName == "DrawingFiles")
        {
            string fileName = Path.GetFileName(e.CommandArgument.ToString());
            Response.Redirect("~/PDF_Files/" + fileName);
        }
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
                                btndrawings.Enabled = false;
                            }
                        }
                        else
                        {
                            btndrawings.ForeColor = System.Drawing.Color.Black;
                        }
                    }

                }
                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
                gvDetails.DataSource = GetData(string.Format("select *,CONVERT(bigint,InwardQTY)-CONVERT(bigint,OutwardQTY) AS Pending from tbl_NewProductionDTLS where JobNo='{0}'",JobNo.Text));
                gvDetails.DataBind();
            }
        }
        catch (Exception ex)
        {

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


    //Search Company Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerName(prefixText);
    }

    public static List<string> AutoFillCustomerName(string prefixText)
   {
        string name = HttpContext.Current.Session["ProjectCode"].ToString();
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT DISTINCT [JobNo] FROM [tbl_NewProductionDTLS] where JobNo like '%' + @Search + '%' " +
                    " AND ProjectCode ='" + name + "' ";

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

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        if (txtCustomerName.Text != "")
        {
            string company = txtCustomerName.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM tbl_NewProductionHDR  WHERE JobNo = '" + company + "' And ProjectCode = '"+hideJobCode.Value+"' ", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
        else
        {
            FillGrid();
        }
    }

    protected void btnrefresh_Click(object sender, EventArgs e)
    {
        FillGrid();
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
        string name = HttpContext.Current.Session["ProjectCode"].ToString();
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT Distinct(ProductName) AS Code FROM [tbl_NewProductionHDR] where ProductName like '%' + @Search +'%' " +
                    " AND ProjectCode ='" + name + "' ";

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
        if (txtjobno.Text != "")
        {
            string Cpono = txtjobno.Text;

            DataTable dt = new DataTable();
            SqlDataAdapter sad = new SqlDataAdapter("SELECT * FROM tbl_NewProductionHDR  WHERE ProductName = '" + Cpono + "' And ProjectCode = '"+hideJobCode.Value+"' ", Cls_Main.Conn);
            sad.Fill(dt);
            GVPurchase.EmptyDataText = "Not Records Found";
            GVPurchase.DataSource = dt;
            GVPurchase.DataBind();
        }
        else
        {
            FillGrid();
        }
    }


    protected void DropDownList1_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void lblBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProdListGPWise.aspx");
    }
}