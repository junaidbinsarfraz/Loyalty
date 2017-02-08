
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/08/2017 11:24:54
-- Generated from EDMX file: D:\Junaid\Github\Loyalty\Loyalty\Loyalty.edmx
-- --------------------------------------------------

USE master
GO

--Create a database
IF EXISTS(SELECT name FROM sys.databases
    WHERE name = 'Loyalty')
    DROP DATABASE Loyalty
GO

CREATE DATABASE Loyalty
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [Loyalty];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserCustomer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customers] DROP CONSTRAINT [FK_UserCustomer];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductProductLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductLines] DROP CONSTRAINT [FK_ProductProductLine];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerProductLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductLines] DROP CONSTRAINT [FK_CustomerProductLine];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductLines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductLines];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Username] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [MobileNumber] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Role] nvarchar(max)  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MemberId] nvarchar(max)  NULL,
    [TotalPoints] float  NULL,
    [RedeemedPoints] float  NULL,
    [AvailablePoints] float  NULL,
    [Balance] float  NULL,
    [User_Id] bigint  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Category] nvarchar(max)  NULL,
    [Code] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [SellingPrice] float  NULL,
    [Quantity] bigint  NULL,
    [TotalSold] bigint  NULL,
    [CostPrice] float  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'ProductLines'
CREATE TABLE [dbo].[ProductLines] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [Quantity] bigint  NULL,
    [Date] datetime  NULL,
    [TrackingNumber] nvarchar(max)  NULL,
    [ProductId] bigint  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Status] bit  NULL,
    [Progress] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductLines'
ALTER TABLE [dbo].[ProductLines]
ADD CONSTRAINT [PK_ProductLines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [User_Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_UserCustomer]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCustomer'
CREATE INDEX [IX_FK_UserCustomer]
ON [dbo].[Customers]
    ([User_Id]);
GO

-- Creating foreign key on [ProductId] in table 'ProductLines'
ALTER TABLE [dbo].[ProductLines]
ADD CONSTRAINT [FK_ProductProductLine]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductProductLine'
CREATE INDEX [IX_FK_ProductProductLine]
ON [dbo].[ProductLines]
    ([ProductId]);
GO

-- Creating foreign key on [CustomerId] in table 'ProductLines'
ALTER TABLE [dbo].[ProductLines]
ADD CONSTRAINT [FK_CustomerProductLine]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerProductLine'
CREATE INDEX [IX_FK_CustomerProductLine]
ON [dbo].[ProductLines]
    ([CustomerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------