//------------GET THE POP UP MESSANGE-----------//



function message_pop() {
  document.getElementById("myModal").style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
function close_me() {
  document.getElementById("myModal").style.display = "none";
}
///// map

mapboxgl.accessToken = 'pk.eyJ1IjoiaHV5bGVnaWE2NzYiLCJhIjoiY2tvaGZpeWY3MHkxbjJ2anhlYXFqaG55dSJ9.sfHhymTo5tzV2d7dFi1TWg';
        var map = new mapboxgl.Map({
        container: 'map', // container ID
        style: 'mapbox://styles/mapbox/streets-v11', // style URL
        center: [138.599503, -34.921230], // starting position [lng, lat]
        zoom: 11 // starting zoom
        });
var nav = new mapboxgl.NavigationControl();
map.addControl(nav);

function venue_signup() {
    window.location.href = "/venue_signup";
}

function msg_pop() {

      // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            if (this.responseText == "no_regis") {
                document.getElementById("myModal").style.display = "flex";
                document.getElementsByClassName("main")[0].style.display = "none";
            }
            else if (this.responseText == "yes_regis"){
                msg_close();
            }
        }
    };

    // Open connection to server
    xmlhttp.open("GET", "/check_register", true);

    // Send request
    xmlhttp.send();

}

function msg_close() {
    document.getElementById("myModal").style.display = "none";
    document.getElementsByClassName("main")[0].style.display = "block";
}



function signupForVenue(){

    let venue = {
        venue_name: document.getElementById('venue_name').value,
        venue_phone: document.getElementById('phone').value,
        street: document.getElementById('street').value,
        suburb: document.getElementById('suburb').value,
        city: document.getElementById('city').value,
        state: document.getElementById('state').value,
        postcode: document.getElementById('postcode').value,
        long: document.getElementById('long').value,
        lat: document.getElementById('lat').value
    };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            window.location.replace("/owner_homepage");


        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Failed!");
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/venue_signup", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(venue));

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
