<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div id="form" runat="server">  
        <div>  
            <asp:Label ID="labelId" runat="server">User Name</asp:Label>  
        <asp:TextBox ID="UserName" runat="server" ToolTip="Enter User Name"></asp:TextBox>  
        </div>  
        <p>  
        <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" />  
        </p>  
        <br />        
            <asp:HyperLink ID="HyperLink1" runat="server" Text="JavaTpoint" NavigateUrl="https://www.javatpoint.com"></asp:HyperLink>  
    </div>  
     <asp:Label ID="userInput" runat="server"></asp:Label> 

</asp:Content>
