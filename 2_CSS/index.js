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

function popHobby() {
  let hobby = document.getElementById("hobby").value;
  if (hobby.trim()) {
    let list = document.getElementById("hobbyList");
    list.innerHTML = "";
    let hobbyArr = ["Singing", "Dancing", "Playing"];
    for (let i = 0; i < hobbyArr.length; i++) {
      if (!hobby.includes(hobbyArr[i])) {
        let option = document.createElement("option");
        option.value = hobby + "," + hobbyArr[i];
        list.appendChild(option);
      }
    }
  }
}

function func() {
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
  if (!patternName.test(fname)) {
    msg.innerText = "Enter a valid name";
  }
}
