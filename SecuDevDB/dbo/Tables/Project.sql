CREATE TABLE [dbo].[Project] (
    [ProjectID]        INT           IDENTITY (1, 1) NOT NULL,
    [CustomerTypeID]   INT           NOT NULL,
    [LocationID]       INT           NOT NULL,
    [SoftwareID]       INT           NOT NULL,
    [InstallTypeID]    INT           NOT NULL,
    [ProjectManagerID] VARCHAR (100) NULL,
    [UIDS]             VARCHAR (MAX) NULL,
    [Content]          VARCHAR (MAX) NULL,
    [Version]          VARCHAR (500) NULL,
    [UUID]             VARCHAR (MAX) NULL,
    [FileName]         VARCHAR (MAX) NULL,
    [InstallDate]      DATETIME      NULL,
    [Status]           INT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC)
);

