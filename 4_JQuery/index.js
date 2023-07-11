let token = "";
const textRegex = /[A-Za-z ]+/;
const emailRegex = /[a-z0-9]+@[a-z]+\.[a-z]{2,3}/;

let flagForFirstError = "";

let form = $(".form-container")[0];
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

    let presentCountry = document.getElementById("presentCountry");
    let permanentCountry = document.getElementById("permanentCountry");
    for (let i = 0; i < arr.length; i++) {
      let optionPresent = document.createElement("option");
      let optionPermanent = document.createElement("option");

      optionPresent.value = arr[i].country_name;
      optionPresent.innerText = arr[i].country_name;

      optionPermanent.value = arr[i].country_name;
      optionPermanent.innerText = arr[i].country_name;

      presentCountry.appendChild(optionPresent);
      permanentCountry.appendChild(optionPermanent);
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

$("[isRequired='true'][type='radio']").each((index, item) => {
  $(item).on("change", () => {
    validateGender();
  });
});

$("[isRequired='true'][type='date']").each((index, item) => {
  $(item).on("change", () => {
    validateDate();
  });
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
  const span = $($("#dpTitle span")[0]);
  const file = document.getElementById("dp");
  if (!file.files[0]) {
    span.text("*Required");
    if (!flagForFirstError) {
      flagForFirstError = "dpTitle";
    }
    return false;
  } else {
    span.text("*");
    return true;
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
  if (validateDP()) {
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

    let data = await res.json();
    let arr = data;
    let state = document.getElementById(stateId);

    for (let i = 0; i < arr.length; i++) {
      var option = document.createElement("option");
      option.value = arr[i].state_name;
      option.innerText = arr[i].state_name;
      state.appendChild(option);
    }
  } catch (error) {
    console.log(error);
  }
}
async function populateStates(addressType) {
  let countryId = addressType + "Country";

  let country = document.getElementById(countryId).value;
  document.getElementById(addressType + "State").value = "";
  if (country != "") {
    await getStates(country, addressType);
  }
  if (addressType == "permanent") {
    let address = document.getElementById("ifPresentSameAsPermanent");
    if (address.checked) {
      let presentAddressState = document.getElementById("presentState").value;
      document.getElementById("permanentState").value = presentAddressState;
    }
  }
}

function populateHobby() {
  let hobby = document.getElementById("hobby").value;
  let list = document.getElementById("hobbyList");
  let hobbyArr = ["Singing", "Dancing", "Playing"];
  list.innerHTML = "";
  for (let i = 0; i < hobbyArr.length; i++) {
    if (!hobby.includes(hobbyArr[i])) {
      let option = document.createElement("option");
      if (hobby.trim() != "") {
        option.value = hobby + ", " + hobbyArr[i];
      } else {
        option.value = hobbyArr[i];
      }
      list.appendChild(option);
    }
  }
}

async function populatePermanentAsPresent() {
  let address = document.getElementById("ifPresentSameAsPermanent");
  let permanentAddressLine1 = document.getElementById("permanentAddressLine1");
  let permanentAddressLine2 = document.getElementById("permanentAddressLine2");
  let permanentCountry = document.getElementById("permanentCountry");
  let permanentState = document.getElementById("permanentState");
  let permanentCity = document.getElementById("permanentCity");
  let permanentPin = document.getElementById("permanentPin");

  if (address.checked == true) {
    let presentAddressLine1 = document.getElementById(
      "presentAddressLine1"
    ).value;
    let presentAddressLine2 = document.getElementById(
      "presentAddressLine2"
    ).value;
    let presentAddressCountry = document.getElementById("presentCountry").value;
    let presentAddressCity = document.getElementById("presentCity").value;
    let presentAddressPin = document.getElementById("presentPin").value;

    permanentAddressLine1.value = presentAddressLine1;
    permanentAddressLine2.value = presentAddressLine2;
    permanentCountry.value = presentAddressCountry;

    populateStates("permanent");

    permanentCity.value = presentAddressCity;
    permanentPin.value = presentAddressPin;
  } else {
    permanentAddressLine1.value = "";
    permanentAddressLine2.value = "";
    permanentCountry.value = "";
    permanentState.value = "";
    permanentCity.value = "";
    permanentPin.value = "";
  }
}

function validateTextById(id, patternName) {
  let data = document.getElementById(id).value;
  let text = id + "Title";
  let span = document.getElementById(text).getElementsByTagName("span")[0];
  if (data === "" || data.trim() === "") {
    span.innerText = "*Required";
    return false;
  } else if (!patternName.test(data)) {
    span.innerText = "*Not valid";
    return false;
  } else {
    span.innerText = "*";
    return true;
  }
}

function validateDate() {
  span = document.getElementById("dobTitle").getElementsByTagName("span")[0];
  let date = document.getElementById("date").value;
  if (date == "") {
    span.innerText = "*Required";
    if (!flagForFirstError) {
      flagForFirstError = "dobTitle";
    }
    return false;
  } else {
    span.innerText = "*";
    return true;
  }
}

function validateGender() {
  span = document.getElementById("genderTitle").getElementsByTagName("span")[0];
  let male = document.getElementById("male");
  let female = document.getElementById("female");
  if (!male.checked && !female.checked) {
    span.innerText = "*Required";
    if (!flagForFirstError) {
      flagForFirstError = "genderTitle";
    }
    return false;
  } else {
    span.innerText = "*";
    return true;
  }
}

function validateAddress(addressType) {
  let msg = "";
  let AddressLine1 = document.getElementById(
    addressType + "AddressLine1"
  ).value;
  let AddressCountry = document.getElementById(addressType + "Country").value;
  let AddressState = document.getElementById(addressType + "State").value;
  let AddressCity = document.getElementById(addressType + "City").value;
  let AddressPin = document.getElementById(addressType + "Pin").value;
  if (addressType == "present") {
    msg = document.getElementById("errMsgPresentAddress");
  } else {
    msg = document.getElementById("errMsgPermanentAddress");
  }

  if (
    !AddressLine1 ||
    !AddressCountry ||
    !AddressState ||
    !AddressCity ||
    !AddressPin
  ) {
    msg.innerText = "*Fill all the required fields";
    if (!flagForFirstError) {
      flagForFirstError = msg.id;
    }
    return false;
  } else {
    msg.innerText = "";
    return true;
  }
}
function validate() {
  flagForFirstError = "";
  var result = [];
  validateTextById("firstName", textRegex);
  validateTextById("lastName", textRegex);
  validateTextById("email", emailRegex);
  validateGender();
  validateDate();
  validateDP();
  validateAddress("present");
  validateAddress("permanent");

  if (flagForFirstError) {
    document
      .getElementById(flagForFirstError)
      .scrollIntoView({ behavior: "smooth", block: "center" });
  } else {
    showDetailsAfterSubmit();
    return true;
  }
}

function showDetailsAfterSubmit() {
  const userObj = {};
  const detailElements = document.querySelectorAll(`[showDetails*="|"]`);
  detailElements.forEach((item) => {
    let valueString = item.getAttribute("showDetails");
    const attributes = valueString.split("|");
    if (attributes.length == 2) {
      if (!item.value) {
        item.value = "NA";
      }
      userObj[attributes[1]] = item.value;
      document.getElementById(
        attributes[1] + "Detail"
      ).innerHTML = `<span class="show-details-element">${
        attributes[0]
      }:</span> ${userObj[attributes[1]]}`;
    } else {
      if (attributes[2] == "radio") {
        const inputRadios = item.getElementsByTagName("input");
        userObj[attributes[1]] = inputRadios[0].checked
          ? inputRadios[0].value
          : inputRadios[1].value;
        document.getElementById(
          attributes[1] + "Detail"
        ).innerHTML = `<span class="show-details-element">${
          attributes[0]
        }:</span>  ${userObj[attributes[1]]}`;
      } else if (attributes[2] == "checkbox") {
        userObj[attributes[1]] = item.checked ? true : false;
        document.getElementById(
          attributes[1] + "Detail"
        ).innerHTML = `<span class="show-details-element">${
          attributes[0]
        }:</span> ${userObj[attributes[1]]}`;
      } else {
        console.log(item.files[0]);
        userObj[attributes[1]] = URL.createObjectURL(item.files[0]);
        displayImageById(item, attributes[1] + "Detail");
      }
    }
  });
  console.log(userObj);
  const showDetails = document.getElementsByClassName("show-details")[0];
  showDetails.style.display = "block ";
  showDetails.scrollIntoView({ behavior: "smooth", block: "center" });
}
