<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LaxmiMaster.master" CodeFile="LaxshmiDashboard.aspx.cs" Inherits="Laxshmi_LaxshmiDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
    <script>
        jQuery(document).ready(function () {
            SweetAlert2Demo.init();
        });
        var SweetAlert2Demo = (function () {
            //== Demos
            var initDemos = function () {
                $("#alert_demo_3_3").click(function (e) {
                    swal("Good job!", "You clicked the button!", {
                        icon: "success",
                        buttons: {
                            confirm: {
                                className: "btn btn-success",
                            },
                        },
                    });
                });
            }
        });
    </script>--%>
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
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form runat="server" style="margin-top: 8%;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row">
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div
                                    class="icon-big text-center icon-success bubble-shadow-small">
                                    <i class="fas fa-luggage-cart"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Customers</p>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblcustomers" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div
                                    class="icon-big text-center icon-primary bubble-shadow-small">
                                    <i class="fas fa-cubes"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Material</p>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblMaterial" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div
                                    class="icon-big text-center icon-warning bubble-shadow-small">
                                    <i class="fas fa-arrow-alt-circle-down"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Inward Quantity</p>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblInwardQuantity" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 col-md-3">
                <div class="card card-stats card-round">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-icon">
                                <div
                                    class="icon-big text-center icon-success bubble-shadow-small">
                                    <i class="fas fa-arrow-alt-circle-up"></i>
                                </div>
                            </div>
                            <div class="col col-stats ms-3 ms-sm-0">
                                <div class="numbers">
                                    <p class="card-category">Ouward Quantity</p>
                                    <h4 class="card-title">
                                        <asp:Label ID="lblOuwardQuantity" runat="server"></asp:Label></h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row form-control">
            <div class="row">
                <div class="col-12 col-md-2 mb-3">
                    <asp:DropDownList ID="DropDownList1" Font-Bold="true" CssClass="form-select form-control-lg" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-12 col-md-2 mb-3" id="MonthDp1" runat="server">
                    <asp:DropDownList ID="DropDownList2" Font-Bold="true" CssClass="form-select form-control-lg" runat="server" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <div class="col-12 col-md-6 mb-3" id="CustName" runat="server">
                    <div class="row">
                        <div class="col-12 col-md-4" style="margin-top: 7px;">
                            <asp:Label ID="lblCustName" runat="server" CssClass="form-control-lg"><b>Customer Name: </b></asp:Label>
                        </div>
                        <div class="col-12 col-md-8">
                            <asp:TextBox ID="txtCustName" runat="server" AutoPostBack="true" CssClass="form-control"
                                Width="100%" Style="border: 1px solid black" OnTextChanged="ddlMonth_SelectedIndexChanged">
                            </asp:TextBox>
                            <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted"
                                CompletionListItemCssClass="listItem"
                                CompletionInterval="10" MinimumPrefixLength="1"
                                ServiceMethod="GetCompanyList"
                                TargetControlID="txtCustName"
                                Enabled="true">
                            </ajaxToolkit:AutoCompleteExtender>
                        </div>
                    </div>
                </div>

                <div class="col-md-12 col-md-2 mb-3">
                    <asp:Button ID="btnDownload" OnClick="btnDownload_Click" CssClass="btn btn-primary" runat="server" Text="Excel" Style="padding: 8px;" />
                </div>
            </div>
            <%--       <asp:Button ID="alert_demo_3_3" runat="server" text="save" />--%>
        </div>
        <div class="col-md-2" id="MonthDp" runat="server">
            <asp:DropDownList ID="ddlMonth" Width="120px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Font-Bold="true" CssClass="form-select form-control-lg">
                <asp:ListItem Text="Months" Value="00"></asp:ListItem>
                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="row">
            <div class="col-md-12">
                <div class="card card-stats card-round form-control">
                    <div class="card-body">
                        <asp:GridView ID="GVCustomerList" runat="server" CellPadding="4" Width="100%" OnRowCommand="GVCustomerList_RowCommand"
                            CssClass="table table-head-bg-primary mt-4" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RowMaterial" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="RowMaterial" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="DeliveryDate" runat="server" Text='<%#Eval("DeliveryDate")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="InwardQty" runat="server" Text='<%#Eval("TotalInwardQty")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Outward Qty" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOutward" runat="server" ToolTip="Outward List" CausesValidation="false" CommandName="RowOutwardList" Text='<%#Eval("TotalOutwardQty")%>' CommandArgument='<%#Eval("CompanyName")+","+Eval("RowMaterial")%>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Balance Qty" HeaderStyle-CssClass="gvhead">
                                    <ItemTemplate>
                                        <asp:Label ID="BalanceQty" runat="server" Text='<%#Eval("BalanceQuantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
        <br />
        <rsweb:reportviewer id="ReportViewer1" runat="server" visible="false"></rsweb:reportviewer>
    </form>

</asp:Content>

