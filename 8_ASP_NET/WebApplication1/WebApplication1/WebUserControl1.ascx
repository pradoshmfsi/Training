<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="WebApplication1.WebUserControl1" %>
<h1>Add message</h1>
<div>

    <asp:TextBox ID="MessageName" runat="server" placeholder="Enter Message" ToolTip="Enter Message" CssClass="form-control" Height="31px" Width="241px"></asp:TextBox>
</div>
<br />
    <asp:Button ID="AddMessageBtn" runat="server" OnClick="AddMessage" Text="Submit" CssClass="btn btn-primary" />
<br />
<br />
<asp:DataList ID="DataList2" runat="server" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="Both" Height="197px" Width="541px">
    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
    <ItemStyle BackColor="White" ForeColor="#330099" />
    <ItemTemplate>
        Message:
                <asp:Label ID="countryNameLabel" runat="server" Text='<%# Eval("objMessage") %>' />
        <br />
    </ItemTemplate>
    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
</asp:DataList>