$(document).ready(() => {

    menuResponsive();

    $("#navAppointment").addClass("selected-nav");

    $("#dateFilterAppointment")[0].valueAsDate = new Date();
    fetchAppointments();

    $("#dateFilterAppointment").on("change", () => {
        fetchAppointments();
    })
    $(".row-group").on("click", ".Open .appointment-button", function () {
        closeOrCancelAppointment($(this).attr("action"));
    })
})
async function closeOrCancelAppointment(actionAttr) {
    let [appointId, appointmentStatus] = actionAttr.split("|");
    let response = await ajaxWebMethodCall({
        url: 'DoctorAppointments.aspx/CloseOrCancelAppointment', data: JSON.stringify({ appointmentStatus: appointmentStatus, appointmentId: Number(appointId) })
    });
    if (response.IsSuccess) {
        fetchAppointments();
    }
    else {
        alert(response.Data);
    }
}

async function fetchAppointments() {
    let response = await ajaxWebMethodCall({
        url: 'DoctorAppointments.aspx/GetAppointmentsList', data: JSON.stringify({ appointmentDate: $("#dateFilterAppointment").val() })
    });
    if (response.IsSuccess) {
        $(".row-group").empty();
        if (response.Data.length == 0) {
            $(".report-error").show();
            return;
        }
        $(".report-error").hide();
        response.Data.forEach((appointment) => {
            $(".row-group").append(generateAppointmentContainer(appointment))
        })
    }
    else {
        alert(response.Data);
    }
}


function generateAppointmentContainer(appointment) {
    return `<div class="row">
                        <div class="table-element">
                            <div class="column-name">Name</div>
                            <div class="column-value">${appointment.PatientName}</div>
                        </div>
                        <div class="table-element">
                            <div class="column-name">Email</div>
                            <div class="column-value">${appointment.PatientEmail}</div>
                        </div>
                        <div class="table-element">
                            <div class="column-name">Phone</div>
                            <div class="column-value">${appointment.PatientPhone}</div>
                        </div>
                        <div class="table-element">
                            <div class="column-name">Date</div>
                            <div class="column-value">${appointment.AppointmentDateUI}</div>
                        </div>
                        <div class="table-element">
                            <div class="column-name">Slot</div>
                            <div class="column-value">${appointment.AppointmentTimeUI}</div>
                        </div>
                        <div class="table-element">
                            <div class="column-name">Status</div>
                            <div class="column-value">${appointment.AppointmentStatusUI}</div>
                        </div>
                        <div class="table-element ${appointment.AppointmentStatusUI}">
                            <div class="appointment-button" action="${appointment.AppointmentId}|2">Close</div>
                            <div class="appointment-button" action="${appointment.AppointmentId}|3">Cancel</div>
                        </div>
                    </div>`;
}