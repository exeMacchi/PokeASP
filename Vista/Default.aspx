<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Vista.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PokeApp</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4">
        <h1 class="text-center">Pokédex Web</h1>
    </div>

    <div class="row justify-content-between gap-3 my-4">
            <!-- Alerta de vacío -->
            <asp:Panel ID="emptyAlert" runat="server" CssClass="alert alert-primary my-4" Visible="false">
                <h2 class="alert-heading fs-2">¡Ups!</h2>
                <p>Parece que no hay ningún Pokémon en la base de datos :(</p>
            </asp:Panel>

            <!-- Foreach aspx -->
            <asp:Repeater ID="PokeCards" runat="server">
                <ItemTemplate>
                    <div class="card" style="width: 18rem;">
                        <img src="<%# Eval("UrlImage") %>" class="card-img-top" alt="Imagen del pokemon">
                        <div class="card-body d-flex flex-column justify-content-between">
                            <h3 class="card-title"><%# Eval("Name") %></h3>
                            <p class="card-text"><%# Eval("Description") %></p>
                            <a href="PokeDetail.aspx?id=<%# Eval("ID") %>" class="btn btn-primary">Ver detalle</a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
</asp:Content>
