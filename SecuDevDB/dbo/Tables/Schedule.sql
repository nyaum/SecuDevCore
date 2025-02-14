CREATE TABLE [dbo].[Schedule] (
    [ScheduleID]   INT           IDENTITY (1, 1) NOT NULL,
    [ScheduleName] VARCHAR (MAX) NULL,
    [StartDate]    VARCHAR (100) NULL,
    [EndDate]      VARCHAR (100) NULL,
    CONSTRAINT [PK__Schedule__9C8A5B69976872EF] PRIMARY KEY CLUSTERED ([ScheduleID] ASC)
);

