$(document).ready(function () {
  let port = 7014;
  server = `https://proj.ruppin.ac.il/cgroup54/test2/tar1/`;
  client = `https://127.0.0.1:5500/`;

  renderFalts();
});

function renderFalts() {
  ajaxCall("GET", server +'api/Flats', "", successFlatCB, errorFlatCB);
}

function successFlatCB(flats) {
  let strFlats = flats.map((flat, i) => createNewFlat(flat, i));

  console.log("flats", strFlats);

  $("#flats-grid").html(strFlats);
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
                <img src="../SRC/airbnb luxe.webp" class="card-img-top" alt="Random flat picture">
                <div class="card-body">
                  <div class="card-text">
                    <p>City: ${flat.city}</p>
                    <p>address: ${flat.address}</p>
                    <p>Number of rooms: ${flat.numberOfRooms}</p>
                    <p>Price: ${flat.price} $</p>
                    </div>
                </div>
                <div class="card-footer">
                  <a href="./vacations.html?flatId=${
                    flat.id
                  }" class="btn btn-primary" target="_blank">Click to order vacation</a>

                </div>
              </div>
            </div>`;
}
