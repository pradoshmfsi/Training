fetch("https://www.universal-tutorial.com/api/countries/", {
  method: "GET",
  headers: {
    Authorization:
      "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJtZnNpLnByYWRvc2hzQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6Ik1ZUEFPS2J2b0hfUV9CaVBIWU5vMlc4VlBRRVAtTTNfN0FLREgyMlpjUkVyRVpUb0M0WkdyU0c1QUhxX0w0bl9XR0kifSwiZXhwIjoxNjg4NzE2NTI3fQ.Np0okMWDxN_87UPbMEgIS3Vo0XpIDH9gAWh0R4KFYTE",
    Accept: "application/json",
  },
})
  .then((response) => response.json())
  .then((data) => {
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
    // let country = document.getElementById("countryList");
    // for (let i = 0; i < arr.length; i++) {
    //   var option = document.createElement("option");
    //   option.value = arr[i].country_name;
    //   country.appendChild(option);
    // }
  })
  .catch((error) => {
    console.error(error);
  });

function dispName() {
  const file = document.getElementById("dp");
  const allowedFormats = ["image/jpeg", "image/jpg", "image/png"];
  if (file.files[0].name != "") {
    if (allowedFormats.includes(file.files[0].type)) {
      document.getElementById("filename").innerText = file.files[0].name;
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

function popStates() {
  let country = document.getElementById("country").value;
  console.log(country);
  document.getElementById("state").value = "";
  if (country != "") {
    let url = "https://www.universal-tutorial.com/api/states/" + country;
    fetch(url, {
      method: "GET",
      headers: {
        Authorization:
          "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJtZnNpLnByYWRvc2hzQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6Ik1ZUEFPS2J2b0hfUV9CaVBIWU5vMlc4VlBRRVAtTTNfN0FLREgyMlpjUkVyRVpUb0M0WkdyU0c1QUhxX0w0bl9XR0kifSwiZXhwIjoxNjg4NzE2NTI3fQ.Np0okMWDxN_87UPbMEgIS3Vo0XpIDH9gAWh0R4KFYTE",
        Accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        let arr = data;
        let state = document.getElementById("state");
        for (let i = 0; i < arr.length; i++) {
          var option = document.createElement("option");
          option.value = arr[i].state_name;
          option.innerText = arr[i].state_name;
          state.appendChild(option);
        }
        // let state = document.getElementById("stateList");
        // state.innerHTML = "";
        // for (let i = 0; i < arr.length; i++) {
        //   var option = document.createElement("option");
        //   option.value = arr[i].state_name;
        //   state.appendChild(option);
        // }
      })
      .catch((error) => {
        console.error(error);
      });
  }
}
function popStates2() {
  let country = document.getElementById("country2").value;
  document.getElementById("state2").value = "";
  if (country != "") {
    let url = "https://www.universal-tutorial.com/api/states/" + country;
    fetch(url, {
      method: "GET",
      headers: {
        Authorization:
          "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7InVzZXJfZW1haWwiOiJtZnNpLnByYWRvc2hzQGdtYWlsLmNvbSIsImFwaV90b2tlbiI6Ik1ZUEFPS2J2b0hfUV9CaVBIWU5vMlc4VlBRRVAtTTNfN0FLREgyMlpjUkVyRVpUb0M0WkdyU0c1QUhxX0w0bl9XR0kifSwiZXhwIjoxNjg4NzE2NTI3fQ.Np0okMWDxN_87UPbMEgIS3Vo0XpIDH9gAWh0R4KFYTE",
        Accept: "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
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
      })
      .catch((error) => {
        console.error(error);
      });
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

function validate(event) {
  event.preventDefault();
  var patternName = /[A-Za-z ]+/;
  let fname = document.getElementById("fname").value;
  let lname = document.getElementById("lname").value;
  let hobby = document.getElementById("hobby").value;
  let flag_id = "";

  //FIRST NAME VALIDATION
  if (fname.trim() == "" || !patternName.test(fname)) {
    if (!flag_id) {
      flag_id = "fname";
    }
    let span = document.createElement("span");
    span.innerText = "*Not valid";
    document.getElementById("fname_title").appendChild(span);
  }

  //LAST NAME VALIDATION
  if (lname.trim() == "" || !patternName.test(lname)) {
    if (!flag_id) {
      flag_id = "lname";
    }
    let span = document.createElement("span");
    span.innerText = "*Not valid";
    document.getElementById("lname_title").appendChild(span);
  }

  //EMAIL VALIDATION
  let email = document.getElementById("email").value;
  let emailPattern = /^[\w.%+-]+@[\w.-]+\.[a-zA-Z]{2,}$/;
  if (email.trim() == "") {
    if (!flag_id) {
      flag_id = "email_title";
    }
    let span = document.createElement("span");
    span.innerText = "*Required";
    document.getElementById("email_title").appendChild(span);
  } else if (!emailPattern.test(email)) {
    if (!flag_id) {
      flag_id = "email_title";
    }
    let span = document.createElement("span");
    span.innerText = "*Invalid email";
    document.getElementById("email_title").appendChild(span);
  }
  //GENDER VALIDATION
  let male = document.getElementById("male");
  let female = document.getElementById("female");
  if (!male.checked && !female.checked) {
    if (!flag_id) {
      flag_id = "gender_title";
    }
    let span = document.createElement("span");
    span.innerText = "*Required";
    document.getElementById("gender_title").appendChild(span);
  }

  let date = document.getElementById("date").value;
  if (date == "") {
    if (!flag_id) {
      flag_id = "date";
    }
    let span = document.createElement("span");
    span.innerText = "*Required";
    document.getElementById("dob_title").appendChild(span);
  }

  //HOBBY VALIDATION
  if (hobby.trim() == "") {
    if (!flag_id) {
      flag_id = "hobby";
    }
    let span = document.createElement("span");
    span.innerText = "*Required";
    document.getElementById("hobby_title").appendChild(span);
  }

  //DP VALIDATION

  const file = document.getElementById("dp");
  if (!file.files[0]) {
    if (!flag_id) {
      flag_id = "dp";
    }
    let span = document.createElement("span");
    span.innerText = "*Required";
    document.getElementById("dp_title").appendChild(span);
  }

  if (flag_id) {
    document
      .getElementById(flag_id)
      .scrollIntoView({ behavior: "smooth", block: "center" });
  } else {
    return true;
  }
}
