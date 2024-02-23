<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Vista.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Ingresar</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-3">
        <div class="col-5">
            <% if (Request.QueryString["alert"] != null) { %>
                <% if (Request.QueryString["alert"] == "error") { %>
                    <asp:Panel ID="loginErrorAlert" runat="server" CssClass="alert alert-danger my-1">
                        <h2 class="alert-heading fs-2">Error</h2>
                        <p><%:(string)Session["AlertMessage"] %></p>
                    </asp:Panel>
                <% } else if (Request.QueryString["alert"] == "success") { %>
                    <asp:Panel ID="loginSuccessAlert" runat="server" CssClass="alert alert-success my-1">
                        <h2 class="alert-heading fs-2">¡Éxito!</h2>
                        <p><%:(string)Session["AlertMessage"] %></p>
                    </asp:Panel>
                <% } %>
            <% } %>
            <h1 class="text-center">Ingresar a su cuenta</h1>
        </div>
    </div>

    <div class="row justify-content-center">
        <div class="col-5">
            <asp:Panel ID="panelErrorAlert" runat="server" CssClass="alert alert-danger my-4" Visible="false">
                <h2 class="alert-heading fs-2">Error</h2>
                <p>Datos de usuario incorrectos. Intente una vez más.</p>
            </asp:Panel>
        </div>
    </div>

    <div class="row my-4 justify-content-center">
        <div class="col-5 d-flex flex-column gap-3">
            <!-- Email -->
            <div>
                <asp:Label ID="lbEmail" runat="server" CssClass="form-label fw-medium"
                           Text="Correo electrónico:" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                             Required="true" MaxLength="50"></asp:TextBox>
            </div>
            <!-- Password -->
            <div>
                <asp:Label ID="lbPass" runat="server" CssClass="form-label fw-medium"
                           Text="Contraseña:" AssociatedControlID="txtPass"></asp:Label>
                <asp:TextBox ID="txtPass" runat="server" TextMode="Password"
                             CssClass="form-control" Required="true" MaxLength="50"></asp:TextBox>
            </div>
            <div>
                <a href="/Pages/Auth/ForgottenPass.aspx"
                   class="link-primary link-underline-opacity-0"
                   tabindex="-1">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
    </div>

    <div class="row my-3 justify-content-end">
        <div class="col-5">
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-danger w-25"
                        Text="Ingresar" OnClick="btnLogin_Click" />
        </div>
    </div>
</asp:Content>
