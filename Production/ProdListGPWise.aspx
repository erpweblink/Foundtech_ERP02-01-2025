<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ProdListGPWise.aspx.cs" Inherits="Production_ProdListGPWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
    <script>
        function HideLabelerror(msg) {
            Swal.fire({
                icon: 'error',
                text: msg,

            })
        };
        function SuccessResult(msg) {
            swal("Success", msg, {
                icon: "success",
                buttons: {
                    confirm: {
                        className: "btn btn-success",
                    },
                },
            });
        };
        function DeleteResult(msg) {
            swal("Delete!", msg, {
                icon: "error",
                buttons: {
                    confirm: {
                        className: "btn btn-danger",
                    },
                },
            });
        };
    </script>
    <script src="../JS/jquery.min.js"></script>
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
    <style>
        #BodyMain {
            background-color: black;
            margin: 0px;
            height: 15vh;
        }

        #DivMain {
            position: relative;
            left: 50%;
            top: 48%;
            transform: translate(-50%, -50%);
            color: white;
        }

        #SpanMain {
            font-size: 40px;
            letter-spacing: 5px;
            text-transform: uppercase;
            line-height: 85%;
            position: relative;
            mix-blend-mode: difference;
            color: white;
        }

        #DivMain::before {
            content: "";
            position: absolute;
            left: 0;
            top: 0;
            width: 0; /* Start with zero width */
            height: 100%;
            background-color: black;
            animation: move 5s linear infinite;
            z-index: -1;
        }

        @keyframes move {
            0%, 100% {
                width: 0; /* Start with width 0 */
            }

            50% {
                width: 100%; /* Cover the entire width of the text */
            }
        }
    </style>
    <style>
        #BodyMain1 {
            background-color: black;
            margin: 0px;
            height: 15vh;
        }

        #DivMain1 {
            position: relative;
            left: 50%;
            top: 48%;
            transform: translate(-50%, -50%);
            color: white;
        }

        #SpanMain1 {
            font-size: 40px;
            letter-spacing: 5px;
            text-transform: uppercase;
            line-height: 85%;
            position: relative;
            mix-blend-mode: difference;
            color: white;
        }

        #DivMain1::before {
            content: "";
            position: absolute;
            left: 0;
            top: 0;
            width: 0; /* Start with zero width */
            height: 100%;
            background-color: black;
            animation: move 5s linear infinite;
            z-index: -1;
        }

        @keyframes move {
            0%, 100% {
                width: 0; /* Start with width 0 */
            }

            50% {
                width: 100%; /* Cover the entire width of the text */
            }
        }
    </style>
    <script type="text/javascript">
        function showLoadingSpinner() {
            // Show loading spinner
            document.getElementById('loadingSpinner').style.display = 'block';
            // Simulate a server-side operation (use the AJAX response callback in real scenario)
            //setTimeout(function () {
            //    // Hide the loading spinner when the operation completes
            //    document.getElementById('loadingSpinner').style.display = 'none';
            //    // Trigger alert
            //    alert('Email sent to client successfully!');
            //}, 3000); // Simulate a delay of 3 seconds (you can adjust the time as needed)
        }
        function showLoadingSpinner1() {
            // Show loading spinner
            document.getElementById('loadingSpinner1').style.display = 'block';
            // Simulate a server-side operation (use the AJAX response callback in real scenario)
            //setTimeout(function () {
            //    // Hide the loading spinner when the operation completes
            //    document.getElementById('loadingSpinner').style.display = 'none';
            //    // Trigger alert
            //    alert('Email sent to client successfully!');
            //}, 3000); // Simulate a delay of 3 seconds (you can adjust the time as needed)
        }
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
                        <div class="col-9 col-md-10">
                            <h4 class="mt-4 "><b>PRODUCTION LIST</b></h4>
                        </div>
                    </div>
                    <hr />
                    <div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Customer Name :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtCustomerName" CssClass="form-control" placeholder="Search Customer" runat="server" OnTextChanged="txtCustomerName_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Company Name"
                                        ControlToValidate="txtCustomerName" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCustomerList"
                                        TargetControlID="txtCustomerName">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="Project Code :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtjobno" CssClass="form-control" placeholder="Search Project Code" runat="server" OnTextChanged="txtjobno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCponoList"
                                        TargetControlID="txtjobno">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Project Name:"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtGST" CssClass="form-control" placeholder="Search Project Name " runat="server" OnTextChanged="txtGST_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Job Number."
                                        ControlToValidate="txtGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetGSTList"
                                        TargetControlID="txtGST">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>

                            <div class="col-md-1" style="margin-top: 36px">
                                <asp:LinkButton ID="btnrefresh" runat="server" OnClick="btnrefresh_Click" Width="100%" CssClass="form-control btn btn-warning"><i style="color:white" class="fa">&#xf021;</i> </asp:LinkButton>
                            </div>
                            <br />

                            <%--<div class="table-responsive text-center">--%>
                            <div class="table ">
                                <br />
                                <%--New Code by Nikhil 03-01-2025--%>
                                <asp:GridView ID="MainGridLoad" runat="server" CellPadding="4" DataKeyNames="ProjectCode" Width="100%"
                                    OnRowDataBound="MainGridLoad_RowDataBound" OnRowCommand="MainGridLoad_RowCommand" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnShowDtls" ToolTip="Send to Details" CommandName="ViewDetails" CommandArgument='<%# Eval("ProjectCode") %>'><i class="fa fa-plus-square" aria-hidden="true" style="font-size: 26px; color: blue;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Code" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectCode" runat="server" Text='<%#Eval("ProjectCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%#Eval("ProjectName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Sub Products" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubProdCount" runat="server" Text='<%#Eval("TotalRecords")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Qty" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyCount" runat="server" Text='<%#Eval("TotalQuantitySum")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Completed Qty" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompletedQtySum" runat="server" Text='<%#Eval("CompletedQuantitySum")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACTION" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="btnSendtopro" ToolTip="Send to Production" CommandName="Sendtoproduction" CommandArgument='<%# Eval("ProjectCode") %>' OnClientClick="showLoadingSpinner();"><i class="fa fa-arrow-circle-right" style="font-size: 26px; color: green;"></i></i></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="btnpdfview" ToolTip="Send to Client" CommandName="SendMail" CommandArgument='<%# Eval("CustomerName") %>' OnClientClick="showLoadingSpinner1();"><i class="fa fa-envelope"  style="font-size: 26px; color:green; "></i></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div id="loadingSpinner" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999;">
                                    <div id="BodyMain">
                                        <div id="DivMain">
                                            <span id="SpanMain">Sending..</span>
                                        </div>
                                    </div>
                                </div>

                                <div id="loadingSpinner1" style="display: none; position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 9999;">
                                    <div id="BodyMain1">
                                        <div id="DivMain1">
                                            <span id="SpanMain1">Sending..</span>
                                        </div>
                                    </div>
                                </div>

                                <%-- End Code --%>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

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
                            <br />
                            <asp:HiddenField runat="server" ID="hdnid" />
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:CheckBox ID="Drawing" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Drawing</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="PlazmaCutting" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Plazma Cutting</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Bending" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Bending</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Fabrication" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Fabrication</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Painting" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Painting</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Packaging" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Quality</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Dispatch" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Dispatch</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="text-align: center">
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-2">
                                        <br />
                                        <asp:Button ID="btnsave" runat="server" CssClass="form-control btn btn-success" OnClick="btnsave_Click" AutoPostBack="true" Text="Submit" OnClientClick="hideButtons();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <script type="text/javascript">
                            function hideButtons() {
                                document.getElementById('<%= btnsave.ClientID %>').style.display = 'none';
                            }
                        </script>

                    </div>
                </div>
                <div class="col-md-1"></div>
            </div>


        </asp:Panel>
    </form>


</asp:Content>

