<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="Vista.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%:((Dominio.Usuario)Session["UserSession"]).Nick%> | Perfil</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Perfil de usuario</h1>
</asp:Content>
