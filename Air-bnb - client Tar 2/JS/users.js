$(document).ready(function () {
  //server = (window.location.hostname === "localhost" || window.location.hostname === `7014` ? `https://localhost:7014/` : `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`);
  server = "https://localhost:7014/";

  isUpdate = window.location.search.slice(1) === "update";

  $("#userForm").submit(addNewUser);

  $("#loginForm").submit(loginUser);

  $("#log-out").click(logoutUser);

  isUpdate && updateRegOrEdit();
});

function updateRegOrEdit() {
  let currentUser = JSON.parse(sessionStorage.getItem("connectUser"));

  document.querySelector(".jumbotron .display-4 b").innerHTML =
    "Edit user details";

  document.querySelector("button[type='submit']").innerHTML = "Update user";

  let form = $("#userForm")[0];

  form.firstName.value = currentUser.FirstName;
  form.lastName.value = currentUser.FamilyName;
  form.email.value = currentUser.Email;
  form.password.value = currentUser.Password;
}

//Register user - Update user
function addNewUser(e) {
  let data = e.target;

  let newUser = {
    FirstName: data.firstName.value,
    FamilyName: data.lastName.value,
    Email: data.email.value,
    Password: data.password.value,
  };

  if (isUpdate) {
    ajaxCall(
      "PUT",
      server + "api/Users/login",
      JSON.stringify(loginUser),
      sUpdateCB,
      eUpdateCB
    );
  } else {
    ajaxCall(
      "POST",
      server + "api/Users",
      JSON.stringify(newUser),
      sInsertCB,
      eInsertCB
    );
  }

  e.target.reset();
  return false;
}

function sInsertCB(res) {
  console.log("res", res);
  swal("User has been registered!", "Great Job", "success");
  sessionStorage.setItem("connectUser", JSON.stringify(res));
  window.location.href = "./Flats.html";
}
function eInsertCB(err) {
  console.log("err");
  alert(err.responseText);
}

//Login user
function loginUser(e) {
  let data = e.target;
  e.preventDefault();
  let loginUser = {
    FirstName: "",
    FamilyName: "",
    Email: data.email.value,
    Password: data.password.value,
  };
  ajaxCall(
    "POST",
    server + "api/Users/login",
    JSON.stringify(loginUser),
    sLoginCB,
    eLoginCB
  );

  e.target.reset();
  return false;
}

function sLoginCB(res) {
  console.log("res", res);
  //swal("User has been registered!", "Great Job", "success");
  sessionStorage.setItem("connectUser", JSON.stringify(res));
  window.location.href = "./flats.html";
}
function eLoginCB(err) {
  console.log("err");
  alert(err.responseText);
}

// update callbacks

function sUpdateCB(res) {
  console.log("res", res);
  //swal("User has been registered!", "Great Job", "success");
  sessionStorage.setItem("connectUser", JSON.stringify(res));
}
function eUpdateCB(err) {
  console.log("err");
  alert(err.responseText);
}

//Logout user
function logoutUser() {
  sessionStorage.removeItem("connectUser");
  window.location.href = "./flats.html";
}
