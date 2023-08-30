<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Initialize.aspx.cs" Inherits="BookMyDoctor.Web.Initialize" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Initialize - BookMyDoctor</title>
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Navbar.css")%>" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="formInitData" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div>
            <p>
                <button id="btnInitData">Initialize Data</button>
            </p>
        </div>
    </form>
    <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/Initialize.js")%>" type="module"></script>
</body>
</html>
