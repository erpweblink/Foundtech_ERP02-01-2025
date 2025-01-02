<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ReturnInventory.aspx.cs" Inherits="Store_ReturnInventory" %>

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
                window.location.href = "ReturnInventory.aspx";
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
                window.location.href = "ReturnInventory.aspx";
            });
        };
    </script> 
  
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
                                <div class="row">
                                    <div class="col-9 col-md-10">
                                        <h4 class="mt-4 "><b>RETURN INVENTORY LIST</b></h4>
                                    </div>
                                  <%--  <div class="col-3 col-md-2 mt-4">
                                        <asp:Button ID="btnCreate" CssClass="form-control btn btn-warning" OnClick="btnCreate_Click" runat="server" Text="Create" />
                                    </div>--%>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-md-3">
                                        <div style="margin-top: 14px;">
                                             <asp:Label ID="lblnumber" Font-Bold="true" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="Label17" Font-Bold="true" runat="server" Text="Row Material :"></asp:Label>
                                            <asp:TextBox ID="txtRowMaterial" CssClass="form-control" placeholder="Search Row Material " runat="server" OnTextChanged="txtRowMaterial_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRMCList"
                                                TargetControlID="txtRowMaterial">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div style="margin-top: 14px;">
                                            <asp:Label ID="Label18" Font-Bold="true" runat="server" Text="Inward Number :"></asp:Label>
                                            <asp:TextBox ID="txtInwardno" CssClass="form-control" placeholder="Search Inward Number " runat="server" OnTextChanged="txtInwardno_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" CompletionListCssClass="completionList"
                                                CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetInwardnoList"
                                                TargetControlID="txtInwardno">
                                            </asp:AutoCompleteExtender>

                                        </div>
                                    </div>
                                    <div class="col-md-3" style="text-align: center; margin-top: 30px">
                                        <asp:Button ID="btnSearchData" CssClass="btn btn-success" OnClick="btnSearchData_Click" runat="server" Text="Search" Style="padding: 8px;" />
                                        <asp:Button ID="btnresetfilter" OnClick="btnresetfilter_Click" CssClass="btn btn-danger" runat="server" Text="Reset" Style="padding: 8px;" />
                                    </div>
                                </div>
                                <div>
                                    <div class="table-responsive">
                                        <div class="table">
                                            <br />
                                            <asp:GridView ID="GVPurchase" runat="server" CellPadding="4" DataKeyNames="ID,InwardNo,InwardQty" PageSize="10" AllowPaging="true" Width="100%" OnRowDataBound="GVPurchase_RowDataBound" OnRowEditing="GVPurchase_RowEditing"
                                                OnRowCommand="GVPurchase_RowCommand" OnPageIndexChanging="GVPurchase_PageIndexChanging" CssClass="display table table-striped table-hover dataTable" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No." ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Inwardno" runat="server" Text='<%#Eval("InwardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DATE" runat="server" Text='<%#Eval("DATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Row Material Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="MaterialName" runat="server" Text='<%#Eval("RowMaterial")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="InwardQty" runat="server" Text='<%#Eval("InwardQty")%>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Weight (Kg)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Weight" runat="server" Text='<%#Eval("Weight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Size" runat="server" Text='<%#Eval("Size")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                               <%--                     <asp:TemplateField HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                                                        <ItemTemplate>
                                                           
                                                            <asp:LinkButton ID="btnEdit" runat="server" Height="27px" ToolTip="Edit Inward record" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%# Container.DataItemIndex %>'><i class='fas fa-edit' style='font-size:24px;color: blue;'></i></asp:LinkButton>
                                                            <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete......!?')" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-trash' style='font-size:24px;color: red;'></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div id="divinwardform" runat="server" visible="false">
                                <div class="row">
                                    <div class="col-9 col-md-10">
                                        <h4 class="mt-4 "><b>RETURN INVENTORY ENTRY</b></h4>
                                    </div>
                                    <div class="col-3 col-md-2 mt-4">
                                        <asp:Button ID="Button1" CssClass="form-control btn btn-warning" CausesValidation="false" autopostback="true" OnClick="Button1_Click" runat="server" Text="List" />
                                    </div>
                                    <br />
                                </div>
                                <hr />
                                <div class="row container">

                                    <div class="col-md-12">
                                        <div class="profilemodel2">

                                            <br />
                                            <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                                <div class="row">
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Row Material:</asp:Label>
                                                        <asp:TextBox ID="txtrowmetarial" CssClass="form-control" placeholder="Search Row Material" AutoPostBack="true" OnTextChanged="txtrowmetarial_TextChanged" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="Please Enter Row Material"
                                                            ControlToValidate="txtrowmetarial" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListCssClass="completionList"
                                                            CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                                            CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetRMCList"
                                                            TargetControlID="txtrowmetarial">
                                                        </asp:AutoCompleteExtender>
                                                    </div>                                               
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label15" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Thickness:</asp:Label>

                                                        <asp:TextBox ID="txtThickness" TextMode="Number" placeholder="Thickness" AutoPostBack="true" OnTextChanged="txtThickness_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="1" ErrorMessage="Please Enter Thickness" ControlToValidate="txtThickness" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Width:</asp:Label>

                                                        <asp:TextBox ID="txtwidth" CssClass="form-control" placeholder="Width" AutoPostBack="true" OnTextChanged="txtwidth_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="1" ErrorMessage="Please Enter width" ControlToValidate="txtwidth" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Length:</asp:Label>

                                                        <asp:TextBox ID="txtlength" CssClass="form-control" placeholder="Length" AutoPostBack="true" OnTextChanged="txtlength_TextChanged" TextMode="Number" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="1" ErrorMessage="Please Enter length" ControlToValidate="txtlength" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Total QTY:</asp:Label>
                                                        <asp:TextBox ID="txtTotalQty" CssClass="form-control" ReadOnly="true" placeholder="Enter Total Qty" runat="server"></asp:TextBox>

                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" CssClass="form-label">Inward QTY:</asp:Label>
                                                        <asp:TextBox ID="txtinwardqantity" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtinwardqantity_TextChanged" TextMode="Number" placeholder="Enter Inward Qantity" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Inward Qantity"
                                                            ControlToValidate="txtinwardqantity" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                    </div>
                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>
                                                        <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                    </div>

                                                    <div class="col-md-6 col-12 mb-3">
                                                        <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label"><span class="spncls">*</span>Weight (Kg):</asp:Label>
                                                        <asp:TextBox ID="txtWeight" CssClass="form-control" ReadOnly="true" placeholder="Enter Weight" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Weight"
                                                            ControlToValidate="txtWeight" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                    </div>

                                                    <asp:HiddenField ID="hdnid" runat="server" />
                                                    <asp:HiddenField ID="hdnquantity" runat="server" />
                                                    <div class="col-md-12">
                                                        <div class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-3" style="margin-top: 18px">
                                                                <asp:LinkButton runat="server" ID="btnsavedata" ValidationGroup="form1" class="btn btn-success" OnClick="btnsavedata_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-check"></i>
                                                        </span>
                                                       Save
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

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
                                        <h4 class="modal-title">Outward Entry
                                    <button type="button" id="Closepophistory" class="btnclose" style="display: inline-block;" data-dismiss="modal">Close</button></h4>
                                    </div>

                                    <br />
                                    <div class="body" style="margin-right: 10px; margin-left: 10px; padding-right: 1px; padding-left: 1px;">
                                        <div class="row">
                                            <%--        <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="true" CssClass="form-label">Inward No:</asp:Label>
                                                <asp:TextBox ID="txtInwardnopop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label5" runat="server" Font-Bold="true" CssClass="form-label">Customer Name:</asp:Label>
                                                <asp:TextBox ID="txtcustomernamepop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="form-label">Row Material:</asp:Label>
                                                <asp:TextBox ID="txtrowmaterialpop" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>

                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label16" runat="server" Font-Bold="true" CssClass="form-label">Inward QTY:</asp:Label>
                                                <asp:TextBox ID="txtinwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="true" CssClass="form-label">Old Outward Qty:</asp:Label>
                                                <asp:TextBox ID="txtOldoutwardqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="true" CssClass="form-label">Old Defected Qty:</asp:Label>
                                                <asp:TextBox ID="txtolddefectedqty" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" CssClass="form-label">Outward QTY:</asp:Label>
                                                <asp:TextBox ID="txtoutwardqty" CssClass="form-control" placeholder="Enter Outward QTY" TextMode="Number" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Please Enter Outward Quantity"
                                                    ControlToValidate="txtoutwardqty" ValidationGroup="form2" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label10" runat="server" Font-Bold="true" CssClass="form-label">Total Defect QTY:</asp:Label>
                                                <asp:TextBox ID="txtDefectqty" placeholder="Enter Total Defect QTY" TextMode="Number" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label8" runat="server" Font-Bold="true" CssClass="form-label">Remaining QTY:</asp:Label>
                                                <asp:TextBox ID="txtRemaining" placeholder="Enter Remaining QTY" ReadOnly="true" TextMode="Number" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label3" runat="server" Font-Bold="true" CssClass="form-label">Description:</asp:Label>
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Description" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </div>
                                            <hr />
                                            <div class="col-md-12 mb-3">
                                                <b>Add Defects Wise Quantity</b>

                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label12" runat="server" Font-Bold="true" CssClass="form-label">Defects Type:</asp:Label>
                                                <asp:DropDownList ID="ddlDefectsType" CssClass="form-control" runat="server">
                                                    <asp:ListItem Value="0" Text="--select--"></asp:ListItem>
                                                    <asp:ListItem Value="Cracks in the bending angle" Text="Cracks in the bending angle"></asp:ListItem>
                                                    <asp:ListItem Value="Hole deformation" Text="Hole deformation"></asp:ListItem>
                                                    <asp:ListItem Value="Unstable bending angle" Text="Unstable bending angle"></asp:ListItem>
                                                    <asp:ListItem Value="Splits" Text="Splits"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-6 col-12 mb-3">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="true" CssClass="form-label">Defected QTY:</asp:Label>
                                                <asp:TextBox ID="txtdefectedqty" placeholder="Enter Defected QTY" TextMode="Number" Text="0" AutoPostBack="true" OnTextChanged="txtdefectedqty_TextChanged" CssClass="form-control" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="txtdefectedqty"
                                                    ErrorMessage="Defected quantity is required."
                                                    ValidationGroup="form2"
                                                    CssClass="text-danger"
                                                    Display="Dynamic" />
                                                <br />
                                                <asp:LinkButton runat="server" ID="lnkbtnadd" ValidationGroup="form2" class="btn btn-secondary" OnClick="lnkbtnadd_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-plus"></i>
                                                        </span>
                                                       Add Defects 
                                                </asp:LinkButton>
                                            </div>
                                            <div class="row">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="GVDefects" CssClass="display table table-striped table-hover" AutoGenerateColumns="false" runat="server" CellPadding="4" ForeColor="#333333" PageSize="30" AllowPaging="true" Width="100%" OnRowEditing="GVDefects_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SR.NO" ItemStyle-Width="20" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsrno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defects" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDefects" runat="server" Text='<%# Eval("Defect") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Defect QTY" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead sno">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQty" runat="server" Text='<%# Eval("DefectQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="120" HeaderStyle-CssClass="gvhead">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="ImgbtnDelete" ToolTip="Delete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" OnClick="ImgbtnDelete_Click" CausesValidation="false"><i class="fa fa-trash" style="font-size:24px"></i></asp:LinkButton>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="col-md-12">
                                                <div class="col-md-4"></div>
                                                <div class="col-md-3" style="margin-top: 18px">
                                                    <asp:LinkButton runat="server" ID="btnSendtopro" ValidationGroup="form1" class="btn btn-success" OnClick="btnsave_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-check"></i>
                                                        </span>
                                                       Submit 
                                                    </asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="LinkButton2" CausesValidation="false" class="btn btn-warning" OnClick="LinkButton2_Click">
                                                        <span class="btn-label">
                                                            <i class="fa fa-back"></i>
                                                        </span>
                                                       Back 
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-1"></div>
                        </div>
                    </asp:Panel>


                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
    </form>

</asp:Content>

