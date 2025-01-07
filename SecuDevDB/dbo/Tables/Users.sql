CREATE TABLE [dbo].[Users] (
    [UID]            VARCHAR (100) NOT NULL,
    [UserName]       VARCHAR (500) NOT NULL,
    [Password]       VARCHAR (MAX) NOT NULL,
    [Email]          VARCHAR (500) NULL,
    [AuthorityLevel] INT           NOT NULL,
    [InsertDate]     DATETIME      NULL,
    [UpdateDate]     DATETIME      NULL,
    [LastLogin]      DATETIME      NULL,
    [Status]         INT           CONSTRAINT [DF_Users_Status] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UID] ASC)
)