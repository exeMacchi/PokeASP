<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vista.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - PokeApp</title>
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
    <div class="row my-4 align-items-center">
        <div class="col-6">
            <h1>Admin - Lista de pokemons</h1>
        </div>
        <div class="col-6 d-flex justify-content-end">
            <button class="btn btn-danger fs-5">Agregar nuevo pokemon</button>
        </div>
    </div>
    <div class="row">
         <asp:GridView ID="gvPokemons" 
                       runat="server"
                       CssClass="col table table-bordered"
                       HeaderStyle-CssClass="table-dark text-center"
                       RowStyle-CssClass="table-light align-middle"
                       AutoGenerateColumns="false"
                       DataKeyNames="ID">
        <Columns>
            <asp:BoundField HeaderText="Número" DataField="Number"/>
            <asp:BoundField HeaderText="Nombre" DataField="Name"/>
            <asp:BoundField HeaderText="Descripción" DataField="Description" ItemStyle-CssClass="w-50"/>
            <asp:BoundField HeaderText="Tipo" DataField="Type.Description" ItemStyle-CssClass="text-center" />
            <asp:BoundField HeaderText="Debilidad" DataField="Weakness.Description" ItemStyle-CssClass="text-center" />
            <asp:CommandField HeaderText="Editar" SelectText="" ShowSelectButton="true" ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-pencil-square text-danger fs-4"/>
            <asp:CommandField HeaderText="Eliminar" DeleteText="" ShowDeleteButton="true" ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-trash-fill text-danger fs-4"/>
        </Columns>
    </asp:GridView>
    </div>
</asp:Content>
