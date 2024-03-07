const months = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];

// will run when the document is ready
$(document).ready(function () {
  // once the document is ready we fetch a list of cars from the server
  ajaxCall("GET", server + "api/users", "", getSuccess, error);

  $("#dataTable").hide();
  $("#manageBtn").click(showDataTable);

  renderMonthsOptions();

  $("#dtlMonths").change(checkFromList);

  $("#formReport").submit(generateReport);
});

function renderMonthsOptions() {
  let monthStr = months.map((m, i) => `<option value=${i + 1}>${m}</option>`);

  $("#months").append(monthStr);
}

function generateReport(e) {
  let month = parseInt(e.target.dtlMonths.value);

  ajaxCall(
    "GET",
    server + `api/Vacations/report/${month}`,
    "",
    sReportCB,
    error
  );

  $("#tableCaption").html(`Average cost per night for ${months[month - 1]}`);
  return false;
}

function sReportCB(data) {
  console.log("data", data);
  let dataStr = data.map((row) => {
    return ` 
        <tr >
            <th scope="row">${row.city}</td>
            <td>${row.averagePerNight}</td>
        </tr>`;
  });

  $("#reportBody").html(dataStr);

  $("#reportDiv").removeClass("hide");
}

function checkFromList(e) {
  let month = e.target;
  if (!months.find((m, i) => i + 1 == month.value)) {
    month.validity.valid = false;
    month.setCustomValidity("Pick a valid monthe");
  } else {
    month.validity.valid = true;
    month.setCustomValidity("");
  }
}

function showDataTable() {
  $("#dataTable").show();
  $("#manageBtn").removeClass("d-block");
  $("#manageBtn").addClass("d-none");
}

// wire all the buttons to their functions
function buttonEvents() {
  $(document).on("click", ".editBtn", function () {
    markSelected(this);
    let email = this.getAttribute("data-UserEmail");
    $(`[data-userActive="check${email}"]`).prop("disabled", false);
    $(`[data-userEmail="${email}"]`).toggle();
  });

  $(document).on("click", ".saveBtn", function () {
    markSelected(this);
    let email = this.getAttribute("data-UserEmail");
    $(`[data-userActive="check${email}"]`).prop("disabled", true);
    user = users.find((u) => u.email == email);
    $(`[data-userEmail="${email}"]`).toggle();
    onSubmitFunc(user);
  });
}

// mark the selected row
function markSelected(btn) {
  $("#usersTable tr").removeClass("selected"); // remove seleced class from rows that were selected before
  row = btn.parentNode.parentNode; // button is in TD which is in Row
  row.className = "selected"; // mark as selected
}

function onSubmitFunc(user) {
  user.isActive = $(`[data-userActive="check${user.email}"]`).is(":checked");

  // update a new Car record to the server
  ajaxCall(
    "PUT",
    server + "api/users",
    JSON.stringify(user),
    updateSuccess,
    error
  );
  return false;
}

// success callback function after update
function updateSuccess(usersdata) {
  tbl.clear();
  users = usersdata;
  redrawTable(tbl, usersdata);
  swal("Updated Successfuly!", "Great Job", "success");
}

// success callback function after delete
function deleteSuccess(usersdata) {
  tbl.clear();
  redrawTable(tbl, usersdata);
  buttonEvents(); // after redrawing the table, we must wire the new buttons
  swal("Deleted Successfuly!", "Great Job", "success");
}

// redraw a datatable with new data
function redrawTable(tbl, data) {
  tbl.clear();
  for (var i = 0; i < data.length; i++) {
    tbl.row.add(data[i]);
  }
  tbl.draw();
}

// this function is activated in case of a success
function getSuccess(usersdata) {
  users = usersdata; // keep the cars array in a global variable;
  try {
    tbl = $("#usersTable").DataTable({
      data: usersdata,
      pageLength: 5,
      columns: [
        { data: "firstName" },
        { data: "familyName" },
        { data: "email" },
        {
          data: "isActive",
          render: function (data, type, row, meta) {
            if (data == true)
              return `<input data-userActive="check${row.email}" type="checkbox" checked disabled="disabled" />`;
            else
              return `<input data-userActive="check${row.email}" type="checkbox" disabled="disabled"/>`;
          },
        },
        {
          render: function (data, type, row, meta) {
            let dataUser = "data-UserEmail='" + row.email + "'";

            editBtn =
              "<button type='button' class = 'editBtn btn btn-success' " +
              dataUser +
              "> Edit </button>";
            saveBtn =
              "<button style='display: none'  type='button' class = 'saveBtn btn btn-warning' " +
              dataUser +
              "> Save </button>";
            return editBtn + saveBtn;
          },
        },
      ],
    });
    buttonEvents();
  } catch (err) {
    alert(err);
  }
}

// this function is activated in case of a failure
function error(err) {
  console.log("err", err);
  swal("Error ", err.responseText, "error");
}
