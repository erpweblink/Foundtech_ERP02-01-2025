using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Store_StoreList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    CommonCls objcls = new CommonCls();
    DataTable Dt_Component = new DataTable();
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
                GetstoreList();
            }
        }


    }



    protected void GVPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RowEdit")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
            var inwardNo = GVPurchase.DataKeys[rowIndex]["InwardNo"].ToString();
            var ID = GVPurchase.DataKeys[rowIndex]["ID"].ToString();
            string RowMaterial = ((Label)row.FindControl("RowMaterial")).Text;
            string Thickness = ((Label)row.FindControl("Thickness")).Text;
            string Width = ((Label)row.FindControl("Width")).Text;
            string Length = ((Label)row.FindControl("Length")).Text;
            string Weight = ((Label)row.FindControl("Weight")).Text;
            string NeedQty = ((Label)row.FindControl("NeedQty")).Text;
            string PerWeight = ((Label)row.FindControl("PerWeight")).Text;
            GetAvailableDetals(RowMaterial, Thickness, Width, Length, NeedQty, Weight,PerWeight);

            if(PerWeight != "")
            {
                PerWeightVi.Visible = true;
                txtThickness.Enabled = false;
                txtThickness.Text = "0";
                txtwidth.Enabled = false;
                txtwidth.Text = "0";
                txtlength.Enabled = false;
                txtlength.Text = "0";
                Txtweight.Enabled = false;
                txtPerWeight.Text = "";
                txtApprovQuantity.Text = "";
                Txtweight.Text = "";
            }
            else
            {
                PerWeightVi.Visible = false;
                txtPerWeight.Text = "0";
                txtThickness.Enabled = true;
                txtThickness.Text = "";
                txtwidth.Enabled = true;
                txtwidth.Text = "";
                txtlength.Enabled = true;
                txtlength.Text = "";
                txtApprovQuantity.Text = "";
                Txtweight.Text = "";
                Txtweight.Enabled = false;
            }

            HDnInward.Value = Convert.ToString(inwardNo);
            HddnID.Value = Convert.ToString(ID);
            this.ModalPopupHistory.Show();
            txtRMC.Text = RowMaterial;
        }

        if (e.CommandName == "RejectCancel")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GVPurchase.Rows[rowIndex];
            var ID = GVPurchase.DataKeys[rowIndex]["ID"].ToString();
            HddnID.Value = Convert.ToString(ID);
            string Mode = "CancelRequest";
            UpdateStatus(Mode);

        }


        if (e.CommandName == "RowDelete")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            var ID = GVPurchase.DataKeys[rowIndex]["ID"].ToString();
            HddnID.Value = Convert.ToString(ID);
            string Mode = "DeleteRecord";
            UpdateStatus(Mode);
            GetstoreList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Delete Record Successfully..!!');", true);
        }



    }

    protected void GVPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    public void GetstoreList()
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_StoreDeatils", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetInwarteddata");
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('" + errorMsg + "') ", true);

        }


    }

    public void GetAvailableDetals(string RowMaterial, string Thickness, string Width, string Length, string NeedQty, string Weight, string PerWeight)
    {
        try
        {
            SqlCommand cmd = new SqlCommand("SP_StoreDeatils", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Mode", "GetAvialbleDeatils");
            cmd.Parameters.AddWithValue("@RowMaterial", RowMaterial);
            cmd.Parameters.AddWithValue("@Thickness", Thickness);
            cmd.Parameters.AddWithValue("@Width", Width);
            cmd.Parameters.AddWithValue("@Length", Length);
            cmd.Parameters.AddWithValue("@PerWeight", PerWeight);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0) // Check if there are any rows in the DataTable
            {
                txtavailableQty.Text = dt.Rows[0]["AvilableQty"].ToString();
                txtThickness.Text = Thickness;
                txtwidth.Text = Width;
                txtlength.Text = Length;
                txtApprovQuantity.Text = NeedQty;
                Txtweight.Text = Weight;
                txtRMC.Text = RowMaterial;
            }
            else
            {
                txtavailableQty.Text = "Not Available";
                txtavailableQty.ForeColor = Color.Red;
            }


        }
        catch (Exception ex)
        {

            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('" + errorMsg + "') ", true);

        }
    }

    protected void btnSendtopro_Click(object sender, EventArgs e)
    {
        try
        {

            Cls_Main.Conn_Open();
            SqlCommand cmd = new SqlCommand("SP_StoreDeatils", Cls_Main.Conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Createdby", Session["UserCode"].ToString());
            cmd.Parameters.AddWithValue("@Mode", "UpdateInwardQty");
            cmd.Parameters.AddWithValue("@Quantity", txtApprovQuantity.Text);
            cmd.Parameters.AddWithValue("@Thickness", txtThickness.Text);
            cmd.Parameters.AddWithValue("@Width", txtwidth.Text);
            cmd.Parameters.AddWithValue("@Length", txtlength.Text);
            cmd.Parameters.AddWithValue("@Weight", Txtweight.Text);
            cmd.Parameters.AddWithValue("@PerWeight", txtPerWeight.Text);
            cmd.Parameters.AddWithValue("@ID", HddnID.Value);
            cmd.ExecuteNonQuery();
            Cls_Main.Conn_Close();
            Cls_Main.Conn_Dispose();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Saved Record Successfully..!!');", true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }


    public void UpdateStatus(String Mode)
    {
        try
        {
            Cls_Main.Conn_Open();
            SqlCommand cmd1 = new SqlCommand("SP_StoreDeatils", Cls_Main.Conn);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@Mode", Mode);
            cmd1.Parameters.AddWithValue("@ID", SqlDbType.BigInt).Value = Convert.ToInt64(HddnID.Value);
            var result = cmd1.ExecuteScalar();
            Cls_Main.Conn_Close();
            Cls_Main.Conn_Dispose();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "SuccessResult('Reject Request Successfully..!!');", true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    protected void txtThickness_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtwidth_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtlength_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    protected void txtneedqty_TextChanged(object sender, EventArgs e)
    {
        Getdata();
    }

    public void Getdata()
    {
        try
        {
            if (txtPerWeight.Text == "0")
            {
                DataTable dtpt = Cls_Main.Read_Table("select SUM(CAST(InwardQty AS FLOAT)) AS Quantity from tbl_InwardData WHERE RowMaterial='" + txtRMC.Text.Trim() + "' AND Thickness='" + txtThickness.Text.Trim() + "' AND Width='" + txtwidth.Text.Trim() + "' AND Length='" + txtlength.Text.Trim() + "' AND IsDeleted=0");
                if (dtpt.Rows.Count > 0)
                {
                    txtavailableQty.Text = dtpt.Rows[0]["Quantity"] != DBNull.Value ? dtpt.Rows[0]["Quantity"].ToString() : "0";

                }
                else
                {

                }
                if (txtThickness.Text != "" && txtwidth.Text != "" && txtlength.Text != "")
                {
                    double thickness = Convert.ToDouble(txtThickness.Text);
                    double width = Convert.ToDouble(txtwidth.Text);
                    double length = Convert.ToDouble(txtlength.Text);
                    double Quantity = string.IsNullOrEmpty(txtApprovQuantity.Text) ? 0 : Convert.ToDouble(txtApprovQuantity.Text);

                    // Ensure inputs are non-negative
                    if (thickness <= 0 || width <= 0 || length <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please enter positive values for thickness, width, and length...!!');", true);

                    }

                    // Calculate weight in kilograms
                    double weight = length / 1000 * width / 1000 * thickness * 7.85;
                    double totalweight = weight * Quantity;
                    // Display the calculated weight
                    Txtweight.Text = totalweight.ToString();
                }
            }
            else
            {
              
                double PerWeight = Convert.ToDouble(txtPerWeight.Text);
                double Quantity = string.IsNullOrEmpty(txtApprovQuantity.Text) ? 0 : Convert.ToDouble(txtApprovQuantity.Text);

                // Ensure inputs are non-negative
                if (PerWeight <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "DeleteResult('Please enter positive values for thickness, width, and length...!!');", true);

                }
                double totalweight = PerWeight * Quantity;
                // Display the calculated weight
                Txtweight.Text = totalweight.ToString();

            }
            this.ModalPopupHistory.Show();
        }
        catch { }
    }

    protected void GVPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Authorization
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
                LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
                LinkButton Lnkcancel = e.Row.FindControl("Lnkcancel") as LinkButton;

                string empcode = Session["UserCode"].ToString();
                DataTable Dt = new DataTable();
                SqlDataAdapter Sd = new SqlDataAdapter("Select ID from tbl_UserMaster where UserCode='" + empcode + "'", con);
                Sd.Fill(Dt);
                if (Dt.Rows.Count > 0)
                {
                    string id = Dt.Rows[0]["ID"].ToString();
                    DataTable Dtt = new DataTable();
                    SqlDataAdapter Sdd = new SqlDataAdapter("Select * FROM tblUserRoleAuthorization where UserID = '" + id + "' AND PageName = 'StoreList.aspx' AND PagesView = '1'", con);
                    Sdd.Fill(Dtt);
                    if (Dtt.Rows.Count > 0)
                    {
                        GVPurchase.Columns[10].Visible = false;
                        btnEdit.Visible = false;
                        btnDelete.Visible = false;
                        Lnkcancel.Visible = false;
                    }
                }
            }
        }
        catch
        {

        }
    }
}