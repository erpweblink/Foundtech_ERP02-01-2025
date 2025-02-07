using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spire.Pdf.Exporting.XPS.Schema;

public partial class Production_SubProducts : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    string Discr = "";
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
                    string[] val = Id.Split(',');

                    hideJobNo.Value = val[0];
                    hideProdName.Value = val[1];
                    Discr = val[2];
                    FillGrid();
                }
            }
        }

    }


    private void FillGrid()
    {

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_NewProductionHDR Where JobNo = '" + hideJobNo.Value + "' AND ProductName = '" + hideProdName.Value + "'");
        if (Dt.Rows.Count > 0)
        {
            string oanum = Dt.Rows[0]["OaNumber"].ToString();
            DataTable Dta = Cls_Main.Read_Table("SELECT * FROM tbl_NewOrderAcceptanceDtls Where pono = '" + oanum + "' AND ProductName = '" + hideProdName.Value + "' AND Description = '"+ Discr + "'");
            if (Dta.Rows.Count > 0)
            {
                string Id = Dta.Rows[0]["ID"].ToString();

                DataTable Dtas = Cls_Main.Read_Table("SELECT * FROM tbl_NewSubProducts Where pono = '" + Id + "' AND ProductName = '" + hideProdName.Value + "'");
                GVPurchase.DataSource = Dtas;
                GVPurchase.DataBind();
            }
        }

        DataTable Dts = Cls_Main.Read_Table("SELECT CustomerName,ProjectName,ProductName FROM [tbl_NewProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "' AND JobNo = '" + hideJobNo.Value + "' AND ProductName = '" + hideProdName.Value + "'");
        if (Dts.Rows.Count > 0)
        {
            txtProjectCode.Text = Session["ProjectCode"].ToString();
            txtProjectName.Text = Dts.Rows[0]["ProjectName"].ToString();
            txtCustoName.Text = Dts.Rows[0]["CustomerName"].ToString();
            txtProductName.Text = Dts.Rows[0]["ProductName"].ToString();
        }
    }
}