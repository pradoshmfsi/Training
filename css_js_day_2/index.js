const arr2 = [
  ["State11", "State12", "State13"],
  ["State21", "State22", "State23"],
  ["State31", "State32", "State33"],
];
const arr = ["Country1", "Country2", "Country3"];
let country = document.getElementById("countryList");
for (let i = 0; i < arr.length; i++) {
  var option = document.createElement("option");
  option.value = arr[i];
  country.appendChild(option);
}

function dispName() {
  const file = document.getElementById("dp").files[0].name;
  if (file != "") {
    document.getElementById("filename").innerText = file;
    var card = document.getElementById("card");
    var icon = document.createElement("i");
    icon.className = "fa-solid fa-check";
    card.innerHTML = "";
    card.appendChild(icon);
  }
}

function popStates() {
  let country = document.getElementById("country").value;
  let state = document.getElementById("stateList");
  state.innerHTML = "";
  if (country != "") {
    for (let i = 0; i < arr.length; i++) {
      if (arr[i] == country) {
        for (let j = 0; j < arr2[i].length; j++) {
          var option = document.createElement("option");
          option.value = arr2[i][j];
          state.appendChild(option);
        }
      }
    }
  }
}
function popStates2() {
  let country = document.getElementById("country2").value;
  let state = document.getElementById("stateList2");
  state.innerHTML = "";
  if (country != "") {
    for (let i = 0; i < arr.length; i++) {
      if (arr[i] == country) {
        for (let j = 0; j < arr2[i].length; j++) {
          var option = document.createElement("option");
          option.value = arr2[i][j];
          state.appendChild(option);
        }
      }
    }
  }
}
function func() {
  var address = document.getElementById("addr");
  var line1 = document.getElementById("al1p");
  var line2 = document.getElementById("al2p");
  var country = document.getElementById("country2");
  var state = document.getElementById("state2");
  var city = document.getElementById("city2");
  var pin = document.getElementById("pin2");
  if (address.checked == true) {
    var pre_line1 = document.getElementById("al1").value;
    var pre_line2 = document.getElementById("al2").value;
    var pre_country = document.getElementById("country").value;
    var pre_state = document.getElementById("state").value;
    var pre_city = document.getElementById("city").value;
    var pre_pin = document.getElementById("pin").value;

    line1.value = pre_line1;
    line2.value = pre_line2;
    country.value = pre_country;
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
