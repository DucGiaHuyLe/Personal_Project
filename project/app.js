var express = require('express');
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');
var session = require('express-session');
var mysql = require('mysql');

var indexRouter = require('./routes/index');
var usersRouter = require('./routes/users');

var app = express();

var dbConnectionPool = mysql.createPool({ host: 'localhost', database: 'Covidfy'});
app.use(function(req, res, next) {
    req.pool = dbConnectionPool;
    next();
});



app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());

app.use(session({                             //           //
  secret: 'a string of your choice',          //           //
  resave: false,                              // THIS CODE //
  saveUninitialized: true,                    //           //
  cookie: { secure: false }                   //           //
}));


app.use(express.static(path.join(__dirname, 'public'), {extensions: ['html']}));

app.use('/', indexRouter);

app.use('/users', usersRouter);
app.use('/', express.static(path.join(__dirname, 'private'), {extensions: ['html']}));







module.exports = app;
