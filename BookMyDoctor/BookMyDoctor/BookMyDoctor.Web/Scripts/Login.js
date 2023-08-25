$(document).ready(() => {
    menuResponsive();
    $("#navLogin").addClass("selected-nav");
    $("#btnLogin").on("click", (event) => {
        validate(event);
    })
})

async function validate(event) {
    event.preventDefault();
    let result = [];
    $(".txt").each((index, item) => {
        result.push(validateTextById($(item).attr("isrequired")));
    })
    if (result[0] == "" && result[1] == "") {
        let loginResult = await ajaxWebMethodCall({ url: "Login.aspx/AuthorizeUser", data: JSON.stringify({ email: $("#txtEmail").val().toLowerCase(), password: $("#txtPassword").val() }) });
        if (loginResult.IsSuccess) {
            window.location.href = "DoctorAppointments.aspx";
        }
        else {
            $("#lblError").text(loginResult.Data);
        }
    }
}