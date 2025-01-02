using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesMarketing_OrderAcceptance : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    DataTable Dt_Product = new DataTable();
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
                POCode(); FillddlUsers();

                if (Request.QueryString["ID"] != null)
                {
                    string Id = objcls.Decrypt(Request.QueryString["ID"].ToString());
                    hhd.Value = Id;
                    Load_Record(Id);
                }

                if (Session["UserCode"] == null)
                {
                    Response.Redirect("../Login.aspx");
                }
                else
                {
                    txtpodate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    txtdeliverydate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                ViewState["RowNo"] = 0;

                Dt_Product.Columns.AddRange(new DataColumn[7] { new DataColumn("id"), new DataColumn("Productname"), new DataColumn("Description"), new DataColumn("Quantity"), new DataColumn("Length"), new DataColumn("Weight"), new DataColumn("TotalWeight") });
                ViewState["PurchaseOrderProduct"] = Dt_Product;

                //Edit 
                if (Request.QueryString["ID"] != null)
                {
                    ID = objcls.Decrypt(Request.QueryString["ID"].ToString());
                    btnsave.Text = "Update";
                    ShowDtlEdit();
                    hhd.Value = ID;

                }
            }
        }
    }
    protected void POCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([ID]) as maxid FROM [tbl_OrderAcceptanceHdr]", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            txtpono.Text = "FTECH/OA-" + (maxid + 1).ToString();
        }
        else
        {
            txtpono.Text = string.Empty;
        }
    }
    //Data Fetch
    private void Load_Record(string ID)
    {
        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_OrderAcceptanceHdr] AS CP LEFT JOIN tbl_UserMaster AS UM ON UM.UserCode=CP.UserName where CP.IsDeleted=0 AND CP.ID ='" + ID + "' ");
        if (Dt.Rows.Count > 0)
        {
            btnsave.Text = "Update";

            txtcompanyname.Text = Dt.Rows[0]["CustomerName"].ToString();
            txtpaymentterm.Text = Dt.Rows[0]["paymentterm"].ToString();
            txtpono.Text = Dt.Rows[0]["Pono"].ToString();
            txtserialno.Text = Dt.Rows[0]["SerialNo"].ToString();
            FillKittens();
            lblfile1.Text = Dt.Rows[0]["fileName"].ToString();

            ddlContacts.SelectedItem.Text = Dt.Rows[0]["KindAtt"].ToString();
            FillddlUsers();
            if (Dt.Rows[0]["UserCode"] != null && Dt.Rows[0]["UserCode"] != DBNull.Value)
            {
                ddlUser.SelectedValue = Dt.Rows[0]["UserCode"].ToString();
            }
            DateTime ffff2 = Convert.ToDateTime(Dt.Rows[0]["PoDate"].ToString());
            txtmobileno.Text = Dt.Rows[0]["Mobileno"].ToString();
            txtgstno.Text = Dt.Rows[0]["GSTNo"].ToString();
            txtpanno.Text = Dt.Rows[0]["PANNo"].ToString();
            txtaddress.Text = Dt.Rows[0]["BillingAddress"].ToString();
            Fillddlshippingaddress(txtcompanyname.Text);
            ddlShippingaddress.SelectedItem.Text = Dt.Rows[0]["ShippingAddress"].ToString();
            DateTime ffff3 = Convert.ToDateTime(Dt.Rows[0]["Deliverydate"].ToString());
            txtreferquotation.Text = Dt.Rows[0]["Referquotation"].ToString();
            txtremark.Text = Dt.Rows[0]["Remarks"].ToString();
            //txtagainstNumber.SelectedItem.Text = Dt.Rows[0]["AgainstNumber"].ToString();




        }
    }

    protected void ShowDtlEdit()
    {
        //divTotalPart.Visible = true;

        SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM [tbl_OrderAcceptanceDtls] WHERE Pono='" + txtpono.Text + "'", Cls_Main.Conn);
        DataTable DTCOMP = new DataTable();
        Da.Fill(DTCOMP);

        int count = 0;
        if (DTCOMP.Rows.Count > 0)
        {
            if (Dt_Product.Columns.Count < 0)
            {
                Show_Grid();
            }

            for (int i = 0; i < DTCOMP.Rows.Count; i++)
            {
                Dt_Product.Rows.Add(count, DTCOMP.Rows[i]["Productname"].ToString(), DTCOMP.Rows[i]["Description"].ToString(), DTCOMP.Rows[i]["HSN"].ToString(), DTCOMP.Rows[i]["Quantity"].ToString(), DTCOMP.Rows[i]["Units"].ToString(), DTCOMP.Rows[i]["Rate"].ToString(), DTCOMP.Rows[i]["Total"].ToString(), DTCOMP.Rows[i]["CGSTPer"].ToString(), DTCOMP.Rows[i]["CGSTAmt"].ToString(), DTCOMP.Rows[i]["SGSTPer"].ToString(), DTCOMP.Rows[i]["SGSTAmt"].ToString(), DTCOMP.Rows[i]["IGSTPer"].ToString(), DTCOMP.Rows[i]["IGSTAmt"].ToString(), DTCOMP.Rows[i]["Discountpercentage"].ToString(), DTCOMP.Rows[i]["DiscountAmount"].ToString(), DTCOMP.Rows[i]["Alltotal"].ToString(), DTCOMP.Rows[i]["Weight"].ToString());
                count = count + 1;
            }
        }

        dgvMachineDetails.EmptyDataText = "No Data Found";
        dgvMachineDetails.DataSource = Dt_Product;
        dgvMachineDetails.DataBind();
    }

    private void Show_Grid()
    {
        //divTotalPart.Visible = true;
        DataTable Dt = (DataTable)ViewState["PurchaseOrderProduct"];
        // Dt.Rows.Add(ViewState["RowNo"], txtProduct.Text, txtdescription.Text.Trim(), txthsnsac.Text.Trim(), txtquantity.Text, txtunit.Text, txtrate.Text, txttotal.Text, txtCGST.Text, txtCGSTamt.Text, txtSGST.Text, txtSGSTamt.Text, txtIGST.Text, txtIGSTamt.Text, txtdiscount.Text, txtdiscountamt.Text, txtgrandtotal.Text, txtweight.Text);
        Dt.Rows.Add(ViewState["RowNo"], txtProduct.Text, txtdescription.Text.Trim(), txtquantity.Text, txtlength.Text, txtWeight.Text, txtTotalWeight.Text);
        ViewState["PurchaseOrderProduct"] = Dt;
        txtProduct.Text = string.Empty;
        txtdescription.Text = string.Empty;
        txtquantity.Text = "0";
        txtlength.Text = "0.00";
        txtWeight.Text = "0.00";
        txtTotalWeight.Text = "0.00";

        dgvMachineDetails.DataSource = (DataTable)ViewState["PurchaseOrderProduct"];
        dgvMachineDetails.DataBind();
    }


    private void FillddlUsers()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select Username,UserCode from tbl_UserMaster where  Status=1 and IsDeleted=0", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "UserCode";
            ddlUser.DataTextField = "Username";
            ddlUser.DataBind();
            ddlUser.Items.Insert(0, "-- Select User Name--");
        }
    }
    private void FillKittens()
    {
        SqlDataAdapter ad = new SqlDataAdapter("select CCD.ID,CCD.Name from tbl_CompanyMaster AS CM Inner JOIN tbl_CompanyContactDetails AS CCD ON CCD.CompanyCode=CM.CompanyCode where Companyname='" + txtcompanyname.Text.Trim() + "' ", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlContacts.DataSource = dt;
            ddlContacts.DataValueField = "ID";
            ddlContacts.DataTextField = "Name";
            ddlContacts.DataBind();
            ddlContacts.Items.Insert(0, "-- Select Kindd Att --");
        }
    }

    private void Fillddlshippingaddress(string ID)
    {

        SqlDataAdapter ad = new SqlDataAdapter("SELECT SA.ShippingAddress FROM tbl_ShippingAddress  AS SA INNER JOIN tbl_CompanyMaster AS CM ON CM.ID=SA.c_id where Companyname='" + ID + "'", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlShippingaddress.DataSource = dt;
            ddlShippingaddress.DataValueField = "ShippingAddress";
            ddlShippingaddress.DataTextField = "ShippingAddress";
            ddlShippingaddress.DataBind();
        }
        else
        {
            ddlShippingaddress.DataSource = null;
            ddlShippingaddress.DataBind();
            ddlShippingaddress.Items.Insert(0, "-Select Shipping Address-");
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
                com.CommandText = "Select DISTINCT [Companyname] from [tbl_CompanyMaster] where " + "Companyname like @Search + '%' and IsDeleted=0";

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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcompanyname.Text == " --- Select Company --- " || ddlContacts.SelectedItem.Text == "-- Select Kindd Att --" || ddlContacts.SelectedItem.Text == "" || txtpodate.Text == "" || txtdeliverydate.Text == "" || txtserialno.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kindly Enter the Data..!!')", true);
            }
            else
            {
                if (dgvMachineDetails.Rows.Count > 0)
                {
                    if (btnsave.Text == "Save")
                    {
                        Cls_Main.Conn_Open();
                        SqlCommand cmd = new SqlCommand("SP_OrderAcceptanceHdr", Cls_Main.Conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerName", txtcompanyname.Text);
                        cmd.Parameters.AddWithValue("@Pono", txtpono.Text);
                        cmd.Parameters.AddWithValue("@SerialNo", txtserialno.Text);
                        cmd.Parameters.AddWithValue("@KindAtt", ddlContacts.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@PoDate", txtpodate.Text);
                        cmd.Parameters.AddWithValue("@Mobileno", txtmobileno.Text);
                        cmd.Parameters.AddWithValue("@EmailID", txtemail.Text);

                        cmd.Parameters.AddWithValue("@GSTNo", txtgstno.Text);
                        cmd.Parameters.AddWithValue("@PANNo", txtpanno.Text);
                        if (ddlUser.SelectedValue == "-- Select User Name--")
                        {
                            cmd.Parameters.AddWithValue("@UserName", Session["UserCode"].ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@UserName", ddlUser.SelectedValue);
                        }

                        if (ViewState["attachment"] != null)
                        {
                            byte[] fileContent = (byte[])ViewState["attachment"];
                            cmd.Parameters.AddWithValue("@fileName", lblfile1.Text);
                            string[] pdffilename = lblfile1.Text.Split('.');
                            string pdffilename1 = pdffilename[0];
                            string filenameExt = pdffilename[1];

                            string filePath = Server.MapPath("~/PDF_Files/") + pdffilename1 + "." + filenameExt;

                            // Save the file to the specified path
                            System.IO.File.WriteAllBytes(filePath, fileContent);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fileName", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@BillingAddress", txtaddress.Text);
                        if (ddlShippingaddress.SelectedItem.Text == "-Select Shipping Address-")
                        {
                            cmd.Parameters.AddWithValue("@ShippingAddress", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ShippingAddress", ddlShippingaddress.SelectedItem.Text);
                        }
                        cmd.Parameters.AddWithValue("@Deliverydate", txtdeliverydate.Text);
                        cmd.Parameters.AddWithValue("@Referquotation", txtreferquotation.Text);
                        cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
                        //cmd.Parameters.AddWithValue("@CGST_Amt", txt_cgstamt.Text);
                        //cmd.Parameters.AddWithValue("@SGST_Amt", txt_sgstamt.Text);
                        //cmd.Parameters.AddWithValue("@IGST_Amt", txt_igstamt.Text);
                        //cmd.Parameters.AddWithValue("@Total_Price", txt_grandTotal.Text);
                        //cmd.Parameters.AddWithValue("@Paymentterm", txtpaymentterm.Text);
                        //cmd.Parameters.AddWithValue("@Totalinword", lbl_total_amt_Value.Text);
                        cmd.Parameters.AddWithValue("@IsDeleted", '0');
                        cmd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                        cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Action", "Save");
                        cmd.ExecuteNonQuery();
                        Cls_Main.Conn_Close();
                        Cls_Main.Conn_Dispose();

                        //Save Product Details 
                        foreach (GridViewRow grd1 in dgvMachineDetails.Rows)
                        {
                            string lblproduct = (grd1.FindControl("lblproduct") as Label).Text;
                            string lblDescription = (grd1.FindControl("lblDescription") as Label).Text;
                            string lblhsn = (grd1.FindControl("lblhsn") as Label).Text;
                            string lblQuantity = (grd1.FindControl("lblQuantity") as Label).Text;
                            string lblUnit = (grd1.FindControl("lblUnit") as Label).Text;
                            string lblRate = (grd1.FindControl("lblRate") as Label).Text;
                            string lblTotal = (grd1.FindControl("lblTotal") as Label).Text;
                            string lblCGSTPer = (grd1.FindControl("lblCGSTPer") as Label).Text;
                            string lblCGST = (grd1.FindControl("lblCGST") as Label).Text;
                            string lblSGSTPer = (grd1.FindControl("lblSGSTPer") as Label).Text;
                            string lblSGST = (grd1.FindControl("lblSGST") as Label).Text;
                            string lblIGSTPer = (grd1.FindControl("lblIGSTPer") as Label).Text;
                            string lblIGST = (grd1.FindControl("lblIGST") as Label).Text;
                            string lblDiscount = (grd1.FindControl("lblDiscount") as Label).Text;
                            string lblDiscountAmount = (grd1.FindControl("lblDiscountAmount") as Label).Text;
                            string lblAlltotal = (grd1.FindControl("lblAlltotal") as Label).Text;
                            string lblWeight = (grd1.FindControl("lblWeight") as Label).Text;


                            Cls_Main.Conn_Open();
                            SqlCommand cmdd = new SqlCommand("INSERT INTO tbl_OrderAcceptanceDtls (Pono,Productname,Description,HSN,Quantity,Units,Rate,CGSTPer,CGSTAmt,SGSTPer,SGSTAmt,IGSTPer,IGSTAmt,Total,Discountpercentage,DiscountAmount,Alltotal,Weight,CreatedOn) VALUES(@Pono,@Productname,@Description,@HSN,@Quantity,@Units,@Rate,@CGSTPer,@CGSTAmt,@SGSTPer,@SGSTAmt,@IGSTPer,@IGSTAmt,@Total,@Discountpercentage,@DiscountAmount,@Alltotal,@lblWeight,@CreatedOn)", Cls_Main.Conn);
                            cmdd.Parameters.AddWithValue("@Pono", txtpono.Text);
                            cmdd.Parameters.AddWithValue("@Productname", lblproduct);
                            cmdd.Parameters.AddWithValue("@Description", lblDescription);
                            cmdd.Parameters.AddWithValue("@HSN", lblhsn);
                            cmdd.Parameters.AddWithValue("@Quantity", lblQuantity);
                            cmdd.Parameters.AddWithValue("@Units", lblUnit);
                            cmdd.Parameters.AddWithValue("@Rate", lblRate);
                            cmdd.Parameters.AddWithValue("@CGSTPer", lblCGSTPer);
                            cmdd.Parameters.AddWithValue("@CGSTAmt", lblCGST);
                            cmdd.Parameters.AddWithValue("@SGSTPer", lblSGSTPer);
                            cmdd.Parameters.AddWithValue("@SGSTAmt", lblSGST);
                            cmdd.Parameters.AddWithValue("@IGSTPer", lblIGSTPer);
                            cmdd.Parameters.AddWithValue("@IGSTAmt", lblIGST);
                            cmdd.Parameters.AddWithValue("@Total", lblTotal);
                            cmdd.Parameters.AddWithValue("@Discountpercentage", lblDiscount);
                            cmdd.Parameters.AddWithValue("@DiscountAmount", lblDiscountAmount);
                            cmdd.Parameters.AddWithValue("@Alltotal", lblAlltotal);
                            cmdd.Parameters.AddWithValue("@lblWeight", lblWeight);
                            cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                            cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                            cmdd.ExecuteNonQuery();
                            Cls_Main.Conn_Close();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Acceptance Save Successfully..!!');window.location='OAList.aspx'; ", true);
                    }
                    else if (btnsave.Text == "Update")
                    {
                        DateTime Date = DateTime.Now;
                        Cls_Main.Conn_Open();

                        SqlCommand cmd = new SqlCommand("SP_OrderAcceptanceHdr", Cls_Main.Conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerName", txtcompanyname.Text);
                        cmd.Parameters.AddWithValue("@Pono", txtpono.Text);
                        cmd.Parameters.AddWithValue("@SerialNo", txtserialno.Text);
                        cmd.Parameters.AddWithValue("@KindAtt", ddlContacts.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@PoDate", txtpodate.Text);
                        cmd.Parameters.AddWithValue("@Mobileno", txtmobileno.Text);
                        cmd.Parameters.AddWithValue("@EmailID", txtemail.Text);
                        cmd.Parameters.AddWithValue("@GSTNo", txtgstno.Text);
                        cmd.Parameters.AddWithValue("@PANNo", txtpanno.Text);
                        cmd.Parameters.AddWithValue("@BillingAddress", txtaddress.Text);
                        cmd.Parameters.AddWithValue("@ShippingAddress", ddlShippingaddress.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@Deliverydate", txtdeliverydate.Text);
                        cmd.Parameters.AddWithValue("@Referquotation", txtreferquotation.Text);
                        cmd.Parameters.AddWithValue("@Remarks", txtremark.Text);
                        //cmd.Parameters.AddWithValue("@CGST_Amt", txt_cgstamt.Text);
                        //cmd.Parameters.AddWithValue("@SGST_Amt", txt_sgstamt.Text);
                        //cmd.Parameters.AddWithValue("@IGST_Amt", txt_igstamt.Text);
                        //cmd.Parameters.AddWithValue("@Total_Price", txt_grandTotal.Text);
                        //cmd.Parameters.AddWithValue("@Totalinword", lbl_total_amt_Value.Text);
                        cmd.Parameters.AddWithValue("@Paymentterm", txtpaymentterm.Text);
                        if (ddlUser.SelectedValue == "-- Select User Name--")
                        {
                            cmd.Parameters.AddWithValue("@UserName", Session["UserCode"].ToString());
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@UserName", ddlUser.SelectedValue);
                        }
                        cmd.Parameters.AddWithValue("@IsDeleted", '0');
                        if (ViewState["attachment"] != null)
                        {
                            byte[] fileContent = (byte[])ViewState["attachment"];
                            cmd.Parameters.AddWithValue("@fileName", lblfile1.Text);
                            string[] pdffilename = lblfile1.Text.Split('.');
                            string pdffilename1 = pdffilename[0];
                            string filenameExt = pdffilename[1];

                            string filePath = Server.MapPath("~/PDF_Files/") + pdffilename1 + "." + filenameExt;

                            // Save the file to the specified path
                            System.IO.File.WriteAllBytes(filePath, fileContent);

                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@fileName", lblfile1.Text);
                        }
                        cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserCode"].ToString());
                        cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Action", "Update");
                        cmd.ExecuteNonQuery();
                        Cls_Main.Conn_Close();

                        //DELETE DETAILS DATA FOR UPDATE
                        Cls_Main.Conn_Open();
                        SqlCommand cmddelete = new SqlCommand("DELETE FROM tbl_OrderAcceptanceDtls WHERE Pono=@Pono", Cls_Main.Conn);
                        cmddelete.Parameters.AddWithValue("@Pono", txtpono.Text);
                        cmddelete.ExecuteNonQuery();
                        Cls_Main.Conn_Close();

                        //Save Product Details 
                        foreach (GridViewRow grd1 in dgvMachineDetails.Rows)
                        {
                            string lblproduct = (grd1.FindControl("lblproduct") as Label).Text;
                            string lblDescription = (grd1.FindControl("lblDescription") as Label).Text;
                            string lblhsn = (grd1.FindControl("lblhsn") as Label).Text;
                            string lblQuantity = (grd1.FindControl("lblQuantity") as Label).Text;
                            string lblUnit = (grd1.FindControl("lblUnit") as Label).Text;
                            string lblRate = (grd1.FindControl("lblRate") as Label).Text;
                            string lblTotal = (grd1.FindControl("lblTotal") as Label).Text;
                            string lblCGSTPer = (grd1.FindControl("lblCGSTPer") as Label).Text;
                            string lblCGST = (grd1.FindControl("lblCGST") as Label).Text;
                            string lblSGSTPer = (grd1.FindControl("lblSGSTPer") as Label).Text;
                            string lblSGST = (grd1.FindControl("lblSGST") as Label).Text;
                            string lblIGSTPer = (grd1.FindControl("lblIGSTPer") as Label).Text;
                            string lblIGST = (grd1.FindControl("lblIGST") as Label).Text;
                            string lblDiscount = (grd1.FindControl("lblDiscount") as Label).Text;
                            string lblDiscountAmount = (grd1.FindControl("lblDiscountAmount") as Label).Text;
                            string lblAlltotal = (grd1.FindControl("lblAlltotal") as Label).Text;
                            string lblWeight = (grd1.FindControl("lblWeight") as Label).Text;



                            Cls_Main.Conn_Open();
                            SqlCommand cmdd = new SqlCommand("INSERT INTO tbl_OrderAcceptanceDtls (Pono,Productname,Description,HSN,Quantity,Units,Rate,CGSTPer,CGSTAmt,SGSTPer,SGSTAmt,IGSTPer,IGSTAmt,Total,Discountpercentage,DiscountAmount,Alltotal,Weight,CreatedOn) VALUES(@Pono,@Productname,@Description,@HSN,@Quantity,@Units,@Rate,@CGSTPer,@CGSTAmt,@SGSTPer,@SGSTAmt,@IGSTPer,@IGSTAmt,@Total,@Discountpercentage,@DiscountAmount,@Alltotal,@lblWeight,@CreatedOn)", Cls_Main.Conn);
                            cmdd.Parameters.AddWithValue("@Pono", txtpono.Text);
                            cmdd.Parameters.AddWithValue("@Productname", lblproduct);
                            cmdd.Parameters.AddWithValue("@Description", lblDescription);
                            cmdd.Parameters.AddWithValue("@HSN", lblhsn);
                            cmdd.Parameters.AddWithValue("@Quantity", lblQuantity);
                            cmdd.Parameters.AddWithValue("@Units", lblUnit);
                            cmdd.Parameters.AddWithValue("@Rate", lblRate);
                            cmdd.Parameters.AddWithValue("@CGSTPer", lblCGSTPer);
                            cmdd.Parameters.AddWithValue("@CGSTAmt", lblCGST);
                            cmdd.Parameters.AddWithValue("@SGSTPer", lblSGSTPer);
                            cmdd.Parameters.AddWithValue("@SGSTAmt", lblSGST);
                            cmdd.Parameters.AddWithValue("@IGSTPer", lblIGSTPer);
                            cmdd.Parameters.AddWithValue("@IGSTAmt", lblIGST);
                            cmdd.Parameters.AddWithValue("@Total", lblTotal);
                            cmdd.Parameters.AddWithValue("@Discountpercentage", lblDiscount);
                            cmdd.Parameters.AddWithValue("@DiscountAmount", lblDiscountAmount);
                            cmdd.Parameters.AddWithValue("@Alltotal", lblAlltotal);
                            cmdd.Parameters.AddWithValue("@lblWeight", lblWeight);
                            cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                            cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                            cmdd.ExecuteNonQuery();
                            Cls_Main.Conn_Close();
                        }
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Acceptance Update Successfully..!!');window.location='OAList.aspx'; ", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kindly Enter the Product Data..!!')", true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void txtquantity_TextChanged(object sender, EventArgs e)
    {

        string qty = txtquantity.Text;
        string val = txtWeight.Text;

        if (qty != "" && qty != "0" && val != "" && val != "0")
        {
            var TotalWeight = Convert.ToDecimal(txtquantity.Text.Trim()) * Convert.ToDecimal(txtWeight.Text.Trim());

            txtTotalWeight.Text = Convert.ToString(TotalWeight);
        }
    }

    //protected void txtCGST_TextChanged(object sender, EventArgs e)
    //{
    //    var TotalAmt = Convert.ToDecimal(txtquantity.Text.Trim()) * Convert.ToDecimal(txtrate.Text.Trim());

    //    decimal total;

    //    decimal Percentage = Convert.ToDecimal(txtCGST.Text);

    //    total = (TotalAmt * Percentage / 100);

    //    txtCGSTamt.Text = total.ToString();

    //    txtSGSTamt.Text = txtCGSTamt.Text;

    //    txtSGST.Text = txtCGST.Text;

    //    var GrandTotal = Convert.ToDecimal(txttotal.Text.Trim()) + Convert.ToDecimal(txtCGSTamt.Text.Trim()) + Convert.ToDecimal(txtSGSTamt.Text.Trim());
    //    txtgrandtotal.Text = GrandTotal.ToString();


    //    if (txtCGST.Text == "0" || txtCGST.Text == "")
    //    {
    //        txtIGST.Enabled = true;
    //        txtIGST.Text = "0";
    //    }
    //    else
    //    {
    //        txtIGST.Enabled = false;
    //        txtIGST.Text = "0";
    //    }
    //}

    //protected void txtSGST_TextChanged(object sender, EventArgs e)
    //{
    //    var TotalAmt = Convert.ToDecimal(txtquantity.Text.Trim()) * Convert.ToDecimal(txtrate.Text.Trim());

    //    decimal total;

    //    decimal Percentage = Convert.ToDecimal(txtSGST.Text);

    //    total = (TotalAmt * Percentage / 100);

    //    txtSGSTamt.Text = total.ToString();

    //    txtCGSTamt.Text = txtSGSTamt.Text;

    //    txtCGST.Text = txtSGST.Text;

    //    var GrandTotal = Convert.ToDecimal(txttotal.Text.Trim()) + Convert.ToDecimal(txtCGSTamt.Text.Trim()) + Convert.ToDecimal(txtSGSTamt.Text.Trim());
    //    txtgrandtotal.Text = GrandTotal.ToString();

    //    if (txtSGST.Text == "0" || txtSGST.Text == "")
    //    {
    //        txtIGST.Enabled = true;
    //        txtIGST.Text = "0";
    //    }
    //    else
    //    {
    //        txtIGST.Enabled = false;
    //        txtIGST.Text = "0";
    //    }
    //}

    //protected void txtIGST_TextChanged(object sender, EventArgs e)
    //{
    //    var TotalAmt = Convert.ToDecimal(txtquantity.Text.Trim()) * Convert.ToDecimal(txtrate.Text.Trim());

    //    decimal total;

    //    decimal Percentage = Convert.ToDecimal(txtIGST.Text);

    //    total = (TotalAmt * Percentage / 100);

    //    txtIGSTamt.Text = total.ToString();

    //    var GrandTotal = Convert.ToDecimal(txttotal.Text.Trim()) + Convert.ToDecimal(txtIGSTamt.Text.Trim());
    //    txtgrandtotal.Text = GrandTotal.ToString();


    //    if (txtIGST.Text == "0" || txtIGST.Text == "")
    //    {
    //        txtCGST.Enabled = true;
    //        txtCGST.Text = "0";
    //        txtSGST.Enabled = true;
    //        txtSGST.Text = "0";
    //    }
    //    else
    //    {
    //        txtCGST.Enabled = false;
    //        txtCGST.Text = "0";
    //        txtSGST.Enabled = false;
    //        txtSGST.Text = "0";
    //    }
    //}

    //protected void txtdiscount_TextChanged(object sender, EventArgs e)
    //{
    //    decimal DiscountAmt;
    //    decimal val1 = Convert.ToDecimal(txtgrandtotal.Text);
    //    decimal val2 = Convert.ToDecimal(txtdiscount.Text);
    //    DiscountAmt = (val1 * val2 / 100);
    //    txtgrandtotal.Text = (val1 - DiscountAmt).ToString();

    //    txtdiscountamt.Text = DiscountAmt.ToString();
    //}

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        if (txtquantity.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill Quantity and Price !!!');", true);
            txtquantity.Focus();
        }
        else
        {
            Show_Grid();
        }
    }

    protected void dgvMachineDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvMachineDetails.EditIndex = e.NewEditIndex;
        dgvMachineDetails.DataSource = (DataTable)ViewState["PurchaseOrderProduct"];
        dgvMachineDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }


    string lblproduct, Description, Quantity, Weight, TotalWeight;

    protected void dgvMachineDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton lnkedit = e.Row.FindControl("btn_edit") as LinkButton;


                //    lblproduct = (e.Row.FindControl("txtproduct") as TextBox).Text;
                //    Description = (e.Row.FindControl("txtDescription") as TextBox).Text;

                //    Quantity = (e.Row.FindControl("txtQuantity") as TextBox).Text;
                //    Weight = (e.Row.FindControl("txtWeight") as TextBox).Text;
                //    TotalWeight = (e.Row.FindControl("txtTotalWeight") as TextBox).Text;


            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void gv_update_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        string Product = ((TextBox)row.FindControl("Product")).Text;
        string Description = ((TextBox)row.FindControl("Description")).Text;
        string Quantity = ((TextBox)row.FindControl("Quantity")).Text;
        string Lenght = ((TextBox)row.FindControl("Lenght")).Text;
        string Weight = ((TextBox)row.FindControl("Weight")).Text;
        string TotalWeight = ((TextBox)row.FindControl("TotalWeight")).Text;

        DataTable Dt = ViewState["PurchaseOrderProduct"] as DataTable;
        Dt.Rows[row.RowIndex]["Productname"] = Product;
        Dt.Rows[row.RowIndex]["Description"] = Description;

        Dt.Rows[row.RowIndex]["Quantity"] = Quantity;
        Dt.Rows[row.RowIndex]["Length"] = Lenght;

        Dt.Rows[row.RowIndex]["Weight"] = Weight;
        Dt.Rows[row.RowIndex]["TotalWeight"] = TotalWeight;
        Dt.AcceptChanges();
        ViewState["PurchaseOrderProduct"] = Dt;
        dgvMachineDetails.EditIndex = -1;
        dgvMachineDetails.DataSource = (DataTable)ViewState["PurchaseOrderProduct"];
        dgvMachineDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void gv_cancel_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        string Product = ((TextBox)row.FindControl("Product")).Text;
        string Description = ((TextBox)row.FindControl("Description")).Text;
        string Quantity = ((TextBox)row.FindControl("Quantity")).Text;
        string Lenght = ((TextBox)row.FindControl("Lenght")).Text;
        string Weight = ((TextBox)row.FindControl("Weight")).Text;
        string TotalWeight = ((TextBox)row.FindControl("TotalWeight")).Text;

        DataTable Dt = ViewState["PurchaseOrderProduct"] as DataTable;
        Dt.Rows[row.RowIndex]["Productname"] = Product;
        Dt.Rows[row.RowIndex]["Description"] = Description;

        Dt.Rows[row.RowIndex]["Quantity"] = Quantity;
        Dt.Rows[row.RowIndex]["Length"] = Lenght;

        Dt.Rows[row.RowIndex]["Weight"] = Weight;
        Dt.Rows[row.RowIndex]["TotalWeight"] = TotalWeight;
        Dt.AcceptChanges();
        ViewState["PurchaseOrderProduct"] = Dt;
        dgvMachineDetails.EditIndex = -1;
        dgvMachineDetails.DataSource = (DataTable)ViewState["PurchaseOrderProduct"];
        dgvMachineDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt = ViewState["PurchaseOrderProduct"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["PurchaseOrderProduct"] = dt;
        dgvMachineDetails.DataSource = (DataTable)ViewState["PurchaseOrderProduct"];
        dgvMachineDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Order Acceptance Delete Succesfully !!!');", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    //CONVRT NUMBERS TO WORD START

    public static string ConvertNumbertoWords(string numbers)
    {
        Boolean paisaconversion = false;
        var pointindex = numbers.ToString().IndexOf(".");
        var paisaamt = 0;
        if (pointindex > 0)
            paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));

        int number = Convert.ToInt32(numbers);

        if (number == 0) return "Zero";
        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
        int[] num = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (number < 0)
        {
            sb.Append("Minus ");
            number = -number;
        }
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };
        num[0] = number % 1000; // units
        num[1] = number / 1000;
        num[2] = number / 100000;
        num[1] = num[1] - 100 * num[2]; // thousands
        num[3] = number / 10000000; // crores
        num[2] = num[2] - 100 * num[3]; // lakhs
        for (int i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10; // ones
            t = num[i] / 10;
            h = num[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }

        if (paisaamt == 0 && paisaconversion == false)
        {
            sb.Append("Rupees ");
        }
        else if (paisaamt > 0)
        {
            var paisatext = ConvertNumbertoWords(Convert.ToString(paisaamt));
            sb.AppendFormat("Rupees {0} paise", paisatext);
        }
        return sb.ToString().TrimEnd();
    }
    private static String ones(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = "";
        switch (_Number)
        {

            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    private static String tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }
    private static String ConvertWholeNumber(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX  
            bool isDone = false;//test if already translated  
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))  
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric  
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping  
                String place = "";//digit grouping name:hundres,thousand,etc...  
                switch (numDigits)
                {
                    case 1://ones' range  

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range  
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range  
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range  
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range  
                    case 8:
                        pos = (numDigits % 6) + 1;
                        place = " Lac ";
                        break;
                    case 9:
                        pos = (numDigits % 8) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range  
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...  
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)  
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }

                    //check for trailing zeros  
                    //if (beginsZero) word = " and " + word.Trim();  
                }
                //ignore digit grouping names  
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }
        return word.Trim();
    }
    private static String ConvertToWords(String numb)
    {
        String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = "Only";
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = "and";// just to separate whole numbers from points/cents  
                    endStr = "Paisa " + endStr;//Cents  
                    pointStr = ConvertDecimals(points);
                }
            }
            val = String.Format("{0} {1}{2} {3}", ConvertNumbertoWords(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch { }
        return val;
    }
    private static String ConvertDecimals(String number)
    {
        String cd = "", digit = "", engOne = "";
        for (int i = 0; i < number.Length; i++)
        {
            digit = number[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cd += " " + engOne;
        }
        return cd;
    }

    protected void lnkBtmNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Masters/CompanyMaster.aspx");
    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
        Calculations(row);
    }

    public void Calculations(GridViewRow row)
    {

        TextBox Qty = (TextBox)row.FindControl("Quantity");
        TextBox Weight = (TextBox)row.FindControl("Weight");
        TextBox TotalWeight = (TextBox)row.FindControl("TotalWeight");

        if (Qty.Text != "" && Qty.Text != "0" && Weight.Text != "" && Weight.Text != "0")
        {
            var TotalWeights = Convert.ToDecimal(Qty.Text.Trim()) * Convert.ToDecimal(Weight.Text.Trim());

            TotalWeight.Text = string.Format("{0:0.00}", TotalWeights);
        }
    }

    //protected void txtRate_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    //protected void txtCGSTPer_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    //protected void txtSGSTPer_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    //protected void txtIGSTPer_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    //protected void txtDiscount_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("OAList.aspx");
    }

    protected void txtcompanyname_TextChanged(object sender, EventArgs e)
    {
        SqlDataAdapter Da = new SqlDataAdapter("select CM.PrimaryEmailID,CM.Companyname,CM.Companypancard,CM.PaymentTerm,CM.StateCode,CC.Name,CC.Number,CM.PrimaryEmailID,CM.GSTno,CM.Billingaddress,Shippingaddress from tbl_CompanyMaster AS CM left join tbl_CompanyContactDetails AS CC on CM.CompanyCode=CC.CompanyCode WHERE Companyname='" + txtcompanyname.Text + "'", Cls_Main.Conn);
        DataTable Dt = new DataTable();
        Da.Fill(Dt);
        if (Dt.Rows.Count > 0)
        {
            Fillddlshippingaddress(Dt.Rows[0]["Companyname"].ToString());
            Fillddlshippingaddress(Dt.Rows[0]["Companyname"].ToString());
            txtmobileno.Text = Dt.Rows[0]["Number"].ToString();
            txtemail.Text = Dt.Rows[0]["PrimaryEmailID"].ToString();
            txtgstno.Text = Dt.Rows[0]["GSTno"].ToString();
            txtaddress.Text = Dt.Rows[0]["Billingaddress"].ToString();
            txtpaymentterm.Text = Dt.Rows[0]["PaymentTerm"].ToString();
            txtpanno.Text = Dt.Rows[0]["Companypancard"].ToString();
            hhdstate.Value = Dt.Rows[0]["StateCode"].ToString();
            FillKittens();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("OAList.aspx");
    }

    //protected void txtOquantity_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}

    //protected void txtSQuantity_TextChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = (sender as TextBox).NamingContainer as GridViewRow;
    //    Calculations(row);
    //}


    //protected void txtrate_TextChanged(object sender, EventArgs e)
    //{
    //    var TotalAmt = Convert.ToDecimal(txtquantity.Text.Trim()) * Convert.ToDecimal(txtrate.Text.Trim());
    //    txttotal.Text = Convert.ToString(TotalAmt);
    //    decimal total;
    //    decimal Percentage;
    //    if (txtIGST.Text == null || txtIGST.Text == "" || txtIGST.Text == "0")
    //    {
    //        Percentage = Convert.ToDecimal(txtCGST.Text);
    //        total = (TotalAmt * Percentage / 100);

    //        txtCGSTamt.Text = total.ToString();

    //        txtSGSTamt.Text = txtCGSTamt.Text;

    //        txtSGST.Text = txtCGST.Text;
    //        var GrandTotal = Convert.ToDecimal(txttotal.Text.Trim()) + Convert.ToDecimal(txtCGSTamt.Text.Trim()) + Convert.ToDecimal(txtSGSTamt.Text.Trim());
    //        txtgrandtotal.Text = GrandTotal.ToString();
    //    }
    //    else
    //    {
    //        Percentage = Convert.ToDecimal(txtIGST.Text);
    //        total = (TotalAmt * Percentage / 100);

    //        txtIGSTamt.Text = total.ToString();
    //        var GrandTotal = Convert.ToDecimal(txttotal.Text.Trim()) + Convert.ToDecimal(txtIGSTamt.Text.Trim());
    //        txtgrandtotal.Text = GrandTotal.ToString();
    //    }
    //}

    protected void uploadfile_Click(object sender, EventArgs e)
    {
        if (AttachmentUpload.HasFile)
        {
            // Set EPPlus License Context for non-commercial use
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            // Read the file directly from the uploaded byte stream (no need to save it to the file system)
            byte[] fileContent;
            using (Stream fs = AttachmentUpload.PostedFile.InputStream)
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fileContent = br.ReadBytes((int)fs.Length);
                }
            }

            using (var package = new OfficeOpenXml.ExcelPackage(new MemoryStream(fileContent)))
            {
                var worksheet = package.Workbook.Worksheets[0]; 
                var rowCount = worksheet.Dimension.End.Row;
                var colCount = worksheet.Dimension.End.Column;

               
                DataTable Dt = (DataTable)ViewState["PurchaseOrderProduct"];
                int Count = 1;
                for (int row = 2; row <= rowCount; row++) 
                {
                    DataRow dataRow = Dt.NewRow();
                    dataRow["id"] = Count;
                    dataRow["Productname"] = worksheet.Cells[row, 2].Text;
                    dataRow["Description"] = worksheet.Cells[row, 3].Text;
                    dataRow["Quantity"] = worksheet.Cells[row, 4].Text;
                    dataRow["Length"] = worksheet.Cells[row, 5].Text;
                    dataRow["Weight"] = worksheet.Cells[row, 6].Text;
                    dataRow["TotalWeight"] = worksheet.Cells[row, 7].Text;

                    Dt.Rows.Add(dataRow);
                    Count++;
                }
                ViewState["PurchaseOrderProduct"] = Dt;
                dgvMachineDetails.DataSource = ViewState["PurchaseOrderProduct"];
                dgvMachineDetails.DataBind();
            }
        }
    }


}