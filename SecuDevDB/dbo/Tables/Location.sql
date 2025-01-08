CREATE TABLE [dbo].[Location] (
    [LocationID]       INT           IDENTITY (1, 1) NOT NULL,
    [CustomerTypeID]   INT           NULL,
    [MasterLocationID] INT           NOT NULL,
    [LocationName]     VARCHAR (500) NULL,
    [ParentLocationID] INT           NULL,
    [ContactIDs]       VARCHAR (500) NULL,
    [InsertDate]       DATETIME      NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationID] ASC)
);

