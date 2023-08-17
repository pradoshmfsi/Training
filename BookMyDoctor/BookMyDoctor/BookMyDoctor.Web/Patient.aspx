<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="BookMyDoctor.Web.Patient" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title></title>
    <link rel="stylesheet" href="Content/Patient.css" />
    <link rel="stylesheet" href="Content/Navbar.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="formPatient" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div>
            <div class="form-heading">
                Book your appoinment
            </div>
            <div class="doctor-list-container">
            </div>
        </div>
        <script src="Scripts/Patient.js"></script>
    </form>
</body>
</html>
