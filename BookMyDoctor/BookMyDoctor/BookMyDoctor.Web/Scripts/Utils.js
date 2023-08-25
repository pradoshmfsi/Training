async function ajaxWebMethodCall(requestObj) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: requestObj.url,
            contentType: 'application/json; charset=utf-8' ,
            dataType: 'json',
            data: requestObj.data,
            beforeSend: function () {
                $("#loading img").show();
            },
            success: function (r) {
                resolve(r.d);
            },
            error: function (r) {
                reject(r.d);
            },
            complete: function () {
                $("#loading img").hide();
            }
        });
    });
}

function getTodaysDate() {
    let today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1;
    let dd = today.getDate();
    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;
    return yyyy + "-" + mm + "-" + dd;
}

function validateTextById(attributeString) {
    let attrArray = attributeString.split("|");
    let attrObj = {};
    attrArray.forEach((attr) => {
        attrObj[attr.split(":")[0]] = attr.split(":")[1];
    })
    let data = $(attrObj["Id"]).val();
    let span = $(attrObj["TitleId"]);
    let patternName = new RegExp(attrObj["Regex"], "g");
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

function menuResponsive() {
    $(".menu").on("click", function () {
        if ($(this).attr("icon") == "bars") {
            $(".menu").html("<i class='fa-solid fa-xmark'></i>");
            $(this).attr("icon","xmark");
        }
        else {
            $(".menu").html("<i class='fa-solid fa-bars'></i>");
            $(this).attr("icon","bars");
        }
        $(".navbar").toggleClass("responsive");
    })
}
