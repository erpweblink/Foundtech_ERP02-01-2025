<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="QualityPage.aspx.cs" Inherits="Production_QualityPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
    <script>     
        function SuccessResult(msg, url) {
            swal("Success", msg, {
                icon: "success",
                buttons: {
                    confirm: {
                        className: "btn btn-success",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                // window.location.href = "Packing.aspx";
                window.location.href = url;
            });
        };
        function DeleteResult(msg, url) {
            swal("Delete!", msg, {
                icon: "error",
                buttons: {
                    confirm: {
                        className: "btn btn-danger",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                /*window.location.href = "Packing.aspx";*/
                window.location.href = url;
            });
        };
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
            margin-top: 15px;
            /*padding: 17px 5px 18px 22px;*/
            padding: 0px 0px 15px 0px;
            width: 1116px;
            top: 40px;
            color: #000;
            border-radius: 5px;
        }

        .lblpopup {
            text-align: center;
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
    <style>
        /* Make GridView header sticky */
        .sticky-header {
            position: sticky;
            top: 0;
            background-color: #fff; /* Ensure background stays white */
            z-index: 1; /* Keeps header above the grid content */
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); /* Optional shadow */
        }

            /* Optional: Style for header text */
            .sticky-header th {
                padding: 10px;
                background-color: #f1f1f1; /* Light gray background */
                font-weight: bold;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                            <h4 class="mt-4 "><b>QUALITY LIST</b></h4>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <br />
                        <div id="divtable" runat="server">
                            <div class="card">
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div class="table">
                                            <br />
                                            <%-- Testing Gtid --%>

                                            <asp:GridView ID="GroupRecords" runat="server" CellPadding="4" DataKeyNames="ProjectCode" Width="100%" OnRowEditing="GroupRecords_RowEditing"
                                                OnRowCommand="GroupRecords_RowCommand" OnRowDataBound="GroupRecords_RowDataBound" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
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
                                                    <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Code" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectCode" runat="server" Text='<%#Eval("ProjectCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OA Number" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOaNumber" runat="server" Text='<%#Eval("OaNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectName" runat="server" Text='<%#Eval("ProjectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqQty" runat="server" Text='<%#Eval("RawMateReqQTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Outward Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSentQty" runat="server" Text='<%#Eval("SentQTy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainReqQty" runat="server" Text='<%#Eval("RawMateRemainingReqQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Status" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdStatus" runat="server" Text='<%#Eval("ProductStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Qty" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalQuantity" runat="server" Text='<%#Eval("TotalQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Set" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedSet" runat="server" Text='<%#Eval("ReceivedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Remaining set" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainingSet" runat="server" Text='<%#Eval("RemainingQTy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job No List" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobNoList" runat="server" Text='<%#Eval("JobNoList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total QTY List" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalQTYlist" runat="server" Text='<%#Eval("TotalQTYlist")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward QTY" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInwardQtylist" runat="server" Text='<%#Eval("InwardQTYlist")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Disc List" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDisclist" runat="server" Text='<%#Eval("Disc")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PDF File" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnPdfFile" ToolTip="Show drawings" CausesValidation="false" CommandName="PdfDownload" CommandArgument='<%# Eval("FilePath") %>'><i class="fas fa-folder-open"  style="font-size: 26px;"></i></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                            <%-- End  --%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <%-- Main PopUp Modal --%>
                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />

                    <asp:Panel ID="PopupHistoryDetail" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Product Details 
                                    <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal" onclick="closePopupAndRefresh();">Close</button></h4>
                                    </div>

                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-5">

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">Total Set:</asp:Label>
                                                        <asp:TextBox ID="txtRequestedQty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label"> Remaining Set:</asp:Label>
                                                        <asp:TextBox ID="txtRemainRequeQty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-12 d-none">
                                                    <asp:Label ID="Label50" runat="server" Font-Bold="true" CssClass="form-label">Job Nos.:</asp:Label>
                                                    <asp:TextBox ID="txtJobList" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 ">
                                                    <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Per Set QTY:</asp:Label>
                                                    <asp:TextBox ID="txtinwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 ">
                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Total Set Quantity:</asp:Label>
                                                    <asp:TextBox ID="txttotalqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 d-none">
                                                    <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label"> Send/Revert Requested QTY:</asp:Label>
                                                    <asp:TextBox ID="txtpending" CssClass="form-control" placeholder="Enter Pending QTY" ReadOnly="true" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-12 d-none">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                    <asp:TextBox ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                                </div>
                                                <p style="font-size: 11px; color: red;"><span style="color: red; font-size: 13px">Note<span style="color: red;">*</span> : When The status is completed for all the rows in beside table then only user can be able to sent the products to next stage. </p>
                                            </div>
                                            <br />
                                            <div class="col-md-7">
                                                <div class="col-md-12" style="height: 379px; overflow-y: scroll;">
                                                    <asp:GridView ID="grdgrid" runat="server" OnRowEditing="grdgrid_RowEditing" OnRowDataBound="grdgrid_RowDataBound"
                                                        CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="lblCheckBox" runat="server" OnCheckedChanged="lblCheckBox_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job No" Visible="false">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discreption">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscr" runat="server" Text='<%# Eval("Discr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="InwardQty">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInwardQty" runat="server" Text='<%# Eval("InwardQTYlist") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty Per Set">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQtyPerSet" runat="server" Text='<%# Eval("PerSetQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                        <br />
                                        <br />
                                        <center>
                                            <%-- OnClick="btnsendtoback_Click" OnClientClick="hideButtons();" --%>
                                            <asp:LinkButton runat="server" ID="btnsendtoback" class="btn btn-warning d-none" OnClick="BackModalPopUP">
                                                <span class="btn-label">
                                                    <i class="fa fa-arrow-left"></i>
                                                </span>
                                                Save & Back
                                            </asp:LinkButton>
                                            <%-- OnClick="btnsave_Click" OnClientClick="hideButtons();" --%>
                                            <asp:LinkButton runat="server" ID="btnSendtopro" class="btn btn-success" OnClick="SendModalPopUP">
                                                <span class="btn-label">
                                                    <i class="fa fa-check"></i>
                                                </span>
                                                Send To Dispatch
                                            </asp:LinkButton>
                                        </center>
                                        <script type="text/javascript">
                                            function hideButtons() {
                                                document.getElementById('<%= btnsendtoback.ClientID %>').style.display = 'none';
                                                document.getElementById('<%= btnSendtopro.ClientID %>').style.display = 'none';
                                            }
                                        </script>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <script type="text/javascript">
                            function closePopupAndRefresh() {
                                // Close the popup
                                $find('<%= ModalPopupHistory.ClientID %>').hide();

                                // Trigger postback to call server-side method
                                __doPostBack('<%= btnhist.ClientID %>', '');
                            }
                        </script>
                    </asp:Panel>
                    <%-- End --%>

                    <%-- To send production to dispatch  --%>
                    <asp:Button ID="SendModal1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="SendModal1"
                        PopupControlID="PopupSendDetail" OkControlID="Closepophistory1" />

                    <asp:Panel ID="PopupSendDetail" runat="server">
                        <div class="row container" style="margin-top: -64px; margin-left: -61px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <div style="background-color: #fefefe; width: 396px; height: 558px; padding: 21px 7px; border: 3px solid black;">
                                    <div style="background-color: #01a9ac; height: 52px; width: 373px; margin-top: -15px;">
                                        <h4 style="padding: 8px; color: #fff;">Send to Dispatch 
                                    <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label">Requested Qty:</asp:Label>
                                                    <asp:TextBox ID="txtReqQuantity" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 d-none">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Job Nos:</asp:Label>
                                                    <asp:TextBox ID="txtJobNos" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-12 ">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label">Sent Quantity:</asp:Label>
                                                    <asp:TextBox ID="txtEnteredQty" CssClass="form-control" runat="server" OnTextChanged="txtEnteredQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 " id="AttachmentID" runat="server" visible="false">
                                                    <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label">Upload File:</asp:Label>
                                                    <asp:FileUpload ID="AttachmentUpload" runat="server" CssClass="form-control" />
                                                    <asp:Label ID="lblfile1" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>
                                                    <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <center>
                                                    <%--  --%>
                                                    <asp:LinkButton runat="server" ID="btnSendDispatch" class="btn btn-success" OnClick="btnsave_Click" OnClientClick="hideButtons();">
                                                        <span class="btn-label">
                                                            <i class="fa fa-check"></i>
                                                        </span>
                                                        Save & Next
                                                    </asp:LinkButton>
                                                </center>

                                            </div>
                                        </div>
                                        <script type="text/javascript">
                                            function hideButtons() {
                                                document.getElementById('<%= btnSendDispatch.ClientID %>').style.display = 'none';
                                            }
                                        </script>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <%-- End  --%>

                    <%-- To send products back to painting  --%>
                    <asp:Button ID="BackModal" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="BackModal"
                        PopupControlID="PopupBackDetail" OkControlID="Closepophistory2" />

                    <asp:Panel ID="PopupBackDetail" runat="server">
                        <div class="row container" style="margin-top: -64px; margin-left: -128px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <div style="background-color: #fefefe; width: 562px; height: 560px; padding: 18px 7px; border: 3px solid black;">
                                    <div style="background-color: #01a9ac; height: 52px; width: 539px; margin-top: -15px;">
                                        <h4 style="padding: 8px; color: #fff;">Send to Painting 
                                     <button type="button" id="Closepophistory2" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 5px; margin-left: 3px;">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="col-md-12" style="height: 279px; overflow-y: scroll;">
                                                    <asp:GridView ID="GridSendBack" runat="server" OnRowEditing="GridSendBack_RowEditing" OnRowDataBound="GridSendBack_RowDataBound"
                                                        CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="lblCheckBox1" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Job No" Visible="false">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobNo1" runat="server" Text='<%# Eval("JobNo") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Discreption">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDiscr1" runat="server" Text='<%# Eval("Discr") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty1" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="InwardQty">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInwardQty1" runat="server" Text='<%# Eval("InwardQTYlist") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Qty Per Set" Visible="false">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQtyPerSet1" runat="server" Text='<%# Eval("PerSetQty") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status">
                                                                <HeaderStyle CssClass="sticky-header" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <br />
                                                <div class="col-md-12 " id="Div1" runat="server" visible="false">
                                                    <asp:Label ID="Label14" runat="server" Font-Bold="true" CssClass="form-label">Upload File:</asp:Label>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="true" ForeColor="blue" Text=""></asp:Label>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label17" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>
                                                    <asp:TextBox ID="TextBox4" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <center>
                                                    <%--  --%>
                                                    <asp:LinkButton runat="server" ID="LinkButton1" class="btn btn-success" OnClick="btnsave_Click" OnClientClick="hideButtons();">
                                                        <span class="btn-label">
                                                            <i class="fa fa-check"></i>
                                                        </span>
                                                        Save & Next
                                                    </asp:LinkButton>
                                                </center>
                                            </div>
                                        </div>

                                        <script type="text/javascript">
                                            function hideButtons() {
                                                document.getElementById('<%= btnSendtopro.ClientID %>').style.display = 'none';
                                            }
                                        </script>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <%-- End  --%>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendDispatch" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

</asp:Content>

