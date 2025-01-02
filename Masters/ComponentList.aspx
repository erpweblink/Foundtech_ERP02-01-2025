<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ComponentList.aspx.cs" Inherits="Masters_ComponentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
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
                            <h4 class="mt-4 ">&nbsp <b>ROW MATERIAL LIST</b></h4>
                        </div>
                        <div class="col-3 col-md-2 mt-4">
                            <asp:Button ID="btnCreate" CssClass="form-control btn btn-warning" OnClick="btnCreate_Click" runat="server" Text="Create" />
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-3">
                            <%--<asp:Label ID="Label2" runat="server" Text="Quotation No.:"></asp:Label>--%>
                            <div>
                                <asp:TextBox ID="txtcomponent" CssClass="form-control" placeholder="Search Row Material" runat="server" OnTextChanged="txtcomponent_TextChanged" Width="100%" AutoPostBack="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="Please Enter Row Material Name"
                                    ControlToValidate="txtcomponent" ValidationGroup="form1" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionListCssClass="completionList"
                                    CompletionListHighlightedItemCssClass="itemHighlighted" CompletionListItemCssClass="listItem"
                                    CompletionInterval="10" MinimumPrefixLength="1" ServiceMethod="GetComponentList"
                                    TargetControlID="txtcomponent">
                                </asp:AutoCompleteExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="btnrefresh" OnClick="btnrefresh_Click" runat="server" CssClass="form-control btn btn-warning"><i style="color:white" class="fa">&#xf021;</i></asp:LinkButton>
                        </div>
                    </div>
                    <br />
                    <div>
                        <div class="row">
                            <div class="table-responsive text-center">
                                <div class="table ">
                                    <asp:GridView ID="GVComponentlist" runat="server" CellPadding="4" DataKeyNames="id" PageSize="10" AllowPaging="true" Width="100%"
                                        CssClass="display table table-striped table-hover" AutoGenerateColumns="false" OnRowCommand="GVComponentlist_RowCommand" OnPageIndexChanging="GVComponentlist_PageIndexChanging" OnRowDataBound="GVComponentlist_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Row Material Code" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="ComponentCode" runat="server" Text='<%#Eval("ComponentCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Row Material Name" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="ComponentName" runat="server" Text='<%#Eval("ComponentName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Price" HeaderStyle-CssClass="gvhead">
                                        <ItemTemplate>
                                            <asp:Label ID="Price" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax %" HeaderStyle-CssClass="gvhead">
                                        <ItemTemplate>
                                            <asp:Label ID="Tax" runat="server" Text='<%#Eval("Tax")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="HSN" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="HSN" runat="server" Text='<%#Eval("HSN")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:Label ID="Status" runat="server" Text='<%#Eval("Status")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION" HeaderStyle-CssClass="gvhead">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" Height="27px" CausesValidation="false" CommandName="RowEdit" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-edit' style='font-size:24px;color: #212529;'></i></asp:LinkButton>
                                                    <asp:LinkButton ID="btnDelete" runat="server" Height="27px" ToolTip="Delete" CausesValidation="false" CommandName="RowDelete" OnClientClick="Javascript:return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("ID")%>'><i class='fas fa-trash' style='font-size:24px;color: red;'></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>

