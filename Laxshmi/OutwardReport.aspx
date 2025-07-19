<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LaxmiMaster.master" CodeFile="OutwardReport.aspx.cs" Inherits="Laxshmi_OutwardReport" %>

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
                        <div class="col-9 col-md-10">
                            <h4 class="mt-4 ">OUTWARD REPORT</h4>
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
                                <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Outward Number :"></asp:Label>
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
                                <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="DeliveryNote No.:"></asp:Label>
                                <asp:TextBox ID="txtDeliveryNoteno" CssClass="form-control" placeholder="Search DeliveryNote Number " runat="server" OnTextChanged="txtDeliveryNoteno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetDeliveryNotenoList"
                                    TargetControlID="txtDeliveryNoteno">
                                </asp:AutoCompleteExtender>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-top: 14px;">
                                <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Reference No. :"></asp:Label>
                                <asp:TextBox ID="txtReferenceNo" CssClass="form-control" placeholder="Search Reference No. " runat="server" OnTextChanged="txtReferenceNo_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetReferenceNoList"
                                    TargetControlID="txtReferenceNo">
                                </asp:AutoCompleteExtender>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-top: 14px;">
                                <asp:Label ID="lblfromdate" runat="server" Font-Bold="true" Text="From Date(Delivery Date) :"></asp:Label>
                                <asp:TextBox ID="txtfromdate" CssClass="form-control" runat="server" TextMode="Date" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-top: 14px;">
                                <asp:Label ID="lbltodate" runat="server" Font-Bold="true" Text="To Date(Delivery Date) :"></asp:Label>
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

                </div>
                <div class="container-fluid">

                    <div class="col-md-12" style="padding: 0px; margin-top: 5px;">
                        <div id="DivRoot1" align="left" runat="server">
                            <div style="overflow: hidden;" id="DivHeaderRow1">
                            </div>
                            <div style="overflow: scroll;" class="dt-responsive table-responsive" onscroll="OnScrollDiv(this)" id="DivMainContent1">
                                <asp:GridView ID="GVfollowup" runat="server" DataKeyNames="ID" CellPadding="4" Font-Names="Verdana" ShowFooter="true"
                                    Font-Size="10pt" Width="100%"
                                    GridLines="Both" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false"
                                    OnRowDataBound="GVfollowup_RowDataBound" OnRowCommand="GVfollowup_RowCommand" OnRowEditing="GVfollowup_RowEditing">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btn_edit" CausesValidation="false" Text="Edit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("OutwardNo") %>'><i class='fas fa-edit' style='font-size:24px;color: blue;'></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSrNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Outward No" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInwardNo" runat="server" Text='<%#Eval("OutwardNo")%>'></asp:Label>
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

                                        <asp:TemplateField HeaderText="Outward Qty" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOutwardQty" runat="server" Text='<%#Eval("OutwardQty")%>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="DeliveryNote No." HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryNoteno" runat="server" Text='<%#Eval("DeliveryNoteno")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblGradTotal" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DeliveryNote date" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeliveryNotedate" runat="server" Text='<%#Eval("DeliveryNotedate")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblGradTotal" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference No." HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldReferenceNo" runat="server" Text='<%#Eval("ReferenceNo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblGrandTtal" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reference Date" HeaderStyle-CssClass="gvhead" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReferenceDate" runat="server" Text='<%#Eval("ReferenceDate")%>'></asp:Label>
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
                                    </Columns>
                                    <FooterStyle Font-Bold="True" ForeColor="Yellow" HorizontalAlign="Center" />

                                </asp:GridView>
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
                                    <h4 class="modal-title">Edit Outward Entry
                                       <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                </div>

                                <br />
                                <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                    <div class="row">
                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Outward No:</asp:Label>
                                            <asp:TextBox ID="txtInwardnopop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Customer Name:</asp:Label>
                                            <asp:TextBox ID="txtcustomernamepop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label">Row Material:</asp:Label>
                                            <asp:TextBox ID="txtrowmaterialpop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                            <asp:TextBox ID="txtinwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                            <asp:TextBox ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Outward Quantity"
                                                ControlToValidate="txtoutwardqty" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                            <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Enter Delivery Note No." runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ErrorMessage="Please Enter Delivery Note No."
                                                ControlToValidate="TextBox1" ValidationGroup="1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                            <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>
                                            <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </div>
                                        <hr />
                                        <div class="col-md-12">
                                            <center style="margin-top: 18px">
                                                <asp:LinkButton runat="server" ID="btnSendtopro" ValidationGroup="1" class="btn btn-success" OnClick="btnsave_Click">
                                                         <span class="btn-label">
                                                             <i class="fa fa-check"></i>
                                                         </span>
                                                        Update 
                                                </asp:LinkButton>
                                            </center>
                                        </div>

                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                </asp:Panel>

                <br />
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="false"></rsweb:ReportViewer>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnDownload" />
                <asp:PostBackTrigger ControlID="btnPDF" />
                <asp:PostBackTrigger ControlID="GVfollowup" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</asp:Content>
