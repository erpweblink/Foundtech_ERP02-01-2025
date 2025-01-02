using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QuotationCount(); OACount(); CustomerCount(); UserCount();
        }
    }
    protected void QuotationCount()
    {

        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_QuotationHdr where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_EnquiryData where sessionname='" + Session["UserCode"].ToString() + "'  AND IsActive=1 AND CONVERT(nvarchar(10),regdate,103)=CONVERT(nvarchar(10),GETDATE(),103) ", Cls_Main.Conn);
        //    count = Convert.ToInt16(cmd.ExecuteScalar());
        //}

        lblquotation.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void OACount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_OrderAcceptanceHdr where IsDeleted=0 ", Cls_Main.Conn);
        count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_QuotationHdr where CreatedBy='" + Session["UserCode"].ToString() + "'  AND IsDeleted=0 AND CONVERT(nvarchar(10),CreatedOn,103)=CONVERT(nvarchar(10),GETDATE(),103)", Cls_Main.Conn);
        //    count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        lbloa.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
    protected void CustomerCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
        SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_CompanyMaster where IsDeleted=0 ", Cls_Main.Conn);
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
    protected void UserCount()
    {
        Cls_Main.Conn_Open();
        int count = 0;
        //if (Session["Role"].ToString() == "Admin")
        //{
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_UserMaster where Status=1 and IsDeleted=0", Cls_Main.Conn);
            count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        //else
        //{
        //    SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_EnquiryData where sessionname='" + Session["UserCode"].ToString() + "' AND Sample=1   AND IsActive=1 AND CONVERT(nvarchar(10),regdate,103)=CONVERT(nvarchar(10),GETDATE(),103) ", Cls_Main.Conn);
        //    count = Convert.ToInt16(cmd.ExecuteScalar());
        //}
        lblusers.Text = count.ToString();
        Cls_Main.Conn_Close();
    }
}