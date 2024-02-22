<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="PokeDetail.aspx.cs" Inherits="Vista.PokeDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%: Poke.Name %></title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4">
        <div class="col-6 d-flex justify-content-end">
            <figure class="w-75">
                <img src="<%: Poke.UrlImage %>" alt="Imagen de un pokemon" class="w-100"/>
            </figure>
        </div>

        <div class="col-6 d-flex flex-column gap-3 border-start border-3 border-opacity-10 border-dark">
            <div>
                <h1 class="text-center d-inline"><%: Poke.Name %></h1>
                <p class="d-inline fs-4 fw-semibold text-secondary ms-3">N° <%: Poke.Number %></p>
            </div>
            <p class="lead"><%: Poke.Description %></p>
            <div class="d-flex justify-content-around">
                <div class="d-flex flex-column gap-3 align-items-center">
                    <h3>TIPO</h3>
                    <% if (Poke.Type.Description == "Fuego") { %>
                        <i class="bi bi-fire text-danger fs-1"></i>
                    <% } else if (Poke.Type.Description == "Agua") { %>
                        <i class="bi bi-droplet-half text-primary fs-1"></i>
                    <% } else if (Poke.Type.Description == "Planta") { %>
                        <i class="bi bi-feather text-success fs-1"></i>
                    <% } %>
                    <p><%: Poke.Type.Description %></p>
                </div>
                <div class="d-flex flex-column gap-3 align-items-center">
                    <h3>DEBILIDAD</h3>
                    <% if (Poke.Weakness.Description == "Fuego") { %>
                        <i class="bi bi-fire text-danger fs-1"></i>
                    <% } else if (Poke.Weakness.Description == "Agua") { %>
                        <i class="bi bi-droplet-half text-primary fs-1"></i>
                    <% } else if (Poke.Weakness.Description == "Planta") { %>
                        <i class="bi bi-feather text-success fs-1"></i>
                    <% } %>
                    <p><%: Poke.Weakness.Description %></p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
