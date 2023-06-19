USE master;
GO;

CREATE DATABASE Coddit;
GO;

CREATE TABLE Users (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	Email VARCHAR(100) UNIQUE NOT NULL,
	Username VARCHAR(50) UNIQUE NOT NULL,
	Password VARCHAR(100) NOT NULL,
	DateBirth DATE NOT NULL,
	Picture BINARY(MAX) NULL, /* TODO */
	IsActive BIT DEFAULT 0,
	CreatedAt DATE DEFAULT GETDATE(),
);
GO;

CREATE TABLE Forums (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
	Title VARCHAR(50) UNIQUE NOT NULL,
    Description VARCHAR(150) NOT NULL,
	CreatedAt DATE DEFAULT GETDATE()
);
GO;

CREATE TABLE Posts (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
    UserID BIGINT REFERENCES Users(ID) NOT NULL,
    ForumID BIGINT REFERENCES Forums(ID) NOT NULL,
    Title VARCHAR(50) NOT NULL,
    Message VARCHAR(255) NOT NULL,
    Attachment VARCHAR(MAX) NULL, /* TODO */
    Likes INT DEFAULT 0,
	CreatedAt DATE DEFAULT GETDATE()
);
GO;

CREATE TABLE Comment (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
    UserID BIGINT REFERENCES Users(ID) NOT NULL,
    PostID BIGINT REFERENCES Posts(ID) NULL,
    CommentID BIGINT REFERENCES Comment(ID) NULL,
    Message VARCHAR(255) NOT NULL,
    Likes INT DEFAULT 0,
	CreatedAt DATE DEFAULT GETDATE()
);
GO;

CREATE TABLE Roles (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
    ForumID BIGINT REFERENCES Forums(ID) NOT NULL,
    Title VARCHAR(50) UNIQUE NOT NULL,
    /* TODO */
    CanSeePosts BIT DEFAULT 1,
    CanCreatePosts BIT DEFAULT 0,
    CanInteract BIT DEFAULT 1,
    IsOwner BIT DEFAULT 0,
    IsDefault BIT DEFAULT 0
);
GO;

CREATE TABLE Members (
	ID BIGINT PRIMARY KEY IDENTITY(1,1),
    UserID BIGINT REFERENCES Users(ID) NOT NULL,
    ForumID BIGINT REFERENCES Forums(ID) NOT NULL,
    RoleID BIGINT REFERENCES Roles(ID) NOT NULL
);
GO;