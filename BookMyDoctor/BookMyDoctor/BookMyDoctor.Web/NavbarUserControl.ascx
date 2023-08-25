<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavbarUserControl.ascx.cs" Inherits="BookMyDoctor.Web.NavbarUserControl" %>
<script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/Utils.js")%>"></script> 
<div class="navbar" id="patientNav" runat="server">
    <div class="menu" icon="bars"><i class="fa-solid fa-bars"></i></div>
    <a id="navHome" href="Patient.aspx" class="nav-item">HOME</a>
    <a id="navLogin" href="Login.aspx" class="nav-item">LOGIN</a>
</div>

<div class="navbar" id="doctorNav" runat="server">
    <div class="menu" icon="bars"><i class="fa-solid fa-bars"></i></div>
    <a id="navHome" href="Patient.aspx" class="nav-item">HOME</a>
    <a id="navAppointment" href="DoctorAppointments.aspx" class="nav-item">APPOINTMENTS</a>
    <a id="navReport" href="Report.aspx" class="nav-item">REPORTS</a>

    <div id="divLogoutContainer">
        <asp:Label ID="lblDoctorName" runat="server"></asp:Label>
        <button runat="server" id="btnRun" onserverclick="LogoutUser" class="btn btn-mini" title="Logout">
            <i class="fa-solid fa-arrow-right-from-bracket"></i>
        </button>
    </div>
</div>
<div id="loading">
    <img src="Assets/ZZ5H.gif" />
</div>

