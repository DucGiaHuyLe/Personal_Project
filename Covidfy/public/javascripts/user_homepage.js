//------------GET THE POP UP MESSANGE-----------//

// Get the modal


// When the user clicks the button, open the modal
function pop_message() {
    document.getElementById("myModal").style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
function close_me() {
    document.getElementById("myModal").style.display = "none";
}

///////////////////////////////////////////////////////////////

mapboxgl.accessToken = 'pk.eyJ1IjoiaHV5bGVnaWE2NzYiLCJhIjoiY2tvaGZpeWY3MHkxbjJ2anhlYXFqaG55dSJ9.sfHhymTo5tzV2d7dFi1TWg';
        var map = new mapboxgl.Map({
        container: 'map', // container ID
        style: 'mapbox://styles/mapbox/streets-v11', // style URL
        center: [138.599503, -34.921230], // starting position [lng, lat]
        zoom: 11 // starting zoom
        });
var nav = new mapboxgl.NavigationControl();
map.addControl(nav);

/////////////////HOT SPOT TOOLS////////////////////////////
var draw = new MapboxDraw({
    displayControlsDefault: false,
    keybindings: false,
    touchEnabled: true,
    boxSelect: false,
    drawing:false,
    controls: {
      polygon: false,
      trash: false
    },
   styles:
        [{
            'id': 'gl-draw-polygon-fill-inactive',
            'type': 'fill',

            'paint': {
                'fill-color': 'red',
                'fill-outline-color': '#3bb2d0',
                'fill-opacity': 0.3
            }
        },
        {
            'id': 'gl-draw-polygon-and-line-vertex-inactive',
            'type': 'circle',
            'filter': ['all', ['==', 'meta', 'vertex'],
                ['==', '$type', 'Point'],
                ['!=', 'mode', 'static']
            ],
            'paint': {
                'circle-radius': 3,
                'circle-color': '#fbb03b'
            }
        }]


  });
map.addControl(draw);
/*------Check in with GPS-------- */
var locations = [];
function get_location(){

    let succeed = function(pos){
        // console.log(pos.coords);
        // console.log(pos.coords.longitude);
        // console.log(pos.coords.latitude);

        //set the coordinate to 3 decimal places because that is acceptable accuracy for the venue's coordinate
        var myLongtitude=pos.coords.longitude;
        var myLongtitude_fixed=parseFloat(myLongtitude).toFixed(3);

        var myLatitude=pos.coords.latitude;
        var myLatitude_fixed=parseFloat(myLatitude).toFixed(3);



        map.addControl(nav);

        ///////////////SEND COORDINATE OF USER TO THE SERVER//////////////

    let coordinate = {
        long: myLongtitude_fixed,
        lat: myLatitude_fixed
    };
    console.log(coordinate);
    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText == "The checked in venue is not defined or yet registered at Covidfy"){
                document.getElementById('res').innerHTML = this.responseText;
                pop_message();
            }

            else {
                locations = JSON.parse(this.responseText);
                for(let i=0; i<locations.length; i++){

                    let location = locations[i];

                    document.getElementById("infodiv").innerHTML =
                    `
                        <p class="info">Name: ${location.Venue_Name}</p>
                        <p class="info">Address: ${location.Address}</p>
                        <p class="info">Coordinate: (${location.Latitude}, ${location.Longitude})</p>

                    `;
                    document.getElementById('res').innerHTML = "CHECKED IN!!";
                    pop_message();
                    var map = new mapboxgl.Map({
                        container: 'map', // container ID
                        style: 'mapbox://styles/mapbox/streets-v11', // style URL
                        center: [pos.coords.longitude, pos.coords.latitude], // starting position [lng, lat]
                        zoom: 18 // starting zoom
                    });

                    var marker = new mapboxgl.Marker()
                    .setLngLat ([pos.coords.longitude, pos.coords.latitude])
                    .addTo(map);

                }

            }
            // alert(this.responseText);
        }
    };

    // Open connection to server & send the post data using a POST request
    xmlhttp.open("POST", "/users/add_coor", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(coordinate));



};

    let failure = function(err){
        console.log(err);
    };

    let setting = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };

    navigator.geolocation.getCurrentPosition(succeed, failure, setting);
}


/*------Sign out of session-------- */
function signOut() {
      var auth2 = gapi.auth2.getAuthInstance();
      auth2.signOut().then(function () {
        console.log('User signed out.');
      });

      // Do our logout on server here
      // Create AJAX Request
      var xmlhttp = new XMLHttpRequest();

      // Open connection to server & send the post data using a POST request

      xmlhttp.open("POST", "/logout", true);
      xmlhttp.send();
      window.location.replace("/");

    }
function onLoad() {
      gapi.load('auth2', function() {
        gapi.auth2.init();
      });
    }

/*------Route back-------- */

function goBack() {
  window.history.back();
}

/*------SHOW CHECK IN HISTORY-------- */

var coords = [];
function show_history(){
    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            // Parse the JSON and update the coords array
            coords = JSON.parse(this.responseText);
            // Call the updatePosts function to update the page
            for(let i=0; i<coords.length; i++){

                let coord = coords[i];

                var marker = new mapboxgl.Marker()
                .setLngLat ([coord.Longitude, coord.Latitude]) //from DATABASE
                .addTo(map);


            }
        }
    };
    // Open connection to server
    xmlhttp.open("GET", "/users/get_coor", true);

    // Send request
    xmlhttp.send();
}

/////////////////FOR THE SPECIFIC INFO/////////////
var infos = [];
function get_history(){
    // Create AJAX Request
var xmlhttp = new XMLHttpRequest();
    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            // Parse the JSON and update the actors array
            infos = JSON.parse(this.responseText);
            // Call the updatePosts function to update the page
            for(let i=0; i<infos.length; i++){

                let info = infos[i];

                let postDiv = document.createElement("TR");
                postDiv.classList.add("info");


                postDiv.innerHTML =
                `
                  <td>${new Date(info.timestamp).toLocaleString()}</td>
                  <td>${info.Venue_name}</td>
                  <td>${info.Street}</td>
                  <td>${info.Suburb}</td>
                  <td>${info.City}</td>
                  <td>${info.State}</td>
                  <td>${info.Postcode}</td>
                `;

                document.getElementById("info_list").appendChild(postDiv);
            }
        }
    };
    // Open connection to server
    xmlhttp.open("GET", "/users/get_history", true);

    // Send request
    xmlhttp.send();
}




/*------SHOW HOTSPOTS-------- */
var data = [];
function show_hotspots(){
    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
          // Parse the JSON and update the data array
          data = JSON.parse(this.responseText);

          for(let i=0; i<data.length; i++){

            let da = data[i];
            da_json = JSON.parse(da.Data_to_draw);

            for(let j=0; j < da_json.features.length; j++){



              let data_to_draw = da_json.features[j].geometry;
            //   console.log(data_to_draw);
              draw.add(data_to_draw);
            }
          }
        }
    };
    // Open connection to server
    xmlhttp.open("GET", "/users/get_hotspots", true);

    // Send request
    xmlhttp.send();
}