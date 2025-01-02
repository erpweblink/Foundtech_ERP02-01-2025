<%@ Page Title="" Language="C#" MasterPageFile="~/LaxmiMaster.master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="Laxshmi_Inventory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Content1/css/styles.css" rel="stylesheet" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">

    <%--    <style>
        .completionList {
            background-color: #f1f1f1;
            border: 1px solid #ccc;
            z-index: 10000 !important; /* Ensure it's higher than the modal */
            position: absolute; /* Ensure the list is positioned correctly */
        }

        .itemHighlighted {
            background-color: #007bff;
            color: white;
        }

        .listItem {
            padding: 5px;
            cursor: pointer;
        }
    </style>--%>
    <style>
        .spncls {
            color: red;
        }

        .spancls {
            color: #5d5656 !important;
            font-size: 13px !important;
            font-weight: 600;
            text-align: left;
        }

        .starcls {
            color: red;
            font-size: 18px;
            font-weight: 700;
        }

        .card .card-header span {
            color: #060606;
            display: block;
            font-size: 13px;
            margin-top: 5px;
        }

        .errspan {
            float: right;
            margin-right: 6px;
            margin-top: -25px;
            position: relative;
            z-index: 2;
            color: black;
        }

        .currentlbl {
            text-align: center !important;
        }

        /*.completionList {
            border: solid 1px Gray;
            border-radius: 5px;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }*/

        .reqcls {
            color: red;
            font-weight: 600;
            font-size: 14px;
        }

        .aspNetDisabled {
            cursor: not-allowed !important;
        }

        .rwotoppadding {
            padding-top: 10px;
        }
    </style>
    <style>
        .modelprofile1 {
            background-color: rgba(0, 0, 0, 0.54);
            display: block;
            position: fixed;
            z-index: 1;
            left: 0;
            /*top: 10px;*/
            height: 100%;
            overflow: auto;
            width: 100%;
            margin-bottom: 25px;
        }

        .profilemodel2 {
            background-color: #fefefe;
            margin-top: 25px;
            /*padding: 17px 5px 18px 22px;*/
            padding: 0px 0px 15px 0px;
            width: 100%;
            top: 40px;
            color: #000;
            border-radius: 5px;
        }

        .lblpopup {
            text-align: left;
        }

        .wp-block-separator:not(.is-style-wide):not(.is-style-dots)::before, hr:not(.is-style-wide):not(.is-style-dots)::before {
            content: '';
            display: block;
            height: 1px;
            width: 100%;
            background: #cccccc;
        }

        .btnclose {
            background-color: #ef1e24;
            float: right;
            font-size: 18px !important;
            /* font-weight: 600; */
            color: #f7f6f6 !important;
            border: 0px groove !important;
            background-color: none !important;
            /*margin-right: 10px !important;*/
            cursor: pointer;
            font-weight: 600;
            border-radius: 4px;
            padding: 4px;
        }

        /*hr {
            margin-top: 5px !important;
            margin-bottom: 15px !important;
            border: 1px solid #eae6e6 !important;
            width: 100%;
        }*/
        hr.new1 {
            border-top: 1px dashed green !important;
            border: 0;
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            width: 100%;
        }

        .errspan {
            float: right;
            margin-right: 6px;
            margin-top: -25px;
            position: relative;
            z-index: 2;
            color: black;
        }

        .currentlbl {
            text-align: center !important;
        }

        .completionList {
            border: solid 1px Gray;
            border-radius: 5px;
            margin: 0px;
            padding: 3px;
            height: 120px;
            overflow: auto;
            background-color: #FFFFFF;
        }

        .listItem {
            color: #191919;
        }

        .itemHighlighted {
            background-color: #ADD6FF;
        }

        .headingcls {
            background-color: #01a9ac;
            color: #fff;
            padding: 15px;
            border-radius: 5px 5px 0px 0px;
        }

        @media (min-width: 1200px) {
            .container {
                max-width: 1250px !important;
            }
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../Content1/img/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../Content1/img/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $('#<%= txtVehicleno.ClientID %>').on('input', function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container-fluid px-4">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="card">
                        <div class="card-body">
                            <div id="divtabl" runat="server">
                                <div class="row">
                                    <div class="col-9 col-md-10">
                                        <h4 class="mt-4 "><b>OUTWARD LIST</b></h4>
                                    </div>
                                    <%--    <div class="col-3 col-md-2 mt-4">
                                        <asp:Button ID="btnCreate" CssClass="form-control btn btn-warning" OnClick="btnCreate_Click" runat="server" Text="Inward Entry" />
                                    </div>--%>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="margin-top: 14px;">
                                            <asp:Label ID="lblcompanyname" Font-Bold="true" runat="server" Text="Company Name :"></asp:Label>
                                            <asp:TextBox ID="txtCustomerName" CssClass="form-control" placeholder="Search Customer" runat="server" OnTextChanged="txtCustomerName_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                TargetControlID="txtCustomerName">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="margin-top: 14px;">
                                            <asp:Label ID="Label17" Font-Bold="true" runat="server" Text="Material :"></asp:Label>
                                            <asp:TextBox ID="txtRowMaterial" CssClass="form-control" placeholder="Search Material " runat="server" OnTextChanged="txtRowMaterial_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRMCList"
                                                TargetControlID="txtRowMaterial">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                        <div style="margin-top: 14px;">
                                            <asp:Label ID="Label18" Font-Bold="true" runat="server" Text="Inward Number :"></asp:Label>
                                            <asp:TextBox ID="txtInwardno" CssClass="form-control" placeholder="Search Inward Number " runat="server" OnTextChanged="txtInwardno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetInwardnoList"
                                                TargetControlID="txtInwardno">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>--%>
                                    <div class="col-md-3" style="text-align: center; margin-top: 30px">
                                        <asp:Button ID="btnSearchData" CssClass="btn btn-success" OnClick="btnSearchData_Click" runat="server" Text="Search" Style="padding: 8px;" />
                                        <asp:Button ID="btnresetfilter" OnClick="btnresetfilter_Click" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" />
                                    </div>
                                </div>
                                <div>
                                    <div class="table-responsive">
                                        <div class="table">
                                            <br />
                                            <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="CompanyName,TotalInwardQty" PageSize="10" AllowPaging="true" Width="100%" OnRowDataBound="GVPurchase_RowDataBound" OnRowEditing="GVPurchase_RowEditing"
                                                OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CustomerName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MaterialName" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Qty" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="InwardQty" runat="server" Text='<%#Eval("TotalRemainingQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Out Qty" ItemStyle-HorizontalAlign="Center"  Visible="false">

                                                        <ItemTemplate>
                                                            <asp:Label ID="OutwardQty" runat="server" Text='<%#Eval("TotalOutwardQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Defected Qty" ItemStyle-HorizontalAlign="Center"  Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DefectedQty1" runat="server" Text='<%#Eval("DefectedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                               
                                                    <asp:TemplateField HeaderText="Remaining Qty" ItemStyle-HorizontalAlign="Center">

                                                        <ItemTemplate>
                                                            <asp:Label ID="RemainingQty" runat="server" Text='<%#Eval("TotalRemainingQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Defected Qty" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DefectedQty" runat="server" Text='<%#Eval("RemainingDefectQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Width="20" HeaderText="Defects" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                <br />
                                                                <asp:GridView ID="gvDetails" runat="server" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="DefectType" HeaderText="Defect Type" />
                                                                        <asp:BoundField ItemStyle-Width="150px" DataField="DefectQty" HeaderText="Defect Quantity" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnoutward" Height="27px" ToolTip="Create Outward Entry" CausesValidation="false" CommandName="RowOutward" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-shipping-fast"  style="font-size: 24px; color:orangered; "></i></asp:LinkButton>&nbsp;

                                                             <asp:LinkButton runat="server" ID="lnkDefectout" Height="27px" ToolTip="Create Defect Outward Entry" CausesValidation="false" CommandName="RowDefect" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-shipping-fast"  style="font-size: 24px; color:green; "></i></asp:LinkButton>&nbsp;
                                                      <%--  <asp:LinkButton ID="btnEdit" runat="server" Height="27px" ToolTip="Edit Inward record" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%# Container.DataItemIndex %>'><i class='fas fa-edit' style='font-size:24px;color: blue;'></i></asp:LinkButton>--%>

                                                            <%--   <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-trash' style='font-size:24px;color: red;'></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />

                    <asp:Panel ID="PopupHistoryDetail" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-3"></div>
                            <div class="col-md-8">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Outward Entry
                                    <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>

                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">Outward No:</asp:Label>
                                                <asp:TextBox ID="txtInwardnopop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Customer Name:</asp:Label>
                                                <asp:TextBox ID="txtcustomernamepop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Row Material:</asp:Label>
                                                <asp:TextBox ID="txtrowmaterialpop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Inward QTY:</asp:Label>
                                                <asp:TextBox ID="txtinwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                <asp:TextBox ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Outward Quantity"
                                                    ControlToValidate="txtoutwardqty" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label">Total Defect QTY:</asp:Label>
                                                <asp:TextBox ID="txtDefectqty" placeholder="Enter Total Defect QTY" TextMode="Number" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Remaining QTY:</asp:Label>
                                                <asp:TextBox ID="txtRemaining" placeholder="Enter Remaining QTY" ReadOnly="true" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Vehicle  No. :</asp:Label>
                                                <asp:TextBox ID="txtVehicleno" CssClass="form-control" placeholder="Enter Vehicle  No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="Please Enter Vehicle  No."
                                                    ControlToValidate="txtVehicleno" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight :</asp:Label>
                                                <asp:TextBox ID="txtWeight" CssClass="form-control" placeholder="Enter Weight" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Weight"
                                                    ControlToValidate="txtWeight" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label27" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Note No.:</asp:Label>
                                                <asp:TextBox ID="txtDeliverynoteno" CssClass="form-control" placeholder="Enter Delivery Note No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Note No."
                                                    ControlToValidate="txtDeliverynoteno" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label26" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Note Date:</asp:Label>
                                                <asp:TextBox ID="txtDeliverynotedate" CssClass="form-control" placeholder="Enter Delivery Date" TextMode="date" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Date"
                                                    ControlToValidate="txtDeliverynotedate" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label28" runat="server" Font-Bold="true" CssClass="form-label">Reference No.:</asp:Label>
                                                <asp:TextBox ID="txtrefrenceno" CssClass="form-control" placeholder="Enter Reference No." runat="server"></asp:TextBox>
                                           </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label29" runat="server" Font-Bold="true" CssClass="form-label">Reference Date:</asp:Label>
                                                <asp:TextBox ID="txtReferenceDate" CssClass="form-control" placeholder="Enter Delivery Date" TextMode="date" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                            <hr />
                                            <div class="col-md-12 mb-3">
                                                <b>Add Defects Wise Quantity</b>

                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Defects Type:</asp:Label>
                                                <asp:DropDownList ID="ddlDefectsType" CssClass="form-control" runat="server">
                                                    <%--   <asp:ListItem Value="0" Text="--select--"></asp:ListItem>--%>
                                                    <%--     <asp:ListItem Value="Cracks in the bending angle" Text="Cracks in the bending angle"></asp:ListItem>
                                                    <asp:ListItem Value="Hole deformation" Text="Hole deformation"></asp:ListItem>
                                                    <asp:ListItem Value="Unstable bending angle" Text="Unstable bending angle"></asp:ListItem>
                                                    <asp:ListItem Value="Splits" Text="Splits"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label">Defected QTY:</asp:Label>
                                                <asp:TextBox ID="txtdefectedqty" placeholder="Enter Defected QTY" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="txtdefectedqty_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtdefectedqty"
                                                    ErrorMessage="Defected quantity is required."
                                                    ValidationGroup="f"
                                                    CssClass="text-danger"
                                                    Display="Dynamic" />
                                                <br />
                                                <asp:LinkButton runat="server" ID="lnkbtnadd" ValidationGroup="f" class="btn btn-secondary" OnClick="lnkbtnadd_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-plus"></i>
                                                        </span>
                                                       Add Defects 
                                                </asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GVDefects" CssClass="display table table-striped table-hover" AutoGenerateColumns="false" runat="server" CellPadding="4" ForeColor="#333333" PageSize="10" AllowPaging="true" Width="100%" OnRowEditing="GVDefects_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SR.NO" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defects" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDefects" runat="server" Text='<%# Eval("Defect") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defect QTY" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("DefectQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="ImgbtnDelete" ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" OnClick="ImgbtnDelete_Click" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="col-md-12">
                                                <div class="col-md-4"></div>
                                                <div class="col-md-3" style="margin-top: 18px">
                                                    <asp:LinkButton runat="server" ID="btnSendtopro" ValidationGroup="1" class="btn btn-success" OnClick="btnsave_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-check"></i>
                                                        </span>
                                                       Submit 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="LinkButton2" CausesValidation="false" class="btn btn-warning" OnClick="LinkButton2_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-back"></i>
                                                        </span>
                                                       Back 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </asp:Panel>

                    <asp:Button ID="btnhist1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory1" runat="server" TargetControlID="btnhist1"
                        PopupControlID="PopupHistoryDetail1" OkControlID="Closepophistory1" />

                    <asp:Panel ID="PopupHistoryDetail1" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-3"></div>
                            <div class="col-md-8">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Outward Defect Entry
                   <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Customer Name:</asp:Label>
                                                <asp:TextBox ID="txtcustomerdefeted" CssClass="form-control" ReadOnly="true" placeholder="Customer Name" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="true" CssClass="form-label">Row Material:</asp:Label>
                                                <asp:TextBox ID="txtrowmaterialdef" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="true" CssClass="form-label">Defected Qty:</asp:Label>
                                                <asp:TextBox ID="txtdefectedqtydef" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Defect Out QTY:</asp:Label>
                                                <asp:TextBox ID="txtoutqantity" CssClass="form-control" placeholder="Enter Inward Qantity" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter out Qantity"
                                                    ControlToValidate="txtoutqantity" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Note No.:</asp:Label>
                                                <asp:TextBox ID="txtDeliverynotenodef" CssClass="form-control" placeholder="Enter Delivery Note No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Note No."
                                                    ControlToValidate="txtDeliverynotenodef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Note Date:</asp:Label>
                                                <asp:TextBox ID="txtDeliverynotedatedef" CssClass="form-control" placeholder="Enter Delivery Date" TextMode="date" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Date"
                                                    ControlToValidate="txtDeliverynotedatedef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Reference No.:</asp:Label>
                                                <asp:TextBox ID="txtrefrencenodef" CssClass="form-control" placeholder="Enter Reference No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter Reference No."
                                                    ControlToValidate="txtrefrencenodef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Reference Date:</asp:Label>
                                                <asp:TextBox ID="txtReferenceDatedef" CssClass="form-control" placeholder="Enter Delivery Date" TextMode="date" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Enter Reference Date"
                                                    ControlToValidate="txtReferenceDatedef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label18" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight :</asp:Label>
                                                <asp:TextBox ID="txtWeightdef" CssClass="form-control" placeholder="Enter Weight" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter Weight"
                                                    ControlToValidate="txtWeightdef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Vehicle  No. :</asp:Label>
                                                <asp:TextBox ID="txtVehiclenodef" CssClass="form-control" placeholder="Enter Vehicle  No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ErrorMessage="Please Enter Vehicle  No."
                                                    ControlToValidate="txtVehiclenodef" ValidationGroup="2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <hr />
                                            <div class="col-md-12">
                                                <div class="col-md-4"></div>
                                                <div class="col-md-3" style="margin-top: 18px">
                                                    <asp:LinkButton runat="server" ID="LinkButton1" ValidationGroup="2" class="btn btn-success" OnClick="btnSendtopro_Click">
                                       <span class="btn-label">
                                           <i class="fa fa-check"></i>
                                       </span>
                                      Submit 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkBack" CausesValidation="false" class="btn btn-warning" OnClick="lnkBack_Click">
                                       <span class="btn-label">
                                           <i class="fa fa-back"></i>
                                       </span>
                                      Back 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendtopro" />
                <asp:PostBackTrigger ControlID="LinkButton1" />


            </Triggers>
        </asp:UpdatePanel>
    </form>

</asp:Content>

