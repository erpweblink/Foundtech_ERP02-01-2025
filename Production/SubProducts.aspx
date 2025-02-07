<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubProducts.aspx.cs" Inherits="Production_SubProducts" %>

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

    <link rel="stylesheet" href="Content/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/assets/css/plugins.min.css" />
    <link rel="stylesheet" href="Content/assets/css/kaiadmin.min.css" />


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


        @import url("https://fonts.googleapis.com/css?family=Lato:400,400i,700");

        * {
            font-family: Lato, sans-serif;
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }

        body {
            background-color: #F5F5F5;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            margin: 50px 0;
        }

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
                max-width: 95% !important;
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

        hr {
            text-shadow: 1px 4px #40F2B8C4;
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
        <form id="form1" runat="server">
            <center>
                <h2 style="color: #747474; font-family: Roboto,sans-serif; font-size: 36px; font-style: normal; font-weight: 800;" class="mt-2">SUB PRODUCT LIST</h2>
            </center>
            <hr style="border: 1px solid rgb(182, 178, 156);">
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lblProjectcode" runat="server" Text="Project Code :" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="txtProjectCode" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name :" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="txtProjectName" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="txtCustoName" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblProductName" runat="server" Text="Product Name :" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="txtProductName" runat="server"></asp:Label>
                </div>
            </div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
            </asp:ToolkitScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="container-fluid px-4">
                        <div class="row">
                            <asp:HiddenField runat="server" ID="hideJobNo" />
                            <asp:HiddenField runat="server" ID="hideProdName" />
                            <br />
                            <%--<div class="table-responsive text-center">--%>
                            <div class="table table-responsive">
                                <br />
                                <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="id" Width="100%"
                                    CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sub Product" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="SubProd" runat="server" Text='<%#Eval("SubProductname")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Discription" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="ProdDiscript" runat="server" Text='<%#Eval("SubDescription")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Qty" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lenght" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="Length" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Weight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Width" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Width" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Thickness" runat="server" Text='<%#Eval("Thickness")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Weight" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="TotWeight" runat="server" Text='<%#Eval("TotalWeight")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        <br>
    </div>

    <script src="https://storage.ko-fi.com/cdn/scripts/overlay-widget.js"></script>
    <script src="script.js"></script>
</body>

</html>
