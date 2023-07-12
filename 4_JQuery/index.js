let token = "";
const textRegex = /[A-Za-z ]+/;
const emailRegex = /[a-z0-9]+@[a-z]+\.[a-z]{2,3}/;

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
    fetchCountry(token);
  } catch (error) {
    console.log(error);
  }
}

async function fetchCountry(token) {
  try {
    const response = await fetch(
      "https://www.universal-tutorial.com/api/countries/",
      {
        method: "GET",
        headers: {
          Authorization: token,
          Accept: "application/json",
        },
      }
    );

    const data = await response.json();
    let arr = data;

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

$(".form-container [isRequired='true'][type='text']").each(
  (index, inputElement) => {
    let inputElementId = inputElement.id;
    if (inputElement.type == "text" && inputElementId.slice(-4) == "Name") {
      $(inputElement).on("change", () => {
        validateTextById(inputElementId, textRegex);
      });
    } else {
      $(inputElement).on("change", () => {
        validateTextById(inputElementId, emailRegex);
      });
    }
  }
);

$("[isRequired='true'][type='radio']").on("change", () => {
  validateGender();
});

$("[isRequired='true'][type='date']").on("change", () => {
  validateDate();
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
    return "dpTitle";
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
  let stateId = addressType + "State";
  try {
    let url = "https://www.universal-tutorial.com/api/states/" + country;
    let res = await fetch(url, {
      method: "GET",
      headers: {
        Authorization: token,
        Accept: "application/json",
      },
    });

    let arr = await res.json();
    for (let i = 0; i < arr.length; i++) {
      $(`#${stateId}`).append($(`<option></option>`).text(arr[i].state_name));
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
  if (addressType == "permanent") {
    if ($("#ifPresentSameAsPermanent").prop("checked")) {
      $("#permanentState").val($("#presentState").val());
    }
  }
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

async function populatePermanentAsPresent() {
  if ($("#ifPresentSameAsPermanent").prop("checked")) {
    $("#permanentAddressLine1").val($("#presentAddressLine1").val());
    $("#permanentAddressLine2").val($("#presentAddressLine2").val());
    $("#permanentCountry").val($("#presentCountry").val());
    populateStates("permanent");
    $("#permanentState").val($("#presentState").val());
    $("#permanentCity").val($("#presentCity").val());
    $("#permanentPin").val($("#presentPin").val());
  }
}

function validateTextById(id, patternName = /.*/) {
  let data = $(`#${id}`).val();
  let span = $(`#${id}Title span`);
  if (data === "" || data.trim() === "") {
    span.text("*Required");
    return id + "Title";
  } else if (!patternName.test(data)) {
    span.text("*Not valid");
    return id + "Title";
  } else {
    span.text("*");
    return "";
  }
}

function validateDate() {
  span = $("#dobTitle span");
  let date = $("#date").val();
  if (date == "") {
    span.text("*Required");
    return "dobTitle";
  } else {
    span.text("");
    return "";
  }
}

function validateGender() {
  span = $("#genderTitle span");
  if (!$("#male").prop("checked") && !$("#female").prop("checked")) {
    span.text("*Required");
    return "genderTitle";
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
    return msg.attr("id");
  } else {
    msg.text("");
    return "";
  }
}
function validate() {
  let isCorrect = true;
  var result = [
    validateTextById("firstName", textRegex),
    validateTextById("lastName", textRegex),
    validateTextById("email", emailRegex),
    validateGender(),
    validateDate(),
    validateDP(),
    validateAddress("present"),
    validateAddress("permanent"),
  ];
  for (res of result) {
    if (res != "") {
      isCorrect = false;
      $(`#${res}`)[0].scrollIntoView({
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
  const showDetails = $(".show-details");
  showDetails.css("display", "block");
  showDetails[0].scrollIntoView({ behavior: "smooth", block: "center" });
}
