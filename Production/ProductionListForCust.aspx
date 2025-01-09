<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductionListForCust.aspx.cs" Inherits="Production_ProductionListForCust" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<html>
<head runat="server">
    <title>FoundTech Engineering</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="#" />
    <meta name="keywords" content="Admin" />
    <meta name="author" content="#" />

    <link rel="stylesheet" href="Content/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/assets/css/plugins.min.css" />
    <link rel="stylesheet" href="Content/assets/css/kaiadmin.min.css" />
    <script src="../JS/jquery.min.js"></script>
    <link href="../Content/css/Griddiv.css" rel="stylesheet" />

    <script src="../Content/assets/js/plugin/webfont/webfont.min.js"></script>
    <script>
        WebFont.load({
            google: { families: ["Public Sans:300,400,500,600,700"] },
            custom: {
                families: [
                    "Font Awesome 5 Solid",
                    "Font Awesome 5 Regular",
                    "Font Awesome 5 Brands",
                    "simple-line-icons",
                ],
                urls: ["../Content/assets/css/fonts.min.css"],
            },
            active: function () {
                sessionStorage.fonts = true;
            },
        });
    </script>
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
    <!-- Kaiadmin JS -->
    <script src="../Content/assets/js/kaiadmin.min.js"></script>
</head>


<form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
            <div class="container-fluid px-4">
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <div class="col-9 col-md-10">
                        <h4 class="mt-4 "><b>PRODUCTION LIST</b></h4>
                    </div>
                </div>
                <hr />
                <div>
                    <div class="row">
                        <br />

                        <%--<div class="table-responsive text-center">--%>
                        <div class="table ">
                            <br />
                            <%--New Code by Nikhil 03-01-2025--%>
                            <asp:GridView ID="MainGridLoad" runat="server" CellPadding="4" DataKeyNames="ProjectCode" Width="100%"
                                OnRowDataBound="MainGridLoad_RowDataBound" OnRowCommand="MainGridLoad_RowCommand" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                <columns>
                                    <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                            <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="id,JobNo" Width="100%" OnRowDataBound="GVPurchase_RowDataBound"
                                                    OnRowCommand="GVPurchase_RowCommand" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                    <columns>
                                                        <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                                                <asp:Panel ID="Panel1" runat="server" Style="display: none">
                                                                    <b>Customer Name :</b>
                                                                    <asp:Label ID="lblmessagee" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                                    <br />
                                                                    <b>Product Name :</b>
                                                                    <asp:Label ID="Label7" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                                    <br />
                                                                    <b>Description :</b>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("Discription") %>'></asp:Label>
                                                                    <br />
                                                                    <hr />
                                                                    <asp:GridView ID="gvDetails" runat="server" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                                        <columns>
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Stage" HeaderText="Stage" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TotalQTY" HeaderText="Total Quantity" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="InwardQTY" HeaderText="Inward Quantity" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="OutwardQTY" HeaderText="Outward Quantity" />
                                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Pending" HeaderText="Pending" />
                                                                        </columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job No." HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="jobno" runat="server" Text='<%#Eval("JobNo")%>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="OA No." HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="OAno" runat="server" Text='<%#Eval("OANumber")%>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="ProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Product Discription" HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="ProdDiscript" runat="server" Text='<%#Eval("Discription")%>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="Deliverydate" runat="server" Text='<%# Eval("Deliverydate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Quantity" HeaderStyle-CssClass="gvhead">
                                                            <itemtemplate>
                                                                <asp:Label ID="Total_Price" runat="server" Text='<%#Eval("TotalQuantity")%>'></asp:Label>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quality File" ItemStyle-HorizontalAlign="Center">
                                                            <itemtemplate>
                                                                <asp:LinkButton runat="server" ID="btndrawings" ToolTip="Show drawings" CausesValidation="false" CommandName="DrawingFiles" CommandArgument='<%# Eval("FilePath") %>'><i class="fas fa-folder-open" style="font-size: 26px;"></i></i></asp:LinkButton>
                                                            </itemtemplate>
                                                        </asp:TemplateField>
                                                    </columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Code" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblProjectCode" runat="server" Text='<%#Eval("ProjectCode")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Project Name" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblProjectName" runat="server" Text='<%#Eval("ProjectName")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Sub Products" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblSubProdCount" runat="server" Text='<%#Eval("TotalRecords")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Qty" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblQtyCount" runat="server" Text='<%#Eval("TotalQuantitySum")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Completed Qty" HeaderStyle-CssClass="gvhead">
                                        <itemtemplate>
                                            <asp:Label ID="lblCompletedQtySum" runat="server" Text='<%#Eval("CompletedQuantitySum")%>'></asp:Label>
                                        </itemtemplate>
                                    </asp:TemplateField>
                                </columns>
                            </asp:GridView>

                            <%-- End Code --%>
                        </div>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
</form>
</html>
