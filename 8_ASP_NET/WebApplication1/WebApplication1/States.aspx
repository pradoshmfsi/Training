<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="States.aspx.cs" Inherits="WebApplication1.States" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h3>States of the given country are</h3>
        <asp:GridView ID="GridViewStates" EmptyDataText="No data found" runat="server" >

        </asp:GridView>
    </p>

</asp:Content>
