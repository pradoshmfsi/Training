let token = "";
let inputElements = $(".form-container [isRequired='true'][type='text']");
$("#submitForm").click(validate);

async function fetchKey() {
  try {
    const response = await fetch(
      "https://www.universal-tutorial.com/api/getaccesstoken",
      {
        method: "GET",
        headers: {
          Accept: "application/json",
          "api-token":
            "MYPAOKbvoH_Q_BiPHYNo2W8VPQEP-M3_7AKDH22ZcRErEZToC4ZGrSG5AHq_L4n_WGI",
          "user-email": "mfsi.pradoshs@gmail.com",
        },
      }
    );

    const data = await response.json();
    token = "Bearer " + data.auth_token;
    fetchCountry();
  } catch (error) {
    console.log(error);
  }
}

async function fetchFromApi(country = "countries/") {
  let url = "https://www.universal-tutorial.com/api/" + country;
  try {
    const response = await fetch(url, {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
    });

    const data = await response.json();
    return data;
  } catch (error) {
    console.error(error);
  }
}

async function fetchCountry() {
  try {
    let arr = await fetchFromApi();
    for (let i = 0; i < arr.length; i++) {
      $("#presentCountry").append(
        $(`<option></option>`).text(arr[i].country_name)
      );
      $("#permanentCountry").append(
        $(`<option></option>`).text(arr[i].country_name)
      );
    }
  } catch (error) {
    console.error(error);
  }
}

$(document).ready(fetchKey);

$(".form-container [isRequired*='|']").each((index, inputElement) => {
  let attributes = $(inputElement).attr("isRequired").split("|");
  $(inputElement).on("change", () => {
    validateTextById(
      attributes[0],
      attributes[1],
      attributes[2],
      attributes[3]
    );
  });
});

$("[isRequired='true'][type='radio']").on("change", () => {
  validateGender();
});

$("#hobby").on("input", () => populateHobby());

$("#dp").on("change", () => {
  displayProfilePicName();
});

$("#ifPresentSameAsPermanent").on("click", () => {
  populatePermanentAsPresent();
});

$(`[id *= "Country"]`).each((index, item) => {
  $(item).on("change", () => {
    populateStates($(item).attr("addressType"));
  });
});

function validateDP() {
  const span = $("#dpTitle span");
  const file = document.getElementById("dp");
  if (!file.files[0]) {
    span.text("*Required");
    return "#dpTitle";
  } else {
    span.text("*");
    return "";
  }
}

function displayImageById(file, id) {
  const path = URL.createObjectURL(file.files[0]);
  $(`#${id}`).attr("src", path);
}

function displayProfilePicName() {
  const file = $("#dp")[0];
  const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];
  let filename = $("#filename");
  if (!validateDP()) {
    if (allowedFormats.includes(file.files[0].type)) {
      filename.text(file.files[0].name);
      filename.css("color", "black");

      var icon = $("#iconForAddPic");
      icon.addClass("fa-solid fa-check");

      displayImageById(file, "profileImage");
    } else {
      filename.text("Invalid file format, only accepts(jpg/png)");
      filename.css("color", "red");
    }
  }
}

async function getStates(country, addressType) {
  try {
    let arr = await fetchFromApi("states/" + country);
    for (let i = 0; i < arr.length; i++) {
      $(`#${addressType}State`).append(
        $(`<option></option>`).text(arr[i].state_name)
      );
    }
  } catch (error) {
    console.log(error);
  }
}

async function populateStates(addressType) {
  let countryId = addressType + "Country";

  let country = $(`#${countryId}`).val();

  $(`#${addressType}State`).val("");
  if (country != "") {
    await getStates(country, addressType);
  }
  if (
    addressType == "permanent" &&
    $("#ifPresentSameAsPermanent").prop("checked")
  ) {
    $("#permanentState").val($("#presentState").val());
  }
  console.log("States Populated...");
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

function populatePermanentAsPresent() {
  if ($("#ifPresentSameAsPermanent").prop("checked")) {
    $("[validateAddress*='|']").each((index, item) => {
      let attributes = $(item).attr("validateAddress").split("|");
      $(`#${attributes[1]}`).val($(`#${attributes[0]}`).val());
      if (attributes[attributes.length - 1] == "permanent") {
        populateStates(attributes[2]);
      }
    });
  }
}

function validateTextById(id, showId, spanQuery, patternName) {
  let data = $(id).val();
  let span = $(spanQuery);
  patternName = new RegExp(patternName, "g");
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
}

function validateGender() {
  span = $("#genderTitle span");
  if (!$("#male").prop("checked") && !$("#female").prop("checked")) {
    span.text("*Required");
    return "#genderTitle";
  } else {
    span.text("*");
    return "";
  }
}

function validateAddress(addressType) {
  let msg = "";
  if (addressType == "present") {
    msg = $("#errMsgPresentAddress");
  } else {
    msg = $("#errMsgPermanentAddress");
  }
  if (
    $(`#${addressType}AddressLine1`).val() == "" ||
    $(`#${addressType}Country`).val() == "" ||
    $(`#${addressType}State`).val() == "" ||
    $(`#${addressType}AddressCity`).val() == "" ||
    $(`#${addressType}AddressPin`).val() == ""
  ) {
    msg.text("*Fill all the required fields");
    return "#" + msg.attr("id");
  } else {
    msg.text("");
    return "";
  }
}
function validate() {
  let isCorrect = true;
  var result = [
    validateTextById(
      "#firstName",
      "#firstNameTitle",
      "#firstNameTitle span",
      "[A-Za-z ]+"
    ),
    validateTextById(
      "#lastName",
      "#lastNameTitle",
      "#lastNameTitle span",
      "[A-Za-z ]+"
    ),
    validateTextById(
      "#email",
      "#emailTitle",
      "#emailTitle span",
      "[a-z0-9]+@[a-z]+.[a-z]{2,3}"
    ),
    validateGender(),
    validateTextById("#date", "#dobTitle", "#dobTitle span", "."),
    validateDP(),
    validateAddress("present"),
    validateAddress("permanent"),
  ];
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
    showDetailsAfterSubmit();
    return true;
  }
}

function showDetailsAfterSubmit() {
  const userObj = {};

  $(`[showDetails*="|"]`).each((index, item) => {
    const attributes = $(item).attr("showDetails").split("|");
    if (attributes[2] != "file") {
      if (attributes[2] == "text") {
        if ($(item).val() == "") {
          $(item).val("NA");
        }
        userObj[attributes[1]] = $(item).val();
      } else if (attributes[2] == "radio") {
        const inputRadios = $(item).children("input");
        userObj[attributes[1]] = inputRadios[0].checked
          ? inputRadios[0].value
          : inputRadios[1].value;
      } else {
        userObj[attributes[1]] = item.checked ? true : false;
      }

      $(`#${attributes[1]}Detail`).html(
        `<span class="show-details-element">${attributes[0]}:</span> ${
          userObj[attributes[1]]
        }`
      );
    } else {
      userObj[attributes[1]] = URL.createObjectURL(item.files[0]);
      displayImageById(item, attributes[1] + "Detail");
    }
  });
  console.log(userObj);
  $(".show-details").css("display", "block");
  $(".show-details")[0].scrollIntoView({ behavior: "smooth", block: "center" });
}
