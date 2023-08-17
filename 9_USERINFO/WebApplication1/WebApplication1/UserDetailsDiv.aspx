<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDetailsDiv.aspx.cs" Inherits="WebApplication1.UserDetailsDiv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <link rel="stylesheet" href="Content/userDetailsDiv.css" />
</head>
<body>

    <form id="formUserDetails" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div class="form-heading-element">
            <div class="form-heading-text">User Details</div>
            <div class="form-heading-graphic"></div>
        </div>
        <div class="submit">
            <asp:Button ID="addForm" Text="ADD USER" runat="server" OnClick="AddUserBtn" />
            <br />
        </div>
        <div id="userDetailsDiv">
            <div class="row" id="header">
                <div>USER ID</div>
                <div>FIRST NAME</div>
                <div>LAST NAME</div>
                <div>EMAIL</div>
                <div>ROLES</div>
                <div>HOBBIES</div>
                <div>EDIT</div>
            </div>
        </div>
    </form>
    <script src="Scripts/userDetailsDiv.js"></script>
</body>
</html>
