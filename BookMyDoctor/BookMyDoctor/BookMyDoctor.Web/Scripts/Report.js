$(document).ready(() => {

    menuResponsive();

    $("#navReport").addClass("selected-nav");

    var dp = $(".filter-month").datepicker({
        format: "yyyy-mm-dd",
        startView: "months",
        minViewMode: "months"
    });

    $("#hiddenTxtSelectedMonth").val(getTodaysDate());

    $(".report-list-container").each((index, item) => {
        fetchReports(item.id, $(item).attr("type"));
    })

    $("#divSummaryNav").on("click", () => {
        selectNav("#divSummaryNav", "#divSummaryContainer");
    })
    $("#divDetailedNav").on("click", () => {
        selectNav("#divDetailedNav", "#divDetailedContainer");
    })

    dp.on('changeMonth', function (e) {
        $("#hiddenTxtSelectedMonth").val(e.format());
        $(".report-list-container").each((index, item) => {
            fetchReports(item.id, $(item).attr("type"));
        })
    });

    $(".download-report").on("click", function () {
        window.location.href = $(this).attr("handler") + "&reportMonth=" + $("#hiddenTxtSelectedMonth").val();
    })
})
function selectNav(id, reportClass) {
    $("#divReportNav div").removeClass("selected-report-nav");
    $(".report-list-container").hide();
    $(reportClass).show();
    $(id).addClass("selected-report-nav");
}

async function fetchReports(id, type) {
    let reportResponse = await ajaxWebMethodCall({ url: 'Report.aspx/GetReportList', data: JSON.stringify({type:type, reportMonth: $("#hiddenTxtSelectedMonth").val() }) });
    if (reportResponse.IsSuccess) {
        $("#" + id + " .row-group").empty();
        if (reportResponse.Data.length == 0) {
            $(".report-error").show();
            return;
        }       
        $(".report-error").hide();
        if (id == "divSummaryContainer") {
            reportResponse.Data.forEach((report) => {
                $("#" + id + " .row-group").append(generateReportSummaryDiv(report));
            })
        }
        else {
            reportResponse.Data.forEach((report) => {
                $("#" + id + " .row-group").append(generateReportDetailedDiv(report));
            })
        }
    }
    else {
        alert(reportResponse.Data);
    }
}

function generateReportSummaryDiv(report) {
    return `<div class="row">
                <div class="table-element">
                    <div class="column-name">Date</div>
                    <div class="column-value">${report.Date}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Total</div>
                    <div class="column-value">${report.TotalAppointments}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Closed</div>
                    <div class="column-value">${report.ClosedAppointments}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Cancelled</div>
                    <div class="column-value">${report.CancelledAppointments}</div>
                </div>
           </div>`
}

function generateSpanDetailedRow(reportList) {
    let result = ""
    reportList.forEach((report) => {
        result += `<div class="row">
                <div class="table-element spanned">
                    <div class="column-name">Date</div>
                    <div class="column-value"></div>
                </div>
                <div class="table-element">
                    <div class="column-name">Patient Name</div>
                    <div class="column-value">${report.PatientName}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Status</div>
                    <div class="column-value">${report.AppointmentStatusUI}</div>
                </div>
           </div>`
    })
    return result;
}

function generateReportDetailedDiv(report) {

    return `<div class="row" id="generateRow">
                <div class="table-element">
                    <div class="column-name">Date</div>
                    <div class="column-value">${report.Date}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Patient Name</div>
                    <div class="column-value">${report.Appointments[0].PatientName}</div>
                </div>
                <div class="table-element">
                    <div class="column-name">Status</div>
                    <div class="column-value">${report.Appointments[0].AppointmentStatusUI}</div>
                </div>
           </div>`
        +
        generateSpanDetailedRow(report.Appointments.slice(1));
}
