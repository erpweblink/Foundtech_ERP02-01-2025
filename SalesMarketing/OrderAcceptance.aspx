<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="OrderAcceptance.aspx.cs" Inherits="SalesMarketing_OrderAcceptance" %>


<%-- New Update page  --%>

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
                                        <asp:TextBox ID="txtUserName" runat="server" ReadOnly="true" ValidationGroup="1" ForeColor="Red" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--<asp:DropDownList runat="server" ID="ddlUser" ValidationGroup="1" CssClass="form-control">
                                        </asp:DropDownList>--%>
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
                                            ForeColor="Red" ErrorMessage="Please Enter OA No" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

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
                                        <asp:Label ID="Label40" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Project Code:</asp:Label>
                                        <asp:TextBox ID="txtprojectCode" CssClass="form-control" ForeColor="Red" runat="server" OnTextChanged="txtprojectCode_TextChanged" AutoPostBack="true" autocomplete="off"></asp:TextBox>
                                        <asp:Label ID="lblProjCodeValidate" runat="server" Text="Project Code Already Exist.." Visible="false" ForeColor="Red" Font-Size="Large" Font-Italic="true"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="1" runat="server" ControlToValidate="txtprojectCode"
                                            ForeColor="Red" ErrorMessage="Please Enter Project Code" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6 col-12 mb-3">
                                        <asp:Label ID="Label41" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Project Name:</asp:Label>
                                        <asp:TextBox ID="txtprojectName" CssClass="form-control" ForeColor="Red" runat="server" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="1" runat="server" ControlToValidate="txtprojectName"
                                            ForeColor="Red" ErrorMessage="Please Enter Project Name" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3 col-12 mb-3" id="txtProducbulk" runat="server">
                                        <asp:Label ID="Label18" runat="server" Font-Bold="true" CssClass="form-label">SubPart Bulk Upload :</asp:Label>
                                        <asp:FileUpload ID="AttachmentUpload" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblfile1" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>
                                    </div>

                                    <div class="col-md-2 col-12 mb-3" id="txtProducbulkbtn" runat="server">
                                        <asp:Button ID="uploadfile" runat="server" CausesValidation="false" AutoPostBack="true" Text="Upload" CssClass="form-control btn btn-outline-primary m-2" OnClick="uploadfile_Click" Style="padding: 4px 11px 4px 11px !important" />
                                    </div>
                                    <div class="col-md-1" id="dummyId" runat="server"></div>
                                    <div class="col-md-3 col-12 mb-3" >
                                        <asp:Label ID="lblPdf" runat="server" Font-Bold="true" CssClass="form-label">Upload Pdf :</asp:Label>
                                        <asp:FileUpload ID="PdfFile" runat="server" CssClass="form-control" />
                                        <asp:Label ID="lblPdfName" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>
                                    </div>

                                    <%--Grid View Start--%>
                                    <div class="card-header head" style="margin-top: 10px;" id="idheader" runat="server" visible="false">
                                        <h5 style="color: white">Product Details</h5>
                                    </div>
                                    <br />
                                  

                                    <%-- New code by Nikhil  --%>
                                    <div class="card">
                                        <div class="col-md-12" runat="server" visible="false" id="dvParticular">

                                            <div class="card-header bg-primary text-uppercase text-white">
                                                <h5>Products</h5>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <div class="table-responsive" id="tblAddNew" runat="server">
                                                        <table class="table-bordered" style="width: 100%; border: 2px solid #0c7d38;">
                                                            <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                                <td>Product (SubPart)</td>
                                                                <td>Req Qty</td>
                                                                <td>Description</td>
                                                                <td>Quantity</td>
                                                                <td>Length</td>
                                                                <td>Weight (kg)</td>
                                                                <td>Width</td>
                                                                <td>Thickness</td>
                                                                <td>Total Weight (Kg)</td>
                                                                <td>Action</td>
                                                            </tr>

                                                            <%--  Row 1--%>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtProduct" CssClass="form-control" ToolTip="Product Name" placeholder="Product" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtReqQty" CssClass="form-control" placeholder="Required Qty" ToolTip="Required Quantity" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtdescription" TextMode="MultiLine" placeholder="Description" ToolTip="Product Description" CssClass="form-control" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtquantity" CssClass="form-control" placeholder="Quantity" ToolTip="Product Quantity" OnTextChanged="txtquantity_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtlength" CssClass="form-control" placeholder="Lenght" ToolTip="Product Length" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWeight" CssClass="form-control" placeholder="Weight" ToolTip="Product Weight" OnTextChanged="txtquantity_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWidth" CssClass="form-control" placeholder="Width" ToolTip="Product Width" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtThickness" CssClass="form-control" placeholder="Thickness" ToolTip="Product Thickness" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalWeight" CssClass="form-control" placeholder="Total Weight" ToolTip="Product Total Weight" runat="server" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnAddMore" CausesValidation="false" OnClick="btnAddMore_Click" Style="padding: 4px 4px 4px 4px !important;" CssClass="btn btn-primary btn-sm btncss" runat="server" Text="Add More" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                    <%--<div class="row" id="divdtls">--%>
                                                    <div class="table-responsive text-center">
                                                        <asp:GridView ID="dgvMachineDetails" runat="server" CellPadding="4" DataKeyNames="id" Width="100%" CssClass="display table table-striped table-hover"
                                                            OnRowEditing="dgvMachineDetails_RowEditing" OnRowDataBound="dgvMachineDetails_RowDataBound" AutoGenerateColumns="false" OnRowCommand="dgvMachineDetails_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                                                    <ItemTemplate>
                                                                        <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                                                        <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                            <asp:GridView ID="gvDetails" DataKeyNames="Id" runat="server" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                                                <Columns>
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="SubProductName" HeaderText="Sub Product" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="SubDescription" HeaderText="Sub Product Description" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Quantity" HeaderText="Quantityn" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Length" HeaderText="Length" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Weight" HeaderText="Weight" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Width" HeaderText="Width" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="Thickness" HeaderText="Thickness" />
                                                                                    <asp:BoundField ItemStyle-Width="150px" DataField="TotalWeight" HeaderText="Total Weight" />
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                        <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Product" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Productname") %>' CssClass="form-control" Width="230px" ID="Product" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblproduct" runat="Server" Text='<%# Eval("Productname") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                  <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("RequestedQTY") %>' CssClass="form-control" Width="230px" ID="ReqQty" runat="server" ReadOnly="true"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblReqQty" runat="Server" Text='<%# Eval("RequestedQTY") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Description") %>' CssClass="form-control" ID="Description" Width="200px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescription" runat="Server" Text='<%# Eval("Description") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Quantity") %>' CssClass="form-control" ID="Quantity" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQuantity" runat="Server" Text='<%# Eval("Quantity") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Length" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Length") %>' CssClass="form-control" ID="Length" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLength" runat="Server" Text='<%# Eval("Length") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weight" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Weight") %>' CssClass="form-control" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" ID="Weight" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWeight" runat="Server" Text='<%# Eval("Weight") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Width" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Width") %>' CssClass="form-control" ID="Width" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWidth" runat="Server" Text='<%# Eval("Width") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Thickness" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("Thickness") %>' CssClass="form-control" ID="Thickness" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblThickness" runat="Server" Text='<%# Eval("Thickness") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total Weight" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox Text='<%# Eval("TotalWeight") %>' ReadOnly="true" CssClass="form-control" ID="TotalWeight" Width="100px" runat="server"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalWeight" runat="Server" Text='<%# Eval("TotalWeight") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                    <ItemTemplate>
                                                                        <%--<asp:LinkButton ID="btn_edit" runat="server" Height="27px" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-edit' style='font-size:24px;color: #212529;'></i></asp:LinkButton>--%>

                                                                        <asp:LinkButton ID="btn_edit" CausesValidation="false" Text="Edit" runat="server" CommandName="Edit"><i class='fas fa-edit' style='font-size:24px;color: #212529;'></i></asp:LinkButton>&nbsp;
                                                                 <asp:LinkButton ID="gv_AddSubProd" Text="Add" CausesValidation="false" CommandName="AddNew" runat="server" Visible="false" CommandArgument='<%# Eval("Id") + "," + Eval("ProductName") + "," + Eval("Description") %>'><i class='fa fa-plus-circle' style='font-size:24px;color: #212529;'></i></asp:LinkButton>&nbsp;
                                                                <asp:LinkButton runat="server" ID="lnkbtnDelete" OnClick="lnkbtnDelete_Click" CommandArgument='<%# Eval("id")%>' ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
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
                                    </div>

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
                                        <br />
                                        <br />
                                        <br />
                                </div>
                            </div>
                            <asp:HiddenField ID="hhd" runat="server" />
                            <asp:HiddenField ID="hhdstate" runat="server" />
                        </div>
                    </div>

                <%--New Code By Nikhil 10-01-2025--%>
                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />

                    <asp:Panel ID="PopupHistoryDetail" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-3"></div>
                            <div class="col-md-8">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">PRODUCTION PLAN
                
                          <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>

                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding - left: 1px;">
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6 d-none">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="true" CssClass="form-label">PONO :</asp:Label>
                                                <asp:TextBox ID="txtPonoProd" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label">Product Name:</asp:Label>
                                                <asp:TextBox ID="txtProductname" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div> 
                                            <div class="col-md-6">
                                                <asp:Label ID="Label144" runat="server" Font-Bold="true" CssClass="form-label">Discription:</asp:Label>
                                                <asp:TextBox ID="txtDiscr" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="lblSubPro" runat="server" Font-Bold="true" CssClass="form-label">SubPart Bulk Upload :</asp:Label>
                                                <asp:FileUpload ID="SubProdFile" runat="server" CssClass="form-control" />
                                                <asp:Label ID="lblsubProduct" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>

                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <div class="col-md-2" style="margin-top: 18px">
                                                    <asp:Button ID="SubProdBtn" runat="server" CausesValidation="false" AutoPostBack="true" Text="Upload" CssClass="form-control btn btn-outline-primary m-2" OnClick="SubProdBtn_Click" Style="padding: 4px 11px 4px 11px !important" />
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <table class="table-bordered" style="width: 100%; border: 2px solid #0c7d38;">
                                            <tr style="background-color: #7ad2d4; color: #000; font-weight: 600; text-align: center;">
                                                <td>Product (SubPart)</td>
                                                <td>Description</td>
                                                <td>Quantity</td>
                                                <td>Length</td>
                                                <td>Weight (kg)</td>
                                                <td>Width</td>
                                                <td>Thickness</td>
                                                <td>Total Weight (Kg)</td>
                                                <td>Action</td>
                                            </tr>

                                            <%--  Row 1--%>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Enter Product" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox2" TextMode="MultiLine" placeholder="Description" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox3" CssClass="form-control" placeholder="Quantity" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox4" CssClass="form-control" placeholder="Lenght" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TextBox5" CssClass="form-control" placeholder="Weight" runat="server"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    <asp:TextBox ID="TextBox6" CssClass="form-control" placeholder="Width" runat="server"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    <asp:TextBox ID="TextBox7" CssClass="form-control" placeholder="Thickness" runat="server"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="TextBox8" CssClass="form-control" placeholder="Total Weight" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button1" CausesValidation="false" OnClick="Button1_Click1" Style="padding: 4px 4px 4px 4px !important;" CssClass="btn btn-primary btn-sm btncss" runat="server" Text="Add More" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1"></div>

                        </div>
                    </asp:Panel>

                    <%--End Code--%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave" />
                <asp:PostBackTrigger ControlID="btncancel" />
                <asp:PostBackTrigger ControlID="uploadfile" />
                <asp:PostBackTrigger ControlID="SubProdBtn" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>
