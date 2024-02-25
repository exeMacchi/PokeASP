<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Vista.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registrarse</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-8">
            <h1 class="text-center">Convertirse en entrenador Pokémon</h1>
            <p class="text-center">
                ¿Deseas ser un entrenador Pokémon? Regístrate ahora y únete a millones de 
                entrenadores de todo el mundo.
            </p>
        </div>
    </div>

    <!-- Alerta error pass -->
    <div class="row justify-content-center">
        <div class="col-6">
            <asp:Panel ID="registerErrorAlert" runat="server" CssClass="alert alert-danger" Visible="false">
                <h2 class="alert-heading fs-3">¡Error!</h2>
                <p>No pueden haber campos vacíos.</p>
            </asp:Panel>
        </div>
    </div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" >
        <ContentTemplate>
            <!-- Nick -->
            <div class="row justify-content-center my-4">
                <div class="col-6">
                    <asp:Label ID="lbNick" runat="server" CssClass="form-label fw-medium" AssociatedControlID="txtNick">
                        Apodo <span class="text-danger fw-bold">*</span>
                    </asp:Label>
                    <asp:TextBox ID="txtNick" runat="server" CssClass="form-control" MaxLength="50"
                                 AutoPostBack="true" OnTextChanged="txtNick_TextChanged" Required="true"
                                 ClientIDMode="Static">
                    </asp:TextBox>
                    <asp:Label ID="lbNickAlert" runat="server" CssClass="text-danger" Visible="false">
                        Apodo no disponible.
                    </asp:Label>
                </div>
            </div>

            <!-- Email -->
            <div class="row justify-content-center my-4">
                <div class="col-6">
                    <asp:Label ID="lbEmail" runat="server" CssClass="form-label fw-medium" AssociatedControlID="txtEmail">
                        Correo electrónico <span class="text-danger fw-bold">*</span>
                    </asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"
                                 MaxLength="50" AutoPostBack="true" OnTextChanged="txtEmail_TextChanged"
                                 Required="true" ClientIDMode="Static"></asp:TextBox>
                    <asp:Label ID="lbEmailAlert" runat="server" CssClass="text-danger" Visible="false">
                        Correo electrónico ya utilizado.
                    </asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <!-- Contraseña -->
            <div class="row justify-content-center my-4">
                <div class="col-6">
                    <asp:Label ID="lbFirstPass" runat="server" AssociatedControlID="txtFirstPass"
                               CssClass="form-label fw-medium">
                            Contraseña <span class="text-danger fw-bold">*</span>
                    </asp:Label>

                    <div class="d-flex align-items-center gap-3">
                        <asp:TextBox ID="txtFirstPass" runat="server" CssClass="form-control"
                                     TextMode="Password" ClientIDMode="Static" MaxLength="50"
                                     Required="true" oninput="checkPasswordsMatch()"></asp:TextBox>
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
                                     Required="true" oninput="checkPasswordsMatch()"></asp:TextBox>
                        <button id="btnSecondPass"
                                class="border border-1 p-1 px-2 rounded-1 bg-light"
                                type="button"  
                                tabindex="-1"
                                onclick="togglePass('txtSecondPass', 'btnSecondPass')">
                            <i class="bi bi-eye-slash"></i>
                        </button>
                    </div>

                    <asp:Label ID="lbPassAlert" runat="server" CssClass="text-danger" ClientIDMode="Static" style="display:none;">
                        Las contraseñas no coinciden.
                    </asp:Label>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- Términos y condiciones -->
    <div class="row justify-content-center my-4">
        <div class="col-6">
            <input type="checkbox" class="form-check-input" required />
            <span class="ms-1">Acepto los <a href="#" class="link-primary link-underline-opacity-0" tabindex="-1">términos y condiciones</a></span>
        </div>
    </div>

    <!-- Submit -->
    <div class="row justify-content-center my-4">
        <div class="col-6 d-flex justify-content-end">
            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-danger w-50"
                        Text="Registrarse" ClientIDMode="Static" OnClick="btnSubmit_Click"/>
        </div>
    </div>

    <script src="/Content/JS/togglePassword.js"></script>
    <script src="/Content/JS/register.js"></script>
</asp:Content>
