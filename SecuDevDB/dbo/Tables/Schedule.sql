CREATE TABLE [dbo].[Schedule] (
    [ScheduleID]   INT           IDENTITY (1, 1) NOT NULL,
    [ScheduleName] VARCHAR (MAX) NULL,
    [StartDate]    DATETIME      NULL,
    [EndDate]      DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ScheduleID] ASC)
);

