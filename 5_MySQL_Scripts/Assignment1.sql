create table user_info(
	`userId` int primary key auto_increment,
    `firstName` varchar(255) not null,
    `lastName` varchar(255) not null,
    `email` varchar(255) not null,
    `password` varchar(255) not null,
    `countryId` int not null,
    `stateId` int not null,
    `city` varchar(255) not null,
    `addressLine1` varchar(255) not null,
    `addressLine2` varchar(255),
    `zipcode` varchar(255) not null
);
create table user_role(
	`roleId` int primary key,
    `roleName` varchar(255) not null
);
create table role_assigned(
	`userId` int,
    `roleId` int,
	primary key(userId,roleId)
);

create table countries(
	`countryId` int primary key,
    `countryName` varchar(255)
);

create table states(
	`stateId` int primary key,
    `countryId` varchar(255),
    `stateName` varchar(255)
);

select * from role_assigned;

select * from user_role;

select * from user_info;

select * from countries;

select * from states;

ALTER TABLE role_assigned
      ADD CONSTRAINT userRoleFK
      FOREIGN KEY (userId) REFERENCES user_info(userId);
      
ALTER TABLE role_assigned
      ADD CONSTRAINT userRoleFK2
      FOREIGN KEY (roleId) REFERENCES user_role(roleId);
      
ALTER TABLE user_info
      ADD CONSTRAINT userCountryFK
      FOREIGN KEY (countryId) REFERENCES countries(countryId);

ALTER TABLE user_info
      ADD CONSTRAINT userStateFK
      FOREIGN KEY (stateId) REFERENCES states(stateId);

ALTER TABLE states
      ADD CONSTRAINT countryStateFK
      FOREIGN KEY (countryId) REFERENCES countries(countryId);
      
-- get the roles of a particular user
select roleName from user_info i,role_assigned a,user_role r where i.userId=a.userId and a.roleId=r.roleId and i.email="pradosh@gmail.com";

-- get the user info along with country and state of a particular user
select u.*,c.countryName,s.stateName from user_info u,countries c,states s where u.countryId=c.countryId and u.stateId=s.stateId;


