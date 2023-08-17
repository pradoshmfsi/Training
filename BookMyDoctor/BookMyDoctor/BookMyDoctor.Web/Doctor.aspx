<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="BookMyDoctor.Web.Doctor" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="Content/Doctor.css" />
    <link rel="stylesheet" href="Content/Navbar.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <title></title>
</head>
<body>
    <form id="formDoctor" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div class="container">
            <div class="form-heading">
                Appointment List
            </div>
            <div class="appointment-list-container">
                <div class="row">
                    <div class="table-header">Name</div>
                    <div class="table-header">Email</div>
                    <div class="table-header">Phone</div>
                    <div class="table-header">Date</div>
                    <div class="table-header">Time</div>
                    <div class="table-header">Status</div>
                </div>
                <div class="row">
                    <div class="table-element">Pradosh</div>
                    <div class="table-element">pradosh@gmail.com</div>
                    <div class="table-element">7008071464</div>
                    <div class="table-element">19-06-2002</div>
                    <div class="table-element">1:00 PM</div>
                    <div class="table-element">OPEN</div>
                </div>
            </div>
        </div>        
    </form>
</body>
</html>
