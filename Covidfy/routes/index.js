var express = require('express');
var router = express.Router();

const CLIENT_ID = '161774089268-k284hobl8s1rlod3lsq91cnsjel25qhp.apps.googleusercontent.com';

const {OAuth2Client} = require('google-auth-library');
const client = new OAuth2Client(CLIENT_ID);
var argon2 = require('argon2');

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Express' });
});


var check;
router.post('/login', function(req, res, next) {

    if( 'username' in req.body &&
        'password' in req.body) {

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }

            var query = `SELECT User.User_ID,
                User.First_name,
                User.Last_name,
                User.Username,
                User.Password,
                User.Email,
                User.User_Type,
                User.Age,
                User.Phone_number,
                Address.Street,
                Address.Suburb,
                Address.City,
                Address.State,
                Address.Postcode
            FROM User INNER JOIN Address ON User.User_ID = Address.User_ID
            WHERE User.Username = ?;`;
            connection.query(query,[req.body.username], async function(err, rows, fields) {
            connection.release(); // release connection
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            if(rows.length > 0){

                let valid = await argon2.verify(rows[0].Password, req.body.password);  //verifying the valid password

                if (valid) {
                    delete rows[0].Password;   //You don't want to show the password even if it been encrypted
                    req.session.user = rows[0];
                    res.json(rows[0]);
                } else {
                    return res.sendStatus(401);
                }

            } else {
                res.sendStatus(401);
            }
        });
    });

    } else if( 'token' in req.body ) {

        async function verify() {
          const ticket = await client.verifyIdToken({
              idToken: req.body.token,
              audience: CLIENT_ID,  // Specify the CLIENT_ID of the app that accesses the backend
              // Or, if multiple clients access the backend:
              //[CLIENT_ID_1, CLIENT_ID_2, CLIENT_ID_3]
          });
          const payload = ticket.getPayload();

          // If request specified a G Suite domain:
          // const domain = payload['hd'];
        //   console.log("LastName: " + payload['family_name']) //lastname
        //   console.log("FirstName: " + payload['given_name']) //firstname
        //   console.log("Email: " + payload['email'])
        //   console.log("Email: " + payload['picture'])


          req.pool.getConnection( function(err,connection) {
                if (err) {
                    console.log(err);
                    res.sendStatus(500);
                    return;
                }
                var query1 = `SELECT Email FROM User WHERE Email=?`
                connection.query(query1,[payload['email']], function(err, result, field) {
                        if (err) {
                            console.log(err);
                            res.sendStatus(500);
                            return;
                        }

                        if (result.length === 0) {
                            var query1 = `INSERT INTO User (First_name,Last_name,Email) VALUES (?,?,?);`;

                            connection.query(query1,[
                                payload['given_name'],
                                payload['family_name'],
                                payload['email']], function(err, results) {

                                    if (err) {
                                        console.log(err);
                                        res.sendStatus(500);
                                        return;
                                    }
                                });
                        }

                    });

                var query2 = `SELECT User.User_ID,
                                User.First_name,
                                User.Last_name,
                                User.Email,
                                User.User_Type
                            FROM User
                            WHERE User.Email = ?;`;
                connection.query(query2,[payload['email']], function(err, rows, fields) {

                    connection.release(); // release connection
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                        return;
                    }
                    if(rows.length > 0){
                        req.session.user = rows[0];
                        res.json(rows[0]);
                    } else {
                        res.sendStatus(401);
                    }
                });
            });
        }
        verify().catch(function(){res.sendStatus(401);});

    } else {
        res.sendStatus(400);
    }

});





router.post('/signup', async function(req, res, next) {
///////////COVIDFY SIGN UP SYSTEM///////////////
    if( 'username' in req.body &&
        'password' in req.body &&
        'email' in req.body &&
        'user_type' in req.body &&
        'first_name' in req.body &&
        'last_name' in req.body &&
        'age' in req.body &&
        'phone' in req.body &&
        'street' in req.body &&
        'suburb' in req.body &&
        'city' in req.body &&
        'state' in req.body &&
        'postcode' in req.body) {


        let hash = await argon2.hash(req.body.password);

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            var query = `SELECT Username, Email FROM User WHERE Username=? OR Email=?`;
            connection.query(query,[
                req.body.username,
                req.body.email], function(err, result, field) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }

                    if (result.length === 0) {
                        var query1 = `INSERT INTO User (First_name,Last_name,Username,Password,Email,User_Type,Age,Phone_number) VALUES (?,?,?,?,?,?,?,?);`;

                        connection.query(query1,[
                            req.body.first_name,
                            req.body.last_name,
                            req.body.username,
                            hash,
                            req.body.email,
                            req.body.user_type,
                            req.body.age,
                            req.body.phone], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                }
                            });

                        var query2 = `INSERT INTO Address(User_ID, Street, Suburb, City, State, Postcode) VALUES ((SELECT User_id FROM User WHERE Username = ?),?,?,?,?,?);`;
                        connection.query(query2,[
                            req.body.username,
                            req.body.street,
                            req.body.suburb,
                            req.body.city,
                            req.body.state,
                            req.body.postcode], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                    return;
                                }
                            connection.release(); // release connection

                            res.send("Thank you for using Covidfy!");
                        });
                    }
                    else{
                        // console.log(rows.length);
                        connection.release();
                        res.send("Username or Email is already existed on database!");

                    }
                });

        });
/////////////////////ADDITIONAL GOOGLE INFO//////////////
    }else if ('user_type' in req.body &&
        'age' in req.body &&
        'phone' in req.body &&
        'street' in req.body &&
        'suburb' in req.body &&
        'city' in req.body &&
        'state' in req.body &&
        'postcode' in req.body) {

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }

            var query1 = `UPDATE User SET
                                User_Type = ?,
                                Age= ?,
                                Phone_number = ?

                            WHERE
                                User_ID=?`;

            connection.query(query1,[
                req.body.user_type,
                req.body.age,
                req.body.phone,
                req.session.user.User_ID], function(err, results) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }
                });

            var query2 = `INSERT INTO Address(User_ID, Street, Suburb, City, State, Postcode) VALUES (?,?,?,?,?,?);`;
            connection.query(query2,[
                req.session.user.User_ID,
                req.body.street,
                req.body.suburb,
                req.body.city,
                req.body.state,
                req.body.postcode], function(err, results) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                        return;
                    }
                connection.release(); // release connection

                res.send("Thank you for using Covidfy!");
            });


        });

    } else {
        res.sendStatus(400);
    }

});




/* Middleware to block each user type to access to other resources and function without login */
var blockedUrl;
router.use(function(req, res, next) {
    if ('user' in req.session) {

        if(req.session.user.User_Type == 'User'){
            blockedUrl = ['/owner_homepage', '/coordinate_search', '/owner_history', '/venue_signup', '/health_homepage', '/health_signup', '/health_database'];
        }
        else if(req.session.user.User_Type == 'Owner'){
            blockedUrl = ['/health_homepage', '/health_signup', '/health_database','/user_homepage', '/user_history'];
        }
        else if(req.session.user.User_Type == 'Health'){
            blockedUrl = ['/user_homepage', '/user_history', '/owner_homepage', '/coordinate_search', '/owner_history', '/venue_signup' ];
        }


        if(!checkFunction(req.url)) {
            next();
        } else {
            res.sendStatus(401);
        }
    } else {
        res.sendStatus(401);
    }
});

var checkFunction = function(url){
    return blockedUrl.find(function(urlCheck){
        return urlCheck === url;
    });
};

/////////////////////////////////////////////////////////////////////////////////////////////////



router.get('/check_register', function(req, res, next) {

    req.pool.getConnection( function(err,connection) {
        if (err) {
            console.log(err);
            res.sendStatus(500);
            return;
        }
        var query = `SELECT User_ID FROM Venue WHERE User_ID = ?`
        connection.query(query,[req.session.user.User_ID], function(err, result, field) {

            if (err) {
                console.log(err);
                res.sendStatus(500);
            }

            if (result.length === 0) {
                connection.release(); // release connection
                res.send("no_regis");
            }

            else {
                connection.release(); // release connection
                res.send("yes_regis");
            }

        });
    });
});

router.post('/venue_signup', async function(req, res, next) {
    let coordinate;
    if( 'venue_name' in req.body &&
        'venue_phone' in req.body &&
        'street' in req.body &&
        'suburb' in req.body &&
        'city' in req.body &&
        'state' in req.body &&
        'postcode' in req.body &&
        'long' in req.body &&
        'lat' in req.body) {
        coordinate = JSON.stringify({lat: parseFloat(req.body.lat), long: parseFloat(req.body.long)});

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }


            var query1 = `INSERT INTO Venue (User_ID, Venue_name,Phone_number,Coordinate) VALUES (?,?,?,?);`;

            connection.query(query1,[
                req.session.user.User_ID,
                req.body.venue_name,
                req.body.venue_phone,
                coordinate], function(err, rows, fields) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }
                });


            var query2 = `INSERT INTO Venue_Address (Checkin_Code, Street, Suburb, City, State, Postcode) VALUES ((SELECT Checkin_Code FROM Venue WHERE User_ID = ?),?,?,?,?,?);`;
            connection.query(query2,[
                req.session.user.User_ID,
                req.body.street,
                req.body.suburb,
                req.body.city,
                req.body.state,
                req.body.postcode], function(err, rows, fields) {

                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                        return;
                    }

                connection.release(); // release connection
                    res.end();
                });
            });


    } else {
        res.sendStatus(400);
    }

});

router.post('/signupForHealth', async function(req, res, next) {
///////////COVIDFY SIGN UP FOR HEALTH OFFICIALS///////////////
    if( 'username' in req.body &&
        'password' in req.body &&
        'email' in req.body &&
        'user_type' in req.body &&
        'first_name' in req.body &&
        'last_name' in req.body &&
        'age' in req.body &&
        'phone' in req.body &&
        'street' in req.body &&
        'suburb' in req.body &&
        'city' in req.body &&
        'state' in req.body &&
        'postcode' in req.body) {


        let hash = await argon2.hash(req.body.password);

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            var query = `SELECT Username, Email FROM User WHERE Username=? OR Email=?`;
            connection.query(query,[
                req.body.username,
                req.body.email], function(err, result, field) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }

                    if (result.length === 0) {
                        var query1 = `INSERT INTO User (First_name,Last_name,Username,Password,Email,User_Type,Age,Phone_number) VALUES (?,?,?,?,?,?,?,?);`;

                        connection.query(query1,[
                            req.body.first_name,
                            req.body.last_name,
                            req.body.username,
                            hash,
                            req.body.email,
                            req.body.user_type,
                            req.body.age,
                            req.body.phone], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                }
                            });

                        var query2 = `INSERT INTO Address(User_ID, Street, Suburb, City, State, Postcode) VALUES ((SELECT User_id FROM User WHERE Username = ?),?,?,?,?,?);`;
                        connection.query(query2,[
                            req.body.username,
                            req.body.street,
                            req.body.suburb,
                            req.body.city,
                            req.body.state,
                            req.body.postcode], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                    return;
                                }
                            connection.release(); // release connection

                            res.send("Thank you for using Covidfy!");
                        });
                    }
                    else{
                        // console.log(rows.length);
                        connection.release();
                        res.send("Username or Email is already existed on database!");

                    }
                });

        });
    }else {
        res.sendStatus(400);
    }

});

router.post('/logout', function(req, res, next) {
    delete req.session.user;
    res.send();
});
module.exports = router;


