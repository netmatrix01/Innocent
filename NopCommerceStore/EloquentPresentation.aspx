<%@ Page Language="C#" MasterPageFile="~/MasterPages/TwoColumn.master" AutoEventWireup="true"
    Inherits="NopSolutions.NopCommerce.Web.EloquentPresentation" Codebehind="EloquentPresentation.aspx.cs" %>

<%@ Register TagPrefix="nopCommerce" TagName="EloquentPresentation" Src="~/Modules/EloquentPresentation.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <nopCommerce:EloquentPresentation ID="ctrlEloquentPresentation" runat="server" />
</asp:Content>
