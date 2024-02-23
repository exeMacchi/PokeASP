<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Vista.Pages.User.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Contacto</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Alerta -->
    <div class="row justify-content-center">
        <div class="col-6">
            <asp:Panel ID="contactAlert" runat="server" CssClass="alert alert-success mt-3" Visible="false">
                <h2 class="alert-heading fs-2">¡Gracias por comunicarse con nosotros!</h2>
                <p>¡El correo electrónico ha sido enviado de forma exitosa! Dentro de poco le responderemos.</p>
            </asp:Panel>
        </div>
    </div>

    <div class="row my-3">
        <h1 class="text-center">Formulario de contacto</h1>
    </div>

    <div class="row my-3 d-flex justify-content-center">
        <div class="col-6 d-flex flex-column gap-3">
            <!-- Nombre -->
            <div>
                <asp:Label ID="lbName" runat="server" Text="Nombre"
                           CssClass="form-label fw-medium" AssociatedControlID="txtName"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <!-- Asunto -->
            <div>
                <asp:Label ID="lbSubject" runat="server" CssClass="form-label fw-medium" 
                           AssociatedControlID="txtSubject">
                    Asunto <span class="text-danger">*</span>
                </asp:Label>
                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" 
                             Required="true"></asp:TextBox>
            </div>

            <!-- Email -->
            <div>
                <asp:Label ID="lbEmail" runat="server" CssClass="form-label fw-medium" 
                           AssociatedControlID="txtEmail">
                    Email <span class="text-danger">*</span>
                </asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"
                             Required="true"></asp:TextBox>
            </div>

            <!-- Mensaje -->
            <div>
                <asp:Label ID="lbMessage" runat="server" CssClass="form-label fw-medium" 
                           AssociatedControlID="txtEmail">
                    Mensaje <span class="text-danger">*</span>
                </asp:Label>
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control"
                             TextMode="MultiLine" Required="true" Rows="10" style="resize: none;"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="row my-3 justify-content-center">
        <div class="col-6 d-flex justify-content-end">
            <asp:Button ID="btnSend" runat="server" Text="Enviar"
                        CssClass="btn btn-danger w-25" OnClick="btnSend_Click" />
        </div>
    </div>
</asp:Content>
