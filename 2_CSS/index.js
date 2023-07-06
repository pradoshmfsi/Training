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
    console.log(data);
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
  if (file.files[0].name != "") {
    document.getElementById("filename").innerText = file.files[0].name;
    var card = document.getElementById("card");
    var icon = document.createElement("i");
    icon.className = "fa-solid fa-check";
    card.innerHTML = "";
    card.appendChild(icon);
    const path = URL.createObjectURL(file.files[0]);
    document.getElementById("profileImage").src = path;
    // const reader = new FileReader();
    // reader.readAsDataURL(file.files[0]);
    // reader.onload = function (e) {
    //   document.getElementById("profileImage").src = e.target.result;
    // };
    // image.src = file.value;
    // console.log(image.src);
    // document.getElementById("img-head").appendChild(image);
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
    state.value = pre_state;
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

function validate() {
  var patternName = /[A-Za-z]+/;
  let fname = document.getElementById("fname").value;
  let lname = document.getElementById("lname").value;
  let hobby = document.getElementById("hobby").value;
  let msg = document.getElementById("err");
  if (fname.trim() == "" || lname.trim() == "") {
    msg.innerText = "Enter a valid name";
  }
  if (!patternName.test(fname) || !patternName.test(lname)) {
    msg.innerText = "Enter a valid name";
  }
  if (hobby.trim() == "" || hobby.trim() == "") {
    msg.innerText = "Enter a valid name";
  }
  if (!patternName.test(hobby) || !patternName.test(hobby)) {
    msg.innerText = "Enter a valid hobby";
  }
}
