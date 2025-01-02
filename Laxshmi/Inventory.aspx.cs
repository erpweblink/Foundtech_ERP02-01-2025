
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Laxshmi_Inventory : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    DataTable Dt_Component = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.ModalPopupHistory.Hide();
        if (Session["UserCode"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillddlDefects();
                FillGrid();
                ViewState["RowNo"] = 0;
                Dt_Component.Columns.AddRange(new DataColumn[3] { new DataColumn("id"), new DataColumn("Defect"), new DataColumn("DefectQty") });
                ViewState["Details"] = Dt_Component;
            }
        }
    }

    //Fill GridView
    private void FillGrid()
    {

        try
        {
            SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetInventorylist");
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

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Columns.Count > 0)
            {
                GVPurchase.DataSource = dt;
                GVPurchase.DataBind();
            }


        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + errorMsg + "') ", true);

        }

    }

    private void FillddlDefects()
    {
        Cls_Main.Conn_Open();
        SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM tbl_LM_DefectMaster where isdeleted=0", Cls_Main.Conn);
        DataTable dt = new DataTable();
        ad.Fill(dt);
        Cls_Main.Conn_Close();
        if (dt.Rows.Count > 0)
        {
            ddlDefectsType.DataSource = dt;
            ddlDefectsType.DataValueField = "DefectCode";
            ddlDefectsType.DataTextField = "DefectName";
            ddlDefectsType.DataBind();
            ddlDefectsType.Items.Insert(0, "-- Select Defect Type--");
        }

    }
    public string GenerateCode()
    {

        string FinYear = null;
        string FinFullYear = null;
        if (DateTime.Today.Month > 3)
        {
            FinYear = DateTime.Today.AddYears(1).ToString("yy");
            FinFullYear = DateTime.Today.AddYears(1).ToString("yy");
        }
        else
        {
            var finYear = DateTime.Today.AddYears(1).ToString("yy");
            FinYear = (Convert.ToInt32(finYear) - 1).ToString();

            var finfYear = DateTime.Today.AddYears(1).ToString("yy");
            FinFullYear = (Convert.ToInt32(finfYear) - 1).ToString();
        }
        string previousyear = (Convert.ToDecimal(FinFullYear) - 1).ToString();
        string strInvoiceNumber = "";
        string fY = previousyear.ToString() + "-" + FinYear;
        string strSelect = @"select ISNULL(MAX(OutwardNo), '0') AS maxno from tbl_LM_Outwarddata where OutwardNo like '%" + fY + "%'";
        // string strSelect = @"SELECT TOP 1 MAX(ID) FROM tblTaxInvoiceHdr where InvoiceNo like '%" + fY + "%' ";

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandType = CommandType.Text;
        cmd.CommandText = strSelect;
        con.Open();
        string result;

        result = cmd.ExecuteScalar().ToString();


        con.Close();
        if (result != "")
        {
            int numbervalue = Convert.ToInt32(result.Substring(result.LastIndexOf("/") + 1));
            numbervalue = numbervalue + 1;
            strInvoiceNumber = previousyear.ToString() + "-" + FinYear + "/" + numbervalue;
        }
        else
        {
            strInvoiceNumber = previousyear.ToString() + "-" + FinYear + "/" + "01";
        }
        txtInwardnopop.Text = strInvoiceNumber;
        return strInvoiceNumber;
    }

    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "RowEdit")
        //{
        //    divinwardform.Visible = true;
        //    divtabl.Visible = false;
        //    int rowIndex = Convert.ToInt32(e.CommandArgument);
        //    GridViewRow row = GVPurchase.Rows[rowIndex];
        //    txtcustomer.Text = ((Label)row.FindControl("CustomerName")).Text;
        //    txtrowmetarial.Text = ((Label)row.FindControl("MaterialName")).Text;
        //    hdnInwardNo.Value = ((Label)row.FindControl("Inwardno")).Text;
        //    txtinwardqantity.Text = ((Label)row.FindControl("InwardQty")).Text;
        //    string inwardno = ((Label)row.FindControl("Inwardno")).Text;
        //    DataTable Dt = Cls_Main.Read_Table("select * from tbl_LM_inwarddata where InwardNo='" + hdnInwardNo.Value + "'");
        //    txtDeliveryNo.Text = Dt.Rows[0]["DeliveryNo"].ToString();
        //    DateTime ffff1 = Convert.ToDateTime(Dt.Rows[0]["DeliveryDate"].ToString());
        //    txtDeliveryDate.Text = ffff1.ToString("yyyy-MM-dd");
        //    DateTime ffff2 = Convert.ToDateTime(Dt.Rows[0]["WODate"].ToString());
        //    txtWODate.Text = ffff2.ToString("yyyy-MM-dd");

        //    txtWeight.Text = Dt.Rows[0]["Weight"].ToString();
        //    txtTotalWeight.Text = Dt.Rows[0]["TotalWeight"].ToString();
        //    txtVehicleno.Text = Dt.Rows[0]["VehicleNo"].ToString();
        //    txtHSN.Text = Dt.Rows[0]["HSN"].ToString();
        //    btnsavedata.Text = "Update";
        //}
        if (e.CommandName == "RowOutward")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
            // string Inwardno = ((Label)row.FindControl("Inwardno")).Text;
            string InwardQty = ((Label)row.FindControl("InwardQty")).Text;
            string OutwardQty = ((Label)row.FindControl("OutwardQty")).Text;
            string DefectedQty = ((Label)row.FindControl("DefectedQty")).Text;
            string RemainingQty = ((Label)row.FindControl("RemainingQty")).Text;
            string CustomerName = ((Label)row.FindControl("CustomerName")).Text;
            string MaterialName = ((Label)row.FindControl("MaterialName")).Text;
            txtcustomernamepop.Text = CustomerName;
            //txtOldoutwardqty.Text = OutwardQty;
            //txtolddefectedqty.Text = DefectedQty;
            txtRemaining.Text = RemainingQty;
            txtoutwardqty.Text = RemainingQty;
            //  txtInwardnopop.Text = Inwardno;
            txtinwardqty.Text = InwardQty;

            //string inwardno = ((Label)row.FindControl("Inwardno")).Text;
            //DataTable Dt = Cls_Main.Read_Table("select * from tbl_LM_inwarddata where InwardNo='" + inwardno + "'");
            //if (!string.IsNullOrEmpty(Dt.Rows[0]["ReferenceDate"].ToString()) && Dt.Rows[0]["ReferenceDate"].ToString() != " ")
            //{
            //    DateTime ffff1 = Convert.ToDateTime(Dt.Rows[0]["ReferenceDate"].ToString());
            //    txtReferenceDate.Text = ffff1.ToString("yyyy-MM-dd");
            //}

            //if (!string.IsNullOrEmpty(Dt.Rows[0]["DeliveryNotedate"].ToString()) && Dt.Rows[0]["DeliveryNotedate"].ToString() != " ")
            //{
            //    DateTime ffff2 = Convert.ToDateTime(Dt.Rows[0]["DeliveryNotedate"].ToString());
            //    txtDeliverynotedate.Text = ffff2.ToString("yyyy-MM-dd");
            //}


            //txtrefrenceno.Text = Dt.Rows[0]["ReferenceNo"].ToString();
            //txtDeliverynoteno.Text = Dt.Rows[0]["DeliveryNoteno"].ToString();

            //ViewState["RowNo"] = 0;
            //Dt_Component.Columns.AddRange(new DataColumn[3] { new DataColumn("id"), new DataColumn("Defect"), new DataColumn("DefectQty") });
            //ViewState["Details"] = Dt_Component;
            //DataTable Dtt = Cls_Main.Read_Table("select  id,DefectType AS Defect,DefectQty from tbl_LM_Defects where InwardNo='" + Inwardno + "' AND IsDeleted=0");
            //int count = 0;
            //if (Dtt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < Dtt.Rows.Count; i++)
            //    {
            //        Dt_Component.Rows.Add(Dtt.Rows[i]["id"].ToString(), Dtt.Rows[i]["Defect"].ToString(), Dtt.Rows[i]["DefectQty"].ToString());
            //        count = count + 1;
            //    }

            //}
            //GVDefects.EmptyDataText = "No Data Found";
            //GVDefects.DataSource = Dt_Component;
            //GVDefects.DataBind();
            GenerateCode();
            txtrowmaterialpop.Text = MaterialName;
            this.ModalPopupHistory.Show();
        }
        if (e.CommandName == "RowDefect")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
            string CustomerName = ((Label)row.FindControl("CustomerName")).Text;
            string MaterialName = ((Label)row.FindControl("MaterialName")).Text;
            string DefectedQty = ((Label)row.FindControl("DefectedQty")).Text;
            txtcustomerdefeted.Text = CustomerName;
            txtrowmaterialdef.Text = MaterialName;
            txtdefectedqtydef.Text = DefectedQty;
            this.ModalPopupHistory1.Show();
        }
        //if (e.CommandName == "RowDelete")
        //{
        //    Cls_Main.Conn_Open();
        //    SqlCommand Cmd = new SqlCommand("UPDATE [tbl_LM_inwarddata] SET IsDeleted=@IsDeleted,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID", Cls_Main.Conn);
        //    Cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument.ToString()));
        //    Cmd.Parameters.AddWithValue("@IsDeleted", '1');
        //    Cmd.Parameters.AddWithValue("@DeletedBy", Session["UserCode"].ToString());
        //    Cmd.Parameters.AddWithValue("@DeletedOn", DateTime.Now);
        //    Cmd.ExecuteNonQuery();
        //    Cls_Main.Conn_Close();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Entry Deleted Successfully..!!')", true);
        //    FillGrid();
        //}
    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVPurchase.PageIndex = e.NewPageIndex;
        FillGrid();
    }

    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label TotalRemainingQty = e.Row.FindControl("RemainingQty") as Label;
                Label RemainingDefectQty = e.Row.FindControl("DefectedQty") as Label;
                LinkButton lnkDefectout = (LinkButton)e.Row.FindControl("lnkDefectout");
                LinkButton btnoutward = (LinkButton)e.Row.FindControl("btnoutward");
                if (Convert.ToDouble(TotalRemainingQty.Text) ==0 && Convert.ToDouble(RemainingDefectQty.Text)==0)

                {
                    e.Row.Visible = false;
                }

                if(Convert.ToDouble(TotalRemainingQty.Text) <= 0)
                {
                    btnoutward.Visible = false;
                }
                if (Convert.ToDouble(RemainingDefectQty.Text) <= 0)
                {
                    lnkDefectout.Visible = false;
                }
                //Label DefectedQty = e.Row.FindControl("DefectedQty") as Label;
                // LinkButton btnoutward = (LinkButton)e.Row.FindControl("btnoutward");
                //LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                //// LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");


                //// Double total = Convert.ToDouble(OutwardQty.Text) + Convert.ToDouble(DefectedQty.Text);
                //if (Convert.ToDouble(InwardQty.Text) == Convert.ToDouble(OutwardQty.Text))
                //{
                //    //e.Row.BackColor = System.Drawing.Color.LightPink;
                //    btnoutward.Visible = false;
                //}

                //if (OutwardQty.Text != "0" || DefectedQty.Text != "0")
                //{
                //    // btnEdit.Visible = false;
                //    // btnDelete.Visible = false;
                //}


                Label CustomerName = e.Row.FindControl("CustomerName") as Label;
                Label MaterialName = e.Row.FindControl("MaterialName") as Label;
                GridView gvDetails = e.Row.FindControl("gvDetails") as GridView;
                DataTable Dt = Cls_Main.Read_Table("select DefectType,SUM(ISNULL(CAST(DefectQty AS FLOAT), 0)) AS DefectQty from tbl_LM_Defects where CompanyName='" + CustomerName.Text + "' AND RowMaterial='" + MaterialName.Text + "' GROUP BY DefectType");
                gvDetails.DataSource = Dt;
                gvDetails.DataBind();
            }
        }
        catch
        {

        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            Double totaldefect = 0;
            foreach (GridViewRow grd1 in GVDefects.Rows)
            {
                string lblQty = (grd1.FindControl("lblQty") as Label).Text;
                totaldefect += Convert.ToDouble(lblQty);
            }
            Double defecttotal = Convert.ToDouble(txtDefectqty.Text);
            if (totaldefect == defecttotal)
            {
                Double outtotal = Convert.ToDouble(txtoutwardqty.Text);
                Double Alltotal = outtotal + defecttotal;
                Double Pending = Convert.ToDouble(txtRemaining.Text) - Alltotal;
                if (Alltotal <= Convert.ToDouble(txtinwardqty.Text))
                {

                    Cls_Main.Conn_Open();
                    SqlCommand cmd = new SqlCommand("SP_Laxshmidetails", Cls_Main.Conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", "InseartOutwarddata");
                    cmd.Parameters.AddWithValue("@OutwardNo", txtInwardnopop.Text);
                    cmd.Parameters.AddWithValue("@CompanyName", txtcustomernamepop.Text);
                    cmd.Parameters.AddWithValue("@RowMaterial", txtrowmaterialpop.Text);
                    cmd.Parameters.AddWithValue("@RemainingQty", Convert.ToString(Pending));
                    cmd.Parameters.AddWithValue("@OutwardQty", Convert.ToString(outtotal));
                    cmd.Parameters.AddWithValue("@DefectQty", Convert.ToString(defecttotal));
                    cmd.Parameters.AddWithValue("@Description", txtRemarks.Text);
                    cmd.Parameters.AddWithValue("@Vehicleno", txtVehicleno.Text);
                    Double Totalweight = Convert.ToDouble(txtoutwardqty.Text) * Convert.ToDouble(txtWeight.Text);
                    cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                    cmd.Parameters.AddWithValue("@TotalWeight", Convert.ToString(Totalweight));
                    cmd.Parameters.AddWithValue("@DeliveryNoteno", txtDeliverynoteno.Text);
                    cmd.Parameters.AddWithValue("@DeliveryNotedate", txtDeliverynotedate.Text);
                    cmd.Parameters.AddWithValue("@ReferenceNo", txtrefrenceno.Text);
                    cmd.Parameters.AddWithValue("@ReferenceDate", txtReferenceDate.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());

                    cmd.ExecuteNonQuery();
                    Cls_Main.Conn_Close();
                    Cls_Main.Conn_Dispose();

                    Cls_Main.Conn_Open();
                    foreach (GridViewRow grd1 in GVDefects.Rows)
                    {
                        string lblDefects = (grd1.FindControl("lblDefects") as Label).Text;
                        string lblQty = (grd1.FindControl("lblQty") as Label).Text;
                        SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[tbl_LM_Defects]([OutwardNo],CompanyName,RowMaterial,[DefectType],[DefectQty],[CreatedBy],[CreatedOn],IsDeleted) VALUES (@OutwardNo,@CompanyName,@RowMaterial,@DefectType,@DefectQty,@CreatedBy,@CreatedOn,'0')", Cls_Main.Conn);
                        cmdd.Parameters.AddWithValue("@OutwardNo", txtInwardnopop.Text.Trim());
                        cmdd.Parameters.AddWithValue("@CompanyName", txtcustomernamepop.Text.Trim());
                        cmdd.Parameters.AddWithValue("@RowMaterial", txtrowmaterialpop.Text.Trim());
                        cmdd.Parameters.AddWithValue("@DefectQty", lblQty);
                        cmdd.Parameters.AddWithValue("@DefectType", lblDefects);
                        cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
                        cmdd.ExecuteNonQuery();

                    }
                    Cls_Main.Conn_Close();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Record Successfully ..!!');window.location='Inventory.aspx';", true);
                    FillGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('outward quantity not matched ..!!');", true);
                    this.ModalPopupHistory.Show();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Deffect wise quantity not match to total defect quantity ..!!');", true);
                this.ModalPopupHistory.Show();
            }

        }
        catch (Exception ex)
        {

        }

    }

    //Search RMC Search methods
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetRMCList(string prefixText, int count)
    {
        return AutoFillRowmName(prefixText);
    }

    public static List<string> AutoFillRowmName(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select ComponentName from tbl_LM_ComponentMaster where IsDeleted=0 and Status=1 AND " + "ComponentName like '%'+ @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        countryNames.Add(sdr["ComponentName"].ToString());
                    }
                }
                con.Close();
                return countryNames;
            }
        }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        // divinwardform.Visible = true;
        divtabl.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // divinwardform.Visible = false;
        divtabl.Visible = true;
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
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "SELECT DISTINCT [ID],[Companyname] FROM [tbl_LM_CompanyMaster] where " + "Companyname like @Search + '%' and IsDeleted=0";

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

    protected void GVPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    //protected void txtDefectqty_TextChanged(object sender, EventArgs e)
    //{
    //    Double inwardqty = Convert.ToDouble(txtRemaining.Text);
    //    Double outwardqty = Convert.ToDouble(txtoutwardqty.Text);
    //    Double totaldefect = 0;

    //    totaldefect = Convert.ToDouble(txtDefectqty.Text);
    //    Double total = inwardqty - outwardqty - totaldefect;

    //    this.ModalPopupHistory.Show();
    //}

    //protected void txtoutwardqty_TextChanged(object sender, EventArgs e)
    //{
    //    Double inwardqty = Convert.ToDouble(txtinwardqty.Text);
    //    Double outwardqty = Convert.ToDouble(txtRemaining.Text);
    //    Double totaldefect = 0;
    //    if(inwardqty>=outwardqty)
    //    {
    //        //if (txtDefectqty.Text != "")
    //        //{
    //        //    totaldefect = Convert.ToDouble(txtDefectqty.Text);
    //        //    Double total = inwardqty - outwardqty - totaldefect;
    //        //    txtRemaining.Text = Convert.ToString(total);
    //        //}
    //        //else
    //        //{
    //        //    Double total = inwardqty - outwardqty;
    //        //    txtRemaining.Text = Convert.ToString(total);
    //        //}
    //    }
    //    else
    //    {
    //        txtoutwardqty.Text = "";
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add quantity less then inward quantity..!!');", true);
    //    }


    //    this.ModalPopupHistory.Show();
    //}

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Laxshmi/Inventory.aspx");
    }

    protected void GVDefects_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void lnkbtnadd_Click(object sender, EventArgs e)
    {
        if (txtDefectqty.Text != "" && txtdefectedqty.Text != "")
        {


            Double totaldefect = 0;
            foreach (GridViewRow grd1 in GVDefects.Rows)
            {

                string lblQty = (grd1.FindControl("lblQty") as Label).Text;
                totaldefect += Convert.ToDouble(lblQty);
            }
            Double defectqty = Convert.ToDouble(txtDefectqty.Text);
            Double defty = Convert.ToDouble(txtdefectedqty.Text) + totaldefect;
            if (defty <= defectqty)
            {
                ViewState["RowNo"] = (int)ViewState["RowNo"] + 1;
                DataTable Dt = (DataTable)ViewState["Details"];
                Dt.Rows.Add(ViewState["RowNo"], ddlDefectsType.SelectedItem.Text.Trim(), txtdefectedqty.Text.Trim());
                ViewState["Details"] = Dt;
                txtdefectedqty.Text = string.Empty;
                GVDefects.DataSource = (DataTable)ViewState["Details"];
                GVDefects.DataBind();
                this.ModalPopupHistory.Show();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add quantity not match total defect quantity..!!');", true);
                txtdefectedqty.Text = "";
            }

            this.ModalPopupHistory.Show();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add total defect count..!!');", true);
            this.ModalPopupHistory.Show();
        }
     
    }

    protected void txtdefectedqty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDefectqty.Text != "")
            {


                Double totaldefect = 0;
                foreach (GridViewRow grd1 in GVDefects.Rows)
                {

                    string lblQty = (grd1.FindControl("lblQty") as Label).Text;
                    totaldefect += Convert.ToDouble(lblQty);
                }
                Double defectqty = Convert.ToDouble(txtDefectqty.Text);
                Double defty = Convert.ToDouble(txtdefectedqty.Text) + totaldefect;
                if (defty <= defectqty)
                {


                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add quantity not match total defect quantity..!!');", true);
                    txtdefectedqty.Text = "";
                }

                this.ModalPopupHistory.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please add total defect count..!!');", true);
                this.ModalPopupHistory.Show();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txtCustomerName_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
    }

    protected void txtRowMaterial_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
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

    //protected void txtInwardno_TextChanged(object sender, EventArgs e)
    //{
    //    FillGrid();

    //}

    protected void btnSearchData_Click(object sender, EventArgs e)
    {
        FillGrid();

    }

    protected void btnresetfilter_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inventory.aspx");
    }

    protected void ImgbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            DataTable dt = ViewState["Details"] as DataTable;
            dt.Rows.Remove(dt.Rows[row.RowIndex]);
            ViewState["Details"] = dt;
            GVDefects.DataSource = (DataTable)ViewState["Details"];
            GVDefects.DataBind();

            //Double totaldefect = 0;
            //foreach (GridViewRow grd1 in GVDefects.Rows)
            //{
            //    string lblQty = (grd1.FindControl("lblQty") as Label).Text;
            //    totaldefect += Convert.ToDouble(lblQty);
            //}

            //Double Defectremain = Convert.ToDouble(txtDefectqty.Text) - totaldefect;
            //txtRemaining.Text = Convert.ToString(Convert.ToDouble(txtRemaining.Text) + Defectremain);
            //Double TotalDefect = Convert.ToDouble(txtDefectqty.Text) - Defectremain;
            //txtDefectqty.Text = Convert.ToString(TotalDefect);
            //txtoutwardqty.Text = txtRemaining.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Details Delete Succesfully !!!');", true);
            this.ModalPopupHistory.Show();
        }
        catch
        {

        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inventory.aspx");
    }

    protected void btnSendtopro_Click(object sender, EventArgs e)
    {
        try
        {
            Cls_Main.Conn_Open();
            SqlCommand cmdd = new SqlCommand("INSERT INTO [dbo].[tbl_LM_DefectOutward] ([CompanyName] ,RowMaterial,[DEFDeleveryNo] ,[DEFDeliveryDate] ,[DEFReferanceNo] ,[DEFReferanceDate] ,[DEFVehicleNo] ,DefectoutQty,[DEFQty] ,[Weight],[CreatedBy] ,[CreatedOn]) VALUES(@CompanyName,@RowMaterial,@DEFDeleveryNo,@DEFDeliveryDate,@DEFReferanceNo,@DEFReferanceDate,@DEFVehicleNo,@DefectoutQty,@DEFQty,@Weight,@CreatedBy,@CreatedOn)", Cls_Main.Conn);
            cmdd.Parameters.AddWithValue("@CompanyName", txtcustomerdefeted.Text);
            cmdd.Parameters.AddWithValue("@RowMaterial", txtrowmaterialdef.Text);
            cmdd.Parameters.AddWithValue("@DEFDeleveryNo", txtDeliverynotenodef.Text);
            cmdd.Parameters.AddWithValue("@DEFDeliveryDate", txtDeliverynotedatedef.Text);
            cmdd.Parameters.AddWithValue("@DEFReferanceNo", txtrefrencenodef.Text);
            cmdd.Parameters.AddWithValue("@DEFReferanceDate", txtReferenceDatedef.Text);
            cmdd.Parameters.AddWithValue("@DefectoutQty", txtoutqantity.Text);
            cmdd.Parameters.AddWithValue("@DEFQty", txtdefectedqtydef.Text);
            cmdd.Parameters.AddWithValue("@DEFVehicleNo", txtVehiclenodef.Text);
            cmdd.Parameters.AddWithValue("@Weight", txtWeightdef.Text);
            cmdd.Parameters.AddWithValue("@CreatedBy", Session["UserCode"].ToString());
            cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
            cmdd.ExecuteNonQuery();
            Cls_Main.Conn_Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Defect Outward Successfully ..!!');window.location='Inventory.aspx';", true);



        }
        catch
        {

        }

    }

}


