.<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdListPerProjCode.aspx.cs" Inherits="Production_ProdListPerProjCode" %>

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

        function DeleteResult(msg) {
            swal("Delete!", msg, {
                icon: "error",
                buttons: {
                    confirm: {
                        className: "btn btn-danger",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                window.location.href = "DrawingDetails.aspx";
            });
        };
    </script>
    <link href="../Content1/css/styles.css" rel="stylesheet" />

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

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- Select2 CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />

    <!-- Select2 JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>


    <script type="text/javascript">
        $(function () {
            $("[id*=ddlRMC]").select2();
        });
        function addNewDiv() {
            // Get the count of file input fields to create unique names
            var fileCount = $('input[type="file"]').length;

            // Append new file input and remark fields to the container
            $('#container').append(`
     <div class="col-md-6 mb-3">
         <input type="hidden" name="HFfile1" />
         <label class="form-label LblStyle" style="font-weight: bold;">Drawing Attachment:</label>
         <input type="file" name="fileUpload_${fileCount}" class="form-control" />
         <label style="color: red;"></label>
     </div>
     <div class="col-md-6 col-12 mb-3">
         <label class="form-label" style="font-weight: bold;">Drawing Remarks:</label>
         <textarea class="form-control" name="fileRemarks_${fileCount}" placeholder="Enter Drawing Remark"></textarea>
     </div>
                                `);
        }
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

        .wp-block-separator:not(.is-style-wide):not(.is-style-dots)::before {
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
                max-width: 1000px !important;
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

        .container {
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

    <!-- Kaiadmin JS -->
    <script src="../Content/assets/js/kaiadmin.min.js"></script>
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
</head>


<body id="bodyMain">

    <div class="container-fluid" id="comment_form">
        <br />
        <form id="form1" runat="server" enctype="multipart/form-data">

            <center>
                <h2 style="color: #747474; font-family: Roboto,sans-serif; font-size: 36px; font-style: normal; font-weight: 800;" class="mt-2">
                    <asp:Label ID="lblPageName" runat="server"></asp:Label>
                    PRODUCT LIST</h2>
            </center>

            <div class="row">
                <div class="col-md-10"></div>
                <div class="col-md-1">
                    <asp:Button ID="lblBtn" runat="server" CssClass="btn-primary" Text="Back To List" OnClick="lblBtn_Click" Font-Size="17px"></asp:Button>
                </div>
            </div>
            <hr style="border: 1px solid rgb(182, 178, 156); height: 4px;">
            <br />
            <div class="container" style="box-shadow: none; margin-left: 16px;">
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
                        <span>Show Records: 
                           <asp:DropDownList
                               ID="DropDownList1"
                               CssClass="form-control"
                               runat="server"
                               Style="width: 90px; margin-top: -2px; display: inline-block;"
                               AutoPostBack="true"
                               OnTextChanged="DropDownList1_TextChanged">
                           </asp:DropDownList>
                            <b>/</b>
                            <asp:Label ID="lblCount" runat="server" Text="20" Style="display: inline-block;"></asp:Label>
                        </span>
                    </div>
                </div>
            </div>

            <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
            </asp:ToolkitScriptManager>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
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
                                                <asp:LinkButton runat="server" ID="btnShowDtls" ToolTip="View Sub Products" CommandName="ViewDetails" CommandArgument='<%# Eval("JobNo") + "," + Eval("rowmaterial")+ "," + Eval("Discription") %>'><i class="fa fa-info-circle" aria-hidden="true" style="font-size: 26px; color: green;"></i></asp:LinkButton>
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
                                                <asp:Label ID="Deliverydate" runat="server" Text='<%# Eval("DeleveryDate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="Total_Price" runat="server" Text='<%#Eval("TotalQty")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Drawings" ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btndrawings" ToolTip="Show drawings" CausesValidation="false" CommandName="DrawingFiles" CommandArgument='<%# Eval("JobNo") %>'><i class="fas fa-folder-open"  style="font-size: 26px;"></i></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Add Quantity and Send" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Eval("JobNo") %>'><i class="fas fa-plus-square"  style="font-size: 26px; color:blue; "></i></i></asp:LinkButton>&nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>



                    <%-- First PopUp Of Save and next and add drawing --%>
                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />
                    <asp:Panel ID="PopupHistoryDetail" runat="server" CssClass="modelprofile1">
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="container" style="margin-left: 25px;">
                                    <div class="container-fluid" style="display: flex;">
                                        <div class="profilemodel2">
                                            <div class="headingcls">
                                                <h4 class="modal-title">Production Details 
                                    <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button>
                                                </h4>
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
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Product Name:</asp:Label>
                                                        <asp:TextBox ID="txtProductname" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Total Quantity:</asp:Label>
                                                        <asp:TextBox ID="txttotalqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                        <asp:TextBox ID="txtoutwardqty" CssClass="form-control" ReadOnly="true" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <div class="grid-scroll-container">
                                                            <asp:GridView ID="grdgrid" runat="server"
                                                                CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="File Name" HeaderStyle-CssClass="gvhead">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../Content1/img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" CommandArgument='<%# Eval("Id") %>' ToolTip="Open File" />
                                                                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="gvhead">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButtonTrash" runat="server" OnClick="LinkButtonTrash_Click" CommandArgument='<%# Eval("Id") %>' ToolTip="Delete File">
                                                     <i class="fa fa-trash" aria-hidden="true"></i>
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </div>

                                                <!-- GridView is placed before the Remarks section -->
                                                <div class="row">
                                                </div>

                                                <!-- This is the container div for dynamically added elements (still outside the GridView) -->
                                                <div id="container" class="row">
                                                    <!-- Existing divs will go here -->
                                                </div>

                                                <div class="col-md-6 mb-3">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add Drawing" OnClientClick="addNewDiv(); return false;" CssClass="btn btn-primary" Style="padding: 5px; font-size: 16px; background-color: #01a9ac;" />
                                                </div>

                                                <!-- Remarks section -->
                                                <div class="col-md-6 col-12 mb-3">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>
                                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-md-6">
                                                        <asp:LinkButton runat="server" ID="btnSendtopro" class="btn btn-success" OnClick="btnsave_Click" OnClientClick="this.style.display='none';" Style="display: flex; align-items: center;">
                                                            <span class="btn-label" style="padding: 7px 5px 4px 60px;">
                                                                <i class="fa fa-check"></i>
                                                            </span>
                                                           Save & Next
                                                        </asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-3"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </asp:Panel>
                    <%-- End of first popUP  --%>



                    <%-- Second PopUp Of View files --%>
                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                        PopupControlID="PopupHistoryDetail1" OkControlID="Closepophistory1" />
                    <asp:Panel ID="PopupHistoryDetail1" runat="server" CssClass="modelprofile1">
                        <div class="row">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="container" style="margin-left: 25px;">
                                    <div class="container-fulid" style="display: flex;">
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
                            </div>
                            <div class="col-md-3"></div>
                    </asp:Panel>
                    <%-- End of Second popUP  --%>



                    <%-- Third PopUp --%>
                    <div class="row container" id="DivWarehouse" runat="server" visible="false">
                        <div class="col-md-12">
                            <div class="profilemodel2">
                                <div class="headingcls">
                                    <h4 class="modal-title">Warehouse Material Request Page
                                    </h4>
                                </div>
                                <br />
                                <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                    <div class="row">
                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label">Stock/RM:</asp:Label>
                                            <asp:TextBox ID="txtRMC" CssClass="form-control" placeholder="Search Company" runat="server" Width="100%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Stock/RM" ControlToValidate="txtRMC" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRMCList"
                                                TargetControlID="txtRMC">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3" runat="server" visible="false">
                                            <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label"> Available Size:</asp:Label>
                                            <asp:TextBox ID="txtAvailablesize" CssClass="form-control" Font-Bold="true" AutoPostBack="true" OnTextChanged="txtAvailablesize_TextChanged" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Available Size" ControlToValidate="txtAvailablesize" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetAvailablesizeList"
                                                TargetControlID="txtAvailablesize">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3" runat="server" visible="false">
                                            <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label">Available Quantity:</asp:Label>

                                            <asp:TextBox ID="txtAvilableqty" CssClass="form-control" Font-Bold="true" ReadOnly="true" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Need Size:</asp:Label>

                                            <asp:TextBox ID="txtsize" CssClass="form-control" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Need Size" ControlToValidate="txtsize" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label">Need QTY:</asp:Label>

                                            <asp:TextBox ID="txtneedqty" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Need QTY" ControlToValidate="txtneedqty" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label">Size weight:</asp:Label>

                                            <asp:TextBox ID="Txtweight" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Weight" ControlToValidate="Txtweight" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-md-6 col-12 mb-3">
                                            <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>

                                            <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
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
                                                <asp:LinkButton runat="server" ID="btncancle" CausesValidation="false" class="btn btn-danger" OnClick="btncancle_Click">
                                                     <span class="btn-label">
                                                         <i class="fas fa-times"></i>
                                                     </span>
                                                    Cancel
                                                </asp:LinkButton>
                                            </div>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GVRequest" CssClass="display table table-striped table-hover" AutoGenerateColumns="false" DataKeyNames="ID" OnRowCommand="GVRequest_RowCommand" runat="server" CellPadding="4" ForeColor="#333333" PageSize="30" AllowPaging="true" Width="100%" OnRowEditing="GVRequest_RowEditing">
                                                <Columns>
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
                                                    <asp:TemplateField HeaderText="Size" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("NeedSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qantity" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQantity" runat="server" Text='<%# Eval("NeedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label14" runat="server" Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' Font-Bold="true" ForeColor="orange" Text="Pending"></asp:Label>
                                                            <asp:Label ID="Label15" runat="server" Visible='<%# Eval("Status").ToString() == "2" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Approved"></asp:Label>
                                                            <asp:Label ID="Label16" runat="server" Visible='<%# Eval("Status").ToString() == "3" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Rejected"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="ImgbtnDelete" CommandName="RowDelete" ToolTip="Delete" CommandArgument='<%#Eval("ID")%>' Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px;color:red"></i></asp:LinkButton>
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
                    <%-- End of third popup  --%>
                </ContentTemplate>

                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSendtopro" />

                </Triggers>
            </asp:UpdatePanel>
        </form>
        <br>
    </div>

    <script src="https://storage.ko-fi.com/cdn/scripts/overlay-widget.js"></script>
</body>

</html>
