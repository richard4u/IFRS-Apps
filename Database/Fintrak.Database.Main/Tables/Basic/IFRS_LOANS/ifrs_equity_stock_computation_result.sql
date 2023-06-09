﻿CREATE TABLE [dbo].[ifrs_equity_stock_computation_result](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [int] NOT NULL,
	[PortfolioID] [varchar](50) NULL,
	[portfolio] [varchar](250) NULL,
	[ID_portfolioGroup] [varchar](250) NULL,
	[ID_portfolioGroupName] [varchar](250) NULL,
	[StockDescription] [varchar](450) NULL,
	[Cost] [money] NULL,
	[MarketQty] [money] NULL,
	[MarketPrice] [money] NULL,
	[Classification] [varchar](50) NULL,
	[Quoted] [bit] NULL,
	[FairValue] [money] NULL,
	[FairValueGainLoss] [money] NULL,
	[fairvaluebasis] [int] NULL,
	[Currency] [varchar](50) NULL,
	[Period] [int] NULL,
	[Year] [int] NULL,
	[RunDate] [date] NULL,
	[CompanyCode] [varchar](50) NULL, 
    CONSTRAINT [PK_ifrs_equity_stock_computation_result] PRIMARY KEY ([id])
) ON [PRIMARY]