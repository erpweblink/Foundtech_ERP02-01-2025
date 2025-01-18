using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Production_SubProducts : System.Web.UI.Page
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
                    string[] val = Id.Split(',');

                    hideJobNo.Value = val[0];
                    hideProdName.Value = val[1];
                    hideProdDiscr.Value = val[2];

                    FillGrid();
                }
            }
        }

    }


    private void FillGrid()
    {

        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM tbl_ProductionHDR Where JobNo = '" + hideJobNo.Value + "' AND ProductName = '" + hideProdName.Value + "' AND Discription = '" + hideProdDiscr.Value + "' ");
        if (Dt.Rows.Count > 0)
        {
            string oanum = Dt.Rows[0]["OaNumber"].ToString();
            DataTable Dta = Cls_Main.Read_Table("SELECT * FROM tbl_OrderAcceptanceDtls Where pono = '" + oanum + "' AND ProductName = '" + hideProdName.Value + "' AND Description = '" + hideProdDiscr.Value + "' ");
            if (Dta.Rows.Count > 0)
            {
                string Id = Dta.Rows[0]["ID"].ToString();

                DataTable Dtas = Cls_Main.Read_Table("SELECT * FROM tbl_SubProducts Where pono = '" + Id + "' AND ProductName = '" + hideProdName.Value + "' AND discr = '" + hideProdDiscr.Value + "'");
                GVPurchase.DataSource = Dtas;
                GVPurchase.DataBind();
            }
        }

        DataTable Dts = Cls_Main.Read_Table("SELECT CustomerName,ProjectName,ProductName FROM [tbl_ProductionHDR] Where ProjectCode = '" + Session["ProjectCode"].ToString() + "'");
        if (Dts.Rows.Count > 0)
        {
            txtProjectCode.Text = Session["ProjectCode"].ToString();
            txtProjectName.Text = Dts.Rows[0]["ProjectName"].ToString();
            txtCustoName.Text = Dts.Rows[0]["CustomerName"].ToString();
            txtProductName.Text = Dts.Rows[0]["ProductName"].ToString();
        }
    }
}