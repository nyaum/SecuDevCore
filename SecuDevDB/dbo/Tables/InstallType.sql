CREATE TABLE [dbo].[InstallType] (
    [InstallTypeID]   INT           IDENTITY (1, 1) NOT NULL,
    [InstallTypeName] VARCHAR (MAX) NULL,
    [InsertDate]      DATETIME      NULL,
    CONSTRAINT [PK_InstallType] PRIMARY KEY CLUSTERED ([InstallTypeID] ASC)
);

