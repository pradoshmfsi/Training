import { ajaxWebMethodCall } from "./Utils.js";

$(document).ready(() => {
    $("#btnInitData").on("click", function (event) {
        event.preventDefault();
        initializeData();
       
    });
});

async function initializeData() {
    let response = await ajaxWebMethodCall({
        url: 'Initialize.aspx/InitializeData', data: ""
    });
    if (response.IsSuccess) {
        alert(response.Data);
        window.location.href = "Patient.aspx";
    }
    else {
        alert(response.Data);
    }
}