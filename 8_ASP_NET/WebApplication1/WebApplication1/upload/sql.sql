CREATE DATABASE ProfileApplication;
use ProfileApplication;

CREATE TABLE UserData(
	userId int auto_increment,
	firstname varchar(20) NOT NULL,
    lastname varchar(20),
    email varchar(40) NOT NULL unique,
    password varchar(25) NOT NULL,
    countryId int,
    stateId int,
    addressline1 varchar(40) NOT NULL,
    addressline2 varchar(40),
    city varchar(40) NOT NULL,
    zipcode int,
    primary key (userId),
    foreign key (stateId) references state(stateId),
    foreign key (countryId) references country(countryId)
);
DROP TABLE UserData;

INSERT INTO 
  UserData (firstname,lastname,email,password,countryId,stateId,addressline1,addressline2,city,zipcode)
 values("sujeet","rath","suj@gmail.com","123",91,81,"mindfire","","bbsr",761200);

create table country(
	countryId int,
    countryName varchar(30),
    countryAbbr varchar(15),
    primary key (countryId)
);
drop table country;
insert into country values(91,"india","in");
insert into country values(92,"pakistan","pk");

create table state(
	countryId int,
	stateId int,
    stateName varchar(30),
    stateAbbr varchar(15),
    primary key (stateId),
    foreign key (countryId) references country(countryId)
);
drop table state;
insert into state values(91,81,"odisha","od");
insert into state values(91,82,"andhra pradesh","ap");

CREATE TABLE UserRole(
	userId int,
	roleId int,
    primary key(roleId),
    foreign key (userId) references UserData(userId),
    foreign key (roleId) references UserRoleStatic(roleId)
);
insert into userrole values(1,201);
insert into userrole values(2,202);
update userrole set userId = (select userId from userdata where firstname="sujeet"),roleId=(select roleId from userrolestatic where roleName="Admin");

create table UserRoleStatic(
	roleId int,
    roleName varchar(30),
    primary key (roleId)
);
drop table userRoleStatic;
insert into UserRoleStatic values(201,"Admin");
insert into UserRoleStatic values(202,"Database-Manager");
insert into UserRoleStatic values(203,"Intern");

SELECT * from UserData;
truncate table UserData;

SELECT * FROM userrolestatic WHERE roleID = 
    (select roleId from userrole where userId = 
    (select userId from userdata where firstname="sujeet")) ;



