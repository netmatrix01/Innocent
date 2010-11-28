<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.DetailedEmbellishments" Codebehind="DetailedEmbellishments.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="DetailedEmbellishments" Src="~/Modules/DetailedEmbellishments.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <nopCommerce:DetailedEmbellishments ID="ctrlDetailedEmbellishments" runat="server" />
</asp:Content>
