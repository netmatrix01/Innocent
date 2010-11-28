<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.CreativeDecorating" Codebehind="CreativeDecorating.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="CreativeDecorating" Src="~/Modules/CreativeDecorating.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <nopCommerce:CreativeDecorating ID="ctrlCreativeDecorating" runat="server" />
</asp:Content>
