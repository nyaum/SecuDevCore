CREATE TABLE [dbo].[Team] (
    [TeamID]   INT           IDENTITY (1, 1) NOT NULL,
    [TeamName] VARCHAR (500) NULL,
    CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED ([TeamID] ASC)
);

