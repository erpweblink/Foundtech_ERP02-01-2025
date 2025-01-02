<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ProductionList.aspx.cs" Inherits="Production_ProductionList" %>

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
                                    <asp:TextBox ID="txtCustomerName" CssClass="form-control" placeholder="Search Company" runat="server" OnTextChanged="txtCustomerName_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
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
                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text="OA Number :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtjobno" CssClass="form-control" placeholder="Search OA Number" runat="server" OnTextChanged="txtjobno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetCponoList"
                                        TargetControlID="txtjobno">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="Job Number :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtGST" CssClass="form-control" placeholder="Search Job Number " runat="server" OnTextChanged="txtGST_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Job Number."
                                        ControlToValidate="txtGST" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" CompletionListCssClass="completionList"
                                        CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                        CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetGSTList"
                                        TargetControlID="txtGST">
                                    </asp:AutoCompleteExtender>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblfromdate" runat="server" Font-Bold="true" Text="From Date :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txtfromdate" Placeholder="Enter From Date" runat="server" TextMode="Date" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lbltodate" runat="server" Font-Bold="true" Text="To Date :"></asp:Label>
                                <div style="margin-top: 14px;">
                                    <asp:TextBox ID="txttodate" Placeholder="Enter To Date" runat="server" TextMode="Date" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1 col-2" style="margin-top: 36px">
                                <asp:LinkButton ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="100%" CssClass="form-control btn btn-info"><i style="color:white" class="fa">&#xf002;</i> </asp:LinkButton>
                            </div>
                            <div class="col-md-1" style="margin-top: 36px">
                                <asp:LinkButton ID="btnrefresh" runat="server" OnClick="btnrefresh_Click" Width="100%" CssClass="form-control btn btn-warning"><i style="color:white" class="fa">&#xf021;</i> </asp:LinkButton>
                            </div>
                            <br />

                            <%--<div class="table-responsive text-center">--%>
                            <div class="table ">
                                <br />
                                <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="id,JobNo" Width="100%" OnRowDataBound="GVPurchase_RowDataBound"
                                    OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="20" HeaderText=" " HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <img alt="" style="cursor: pointer" src="../Content1/img/plus.png" />
                                                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                                    <%-- <asp:Label ID="lblmessageee" runat="server" Text='<%# "Feedback : " +  Eval("Feedback") %>'></asp:Label>--%>
                                                    <%--     <asp:Label ID="lblo" runat="server" CssClass="font-weight-bold" ForeColor="Red" Text="Feedback of Client Update :"></asp:Label>--%>
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
                                                        <Columns>
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Stage" HeaderText="Stage" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="TotalQTY" HeaderText="Total Quantity" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="InwardQTY" HeaderText="Inward Quantity" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="OutwardQTY" HeaderText="Outward Quantity" />
                                                            <asp:BoundField ItemStyle-Width="150px" DataField="Pending" HeaderText="Pending" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Job No." HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="jobno" runat="server" Text='<%#Eval("JobNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OA No." HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="OAno" runat="server" Text='<%#Eval("OANumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="ProductName" runat="server" Text='<%#Eval("ProductName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Deliverydate" runat="server" Text='<%# Eval("Deliverydate", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Quantity" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <asp:Label ID="Total_Price" runat="server" Text='<%#Eval("TotalQuantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ACTION" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                            <ItemTemplate>
                                                <%--  <asp:LinkButton runat="server" ID="btnpdfview" ToolTip="View Order Acceptance PDF" CommandName="RowView" CommandArgument='<%# Eval("Pono") %>'><i class="fas fa-file-pdf"  style="font-size: 26px; color:red; "></i></i></asp:LinkButton>--%>
                                                <asp:Label ID="Totale" runat="server" Visible='<%# Eval("Status").ToString() == "0" ? true : false %>' Font-Bold="true" ForeColor="blue" Text="Not Created"></asp:Label>
                                                <asp:Label ID="Label4" runat="server" Visible='<%# Eval("Status").ToString() == "1" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Created"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Visible='<%# Eval("Status").ToString() == "2" ? true : false %>' Font-Bold="true" ForeColor="Green" Text="Completed"></asp:Label>
                                                <asp:LinkButton runat="server" ID="btnSendtopro" ToolTip="Send to Production" CommandName="Sendtoproduction" Visible='<%# Eval("Status").ToString() == "0" ? true : false %>' CommandArgument='<%# Eval("JobNo") %>'><i class="fa fa-arrow-circle-right"  style="font-size: 26px; color:green; "></i></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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

                        <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                            <br />
                            <br />
                            <asp:HiddenField runat="server" ID="hdnid" />
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:CheckBox ID="Drawing" Checked="true"  runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Drawing</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="PlazmaCutting" Checked="true"  runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Plazma Cutting</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Fabrication" Checked="true"  runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Fabrication</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Bending" Checked="true"  runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Bending</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Painting" Checked="true"  runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Painting</label>&nbsp;&nbsp;&nbsp;
                                       
                                        <asp:CheckBox ID="Packaging" Checked="true" runat="server" CssClass="form-check-input" />
                                        <label style="font-weight: bold">Packaging</label>&nbsp;&nbsp;&nbsp;
                                       
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
                                        <asp:Button ID="btnsave" runat="server" CssClass="form-control btn btn-success" OnClick="btnsave_Click" AutoPostBack="true" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="col-md-1"></div>
            </div>


        </asp:Panel>
    </form>


</asp:Content>

