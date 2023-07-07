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

    let country = document.getElementById("country");
    let country2 = document.getElementById("country2");
    for (let i = 0; i < arr.length; i++) {
      let option = document.createElement("option");
      let option2 = document.createElement("option");

      option.value = arr[i].country_name;
      option.innerText = arr[i].country_name;

      option2.value = arr[i].country_name;
      option2.innerText = arr[i].country_name;

      country.appendChild(option);
      country2.appendChild(option2);
    }
  } catch (error) {
    console.error(error);
  }
}

fetchKey();
function dispName() {
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
}
async function getStates(country) {
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
    let state = document.getElementById("state");
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
async function getStates2(country) {
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
    let state = document.getElementById("state2");
    for (let i = 0; i < arr.length; i++) {
      var option = document.createElement("option");
      option.value = arr[i].state_name;
      option.innerText = arr[i].state_name;
      state.appendChild(option);
    }
    let address = document.getElementById("addr");
    if (address.checked) {
      let pre_state = document.getElementById("state").value;
      document.getElementById("state2").value = pre_state;
    }
  } catch (error) {
    console.log(error);
  }
}
async function popStates() {
  let country = document.getElementById("country").value;
  console.log(country);
  document.getElementById("state").value = "";
  if (country != "") {
    console.log(country + "Hi");
    getStates(country);
  }
}
function popStates2() {
  let country = document.getElementById("country2").value;
  document.getElementById("state2").value = "";
  if (country != "") {
    getStates2(country);
  }
}

function popHobby() {
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

async function func() {
  let address = document.getElementById("addr");
  let line1 = document.getElementById("al1p");
  let line2 = document.getElementById("al2p");
  let country = document.getElementById("country2");
  let state = document.getElementById("state2");
  let city = document.getElementById("city2");
  let pin = document.getElementById("pin2");
  if (address.checked == true) {
    let pre_line1 = document.getElementById("al1").value;
    let pre_line2 = document.getElementById("al2").value;
    let pre_country = document.getElementById("country").value;
    let pre_state = document.getElementById("state").value;
    let pre_city = document.getElementById("city").value;
    let pre_pin = document.getElementById("pin").value;

    line1.value = pre_line1;
    line2.value = pre_line2;
    country.value = pre_country;
    popStates2();

    city.value = pre_city;
    pin.value = pre_pin;
  } else {
    line1.value = "";
    line2.value = "";
    country.value = "";
    state.value = "";
    city.value = "";
    pin.value = "";
  }
}

// function validateById(id) {
//   let data = document.getElementById(id).value;
//   console.log(data);
//   let text = id + "_title";
//   let span = document.getElementById(text).getElementsByTagName("span")[0];
//   if (data == "" || data.trim() == "") {
//     span.innerText = "*Required";
//   } else {
//     span.innerText = "*";
//   }
// }

function validate(event) {
  event.preventDefault();
  var patternName = /[A-Za-z ]+/;
  let fname = document.getElementById("fname").value;
  let lname = document.getElementById("lname").value;
  let hobby = document.getElementById("hobby").value;
  let flag_id = "";

  //FIRST NAME VALIDATION
  let span = document
    .getElementById("fname_title")
    .getElementsByTagName("span")[0];
  if (fname.trim() == "") {
    if (!flag_id) {
      flag_id = "fname";
    }
    span.innerText = "*Required";
  } else if (!patternName.test(fname)) {
    if (!flag_id) {
      flag_id = "fname";
    }
    span.innerText = "*Not valid";
  } else {
    span.innerText = "*";
  }

  span = document.getElementById("lname_title").getElementsByTagName("span")[0];

  if (lname.trim() == "") {
    //LAST NAME VALIDATION
    if (!flag_id) {
      flag_id = "lname";
    }
    span.innerText = "*Required";
  } else if (!patternName.test(lname)) {
    if (!flag_id) {
      flag_id = "lname";
    }
    span.innerText = "*Not valid";
  } else {
    span.innerText = "*";
  }

  //EMAIL VALIDATION
  let email = document.getElementById("email").value;
  let emailPattern = /^[\w.%+-]+@[\w.-]+\.[a-zA-Z]{2,}$/;
  span = document.getElementById("email_title").getElementsByTagName("span")[0];
  if (email.trim() == "") {
    if (!flag_id) {
      flag_id = "email_title";
    }
    span.innerText = "*Required";
  } else if (!emailPattern.test(email)) {
    if (!flag_id) {
      flag_id = "email_title";
    }
    span.innerText = "*Invalid email";
  } else {
    span.innerText = "*";
  }
  //GENDER VALIDATION
  span = document
    .getElementById("gender_title")
    .getElementsByTagName("span")[0];
  let male = document.getElementById("male");
  let female = document.getElementById("female");
  if (!male.checked && !female.checked) {
    if (!flag_id) {
      flag_id = "gender_title";
    }

    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }
  span = document.getElementById("dob_title").getElementsByTagName("span")[0];
  let date = document.getElementById("date").value;
  if (date == "") {
    if (!flag_id) {
      flag_id = "date";
    }
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }

  // //HOBBY VALIDATION
  // if (hobby.trim() == "") {
  //   if (!flag_id) {
  //     flag_id = "hobby";
  //   }
  //   let span = document.createElement("span");
  //   span.innerText = "Required";
  //   document.getElementById("hobby_title").appendChild(span);
  // }

  //DP VALIDATION
  span = document.getElementById("dp_title").getElementsByTagName("span")[0];
  const file = document.getElementById("dp");
  if (!file.files[0]) {
    if (!flag_id) {
      flag_id = "dp";
    }
    span.innerText = "*Required";
  } else {
    span.innerText = "*";
  }

  //Present Address
  let pre_line1 = document.getElementById("al1").value;
  // let pre_line2 = document.getElementById("al2").value;
  let pre_country = document.getElementById("country").value;
  let pre_state = document.getElementById("state").value;
  let pre_city = document.getElementById("city").value;
  let pre_pin = document.getElementById("pin").value;

  if (!pre_line1 || !pre_country || !pre_state || !pre_city || !pre_pin) {
    if (!flag_id) {
      flag_id = "present_address";
    }
    let msg = document.getElementById("present_address");
    msg.innerText = "*Fill all the required fields";
  } else {
    msg.innerText = "";
  }

  let line1 = document.getElementById("al1p").value;
  // let line2 = document.getElementById("al2p").value;
  let country = document.getElementById("country2").value;
  let state = document.getElementById("state2").value;
  let city = document.getElementById("city2").value;
  let pin = document.getElementById("pin2").value;
  if (!line1 || !country || !state || !city || !pin) {
    if (!flag_id) {
      flag_id = "permanent_address";
    }
    let msg2 = document.getElementById("permanent_address");
    msg2.innerText = "*Fill all the required fields";
  } else {
    msg2.innerText = "";
  }

  if (flag_id) {
    document
      .getElementById(flag_id)
      .scrollIntoView({ behavior: "smooth", block: "center" });
  } else {
    return true;
  }
}

// let inputs = document.getElementsByTagName("input");
// for(let i=0;i<inputs.length;i++){
//   inputs[i].addEventListener("focusout", validate());
// }
