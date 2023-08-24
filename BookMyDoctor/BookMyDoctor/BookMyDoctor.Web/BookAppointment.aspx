<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookAppointment.aspx.cs" Inherits="BookMyDoctor.Web.BookAppointment" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book appointment</title>
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Login.css")%>" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Navbar.css")%>" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
</head>
<body>
    <form id="formAppoint" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div class="appoint-container">
            <div class="form-container">
                <div class="login-container">
                    <div class="form-heading">Fill the details</div>
                    <div id="lblError"></div>
                    <div class="input-container">
                        <div class="title" id="divNameTitle">Name<span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="text"
                                id="txtName"
                                isrequired="Id:#txtName|TitleId:#divNameTitle span|Regex:[A-Za-z]+"
                                name="PatientName" />
                        </div>
                    </div>
                    <div class="input-container">
                        <div class="title" id="divEmailTitle">Email<span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="text"
                                id="txtEmail"
                                isrequired="Id:#txtEmail|TitleId:#divEmailTitle span|Regex:[a-z0-9]+@[a-z]+\.[a-z]{2,3}"
                                name="PatientEmail" />
                        </div>
                    </div>
                    <div class="input-container">
                        <div class="title" id="divPhoneTitle">Phone<span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="text"
                                id="txtPhone"
                                isrequired="Id:#txtPhone|TitleId:#divPhoneTitle span|Regex:[0-9]{10}"
                                name="PatientPhone" />
                        </div>
                    </div>
                    <div class="input-container">
                        <div class="title" id="divAppointDateTitle">Appointment Date <span>*</span></div>
                        <div class="input-element">
                            <input
                                class="txt"
                                type="date"
                                id="dateAppointDate"
                                name="AppointmentDate"
                                isrequired="Id:#dateAppointDate|TitleId:#divAppointDateTitle span|Regex:." />
                        </div>
                    </div>
                    <br />
                    <div class="submit">
                        <input type="submit" id="btnAppoint"
                            class="btn-submit"
                            value="Submit" />
                    </div>
                </div>
            </div>
            <div class="slot-form-container">
                <div class="title" id="divSlotTitle">Select slot<span>*</span></div>
                <div class="slot-list-container">
                </div>
            </div>

        </div>
        <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/BookAppointment.js")%>" type="module"></script>
    </form>
</body>
</html>
