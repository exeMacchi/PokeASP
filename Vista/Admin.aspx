<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vista.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - PokeApp</title>
    <!-- Estilos para la paginación -->
    <link rel="stylesheet" href="Content/CSS/pagination.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (Request.QueryString["alert"] != null) { %>
        <% if ((string)Request.QueryString["alert"] == "success") { %>
            <asp:Panel ID="adminAlert" runat="server" CssClass="alert alert-success my-4">
                <h2 class="alert-heading fs-2">¡Éxito!</h2>
                <p><%:(string)Session["AlertMessage"] %></p>
            </asp:Panel>
        <% } else if ((string)Request.QueryString["alert"] == "error") { %>

        <% } %>
    <% } %>

    <div class="row my-4 align-items-center">
        <div class="col-6">
            <h1>Admin - Lista de Pokémon</h1>
        </div>
    </div>
    
    <div class="row">
        <!-- Filtro -->
        <div class="col-6 d-flex gap-3">
            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control"
                         PlaceHolder="Buscar por nombre..."></asp:TextBox>

            <!-- Buscar -->
            <asp:Button ID="btnFind" runat="server" CssClass="d-none" OnClick="btnFind_Click"/>
            <asp:Label ID="lbFind" runat="server" AssociatedControlID="btnFind"
                       CssClass="btn btn-primary">
                <i class="bi bi-search fs-5"></i>
            </asp:Label>

            <!-- Avanzado -->
            <asp:Button ID="btnAdvanced" runat="server" CssClass="d-none" OnClick="btnAdvanced_Click"/>
            <asp:Label ID="lbAdvanced" runat="server" AssociatedControlID="btnAdvanced"
                       CssClass="btn btn-primary">
                <i class="bi bi-filter fs-5"></i>
            </asp:Label>
        </div>

        <!-- Enlaces -->
        <div class="col-6 d-flex justify-content-end gap-3">
            <asp:HyperLink ID="inactivePokemons" runat="server" NavigateUrl="~/InactivePoke.aspx"
                           CssClass="btn btn-warning fs-5 w-50" Visible="false">Lista Pokémon inactivos</asp:HyperLink>
            <a href="Create_Edit.aspx" class="btn btn-danger fs-5 w-50">Agregar nuevo Pokémon</a>
        </div>
    </div>

    <!-- Filtro avanzado -->
    <asp:Panel ID="advancedPanel" runat="server" CssClass="row my-3" Visible="false">
        <div class="col-6 d-flex justify-content-between gap-3">
            <asp:DropDownList ID="ddlFirstCriteria" runat="server" CssClass="form-select"
                              AutoPostBack="true" OnSelectedIndexChanged="ddlFirstCriteria_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlSecondCriteria" runat="server" CssClass="form-select"></asp:DropDownList>
        </div>
    </asp:Panel>

    <!-- Alertas -->
    <div class="row">
        <div class="col">
            <!-- GridView vacío -->
            <asp:Panel ID="alertEmptyGV" runat="server" CssClass="alert alert-primary my-4" Visible="false">
                <h2 class="alert-heading">¡Ups! parece que no hay ningún Pokémon</h2>
                <p>Añada algún Pokémon poder ver su información en un formato tabular.<p>
            </asp:Panel>

            <!-- Pokemon not found -->
            <asp:Panel ID="alertPokemonNotFound" runat="server" CssClass="alert alert-primary my-4" Visible="false">
                <h2 class="alert-heading">Pokémon no encontrado</h2>
                <p>No se ha encontrado ningún Pokémon con dicho criterio de búsqueda.<p>
            </asp:Panel>
        </div>
    </div>

    
    <div class="row">
        <!-- Tabla -->
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
                <asp:CommandField HeaderText="Editar" SelectText="" ShowSelectButton="true"
                                  ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-pencil-square text-danger fs-4 w-50"/>
                <asp:CommandField HeaderText="Eliminar" DeleteText="" ShowDeleteButton="true"
                                  ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-trash-fill text-danger fs-4 w-50"/>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>

    <!-- Modal de eliminación lógica -->
    <!-- Trigger -->
    <button id="btnDeletePokemon" type="button" style="display:none;" data-bs-toggle="modal" data-bs-target="#deleteModal"></button>
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger" style="border-bottom: none;">
                    <h4 class="modal-title text-white" id="deleteModalLabel">Confirmar eliminación</h4>
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
