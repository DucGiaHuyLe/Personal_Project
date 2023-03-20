var express = require('express');
var router = express.Router();
var argon2 = require('argon2');
/* GET users listing. */
router.get('/', function(req, res, next) {
  res.send('respond with a resource');
});

router.post('/add_coor', function(req, res, next) {
    let coordinate;
    if( 'long' in req.body &&
        'lat' in req.body) {
        coordinate = JSON.stringify({lat: parseFloat(req.body.lat), long: parseFloat(req.body.long)});
        console.log(coordinate);
        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            var query = `SELECT Checkin_Code FROM Venue WHERE Coordinate->>'$.lat' = ? AND Coordinate->>'$.long' = ?;`;  // check if the user check in at the same time
            connection.query(query,[req.body.lat, req.body.long], function(err, result, field) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }
                    if (result.length != 0) {
                        var query1 = `INSERT INTO Checkin (Checkin_Code, User_ID, timestamp, Coordinate)
                                        VALUES ((SELECT Checkin_Code FROM Venue
                                                    WHERE Coordinate->>'$.lat' = ? AND Coordinate->>'$.long' = ?),
                                                ?,NOW(),?);`;

                        connection.query(query1,[
                            req.body.lat,
                            req.body.long,
                            req.session.user.User_ID,
                            coordinate
                           ], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                    return;
                                }
                            });

                        var query2 = `SELECT
                                        Venue_Name,
                                        (SELECT CONCAT(Street, ", ", Suburb, ", ", City, ", ", State, ", ", Postcode) FROM Venue_Address
                                            WHERE Checkin_Code = (SELECT Checkin_Code FROM Venue WHERE Coordinate->>'$.lat' = ? AND Coordinate->>'$.long' = ?)) As Address,
                                        Coordinate->>'$.long' Longitude,
                                        Coordinate->>'$.lat' Latitude
                                    FROM Venue WHERE Coordinate->>'$.lat' = ? AND Coordinate->>'$.long' = ?;`;

                        connection.query(query2,[req.body.lat, req.body.long, req.body.lat, req.body.long], function(err, rows, fields) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                    return;
                                }
                                connection.release(); // release connection
                                res.json(rows); //send response
                            });

                    }
                    else{
                        // console.log(rows.length);
                        connection.release();
                        res.send("The checked in venue is not defined or yet registered at Covidfy");

                    }
                });

        });
    }else {
        res.sendStatus(400);
    }

});


router.get('/get_coor', function(req, res, next) {

    req.pool.getConnection( function(err,connection) {
        if (err) {
          res.sendStatus(500);
          return;
        }
        var query = `SELECT DISTINCT
                                Coordinate->>'$.long' Longitude,
                                Coordinate->>'$.lat' Latitude
                            FROM Checkin WHERE User_ID = ?; `;
        connection.query(query, [req.session.user.User_ID], function(err, rows, fields) {
          connection.release(); // release connection
          if (err) {
            res.sendStatus(500);
            return;
          }
          res.json(rows); //send response
        });
    });
});

router.get('/get_history', function(req, res, next) {

    req.pool.getConnection( function(err,connection) {
        if (err) {
          res.sendStatus(500);
          return;
        }
        var query = `SELECT Checkin.timestamp,
                        Venue.Venue_name,
                        Venue_Address.Street,
                        Venue_Address.Suburb,
                        Venue_Address.City,
                        Venue_Address.State,
                        Venue_Address.Postcode
                    FROM Checkin INNER JOIN Venue ON Checkin.Checkin_Code = Venue.Checkin_code
                    INNER JOIN Venue_Address ON Venue.Checkin_code = Venue_Address.Checkin_code
                    WHERE Checkin.User_ID = ?
                    ORDER BY Checkin.timestamp ASC; `;  //List by time from newest to oldest
        connection.query(query, [req.session.user.User_ID], function(err, rows, fields) {
          connection.release(); // release connection
          if (err) {
            res.sendStatus(500);
            return;
          }
          res.json(rows); //send response
        });
    });
});


router.post('/change_hotspot', function(req, res, next) {
    let data = JSON.stringify(req.body);
    if( 'type' in req.body &&
        'features' in req.body) {

        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            var query = `UPDATE Hotspot SET User_ID = ?, Data_to_draw = ?`;
            connection.query(query,[req.session.user.User_ID, data], function(err, rows, fields) {
              connection.release(); // release connection
              if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
              }
              res.send("Hotspot has been saved!");
            });
        });


    } else {
        res.sendStatus(400);
    }

});

router.get('/get_hotspots', function(req, res, next) {

    req.pool.getConnection( function(err,connection) {
        if (err) {
          res.sendStatus(500);
          return;
        }
        var query = "SELECT Data_to_draw FROM Hotspot;";
        connection.query(query, function(err, results) {
          connection.release(); // release connection
          if (err) {
            res.sendStatus(500);
            return;
          }
          res.send(results); //send response
        });
    });

});

router.post('/edit_profile', function(req, res, next) {
///////////COVIDFY SIGN UP SYSTEM///////////////
    if( 'username' in req.body &&
        'email' in req.body &&
        'first_name' in req.body &&
        'last_name' in req.body &&
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
            var query = `SELECT Username, Email FROM User WHERE Username=? OR Email=?`;
            connection.query(query,[
                req.body.username,
                req.body.email], function(err, result, field) {
                    if (err) {
                        console.log(err);
                        res.sendStatus(500);
                    }

                    if (result.length === 1) {
                        var query1 = `UPDATE User SET
                                        First_name=?, Last_name=?,
                                        Age=?, Phone_number=?,
                                        Email=?, Username=?
                                    WHERE
                                        User_ID =?;`;

                        connection.query(query1,[
                            req.body.first_name,
                            req.body.last_name,
                            req.body.age,
                            req.body.phone,
                            req.body.email,
                            req.body.username,
                            req.session.user.User_ID
                            ], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                }
                            });

                        var query2 = `UPDATE Address SET
                                        Street=?, Suburb=?,
                                        City=?, State=?,
                                        Postcode=?
                                    WHERE
                                        User_ID =?`;
                        connection.query(query2,[
                            req.body.street,
                            req.body.suburb,
                            req.body.city,
                            req.body.state,
                            req.body.postcode,
                            req.session.user.User_ID], function(err, results) {
                                if (err) {
                                    console.log(err);
                                    res.sendStatus(500);
                                    return;
                                }
                            connection.release(); // release connection

                            res.send("Your data was successfully changed!");
                        });
                    }
                    else{
                        // console.log(rows.length);
                        connection.release();
                        res.send("Username or Email is already existed on database!");

                    }
                });

        });

    } else {
        res.sendStatus(400);
    }

});

router.get('/get_profile', function(req, res, next) {

    req.pool.getConnection( function(err,connection) {
        if (err) {
          res.sendStatus(500);
          return;
        }
        var query = `SELECT * FROM User, Address WHERE User.User_ID = ? AND Address.User_ID = ?;`;
        connection.query(query, [req.session.user.User_ID, req.session.user.User_ID], function(err, rows, fields) {
            // console.log(rows);
          connection.release(); // release connection
          if (err) {
            res.sendStatus(500);
            return;
          }
          delete rows[0].Password;   //You don't want to show the password even if it been encrypted
          res.json(rows); //send response
        });
    });

});

router.post('/change_pw', async function(req, res, next) {
///////////COVIDFY SIGN UP SYSTEM///////////////
    if( 'old_pw' in req.body &&
        'new_pw' in req.body) {

        let hash = await argon2.hash(req.body.new_pw);
        req.pool.getConnection( function(err,connection) {
            if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
            }
            var query = `SELECT Password FROM User WHERE User_ID = ?`;
            connection.query(query,[req.session.user.User_ID], async function(err, rows, fields) {
              if (err) {
                console.log(err);
                res.sendStatus(500);
                return;
              }
              console.log(rows);
              if(rows.length != 0){

                  let valid = await argon2.verify(rows[0].Password, req.body.old_pw);

                  if (valid) {

                    var query2 = `UPDATE User SET
                                    Password=?
                                WHERE
                                    User_ID =?`;
                    connection.query(query2,[
                        hash,
                        req.session.user.User_ID], function(err, results) {
                            if (err) {
                                console.log(err);
                                res.sendStatus(500);
                                return;
                            }
                        connection.release(); // release connection

                        res.send("Password changed");
                    });

                  } else {
                      res.send('Incorrect Old Password')
                  }

                } else {
                  res.sendStatus(401);
                }
            });
        });

    } else {
        res.sendStatus(400);
    }

});

module.exports = router;
