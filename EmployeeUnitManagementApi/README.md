CREATE DATABASE EmployeeUnitManagement;

CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    Login VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(64) NOT NULL,
    StatusActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE Unity (
    Id SERIAL PRIMARY KEY,
    Code VARCHAR(64) UNIQUE NOT NULL,
    Name VARCHAR(100) NOT NULL,
    StatusActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE Collaborator (
    Id SERIAL PRIMARY KEY,
    FullName VARCHAR(100) NOT NULL,
    UserId INT UNIQUE NOT NULL,
    UnityId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES "User"(Id),
    FOREIGN KEY (UnityId) REFERENCES Unity(Id)
);

BearerAuthentication
UserAuth = "123";
PasswordAuth = "123";