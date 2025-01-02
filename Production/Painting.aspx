<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Painting.aspx.cs" Inherits="Production_Painting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script src="../Content/assets/js/plugin/sweetalert/sweetalert.min.js"></script>
 <script>     
     function SuccessResult(msg) {
         swal("Success", msg, {
             icon: "success",
             buttons: {
                 confirm: {
                     className: "btn btn-success",
                     TimeRanges: "5000",
                 },
             },
         }).then(function () {
             window.location.href = "Painting.aspx";
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
             window.location.href = "Painting.aspx";
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
                            <h4 class="mt-4 "><b>PAINTING LIST</b></h4>
                        </div>
                    </div>
                    <hr />
                    <div>
                        <div id="divtable" runat="server">
                            <div class="card">
                                <div class="card-body">
                                    <div class="table-responsive">
                                        <div class="table">
                                            <br />

                                            <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="ID,JobNo,Remark,OutwardQTY" PageSize="10" AllowPaging="true" Width="100%" OnRowDataBound="GVPurchase_RowDataBound" OnRowEditing="GVPurchase_RowEditing"
                                                OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                <Columns>
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
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="CustomerName" runat="server" Text='<%#Eval("CustomerName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Deliverydate" runat="server" Text='<%# Eval("Deliverydate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Total_Price" runat="server" Text='<%#Eval("TotalQuantity")%>'></asp:Label>
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
                                                            <asp:LinkButton runat="server" ID="btndrawings" ToolTip="Show drawings" CausesValidation="false" CommandName="DrawingFiles" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-folder-open"  style="font-size: 26px; color:black; "></i></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Status" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACTION" HeaderStyle-Width="120">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnEdit" ToolTip="Add Quantity and Send" CausesValidation="false" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-plus-square"  style="font-size: 26px; color:blue; "></i></i></asp:LinkButton>&nbsp;
                                                       <%-- <asp:LinkButton ID="btnwarrehouse" runat="server" Height="27px" ToolTip="Metarial Request to Store" CausesValidation="false" CommandName="Rowwarehouse" CommandArgument='<%# Container.DataItemIndex %>'><i class='fas fa-cart-plus' style='font-size:24px;color: #212529;'></i></asp:LinkButton>--%>
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


                    <asp:Button ID="btnhist" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupHistory" runat="server" TargetControlID="btnhist"
                        PopupControlID="PopupHistoryDetail" OkControlID="Closepophistory" />

                    <asp:Panel ID="PopupHistoryDetail" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-3"></div>
                            <div class="col-md-8">
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
                                                <asp:TextBox ID="txtpending" CssClass="form-control" placeholder="Enter Pending QTY" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                <asp:TextBox ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Remarks:</asp:Label>
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Remark" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-12" style="margin-top: 18px; text-align: center">

                                                <asp:LinkButton runat="server" ID="btnsendtoback" class="btn btn-warning" OnClick="btnsendtoback_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-arrow-left"></i>
                                                        </span>
                                                       Save & Back
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="btnSendtopro" class="btn btn-success" OnClick="btnsave_Click">
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
                            <div class="col-md-1"></div>
                        </div>
                    </asp:Panel>

                    <%--    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                        PopupControlID="PopupHistoryDetail1" OkControlID="Closepophistory1" />

                    <asp:Panel ID="PopupHistoryDetail1" runat="server" CssClass="modelprofile1" Style="display: none">
                        <div class="row container">
                            <div class="col-md-4"></div>
                            <div class="col-md-8">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Warehouse Material Request Page
                                    <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>

                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label">Stock/RM:</asp:Label>

                                                <asp:TextBox ID="txtRM" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="form-label">Available Quantity:</asp:Label>

                                                <asp:TextBox ID="txtAvilableqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Size:</asp:Label>

                                                <asp:TextBox ID="txtsize" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>

                                                <asp:TextBox ID="txtDescription" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label">Need QTY:</asp:Label>

                                                <asp:TextBox ID="txtneedqty" CssClass="form-control" TextMode="Number" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3" style="margin-top: 18px">
                                                    <asp:Button ID="Button2" OnClick="btnsave_Click" ToolTip="Save" CssClass="form-control btn btn-outline-primary m-2" runat="server" Text="Save" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </asp:Panel>--%>

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
                                                            <asp:Label ID="Label4" runat="server" Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' Font-Bold="true" ForeColor="orange" Text="Pending"></asp:Label>
                                                            <asp:Label ID="Label5" runat="server" Visible='<%# Eval("Status").ToString() == "2" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Approved"></asp:Label>
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

                    <asp:Button ID="Button1" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Button1"
                        PopupControlID="PopupHistoryDetail1" OkControlID="Closepophistory1" />

                    <asp:Panel ID="PopupHistoryDetail1" runat="server" CssClass="modelprofile1">
                        <div class="row container">
                            <div class="col-md-3"></div>
                            <div class="col-md-6">
                                <div class="profilemodel2">
                                    <div class="headingcls">
                                        <h4 class="modal-title">Drawing Files
                     <button type="button" id="Closepophistory1" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button>
                                        </h4>
                                    </div>
                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">

                                            <asp:Repeater ID="rptImages" runat="server">
                                                <ItemTemplate>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <div class="image-item">
                                                            <!-- Display the image -->
                                                            <asp:ImageButton ID="ImageButtonfile2" ImageUrl="../Content1/img/Open-file2.png" runat="server" Width="30px" OnClick="ImageButtonfile2_Click" CommandArgument='<%# Eval("ID") %>' ToolTip="Open File" />
                                                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Text="Drawing Name : " CssClass="form-label"></asp:Label>
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="true" Text='<%# Eval("Remark") %>' CssClass="form-label"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

</asp:Content>
