let token = "";
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
      let option = document.createElement("option");
      let option2 = document.createElement("option");

      option.value = arr[i].country_name;
      option.innerText = arr[i].country_name;

      option2.value = arr[i].country_name;
      option2.innerText = arr[i].country_name;

      presentCountry.appendChild(option);
      permanentCountry.appendChild(option2);
    }
  } catch (error) {
    console.error(error);
  }
}

fetchKey();
function displayProfilePicName() {
  const file = document.getElementById("dp");
  const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];

  if (file.files[0].name != "") {
    if (allowedFormats.includes(file.files[0].type)) {
      let filename = document.getElementById("filename");
      filename.innerText = file.files[0].name;
      filename.style.color = "black";

      var card = document.getElementById("card");
      var icon = document.createElement("i");
      icon.className = "fa-solid fa-check";
      card.innerHTML = "";
      card.appendChild(icon);

      const path = URL.createObjectURL(file.files[0]);
      document.getElementById("profileImage").src = path;
    } else {
      let filename = document.getElementById("filename");
      filename.style.color = "red";
      filename.innerText = "Invalid file format(Accepts only jpg, png)";
    }
  }
  span = document.getElementById("dpTitle").getElementsByTagName("span")[0];
  if (!file.files[0]) {
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
}
async function getStatesPresent(country) {
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
    let state = document.getElementById("presentState");

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
async function getStatesPermanent(country) {
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
    let state = document.getElementById("permanentState");

    for (let i = 0; i < arr.length; i++) {
      var option = document.createElement("option");
      option.value = arr[i].state_name;
      option.innerText = arr[i].state_name;
      state.appendChild(option);
    }

    let address = document.getElementById("ifPresentSameAsPermanent");
    if (address.checked) {
      let presentAddressState = document.getElementById("presentState").value;
      document.getElementById("permanentState").value = presentAddressState;
    }
  } catch (error) {
    console.log(error);
  }
}
async function populateStatesPresent() {
  let country = document.getElementById("presentCountry").value;
  document.getElementById("presentState").value = "";
  if (country != "") {
    getStatesPresent(country);
  }
}
function populateStatesPermanent() {
  let country = document.getElementById("permanentCountry").value;
  document.getElementById("permanentState").value = "";
  if (country != "") {
    getStatesPermanent(country);
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
    // let presentAddressState = document.getElementById("presentState").value;
    let presentAddressCity = document.getElementById("presentCity").value;
    let presentAddressPin = document.getElementById("presentPin").value;

    permanentAddressLine1.value = presentAddressLine1;
    permanentAddressLine2.value = presentAddressLine2;
    permanentCountry.value = presentAddressCountry;
    populateStatesPermanent();

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

function validateTextById(id) {
  let data = document.getElementById(id).value;
  console.log(data);
  let text = id + "Title";
  let span = document.getElementById(text).getElementsByTagName("span")[0];
  if (data == "" || data.trim() == "") {
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
}
function validateDate() {
  span = document.getElementById("dobTitle").getElementsByTagName("span")[0];
  let date = document.getElementById("date").value;
  if (date == "") {
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
}

function validateGender() {
  span = document.getElementById("genderTitle").getElementsByTagName("span")[0];
  let male = document.getElementById("male");
  let female = document.getElementById("female");
  if (!male.checked && !female.checked) {
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
}

function validatePresentAddress() {
  let presentAddressLine1 = document.getElementById(
    "presentAddressLine1"
  ).value;
  let presentAddressLine2 = document.getElementById(
    "presentAddressLine2"
  ).value;
  let presentAddressCountry = document.getElementById("presentCountry").value;
  let presentAddressState = document.getElementById("presentState").value;
  let presentAddressCity = document.getElementById("presentCity").value;
  let presentAddressPin = document.getElementById("presentPin").value;
  let msg = document.getElementById("errMsgPresentAddress");
  if (
    !presentAddressLine1 ||
    !presentAddressLine2 ||
    !presentAddressCountry ||
    !presentAddressState ||
    !presentAddressCity ||
    !presentAddressPin
  ) {
    msg.innerText = "*Fill all the required fields";
  } else {
    msg.innerText = "";
  }
}

function validatePermanentAddress() {
  let permanentAddressLine1 = document.getElementById(
    "permanentAddressLine1"
  ).value;
  let permanentAddressLine2 = document.getElementById(
    "permanentAddressLine2"
  ).value;
  let permanentCountry = document.getElementById("permanentCountry").value;
  let permanentState = document.getElementById("permanentState").value;
  let pemanentCity = document.getElementById("permanentCity").value;
  let permanentPin = document.getElementById("permanentPin").value;
  let msgPermanent = document.getElementById("errMsgPermanentAddress");
  if (
    !permanentAddressLine1 ||
    !permanentAddressLine2 ||
    !permanentCountry ||
    !permanentState ||
    !pemanentCity ||
    !permanentPin
  ) {
    msgPermanent.innerText = "*Fill all the required fields";
  } else {
    msgPermanent.innerText = "";
  }
}

function validate(event) {
  event.preventDefault();
  var patternName = /[A-Za-z ]+/;
  let firstName = document.getElementById("firstName").value;
  let lastName = document.getElementById("lastName").value;
  // let hobby = document.getElementById("hobby").value;
  let flagForFirstError = "";

  //FIRST NAME VALIDATION
  let span = document
    .getElementById("firstNameTitle")
    .getElementsByTagName("span")[0];

  if (firstName.trim() == "") {
    if (!flagForFirstError) {
      flagForFirstError = "firstName";
    }
    span.innerText = "*Required";
  } else if (!patternName.test(firstName)) {
    if (!flagForFirstError) {
      flagForFirstError = "firstName";
    }
    span.innerText = "*Not valid";
  } else {
    span.innerText = "*";
  }

  span = document
    .getElementById("lastNameTitle")
    .getElementsByTagName("span")[0];

  if (lastName.trim() == "") {
    //LAST NAME VALIDATION
    if (!flagForFirstError) {
      flagForFirstError = "lastName";
    }
    span.innerText = "*Required";
  } else if (!patternName.test(lastName)) {
    if (!flagForFirstError) {
      flagForFirstError = "lastName";
    }
    span.innerText = "*Not valid";
  } else {
    span.innerText = "*";
  }

  //EMAIL VALIDATION
  let email = document.getElementById("email").value;
  let emailPattern = /^[\w.%+-]+@[\w.-]+\.[a-zA-Z]{2,}$/;

  span = document.getElementById("emailTitle").getElementsByTagName("span")[0];

  if (email.trim() == "") {
    if (!flagForFirstError) {
      flagForFirstError = "emailTitle";
    }
    span.innerText = "*Required";
  } else if (!emailPattern.test(email)) {
    if (!flagForFirstError) {
      flagForFirstError = "emailTitle";
    }
    span.innerText = "*Invalid email";
  } else {
    span.innerText = "*";
  }

  //GENDER VALIDATION
  span = document.getElementById("genderTitle").getElementsByTagName("span")[0];
  let male = document.getElementById("male");
  let female = document.getElementById("female");
  if (!male.checked && !female.checked) {
    if (!flagForFirstError) {
      flagForFirstError = "genderTitle";
    }

    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
  span = document.getElementById("dobTitle").getElementsByTagName("span")[0];
  let date = document.getElementById("date").value;
  if (date == "") {
    if (!flagForFirstError) {
      flagForFirstError = "date";
    }
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }

  // //HOBBY VALIDATION
  // if (hobby.trim() == "") {
  //   if (!flagForFirstError) {
  //     flagForFirstError = "hobby";
  //   }
  //   let span = document.createElement("span");
  //   span.innerText = "Required";
  //   document.getElementById("hobbyTitle").appendChild(span);
  // }

  //DP VALIDATION
  span = document.getElementById("dpTitle").getElementsByTagName("span")[0];
  const file = document.getElementById("dp");
  if (!file.files[0]) {
    if (!flagForFirstError) {
      flagForFirstError = "dp";
    }
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }

  //Present Address
  let presentAddressLine1 = document.getElementById(
    "presentAddressLine1"
  ).value;
  let presentAddressLine2 = document.getElementById(
    "presentAddressLine2"
  ).value;
  let presentAddressCountry = document.getElementById("presentCountry").value;
  let presentAddressState = document.getElementById("presentState").value;
  let presentAddressCity = document.getElementById("presentCity").value;
  let presentAddressPin = document.getElementById("presentPin").value;
  let msg = document.getElementById("errMsgPresentAddress");
  if (
    !presentAddressLine1 ||
    !presentAddressLine2 ||
    !presentAddressCountry ||
    !presentAddressState ||
    !presentAddressCity ||
    !presentAddressPin
  ) {
    if (!flagForFirstError) {
      flagForFirstError = "errMsgPresentAddress";
    }

    msg.innerText = "*Fill all the required fields";
  } else {
    msg.innerText = "";
  }

  let permanentAddressLine1 = document.getElementById(
    "permanentAddressLine1"
  ).value;
  let permanentAddressLine2 = document.getElementById(
    "permanentAddressLine2"
  ).value;
  let permanentCountry = document.getElementById("permanentCountry").value;
  let permanentState = document.getElementById("permanentState").value;
  let permanentCity = document.getElementById("permanentCity").value;
  let permanentPin = document.getElementById("permanentPin").value;
  let msgPermanent = document.getElementById("errMsgPermanentAddress");
  if (
    !permanentAddressLine1 ||
    !permanentAddressLine2 ||
    !permanentCountry ||
    !permanentState ||
    !permanentCity ||
    !permanentPin
  ) {
    if (!flagForFirstError) {
      flagForFirstError = "errMsgPermanentAddress";
    }

    msgPermanent.innerText = "*Fill all the required fields";
  } else {
    msgPermanent.innerText = "";
  }

  if (flagForFirstError) {
    document
      .getElementById(flagForFirstError)
      .scrollIntoView({ behavior: "smooth", block: "center" });
  } else {
    return true;
  }
}

// let inputs = document.getElementsByTagName("input");
// for(let i=0;i<inputs.length;i++){
//   inputs[i].addEventListener("focusout", validate());
// }
