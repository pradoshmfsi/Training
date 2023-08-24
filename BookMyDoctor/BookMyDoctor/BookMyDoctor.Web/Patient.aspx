<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="BookMyDoctor.Web.Patient" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Patient</title>
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Patient.css")%>" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Navbar.css")%>" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    
</head>
<body>
    <form id="formPatient" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div class="container">
            <div class="form-heading">
                Book your appoinment!
            </div>
            <div class="doctor-list-container">
            </div>
        </div>
        <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/Patient.js")%>" type="module">
        </script>
    </form>
</body>
</html>
