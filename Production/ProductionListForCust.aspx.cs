using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Windows.Threading;

public partial class Production_ProductionListForCust : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    string Name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                string Id = objcls.Decrypt(Request.QueryString["ID"].ToString());
                  Name = objcls.Decrypt(Request.QueryString["Name"].ToString());
                FillGrid();
            }
        }
    }
    private void FillGrid()
    {
        DataTable Dts = Cls_Main.Read_Table(" SELECT ProjectCode, ProjectName, CustomerName, COUNT(*) AS TotalRecords, " +
            " SUM(CAST(TotalQuantity AS INT)) AS TotalQuantitySum, SUM(CAST(CompletedQTY AS INT)) AS CompletedQuantitySum, " +
            " MAX(CAST(Stage AS INT)) AS MaxStage FROM tbl_NewProductionHDR WHERE CustomerName = '"+Name+"'" +
            " GROUP BY ProjectCode, CustomerName,  ProjectName " +
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

    protected void MainGridLoad_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int maxStage = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MaxStage"));

                Label ProjectCode = e.Row.FindControl("lblProjectCode") as Label;
                GridView GVPurchase = e.Row.FindControl("GVPurchase") as GridView;

                if (GVPurchase == null)
                {

                    return;
                }

                if (ProjectCode != null && !string.IsNullOrEmpty(ProjectCode.Text))
                {
                    var data = GetData(string.Format("SELECT * FROM tbl_NewProductionHDR WHERE ProjectCode='{0}'", ProjectCode.Text));
                    if (data != null && data.Rows.Count > 0)
                    {
                        GVPurchase.DataSource = data;
                        GVPurchase.DataBind();
                    }
                    else
                    {
                        GVPurchase.Visible = false;
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
           
        }
    }

}