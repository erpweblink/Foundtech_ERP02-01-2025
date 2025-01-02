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

                </div>
                <div class="container-fluid">

                    <div class="col-md-12" style="padding: 0px; margin-top: 5px;">
                        <div id="DivRoot1" align="left" runat="server">
                            <div style="overflow: hidden;" id="DivHeaderRow1">
                            </div>
                            <div style="overflow-x: auto; max-height: 400px; overflow-y: auto; border: 1px solid #ccc;">
                                <asp:GridView ID="GVfollowup" runat="server" CellPadding="4" Font-Names="Verdana" ShowFooter="true"
                                    Font-Size="10pt" Width="100%"
                                    GridLines="Both" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false"
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
