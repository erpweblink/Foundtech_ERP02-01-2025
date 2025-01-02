<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreList.aspx.cs" Inherits="Store_StoreList" MasterPageFile="~/AdminMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="../Content1/css/styles.css" rel="stylesheet" />
    
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
                window.location.href = "StoreList.aspx";
            });
        };
        function DeleteResult(msg) {
            swal("error!", msg, {
                icon: "error",
                buttons: {
                    confirm: {
                        className: "btn btn-danger",
                        TimeRanges: "5000",
                    },
                },
            }).then(function () {
                window.location.href = "StoreList.aspx";
            });
        };
    </script> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <style>
        .spncls {
            color: #f20707 !important;
            font-size: 13px !important;
            font-weight: bold;
            text-align: left;
        }

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

        /*.completionList {
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
        }*/

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
                    <div class="card">
                        <div class="card-body">
                            <div id="divtabl" runat="server">
                                <div class="col-9 col-md-10">
                                    <h4 class="mt-4 "><b>Production Request List </b></h4>
                                </div>
                                <asp:HiddenField ID="HDnInward" runat="server" />
                                <asp:HiddenField ID="HddnID" runat="server" />
                                <div class="table-responsive">
                                    <div class="table">
                                        <br />
                                        <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="ID,InwardNo" Width="100%"
                                            OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JobNo" runat="server" Text='<%#Eval("JobNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RequestNo" runat="server" Text='<%#Eval("RequestNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Raw Material">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RowMaterial" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

                                                <asp:TemplateField HeaderText="Need Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NeedQty" runat="server" Text='<%#Eval("NeedQty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Need Weight">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Weight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Department" runat="server" Text='<%#Eval("Department")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                                                    <ItemTemplate>
                                                        <%--    <asp:LinkButton runat="server" ID="btnoutward" Height="27px" ToolTip="Create Outward Entry" CausesValidation="false" CommandName="RowOutward" CommandArgument='<%# Container.DataItemIndex %>'><i class="fas fa-shipping-fast"  style="font-size: 24px; color:orangered; "></i></asp:LinkButton>&nbsp;--%>
                                                        <asp:LinkButton ID="btnEdit" runat="server" Height="27px" ToolTip="Edit Inward record" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%# Container.DataItemIndex %>' Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'><i class='fas fa-edit' style='font-size:24px;color: blue;'></i></asp:LinkButton>

                                                        <asp:Label ID="lblApproved" runat="server"
                                                            Text="Approved"
                                                            Visible='<%# Eval("Status").ToString() == "2" %>'
                                                            ForeColor="Green"
                                                            Style="font-weight: bold; margin-left: 10px;"></asp:Label>
                                                        &nbsp
                                                        <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete......!?')" CommandArgument='<%# Container.DataItemIndex %>'><i class='fas fa-trash' style='font-size:24px;color: red;'></i></asp:LinkButton>
                                                        &nbsp
                                                              <asp:Label ID="Label1" runat="server"
                                                                  Text="Reject"
                                                                  Visible='<%# Eval("Status").ToString() == "3" %>'
                                                                  ForeColor="Red"
                                                                  Style="font-weight: bold; margin-left: 10px;"></asp:Label>
                                                        <asp:LinkButton ID="Lnkcancel" runat="server" Height="27px" ToolTip="Cancel Request" CausesValidation="false" CommandName="RejectCancel" CommandArgument='<%# Container.DataItemIndex %>' Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'><i class='fas fa-times-circle' style='font-size:26px;color:#905151'></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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
                                                    <h4 class="modal-title">Available Details 
     <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                                </div>

                                                <br />
                                                <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                                    <div class="row">
                                                        <div class="col-md-6 col-12 mb-3">
                                                            <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Stock/RM:</asp:Label>
                                                            <asp:TextBox ID="txtRMC" CssClass="form-control" placeholder="Search Stock/RM" ReadOnly="true" runat="server" Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Stock/RM" ControlToValidate="txtRMC" ForeColor="Red"></asp:RequiredFieldValidator>
                                                     
                                                        </div>
                                                        <div class="col-md-6 col-12 mb-3">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Available Quantity:</asp:Label>
                                                            <asp:TextBox ID="txtavailableQty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                                        </div>

                                                        <div class="col-md-6 col-12 mb-3">
                                                            <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Approve Quantity:</asp:Label>
                                                            <asp:TextBox ID="txtApprovQuantity" CssClass="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtneedqty_TextChanged" Placeholder="Enter Quantity"></asp:TextBox>
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

                                                        <div class="col-md-6 col-12 mb-3">
                                                            <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight:</asp:Label>

                                                            <asp:TextBox ID="Txtweight" CssClass="form-control" placeholder="Weight" TextMode="Number" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Weight" ControlToValidate="Txtweight" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="col-md-12" runat="server" id="dvbuttion">

                                                            <div class="col-md-12" style="margin-top: 18px; text-align: center">
                                                                <asp:LinkButton runat="server" ID="btnSendtopro" class="btn btn-success" OnClick="btnSendtopro_Click">
                         <span class="btn-label"  style="text-align:center" >
                             <i class="fa fa-check"></i>
                         </span>
                        Approve
                                                                </asp:LinkButton>
                                                            </div>

                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-1"></div>
                                    </div>
                                </asp:Panel>


                            </div>
                        </div>

                    </div>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


    </form>

</asp:Content>
