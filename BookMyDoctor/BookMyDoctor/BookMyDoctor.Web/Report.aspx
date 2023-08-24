<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="BookMyDoctor.Web.Report" %>

<!DOCTYPE html>
<%@ Register Src="~/NavbarUserControl.ascx" TagPrefix="uc" TagName="Navbar" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Report.css")%>" />
    <link rel="stylesheet" href="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Content/Navbar.css")%>" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/css/datepicker.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script src="https://netdna.bootstrapcdn.com/bootstrap/2.3.2/js/bootstrap.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.2.0/js/bootstrap-datepicker.min.js"></script>
    <link
        rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css"
        integrity="sha512-iecdLmaskl7CVkqkXNQ/ZH/XLlvWZOJyj7Yy7tcenmpD1ypASozpmT/E0iPtmFIB46ZmdtAc9eNBvH0H/ZpiBw=="
        crossorigin="anonymous"
        referrerpolicy="no-referrer" />

    <title>Doctor Reports</title>
</head>
<body>
    <form id="formReport" runat="server">
        <uc:Navbar ID="navbarUserControl" runat="server" />
        <div class="container">
            <div class="doctor-info">
                <div class="filter-month">
                </div>
                <input type="hidden" id="hiddenTxtSelectedMonth" />
            </div>

            <div class="report-container">
                <div class="report-nav" id="divReportNav">
                    <div id="divSummaryNav" class="selected-report-nav">
                        <i class="fa-solid fa-list"></i>&nbsp;SUMMARY 
                    </div>
                    <div id="divDetailedNav">
                        <i class="fa-solid fa-magnifying-glass"></i>&nbsp;DETAILED
                    </div>
                </div>
                
                <div class="report-list-container" id="divSummaryContainer" type="Summary">                    
                    <div class="download-report" handler="ReportDownload.ashx?type=Summary">Download &nbsp;<i class="fa-solid fa-file-pdf"></i></div>
                    <div class="table">
                        <div class="row header">
                            <div class="table-element">Date</div>
                            <div class="table-element">Total</div>
                            <div class="table-element">Closed</div>
                            <div class="table-element">Cancelled</div>
                        </div>
                        <div class="row-group">
                        </div>
                    </div>

                </div>
                <div class="report-list-container" id="divDetailedContainer" type="Detailed">
                    <div class="download-report" handler="ReportDownload.ashx?type=Detailed">Download &nbsp;<i class="fa-solid fa-file-pdf"></i></div>
                    <div class="table">
                        <div class="row header">
                            <div class="table-element">Date</div>
                            <div class="table-element">Name</div>
                            <div class="table-element">Status</div>
                        </div>
                        <div class="row-group">
                        </div>
                    </div>
                </div>
                <div class="report-error">No data found!</div>
            </div>

        </div>
        <script src="<%= BookMyDoctor.Utils.Utilities.GetFilePathForHandler("Scripts/Report.js")%>" type="module"></script>
    </form>
</body>
</html>
