$(document).ready(() => {

    menuResponsive();

    $("#navHome").addClass("selected-nav");

    $("#dateAppointDate")[0].min = getTodaysDate();
    $("#dateAppointDate").val(getTodaysDate());

    populateSlots();

    $("#dateAppointDate").on("change", () => {
        populateSlots();
    })
    $("#btnAppoint").on("click", (event) => {
        validate(event);
    })
    $(".slot-list-container").on("click", ".slot-container:not(.booked)", function () {
        selectSlot(this.id);
    })
})

function selectSlot(id) {
    $(".slot-container").each((index, item) => {
        $(item).removeAttr("selectedSlot");
    })
    $("#" + id).attr("selectedSlot", "true");
}

function validateSlots() {
    let slotChecked = $("[selectedSlot='true']");
    let span = $("#divSlotTitle span");
    if (slotChecked.length != 0) {
        span.text("*");
        return "";
    }
    else {
        span.text("*Required");
        return "#divSlotTitle";
    }
}

function validate(event) {
    event.preventDefault();
    let result = [];
    $(".txt").each((index, item) => {
        result.push(validateTextById($(item).attr("isrequired")))
    })
    result.push(validateSlots());
    if (result.slice(-1) != "") {
        $("#divSlotTitle")[0].scrollIntoView({
            behavior: "smooth",
            block: "center",
        });
    }
    if (result.every((item) => item == "")) {
        submitAppointment();
    }
}

async function populateSlots() {
    if (!validateTextById($("#dateAppointDate").attr("isrequired"))) {
        $(".slot-list-container").empty();
        const params = new URLSearchParams(window.location.search);
        const doctorId = params.get("doctorId");

        let slotList = await ajaxWebMethodCall({
            url: "BookAppointment.aspx/GetAvailableSlots",
            data: JSON.stringify({ doctorId: Number(doctorId), appointmentDate: $("#dateAppointDate").val() })
        });

        if (slotList.IsSuccess) {
            slotList.Data.forEach((option, index) => {
                $(".slot-list-container").append(generateSlotContainer(option, index));
            });
        }
        else {
            alert(slotList.Data);
        }
    }
}

function generateSlotContainer(option, index) {
    return `<div id="slot${index}" class="slot-container ${option.SlotStatus}"  startTime="${option.SlotStartTime}">
                    <div class="slot-time">${option.SlotStartTimeUI}<br/>TO<br/>${option.SlotEndTimeUI}</div>
             </div>`;
}

async function submitAppointment() {
    const params = new URLSearchParams(window.location.search);
    let data = {};
    data["doctorId"] = Number(params.get('doctorId'));
    $("[type='text'],[type='date']").each((index, item) => {
        data[item.name] = $(item).val();
    })
    data["AppointmentTime"] = $("[selectedSlot='true']").attr("startTime");

    let submitResponse = await ajaxWebMethodCall({ url: "BookAppointment.aspx/AddAppointment", data: JSON.stringify({ appointmentObj: data }) });

    if (submitResponse.IsSuccess) {
        window.location.href = submitResponse.Data; //URL for downloading Apoointment PDF
        alert("Appointment Saved Successfully");      
        window.location.href = "Patient.aspx";
    }
    else {
        alert(submitResponse.Data);
    }
}