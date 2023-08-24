<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorAppointments.aspx.cs" Inherits="BookMyDoctor.Web.DoctorAppointments" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Doctor.css")%>" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Navbar.css")%>" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw=="
        crossorigin="anonymous"
        referrerpolicy="no-referrer" />
    <title>Dcotor Appointments</title>
</head>
<body>
    <form id="formDoctor" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div class="container">
            <div class="form-heading">
                Appointment List
            </div>
            <div class="filter-date">
                <input
                    class="txt"
                    type="date"
                    id="dateFilterAppointment"
                    />
            </div>
            <div class="appointment-list-container">
                <div class="row header">
                    <div class="table-element">Name</div>
                    <div class="table-element">Email</div>
                    <div class="table-element">Phone</div>
                    <div class="table-element">Date</div>
                    <div class="table-element">Time</div>
                    <div class="table-element">Status</div>
                    <div class="table-element"></div>
                </div>
                <div class="row-group">
                    
                </div>            
            </div>
            <div class="report-error">No data found!</div>
        </div>
        <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/DoctorAppointments.js")%>" type="module"></script>
    </form>
</body>
</html>
