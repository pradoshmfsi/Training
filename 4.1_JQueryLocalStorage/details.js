const userObj = JSON.parse(localStorage.getItem("user"));
$("#clearData").click(() => {
  localStorage.clear();
  window.location.href = "/index.html";
});
if (userObj) {
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
  window.location.href = "4.1_JQuerylocalStorage/index.html";
}
