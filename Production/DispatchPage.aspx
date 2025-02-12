<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DispatchPage.aspx.cs" Inherits="Production_DispatchPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
                            <h4 class="mt-4 "><b>DISPATCH LIST</b></h4>
                        </div> 
                        <div class="col-9 col-md-2">
                            <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" CssClass="btn btn-outline-success" Text="Import Excel" Style="font-size:14px;margin-top: 15px;"/>
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
                                                    <asp:TemplateField HeaderText="Inward Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRecivedQty" runat="server" Text='<%#Eval("ReceivedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Outward Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSentQty" runat="server" Text='<%#Eval("SentQTy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Set" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemainReqQty" runat="server" Text='<%#Eval("RemainingQTy")%>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="PDF File" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnPdfFile" ToolTip="Show drawings" CausesValidation="false" CommandName="PdfDownload" CommandArgument='<%# Eval("ProjectCode") %>'><i class="fas fa-folder-open"  style="font-size: 26px;"></i></i></asp:LinkButton>
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


                    <%-- To send production to dispatch  --%>
                    <asp:Button ID="SendModal1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="SendModal1"
                        PopupControlID="PopupSendDetail" OkControlID="Closepophistory1" />

                    <asp:Panel ID="PopupSendDetail" runat="server">
                        <div class="row container">
                            <div class="col-md-1"></div>
                            <div class="col-md-11">
                                <div style="background-color: #fefefe;width: 396px;height: 530px;padding: 24px 8px;border: 3px solid black;">
                                    <div style="background-color: #01a9ac; height: 50px; width: 373px; margin-top: -16px;">
                                        <h4 style="padding: 8px; color: #fff;">Send to Dispatch 
                                    <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px;">
                                        <div class="card" style="padding: 2px 0px 0px 0px;">
                                            <div class="card-body">
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Total Set:</asp:Label>
                                                    <asp:TextBox ID="txtSetQty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label">Inward Set:</asp:Label>
                                                    <asp:TextBox ID="txtReqQuantity" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <br />
                                                <div class="col-md-12 d-none">
                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Job Nos:</asp:Label>
                                                    <asp:TextBox ID="txtJobNos" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-md-12">
                                                    <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label">Outward Set:</asp:Label>
                                                    <asp:TextBox ID="txtEnteredQty" CssClass="form-control" runat="server" OnTextChanged="txtEnteredQty_TextChanged" AutoPostBack="true"></asp:TextBox>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendDispatch" />
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="false"></rsweb:ReportViewer>
    </form>

</asp:Content>
