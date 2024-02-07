<%@ Page Title="" Language="C#" MasterPageFile="~/MyMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Vista.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PokeApp</title>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row my-4">
        <h1 class="text-center">PokeApp</h1>
    </div>

    <div class="row justify-content-between gap-3">
            <asp:Repeater ID="PokeCards" runat="server">
                <ItemTemplate>
                    <div class="card" style="width: 18rem;">
                        <img src="<%# Eval("UrlImage") %>" class="card-img-top" alt="Imagen del pokemon">
                        <div class="card-body">
                            <h3 class="card-title"><%# Eval("Name") %></h3>
                            <p class="card-text"><%# Eval("Description") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
</asp:Content>
