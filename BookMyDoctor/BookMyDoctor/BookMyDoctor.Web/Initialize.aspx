<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Initialize.aspx.cs" Inherits="BookMyDoctor.Web.Initialize" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Initialize</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="formInitData" runat="server">
        <div>
            <p>
                <button id="btnInitData">Initialize Data</button>
            </p>
        </div>
    </form>
    <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/Initialize.js")%>" type="module"></script>
</body>
</html>
