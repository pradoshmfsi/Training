$(document).ready(() => {
  $("#clearData").click(() => {
    $.removeCookie("userObj");
    window.location.href = "index.html";
  });
});

if ($.cookie("userObj")) {
  const userObj = JSON.parse($.cookie("userObj"));
  $("[id*=Detail]").each((index, item) => {
    let str = item.id.substring(0, item.id.length - 6);
    if ($(item).attr("id") == "dpDetail") {
      item.src = localStorage.getItem("user-registration-image");
    } else {
      $(item).html(
        `<span class="show-details-element">${userObj[str][0]}:</span> ${userObj[str][1]}`
      );
    }
  });
} else {
  window.location.href = "index.html";
}
