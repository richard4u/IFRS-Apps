﻿/* List of Triggered Processs */

CREATE TABLE [dbo].[cor_processjob]
(
	[ProcessJobId] INT NOT NULL IDENTITY, 
	[Code] VARCHAR(200) NULL, 
    [Status] INT NULL, 
    [Remark] VARCHAR(MAX) NULL, 
    [UserName] VARCHAR(50) NULL, 
	[StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [RunTime] DATETIME NULL, 
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL,
    CONSTRAINT [PK_cor_processjob] PRIMARY KEY ([ProcessJobId])
)
