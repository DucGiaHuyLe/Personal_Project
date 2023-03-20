
// When the user clicks the button, open the modal
function message_pop() {
  document.getElementById("myModal").style.display = "flex";
}

// When the user clicks on <span> (x), close the modal
function close_me() {
  document.getElementById("myModal").style.display = "none";
}
////////////////////////////////////////////////////
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

function edit_profile(){

    let user = {
        username: document.getElementById('username').value,
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
            document.getElementById('res').style.display = "block";
            document.getElementById('pw').style.display = "none";
            message_pop();

        } else if (this.readyState == 4 && this.status >= 400) {
            alert("Failed!");
        }
    };

    // Open connection to server & send the post data using a POST request
    // We will cover POST requests in more detail in week 8
    xmlhttp.open("POST", "/users/edit_profile", true);
    xmlhttp.setRequestHeader("Content-type", "application/json");
    xmlhttp.send(JSON.stringify(user));

}

var profiles = [];
function get_profile(){
    // Create AJAX Request
    var xmlhttp = new XMLHttpRequest();

    // Define function to run on response
    xmlhttp.onreadystatechange = function() {
        if (this.readyState == 4 && this.status == 200) {
            // Parse the JSON and update the profiles array
            profiles = JSON.parse(this.responseText);
            // Call the updatePosts function to update the page
            for(let i=0; i<profiles.length; i++){

                let profile = profiles[i];

                // let postDiv = document.createElement("DIV");
                // postDiv.classList.add("profile");


                // postDiv.innerHTML =
                // `
                //   <td>${profile.first_name}</td>
                //   <td>${profile.last_name}</td>
                // `;

                document.getElementById("firstname").value = profile.First_name;
                document.getElementById("lastname").value = profile.Last_name;
                document.getElementById("age").value = profile.Age;
                document.getElementById("phone").value = profile.Phone_number;
                document.getElementById("email").value = profile.Email;
                document.getElementById("street").value = profile.Street;
                document.getElementById("suburb").value = profile.Suburb;
                document.getElementById("city").value = profile.City;
                document.getElementById("state").value = profile.State;
                document.getElementById("postcode").value = profile.Postcode;
                document.getElementById("username").value = profile.Username;
                document.getElementById("usertype").value = profile.User_Type;
            }
        }
    };
    // Open connection to server
    xmlhttp.open("GET", "/users/get_profile", true);

    // Send request
    xmlhttp.send();
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
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                window.location.href = "/";
            }
        };

      // Open connection to server & send the post data using a POST request

      xmlhttp.open("POST", "/logout", true);
      xmlhttp.send();


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

/*------CHANGE PASSWORD-------- */
function show_pw_box() {
    document.getElementById('pw').innerHTML = `<h1> PASSWORD CHANGE </h1>
                                                <div class="input_wrap">
                                                    <label>Old Password</label>
                                                        <input type="password" id="old_pw" required>
                                                </div>
                                                <div class="input_wrap">
                                                    <label>New Password</label>
                                                        <input type="password" id="new_pw" required>
                                                </div>
                                                <div class="input_wrap">
                                                    <label>New Repeat Password</label>
                                                        <input type="password" id="re_new_pw" required>
                                                </div>
                                                <h3><a class="login" onclick="change_pw()"> <i class="fas fa-check"></i> </a>  </h3>
                                                `;

    document.getElementById('pw').style.display = "block";
    document.getElementById('res').style.display = "none";
    message_pop();




}


//////////////VERIFY THE INPUT///////////////////////////
function change_pw(){
    if (document.getElementById('new_pw').value == document.getElementById('re_new_pw').value) {
        let pw = {
            old_pw: document.getElementById('old_pw').value,
            new_pw: document.getElementById('new_pw').value,
        };

        // Create AJAX Request
        var xmlhttp = new XMLHttpRequest();

        // Define function to run on response
        xmlhttp.onreadystatechange = function() {
            if (this.readyState == 4 && this.status == 200) {
                document.getElementById('res').innerHTML = this.responseText;
                document.getElementById('res').style.display = "block";
                document.getElementById('pw').style.display = "none";
                message_pop();

            } else if (this.readyState == 4 && this.status >= 400) {
                alert("Failed!");
            }
        };

        // Open connection to server & send the post data using a POST request
        // We will cover POST requests in more detail in week 8
        xmlhttp.open("POST", "/users/change_pw", true);
        xmlhttp.setRequestHeader("Content-type", "application/json");
        xmlhttp.send(JSON.stringify(pw));
    } else{
        alert("Your repeated password is not matched");
    }
}
