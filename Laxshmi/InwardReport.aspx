<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LaxmiMaster.master" CodeFile="InwardReport.aspx.cs" Inherits="Laxshmi_InwardReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/css/Griddiv.css" rel="stylesheet" />
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
    <style>
        .gvhead {
            text-align: center;
            color: #ffffff;
            background-color: #212529;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 13px;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                /*.pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }*/

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
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
                    <div id="divtabl" runat="server">
                        <div class="row">
                            <div class="col-9 col-md-10">
                                <h4 class="mt-4 "><b>INWARD LIST</b></h4>
                            </div>
                            <div class="col-3 col-md-2 mt-4">
                                <asp:Button ID="btnCreate" CssClass="form-control btn btn-warning" OnClick="btnCreate_Click" runat="server" Text="Inward Entry" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="lblcompanyname" Font-Bold="true" runat="server" Text="Company Name :"></asp:Label>
                                    <asp:TextBox ID="txtCustomerName" CssClass="form-control" placeholder="Search Customer" runat="server" OnTextChanged="txtCustomerName_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Customer Name"
                                        ControlToValidate="txtCustomerName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                        TargetControlID="txtCustomerName">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Row Material :"></asp:Label>
                                    <asp:TextBox ID="txtRowMaterial" CssClass="form-control" placeholder="Search Row Material " runat="server" OnTextChanged="txtRowMaterial_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRowMaterialList"
                                        TargetControlID="txtRowMaterial">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Inward Number :"></asp:Label>
                                    <asp:TextBox ID="txtInwardno" CssClass="form-control" placeholder="Search Inward Number " runat="server" OnTextChanged="txtInwardno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetInwardnoList"
                                        TargetControlID="txtInwardno">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Delivery No.:"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryNo" CssClass="form-control" placeholder="Search Delivery Number " runat="server" OnTextChanged="txtDeliveryNo_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetDeliveryNoList"
                                        TargetControlID="txtDeliveryNo">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="HSN :"></asp:Label>
                                    <asp:TextBox ID="txtHSN" CssClass="form-control" placeholder="Search HSN " runat="server" OnTextChanged="txtHSN_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetHSNList"
                                        TargetControlID="txtHSN">
                                    </asp:AutoCompleteExtender>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="lblfromdate" runat="server" Font-Bold="true" Text="From Date :"></asp:Label>
                                    <asp:TextBox ID="txtfromdate" CssClass="form-control" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div style="margin-top: 14px;">
                                    <asp:Label ID="lbltodate" runat="server" Font-Bold="true" Text="To Date :"></asp:Label>
                                    <asp:TextBox ID="txttodate" CssClass="form-control" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-xl-4 col-md-4" style="text-align: left">
                                <br />
                                <asp:Button ID="btnDownload" OnClick="btnDownload_Click" CssClass="btn btn-primary" runat="server" Text="Excel" Style="padding: 8px;" />
                                <asp:Button ID="btnPDF" OnClick="btnPDF_Click" CssClass="btn btn-primary" runat="server" Text="PDF" Style="padding: 8px;" />
                            </div>
                            <div class="col-xl-4 col-md-4" style="text-align: center">
                                <br />
                                <asp:Button ID="btnSearchData" CssClass="btn btn-success" OnClick="btnSearchData_Click" runat="server" Text="Search" Style="padding: 8px;" />
                                <asp:Button ID="btnresetfilter" OnClick="btnresetfilter_Click" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" />
                            </div>

                            <div class="col-xl-4 col-md-4" style="text-align: center"></div>
                        </div>

                        <div class="col-md-12" style="padding: 0px; margin-top: 5px;">
                            <div id="DivRoot1" align="left" runat="server">
                                <div style="overflow: hidden;" id="DivHeaderRow1">
                                </div>
                                <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent1">
                                    <asp:GridView ID="GVfollowup" runat="server" CellPadding="4" Font-Names="Verdana" ShowFooter="true"
                                        Font-Size="10pt" Width="100%"
                                        GridLines="Both" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false" OnRowCommand="GVfollowup_RowCommand"
                                        OnRowDataBound="GVfollowup_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inward No" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardNo" runat="server" Text='<%#Eval("InwardNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Row Material" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowMaterial" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardQty" runat="server" Text='<%#Eval("InwardQty")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalOutwardQty" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWeight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGradTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery No." HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryNo" runat="server" Text='<%#Eval("DeliveryNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGradTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryDate" runat="server" Text='<%#Eval("DeliveryDate")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGrandTtal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WO Date" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWODate" runat="server" Text='<%#Eval("WODate")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGradTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VehicleNo" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleNo" runat="server" Text='<%#Eval("VehicleNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGradTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HSN" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHSN" runat="server" Text='<%#Eval("HSN")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblGradTotal" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" Height="27px" ToolTip="Edit Inward record" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%# Container.DataItemIndex %>'><i class='fas fa-edit' style='font-size:24px;color: blue;'></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-trash' style='font-size:24px;color: red;'></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle Font-Bold="True" ForeColor="Yellow" HorizontalAlign="Center" />

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="divinwardform" runat="server" visible="false">
                        <div class="row">
                            <div class="col-9 col-md-10">
                                <h4 class="mt-4 "><b>Inward Entry</b></h4>
                            </div>
                            <div class="col-3 col-md-2 mt-4">
                                <asp:Button ID="Button1" CssClass="form-control btn btn-warning" CausesValidation="false" autopostback="true" OnClick="Button1_Click" runat="server" Text="List" />
                            </div>
                            <br />
                        </div>
                        <hr />
                        <div class="row container">

                            <div class="col-md-12">
                                <div class="profilemodel2">

                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:HiddenField runat="server" ID="hdnInwardNo" />
                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Customer Name:</asp:Label>
                                                <asp:TextBox ID="txtcustomer" CssClass="form-control" placeholder="Search Customer Name" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="Please Enter Customer Name"
                                                    ControlToValidate="txtcustomer" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                                    TargetControlID="txtcustomer">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Material:</asp:Label>
                                                <asp:TextBox ID="txtrowmetarial" CssClass="form-control" placeholder="Search Material" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Material"
                                                    ControlToValidate="txtrowmetarial" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRowMaterialList"
                                                    TargetControlID="txtrowmetarial">
                                                </asp:AutoCompleteExtender>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery No. :</asp:Label>
                                                <asp:TextBox ID="txtDeliveryNo1" CssClass="form-control" placeholder="Enter Delivery No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery No."
                                                    ControlToValidate="txtDeliveryNo" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Delivery Date:</asp:Label>
                                                <asp:TextBox ID="txtDeliveryDate" CssClass="form-control" placeholder="Enter Delivery Date" TextMode="Date" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Date"
                                                    ControlToValidate="txtDeliveryDate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label23" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>WO Date:</asp:Label>
                                                <asp:TextBox ID="txtWODate" CssClass="form-control" placeholder="Enter WO Date" TextMode="Date" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter Wo Date"
                                                    ControlToValidate="txtWODate" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Inward QTY:</asp:Label>
                                                <asp:TextBox ID="txtinwardqantity" CssClass="form-control" placeholder="Enter Inward Qantity" AutoPostBack="true" OnTextChanged="txtWeight_TextChanged" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Inward Qantity"
                                                    ControlToValidate="txtinwardqantity" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label21" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight :</asp:Label>
                                                <asp:TextBox ID="txtWeight" CssClass="form-control" placeholder="Enter Weight" AutoPostBack="true" OnTextChanged="txtWeight_TextChanged" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="Please Enter Weight"
                                                    ControlToValidate="txtWeight" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label24" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Vehicle  No. :</asp:Label>
                                                <asp:TextBox ID="txtVehicleno" CssClass="form-control" placeholder="Enter Vehicle  No." runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="Please Enter Vehicle  No."
                                                    ControlToValidate="txtVehicleno" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label25" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>HSN :</asp:Label>
                                                <asp:TextBox ID="txtHSN1" CssClass="form-control" placeholder="Enter HSN" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ErrorMessage="Please Enter HSN"
                                                    ControlToValidate="txtHSN" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label22" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Total Weight :</asp:Label>
                                                <asp:TextBox ID="txtTotalWeight" CssClass="form-control" ReadOnly="true" placeholder="Enter Total Weight" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>
                                                <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="col-md-4"></div>
                                                    <div class="col-md-3" style="margin-top: 18px">
                                                        <asp:LinkButton runat="server" ID="btnsavedata" ValidationGroup="form1" class="btn btn-success" OnClick="btnsavedata_Click">
                                <span class="btn-label">
                                    <i class="fa fa-check"></i>
                                </span>
                               Save
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="false"></rsweb:ReportViewer>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDownload" />
                <asp:PostBackTrigger ControlID="btnPDF" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>
