<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="ForgottenPass.aspx.cs" Inherits="Vista.Pages.Auth.ForgottenPass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <% if (Request.QueryString["id"] != null) { %>
        <title>Cambiar contraseña</title>
    <% } else { %>
        <title>Recuperar contraseña</title>
    <% } %> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <% // CAMBIAR CONTRASEÑA %>
    <% if (Request.QueryString["id"] != null) { %>
        <div class="row mt-3">
            <h1 class="text-center">Cambiar contraseña</h1>
            <p class="text-center">Por favor, ingrese su nueva contraseña y confírmela.</p>
         </div>

        <!-- Alerta error pass -->
        <div class="row justify-content-center">
            <div class="col-6">
                <asp:Panel ID="passErrorAlert" runat="server" CssClass="alert alert-danger" Visible="false">
                    <h2 class="alert-heading fs-2">¡Error!</h2>
                    <p>
                        Las contraseñas introducidas no coinciden. Por favor, inténtelo de nuevo.
                    </p>
                </asp:Panel>
            </div>
        </div>

        <!-- Nueva contraseña -->
        <div class="row justify-content-center my-4">
            <div class="col-6">
                <asp:Label ID="lbFirstPass" runat="server" AssociatedControlID="txtFirstPass"
                    CssClass="form-label fw-medium">
                        Nueva contraseña <span class="text-danger">*</span>
                </asp:Label>

                <div class="d-flex align-items-center gap-3">
                    <asp:TextBox ID="txtFirstPass" runat="server" CssClass="form-control"
                                 TextMode="Password" ClientIDMode="Static" MaxLength="50"
                                 Required="true"></asp:TextBox>
                    <button id="btnFirstPass"
                            class="border border-1 p-1 px-2 rounded-1 bg-light"
                            type="button" 
                            tabindex="-1"
                            onclick="togglePass('txtFirstPass', 'btnFirstPass')">
                        <i class="bi bi-eye-slash"></i>
                    </button>
                </div>
            </div>
        </div>

        <!-- Confirmar contraseña -->
        <div class="row justify-content-center my-4">
            <div class="col-6">
                <asp:Label ID="lbSecondPass" runat="server" AssociatedControlID="txtSecondPass"
                    CssClass="form-label fw-medium">
                        Confirmar contraseña <span class="text-danger">*</span>
                </asp:Label>

                <div class="d-flex align-items-center gap-3">
                    <asp:TextBox ID="txtSecondPass" runat="server" CssClass="form-control"
                                 TextMode="Password" ClientIDMode="Static" MaxLength="50"
                                 Required="true"></asp:TextBox>
                    <button id="btnSecondPass"
                            class="border border-1 p-1 px-2 rounded-1 bg-light"
                            type="button"  
                            tabindex="-1"
                            onclick="togglePass('txtSecondPass', 'btnSecondPass')">
                        <i class="bi bi-eye-slash"></i>
                    </button>
                </div>
            </div>
        </div>

        <!-- Confirmar -->
        <div class="row justify-content-center my-4">
            <div class="col-6 d-flex justify-content-end">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-danger w-50" 
                            Text="Confirmar" OnClick="btnSubmit_Click"/>
            </div>
        </div>

        <script src="/Content/JS/togglePassword.js"></script>

    <% // RECUPERAR CUENTA %>
    <% } else { %>
        <div class="row mt-3">
            <h1 class="text-center">Recuperar cuenta</h1>
            <p class="text-center">
                ¿Te olvidaste tu contraseña? ¡No te preocupes! Ingresá tu correo electrónico
                y recibí las instrucciones para recuperarla.
            </p>
         </div>

        <!-- Alerta correo enviado -->
        <div class="row justify-content-center">
            <div class="col-6">
                <asp:Panel ID="forgottenAlert" runat="server" CssClass="alert alert-success" Visible="false">
                    <h2 class="alert-heading fs-2">¡Correo electrónico enviado!</h2>
                    <p>
                        Le hemos enviado un correo electrónico con las instrucciones a seguir 
                        para cambiar su contraseña. Verifique su bandeja de entrada principal
                        o en la bandeja spam.
                    </p>
                </asp:Panel>
            </div>

        </div>
        <!-- Alerta error email DB -->
        <div class="row justify-content-center">
            <div class="col-6">
                <asp:Panel ID="emailErrorAlert" runat="server" CssClass="alert alert-danger" Visible="false">
                    <h2 class="alert-heading fs-2">¡Error!</h2>
                    <p>
                        El correo electrónico proporcionado no se encuentra en la base de datos.
                        Por favor, verifique que el correo electrónico ingresado sea correcto.
                    </p>
                </asp:Panel>
            </div>
        </div>

        <div class="row justify-content-center my-4">
            <div class="col-6">
                <asp:Label ID="lbEmail" runat="server" AssociatedControlID="txtEmail"
                           CssClass="form-label fw-medium">
                    Email <span class="text-danger">*</span>
                </asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                             Required="true" placeholder="example@pokedex.com" MaxLength="50"></asp:TextBox>
            </div>
        </div>

        <div class="row justify-content-center my-4">
            <div class="col-6 d-flex justify-content-end">
                <asp:Button ID="btnSend" runat="server" Text="Enviar" 
                            CssClass="btn btn-danger w-50" OnClick="btnSend_Click" />
            </div>
        </div>

    <% } %> 
</asp:Content>
