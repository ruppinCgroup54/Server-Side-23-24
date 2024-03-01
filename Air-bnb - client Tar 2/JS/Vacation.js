$(document).ready(function () {
  server = `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`;

  getFlatIdFromQueryString();

  $(`#vacation-form`).submit(submitNewVacation);

  $(`input[type="date"]`).change(checkDate);

  $("#getVacations").click(getVacations);
});

function getFlatIdFromQueryString() {
  const searchParams = new URLSearchParams(window.location.search);
  // swal(searchParams.get('flatId'),"great", 'success'); // price_descending
  $("#flatId").val(searchParams.get("flatId"));
}

function submitNewVacation(e) {
  console.log("e.target", e.target);

  let myForm = e.target;

  let newVac = {
    id:
      Math.random().toString(36).slice(2, 12) + new Date().getTime().toString(),
    userId: myForm.userId.value,
    flatId: myForm.flatId.value,
    startDate: myForm.inpStartDate.value,
    endDate: myForm.inpEndDate.value,
  };

  ajaxCall(
    "POST",
    server + "api/Vacations",
    JSON.stringify(newVac),
    sInsertVacationsCB,
    eInsertVacationsCB
  );

  console.log("newVac", newVac);
  return false;
}

function sInsertVacationsCB(data) {
  console.log("data", data);
  swal("Vacation has been Added!", "Great Job", "success");
}

function eInsertVacationsCB(error) {
  alert("error", error);
}

function checkDate() {
  let start = $("#inpStartDate");
  let end = $("#inpEndDate");

  let sDate = new Date(start.val());
  let eDate = new Date(end.val());

  if ((eDate - sDate) / 8.64e7 > 20 || eDate <= sDate) {
    this.classList.add("is-invalid");
    let errorToShow =
      eDate <= sDate
        ? "End date must be bigger then the start date"
        : "Vacation can't be longer than 20 days";
    this.nextElementSibling.innerHTML = errorToShow;
    return;
  } else {
    start.removeClass("is-invalid");
    end.removeClass("is-invalid");
  }
}

function getVacations() {
  let idInp = $("#userId").val();

  if (idInp === "") {
    alert("Please enter your user id");
    return;
  }

  ajaxCall(
    "GET",
    server + `api/Vacations/userId/${idInp}`,
    "",
    sVacationsCB,
    eVacationsCB
  );
}

async function sVacationsCB(data) {
  let flats = await fetch(server + `api/Flats`)
    .then((res) => res.json())
    .then((fdata) => fdata);

  let strVacations = data.map((vac, i) => {
    let flatVac = flats.find((flat) => (flat.id = vac.flatId));

    return createVacation(vac, flatVac, i);
  });

  $("#lower-section").attr("hidden", false);

  $("#vacations-grid").html(strVacations);

  document
    .querySelectorAll("#vacations-grid .card")
    .forEach((el) => observer.observe(el));
}

function eVacationsCB(error) {
  console.log("error", error);
  alert(error.responseText);
}

function createVacation(vac, flat, i) {
  let screenSize = window.innerWidth;
  let diviedBy = 4;
  if (screenSize <= 768) {
    diviedBy = 1;
  } else {
    if (screenSize <= 992) {
      diviedBy = 2;
    } else {
      if (screenSize <= 1200) {
        diviedBy = 3;
      }
    }
  }

  let startDate = new Date(vac.startDate);
  let endDate = new Date(vac.endDate);

  let price =
    flat.price * Math.round((endDate.getTime() - startDate.getTime()) / 8.64e7);

  return ` <div class="col mb-4">
          <div class="card h-100 card-${(i % diviedBy) + 1}" >
            
            <img src="../SRC/Vacation.jpg" class="card-img-top" alt="Random flat picture">
            <div class="card-header">
              <u>Vacation to ${flat.city}:</u>
            </div>
            <div class="card-body">
              <div class="card-text">
                <p>Flat: ${vac.flatId}</p>
                <p>Price: ${price} $</p>
                <p>Dates: ${startDate.toLocaleDateString(
                  "en-UK"
                )} - ${endDate.toLocaleDateString("en-UK")}</p>
                </div>
            </div>
          </div>
        </div>`;
}
