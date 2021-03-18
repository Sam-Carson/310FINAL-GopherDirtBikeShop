
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/16/2021 14:18:34
-- Generated from EDMX file: C:\Users\sam\source\repos\GopherDirtBikeShop\GopherDirtBikeShop\BikesModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GopherDirtBikeShopDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bikes'
CREATE TABLE [dbo].[Bikes] (
    [BikeID] int IDENTITY(1,1) NOT NULL,
    [ComponentID] int  NOT NULL,
    [Type] nvarchar(max)  NOT NULL,
    [Brand] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Size] nvarchar(max)  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Components'
CREATE TABLE [dbo].[Components] (
    [ComponentID] int IDENTITY(1,1) NOT NULL,
    [Fork] nvarchar(max)  NOT NULL,
    [Drivetrain] nvarchar(max)  NOT NULL,
    [RearSuspension] nvarchar(max)  NOT NULL,
    [WheelSize] nvarchar(max)  NOT NULL,
    [Bike_BikeID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BikeID] in table 'Bikes'
ALTER TABLE [dbo].[Bikes]
ADD CONSTRAINT [PK_Bikes]
    PRIMARY KEY CLUSTERED ([BikeID] ASC);
GO

-- Creating primary key on [ComponentID] in table 'Components'
ALTER TABLE [dbo].[Components]
ADD CONSTRAINT [PK_Components]
    PRIMARY KEY CLUSTERED ([ComponentID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Bike_BikeID] in table 'Components'
ALTER TABLE [dbo].[Components]
ADD CONSTRAINT [FK_BikeComponent]
    FOREIGN KEY ([Bike_BikeID])
    REFERENCES [dbo].[Bikes]
        ([BikeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BikeComponent'
CREATE INDEX [IX_FK_BikeComponent]
ON [dbo].[Components]
    ([Bike_BikeID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------