CREATE TABLE People (
    [username]    VARCHAR (150)  NOT NULL ,
    [password]    VARCHAR (150)  NOT NULL,
    [firstName]   VARCHAR (150)  NOT NULL,
    [lastName]    VARCHAR (150)  NOT NULL,
    [address]     VARCHAR (100) NOT NULL,
    [email]       VARCHAR (150)  NOT NULL,
    [phone]       VARCHAR (120)  NOT NULL,
    PRIMARY KEY CLUSTERED ([username] ASC)
);

CREATE TABLE Users (
    [userId]      INT           IDENTITY(1,1) NOT NULL,
    [username]    VARCHAR (150) NOT NULL,
    PRIMARY KEY CLUSTERED ([userId] ASC),
    CONSTRAINT FK_Users_Username FOREIGN KEY ([username]) REFERENCES People ([username])
);

CREATE TABLE Providers (
    [providerId]  INT        IDENTITY(1,1)   NOT NULL,
    [username]    VARCHAR (150) NOT NULL,
    [avatar] IMAGE NOT NULL,
    [type] VARCHAR(150) NOT NULL,
    [pricePerHour]       DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([providerId] ASC),
    CONSTRAINT FK_Providers_Username FOREIGN KEY ([username]) REFERENCES People ([username])
);

CREATE TABLE Reservations (
    [reservationId] INT      IDENTITY(1,1)    NOT NULL,
    [userId]        INT                       NOT NULL        ,
    [providerId]    INT                       NOT NULL        , 
    [startDate]     DATETIME                       NOT NULL       ,
    [endDate]       DATETIME                       NOT NULL       ,
    [location]    VARCHAR (100) NOT NULL,
    [description] VARCHAR (255) NOT NULL,
    [status] VARCHAR(10) NOT NULL,
    PRIMARY KEY CLUSTERED ([reservationId] ASC),
    CONSTRAINT [FK_UserId] FOREIGN KEY ([userId]) REFERENCES [Users] ([userId]),
    CONSTRAINT [FK_ProviderId] FOREIGN KEY ([providerId]) REFERENCES Providers ([providerId]),
);