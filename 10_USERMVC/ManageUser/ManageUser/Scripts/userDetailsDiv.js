$(document).ready(() => {
    $.ajax({
        type: "POST",
        url: 'userDetailsDiv.aspx/GetUsers',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (r) {
            populateDetails(r.d);      
        }
    });
})
//$("#userDetailsDiv .row:not(#header)").on("click", "div:last-child", () => {
//     window.location.href = "UserForm.aspx?userId=" + $(this).attr("id");
//})
function generateRow(user) {
    return `<div class="row">
        <div> ${user.userId} </div>
                <div>${user.firstName}</div>
                <div>${user.lastName}</div>
                <div>${user.email}</div>
                <div>${user.userRole}</div>
                <div>${user.userHobby}</div>
                <div id="editUser${user.userId}"> <a href="UserForm.aspx?userId=${user.userId}">EDIT</a></div>
            </div>`
}

function populateDetails(userList) {
    userList.forEach((user) => {
        $("#userDetailsDiv").append(generateRow(user));
        $(`#editUser${user.userId}`).on("click", () => {
            window.location.href = "UserForm.aspx?userId=" + user.userId;
        })
    });
}
