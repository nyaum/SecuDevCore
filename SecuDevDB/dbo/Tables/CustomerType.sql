CREATE TABLE [dbo].[CustomerType] (
    [CustomerTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [TeamID]           INT           NULL,
    [CustomerTypeName] VARCHAR (500) NULL,
    [InsertDate]       DATETIME      NULL,
    CONSTRAINT [PK_CustomerType] PRIMARY KEY CLUSTERED ([CustomerTypeID] ASC)
);

