let token = "";
$(document).ready(() => {
    $("#submitFormNew").click((event) => {
        validate(event);
    });
    $(".form-container [isRequired*='|']").each((index, inputElement) => {
        $(inputElement).on("change", () => {
            validateTextById($(inputElement).attr("isRequired"));
        });
    });

    $("[isRequired='true'][type='radio']").on("change", () => {
        validateGender();
    });

    $("#chklistUserRole [type='checkbox']").on("click", () => {
        validateUserRoles();
    })

    $("#hobby").on("input", () => populateHobby());

    $("#profilePic").on("change", () => {
        displayProfilePicName();
    });

    $("#documentFile").on("change", () => {
        const file = $("#documentFile")[0];
        let documentName = $("#documentName");
        if (file.files[0]) {
            documentName.text(file.files[0].name);
            var icon = $("#iconForAddPic");
            icon.addClass("fa-solid fa-check");
        } else {
            documentName.text("");
        }
    })

    $("#profileImage").on("click", () => {
        let file = $("#profileImage").attr("src");
        if (file != `https://img.freepik.com/free-icon/user_318-159711.jpg?w=2000`) {
            window.location.href = "ProfileImageHandler.ashx?fileSrc=" + $("#profileImage").attr("src");
        }
    })

    $("#AddDocumentBtn").on("click", (event) => {
        uploadDocumnetAjax(event);
    })

    $("#detailsNav").on("click", () => {
        selectNav("#detailsNav");
        $("#detailsContainer").css("display", "block");
    })
    $("#notesNav").on("click", () => {
        selectNav("#notesNav");
        $("#noteUpdatePanel").css("display", "block");
    })
    $("#documentsNav").on("click", () => {
        selectNav("#documentsNav");
        $("#documentDivUserControl").css("display", "block");
    })

    const params = new URLSearchParams(window.location.search);
    const userId = params.get("userId");
    if (userId) {
        $.ajax({
            type: "POST",
            url: 'UserForm.aspx/GetDocuments',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify({ userId:userId}),
            success: function (r) {
                populateDocuments(r.d);
            }
        });
    }   
});

function selectNav(requiredNav) {
    let navArr = ["#detailsNav", "#notesNav", "#documentsNav"]
    navArr.forEach((nav) => {
        $(nav).css({ "background-color": "rgb(233 233 233)"})
    })
    $("#detailsContainer").hide();
    $("#noteUpdatePanel").hide();
    $("#documentDivUserControl").hide();
    $(requiredNav).css("background-color", "white");
}

function populateDocuments(documentList) {
    if (documentList.length==0) {
        $("#documentList").append(`<span>No documents found!</span>`);
    }
    documentList.forEach((document) => {
        $("#documentList").append(generateDocumentDiv(document));
    });
    $("#documentFile").val("");
}

function generateDocumentDiv(document) {
    return `<div><i class="fa-solid fa-paperclip"></i>&nbsp;<a href="DocumentDownloadHandler.ashx?documentName=${document}">${document}</a></div>`;
}

function validateIfEmailAlreadyPresent() {
    result = "";
    $.ajax({
        type: "POST",
        url: "UserForm.aspx/CheckIfEmailAlreadyPresent",
        data: JSON.stringify({ email: $("#email").val() }),
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: "json",
        success: function (response) {
            result = response.d;
        },
    });
    return result;
}


function uploadDocumnetAjax(event) {
    let files = $("#documentFile")[0].files;
    var data = new FormData();
    data.append(files[0].name, files[0]);
    const params = new URLSearchParams(window.location.search);
    data.append("userId", params.get('userId'));
    $.ajax({
        url: "DocumentUploadHandler.ashx",
        type: "POST",
        data: data,
        contentType: false,
        processData: false,
        success: function (result) {
            populateDocuments([result]);
        },
        error: function (err) {
            alert(err.statusText)
        }
    });

    event.preventDefault();
}

function validateDP() {
    if ($("#profilePic").attr("isrequired") == "true") {
        const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];
        const span = $("#dpTitle span");
        const file = $("#profilePic")[0];
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
    else {
        return "";
    }
}
function validatePassword() {
    let password = $("#password").val();
    let confirmPassword = $("#confirmPassword").val();
    const span = $("#confirmPasswordTitle span");
    if ($("#password").attr("isrequired") &&  (confirmPassword === "" || confirmPassword.trim() === "")) {
        span.text("*Required");
        return "#confirmPasswordTitle";
    }
    else if (password != confirmPassword) {
        span.text("*Mismatch");
        return "#confirmPasswordTitle";
    }
    else {
        span.text("*");
        return "";
    }
}

function displayImageById(file, id) {
    $(`#${id}`).attr("src", URL.createObjectURL(file.files[0]));
}

function displayProfilePicName() {
    const file = $("#profilePic")[0];
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

function validateUserRoles() {
    let span = $("#userRoleTitle span");
    let flagArray = [];
    $("#chklistUserRole [type='checkbox']").each((index, item) => {
        if (item.checked) {
            flagArray.push(1);
        }
    });
    if (flagArray.length > 0) {
        span.text("*");
        return "";
    }
    else {
        span.text("*Required");
        return "#userRoleTitle";
    }
}

function validate(event) {
    event.preventDefault();
    let isCorrect = true;
    var result = [];
    $(`[isRequired*='[']`).each((index, item) => {
        result.push(validateTextById($(item).attr("isRequired")));
    });

    if (!$("#email").attr("isedit")) {
        let span = $("#emailTitle span");
        let emailValid = validateIfEmailAlreadyPresent();
        if (emailValid != "") {
            span.text("*" + emailValid);
            result.push("#email");
        }
    }
    

    if ($("#password").attr("isrequired")) {
        result = [...result,
        validateTextById($("#password").attr("isrequired")),
        validateTextById($("#confirmPassword").attr("isrequired")) ]
    }

    result = [
        ...result,
        validatePassword(),
        validateGender(),
        validateTextById("#date|#dobTitle|#dobTitle span|."),
        validateUserRoles(),
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


//function submitUser() {
//    let data = {};
//    $("[type='text']:not(#hobby),[type='date'],[type='password']:not('#confirmPassword'),select").each((index, item) => {
//        data[item.id] = $(item).val();
//    })



//    console.log(data);
//}