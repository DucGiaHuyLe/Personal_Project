//------------GET THE POP UP MESSANGE-----------//


// When the user clicks the button, open the modal
function message_pop() {
  document.getElementById("myModal").style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
function close_me() {
  document.getElementById("myModal").style.display = "none";
}

///////////////////////////////////////////////////////////////


function showpassword() {
    var x = document.getElementById("password");

    if (x.type === "password")
    {
        x.type = "text";
    }
    else
    {
        x.type = "password";
    }
}

//////////////////GOOGLE LOG IN/////////////////////


function onSignIn(googleUser) {
    // Read the token data on the client side
    var profile = googleUser.getBasicProfile();
    console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
    console.log('Name: ' + profile.getName());
    console.log('Image URL: ' + profile.getImageUrl());
    console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.

    // Prepare to send the TOKEN to the server for validation
    var id_token = { token: googleUser.getAuthResponse().id_token };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            let data = JSON.parse(this.responseText);
            if (data.User_Type === null){
                alert("Please provide us some additional data to process!!");
            }

            if (data.User_Type == "User"){
                window.location.href = "/user_homepage";
            }


            else if (data.User_Type == "Health"){
                window.location.href = "/health_homepage";
            }

            else if (data.User_Type == "Owner")
                window.location.href = "/owner_homepage";

        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Your Google data was updated to our database");
        }
    };

    // Open connection to server & send the token using a POST request
    xmlhttp.open("POST", "/login", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(id_token));

}




////////////////////////////////////////////////////////////////////////

function login(){

    let user = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value
    };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            // alert("Welcome "+this.responseText);
            let data = JSON.parse(this.responseText);
            if (data.User_Type == "User"){
                window.location.href = "/user_homepage";
            }


            else if (data.User_Type == "Health"){
                window.location.href = "/health_homepage";
            }

            else if (data.User_Type == "Owner")
                window.location.href = "/owner_homepage";

        } else if (this.readyState == 4 && this.status >= 400) {
            message_pop();
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/login", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(user));

}