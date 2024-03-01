$(document).ready(function () {
  //server = (window.location.hostname === "localhost" || window.location.hostname === `7014` ? `https://localhost:7014/` : `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`);
  server = "https://localhost:7014/";
  $("#userForm").submit(addNewUser);

  $("#loginForm").submit(loginUser);
});

function addNewUser(e) {
  let data = e.target;

  let newUser = {
    FirstName: data.firstName.value,
    FamilyName: data.lastName.value,
    Email: data.email.value,
    Password: data.password.value,
  };
  ajaxCall(
    "POST",
    server + "api/Users",
    JSON.stringify(newUser),
    sInsertCB,
    eInsertCB
  );
  e.target.reset();
  return false;
}

function sInsertCB(res) {
  console.log("res", res);
  swal("User has been registered!", "Great Job", "success");
  sessionStorage.setItem('conectUser',res);
}
function eInsertCB(err) {
  console.log("err");
  alert(err.responseText);
}

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
    sLInsertCB,
    eLInsertCB
  );
  e.target.reset();
  return false;
}

function sLInsertCB(res) {
  console.log("res", res);
  //swal("User has been registered!", "Great Job", "success");
}
function eLInsertCB(err) {
  console.log("err");
  alert(err.responseText);
}
