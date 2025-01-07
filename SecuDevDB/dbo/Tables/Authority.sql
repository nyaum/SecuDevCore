CREATE TABLE [dbo].[Authority] (
    [AuthorityLevel] INT           IDENTITY (0, 1) NOT NULL,
    [AuthorityName]  VARCHAR (200) NOT NULL,
    CONSTRAINT [PK__Authorit__29B5EF10CDBCECF4] PRIMARY KEY CLUSTERED ([AuthorityLevel] ASC)
);

