// When the user clicks the button, open the modal
function pop_message() {
    document.getElementById("myModal").style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
function close_me() {
    document.getElementById("myModal").style.display = "none";
}

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
    controls: {
      polygon: true,
      trash: true
    },
    defaultMode: 'draw_polygon',
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

/*------Edit the hotspots (Health official's feature only)-------- */
var hotspot_data;
function get_hotspot(){
  document.getElementById('res').innerHTML = 'Use the draw tools to set covid hotspots';
  pop_message();



  map.addControl(draw);

  map.on('draw.create', updateData);
  map.on('draw.delete', updateData);
  map.on('draw.update', updateData);


  function updateData() {
    hotspot_data = draw.getAll();

  }


}


function save_changes(){
  var answer = document.getElementById('calculated-area');
  if (hotspot_data === undefined ){
    answer.innerHTML = `<p style="text-align:center"> 0 m<sup>2</sup></p>`;
    document.getElementById('res').innerHTML = 'No hotspots added';
    pop_message();

  }
  else {
    if (hotspot_data['features'].length == 0) {
      answer.innerHTML = `<p style="text-align:center"> 0 m<sup>2</sup></p>`;
      document.getElementById('res').innerHTML = 'No hotspots added';
      pop_message();

    }
    else {

      var area = turf.area(hotspot_data);
      // restrict to area to 2 decimal points
      var rounded_area = Math.round(area * 100) / 100;
      answer.innerHTML =
        '<p style="text-align:center"><strong>' +
        rounded_area +
        '</strong> m<sup>2</sup> </p>';
      /////////////////////ADD HOTSPOT TO DATABASE////////////
      change_hotspot();


    }
  }
  // var coordinate_of_each_polygon_point = data['features'][0]['geometry']['coordinates'][0];
  // var data_to_draw_polygon = data['features'][0]['geometry'];

  // console.log(data_to_draw_polygon);

  // console.log(coordinate_of_each_polygon_point); //list of coordinate

}


function change_hotspot(){
    console.log(hotspot_data);
    let data = JSON.stringify(hotspot_data);
    console.log(data);
    console.log(JSON.parse(data));
    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
          document.getElementById('res').innerHTML = this.responseText;
          pop_message();
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/users/change_hotspot", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(hotspot_data));  //data of the hotspot

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
              console.log(data_to_draw);
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