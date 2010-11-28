<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.InspiredBaking" Codebehind="InspiredBaking.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="InspiredBaking" Src="~/Modules/InspiredBaking.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <nopCommerce:InspiredBaking ID="ctrlInspiredBaking" runat="server" />
</asp:Content>
