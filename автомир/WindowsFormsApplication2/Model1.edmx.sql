
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2020 13:29:05
-- Generated from EDMX file: C:\Users\Ivan\Documents\Visual Studio 2010\Projects\WindowsFormsApplication2\WindowsFormsApplication2\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [testDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BICDirectoryEntryAccounts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AccountsSet] DROP CONSTRAINT [FK_BICDirectoryEntryAccounts];
GO
IF OBJECT_ID(N'[dbo].[FK_ParticipantInfoBICDirectoryEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ParticipantInfoSet] DROP CONSTRAINT [FK_ParticipantInfoBICDirectoryEntry];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AccountsSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AccountsSet];
GO
IF OBJECT_ID(N'[dbo].[ParticipantInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ParticipantInfoSet];
GO
IF OBJECT_ID(N'[dbo].[BICDirectoryEntrySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BICDirectoryEntrySet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AccountsSet'
CREATE TABLE [dbo].[AccountsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Account] nvarchar(max)  NOT NULL,
    [RegulationAccountType] nvarchar(max)  NOT NULL,
    [CK] nvarchar(max)  NOT NULL,
    [AccountCBRBIC] nvarchar(max)  NOT NULL,
    [DateIn] nvarchar(max)  NOT NULL,
    [AccountStatus] nvarchar(max)  NOT NULL,
    [BICDirectoryEntryId] int  NOT NULL
);
GO

-- Creating table 'ParticipantInfoSet'
CREATE TABLE [dbo].[ParticipantInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameP] nvarchar(max)  NULL,
    [CntrCd] nvarchar(max)  NULL,
    [Rgn] nvarchar(max)  NULL,
    [Ind] nvarchar(max)  NULL,
    [BICDirectoryEntry_Id] int  NOT NULL
);
GO

-- Creating table 'BICDirectoryEntrySet'
CREATE TABLE [dbo].[BICDirectoryEntrySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BIC] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AccountsSet'
ALTER TABLE [dbo].[AccountsSet]
ADD CONSTRAINT [PK_AccountsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParticipantInfoSet'
ALTER TABLE [dbo].[ParticipantInfoSet]
ADD CONSTRAINT [PK_ParticipantInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BICDirectoryEntrySet'
ALTER TABLE [dbo].[BICDirectoryEntrySet]
ADD CONSTRAINT [PK_BICDirectoryEntrySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BICDirectoryEntryId] in table 'AccountsSet'
ALTER TABLE [dbo].[AccountsSet]
ADD CONSTRAINT [FK_BICDirectoryEntryAccounts]
    FOREIGN KEY ([BICDirectoryEntryId])
    REFERENCES [dbo].[BICDirectoryEntrySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BICDirectoryEntryAccounts'
CREATE INDEX [IX_FK_BICDirectoryEntryAccounts]
ON [dbo].[AccountsSet]
    ([BICDirectoryEntryId]);
GO

-- Creating foreign key on [BICDirectoryEntry_Id] in table 'ParticipantInfoSet'
ALTER TABLE [dbo].[ParticipantInfoSet]
ADD CONSTRAINT [FK_ParticipantInfoBICDirectoryEntry]
    FOREIGN KEY ([BICDirectoryEntry_Id])
    REFERENCES [dbo].[BICDirectoryEntrySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ParticipantInfoBICDirectoryEntry'
CREATE INDEX [IX_FK_ParticipantInfoBICDirectoryEntry]
ON [dbo].[ParticipantInfoSet]
    ([BICDirectoryEntry_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------