$(document).ready(function () {
  isUpdate = window.location.search.slice(1) === "update";

  $("#userForm").submit(addNewUser);

  $("#loginForm").submit(loginUser);

  $("#log-out").click(logoutUser);

  isUpdate && updateRegOrEdit();
});

function updateRegOrEdit() {
  currentUser = JSON.parse(sessionStorage.getItem("connectUser"));

  document.querySelector(".jumbotron .display-4 b").innerHTML =
    "Edit user details";

  document.querySelector("button[type='submit']").innerHTML = "Update user";

  let form = $("#userForm")[0];

  form.email.setAttribute('disabled',true);

  form.firstName.value = currentUser.firstName;
  form.lastName.value = currentUser.familyName;
  form.email.value = currentUser.email;
  form.password.value = currentUser.password;
}

//Register user - Update user
function addNewUser(e) {
  let data = e.target;

  let newUser = {
    FirstName: data.firstName.value,
    FamilyName: data.lastName.value,
    Email: data.email.value,
    Password: data.password.value,
    isActive:true
  };

  if (isUpdate) {
    ajaxCall(
      "PUT",
      server + `api/Users/${currentUser.email}`,
      JSON.stringify(newUser),
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

  window.location.href =
    res.email === "admin@gmail.com" ? "./admin.html" : "./flats.html";
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
  window.location.href = "./flats.html";
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
