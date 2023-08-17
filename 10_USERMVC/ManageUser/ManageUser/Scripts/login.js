$(document).ready(() => {
    //$(".form-container [isrequired*='|']").each((index, inputElement) => {
    //    $(inputElement).on("change", () => {
    //        validateTextById($(inputElement).attr("isRequired"));
    //    });
    //});
})
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
function validate() {
    //if (validateTextById($("#email").attr("isRequired")) || validateTextById($("#password").attr("isRequired"))) {
    //    return false;
    //}
    return true;
}