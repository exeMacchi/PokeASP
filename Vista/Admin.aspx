<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Vista.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Admin - PokeApp</title>
    <!-- Estilos para la paginación -->
    <link rel="stylesheet" href="Content/CSS/pagination.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4 align-items-center">
        <div class="col-6">
            <h1>Admin - Lista de pokemons</h1>
        </div>
    </div>
    
    <div class="row">
        <div class="col-6 d-flex gap-3">
            <asp:TextBox ID="txtFilter" runat="server" CssClass="form-control" PlaceHolder="Buscar..."></asp:TextBox>
            <asp:Button ID="btFind" runat="server" CssClass="d-none"/>
            <asp:Label ID="lbFind" runat="server" AssociatedControlID="btFind" CssClass="btn btn-primary">
                <i class="bi bi-search"></i>
            </asp:Label>
        </div>

        <div class="col-6 d-flex justify-content-end">
            <asp:Button ID="btAdd" runat="server" CssClass="btn btn-danger fs-5" Text="Agregar nuevo pokemon"/>
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
                           DataKeyNames="ID">
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
</asp:Content>
