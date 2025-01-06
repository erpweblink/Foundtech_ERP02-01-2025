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
using System.Windows.Threading;

public partial class AdminMaster : System.Web.UI.MasterPage
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["Username"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {

                PageAuthorization();
                lblname.Text = Session["Username"].ToString();
            }
        }

    }
    protected void PageAuthorization()
    {
        string username = Session["ID"].ToString();
        DataTable dt = new DataTable();
        SqlCommand cmd1 = new SqlCommand("SELECT * FROM [tblUserRoleAuthorization] where UserID='" + username + "'", con);
        SqlDataAdapter sad = new SqlDataAdapter(cmd1);
        sad.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            foreach (DataRow row in dt.Rows)
            {
                string MenuName = row["PageName"].ToString();
                //Masters
                if (MenuName == "UserMaster.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    Li1.Visible = page1 == "True" ? true : false;
                }
                if (MenuName == "RoleList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Li2.Visible = false;
                    }
                    else
                    {
                        Li2.Visible = true;
                    }
                }
                if (MenuName == "CompanyMasterList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        companymasterid.Visible = false;
                    }
                    else
                    {
                        companymasterid.Visible = true;
                    }
                }
                if (MenuName == "ComponentList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        componentmasterid.Visible = false;
                    }
                    else
                    {
                        componentmasterid.Visible = true;
                    }
                }
                if (MenuName == "SupplierMasterList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        suppliermasterid.Visible = false;
                    }
                    else
                    {
                        suppliermasterid.Visible = true;
                    }
                }
                if (MenuName == "TransporterList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        transportmastereID.Visible = false;
                    }
                    else
                    {
                        transportmastereID.Visible = true;
                    }
                }
                if (MenuName == "QuatationList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        Quotationlsit.Visible = false;
                    }
                    else
                    {
                        Quotationlsit.Visible = true;
                    }
                }

                //Sales section
                if (MenuName == "OAList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        oaid.Visible = false;
                    }
                    else
                    {
                        oaid.Visible = true;
                    }
                }
                if (MenuName == "ProdListGPWise.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ProdListId.Visible = false;
                    }
                    else
                    {
                        ProdListId.Visible = true;
                    }
                }
                if (MenuName == "DrawingDetails.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        DrawingId.Visible = false;
                    }
                    else
                    {
                        DrawingId.Visible = true;
                    }
                }
                if (MenuName == "PlazmaCutting.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PlazmaCuttingId.Visible = false;
                    }
                    else
                    {
                        PlazmaCuttingId.Visible = true;
                    }
                }
                if (MenuName == "Fabrication.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        FabricationId.Visible = false;
                    }
                    else
                    {
                        FabricationId.Visible = true;
                    }
                }
                if (MenuName == "Bending.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        BendingId.Visible = false;
                    }
                    else
                    {
                        BendingId.Visible = true;
                    }
                }
                //Account section
                if (MenuName == "Painting.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PaintingId.Visible = false;
                    }
                    else
                    {
                        PaintingId.Visible = true;
                    }
                }
                if (MenuName == "Packing.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        PackingId.Visible = false;
                    }
                    else
                    {
                        PackingId.Visible = true;
                    }
                }
                if (MenuName == "Dispatch.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        DispatchId.Visible = false;
                    }
                    else
                    {
                        DispatchId.Visible = true;
                    }
                }

                if (MenuName == "InwardEntry.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        InwardEntryId.Visible = false;
                    }
                    else
                    {
                        InwardEntryId.Visible = true;
                    }
                }

                //Purchase Section
                if (MenuName == "Inventory.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        InventoryId.Visible = false;
                    }
                    else
                    {
                        InventoryId.Visible = true;
                    }
                }
                if (MenuName == "StoreList.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        StoreListId.Visible = false;
                    }
                    else
                    {
                        StoreListId.Visible = true;
                    }
                }
                if (MenuName == "ReturnInventory.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        ReturnInventoryId.Visible = false;
                    }
                    else
                    {
                        ReturnInventoryId.Visible = true;
                    }
                }
                if (MenuName == "UserAuthorization.aspx")
                {
                    string page1 = row["Pages"].ToString();
                    string pageView = row["PagesView"].ToString();
                    if (page1 == "False" && pageView == "False")
                    {
                        UserAuthorizationId.Visible = false;
                    }
                    else
                    {
                        UserAuthorizationId.Visible = true;
                    }
                }


                if (Li1.Visible == false && Li2.Visible == false && companymasterid.Visible == false && componentmasterid.Visible == false && suppliermasterid.Visible == false && transportmastereID.Visible == false)
                {
                    MasterId.Visible = false;
                }
                if (Quotationlsit.Visible == false && oaid.Visible == false)
                {
                    SalesId.Visible = false;
                }
                if (DrawingId.Visible == false &&  ProdListId.Visible == false && PlazmaCuttingId.Visible == false && FabricationId.Visible == false && BendingId.Visible == false && PaintingId.Visible == false && PackingId.Visible == false && DispatchId.Visible == false)
                {
                    ProductionId.Visible = false;
                }
                if (InwardEntryId.Visible == false && InventoryId.Visible == false && StoreListId.Visible == false && ReturnInventoryId.Visible == false)
                {
                    StoreId.Visible = false;
                }
            }
        }
        else
        {
            Response.Redirect("~/Login.aspx");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", "alert('MyButton clicked!');", true);

        }
    }
}
