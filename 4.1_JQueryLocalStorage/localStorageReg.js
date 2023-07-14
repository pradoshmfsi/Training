let token = "";
$(document).ready(() => {
  fetchKey();
  $("#submitForm").click(validate);

  $(".form-container [isRequired*='|']").each((index, inputElement) => {
    $(inputElement).on("change", () => {
      validateTextById($(inputElement).attr("isRequired"));
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
});
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

function validateDP() {
  const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];
  const span = $("#dpTitle span");
  const file = $("#dp")[0];
  if (!file.files[0]) {
    span.text("*Required");
    return "#dpTitle";
  } else if (!allowedFormats.includes(file.files[0].type)) {
    span.text("*Invalid file format");
    return "#dpTitle";
  } else {
    span.text("*");
    return "";
  }
}

function displayImageById(file, id) {
  $(`#${id}`).attr("src", URL.createObjectURL(file.files[0]));
}

function displayProfilePicName() {
  const file = $("#dp")[0];
  let filename = $("#filename");

  if (!validateDP()) {
    filename.text(file.files[0].name);
    var icon = $("#iconForAddPic");
    icon.addClass("fa-solid fa-check");
    displayImageById(file, "profileImage");
  } else {
    filename.text("");
    $("#profileImage").attr(
      "src",
      "https://img.freepik.com/free-icon/user_318-159711.jpg?w=2000"
    );
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
  $(`#${addressType}State`).html(`<option value=""></option>`);

  if (country != "") {
    await getStates(country, addressType);
  }
  if (
    $("#ifPresentSameAsPermanent").prop("checked") &&
    $("#presentState").val().trim()
  ) {
    $("#permanentState").val($("#presentState").val()).trigger("change");
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

function populatePermanentAsPresent() {
  if ($("#ifPresentSameAsPermanent").prop("checked")) {
    $("[populateAddress*='|']").each(async (index, item) => {
      let attributes = $(item).attr("populateAddress").split("|");
      let presentValue = $(`#${attributes[0]}`).val();
      if (presentValue.trim()) {
        $(`#${attributes[1]}`).val(presentValue).trigger("change");
        if (attributes[attributes.length - 1] == "permanent") {
          await populateStates(attributes[2]);
        }
      }
    });
  }
}

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
    console.log(error);
  }
}

function validateGender() {
  let span = $("#genderTitle span");
  if (!$("#male").prop("checked") && !$("#female").prop("checked")) {
    span.text("*Required");
    return "#genderTitle";
  } else {
    span.text("*");
    return "";
  }
}

function validate() {
  let isCorrect = true;
  var result = [];

  $(`[isRequired*='[']`).each((index, item) => {
    result.push(validateTextById($(item).attr("isRequired")));
  });

  result = [
    ...result,
    validateGender(),
    validateTextById("#date|#dobTitle|#dobTitle span|."),
    validateDP(),
  ];

  //VALIDATE PRESENT AND PERMANENT ADDRESS USING USER DEFINED ATTRIBUTES
  $(`fieldset [isRequired*='|']`).each((index, item) => {
    result.push(validateTextById($(item).attr("isRequired")));
  });

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
  }
}

function showDetailsAfterSubmit() {
  const userObj = {};

  $(`[showDetails*="|"]`).each((index, item) => {
    const attributes = $(item).attr("showDetails").split("|");
    userObj[attributes[1]] = [attributes[0]];

    if (attributes[2] != "file") {
      if (attributes[2] == "text") {
        if ($(item).val() == "") {
          $(item).val("NA");
        }
        userObj[attributes[1]].push($(item).val());
      } else if (attributes[2] == "radio") {
        const inputRadios = $(item).children("input");
        userObj[attributes[1]].push(
          inputRadios[0].checked ? inputRadios[0].value : inputRadios[1].value
        );
      } else {
        userObj[attributes[1]].push(item.checked ? true : false);
      }
    } else {
      const reader = new FileReader();
      let image = item.files[0];
      reader.readAsDataURL(image);
      reader.addEventListener("load", () => {
        localStorage.setItem("user-registration-image", reader.result);
      });
    }
  });
  localStorage.setItem("user", JSON.stringify(userObj));
  window.location.href = "details.html";
}
