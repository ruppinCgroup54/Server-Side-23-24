$(document).ready(function () {
  server = `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`;

  $("#flatForm").submit(addNewFlat);

  renderFalts();
  getAllCities();
});



function addNewFlat(e) {
  let data = e.target;

  let newFlat = {
    Id:
      Math.random().toString(36).slice(2, 12) + new Date().getTime().toString(),
    City: data.city.value,
    Address: data.address.value,
    NumberOfRooms: Number(data.inputCity.value),
    Price: Number(data.inputPrice.value),
  };
  ajaxCall(
    "POST",
    server + "api/flats",
    JSON.stringify(newFlat),
    sInsertCB,
    eInsertCB
  );
  e.target.reset();
  return false;
}

function sInsertCB(res) {
  console.log("res", res);
  swal("Flat has ben submitted!", "Great Job", "success");
  renderFalts();
}
function eInsertCB(err) {
  console.log("err");
  alert(err.responseText);
}

function getAllCities() {
  let strCities = cities
    .sort((a, b) => (a.name > b.name ? 1 : -1))
    .map((city) => `<option value="${city.name}">`);

  // console.log("city", strCities);

  $("#inputCity").append(strCities);

  // console.log(cities[0]);
}

function renderFalts() {
  ajaxCall("GET", server + "api/Flats", "", successFlatCB, errorFlatCB);
}

function successFlatCB(flats) {
  let strFlats = flats.map((flat, i) => createNewFlat(flat, i));

  console.log("flats", strFlats);

  $("#flats-grid").html(strFlats);

  document
    .querySelectorAll("#flats-grid .card")
    .forEach((el) => observer.observe(el));
}
function errorFlatCB(error) {
  console.log("error", error);
}

function createNewFlat(flat, i) {
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

  return ` <div class="col mb-4">
              <div class="card h-100 card-${(i % diviedBy) + 1}" >
                <img src="../SRC/air-bnb.png" class="card-img-top" alt="Random flat picture">
                <div class="card-header">
                  <u>Flat details:</u>
                </div>
                <div class="card-body">
                  <div class="card-text">
                    <p class="p-0 m-0">City: ${flat.city}</p>
                    <p class="p-0 m-0">address: ${flat.address}</p>
                    <p class="p-0 m-0">Number of rooms: ${
                      flat.numberOfRooms
                    }</p>
                    <p class="p-0 m-0"  >Price: ${flat.price} $</p>
                    </div>
                </div>
                <div class="card-footer">
                  <a href="./vacations.html?flatId=${
                    flat.id
                  }" class="btn btn-primary" target="_blank">Order vacation</a>

                </div>
              </div>
            </div>`;
}
