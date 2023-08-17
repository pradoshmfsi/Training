$(document).ready(() => {
    $("#btnLogin").on("click", (event) => {
        validate(event);
    })
})
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
async function validate(event) {
    event.preventDefault();
    result = [];
    $(".txt").each((index, item) => {
        result.push(validateTextById($(item).attr("isrequired")));
    })
    if (result[0] == "" && result[1] == "") {
        let loginResult = await authorizeUser();
        if (loginResult.IsSuccess) {
            window.location.href = "Doctor.aspx";
        }
        else {
            $("#lblError").text(loginResult.Data);
        }
    }
}
async function authorizeUser() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "Login.aspx/AuthorizeUser",
            type: "POST",
            data: JSON.stringify({ email:$("#txtEmail").val(),password:$("#txtPassword").val() }),
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