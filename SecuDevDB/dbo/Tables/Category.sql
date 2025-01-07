CREATE TABLE [dbo].[Category] (
    [CID]             INT           IDENTITY (0, 1) NOT NULL,
    [CategoryName]    VARCHAR (500) NULL,
    [InsertDate]      DATETIME      NULL,
    [UpdateDate]      DATETIME      NULL,
    [BackgroundColor] VARCHAR (500) CONSTRAINT [DF_Category_BackgroundColor] DEFAULT ('#6c757d') NULL,
    [FontColor]       VARCHAR (500) CONSTRAINT [DF_Category_FontColor] DEFAULT ('#ffffff') NULL,
    PRIMARY KEY CLUSTERED ([CID] ASC)
);

