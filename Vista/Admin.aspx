<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vista.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - PokeApp</title>
    <!-- Estilos para la paginación -->
    <link rel="stylesheet" href="Content/CSS/pagination.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4 align-items-center">
        <div class="col-6">
            <h1>Admin - Lista de Pokémon</h1>
        </div>
    </div>

    <% if (Request.QueryString["alert"] != null) { %>
        <% if ((string)Request.QueryString["alert"] == "success") { %>
            <asp:Panel ID="adminAlert" runat="server" CssClass="alert alert-success my-4">
                <h2 class="alert-heading fs-2">¡Éxito!</h2>
                <p><%:(string)Session["AlertMessage"] %></p>
            </asp:Panel>
        <% } else if ((string)Request.QueryString["alert"] == "error") { %>

        <% } %>
    <% } %>
    
    <div class="row">
        <div class="col-6 d-flex gap-3">
            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" PlaceHolder="Buscar..."></asp:TextBox>
            <asp:Button ID="btFind" runat="server" CssClass="d-none"/>
            <asp:Label ID="lbFind" runat="server" AssociatedControlID="btFind" CssClass="btn btn-primary">
                <i class="bi bi-search"></i>
            </asp:Label>
        </div>

        <div class="col-6 d-flex justify-content-end">
            <a href="Create_Edit.aspx" class="btn btn-danger fs-5">Agregar nuevo Pokemon</a>
        </div>
    </div>

    <div class="row">
         <asp:GridView ID="gvPokemons"
                           runat="server"
                           CssClass="col table table-bordered my-4" 
                           AllowPaging="true" PageSize="5" OnPageIndexChanging="gvPokemons_PageIndexChanging"
                           HeaderStyle-CssClass="table-dark text-center"
                           RowStyle-CssClass="table-light align-middle"
                           AutoGenerateColumns="false"
                           DataKeyNames="ID"
                           OnSelectedIndexChanged="gvPokemons_SelectedIndexChanged" 
                           OnRowDeleting="gvPokemons_RowDeleting">
            <Columns>
                <asp:BoundField HeaderText="Número" DataField="Number" ItemStyle-CssClass="text-center"/>
                <asp:BoundField HeaderText="Nombre" DataField="Name"/>
                <asp:BoundField HeaderText="Tipo" DataField="Type.Description" ItemStyle-CssClass="text-center" />
                <asp:BoundField HeaderText="Debilidad" DataField="Weakness.Description" ItemStyle-CssClass="text-center" />
                <asp:CommandField HeaderText="Editar" SelectText="" ShowSelectButton="true" ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-pencil-square text-danger fs-4"/>
                <asp:CommandField HeaderText="Eliminar" DeleteText="" ShowDeleteButton="true" ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-trash-fill text-danger fs-4"/>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>

    <!-- Trigger -->
    <button id="btnDeletePokemon" type="button" style="display:none;" data-bs-toggle="modal" data-bs-target="#deleteModal"></button>

    <!-- Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: none;">
                    <h4 class="modal-title" id="deleteModalLabel">Confirmar eliminación</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>
                        ¿Seguro que desea eliminar al Pokemon <asp:Label ID="lbDeletePokemon" CssClass="fw-bold" runat="server"></asp:Label>?
                    </p>
                </div>
                <div class="modal-footer" style="border-top: none;">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnDeleteConfirm" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnDeleteConfirm_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
