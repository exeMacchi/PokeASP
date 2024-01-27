<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Vista.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PokeApp</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="PokeCarousel" runat="server">
    <div class="navbar-nav justify-content-end bg-danger">
        <div id="Carousel" class="col-2 carousel slide" data-bs-ride="carousel">
            <div class="carousel-inner">
                <% foreach (Dominio.Pokemon poke in Pokemons) { %>
                <%    if (poke.ID <= 10) { %>
                         <div class="carousel-item <%= poke.ID == 1 ? "active" : "" %>">
                             <img src="<%: poke.UrlImage %>" class="d-block w-100" alt="Imagen de un pokemon">
                         </div>
                <%    } %>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4">
        <h1 class="text-center">PokeApp</h1>
    </div>

    <div class="row justify-content-between gap-3">
            <asp:Repeater ID="PokeCards" runat="server">
                <ItemTemplate>
                    <div class="card" style="width: 18rem;">
                        <img src="<%# Eval("UrlImage") %>" class="card-img-top" alt="Imagen del pokemon">
                        <div class="card-body">
                            <h3 class="card-title"><%# Eval("Name") %></h3>
                            <p class="card-text"><%# Eval("Description") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
</asp:Content>
