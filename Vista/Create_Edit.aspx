<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Create_Edit.aspx.cs" Inherits="Vista.Create_Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% if (Request.QueryString["id"] != null) { %>
            <title>Modificar un Pokemon</title>
    <% } else { %>
            <title>Agregar nuevo Pokemon</title>
    <% } %> 
    <link rel="stylesheet" href="Content/CSS/DropDownList.css"/>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <!-- Alerta de error -->
            <asp:Panel ID="errorAlert" runat="server" CssClass="alert alert-danger my-4" Visible="false">
                <h2 class="alert-heading fs-2">¡Ups!</h2>
                <asp:Label ID="errorText" runat="server" Text=""></asp:Label>
            </asp:Panel>

            <div class="row my-4">
                <div class="col-6 d-flex flex-column gap-3 justify-content-between">
                    <!-- Nombre -->
                    <div>
                        <asp:Label runat="server" CssClass="form-label fw-medium" Text="Nombre:" AssociatedControlID="txbxName"></asp:Label>
                        <asp:TextBox ID="txbxName" runat="server" CssClass="form-control"
                                     Required="true" placeholder="Ingrese el nombre..."
                                     AutoPostBack="true" OnTextChanged="VerifyInformation"></asp:TextBox>
                    </div>

                    <!-- Numero -->
                    <div>
                        <asp:Label runat="server" CssClass="form-label fw-medium" Text="Número:" AssociatedControlID="txbxNumber"></asp:Label>
                        <asp:TextBox ID="txbxNumber" runat="server" TextMode="Number"
                                     CssClass="form-control" Required="true" Min="1"
                                     AutoPostBack="true" OnTextChanged="VerifyInformation"></asp:TextBox>
                    </div>

                    <!-- Descripción -->
                    <div>
                        <asp:Label runat="server" CssClass="form-label fw-medium" Text="Descripción:" AssociatedControlID="txbxDescription"></asp:Label>
                        <asp:TextBox ID="txbxDescription" runat="server" TextMode="MultiLine"
                                     Rows="8" CssClass="form-control" style="resize:none;"
                                     Required="true" OnTextChanged="VerifyInformation" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>

                <div class="col-6 d-flex flex-column gap-3">
                    <div class="d-flex justify-content-between gap-3">
                        <!-- Tipo -->
                        <div class="w-50">
                            <asp:Label runat="server" CssClass="form-label fw-medium" Text="Tipo:" AssociatedControlID="ddlType"></asp:Label>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-select"
                                              OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                              AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!-- Debilidad -->
                        <div class="w-50">
                            <asp:Label runat="server" CssClass="form-label fw-medium" Text="Debilidad:" AssociatedControlID="ddlWeakness"></asp:Label>
                            <asp:DropDownList ID="ddlWeakness" runat="server" CssClass="form-select" Enabled="false" Required="true"></asp:DropDownList>
                        </div>
                    </div>

                    <!-- URL Imagen -->
                    <div>
                        <asp:Label runat="server" CssClass="form-label fw-medium" Text="URL Imagen" AssociatedControlID="txbxUrl"></asp:Label>
                        <asp:TextBox ID="txbxUrl" runat="server" CssClass="form-control"
                                     required="true" placeholder="Inserte una URL de imagen..."
                                     AutoPostBack="true" OnTextChanged="txbxUrl_TextChanged"></asp:TextBox>
                    </div>

                    <!-- Imagen -->
                    <div class="d-flex justify-content-center mt-auto">
                        <figure style="max-width:220px; margin:0">
                            <asp:Image ID="imgPokemon" runat="server" ImageUrl="https://placehold.co/475.png?text=Pokemon" CssClass="w-100"/>
                        </figure>
                    </div>
                </div>
            </div>

            <!-- Botones -->
            <div class="row mt-5">
                <div class="col-6"></div>
                <div class="col-6 d-flex justify-content-between gap-3">
                    <% if (Request.QueryString["id"] != null) { %>
                        <asp:Button ID="btnModify" runat="server" Text="MODIFICAR" CssClass="btn btn-primary w-50" OnClick="btnModify_Click" />
                    <% } else { %>
                        <asp:Button ID="btnAdd" runat="server" Text="AGREGAR" CssClass="btn btn-primary w-50" OnClick="btnAdd_Click"/>
                    <% } %>
                    <a href="Admin.aspx" class="btn btn-danger w-50">CANCELAR</a>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
