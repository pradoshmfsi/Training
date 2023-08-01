let token = "";
$(document).ready(() => {

    $(".form-container [isRequired*='|']").each((index, inputElement) => {
        $(inputElement).on("change", () => {
            validateTextById($(inputElement).attr("isRequired"));
        });
    });

    $("[isRequired='true'][type='radio']").on("change", () => {
        validateGender();
    });

    $("#hobby").on("input", () => populateHobby());

    $("#dp").on("change", () => {
        displayProfilePicName();
    });

    $("#ifPresentSameAsPermanent").on("click", () => {
        populatePermanentAsPresent();
    });

    $(`[id *= "Country"]`).each((index, item) => {
        $(item).on("change", () => {
            populateStates($(item).attr("addressType"));
        });
    });
});
function validateDP() {
    if ($("#dp").attr("isrequired")=="true") {
        const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];
        const span = $("#dpTitle span");
        const file = $("#dp")[0];
        if (!file.files[0]) {
            span.text("*Required");
            return "#dpTitle";
        } else if (!allowedFormats.includes(file.files[0].type)) {
            span.text("*Invalid file format");
            return "#dpTitle";
        } else {
            span.text("*");
            return "";
        }
    }
    
}
function displayImageById(file, id) {
    $(`#${id}`).attr("src", URL.createObjectURL(file.files[0]));
}

function displayProfilePicName() {
    const file = $("#dp")[0];
    let filename = $("#filename");
    if (!validateDP()) {
        filename.text(file.files[0].name);
        var icon = $("#iconForAddPic");
        icon.addClass("fa-solid fa-check");
        displayImageById(file, "profileImage");
    } else {
        filename.text("");
        $("#profileImage").attr(
            "src",
            "https://img.freepik.com/free-icon/user_318-159711.jpg?w=2000"
        );
    }
}
function populateHobby() {
    let hobby = $("#hobby").val();
    let list = $("#hobbyList");
    let hobbyArr = ["Singing", "Dancing", "Playing"];
    list.html("");
    for (let i = 0; i < hobbyArr.length; i++) {
        if (!hobby.includes(hobbyArr[i])) {
            let option = $("<option></option>");
            if (hobby.trim() != "") {
                option.text(hobby + ", " + hobbyArr[i]);
            } else {
                option.text(hobbyArr[i]);
            }
            list.append(option);
        }
    }
}
function validateTextById(attributeString) {
    let [id, showId, spanQuery, patternName] = attributeString.split("|");
    let data = $(id).val();
    let span = $(spanQuery);
    patternName = new RegExp(patternName, "g");
    try {
        if (data === "" || data.trim() === "") {
            span.text("*Required");
            return showId;
        } else if (!patternName.test(data)) {
            span.text("*Not valid");
            return showId;
        } else {
            span.text("*");
            return "";
        }
    } catch (error) {
        console.log(id);
        console.log(error);
    }
}

function validateGender() {
    let span = $("#genderTitle span");
    if (!$("#male").prop("checked") && !$("#female").prop("checked")) {
        span.text("*Required");
        return "#genderTitle";
    } else {
        span.text("*");
        return "";
    }
}

function validate() {
    let isCorrect = true;
    var result = [];
    $(`[isRequired*='[']`).each((index, item) => {
        result.push(validateTextById($(item).attr("isRequired")));
    });
    result = [
        ...result,
        validateGender(),
        validateTextById("#date|#dobTitle|#dobTitle span|."),
        validateDP(),
    ];
    //VALIDATE PRESENT AND PERMANENT ADDRESS USING USER DEFINED ATTRIBUTES
    $(`fieldset [isRequired*='|']`).each((index, item) => {
        result.push(validateTextById($(item).attr("isRequired")));
    });

    for (res of result) {
        if (res != "") {
            isCorrect = false;
            $(res)[0].scrollIntoView({
                behavior: "smooth",
                block: "center",
            });
            $(".show-details").css("display", "none");
            break;
        }
    }
    if (isCorrect) {
        return true;
    }
    else {
        return false;
    }
}