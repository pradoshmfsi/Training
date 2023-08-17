<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LogoutUserControl.ascx.cs" Inherits="ManageUser.LogoutUserControl" %>
<div class="navbar">
    <span id="LoggedUser" runat="server"></span>
    <asp:Button ID="LogoutButton"
        OnClick="LogoutBtn"
        Text="LOGOUT"
        runat="server" />
</div>