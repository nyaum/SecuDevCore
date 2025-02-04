CREATE TABLE [dbo].[Contact] (
    [ContactID]   INT           IDENTITY (1, 1) NOT NULL,
    [ContactName] VARCHAR (500) NULL,
    [Grade]       VARCHAR (500) NULL,
    [Company]     VARCHAR (500) NULL,
    [Tel]         VARCHAR (500) NULL,
    [Email]       VARCHAR (500) NULL,
    CONSTRAINT [PK__Contact__5C6625BBDE9BBD6B] PRIMARY KEY CLUSTERED ([ContactID] ASC)
);

