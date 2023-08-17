const params = new URLSearchParams(window.location.search);
const userId = params.get("userId");
$(document).ready(() => {

    fetchUserRoles();

    fetchCountries();    
    
    $("#presentAddress fieldset legend").text("Present Address");
    $("#permanentAddress fieldset legend").text("Permanent Address");

    $("#submitFormNew").click((event) => {
        validate(event);
    });  

    $("fieldset input,select:not(#present_AddressLine2,#permanentAddressLine2)").each((index, inputElement) => {
        $(inputElement).parentsUntil(".input-container").prev("div").attr("id", inputElement.id + "Title");
        let scrollPosId = "#" + inputElement.id + "Title";
        $(inputElement).attr("isrequired", "#"+inputElement.id + "|" + scrollPosId + "|" + scrollPosId + " span" + "|.");
    });

    $(`<input id="ifPresentSameAsPermanent" type="checkbox" name="ifPresentSameAsPermanent"><label for="ifPresentSameAsPermanent">Same as present address</label>`).prependTo("#permanentAddress fieldset");

    $(".form-container [isRequired*='|']").each((index, inputElement) => {
        $(inputElement).on("change", () => {
            validateTextById($(inputElement).attr("isRequired"));
        });
    }); 

    $("[isRequired='true'][type='radio']").on("change", () => {
        validateGender();
    });

    $("#chklistUserRole").on("click", $("[type = 'checkbox']"), () => {
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
            var icon = $("#iconForAddDocument");
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

    $(`[id *= "Country"]`).each((index, item) => {
        $(item).on("change", () => {
            populateStates(item.id);
        });
    });

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

    $("#AddNote").on("click", (event) => {
        addNotes(event);
    })


    //$("#permanentAddress fieldset").prepend(`<input id="ifPresentSameAsPermanent" type="checkbox" name="ifPresentSameAsPermanent"><label for="ifPresentSameAsPermanent">Same as present address</label>`);

    $("#ifPresentSameAsPermanent").on("click", () => {
        populatePermanentAsPresent();
    });

    if (userId) {
        fetchUserDetails(userId);

        fetchDocuments(userId);

        fetchNotes(userId);
    }
    
});

function fetchUserDetails(userId) {
    
    $.ajax({
        type: "POST",
        url: 'Form.aspx/GetUser',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ userId: Number(userId) }),
        beforeSend: function () {
            $("#loading img").show();
        },
        success: function (r) {
            populateUserDetails(r.d);
        },
        complete: function () {
            $("#loading img").hide();
        }
    });

}
function populateUserDetails(data) {
    console.log(data);
    $("[type='text'],[type='password'],[type='date'],select").each((index, item) => {
        if (item.id.includes("State")) {
            populateStates(item.id.replace("State", "Country"));
        }
        $(item).val(data[item.id.replace("_", "")]);
    })

    $("#confirmPassword").val(data["password"]);

    if (data["gender"] == "male") {
        $("#male").prop("checked", true);
    }
    else {
        $("#female").prop("checked", true);
    }

    $("[id*='chklistUserRole_']").each((index, item) => {
        if (data["userRoles"].includes(Number(item.value))) {
            item.checked = true;
        }
    })

    $("#profileImage").attr("src", "../upload/" + data["profilePicActual"]);
} 

//Change async await populate States
function populatePermanentAsPresent() {
    if ($("#ifPresentSameAsPermanent").prop("checked")) {
        $("#permanentAddress input,select").each((index, item) => {
            if (item.id == "permanent_State") {
                populateStates("permanent_Country")
            }
            $(item).val($("#present_" + item.id.substr(item.id.indexOf('_') + 1)).val())
        });
    }
}
function populateStates(id) {
    let stateSelect = "#" + id.substr(0, id.indexOf('_')) + "_State";
    $(stateSelect).empty();
    $(stateSelect).append(`<option value="">Select State</option>`);
    let countryVal = $("#" + id).val();
    if (countryVal != "") {
        let countryId = Number(countryVal);
        let stateList = fetchStates(countryId);
        stateList.forEach((option) => {
            $(stateSelect).append(generateFunc(option.stateId, option.stateName));
        });
    }
}

function fetchStates(countryId) {
    let result = null;
    $.ajax({
        url: "Form.aspx/GetStates",
        type: "POST",
        data: JSON.stringify({ countryId: countryId }),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async:false,
        beforeSend: function () {
            $("#loading img").show();
        },
        success: (response) => {
            result = response.d;
        },
        error: (response) => {
            alert("Some error occured");
        },
        complete: function () {
            $("#loading img").hide();
        }
    });
    return result;
}
//async function populateStates(id) {
//    let stateSelect = "#" + id.substr(0, id.indexOf('_')) + "_State";
//    $(stateSelect).empty();
//    $(stateSelect).append(`<option value="">Select State</option>`);
//    let countryVal = $("#" + id).val();
//    if (countryVal != "") {
//        let countryId = Number(countryVal);
//        let stateList = await fetchStates(countryId);
//        stateList.forEach((option) => {
//            $(stateSelect).append(generateFunc(option.stateId, option.stateName));
//        });
//    }   
//}

//async function fetchStates(countryId) {
//    return new Promise((resolve, reject) => {
//        $.ajax({
//            url: "Form.aspx/GetStates",
//            type: "POST",
//            data: JSON.stringify({countryId:countryId}),
//            contentType: 'application/json; charset=utf-8',
//            dataType: 'json',
//            beforeSend: function () {
//                $("#loading img").show();
//            },
//            success: (response) => {
//                resolve(response.d);
//            },
//            error: (response) => {
//                reject(response.d);
//            },
//            complete: function () {
//                $("#loading img").hide();
//            }
//        });
//    })
//}

function fetchCountries() {
    $.ajax({
        type: "POST",
        url: 'Form.aspx/GetCountries',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async:false,
        success: function (r) {
            populateCountryList(r.d, "#present_Country");
            populateCountryList(r.d, "#permanent_Country");
        }
    });
}

function populateCountryList(optionList,id) {
    optionList.forEach((option) => {
        $(id).append(generateFunc(option.countryId,option.countryName));
    });
}

function generateFunc(value,text) {
    return `<option value="${value}">${text}</option>`
}

function fetchUserRoles() {
    $.ajax({
        type: "POST",
        url: 'Form.aspx/GetRoles',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (r) {
            populateRoles(r.d);
        }
    });
}

function populateRoles(roleList) {
    roleList.forEach((role) => {
        $("#chklistUserRole").append(generateRoleRow(role));
    });
    
}

function generateRoleRow(role) {
    return `<tr>
        <td>
            <input id="chklistUserRole_${role.roleId}" type="checkbox" name="chklistUserRole$0" value="${role.roleId}"><label for="chklistUserRole_${role.roleId}">${role.roleName}</label>

        </td>
    </tr>`
}

function fetchDocuments(userId) {

    $.ajax({
        type: "POST",
        url: 'UserForm.aspx/GetDocuments',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ userId: userId }),
        success: function (r) {
            populateDocuments(r.d);
        }
    });

}

function fetchNotes(userId) {
    $.ajax({
        type: "POST",
        url: 'UserForm.aspx/GetNotes',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ userId: Number(userId) }),
        success: function (r) {
            populateNotes(r.d);
        }
    });
}

function addNotes(event) {
    let data = {};
    const params = new URLSearchParams(window.location.search);
    data["noteMessage"] = $("#NoteTextArea").val();
    data["userId"] = Number(params.get("userId"));
    data["ifPrivate"] = $("#ifPrivateCheck").prop("checked") ? 1 : 0;
    if ($("#NoteTextArea").val().trim() != "") {
        $.ajax({
            type: "POST",
            url: "UserForm.aspx/AddNoteToDB",
            data: JSON.stringify({ newNote: data }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $("#loading img").show();
            },
            success: function (response) {
                if (response.d) {
                    fetchNotes(params.get("userId"));
                }
                else {
                    alert("Some error occured");
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
    event.preventDefault();
}

function populateNotes(noteList) {
    $(".note-container").empty();
    if (noteList.length == 0) {
        $(".note-container").append(`<span>No notes found!</span>`);
    }
    noteList.forEach((note) => {
        $(".note-container").append(generateNoteDiv(note));
    });
    $("#NoteTextArea").val("");
}

function generateNoteDiv(note) {
    return `<div class="note-element" ${note.ifPrivate == 1 ? "id='privateNote'" : ""}>
        <div class="note-message">${note.noteMessage}</div>
        <div class="note-details">
            <div class="note-by">${note.createdBy}</div>
            <div class="note-on">${new Date(parseInt(note.createdOn.replace(/[^0-9]/g, ""))).toLocaleDateString()}</div>
            ${note.ifPrivate == 1 ? "<div id='private'>PRIVATE</div>" : ""}
        </div>

    </div>`
}

function selectNav(requiredNav) {
    let navArr = ["#detailsNav", "#notesNav", "#documentsNav"]
    navArr.forEach((nav) => {
        $(nav).css({ "background-color": "rgb(233 233 233)" })
    })
    $("#detailsContainer").hide();
    $("#noteUpdatePanel").hide();
    $("#documentDivUserControl").hide();
    $(requiredNav).css("background-color", "white");
}

function populateDocuments(documentList) {
    $("#documentList").empty();
    if (documentList.length == 0) {
        $("#documentList").append(`<span>No documents found!</span>`);
    }
    documentList.forEach((document) => {
        $("#documentList").append(generateDocumentDiv(document));
    });
    $("#documentFile").val("");
}

function generateDocumentDiv(document) {
    let fileType = document.documentName.slice(document.documentName.lastIndexOf(".") + 1);
    if (!['pdf', 'txt', 'png', 'jpg', 'jpeg', 'docx'].includes(fileType)) {
        fileType = "wrong"
    }
    return `<div class="document-element">
            <div class="file-type ${fileType}"></div>
            <div class="document-details">
                <div class="document-name"><a href="DocumentDownloadHandler.ashx?documentName=${document.documentName}">${document.documentName}</a></div>
                <div class="document-created">
                    <div class="document-on">${new Date(parseInt(document.createdOn.replace(/[^0-9]/g, ""))).toLocaleDateString()}</div>
                    <div class="document-by">${document.createdBy}</div>
                </div>
            </div>
        </div>`;
}

async function validateIfEmailAlreadyPresent(userId) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "UserForm.aspx/CheckIfEmailAlreadyPresent",
            data: JSON.stringify({ email: $("#email").val(), userId: userId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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

function uploadDocumnetAjax(event) {
    event.preventDefault();
    let files = $("#documentFile")[0].files;
    if (!files) {
        return;
    }
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
        beforeSend: function () {
            $("#loading img").show();
        },
        success: function (result) {
            if (result) {
                fetchDocuments(params.get('userId'));
            }
            else {
                alert("Some error occured");
            }
        },
        error: function (err) {
            alert(err.statusText)

        },
        complete: function () {
            $("#loading img").hide();
            resetDocumentFile();
        }
    });


}

function resetDocumentFile() {
    $('#documentFile').val('');
    $("#documentName").text('');
    $('#addIconForDocument').addClass('fa-sharp fa-solid fa-plus');
}

function validateDP() {
    if (userId ==null) {
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
    if (userId == null && (confirmPassword === "" || confirmPassword.trim() === "")) {
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

async function validate(event) {

    event.preventDefault();

    let isCorrect = true;
    var result = [];
    $(`[isRequired*='[']`).each((index, item) => {
        result.push(validateTextById($(item).attr("isRequired")));
    });

    let presentUser = 0;

    if (userId != null) {
        presentUser = Number(userId);
    }
    let span = $("#emailTitle span");
    let emailValid = await validateIfEmailAlreadyPresent(presentUser);
    if (emailValid != "") {
        span.text("*" + emailValid);
        result.push("#email");
    }

    if (userId == null) {
        result = [...result,
        validateTextById($("#password").attr("isrequired")),
        validateTextById($("#confirmPassword").attr("isrequired")),
        ]
    }

    result = [
        ...result,   
        validatePassword(),
        validateGender(),
        validateTextById("#dob|#dobTitle|#dobTitle span|."),
        validateUserRoles(),
        validateDP(),
    ];

    //VALIDATE PRESENT AND PERMANENT ADDRESS USING USER DEFINED ATTRIBUTES
    $(`fieldset [isRequired*='|']`).each((index, item) => {
        result.push(validateTextById($(item).attr("isrequired")));
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
        await submitUser();
    }
}

function UploadProfilePic(formData) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "ProfilePicUpload.ashx",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
            beforeSend: function () {
                $("#loading img").show();
            },
            success: (response) => {
                resolve(response);
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

async function submitUser() {

    const params = new URLSearchParams(window.location.search);

    let data = {};

    data["userId"] = Number(params.get('userId'));
    $("[type='text'],[type='date'],select").each((index, item) => {
        data[item.id.replace("_","")] = $(item).val();
    })

    if ($("#password").val()) {
        data[$("#password")[0].id] = $("#password").val();
    }

    data["gender"] = $("#male").prop("checked") ? "male" : "female";

    if ($("#profilePic")[0].files[0]) {
        data["profilePic"] = $("#profilePic")[0].files[0].name;
        let files = $("#profilePic")[0].files;
        let formData = new FormData();
        formData.append(files[0].name, files[0]);
        data["profilePicActual"] = await UploadProfilePic(formData);
    }

    data["userRoles"] = [];

    $("[id*='chklistUserRole_']").each((index, item) => {
        if (item.checked) {
            data["userRoles"].push(Number(item.value));
        }
    })

    console.log(data);

    $.ajax({
        type: "POST",
        url: "UserForm.aspx/AddUser",
        data: JSON.stringify({ user: data }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            $("#loading img").show();
        },
        success: function (response) {
            alert(response.d);

            if (!params.get('userId')) {
                window.location.href = "Details.aspx";
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