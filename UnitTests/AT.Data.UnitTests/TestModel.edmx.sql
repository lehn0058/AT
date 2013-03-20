
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/20/2013 17:18:14
-- Generated from EDMX file: C:\Users\Brandon\Documents\GitHub\AT\UnitTests\AT.Data.UnitTests\TestModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[Test].[FK_Car_Person]', 'F') IS NOT NULL
    ALTER TABLE [Test].[Car] DROP CONSTRAINT [FK_Car_Person];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[Test].[Car]', 'U') IS NOT NULL
    DROP TABLE [Test].[Car];
GO
IF OBJECT_ID(N'[Test].[Person]', 'U') IS NOT NULL
    DROP TABLE [Test].[Person];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Cars'
CREATE TABLE [Test].[Cars] (
    [Id] int  NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [Year] datetime  NOT NULL,
    [MilesPerGallon] int  NOT NULL,
    [HorsePower] int  NULL,
    [PersonId] int  NULL
);
GO

-- Creating table 'People'
CREATE TABLE [Test].[People] (
    [Id] int  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [Age] smallint  NOT NULL,
    [Height] decimal(18,0)  NULL,
    [IQ] int  NULL,
    [Birthday] datetime  NOT NULL,
    [FavoriteNumber] bigint  NULL,
    [IsMarried] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Cars'
ALTER TABLE [Test].[Cars]
ADD CONSTRAINT [PK_Cars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [Test].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PersonId] in table 'Cars'
ALTER TABLE [Test].[Cars]
ADD CONSTRAINT [FK_Car_Person]
    FOREIGN KEY ([PersonId])
    REFERENCES [Test].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Car_Person'
CREATE INDEX [IX_FK_Car_Person]
ON [Test].[Cars]
    ([PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------