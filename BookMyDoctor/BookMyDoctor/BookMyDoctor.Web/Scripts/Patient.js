$(document).ready(() => {
    menuResponsive();
    $("#navHome").addClass("selected-nav");

    fetchDoctors();
    $(".doctor-list-container").on("click", ".txt-link", function () {
        window.location.href = "BookAppointment.aspx?doctorId="+$(this).attr("doctorid");
    })
})

async function fetchDoctors() {
    let doctors = await ajaxWebMethodCall({ url:'Patient.aspx/GetDoctorsList',data:""});
    if (doctors.IsSuccess) {
        doctors.Data.forEach((doctor) => {
            $(".doctor-list-container").append(generateDoctorContainer(doctor))
        }) 
    }
    else {
        alert(doctors.Data);
    }
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
                    ${doctor.DayStartTimeUI}</div>
                <div class="doctor-day-start">
                    <div class="detail-title">Ends</div>
                    ${doctor.DayEndTimeUI}</div>
            </div>
        </div>
        <div class="txt-link" doctorId="${doctor.DoctorId}">Appoint Now</div>
    </div>`
}
