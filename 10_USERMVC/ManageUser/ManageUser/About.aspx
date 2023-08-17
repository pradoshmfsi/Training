<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ManageUser.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
                            <asp:TextBox runat="server" ID="dasdasd" AutoPostBack="true" OnTextChanged="dasdasd_TextChanged"></asp:TextBox>
        <asp:Button Text="asdsad" runat="server" OnClick="Unnamed1_Click" />
    </main>
</asp:Content>
