<%@ Page Title="" Language="C#" MasterPageFile="~/LaxmiMaster.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="Laxshmi_CompanyMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/css/Griddiv.css" rel="stylesheet" />
    <style>
        .LblStyle {
            font-weight: 500;
            color: black;
        }

        .spncls {
            color: red;
        }

        .card_adj {
            margin-bottom: 3px;
            height: 35px;
        }
    </style>
    <style>
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

    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="container-fluid px-3">
                    <br />
                    <br />
                    <br />
                    <br />
                    <div class="row">
                        <div class="col-md-10">
                            <h4 class="mt-4">&nbsp <b>CUSTOMER MASTER</b></h4>
                        </div>
                        <div class="col-md-2 mt-4">
                            <asp:LinkButton ID="Button1" CssClass="form-control btn btn-warning" Font-Bold="true" CausesValidation="false" runat="server" OnClick="Button1_Click">
    <i class="fas fa-file-alt"></i>&nbsp List
                            </asp:LinkButton>
                        </div>
                    </div>
                    <hr />
                    <div class="card mb-4">
                        <div class="card-body ">
                            <div class="row">
                                <div class="col-md-12 mb-3">
                                    <b>PRIMARY DETAILS<span class="spncls"> (MANDATORY FIELD)</span></b>
                                    <hr style="width: 25%" />
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblcompanyname" class="form-label LblStyle"><span class="spncls">*</span>Customer Name : </label>

                                    <asp:TextBox ID="txtcompanyname" OnTextChanged="txtcompanyname_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                        ControlToValidate="txtcompanyname" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCompanyList"
                                        TargetControlID="txtcompanyname" Enabled="true">
                                    </asp:AutoCompleteExtender>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblcompanycode" class="form-label LblStyle">Customer Code :</label>
                                    <asp:TextBox ID="txtcompanycode" CssClass="form-control" ReadOnly="true" ForeColor="red" placeholder="Enter Company Code" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblArea" class="form-label LblStyle"><span class="spncls">*</span>Type of Supply For : </label>
                                    <asp:DropDownList ID="ddlTypeofSupply" OnTextChanged="ddlTypeofSupply_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="-- Select Type of Supply --" Text="-- Select Type of Supply --"></asp:ListItem>
                                        <asp:ListItem Selected="True" Value="B2B" Text="B2B(INDIA)"></asp:ListItem>
                                        <asp:ListItem Value="SEZWOP" Text="SEZWOP"></asp:ListItem>
                                        <asp:ListItem Value="EXPWOP" Text="EXPWOP"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblVendorcode" class="form-label LblStyle">Vendor Code :</label>
                                    <asp:TextBox ID="txtvendorcode" CssClass="form-control" placeholder="Enter Vendor Code" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 col-12 mb-3" id="contry" runat="server" visible="false">
                                    <label for="lblCountry" class="form-label LblStyle"><span class="spncls">*</span>Country Name :</label>
                                    <asp:DropDownList ID="ddlCountryCode" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblPrimaryemailid" class="form-label LblStyle"><span class="spncls">*</span>Primary Email ID : </label>
                                    <asp:TextBox ID="txtPrimaryEmail" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPrimaryEmail_TextChanged" TextMode="Email" placeholder="Enter Primary Email ID" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Primary Email ID" ControlToValidate="txtPrimaryEmail" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-6 col-12 mb-3" id="mail2" runat="server" visible="false">
                                    <label for="lblSecondaryemailid" class="form-label LblStyle">Secondary Email ID : </label>
                                    <asp:TextBox ID="txtSecondaryemailid" CssClass="form-control" TextMode="Email" placeholder="Enter Secondary Email ID" runat="server"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter Secondary Email ID" ControlToValidate="txtSecondaryemailid" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>

                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblgstno" class="form-label LblStyle"><span class="spncls">*</span>GST No. :</label>
                                    <asp:TextBox ID="txtgstno" CssClass="form-control" placeholder="Enter GST No." runat="server" AutoPostBack="true" OnTextChanged="txtgstno_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1" ErrorMessage="Please Enter GST No." ControlToValidate="txtgstno" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revGSTNumber" runat="server"
                                        ControlToValidate="txtgstno"
                                        ValidationExpression="^\d{2}[A-Z]{5}\d{4}[A-Z]{1}\d[Z]{1}[A-Z\d]{1}$"
                                        ErrorMessage="Invalid GST Number. GST number should be in the format 27ATFPS1959J1Z4"
                                        Display="Dynamic"
                                        ForeColor="Red" />
                                </div>

                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblBillingAddress" class="form-label LblStyle">Billing Address: </label>
                                    <asp:TextBox ID="txtBillingAddress" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Billing Address" runat="server"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter Billing Address" ControlToValidate="txtBillingAddress" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <asp:CheckBox ID="check_addresss" OnCheckedChanged="check_addresss_CheckedChanged" AutoPostBack="true" runat="server" />
                                    <asp:Label ID="Label3" runat="server" Text=""><b></b></asp:Label>
                                    <label for="lblshippingaddress" class="form-label LblStyle">Shipping Address :</label>
                                    <asp:TextBox ID="txtshippingaddress" CssClass="form-control" placeholder="Enter Shipping Address" TextMode="MultiLine" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblbillinglocation" class="form-label LblStyle">Billing Location : </label>
                                    <asp:TextBox ID="txtbillinglocation" CssClass="form-control" placeholder="Enter Billing Location" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblshippinglocation" class="form-label LblStyle">Shipping Location : </label>
                                    <asp:TextBox ID="txtshippinglocation" CssClass="form-control" placeholder="Enter Shipping Location" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblBPincode" class="form-label LblStyle">Billing PinCode. :</label>
                                    <asp:TextBox ID="txtBPincode" CssClass="form-control" placeholder="Enter Pincode" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPincode" runat="server" ControlToValidate="txtBPincode" ErrorMessage="Invalid Pincode" ForeColor="Red" ValidationExpression="^\d{6}$"></asp:RegularExpressionValidator>

                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblSPincode" class="form-label LblStyle">Shipping PinCode. :</label>
                                    <asp:TextBox ID="txtSPincode" CssClass="form-control" placeholder="Enter Pincode" onkeypress="return isNumberKey(event)" MaxLength="6" runat="server"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSPincode" ErrorMessage="Invalid Pincode" ForeColor="Red" ValidationExpression="^\d{6}$"></asp:RegularExpressionValidator>

                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblBState" class="form-label LblStyle"><span class="spncls"></span>Billing State. : </label>
                                    <asp:DropDownList ID="ddlBStateCode" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblState" class="form-label LblStyle"><span class="spncls"></span>Shipping State. : </label>
                                    <asp:DropDownList ID="ddlSStatecode" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-4 col-12 mb-3">
                                    <label for="lblCompanyPan" class="form-label LblStyle">Company Pan No. :</label>
                                    <asp:TextBox ID="txtCompanyPan" CssClass="form-control" placeholder="Enter Company Pan No." MaxLength="12" MinLength="10" runat="server"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter  Company Pan No." ControlToValidate="txtCompanyPan" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4 mb-3">
                                    <label for="lblPaymentTerm" class="form-label LblStyle">Payment Term(In days) :</label>
                                    <asp:TextBox ID="txtPaymentTerm" CssClass="form-control" placeholder="Enter Payment Term" onkeypress="return isNumberKey(event)" MaxLength="3" MinLength="1" runat="server"></asp:TextBox>

                                </div>
                                <div class="col-md-4 mb-3">
                                    <label for="lblCompanyPan" class="form-label LblStyle"></label>
                                    <asp:Button ID="btnadd" runat="server" Style="background-color: green; color: white" Width="200px" CausesValidation="false" CssClass="form-control btn1" Text="Save Shipping Address" OnClick="btnadd_Click" />
                                </div>

                                <hr />
                                <div class="col-md-12 mb-3">
                                    <b>SECONDORY DETAILS<span class="spncls"> (NOT-MANDATORY FIELD)</span></b>
                                    <hr style="width: 25%" />
                                </div>

                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblUDYAM" class="form-label LblStyle">UDYAM No. :</label>
                                    <asp:TextBox ID="txtUDYAM" CssClass="form-control" placeholder="Enter UDYAM No." runat="server"></asp:TextBox>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblCINNO" class="form-label LblStyle">CIN NO. : </label>
                                    <asp:TextBox ID="txtCINNO" CssClass="form-control" placeholder="CIN NO." runat="server"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Enter CIN NO." ControlToValidate="txtCINNO" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblClienttype" class="form-label LblStyle">Client type. : </label>
                                    <%-- <asp:TextBox ID="txtClienttype" CssClass="form-control" placeholder="Client type" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlClientType" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="Paid" Text="Paid" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="UnPaid" Text="UnPaid"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter Client type" ControlToValidate="txtClienttype" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblWebsiteLink" class="form-label LblStyle">Website Link :</label>
                                    <asp:TextBox ID="txtWebsiteLink" CssClass="form-control" placeholder="Enter Website Link" runat="server"></asp:TextBox>
                                    <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Please Enter  Website Link" ControlToValidate="txtWebsiteLink" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <asp:HiddenField ID="HFfile1" runat="server" />
                                    <label for="lblCompanyPan" class="form-label LblStyle">Visiting Card :</label>
                                    <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" />
                                    <asp:Label ID="lblPath1" runat="server" Text="" ForeColor="Red"></asp:Label>

                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Please Enter  Company Pan No." ControlToValidate="txtCompanyPan" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-6 col-12 mb-3">
                                    <label for="lblCreditLimit" class="form-label LblStyle">Credit Limit: </label>
                                    <asp:TextBox ID="TxtCreditLimit" CssClass="form-control" placeholder="Enter Credit Limit" runat="server"></asp:TextBox>
                                </div>


                            </div>

                            <%-- -----------------------------------------------------------------------------------------------------%>
                            <div class="row">
                                <div class="table-responsive">
                                    <asp:GridView ID="GVShippingAddress" CssClass="display table table-striped table-hover" AutoGenerateColumns="false" runat="server" CellPadding="4" ForeColor="#333333" PageSize="25" AllowPaging="true" Width="100%" OnRowEditing="GVShippingAddress_RowEditing">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SR.NO" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead sno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ADDRESS" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblshippingaddress" runat="server" Text='<%# Eval("ShippingAddress") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtshipingaddress" CssClass="form-control" TextMode="MultiLine" runat="server" Text='<%# Eval("ShippingAddress") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Location" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSLocation" runat="server" Text='<%# Eval("ShipLocation")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSLocation" CssClass="form-control" runat="server" Text='<%# Eval("ShipLocation")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PinCode" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSPincode" runat="server" Text='<%# Eval("ShipPincode")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSPincode" CssClass="form-control" runat="server" Text='<%# Eval("ShipPincode")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StateCode" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSStateCode" runat="server" Text='<%# Eval("ShipStatecode")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSStateCode" CssClass="form-control" runat="server" Text='<%# Eval("ShipStatecode")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="ImgbtnEdit" CausesValidation="false" Text="Edit" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Edit"></asp:LinkButton>&nbsp;                                                 
                                                        <asp:LinkButton runat="server" ID="ImgbtnDelete" ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" OnClick="lnkbtnDelete_Click" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="Btn_Update" Text="Update" CausesValidation="false" OnClick="Btn_Update_Click" CssClass="btn btn-primary btn-sm" runat="server"></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="Btn_Cancel" CausesValidation="false" OnClick="Btn_Cancel_Click" Text="Cancel" CssClass="btn btn-primary btn-sm " runat="server"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <%--------------------------------------------------------------------------------------------------------------------%>
                            <br />

                            <%--Grid View Start--%>
                            <div class="card-header gvhead">
                                <h5 style="color: black"><b>Contact Details</b></h5>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="display table table-striped table-hover" border="1" style="width: 100%; border: 1px solid #004068;" >
                                            <tr class="gvhead">
                                                <td>Name</td>
                                                <td>Mobile No.</td>
                                                <td>Email ID</td>
                                                <td>Department</td>
                                                <td>Designation</td>
                                                <td>Action</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtmobile" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtemaili" TextMode="Email" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtDepartment" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdesignation" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddMore" CausesValidation="false" OnClick="btnAddMore_Click" CssClass="btn btn-primary btn-sm btncss" runat="server" Text="Save" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <%--<div class="row" id="divdtls">--%>
                                    <div class="table text-center">
                                        <asp:GridView ID="dgvContactDetails" runat="server" CellPadding="4" DataKeyNames="id" PageSize="10" AllowPaging="true" Width="100%" CssClass="display table table-striped table-hover"
                                            OnRowEditing="dgvContactDetails_RowEditing" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        <asp:Label ID="lblid" runat="Server" Text='<%# Eval("id") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Text='<%# Eval("Name") %>' CssClass="form-control" ID="txtname" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblname" runat="Server" Text='<%# Eval("Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Number" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Text='<%# Eval("Number") %>' CssClass="form-control" ID="txtmobile" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnumber" runat="Server" Text='<%# Eval("Number") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email ID" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Text='<%# Eval("EmailID") %>' CssClass="form-control" ID="txtemaili" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemailid" runat="Server" Text='<%# Eval("EmailID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Department" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Text='<%# Eval("Department") %>' CssClass="form-control" ID="txtDepartment" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartment" runat="Server" Text='<%# Eval("Department") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Text='<%# Eval("Designation") %>' CssClass="form-control" ID="txtdesignation" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesignation" runat="Server" Text='<%# Eval("Designation") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btn_edit" CausesValidation="false" Text="Edit" runat="server" CssClass="btn btn-primary btn-sm" CommandName="Edit"></asp:LinkButton>&nbsp;                                                 
                                                        <asp:LinkButton runat="server" ID="lnkbtnDelete" ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" OnClick="lnkbtnDelete_Click" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="gv_update" CausesValidation="false" Text="Update" OnClick="gv_update_Click" CssClass="btn btn-primary btn-sm" runat="server"></asp:LinkButton>&nbsp;
                                                        <asp:LinkButton ID="gv_cancel" CausesValidation="false" OnClick="gv_cancel_Click" Text="Cancel" CssClass="btn btn-primary btn-sm " runat="server"></asp:LinkButton>
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
                        <br />
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-6 col-md-2">
                                <asp:Button ID="btnsave" OnClick="btnsave_Click" ValidationGroup="1" CssClass="form-control btn btn-outline-primary m-2" runat="server" Text="Save" />
                            </div>
                            <div class="col-6 col-md-2">
                                <asp:Button ID="btncancel" OnClick="btncancel_Click" CausesValidation="false" CssClass="form-control btn btn-outline-danger m-2" runat="server" Text="Cancel" />
                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hhd" runat="server" />

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsave" />
                <asp:PostBackTrigger ControlID="btncancel" />
                <asp:AsyncPostBackTrigger ControlID="btnAddMore" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>

