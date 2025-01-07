CREATE TABLE [dbo].[Board] (
    [BID]        INT           IDENTITY (1, 1) NOT NULL,
    [UID]        VARCHAR (100) NOT NULL,
    [CID]        INT           NOT NULL,
    [Title]      VARCHAR (500) NULL,
    [Content]    VARCHAR (MAX) NULL,
    [UUID]       VARCHAR (MAX) NULL,
    [FileName]   VARCHAR (MAX) NULL,
    [Status]     INT           CONSTRAINT [DF_Board_Status] DEFAULT ((0)) NOT NULL,
    [UpdateUID]  VARCHAR (100) NULL,
    [InsertDate] DATETIME      NULL,
    [UpdateDate] DATETIME      NULL,
    [InsertIP]   VARCHAR (100) NULL,
    [UpdateIP]   VARCHAR (100) NULL,
    CONSTRAINT [PK__Board__C6DE0D2193E52B2C] PRIMARY KEY CLUSTERED ([BID] ASC)
);

