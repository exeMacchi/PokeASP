<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="UserFavorites.aspx.cs" Inherits="Vista.UserFavorites" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%:((Dominio.Usuario)Session["UserSession"]).Nick%> | Favoritos</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Pokémon favoritos</h1>
</asp:Content>
