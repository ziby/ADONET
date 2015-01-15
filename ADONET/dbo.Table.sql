CREATE TABLE [dbo].[Table] (
    [Id]          INT           NOT NULL,
    [LastName]    VARCHAR (50)  NULL,
    [FirstName]   VARCHAR (50)  NOT NULL,
    [Login]       VARCHAR (50)  NOT NULL,
    [Password]    VARCHAR (50)  NOT NULL,
    [LastEntry]   DATETIME      NULL,
    [TextMessage] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

