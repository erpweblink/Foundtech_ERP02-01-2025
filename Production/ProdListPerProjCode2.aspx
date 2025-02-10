<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdListPerProjCode2.aspx.cs" Inherits="Production_ProdListPerProjCode2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <title>Foundtech Engineering</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.jssocials/1.4.0/jssocials.css">
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/jquery.jssocials/1.4.0/jssocials-theme-flat.css">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link rel="stylesheet" href="../Content/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/assets/css/plugins.min.css" />
    <link rel="stylesheet" href="../Content/assets/css/kaiadmin.min.css" />

    <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
    <script>     
        function SuccessResult(msg, redirectUrl) {
            swal("Success", msg, {
                icon: "success",
                buttons: {
                    confirm: {
                        className: "btn btn-success",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                window.location.href = redirectUrl;
            });
        };
        function DeleteResult(msg, redirectUrl) {
            swal("Delete!", msg, {
                icon: "error",
                buttons: {
                    confirm: {
                        className: "btn btn-danger",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                window.location.href = redirectUrl;
            });
        };
    </script>
    <link href="../Content1/css/Griddiv.css" rel="stylesheet" />
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

    <script src="../Content/assets/js/core/jquery-3.7.1.min.js"></script>
    <script src="../Content/assets/js/core/popper.min.js"></script>
    <script src="../Content/assets/js/core/bootstrap.min.js"></script>

    <!-- jQuery Scrollbar -->
    <script src="../Content/assets/js/plugin/jquery-scrollbar/jquery.scrollbar.min.js"></script>

    <!-- Chart JS -->
    <script src="../Content/assets/js/plugin/chart.js/chart.min.js"></script>

    <!-- jQuery Sparkline -->
    <script src="../Content/assets/js/plugin/jquery.sparkline/jquery.sparkline.min.js"></script>

    <!-- Chart Circle -->
    <script src="../Content/assets/js/plugin/chart-circle/circles.min.js"></script>

    <!-- Datatables -->
    <script src="../Content/assets/js/plugin/datatables/datatables.min.js"></script>

    <!-- Bootstrap Notify -->
    <script src="../Content/assets/js/plugin/bootstrap-notify/bootstrap-notify.min.js"></script>

    <!-- jQuery Vector Maps -->
    <script src="../Content/assets/js/plugin/jsvectormap/jsvectormap.min.js"></script>
    <script src="../Content/assets/js/plugin/jsvectormap/world.js"></script>

    <!-- Sweet Alert -->
    <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>

    <!-- Kaiadmin JS -->
    <script src="../Content/assets/js/kaiadmin.min.js"></script>

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


        @import url("https://fonts.googleapis.com/css?family=Lato:400,400i,700");

        .wrapper {
            width: 80%;
        }

        h1 {
            margin-bottom: 20px;
        }

        .container {
            background-color: white;
            color: black;
            border-radius: 20px;
            box-shadow: 0 5px 10px 0 rgb(26 160 255);
            margin: 20px 0;
            width: 100%;
        }

        .question {
            font-size: 1.3rem !important;
            font-weight: 600;
            letter-spacing: 0.5px;
            color: #747474;
            padding: 12px 80px 12px 20px;
            position: relative;
            display: flex;
            align-items: center;
            cursor: pointer;
            box-shadow: none !important;
        }

            .question::after {
                content: "\002B";
                font-size: 2.2rem;
                position: absolute;
                right: 20px;
                transition: 0.2s;
            }

            .question.active::after {
                transform: rotate(45deg);
            }

        .answercont {
            max-height: 0;
            overflow: hidden;
            transition: 0.3s;
        }

        .answer {
            padding: 0 20px 20px;
            line-height: 1.5rem;
        }

        .question.active + .answercont {
            @media screen and (max-width: 790px) {
                html {
                    font-size: 14px;
                }

                .wrapper {
                    width: 100%;
                }
            }
        }

        @media (min-width: 768px) {
            .container {
                max-width: 100% !important;
            }

            .question {
                font-size: 0.8rem;
            }
        }

        @media (max-width: 768px) {
            .mob-sty {
                width: 260px !important;
            }

            .question {
                font-size: 0.8rem;
            }
        }

        @media (max-width: 600px) {
            .newline {
                white-space: pre;
                font-weight: bold;
            }
        }

        .abc li {
            display: inline-block;
            font-weight: bold;
        }

        .abc {
            text-align: left;
        }

        @media (max-width: 767px) {
            .abc li {
                display: block;
                font-weight: bold;
            }

            .mr-left {
                margin-left: 12%;
            }

            .wrapper {
                width: 100%;
            }

            .newline {
                white-space: pre;
                font-weight: bold;
                text-align: left;
            }
        }

        .button,
        [type=submit] {
            font-size: x-large;
            align-content: center;
            color: #fff;
            border-color: #747474;
            background-color: #eb1538;
            margin-bottom: .25em;
            text-shadow: 0 .075em .075em rgba(0, 0, 0, .5);
            box-shadow: 0 .25em 0 0 #747474, 0 4px 9px rgba(0, 0, 0, .75);
            border-radius: .5em
        }


            .button:hover,
            [type=submit]:hover {
                color: #fff;
                border-color: #747474;
                background-color: #747474;
                margin-bottom: .25em;
                text-shadow: 0 .075em .075em rgba(0, 0, 0, .5);
                box-shadow: 0 .25em 0 0 #747474, 0 4px 9px rgba(0, 0, 0, .75)
            }

        h2 {
            /* text-shadow: 1px 4px #40F2B8C4; */
            /* text-shadow: 2px 2px #000000c4; */
            /* text-shadow: 1px 4px rgb(227 103 103); */
        }



        .container {
            /* box-shadow: 0 5px 10px 0 rgb(26 255 255); */
            box-shadow: 0 5px 10px 0 rgb(26 160 255);
        }

        p {
            text-align: initial;
            font-weight: bold;
        }

        .circle {
            border-radius: 50%;
            box-shadow: 0px 0px 2px 2px #24c76473;
            position: fixed;
            left: 20px;
            bottom: 15px;
            z-index: 9999;
        }

        .circle1 {
            border-radius: 15px;
            /* box-shadow: 0px 0px 2px 2px #24c76473; */
            position: fixed;
            right: 15px;
            bottom: 10px;
            z-index: 9999;
            background-color: #e31e24;
            padding: 11px;
            color: #fff;
        }

        .pulse {
            animation: pulse-animation 2s infinite;
        }

        @keyframes pulse-animation {
            0% {
                box-shadow: 0 0 0 0px #24c76473;
            }

            100% {
                box-shadow: 0 0 0 20px #24c76473;
            }
        }

        input[type="checkbox"] {
            width: 20px;
            height: 20px;
            margin-right: 10px;
        }

        .awesome {
            font-family: futura;
            font-style: italic;
            width: 100%;
            /* margin: 0 auto; */
            /* text-align: center; */

            color: #313131;
            font-size: 25px;
            /* margin-top: 2px; */
            font-weight: bold;
            /* position: absolute; */
            -webkit-animation: colorchange 20s infinite alternate;
        }

        @-webkit-keyframes colorchange {
            0% {
                color: blue;
            }

            10% {
                color: #8e44ad;
            }

            20% {
                color: #1abc9c;
            }

            30% {
                color: #d35400;
            }

            40% {
                color: blue;
            }

            50% {
                color: #34495e;
            }

            60% {
                color: blue;
            }

            70% {
                color: #2980b9;
            }

            80% {
                color: #f1c40f;
            }

            90% {
                color: #2980b9;
            }

            100% {
                color: pink;
            }
        }
    </style>
    <script type="text/javascript">
        function downloadDWGFile(base64File, fileName) {
            if (!base64File || !fileName) {
                console.error("File data or file name is missing.");
                return;
            }

            var byteCharacters = atob(base64File);
            var byteArray = new Uint8Array(byteCharacters.length);


            for (var i = 0; i < byteCharacters.length; i++) {
                byteArray[i] = byteCharacters.charCodeAt(i);
            }

            var blob = new Blob([byteArray], { type: "application/octet-stream" });

            var link = document.createElement('a');
            link.href = URL.createObjectURL(blob);
            link.download = fileName;

            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    </script>


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



<body>

    <div class="container-fluid" id="comment_form">
        <form id="form1" runat="server" enctype="multipart/form-data">
            <div class="row">
                <div class="col-md-10">
                    <center>
                        <h2 style="color: #747474; font-family: Roboto,sans-serif; font-size: 36px; font-style: normal; font-weight: 800;" class="mt-2">
                            <asp:Label ID="lblPageName" runat="server"></asp:Label>
                            PRODUCT LIST</h2>
                    </center>
                </div>
                <div class="col-md-1" style="margin-top: 18px;">
                    <asp:Button ID="lblBtn" runat="server" CssClass="btn-primary" Text="Back To List" OnClick="lblBtn_Click" Font-Size="17px"></asp:Button>
                </div>
            </div>
            <hr style="border: 1px solid rgb(182, 178, 156);">
            <br />
            <div class="container" style="box-shadow: none; margin-left: -6px; margin-top: -14px;">
                <div class="row">
                    <div class="col-md-4">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Label ID="lblProjectcode" runat="server" Text="Project Code :" Font-Bold="true"></asp:Label><br />
                                    <asp:Label ID="txtProjectCode" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name :" Font-Bold="true"></asp:Label><br />
                                    <asp:Label ID="txtProjectName" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" Font-Bold="true"></asp:Label><br />
                                    <asp:Label ID="txtCustoName" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-5">
                                <asp:Label ID="lblSerachProduct" runat="server" Text="Product Name :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="tctSearchProduct" runat="server" CssClass="form-control" Style="border: 1px solid black;" OnTextChanged="tctSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetProducts" TargetControlID="tctSearchProduct" runat="server">
                                </asp:AutoCompleteExtender>
                            </div>
                            <div class="col-md-5">
                                <asp:Label ID="lblSerachDiscr" runat="server" Text="Product Discription :" Font-Bold="true"></asp:Label>
                                <asp:TextBox ID="tctSearchDiscr" runat="server" CssClass="form-control" Style="border: 1px solid black;" OnTextChanged="tctSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender4" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetDescription" TargetControlID="tctSearchDiscr" runat="server">
                                </asp:AutoCompleteExtender>
                            </div>
                            <div class="col-md-1" style="margin-top: 21px;">
                                <asp:LinkButton ID="btnrefresh" runat="server" OnClick="btnrefresh_Click" Style="width: 100%;padding: 7px 26px 7px 13px;" CssClass="form-control btn btn-warning"><i style="color:white" class="fa">&#xf021;</i> </asp:LinkButton>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="container-fluid" style="margin-left: 21%;">
                            <div class="row">
                                <div class="col-md-4">
                                    <span><b>Show Records:</b><br />
                                        <asp:DropDownList
                                            ID="DropDownList1"
                                            CssClass="form-control"
                                            runat="server"
                                            Style="width: 72px; display: inline-block;"
                                            AutoPostBack="true"
                                            OnTextChanged="DropDownList1_TextChanged">
                                        </asp:DropDownList>
                                        <b>/</b>
                                        <asp:Label ID="lblCount" runat="server" Text="20" Style="display: inline-block;"></asp:Label>
                                    </span>
                                </div>
                                <div class="col-md-4">
                                    <span><b>Products :</b><br />
                                        <asp:DropDownList ID="txtdropdown" runat="server" CssClass="form-control" OnTextChanged="txtdropdown_TextChanged" AutoPostBack="True" Style="width: 155px;">
                                            <asp:ListItem Text="Not Completed" Value="3" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Completed" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
            </asp:ToolkitScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="divtable" runat="server">
                        <div class="container-fluid px-4">
                            <div class="row">
                                <%--<div class="table-responsive text-center">--%>
                                <div class="table table-responsive">
                                    <br />
                                    <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="ID,JobNo,Remark,OutwardQTY" Width="100%" OnRowDataBound="GVPurchase_RowDataBound" OnRowEditing="GVPurchase_RowEditing"
                                        OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnShowDtls" ToolTip="View Sub Products" CommandName="ViewDetails" CommandArgument='<%# Eval("JobNo") + "," + Eval("rowmaterial") %>'><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 26px; color: green;"></i></asp:LinkButton>
                                                    <asp:Label ID="OaNumber" runat="server" Text='<%#Eval("Oanumber")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Job No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="jobno" runat="server" Text='<%#Eval("JobNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="CustomerName" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Productname" runat="server" Text='<%#Eval("RowMaterial")%>' Enabled="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Discription" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="ProdDiscript" runat="server" Text='<%#Eval("Discription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="Deliverydate" runat="server" Text='<%# Eval("Deliverydate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="Total_Price" runat="server" Text='<%#Eval("TotalQTY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="In Qty" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="InwardQty" runat="server" Text='<%#Eval("InwardQTY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Out Qty" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="OutwardQty" runat="server" Text='<%#Eval("OutwardQTY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revert Qty" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="RevertQty" runat="server" Text='<%#Eval("RevertQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Drawings" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btndrawings" ToolTip="Show drawings" CausesValidation="false" CommandName="DrawingFiles" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-folder-open"  style="font-size: 26px;"></i></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="Status" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="120">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Add Quantity and Send" CausesValidation="false" CommandName="Edit" CommandArgument='<%#Eval("JobNo") %>'><i class="fas fa-plus-square"  style="font-size: 26px; color:blue; "></i></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="btnwarrehouse" runat="server" Height="27px" ToolTip="Metarial Request to Store" CausesValidation="false" CommandName="Rowwarehouse" CommandArgument='<%# Eval("JobNo") %>'><i class='fas fa-cart-plus' style='font-size:24px;color: #212529;'></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>


                    <%-- First Pop up --%>
                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />

                    <aspanel id="PopupHistoryDetail" runat="server" cssclass="modelprofile1">
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="container" style="margin-left: 25px;">


                                    <div class="container-fluid" style="display: flex;">
                                        <div class="profilemodel2">
                                            <div class="headingcls">
                                                <h4 class="modal-title">Production Details 
                                                 <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                            </div>

                                            <br />
                                            <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                                <div class="row">
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">Job No:</asp:Label>
                                                        <asp:TextBox ID="txtjobno" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Customer Name:</asp:Label>
                                                        <asp:TextBox ID="txtcustomername" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Total Quantity:</asp:Label>
                                                        <asp:TextBox ID="txttotalqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Inward QTY:</asp:Label>
                                                        <asp:TextBox ID="txtinwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Pending QTY:</asp:Label>
                                                        <asp:TextBox ID="txtpending" CssClass="form-control" placeholder="Enter Pending QTY" TextMode="Number" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                        <asp:TextBox runat="server" ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>
                                                        <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3" id="AttachmentID" runat="server" visible="false">
                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label">Upload File:</asp:Label>
                                                        <asp:FileUpload ID="AttachmentUpload" runat="server" CssClass="form-control" />
                                                        <asp:Label ID="lblfile1" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 18px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="btnsendtoback" class="btn btn-warning" OnClick="btnsendtoback_Click" OnClientClick="this.style.display='none';">
                                                              <span class="btn-label">
                                                                  <i class="fa fa-arrow-left"></i>
                                                              </span>
                                                             Save & Back
                                                        </asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="btnSendtopro" class="btn btn-success" OnClick="btnsave_Click" OnClientClick="this.style.display='none';">
                                                                <span class="btn-label">
                                                                    <i class="fa fa-check"></i>
                                                                </span>
                                                               Save & Next
                                                        </asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </aspanel>
                    <%-- End pop up  --%>


                    <%-- Second pop Up  --%>
                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                        PopupControlID="PopupHistoryDetail1" OkControlID="Closepophistory1" />

                    <aspanel id="PopupHistoryDetail1" runat="server" cssclass="modelprofile1">
                        <div class="col-md-12 ">

                            <div class="container-fluid" style="display: flex;">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Drawing Files
                                                    <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button>
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <asp:Label ID="lblJobNo" runat="server" Font-Bold="true" Text="Job No : "></asp:Label>
                                        <asp:Label ID="lblJobNumb" runat="server"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblProductName" runat="server" Font-Bold="true" Text="Product Name : "></asp:Label>
                                        <asp:Label ID="lblProdName" runat="server"></asp:Label>
                                        <br />
                                        <br />
                                        <div class="row">
                                            <asp:Repeater ID="rptImages" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <div class="image-item">
                                                            <!-- Display the image -->
                                                            <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../Content1/img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" CommandArgument='<%# Eval("ID") %>' ToolTip="Open File" />
                                                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="Drawing Name : " CssClass="form-label"></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text='<%# Eval("FileName") %>' CssClass="form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                </div>
                            </div>


                        </div>
                    </aspanel>
                    <%-- End second Panel --%>


                    <%-- Third PopUp --%>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-8">
                            <div class="container" id="DivWarehouse" runat="server" visible="false">
                                <div class="col-md-12">
                                    <div class="profilemodel2">
                                        <div class="headingcls">
                                            <div class="row">
                                                <div class="col-md-8">
                                                    <h4 class="modal-title">Warehouse Material Request Page
                                                    </h4>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:LinkButton runat="server" ID="LinkButton1" CausesValidation="false" class="btn btn-danger" OnClick="btncancle_Click" Style="margin-left: 71px;">
                                                 <span class="btn-label">
                                                     <i class="fa fa-chevron-circle-left"></i>
                                                 </span>
                                                Back To Products
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <center id="dropdownEntry" runat="server">
                                            <span>
                                                <asp:Label ID="lblDropDown" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Entry</asp:Label>
                                                <asp:DropDownList ID="txtdropEntry" runat="server" CssClass="form-control" Style="border: 2px solid #00e7ff; width: 154px;" OnTextChanged="txtdropEntry_TextChanged" AutoPostBack="True">
                                                    <asp:ListItem Text="---Please Select---" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Regular Entry" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Plate Entry" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </center>
                                        <br />
                                        <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                            <div class="row">
                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Stock/RM:</asp:Label>
                                                    <asp:TextBox ID="txtRMC" CssClass="form-control" placeholder="Search Stock/RM" runat="server" Width="100%"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Stock/RM" ControlToValidate="txtRMC" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRMCList"
                                                        TargetControlID="txtRMC">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3" runat="server" visible="false">
                                                    <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label"> <span class="spncls">*</span>Available Size:</asp:Label>
                                                    <asp:TextBox ID="txtAvailablesize" CssClass="form-control" Font-Bold="true" AutoPostBack="true" OnTextChanged="txtAvailablesize_TextChanged" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Available Size" ControlToValidate="txtAvailablesize" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetAvailablesizeList"
                                                        TargetControlID="txtAvailablesize">
                                                    </asp:AutoCompleteExtender>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Thickness:</asp:Label>

                                                    <asp:TextBox ID="txtThickness" TextMode="Number" placeholder="Thickness" AutoPostBack="true" OnTextChanged="txtThickness_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Thickness" ControlToValidate="txtThickness" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Width:</asp:Label>

                                                    <asp:TextBox ID="txtwidth" CssClass="form-control" placeholder="Width" AutoPostBack="true" OnTextChanged="txtwidth_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="1" ErrorMessage="Please Enter width" ControlToValidate="txtwidth" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Length:</asp:Label>

                                                    <asp:TextBox ID="txtlength" CssClass="form-control" placeholder="Length" AutoPostBack="true" OnTextChanged="txtlength_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="1" ErrorMessage="Please Enter length" ControlToValidate="txtlength" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3" id="totalqty" runat="server">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Available Quantity:</asp:Label>
                                                    <asp:TextBox ID="txtAvilableqty" CssClass="form-control" Font-Bold="true" placeholder="Available Quantity" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-6 col-12 mb-3" id="Weight" runat="server" visible="false">
                                                    <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight:</asp:Label>
                                                    <asp:TextBox ID="txtWeights" CssClass="form-control" placeholder="Enter Weight" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="Please Enter Weight"
                                                        ControlToValidate="txtWeights" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Need QTY:</asp:Label>

                                                    <asp:TextBox ID="txtneedqty" CssClass="form-control" placeholder="Need QTY" AutoPostBack="true" OnTextChanged="txtneedqty_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Need QTY" ControlToValidate="txtneedqty" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight:</asp:Label>

                                                    <asp:TextBox ID="txtWeight" CssClass="form-control" placeholder="Weight" TextMode="Number" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Weight" ControlToValidate="Txtweight" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>

                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>

                                                    <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                                <asp:HiddenField ID="hdnJobid" runat="server" />
                                                <div class="col-md-12">

                                                    <div class="col-md-12" style="margin-top: 18px; text-align: center">
                                                        <asp:LinkButton runat="server" ID="btnWarehousedata" ValidationGroup="1" class="btn btn-success" OnClick="btnWarehousedata_Click">
                                                         <span class="btn-label">
                                                             <i class="fa fa-check"></i>
                                                         </span>
                                                        Send Request
                                                        </asp:LinkButton>
                                                        <%-- <asp:LinkButton runat="server" ID="btncancle" CausesValidation="false" class="btn btn-danger" OnClick="btncancle_Click">
                                                         <span class="btn-label">
                                                             <i class="fas fa-times"></i>
                                                         </span>
                                                        Cancel
                                                        </asp:LinkButton>--%>
                                                    </div>
                                                </div>

                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GVRequest" CssClass="display table table-striped table-hover" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="GVRequest_RowCommand" runat="server" CellPadding="4" ForeColor="#333333" PageSize="30" AllowPaging="true" Width="100%" OnRowEditing="GVRequest_RowEditing" OnRowDataBound="GVRequest_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                                                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                                        <asp:GridView ID="gvDetails" runat="server" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SR.NO" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead sno">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Row Material" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRowmaterial" runat="server" Text='<%# Eval("RowMaterial") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Thickness">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Thickness" runat="server" Text='<%#Eval("APPThickness")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Width">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Width" runat="server" Text='<%#Eval("APPWidth")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Length">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Length" runat="server" Text='<%#Eval("APPLength")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Weight (Kg)">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Weight" runat="server" Text='<%#Eval("approvedWeight")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Qantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblQantity" runat="server" Text='<%# Eval("APPQuantity")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SR.NO" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Request No" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRequestNo" runat="server" Text='<%# Eval("RequestNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Row Material" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowmaterial" runat="server" Text='<%# Eval("RowMaterial") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--  <asp:TemplateField HeaderText="Size" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                     <ItemTemplate>
                                         <asp:Label ID="lblSize" runat="server" Text='<%# Eval("NeedSize") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Thickness">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Thickness" runat="server" Text='<%#Eval("Thickness")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Width">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Width" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Length">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Length" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="PerWeight" runat="server" Text='<%#Eval("PerWeight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText=" Total Weight (Kg)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Weight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQantity" runat="server" Text='<%# Eval("NeedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label4" runat="server" Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' Font-Bold="true" ForeColor="orange" Text="Pending"></asp:Label>
                                                                    <asp:Label ID="Label5" runat="server" Visible='<%# Eval("Status").ToString() == "2" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Approved"></asp:Label>
                                                                    <asp:Label ID="Label16" runat="server" Visible='<%# Eval("Status").ToString() == "3" ? true : false %>' Font-Bold="true" ForeColor="Red" Text="Rejected"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="ImgbtnDelete" CommandName="RowDelete" ToolTip="Delete" CommandArgument='<%#Eval("ID")%>' Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px;color:red"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnEditss" ToolTip="Extra Quantity Send to Store" Visible='<%# Eval("Status").ToString() == "2" ? true : false %>' CausesValidation="false" CommandName="Edit" CommandArgument='<%#Eval("ID")%>'><i class="fas fa-plus-square"  style="font-size: 26px; color:blue; "></i></i></asp:LinkButton>
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
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <%-- End of Third popUP  --%>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSendtopro" />
                </Triggers>
            </asp:UpdatePanel>

        </form>
        <br>
    </div>



    <%--    <button onclick="goBack()" class="btn btn-danger">Go Back</button>

<script>
    function goBack() {
        window.history.back();
    }
</script>--%>
</body>




</html>
