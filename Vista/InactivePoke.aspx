<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="InactivePoke.aspx.cs" Inherits="Vista.InactivePoke" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - Inactivos</title>
    <!-- Estilos para la paginación -->
    <link rel="stylesheet" href="Content/CSS/pagination.css" />
    <!-- Estilos para los íconos -->
    <link rel="stylesheet" href="Content/CSS/icons.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% if (Request.QueryString["alert"] != null) { %>
        <% if ((string)Request.QueryString["alert"] == "success") { %>
            <asp:Panel ID="inactiveAlert" runat="server"
                       CssClass="alert alert-success my-4">
                <h2 class="alert-heading fs-2">¡Éxito!</h2>
                <p><%:(string)Session["AlertMessage"] %></p>
            </asp:Panel>
        <% } else if ((string)Request.QueryString["alert"] == "error") { %>

        <% } %>
    <% } %>

    <div class="row my-4 align-items-center">
        <h1>Admin - Lista de Pokémon inactivos</h1>
    </div>
    
    <div class="row">
        <!-- Filtro -->
        <div class="col-6 d-flex gap-3">
            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control"
                         PlaceHolder="Buscar..." AutoPostBack="true"
                         OnTextChanged="txtFilter_TextChanged"></asp:TextBox>
            <asp:Button ID="btFind" runat="server" CssClass="d-none"/>
            <asp:Label ID="lbFind" runat="server" AssociatedControlID="btFind"
                       CssClass="btn btn-primary">
                <i class="bi bi-search fs-5"></i>
            </asp:Label>
        </div>
    </div>

    <!-- Alertas -->
    <div class="row">
        <div class="col">
            <!-- GridView vacío -->
            <asp:Panel ID="alertEmptyGV" runat="server" CssClass="alert alert-primary my-4" Visible="false">
                <h2 class="alert-heading">¡No se encontraron registros!</h2>
                <p>No se encontró ningún Pokémon inactivo en la base de datos.<p>
            </asp:Panel>

            <!-- Pokemon not found -->
            <asp:Panel ID="alertPokemonNotFound" runat="server" CssClass="alert alert-primary my-4" Visible="false">
                <h2 class="alert-heading">Pokémon inactivo no encontrado</h2>
                <p>No se ha encontrado ningún Pokémon inactivo con dicho criterio de búsqueda.<p>
            </asp:Panel>
        </div>
    </div>

    <div class="row">
        <!-- Tabla -->
         <asp:GridView ID="gvInactivePokemons"
                           runat="server"
                           CssClass="col table table-bordered my-4" 
                           AllowPaging="true" PageSize="5" OnPageIndexChanging="gvInactivePokemons_PageIndexChanging"
                           HeaderStyle-CssClass="table-dark text-center"
                           RowStyle-CssClass="table-light align-middle"
                           AutoGenerateColumns="false"
                           DataKeyNames="ID"
                           OnSelectedIndexChanged="gvInactivePokemons_SelectedIndexChanged"
                           OnRowDeleting="gvInactivePokemons_RowDeleting">
            <Columns>
                <asp:BoundField HeaderText="Número" DataField="Number" ItemStyle-CssClass="text-center"/>
                <asp:BoundField HeaderText="Nombre" DataField="Name"/>
                <asp:BoundField HeaderText="Tipo" DataField="Type.Description" ItemStyle-CssClass="text-center" />
                <asp:BoundField HeaderText="Debilidad" DataField="Weakness.Description" ItemStyle-CssClass="text-center" />
                <asp:CommandField HeaderText="Reactivar" SelectText="" ShowSelectButton="true"
                                  ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-lightbulb text-success fs-4 w-50"/>
                <asp:CommandField HeaderText="Eliminar" DeleteText="" ShowDeleteButton="true"
                                  ItemStyle-CssClass="text-center" ControlStyle-CssClass="bi bi-trash-fill text-danger fs-4 w-50"/>
            </Columns>
            <PagerSettings Mode="NumericFirstLast" />
            <PagerStyle HorizontalAlign="Center" />
        </asp:GridView>
    </div>

    <!-- Modal reactivar -->
    <!-- Trigger -->
    <button id="btnReactivePokemon" type="button" style="display:none;" data-bs-toggle="modal" data-bs-target="#reactiveModal"></button>

    <div class="modal fade" id="reactiveModal" tabindex="-1" aria-labelledby="reactiveModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-success" style="border-bottom: none;">
                    <h4 class="modal-title text-white" id="deleteModalLabel">Confirmar reactivación</h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>
                        ¿Desea reactivar el Pokemon <asp:Label ID="lbReactivePokemon" CssClass="fw-bold" runat="server"></asp:Label> en la base de datos?
                    </p>
                </div>
                <div class="modal-footer" style="border-top: none;">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnReactiveConfirm" runat="server" CssClass="btn btn-success" Text="Reactivar" OnClick="btnReactiveConfirm_Click"/>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Eliminar -->
    <!-- Primer trigger -->
    <a id="btnDeletePokemon" style="display:none" data-bs-toggle="modal" href="#firstAdvertise" role="button">PRUEBA</a>
    <!-- Primer aviso -->
    <div class="modal fade" id="firstAdvertise" aria-hidden="true" aria-labelledby="firstAdvertiseLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-warning">
                    <h5 class="modal-title" id="exampleModalToggleLabel">
                        ¡ADVERTENCIA!
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Si sigue con el procedimiento, borrará a <asp:Label ID="lbDeletePokemon" runat="server" CssClass="fw-bold"></asp:Label>
                    de forma permanente de la base de datos. ¿Realmente desea continuar con la eliminación?
                </div>
                <div class="modal-footer" style="border-top: none;">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <!-- Segundo trigger -->
                    <a class="btn btn-warning" data-bs-target="#secondAdvertise" data-bs-toggle="modal" data-bs-dismiss="modal">Continuar</a>
                </div>
            </div>
        </div>
    </div>
    <!-- Segundo aviso -->
    <div class="modal fade" id="secondAdvertise" aria-hidden="true" aria-labelledby="secondAdvertiseLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-danger">
                    <h5 class="modal-title text-white fs-3" id="secondAdvertiseLabel">¡ÚLTIMO AVISO!</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    Este es el último aviso de advertencia. ¿Está seguro que quiere eliminar el Pokemon seleccionado? 
                    <strong>No se podrá recuperar el registro una vez eliminado</strong>.
                </div>
                <div class="modal-footer" style="border-top: none;">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnDeleteConfirm" runat="server" Text="Confirmar eliminanación" CssClass="btn btn-danger" OnClick="btnDeleteConfirm_Click"/>
                </div>
            </div>
        </div>
    </div>

    <script src="Content/JS/lightbulb.js"></script>
</asp:Content>
