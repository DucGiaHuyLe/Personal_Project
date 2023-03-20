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

function signup(){

    let user = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value,
        email: document.getElementById('email').value,
        first_name: document.getElementById('firstname').value,
        last_name: document.getElementById('lastname').value,
        user_type: document.getElementById('usertype').value,
        age: document.getElementById('age').value,
        phone: document.getElementById('phone').value,
        street: document.getElementById('street').value,
        suburb: document.getElementById('suburb').value,
        city: document.getElementById('city').value,
        state: document.getElementById('state').value,
        postcode: document.getElementById('postcode').value
    };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('res').innerHTML = this.responseText;
            message_pop();

        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Failed!");
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/signup", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(user));

}


function extra_info(){

    let user = {
        user_type: document.getElementById('usertype').value,
        age: document.getElementById('age').value,
        phone: document.getElementById('phone').value,
        street: document.getElementById('street').value,
        suburb: document.getElementById('suburb').value,
        city: document.getElementById('city').value,
        state: document.getElementById('state').value,
        postcode: document.getElementById('postcode').value
    };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('res').innerHTML = this.responseText;
            message_pop();
        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Failed!");
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/signup", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(user));

}


function signupForHealth(){

    let user = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value,
        email: document.getElementById('email').value,
        first_name: document.getElementById('firstname').value,
        last_name: document.getElementById('lastname').value,
        user_type: 'Health',
        age: document.getElementById('age').value,
        phone: document.getElementById('phone').value,
        street: document.getElementById('street').value,
        suburb: document.getElementById('suburb').value,
        city: document.getElementById('city').value,
        state: document.getElementById('state').value,
        postcode: document.getElementById('postcode').value
    };

    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            document.getElementById('res').innerHTML = this.responseText;
            message_pop();

        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Failed!");
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/signupForHealth", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(user));

}