$(document).ready(() => {
    fetchDoctors();
})

function fetchDoctors() {
    $.ajax({
        type: "POST",
        url: 'Patient.aspx/GetDoctorsList',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        beforeSend: function () {
            $("#loading img").show();
        },
        success: function (r) {
            console.log(r.d);
            populateDoctors(r.d);
        },
        complete: function () {
            $("#loading img").hide();
        }
    });
}

function populateDoctors(doctorList) {
    console.log(doctorList);
    doctorList.forEach((doctor) => {
        $(".doctor-list-container").append(generateDoctorContainer(doctor))
    }) 
}

function generateDoctorContainer(doctor) {
    return `<div class="doctor-container">
        <div class="doctor-view">
            <div class="doctor-name">Dr. ${doctor.DoctorName}</div>
            <div class="doctor-details">
                <div class="doctor-slot">
                    <div class="detail-title">Slot</div>
                    ${doctor.AppointmentSlotTime}mins</div>
                <div class="doctor-day-start">
                    <div class="detail-title">Starts</div>
                    ${doctor.DayStartTime}</div>
                <div class="doctor-day-start">
                    <div class="detail-title">Ends</div>
                    ${doctor.DayEndTime}</div>
            </div>
        </div>
        <a href="Appointment.aspx?doctorId=${doctor.DoctorId}"><div class="txt-link">Appoint Now</div></a>
    </div>`
}
