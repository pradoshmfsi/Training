<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Initialize.aspx.cs" Inherits="BookMyDoctor.Web.Initialize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Initialize</title>
</head>
<body>
    <form id="formInitData" runat="server">
        <div>
        </div>
    <%--<script src="Scripts/Initialize.js"></script>--%>
        <p>
            <asp:Button ID="btnInitData" runat="server" Text="Initialize Data" OnClick="InitData" Height="61px" style="margin-left: 0px" Width="288px"/>
        </p>
    </form>
    </body>
</html>
