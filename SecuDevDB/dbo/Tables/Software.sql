CREATE TABLE [dbo].[Software] (
    [SoftwareID]   INT           IDENTITY (1, 1) NOT NULL,
    [SoftwareName] VARCHAR (500) NULL,
    [InsertDate]   DATETIME      NULL,
    CONSTRAINT [PK_Software] PRIMARY KEY CLUSTERED ([SoftwareID] ASC)
);

