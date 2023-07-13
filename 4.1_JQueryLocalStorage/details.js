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
        `<span class="show-details-element">${str}:</span> ${userObj[str]}`
      );
    }
  });
} else {
  window.location.href = "index.html";
}
