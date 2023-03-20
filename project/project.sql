
/* Create and switch to Database */
CREATE DATABASE IF NOT EXISTS Covidfy;
USE Covidfy;

/* Create Tables in DATABASE project */

CREATE TABLE User (
  User_ID INT AUTO_INCREMENT,
  First_name VARCHAR(50),
  Last_name VARCHAR(50),
  Username VARCHAR(50) UNIQUE,
  Password VARCHAR(256),
  Email VARCHAR(128) UNIQUE NOT NULL,
  User_Type VARCHAR(10),
  Age INT,
  Phone_number VARCHAR(15),
  PRIMARY KEY (User_ID)
);

CREATE TABLE Address (
  User_ID INT,
  Street VARCHAR(256),
  Suburb VARCHAR(256),
  City VARCHAR(256),
  State VARCHAR(256),
  Postcode INT,
  FOREIGN KEY (User_ID) REFERENCES User(User_ID) ON DELETE CASCADE
);



CREATE TABLE Venue (
  Checkin_Code INT AUTO_INCREMENT,
  User_ID INT UNIQUE,
  Venue_Name VARCHAR(256) ,
  Phone_number VARCHAR(15),
  Coordinate JSON NOT NULL,
  PRIMARY KEY (Checkin_Code),
  FOREIGN KEY (User_ID) REFERENCES User(User_ID) ON DELETE SET NULL
);

CREATE TABLE Venue_Address (
  Checkin_Code INT,
  Street VARCHAR(256) UNIQUE NOT NULL,
  Suburb VARCHAR(256),
  City VARCHAR(256),
  State VARCHAR(256),
  Postcode INT,
  FOREIGN KEY (Checkin_Code) REFERENCES Venue(Checkin_Code) ON DELETE CASCADE
);


CREATE TABLE Checkin (
  Checkin_ID INT AUTO_INCREMENT,
  Checkin_Code INT,
  User_ID INT,
  timestamp DATETIME,
  Coordinate JSON NOT NULL,
  PRIMARY KEY (Checkin_ID),
  FOREIGN KEY (User_ID) REFERENCES User(User_ID) ON DELETE CASCADE,
  FOREIGN KEY (Checkin_Code) REFERENCES Venue(Checkin_Code) ON DELETE SET NULL
);

CREATE TABLE Hotspot (
  User_ID INT,
  Data_to_draw JSON NOT NULL,
  FOREIGN KEY (User_ID) REFERENCES User(User_ID) ON DELETE CASCADE

);
-- Password: ihatecovid
INSERT INTO `User` VALUES (1, 'Le', 'Duc Gia Huy', 'health_admin', '$argon2i$v=19$m=4096,t=3,p=1$3qdrYGyY/r5q2J0s4pql2A$Hwb3ytdSBWSIzOw2P3qF7+GBYQFH/Yu40KV5p4yvxv4', 'admin123@gmail.com', 'Health', 22, 0412345678);
INSERT INTO `Address` VALUES (1, '43 Carrington St', 'Adelaide', 'Adelaide', 'SA', '5000');

INSERT INTO `Hotspot` VALUES (1, '{"type": "something to be Modified by Health Officials"}');