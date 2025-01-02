<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AddOA.aspx.cs" Inherits="SalesMarketing_AddOA" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=ddlProduct]").select2();

        });
    </script>
    <style>
        .spncls {
            color: #f20707 !important;
            font-size: 13px !important;
            font-weight: bold;
            text-align: left;
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
        span.select2.select2-container.select2-container--default.select2-container--focus {
            max-width: 100% !important;
        }

        .select2-container {
            box-sizing: border-box;
            display: inline-block;
            margin: 0;
            position: relative;
            vertical-align: middle;
            width: 100% !important;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style>
        .LblStyle {
            font-weight: 500;
            color: black;
        }

        .card_adj {
            margin-bottom: 3px;
            height: 35px;
        }
    </style>

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
                    <div class="row">
                        <div class="col-10 col-md-10">
                            <h4 class="mt-4">&nbsp <b>ORDER ACCEPTANCE</b></h4>
                        </div>
                        <div class="col-md-2 mt-4">
                            <asp:Button ID="LinkButton1" CssClass="form-control btn btn-warning" Font-Bold="true" Text="O. A. List" CausesValidation="false" runat="server" OnClick="Button1_Click" />

                        </div>
                    </div>
                    <hr />
                    <div class="container-fluid px-3" style="padding: 23px;">
                        <%--<h2 class="mt-4 ">Quotation Master</h2>--%>
                        <div class="card mb-4">
                            <div class="card-body ">
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Company Name :</asp:Label>

                                        &nbsp&nbsp&nbsp<asp:LinkButton ID="lnkBtmNew" runat="server" CssClass="lnk" OnClick="lnkBtmNew_Click" CausesValidation="false">+ADD</asp:LinkButton>
                                        <asp:TextBox ID="txtcompanyname" OnTextChanged="txtcompanyname_TextChanged" ValidationGroup="1" AutoPostBack="true" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                            ControlToValidate="txtcompanyname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                            TargetControlID="txtcompanyname" Enabled="true">
                                        </asp:AutoCompleteExtender>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Kindd Att  :</asp:Label>

                                        <asp:DropDownList runat="server" ID="ddlContacts" ValidationGroup="1" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="-- Select Kindd Att --" runat="server" ErrorMessage="Please Add Contact person" ControlToValidate="ddlContacts" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label">User Name:</asp:Label>

                                        <asp:DropDownList runat="server" ID="ddlUser" ValidationGroup="1" CssClass="form-control">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label20" runat="server" Font-Bold="true" CssClass="form-label">Email ID.  :</asp:Label>

                                        <asp:TextBox ID="txtemail" runat="server" ValidationGroup="1" AutoComplete="off" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">OA No.  :</asp:Label>

                                        <asp:TextBox ID="txtpono" runat="server" ForeColor="red" ValidationGroup="1" AutoComplete="off" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please fill Serial No." ControlToValidate="txtpono" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Customer PO No.  :</asp:Label>

                                        <asp:TextBox ID="txtserialno" runat="server" ForeColor="red" ValidationGroup="1" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="1" runat="server" ControlToValidate="txtserialno"
                                            ForeColor="Red" ErrorMessage="* Please Enter OA No" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>P.O. Date  :</asp:Label>

                                        <asp:TextBox ID="txtpodate" runat="server" AutoComplete="off" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please fill P.O. Date." ControlToValidate="txtpodate" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Date  :</asp:Label>

                                        <asp:TextBox ID="txtdeliverydate" runat="server" ValidationGroup="1" AutoComplete="off" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please fill Delivery Date." ControlToValidate="txtdeliverydate" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Mobile No.   :</asp:Label>

                                        <asp:TextBox ID="txtmobileno" ReadOnly="true" CssClass="form-control" placeholder="Enter Mobile No." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label">Refer Quotation   :</asp:Label>

                                        <asp:TextBox ID="txtreferquotation" CssClass="form-control" placeholder="Enter Refer Quotation" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label">GST No. :</asp:Label>
                                        <asp:TextBox ID="txtgstno" ReadOnly="true" CssClass="form-control" placeholder="GST No." runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label">PAN No.   :</asp:Label>
                                        <asp:TextBox ID="txtpanno" CssClass="form-control" placeholder="PAN No." runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Billing Address   :</asp:Label>

                                        <asp:TextBox ID="txtaddress" CssClass="form-control" placeholder="Enter Billing Address" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label3" runat="server" Text=""><b></b></asp:Label>
                                        <asp:Label ID="Label17" runat="server" Font-Bold="true" CssClass="form-label">Shipping Address   :</asp:Label>
                                        <asp:DropDownList ID="ddlShippingaddress" Width="560px"
                                            CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" CssClass="form-label">Payment term(In days) :</asp:Label>

                                        <asp:TextBox ID="txtpaymentterm" ReadOnly="true" CssClass="form-control" placeholder="Enter payment term" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>

                                        <asp:TextBox ID="txtremark" CssClass="form-control" placeholder="Enter Remarks" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label18" runat="server" Font-Bold="true" CssClass="form-label">Attachment :</asp:Label>
                                        <asp:FileUpload ID="AttachmentUpload" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblfile1" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>

                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <div class="col-md-2" style="margin-top: 18px">
                                            <asp:Button ID="uploadfile" runat="server" CausesValidation="false" AutoPostBack="true" Text="Upload" CssClass="form-control btn btn-outline-primary m-2" OnClick="uploadfile_Click" />
                                        </div>
                                    </div>
                                </div>

                                <%--Grid View Start--%>
                                <div class="card-header head" style="margin-top: 10px;" id="idheader" runat="server" visible="false">
                                    <h5 style="color: white">Product Details</h5>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <table class="table table-bordered table-bordered-bd-warning mt-4" border="1" style="width: 100%; border: 1px solid #0c7d38;">
                                                <thead>
                                                    <tr class="gvhead" style="text-align: center">
                                                        <td>Product</td>
                                                        <td>Description</td>
                                                        <td>HSN / SAC</td>
                                                        <td>Quantity/Kg</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtProduct" CssClass="form-control" placeholder="Enter Product" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdescription" Width="230px" TextMode="MultiLine" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txthsnsac" Width="190px" CssClass="form-control" Text="NULL" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtquantity" Width="190px" TextMode="Number" CssClass="form-control" Text="0" OnTextChanged="txtquantity_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                        </td>


                                                    </tr>
                                                </tbody>
                                                <thead>
                                                    <tr class="gvhead" style="text-align: center">

                                                        <td>Rate</td>
                                                        <td colspan="2">CGST</td>
                                                        <td>Unit</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtrate" Width="190px" AutoPostBack="true" TextMode="Number"  Text="0" OnTextChanged="txtrate_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCGST" placeholder="%" Width="100px" TextMode="Number"  Text="0" OnTextChanged="txtCGST_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCGSTamt" Width="100px" ReadOnly="true" TextMode="Number"  Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtunit" Width="190px" CssClass="form-control" Text="Null" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                                <thead>
                                                    <tr class="gvhead" style="text-align: center">
                                                        <td colspan="2">SGST</td>
                                                        <td colspan="2">IGST</td>

                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtSGST" placeholder="%" Width="100px" TextMode="Number"  Text="0" OnTextChanged="txtSGST_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSGSTamt" Width="100px" ReadOnly="true" TextMode="Number"  Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIGST" OnTextChanged="txtIGST_TextChanged" TextMode="Number"  Text="0" AutoPostBack="true" placeholder="%" Width="100px" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIGSTamt" Width="100px" Text="0" TextMode="Number"  CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </tbody>

                                                <thead>
                                                    <tr class="gvhead" style="text-align: center">
                                                        <td>Total </td>
                                                        <td>Discount(%)</td>
                                                        <td>Grand Total</td>
                                                        <td>Weight (Kg)</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>


                                                        <td>
                                                            <asp:TextBox ID="txttotal" Width="100" TextMode="Number"  CssClass="form-control" Text="0" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdiscount" OnTextChanged="txtdiscount_TextChanged" TextMode="Number"  Text="0" Width="80px" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:TextBox ID="txtdiscountamt" Visible="false" Width="80px" Text="0" TextMode="Number"  AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtgrandtotal" Width="150px" Text="0" TextMode="Number"  CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtweight" Width="100" CssClass="form-control" TextMode="Number"  Text="0" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                                <thead>
                                                    <tr class="gvhead" style="text-align: center">

                                                        <td>Action</td>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="btnAddMore" CausesValidation="false" OnClick="btnAddMore_Click" CssClass="btn btn-primary btn-sm btncss" runat="server" Text="Add More" />
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>


                                        <%--<div class="row" id="divdtls">--%>
                                        <div class="table-responsive text-center">
                                            <asp:GridView ID="dgvMachineDetails" runat="server" CellPadding="4" DataKeyNames="id" PageSize="10" AllowPaging="true" Width="100%" CssClass="display table table-striped table-hover"
                                                OnRowEditing="dgvMachineDetails_RowEditing" OnRowDataBound="dgvMachineDetails_RowDataBound" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Productname") %>' ReadOnly="true" CssClass="form-control" Width="230px" ID="txtproduct" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblproduct" runat="Server" Text='<%# Eval("Productname") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Description") %>' CssClass="form-control" ID="txtDescription" Width="200px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="Server" Text='<%# Eval("Description") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HSN" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("HSN") %>' CssClass="form-control" ID="txthsn" Width="200px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblhsn" runat="Server" Text='<%# Eval("HSN") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Quantity") %>' CssClass="form-control" ID="txtQuantity" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="Server" Text='<%# Eval("Quantity") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Units") %>' CssClass="form-control" ID="txtUnit" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnit" runat="Server" Text='<%# Eval("Units") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Rate") %>' CssClass="form-control" OnTextChanged="txtRate_TextChanged" AutoPostBack="true" ID="txtRate" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="Server" Text='<%# Eval("Rate") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Total") %>' CssClass="form-control" ID="txtTotal" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotal" runat="Server" Text='<%# Eval("Total") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CGST %" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("CGSTPer") %>' CssClass="form-control" OnTextChanged="txtCGSTPer_TextChanged" AutoPostBack="true" ID="txtCGSTPer" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGSTPer" runat="Server" Text='<%# Eval("CGSTPer") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CGST" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("CGSTAmt") %>' CssClass="form-control" ID="txtCGST" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" runat="Server" Text='<%# Eval("CGSTAmt") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST %" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("SGSTPer") %>' CssClass="form-control" ID="txtSGSTPer" OnTextChanged="txtSGSTPer_TextChanged" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGSTPer" runat="Server" Text='<%# Eval("SGSTPer") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("SGSTAmt") %>' CssClass="form-control" ID="txtSGST" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" runat="Server" Text='<%# Eval("SGSTAmt") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IGST %" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("IGSTPer") %>' CssClass="form-control" ID="txtIGSTPer" OnTextChanged="txtIGSTPer_TextChanged" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGSTPer" runat="Server" Text='<%# Eval("IGSTPer") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IGST" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("IGSTAmt") %>' CssClass="form-control" ID="txtIGST" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIGST" runat="Server" Text='<%# Eval("IGSTAmt") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount(%)" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Discountpercentage") %>' CssClass="form-control" ID="txtDiscount" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscount" runat="Server" Text='<%# Eval("Discountpercentage") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discount Amount" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("DiscountAmount") %>' CssClass="form-control" ReadOnly="true" ID="txtDiscountAmount" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscountAmount" runat="Server" Text='<%# Eval("DiscountAmount") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grand Total" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Alltotal") %>' CssClass="form-control" ID="txtAlltotal" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAlltotal" runat="Server" Text='<%# Eval("Alltotal") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Weight" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <EditItemTemplate>
                                                            <asp:TextBox Text='<%# Eval("Weight") %>' CssClass="form-control" ID="Weight" Width="100px" runat="server"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWeight" runat="Server" Text='<%# Eval("Weight") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <%--<asp:LinkButton ID="btn_edit" runat="server" Height="27px" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-edit' style='font-size:24px;color: #212529;'></i></asp:LinkButton>--%>

                                                            <asp:LinkButton ID="btn_edit" CausesValidation="false" Text="Edit" runat="server" CommandName="Edit"><i class='fas fa-edit' style='font-size:24px;color: #212529;'></i></asp:LinkButton>

                                                            <asp:LinkButton runat="server" ID="lnkbtnDelete" OnClick="lnkbtnDelete_Click" ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="gv_update" OnClick="gv_update_Click" Text="Update" CausesValidation="false" CssClass="btn btn-primary btn-sm" runat="server"></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="gv_cancel" OnClick="gv_cancel_Click" CausesValidation="false" Text="Cancel" CssClass="btn btn-primary btn-sm " runat="server"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <%--Grid View End--%>


                            <%--last total show--%>

                            <div id="divTotalPart" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <br />
                                                <center>
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lbl_total_amt" runat="server" class="control-label col-sm-6">Total Amount (In Words) :<span class="spncls"></span></asp:Label><br />
                                                        <asp:Label ID="lbl_total_amt_Value" ForeColor="red" class="control-label col-sm-6 font-weight-bold" runat="server" Text=""></asp:Label>
                                                        <asp:HiddenField ID="hfTotal" runat="server" />
                                                    </div>
                                                </center>
                                            </div>
                                            <div class="col-md-6" style="text-align: right">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lbl_Subtotal" runat="server" class="control-label col-sm-6">SubTotal :<span class="spncls"></span></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label runat="server" class="control-label col-sm-6" ReadOnly="true" ID="txt_Subtotal" Text="0.00"></asp:Label><br />
                                                    </div>
                                                </div>
                                                <asp:Panel ID="taxPanel1" runat="server">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lbl_cgst9" runat="server" class="control-label col-sm-6">CGST  Amount :<span class="spncls"></span></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label runat="server" class="control-label col-sm-6" ReadOnly="true" ID="txt_cgstamt" Text="0.00"></asp:Label><br />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lbl_sgst9" runat="server" class="control-label col-sm-6">SGST  Amount :<span class="spncls"></span></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label runat="server" class="control-label col-sm-6" ReadOnly="true" ID="txt_sgstamt" Text="0.00"></asp:Label><br />
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <asp:Label ID="lbligst" runat="server" class="control-label col-sm-6">IGST  Amount :<span class="spncls"></span></asp:Label>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <asp:Label runat="server" class="control-label col-sm-6" ReadOnly="true" ID="txt_igstamt" Text="0.00"></asp:Label><br />
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="lbl_grandTotal" runat="server" class="control-label col-sm-6">Grand Total :<span class="spncls"></span></asp:Label>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Label runat="server" class="control-label col-sm-6" ReadOnly="true" ID="txt_grandTotal" Text="0.00"></asp:Label><br />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--  last total show--%>






                            <br />
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-6 col-md-2">
                                    <asp:Button ID="btnsave" OnClick="btnsave_Click" ValidationGroup="1" CssClass="form-control btn btn-outline-primary m-2" runat="server" Text="Save" />
                                </div>
                                <div class="col-6 col-md-2">
                                    <asp:Button ID="btncancel" OnClick="btncancel_Click" CssClass="form-control btn btn-outline-danger m-2" runat="server" Text="Cancel" />
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                            <div>
                                <br />
                                <br />
                                <br />
                            </div>

                        </div>
                    </div>
                    <asp:HiddenField ID="hhd" runat="server" />
                    <asp:HiddenField ID="hhdstate" runat="server" />
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave" />
                <asp:PostBackTrigger ControlID="btncancel" />
                <asp:PostBackTrigger ControlID="uploadfile" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>
