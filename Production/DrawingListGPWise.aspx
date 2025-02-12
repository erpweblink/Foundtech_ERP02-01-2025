<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="DrawingListGPWise.aspx.cs" Inherits="Production_DrawingListGPWise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
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
                    <div class="row">
                        <div class="col-9 col-md-9">
                            <h4 class="mt-4 "><b>DRAWING LIST</b></h4>
                        </div>
                        <div class="col-9 col-md-3" style="margin-top: 19px;">
                            <asp:Button ID="btnDetails" CssClass="btn btn-outline-success" Text="See Details" runat="server" Style="margin-left: 103px;" OnClick="btnDetails_Click" AutoPostBack="true" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblSearchDis" runat="server" Text="Discription" Font-Bold="true" Font-Size="Medium"></asp:Label>
                            <asp:TextBox ID="txtSerachDisc" CssClass="form-control" placeholder="Discription" runat="server" OnTextChanged="txtSerachDisc_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" CompletionListCssClass="completionList"
                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetDiscription" TargetControlID="txtSerachDisc" runat="server">
                            </asp:AutoCompleteExtender>
                            <br />
                        </div>
                        <div class="col-md-1" style="margin-top: 27px">
                            <asp:LinkButton ID="btnrefresh" runat="server" OnClick="btnrefresh_Click" Style="width: 86%; padding: 3px 4px 3px 4px;" CssClass="form-control btn btn-warning"><i style="color:white" class="fa">&#xf021;</i> </asp:LinkButton>
                        </div>
                        <div class="col-md-7">
                        </div>
                    </div>
                    <div class="row">
                        <br />
                        <div id="divtable" runat="server">
                            <div class="card">
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div class="table">
                                            <br />
                                            <%-- Testing Gtid --%>

                                            <asp:GridView ID="GVPurchase" runat="server" CellPadding="5" DataKeyNames="Discription,Length,Width" Width="100%" OnRowEditing="GVPurchase_RowEditing"
                                                OnRowCommand="GVPurchase_RowCommand" OnRowDataBound="GVPurchase_RowDataBound" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Discription" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscription" runat="server" Text='<%#Eval("Discription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLength" runat="server" Text='<%#Eval("Length")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Width" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWidth" runat="server" Text='<%#Eval("Width")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total QTY" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalQty" runat="server" Text='<%#Eval("TotalQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job No List" HeaderStyle-CssClass="gvhead" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobNoList" runat="server" Text='<%#Eval("JobNoList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawings" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btndrawings" ToolTip="Show drawings" CausesValidation="false" CommandName="DrawingFiles" CommandArgument='<%# Eval("Discription") %>'><i class="fas fa-folder-open"  style="font-size: 26px;"></i></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="gvhead">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProdStatus" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Add Quantity and Send" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Eval("JobNoList") %>'><i class="fas fa-plus-square"  style="font-size: 26px; color:blue; "></i></i></asp:LinkButton>&nbsp;
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
                  <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal" onclick="closePopupAndRefresh();">Close</button>
                                                </h4>
                                            </div>

                                            <br />
                                            <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                                <div class="row">
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">Total QTY:</asp:Label>
                                                        <asp:TextBox ID="txtjobno" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Discription:</asp:Label>
                                                        <asp:TextBox ID="txtcustomername" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Length:</asp:Label>
                                                        <asp:TextBox ID="txtProductname" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Width:</asp:Label>
                                                        <asp:TextBox ID="txttotalqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3  d-none">
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
                                                                            <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../Content1/img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" CommandArgument='<%# Eval("ID") %>' ToolTip="Open File" />
                                                                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("FileName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="gvhead">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButtonTrash" runat="server" OnClick="LinkButtonTrash_Click" CommandArgument='<%# Eval("ID") %>' ToolTip="Delete File">
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
                                <script type="text/javascript">
                                    function closePopupAndRefresh() {
                                        // Close the popup
                                        $find('<%= ModalPopupHistory.ClientID %>').hide();

                                           // Trigger postback to call server-side method
                                           __doPostBack('<%= btnhist.ClientID %>', '');
                                    }
                                </script>
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
                          <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal" >Close</button>
                                                </h4>
                                            </div>
                                            <br />
                                            <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                                <asp:Label ID="lblJobNo" runat="server" Font-Bold="true" Text="Discription : "></asp:Label>
                                                <asp:Label ID="lblJobNumb" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Label ID="lblProductName" runat="server" Font-Bold="true" Text="Length : "></asp:Label>
                                                <asp:Label ID="lblProdName" runat="server"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          <asp:Label ID="lblWidth" runat="server" Font-Bold="true" Text="Width : "></asp:Label>
                                                <asp:Label ID="lblWid" runat="server"></asp:Label>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSendtopro" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

</asp:Content>
