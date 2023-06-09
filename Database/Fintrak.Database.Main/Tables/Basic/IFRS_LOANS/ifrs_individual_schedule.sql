﻿/*** GL Mapping table ***/

CREATE TABLE [dbo].[ifrs_individual_schedule]
(
	[Id] INT NOT NULL IDENTITY, 
    [RefNo] VARCHAR(50) NOT NULL, 
    [LRR] decimal(18,4) NULL DEFAULT 0, 
	[AmountPrinEnd] decimal(18,4) NULL DEFAULT 0, 
	[Amount] decimal(18,4) NULL DEFAULT 0, 
	[ValueDate] date NULL,    
	[MaturityDate] date NULL, 
	[FeeAmount] decimal(18,4) NULL DEFAULT 0, 
	[RunDate] date NULL,
	[Processed] bit NULL DEFAULT 0,
	[Active] BIT NULL, 
    [Deleted] BIT NULL, 
    [CreatedBy] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(50) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [RowVersion] TIMESTAMP NOT NULL, 
    CONSTRAINT [PK_ifrs_individual_schedule] PRIMARY KEY ([Id]) 
    
)
