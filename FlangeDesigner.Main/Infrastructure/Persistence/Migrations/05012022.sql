-- create projects and configurations tables

create table projects
(
    Id INTEGER not null
        primary key autoincrement
        unique,
    Name TEXT
);

create table configurations
(
    Id INTEGER not null
        primary key autoincrement
        unique,
    ProjectId INTEGER not null,
    Dimensions TEXT default '[]'
);