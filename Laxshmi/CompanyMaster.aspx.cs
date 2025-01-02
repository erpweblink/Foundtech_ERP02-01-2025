
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Laxshmi_CompanyMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    DataTable Dt_Component = new DataTable();
    CommonCls objcls = new CommonCls();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserCode"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            ViewState["CODE"] = null;
            if (!IsPostBack)
            {



                ViewState["RowNo"] = 0;
                Dt_Component.Columns.AddRange(new DataColumn[6] { new DataColumn("id"), new DataColumn("Name"), new DataColumn("Number"), new DataColumn("EmailID"), new DataColumn("Department"), new DataColumn("Designation") });
                ViewState["ContactDetails"] = Dt_Component;

                ViewState["RowNo"] = 0;
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("id"), new DataColumn("ShippingAddress"), new DataColumn("ShipLocation"), new DataColumn("ShipPincode"), new DataColumn("ShipStatecode") });
                ViewState["AddressData"] = dt;

                FillddlState();
                CompanyCode();
                fillddlCountryCode();
                if (Request.QueryString["ID"] != null)
                {
                    string Id = objcls.Decrypt(Request.QueryString["ID"].ToString());
                    hhd.Value = Id;
                    Load_Record(Id);
                    btnsave.Text = "Update";
                    txtcompanycode.ReadOnly = true;
                    ShowDtlEdit();
                }


            }
        }
    }
    private void FillddlState()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM [tbl_States]", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ddlBStateCode.DataSource = dt;
            ddlBStateCode.DataValueField = "StateCode";
            ddlBStateCode.DataTextField = "StateName";
            ddlBStateCode.DataBind();
            ddlBStateCode.Items.Insert(0, "-- Select State --");

            ddlSStatecode.DataSource = dt;
            ddlSStatecode.DataValueField = "StateCode";
            ddlSStatecode.DataTextField = "StateName";
            ddlSStatecode.DataBind();
            ddlSStatecode.Items.Insert(0, "-- Select State --");
        }
    }
    private void fillddlCountryCode()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("select * from tblCountryCode", Cls_Main.Conn);
        DataTable dtpt = new DataTable();
        adpt.Fill(dtpt);

        if (dtpt.Rows.Count > 0)
        {
            ddlCountryCode.DataSource = dtpt;
            ddlCountryCode.DataValueField = "CountryCode";
            ddlCountryCode.DataTextField = "CountryName";
            ddlCountryCode.DataBind();
            ddlCountryCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
    }
    //Company Code Auto
    protected void CompanyCode()
    {
        SqlDataAdapter ad = new SqlDataAdapter("SELECT max([Id]) as maxid FROM [tbl_LM_CompanyMaster]", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            int maxid = dt.Rows[0]["maxid"].ToString() == "" ? 0 : Convert.ToInt32(dt.Rows[0]["maxid"].ToString());
            txtcompanycode.Text = "LXMI/C-" + (maxid + 1).ToString();
        }
        else
        {
            txtcompanycode.Text = string.Empty;
        }
    }
    //Data Fetch
    private void Load_Record(string ID)
    {
        DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_LM_CompanyMaster] WHERE ID ='" + ID + "' ");
        if (Dt.Rows.Count > 0)
        {
            btnsave.Text = "Update";
            hhd.Value = Dt.Rows[0]["ID"].ToString();
            txtvendorcode.Text = Dt.Rows[0]["VendorCode"].ToString();
            txtcompanyname.Text = Dt.Rows[0]["Companyname"].ToString();
            txtcompanycode.Text = Dt.Rows[0]["CompanyCode"].ToString();
            txtPrimaryEmail.Text = Dt.Rows[0]["PrimaryEmailID"].ToString();
            txtSecondaryemailid.Text = Dt.Rows[0]["SecondaryEmailID"].ToString();
            txtgstno.Text = Dt.Rows[0]["GSTno"].ToString();
            txtUDYAM.Text = Dt.Rows[0]["UDYAMNO"].ToString();
            txtCINNO.Text = Dt.Rows[0]["CINNO"].ToString();
            txtCompanyPan.Text = Dt.Rows[0]["Companypancard"].ToString();
            ddlClientType.SelectedItem.Text = Dt.Rows[0]["Clienttype"].ToString();
            txtWebsiteLink.Text = Dt.Rows[0]["WebsiteLink"].ToString();
            TxtCreditLimit.Text = Dt.Rows[0]["creditlimit"].ToString();
            ddlTypeofSupply.SelectedValue = Dt.Rows[0]["E_inv_Typeof_supply"].ToString();
            ddlSStatecode.SelectedValue = Dt.Rows[0]["Shipping_statecode"].ToString();
            ddlBStateCode.SelectedValue = Dt.Rows[0]["Billing_statecode"].ToString();
            txtBillingAddress.Text = Dt.Rows[0]["Billingaddress"].ToString();
            txtshippingaddress.Text = Dt.Rows[0]["Shippingaddress"].ToString();
            txtbillinglocation.Text = Dt.Rows[0]["Billinglocation"].ToString();
            txtshippinglocation.Text = Dt.Rows[0]["Shippinglocation"].ToString();
            txtBPincode.Text = Dt.Rows[0]["Billingpincode"].ToString();
            txtSPincode.Text = Dt.Rows[0]["Shippingpincode"].ToString();
            txtPaymentTerm.Text = Dt.Rows[0]["PaymentTerm"].ToString();
            lblPath1.Text = Dt.Rows[0]["VisitingCardPath"].ToString(); FileUpload1.Enabled = true;
            BindAddressGrid(ID);
            //ShowDtlEdit();
        }
    }

    private void BindAddressGrid(string id)
    {
        ViewState["RowNo"] = 0;
        DataTable Dtproduct = new DataTable();
        SqlDataAdapter Daa = new SqlDataAdapter("SELECT * FROM tbl_LM_ShippingAddress WHERE c_id='" + id + "' ", con);
        Daa.Fill(Dtproduct);
        ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
        DataTable Dt = ViewState["AddressData"] as DataTable;
        //int count = 1;
        if (Dtproduct.Rows.Count > 0)
        {
            for (int i = 0; i < Dtproduct.Rows.Count; i++)
            {
                Dt.Rows.Add(ViewState["RowNo"], Dtproduct.Rows[i]["ShippingAddress"].ToString(), Dtproduct.Rows[i]["ShipLocation"].ToString(), Dtproduct.Rows[i]["ShipPincode"].ToString(), Dtproduct.Rows[i]["ShipStatecode"].ToString());
                //count = count + 1;
                ViewState["AddressData"] = Dt;
            }
        }

        GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
        GVShippingAddress.DataBind();
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcompanyname.Text == "" || txtPrimaryEmail.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Kindly Enter the Data..!!')", true);
            }
            else
            {

                string PathH = null;

                if (dgvContactDetails.Rows.Count > 0)
                {
                    if (GVShippingAddress.Rows.Count > 0)
                    {

                        if (btnsave.Text == "Update")
                        {
                            Cls_Main.Conn_Open();
                            SqlCommand Cmd = new SqlCommand("SP_LM_CompanyMaster", Cls_Main.Conn);
                            HttpPostedFile postedFile = FileUpload1.PostedFile;
                            if (FileUpload1.HasFile)
                            {

                                foreach (HttpPostedFile PostedFile in FileUpload1.PostedFiles)
                                {
                                    string filename = Path.GetFileName(postedFile.FileName);
                                    string[] pdffilename = filename.Split('.');
                                    string pdffilename1 = pdffilename[0];
                                    string filenameExt = pdffilename[1];
                                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                                    //{
                                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                                    postedFile.SaveAs(Server.MapPath("~/VisitingcardFiles/") + pdffilename1 + time1 + "." + filenameExt);

                                    Cmd.Parameters.AddWithValue("@VisitingCardPath", "VisitingcardFiles/" + pdffilename1 + time1 + "." + filenameExt);
                                    //}
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                                    //}
                                }
                            }
                            else
                            {
                                Cmd.Parameters.AddWithValue("@VisitingCardPath", lblPath1.Text);
                            }
                            Cmd.CommandType = CommandType.StoredProcedure;
                            Cmd.Parameters.AddWithValue("@Action", "Update");
                            Cmd.Parameters.AddWithValue("@ID", hhd.Value);
                            Cmd.Parameters.AddWithValue("@Companyname", txtcompanyname.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CompanyCode", txtcompanycode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@TypeofSupply", ddlTypeofSupply.SelectedValue);
                            Cmd.Parameters.AddWithValue("@CreditLimit", TxtCreditLimit.Text.Trim());
                            Cmd.Parameters.AddWithValue("@BState", ddlBStateCode.SelectedValue);
                            // Cmd.Parameters.AddWithValue("@SState", ddlSStatecode.SelectedValue);
                            //Cmd.Parameters.AddWithValue("@Area", txtArea.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Vendorcode", txtvendorcode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@PrimaryEmail", txtPrimaryEmail.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Secondaryemailid", txtSecondaryemailid.Text.Trim());
                            Cmd.Parameters.AddWithValue("@GSTno", txtgstno.Text.Trim());
                            Cmd.Parameters.AddWithValue("@UDYAMNO", txtUDYAM.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CINNO", txtCINNO.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CompanyPancard", txtCompanyPan.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Clienttype", ddlClientType.SelectedValue);
                            Cmd.Parameters.AddWithValue("@WebsiteLink", txtWebsiteLink.Text.Trim());
                            //  Cmd.Parameters.AddWithValue("@statecode", Convert.ToInt64(ddlstate.SelectedValue));
                            Cmd.Parameters.AddWithValue("@Paymentterm", txtPaymentTerm.Text);
                            //Cmd.Parameters.AddWithValue("@VisitingCardPath", PathH);
                            Cmd.Parameters.AddWithValue("@BillingAddress", txtBillingAddress.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Shippingaddress", txtshippingaddress.Text.Trim());
                            Cmd.Parameters.AddWithValue("@BillingPincode", txtBPincode.Text.Trim());
                            //  Cmd.Parameters.AddWithValue("@ShippingPincode", txtSPincode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@billinglocation", txtbillinglocation.Text.Trim());
                            //  Cmd.Parameters.AddWithValue("@shippinglocation", txtshippinglocation.Text.Trim());
                            Cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now);
                            Cmd.Parameters.AddWithValue("@IsDeleted", '0');
                            Cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserCode"].ToString());
                            Cmd.ExecuteNonQuery();
                            Cls_Main.Conn_Close();
                            Cls_Main.Conn_Dispose();

                            // Delete Contact Details
                            Cls_Main.Conn_Open();
                            SqlCommand cmddelete = new SqlCommand("DELETE FROM tbl_LM_CompanyContactDetails WHERE CompanyCode=@CompanyCode", Cls_Main.Conn);
                            cmddelete.Parameters.AddWithValue("@CompanyCode", txtcompanycode.Text);
                            cmddelete.ExecuteNonQuery();
                            Cls_Main.Conn_Close();

                            //Save Contact Details 
                            foreach (GridViewRow grd1 in dgvContactDetails.Rows)
                            {
                                string lblname = (grd1.FindControl("lblname") as Label).Text;
                                string lblnumber = (grd1.FindControl("lblnumber") as Label).Text;
                                string lblemailid = (grd1.FindControl("lblemailid") as Label).Text;
                                string lblDepartment = (grd1.FindControl("lblDepartment") as Label).Text;
                                string lbldesignation = (grd1.FindControl("lbldesignation") as Label).Text;
                                Cls_Main.Conn_Open();
                                SqlCommand cmdd = new SqlCommand("INSERT INTO tbl_LM_CompanyContactDetails (CompanyCode,Name,Number,EmailID,Department,Designation,CreatedBy,CreatedOn) VALUES (@CompanyCode,@Name,@Number,@EmailID,@Department,@Designation,@CreatedBy,@createdOn)", Cls_Main.Conn);
                                cmdd.Parameters.AddWithValue("@CompanyCode", txtcompanycode.Text.Trim());

                                cmdd.Parameters.AddWithValue("@Name", lblname);
                                cmdd.Parameters.AddWithValue("@Number", lblnumber);
                                cmdd.Parameters.AddWithValue("@EmailID", lblemailid);
                                cmdd.Parameters.AddWithValue("@Department", lblDepartment);
                                cmdd.Parameters.AddWithValue("@Designation", lbldesignation);
                                cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                                cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                                cmdd.ExecuteNonQuery();
                                Cls_Main.Conn_Close();
                            }




                            //DataTable Dtt = new DataTable();
                            //SqlDataAdapter Daa = new SqlDataAdapter("SELECT * FROM tbl_ShippingAddress WHERE c_id ='" + hhd.Value + "'", Cls_Main.Conn);
                            //Daa.Fill(Dtt);
                            Cls_Main.Conn_Open();
                            SqlCommand cmddelete1 = new SqlCommand("DELETE FROM tbl_LM_ShippingAddress WHERE c_id=@c_id", Cls_Main.Conn);
                            cmddelete1.Parameters.AddWithValue("@c_id", hhd.Value);
                            cmddelete1.ExecuteNonQuery();
                            Cls_Main.Conn_Close();

                            foreach (GridViewRow g2 in GVShippingAddress.Rows)
                            {
                                string Location = (g2.FindControl("lblSLocation") as Label).Text;
                                string ShipingAddress = (g2.FindControl("lblshippingaddress") as Label).Text;
                                string Pincode = (g2.FindControl("lblSPincode") as Label).Text;
                                string StateCode = (g2.FindControl("lblSStateCode") as Label).Text;
                                Cls_Main.Conn_Open();
                                SqlCommand Cmdd = new SqlCommand("INSERT INTO tbl_LM_ShippingAddress ([c_id],[ShipLocation],[ShippingAddress],[ShipPincode],[ShipStatecode]) VALUES (@c_id,@ShipLocation,@ShippingAddress,@ShipPincode,@ShipStatecode)", Cls_Main.Conn);
                                Cmdd.Parameters.AddWithValue("@c_id", hhd.Value);
                                Cmdd.Parameters.AddWithValue("@ShipLocation", Location);
                                Cmdd.Parameters.AddWithValue("@ShippingAddress", ShipingAddress);
                                Cmdd.Parameters.AddWithValue("@ShipPincode", Pincode);
                                Cmdd.Parameters.AddWithValue("@ShipStatecode", StateCode);
                                Cmdd.ExecuteNonQuery();
                                Cls_Main.Conn_Close();

                            }
                            if (Request.QueryString["CODE"] != null)
                            {

                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company Updated Successfully..!!'); ", true);
                                Response.Redirect("QuatationMaster.aspx?CODE=" + Request.QueryString["CODE"].ToString() + "");

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company Updated Successfully..!!');window.location='CompanyMasterList.aspx'; ", true);
                            }
                        }
                        else
                        {
                            Cls_Main.Conn_Open();
                            SqlCommand Cmd = new SqlCommand("SP_LM_CompanyMaster", Cls_Main.Conn);
                            Cmd.CommandType = CommandType.StoredProcedure;
                            HttpPostedFile postedFile = FileUpload1.PostedFile;
                            if (FileUpload1.HasFile)
                            {
                                foreach (HttpPostedFile PostedFile in FileUpload1.PostedFiles)
                                {
                                    string filename = Path.GetFileName(postedFile.FileName);
                                    string[] pdffilename = filename.Split('.');
                                    string pdffilename1 = pdffilename[0];
                                    string filenameExt = pdffilename[1];
                                    //if (filenameExt == "pdf" || filenameExt == "PDF")
                                    //{
                                    string time1 = DateTime.Now.ToString("ddmmyyyyttmmss");
                                    postedFile.SaveAs(Server.MapPath("~/VisitingcardFiles/") + pdffilename1 + time1 + "." + filenameExt);

                                    Cmd.Parameters.AddWithValue("@VisitingCardPath", "EnquiryFiles/" + pdffilename1 + time1 + "." + filenameExt);
                                    //}
                                    //else
                                    //{
                                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please select a pdf file only !!');", true);
                                    //}
                                }
                            }
                            Cmd.Parameters.AddWithValue("@Action", "Save");
                            Cmd.Parameters.AddWithValue("@ID", hhd.Value);
                            Cmd.Parameters.AddWithValue("@Companyname", txtcompanyname.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CompanyCode", txtcompanycode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@TypeofSupply", ddlTypeofSupply.SelectedValue);
                            Cmd.Parameters.AddWithValue("@CreditLimit", TxtCreditLimit.Text.Trim());
                            Cmd.Parameters.AddWithValue("@BState", ddlBStateCode.SelectedValue);
                            Cmd.Parameters.AddWithValue("@SState", DBNull.Value);
                            Cmd.Parameters.AddWithValue("@PrimaryEmail", txtPrimaryEmail.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Vendorcode", txtvendorcode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Secondaryemailid", txtSecondaryemailid.Text.Trim());
                            Cmd.Parameters.AddWithValue("@GSTno", txtgstno.Text.Trim());
                            Cmd.Parameters.AddWithValue("@UDYAMNO", txtUDYAM.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CINNO", txtCINNO.Text.Trim());
                            Cmd.Parameters.AddWithValue("@CompanyPancard", txtCompanyPan.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Clienttype", ddlClientType.SelectedValue);
                            Cmd.Parameters.AddWithValue("@WebsiteLink", txtWebsiteLink.Text.Trim());
                            Cmd.Parameters.AddWithValue("@VisitingCardPath", PathH);
                            Cmd.Parameters.AddWithValue("@Paymentterm", txtPaymentTerm.Text);
                            Cmd.Parameters.AddWithValue("@BillingAddress", txtBillingAddress.Text.Trim());
                            Cmd.Parameters.AddWithValue("@Shippingaddress", txtshippingaddress.Text.Trim());
                            Cmd.Parameters.AddWithValue("@BillingPincode", txtBPincode.Text.Trim());
                            Cmd.Parameters.AddWithValue("@ShippingPincode", DBNull.Value);
                            Cmd.Parameters.AddWithValue("@billinglocation", txtbillinglocation.Text.Trim());
                            Cmd.Parameters.AddWithValue("@shippinglocation", DBNull.Value);
                            Cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                            Cmd.Parameters.AddWithValue("@IsDeleted", '0');
                            Cmd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                            Cmd.ExecuteNonQuery();
                            Cls_Main.Conn_Close();
                            Cls_Main.Conn_Dispose();

                            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_LM_CompanyMaster] WHERE CompanyCode ='" + txtcompanycode.Text + "' ");
                            if (Dt.Rows.Count > 0)
                            {

                                hhd.Value = Dt.Rows[0]["ID"].ToString();
                            }

                            foreach (GridViewRow g2 in GVShippingAddress.Rows)
                            {
                                string Location = (g2.FindControl("lblSLocation") as Label).Text;
                                string ShipingAddress = (g2.FindControl("lblshippingaddress") as Label).Text;
                                string Pincode = (g2.FindControl("lblSPincode") as Label).Text;
                                string StateCode = (g2.FindControl("lblSStateCode") as Label).Text;
                                Cls_Main.Conn_Open();
                                SqlCommand Cmdd = new SqlCommand("INSERT INTO tbl_LM_ShippingAddress ([c_id],[ShipLocation],[ShippingAddress],[ShipPincode],[ShipStatecode]) VALUES (@c_id,@ShipLocation,@ShippingAddress,@ShipPincode,@ShipStatecode)", Cls_Main.Conn);
                                Cmdd.Parameters.AddWithValue("@c_id", hhd.Value);
                                Cmdd.Parameters.AddWithValue("@ShipLocation", Location);
                                Cmdd.Parameters.AddWithValue("@ShippingAddress", ShipingAddress);
                                Cmdd.Parameters.AddWithValue("@ShipPincode", Pincode);
                                Cmdd.Parameters.AddWithValue("@ShipStatecode", StateCode);
                                Cmdd.ExecuteNonQuery();
                                Cls_Main.Conn_Close();

                            }


                            //Save Contact Details 
                            foreach (GridViewRow grd1 in dgvContactDetails.Rows)
                            {

                                string lblname = (grd1.FindControl("lblname") as Label).Text;
                                string lblnumber = (grd1.FindControl("lblnumber") as Label).Text;
                                string lblemailid = (grd1.FindControl("lblemailid") as Label).Text;
                                string lblDepartment = (grd1.FindControl("lblDepartment") as Label).Text;
                                string lbldesignation = (grd1.FindControl("lbldesignation") as Label).Text;

                                Cls_Main.Conn_Open();
                                SqlCommand cmdd = new SqlCommand("INSERT INTO tbl_LM_CompanyContactDetails (CompanyCode,Name,Number,EmailID,Department,Designation,CreatedBy,CreatedOn) VALUES (@CompanyCode,@Name,@Number,@EmailID,@Department,@Designation,@CreatedBy,@createdOn)", Cls_Main.Conn);
                                cmdd.Parameters.AddWithValue("@CompanyCode", txtcompanycode.Text.Trim());

                                cmdd.Parameters.AddWithValue("@Name", lblname);
                                cmdd.Parameters.AddWithValue("@Number", lblnumber);
                                cmdd.Parameters.AddWithValue("@EmailID", lblemailid);
                                cmdd.Parameters.AddWithValue("@Department", lblDepartment);
                                cmdd.Parameters.AddWithValue("@Designation", lbldesignation);
                                cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                                cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                                cmdd.ExecuteNonQuery();
                                Cls_Main.Conn_Close();
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Company Added Successfully..!!');window.location='CompanyMasterList.aspx'; ", true);




                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Add Shipping Address..!!'); ", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Fill Atleast One Contact detail..!!'); ", true);
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyMasterList.aspx", true);
    }

    private void Show_Grid()
    {
        DataTable Dt = (DataTable)ViewState["ContactDetails"];
        Dt.Rows.Add(ViewState["RowNo"], txtname.Text.Trim(), txtmobile.Text.Trim(), txtemaili.Text.Trim(), txtDepartment.Text.Trim(), txtdesignation.Text.Trim());
        ViewState["ContactDetails"] = Dt;
        txtDepartment.Text = string.Empty;
        txtname.Text = string.Empty;
        txtmobile.Text = string.Empty;
        txtemaili.Text = string.Empty;
        txtdesignation.Text = string.Empty;
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        btnsave.Focus();
        if (txtname.Text == "" || txtmobile.Text == "" || txtemaili.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill Contact Information  !!!');", true);
        }
        else
        {
            Show_Grid();
        }
    }

    protected void ShowDtlEdit()
    {
        SqlDataAdapter Da = new SqlDataAdapter("SELECT * FROM [tbl_LM_CompanyContactDetails] WHERE CompanyCode='" + txtcompanycode.Text + "'", Cls_Main.Conn);
        DataTable DTCOMP = new DataTable();
        Da.Fill(DTCOMP);

        int count = 0;
        if (DTCOMP.Rows.Count > 0)
        {
            if (Dt_Component.Columns.Count < 1)
            {
                Show_Grid();
            }

            for (int i = 0; i < DTCOMP.Rows.Count; i++)
            {
                Dt_Component.Rows.Add(count, DTCOMP.Rows[i]["Name"].ToString(), DTCOMP.Rows[i]["Number"].ToString(), DTCOMP.Rows[i]["EmailID"].ToString(), DTCOMP.Rows[i]["Department"].ToString(), DTCOMP.Rows[i]["Designation"].ToString());
                count = count + 1;
            }
        }

        dgvContactDetails.EmptyDataText = "No Data Found";
        dgvContactDetails.DataSource = Dt_Component;
        dgvContactDetails.DataBind();
    }

    protected void gv_cancel_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        string id = ((TextBox)row.FindControl("lblsno")).Text;
        string Name = ((TextBox)row.FindControl("lblname")).Text;
        string Number = ((TextBox)row.FindControl("lblnumber")).Text;
        string Email = ((TextBox)row.FindControl("lblemailid")).Text;
        string Department = ((TextBox)row.FindControl("lblDepartment")).Text;
        string Designation = ((TextBox)row.FindControl("lbldesignation")).Text;

        DataTable Dt = ViewState["ContactDetails"] as DataTable;
        Dt.Rows[row.RowIndex]["id"] = id;
        Dt.Rows[row.RowIndex]["Name"] = Name;
        Dt.Rows[row.RowIndex]["Number"] = Number;
        Dt.Rows[row.RowIndex]["EmailID"] = Email;
        Dt.Rows[row.RowIndex]["Department"] = Department;
        Dt.Rows[row.RowIndex]["Designation"] = Designation;
        Dt.AcceptChanges();
        ViewState["ContactDetails"] = Dt;
        dgvContactDetails.EditIndex = -1;
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void lnkbtnDelete_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
        DataTable dt = ViewState["AddressData"] as DataTable;
        dt.Rows.Remove(dt.Rows[row.RowIndex]);
        ViewState["AddressData"] = dt;
        GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
        GVShippingAddress.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Address Delete Succesfully !!!');", true);
        //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void dgvContactDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgvContactDetails.EditIndex = e.NewEditIndex;
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void gv_update_Click(object sender, EventArgs e)
    {
        GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

        string Name = ((TextBox)row.FindControl("txtname")).Text;
        string Number = ((TextBox)row.FindControl("txtmobile")).Text;
        string Email = ((TextBox)row.FindControl("txtemaili")).Text;
        string Department = ((TextBox)row.FindControl("txtDepartment")).Text;
        string Designation = ((TextBox)row.FindControl("txtdesignation")).Text;
        DataTable Dt = ViewState["ContactDetails"] as DataTable;

        Dt.Rows[row.RowIndex]["Name"] = Name;
        Dt.Rows[row.RowIndex]["Number"] = Number;
        Dt.Rows[row.RowIndex]["EmailID"] = Email;
        Dt.Rows[row.RowIndex]["Department"] = Department;
        Dt.Rows[row.RowIndex]["Designation"] = Designation;
        Dt.AcceptChanges();
        ViewState["ContactDetails"] = Dt;
        dgvContactDetails.EditIndex = -1;
        dgvContactDetails.DataSource = (DataTable)ViewState["ContactDetails"];
        dgvContactDetails.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "scrollToElement();", true);
    }

    protected void check_addresss_CheckedChanged(object sender, EventArgs e)
    {
        if (check_addresss.Checked == true)
        {
            txtshippingaddress.Text = txtBillingAddress.Text;
            txtshippinglocation.Text = txtbillinglocation.Text;
            ddlSStatecode.SelectedValue = ddlBStateCode.SelectedValue;
            txtSPincode.Text = txtBPincode.Text;
        }
    }



    protected void ddlTypeofSupply_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTypeofSupply.SelectedItem.Text == "EXPWOP")
            {
                fillddlCountryCode();
                txtgstno.Text = "URP"; txtgstno.Enabled = false;
                txtBPincode.Text = "999999"; ddlBStateCode.SelectedItem.Text = "96"; txtBPincode.Enabled = false; ddlBStateCode.Enabled = false;

                contry.Visible = true;
            }
            else
            {
                txtgstno.Text = ""; txtgstno.Enabled = true;
                txtBPincode.Text = ""; txtBPincode.Enabled = true; ddlBStateCode.Enabled = true;

                contry.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }

    protected void txtgstno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (hhd.Value == "")
            {


                Cls_Main.Conn_Open();
                int count = 0;
                SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM tbl_LM_CompanyMaster where GSTno='" + txtgstno.Text.Trim() + "'", Cls_Main.Conn);
                count = Convert.ToInt16(cmd.ExecuteScalar());
                Cls_Main.Conn_Close();
                if (count > 0)
                {
                    txtgstno.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Same GST number Company already available...!');", true);
                }
                else
                {
                    string gstNumber = txtgstno.Text.Trim();
                    string pattern = @"^\d{2}[A-Z]{5}\d{4}[A-Z]{1}\d[Z]{1}[A-Z\d]{1}$";

                    if (System.Text.RegularExpressions.Regex.IsMatch(gstNumber, pattern))
                    {
                        ddlBStateCode.SelectedValue = gstNumber.Substring(0, 2);
                        txtCompanyPan.Text = gstNumber.Substring(2, 10);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Invalid GST Number. GST number should be in the format- 27ATFPS1959J1Z4');", true);
                    }
                }
            }
        }
        catch (Exception)
        {

        }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtshippingaddress.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Please fill Shipping Address !!!');", true);
                txtshippingaddress.Focus();
            }
            else
            {
                Bind_Grid();
                btnsave.Focus();
            }

        }
        catch (Exception)
        {

            throw;
        }

    }


    private void Bind_Grid()
    {

        DataTable Dt = (DataTable)ViewState["AddressData"];
        Dt.Rows.Add(ViewState["RowNo"], txtshippingaddress.Text, txtshippinglocation.Text, txtSPincode.Text, ddlSStatecode.SelectedValue);
        ViewState["AddressData"] = Dt;

        GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
        GVShippingAddress.DataBind();

        txtshippinglocation.Text = string.Empty;
        txtshippingaddress.Text = string.Empty;
        txtSPincode.Text = string.Empty;

    }


    protected void ImgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            GridViewRow row = (sender as ImageButton).NamingContainer as GridViewRow;

            DataTable dt = ViewState["AddressData"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["AddressData"] = dt;
            GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
            GVShippingAddress.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Record Delete Succesfully !!!');", true);
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void GVShippingAddress_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GVShippingAddress.EditIndex = e.NewEditIndex;
        GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
        GVShippingAddress.DataBind();
    }



    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            string ShipLocation = ((TextBox)row.FindControl("txtSLocation")).Text;
            string ShippingAddress = ((TextBox)row.FindControl("txtshipingaddress")).Text;
            string ShipPincode = ((TextBox)row.FindControl("txtSPincode")).Text;
            string ShipStatecode = ((TextBox)row.FindControl("txtSStateCode")).Text;

            DataTable Dt = ViewState["AddressData"] as DataTable;

            Dt.Rows[row.RowIndex]["ShipLocation"] = ShipLocation;
            Dt.Rows[row.RowIndex]["ShippingAddress"] = ShippingAddress;
            Dt.Rows[row.RowIndex]["ShipPincode"] = ShipPincode;
            Dt.Rows[row.RowIndex]["ShipStatecode"] = ShipStatecode;
            Dt.AcceptChanges();

            ViewState["AddressData"] = Dt;
            GVShippingAddress.EditIndex = -1;

            GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
            GVShippingAddress.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

            string ShipLocation = ((TextBox)row.FindControl("txtSLocation")).Text;
            string ShippingAddress = ((TextBox)row.FindControl("txtshipingaddress")).Text;
            string ShipPincode = ((TextBox)row.FindControl("txtSPincode")).Text;
            string ShipStatecode = ((TextBox)row.FindControl("txtSStateCode")).Text;

            DataTable Dt = ViewState["AddressData"] as DataTable;

            Dt.Rows[row.RowIndex]["ShipLocation"] = ShipLocation;
            Dt.Rows[row.RowIndex]["ShippingAddress"] = ShippingAddress;
            Dt.Rows[row.RowIndex]["ShipPincode"] = ShipPincode;
            Dt.Rows[row.RowIndex]["ShipStatecode"] = ShipStatecode;
            Dt.AcceptChanges();

            ViewState["AddressData"] = Dt;
            GVShippingAddress.EditIndex = -1;

            GVShippingAddress.DataSource = (DataTable)ViewState["AddressData"];
            GVShippingAddress.DataBind();
        }
        catch (Exception)
        {
            throw;
        }
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
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
                com.CommandText = "Select DISTINCT [Companyname] from [tbl_LM_CompanyMaster] where " + "Companyname like @Search + '%' and IsDeleted=0";

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

    protected void txtcompanyname_TextChanged(object sender, EventArgs e)
    {
        if (txtcompanyname.Text != null)
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_LM_CompanyMaster] WHERE Companyname ='" + txtcompanyname.Text.Trim() + "' ");
            if (Dt.Rows.Count > 0)
            {
                // txtcompanyname.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same Name customer already available..!!')", true);
            }
        }
    }

    protected void txtPrimaryEmail_TextChanged(object sender, EventArgs e)
    {
        if (txtPrimaryEmail.Text != null)
        {
            DataTable Dt = Cls_Main.Read_Table("SELECT * FROM [tbl_LM_CompanyMaster] WHERE Companyname='" + txtcompanyname.Text.Trim() + "' AND PrimaryEmailID ='" + txtPrimaryEmail.Text.Trim() + "' ");
            if (Dt.Rows.Count > 0)
            {
                txtcompanyname.Text = string.Empty;
                txtPrimaryEmail.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Same EmailID customer already available..!!')", true);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("CompanyMasterList.aspx");
    }
}