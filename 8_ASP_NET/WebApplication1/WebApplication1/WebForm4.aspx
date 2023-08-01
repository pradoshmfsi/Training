<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="WebApplication1.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        User Name:-<asp:textbox id="TextBox1" runat="server"></asp:textbox>  
        <br />  
        Password  :-<asp:textbox id="TextBox2" runat="server"></asp:textbox>  
        <br /> 
        <asp:button id="Button1" runat="server" onclick="Button1_Click" text="Submit" /> 
        
    </form>
</body>
</html>
