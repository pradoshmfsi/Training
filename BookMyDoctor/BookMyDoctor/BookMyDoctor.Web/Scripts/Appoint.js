$(document).ready(() => {
    
    $("#dateAppointDate")[0].min = getTodaysDate();
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

function getTodaysDate() {
    let today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1;
    let dd = today.getDate();
    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
    return yyyy + "-" + mm + "-" + dd;
}

function selectSlot(id) {
    $(".slot-container").each((index, item) => {
        $(item).removeAttr("selectedSlot");
    })
    $("#" + id).attr("selectedSlot", "true");
}

function validateTextById(attributeString) {
    let attrArray = attributeString.split("|");
    attrObj = {};
    attrArray.forEach((attr) => {
        attrObj[attr.split(":")[0]] = attr.split(":")[1];
    })
    let data = $(attrObj["Id"]).val();
    let span = $(attrObj["TitleId"]);
    patternName = new RegExp(attrObj["Regex"], "g");
    try {
        if (data === "" || data.trim() === "") {
            span.text("*Required");
            return attrObj["TitleId"];
        } else if (!patternName.test(data)) {
            span.text("*Not valid");
            return attrObj["TitleId"];
        } else {
            span.text("*");
            return "";
        }
    } catch (error) {
        console.log(id);
        console.log(error);
    }
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

async function validate(event) {
    event.preventDefault();
    result = [];
    $(".txt").each((index, item) => {
        result.push(validateTextById($(item).attr("isrequired")))
    })
    result.push(validateSlots());
    if (result.every((item) => item == "")) {
        submitAppointment();
    }
}

async function populateSlots() {
    if (!validateTextById($("#dateAppointDate").attr("isrequired"))) {
        $(".slot-list-container").empty();
        const params = new URLSearchParams(window.location.search);
        const doctorId = params.get("doctorId");
        let slotList = await fetchSlots(Number(doctorId), $("#dateAppointDate").val());
        console.log(slotList.Data);
        slotList.Data.forEach((option, index) => {
            $(".slot-list-container").append(generateSlotContainer(option, index));
        });
    }
}

function generateSlotContainer(option, index) {
    return `<div id="slot${index}" class="slot-container ${option.SlotStatus}"  startTime="${option.SlotTime}">
                    <div class="slot-time">${option.SlotStartTime}<br/>TO<br/>${option.SlotEndTime}</div>
             </div>`;
}

async function fetchSlots(doctorId, date) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "Appointment.aspx/GetAvailableSlots",
            type: "POST",
            data: JSON.stringify({ doctorId: doctorId, appointmentDate: date }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            beforeSend: function () {
                $("#loading img").show();
            },
            success: (response) => {
                resolve(response.d);
            },
            error: (response) => {
                reject(response.d);
            },
            complete: function () {
                $("#loading img").hide();
            }
        });
    })
}

function submitAppointment() {
    const params = new URLSearchParams(window.location.search);
    let data = {};
    data["doctorId"] = Number(params.get('doctorId'));

    $("[type='text'],[type='date']").each((index, item) => {
        data[item.name] = $(item).val();
    })

    data["AppointmentTime"] = $("[selectedSlot='true']").attr("startTime");
    data["AppointmentStatus"] = "Open";

    $.ajax({
        type: "POST",
        url: "Appointment.aspx/AddAppointment",
        data: JSON.stringify({ appointmentObj: data}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loading img").show();
        },
        success: function (response) {
            alert(response.d.Data);
            if (response.d.IsSuccess) {
                window.location.href = "Patient.aspx";
            }            
        },
        error: function (err) {
            alert("Some error occured");
        },
        complete: function () {
            $("#loading img").hide();
        }
    });

}