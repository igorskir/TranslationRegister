
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/25/2017 04:06:26
-- Generated from EDMX file: C:\Users\Иван\Documents\Visual Studio 2017\Projects\TranslationReg\TranslationRegistryModel\TRegModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SqlRepository.SqlContext];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Documents_dbo_Projects_ProjectId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_dbo_Documents_dbo_Projects_ProjectId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Documents_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_dbo_Documents_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentStage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stages] DROP CONSTRAINT [FK_DocumentStage];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Projects_dbo_LanguagePairs_LanguagePairId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_dbo_Projects_dbo_LanguagePairs_LanguagePairId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Projects_dbo_ProjectStatuses_ProjectStatuseId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_dbo_Projects_dbo_ProjectStatuses_ProjectStatuseId];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_ProjectUser];
GO
IF OBJECT_ID(N'[dbo].[FK_StageWorkType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stages] DROP CONSTRAINT [FK_StageWorkType];
GO
IF OBJECT_ID(N'[dbo].[FK_User_StageStage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Stage] DROP CONSTRAINT [FK_User_StageStage];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUsers_Projects]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectUsers] DROP CONSTRAINT [FK_ProjectUsers_Projects];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectUsers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectUsers] DROP CONSTRAINT [FK_ProjectUsers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageLanguagePair]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LanguagePairs] DROP CONSTRAINT [FK_LanguageLanguagePair];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageLanguagePair1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LanguagePairs] DROP CONSTRAINT [FK_LanguageLanguagePair1];
GO
IF OBJECT_ID(N'[dbo].[FK_DocumentFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_DocumentFile];
GO
IF OBJECT_ID(N'[dbo].[FK_DocFinalFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Documents] DROP CONSTRAINT [FK_DocFinalFile];
GO
IF OBJECT_ID(N'[dbo].[FK_DocFileStage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Stages] DROP CONSTRAINT [FK_DocFileStage];
GO
IF OBJECT_ID(N'[dbo].[FK_UserUser_Stage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Stage] DROP CONSTRAINT [FK_UserUser_Stage];
GO
IF OBJECT_ID(N'[dbo].[FK_User_StageDocFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User_Stage] DROP CONSTRAINT [FK_User_StageDocFile];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Documents]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Documents];
GO
IF OBJECT_ID(N'[dbo].[DocFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DocFiles];
GO
IF OBJECT_ID(N'[dbo].[LanguagePairs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LanguagePairs];
GO
IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[ProjectStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectStatuses];
GO
IF OBJECT_ID(N'[dbo].[Stages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Stages];
GO
IF OBJECT_ID(N'[dbo].[User_Stage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User_Stage];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[WorkTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkTypes];
GO
IF OBJECT_ID(N'[dbo].[ProjectUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectUsers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Documents'
CREATE TABLE [dbo].[Documents] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [OwnerId] int  NULL,
    [WordsNumber] int  NOT NULL,
    [ProjectId] int  NULL,
    [OriginalFileId] int  NOT NULL,
    [FinalFileId] int  NOT NULL
);
GO

-- Creating table 'DocFiles'
CREATE TABLE [dbo].[DocFiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'LanguagePairs'
CREATE TABLE [dbo].[LanguagePairs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [OriginalLanguageId] int  NOT NULL,
    [TranslationLanguageId] int  NOT NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [WordsNumber] int  NOT NULL,
    [Customer] nvarchar(max)  NULL,
    [LanguagePairId] int  NULL,
    [ProjectStatuseId] int  NULL,
    [CreatorId] int  NOT NULL
);
GO

-- Creating table 'ProjectStatuses'
CREATE TABLE [dbo].[ProjectStatuses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'DocStages'
CREATE TABLE [dbo].[DocStages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DocumentId] int  NOT NULL,
    [WorkTypeId] int  NOT NULL,
    [DocFileId] int  NOT NULL
);
GO

-- Creating table 'User_Stage'
CREATE TABLE [dbo].[User_Stage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Amount] int  NOT NULL,
    [StageId] int  NOT NULL,
    [UserId] int  NOT NULL,
    [DocFileId] int  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkTypes'
CREATE TABLE [dbo].[WorkTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NULL
);
GO

-- Creating table 'ProjectUsers'
CREATE TABLE [dbo].[ProjectUsers] (
    [Projects_Id] int  NOT NULL,
    [Workers_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [PK_Documents]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocFiles'
ALTER TABLE [dbo].[DocFiles]
ADD CONSTRAINT [PK_DocFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LanguagePairs'
ALTER TABLE [dbo].[LanguagePairs]
ADD CONSTRAINT [PK_LanguagePairs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProjectStatuses'
ALTER TABLE [dbo].[ProjectStatuses]
ADD CONSTRAINT [PK_ProjectStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DocStages'
ALTER TABLE [dbo].[DocStages]
ADD CONSTRAINT [PK_DocStages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User_Stage'
ALTER TABLE [dbo].[User_Stage]
ADD CONSTRAINT [PK_User_Stage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkTypes'
ALTER TABLE [dbo].[WorkTypes]
ADD CONSTRAINT [PK_WorkTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Projects_Id], [Workers_Id] in table 'ProjectUsers'
ALTER TABLE [dbo].[ProjectUsers]
ADD CONSTRAINT [PK_ProjectUsers]
    PRIMARY KEY CLUSTERED ([Projects_Id], [Workers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProjectId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_dbo_Documents_dbo_Projects_ProjectId]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Documents_dbo_Projects_ProjectId'
CREATE INDEX [IX_FK_dbo_Documents_dbo_Projects_ProjectId]
ON [dbo].[Documents]
    ([ProjectId]);
GO

-- Creating foreign key on [OwnerId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_dbo_Documents_dbo_Users_UserId]
    FOREIGN KEY ([OwnerId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Documents_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_Documents_dbo_Users_UserId]
ON [dbo].[Documents]
    ([OwnerId]);
GO

-- Creating foreign key on [DocumentId] in table 'DocStages'
ALTER TABLE [dbo].[DocStages]
ADD CONSTRAINT [FK_DocumentStage]
    FOREIGN KEY ([DocumentId])
    REFERENCES [dbo].[Documents]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentStage'
CREATE INDEX [IX_FK_DocumentStage]
ON [dbo].[DocStages]
    ([DocumentId]);
GO

-- Creating foreign key on [LanguagePairId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_dbo_Projects_dbo_LanguagePairs_LanguagePairId]
    FOREIGN KEY ([LanguagePairId])
    REFERENCES [dbo].[LanguagePairs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Projects_dbo_LanguagePairs_LanguagePairId'
CREATE INDEX [IX_FK_dbo_Projects_dbo_LanguagePairs_LanguagePairId]
ON [dbo].[Projects]
    ([LanguagePairId]);
GO

-- Creating foreign key on [ProjectStatuseId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_dbo_Projects_dbo_ProjectStatuses_ProjectStatuseId]
    FOREIGN KEY ([ProjectStatuseId])
    REFERENCES [dbo].[ProjectStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Projects_dbo_ProjectStatuses_ProjectStatuseId'
CREATE INDEX [IX_FK_dbo_Projects_dbo_ProjectStatuses_ProjectStatuseId]
ON [dbo].[Projects]
    ([ProjectStatuseId]);
GO

-- Creating foreign key on [CreatorId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK_ProjectUser]
    FOREIGN KEY ([CreatorId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectUser'
CREATE INDEX [IX_FK_ProjectUser]
ON [dbo].[Projects]
    ([CreatorId]);
GO

-- Creating foreign key on [WorkTypeId] in table 'DocStages'
ALTER TABLE [dbo].[DocStages]
ADD CONSTRAINT [FK_StageWorkType]
    FOREIGN KEY ([WorkTypeId])
    REFERENCES [dbo].[WorkTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StageWorkType'
CREATE INDEX [IX_FK_StageWorkType]
ON [dbo].[DocStages]
    ([WorkTypeId]);
GO

-- Creating foreign key on [StageId] in table 'User_Stage'
ALTER TABLE [dbo].[User_Stage]
ADD CONSTRAINT [FK_User_StageStage]
    FOREIGN KEY ([StageId])
    REFERENCES [dbo].[DocStages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_StageStage'
CREATE INDEX [IX_FK_User_StageStage]
ON [dbo].[User_Stage]
    ([StageId]);
GO

-- Creating foreign key on [Projects_Id] in table 'ProjectUsers'
ALTER TABLE [dbo].[ProjectUsers]
ADD CONSTRAINT [FK_ProjectUsers_Projects]
    FOREIGN KEY ([Projects_Id])
    REFERENCES [dbo].[Projects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Workers_Id] in table 'ProjectUsers'
ALTER TABLE [dbo].[ProjectUsers]
ADD CONSTRAINT [FK_ProjectUsers_Users]
    FOREIGN KEY ([Workers_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectUsers_Users'
CREATE INDEX [IX_FK_ProjectUsers_Users]
ON [dbo].[ProjectUsers]
    ([Workers_Id]);
GO

-- Creating foreign key on [OriginalLanguageId] in table 'LanguagePairs'
ALTER TABLE [dbo].[LanguagePairs]
ADD CONSTRAINT [FK_LanguageLanguagePair]
    FOREIGN KEY ([OriginalLanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageLanguagePair'
CREATE INDEX [IX_FK_LanguageLanguagePair]
ON [dbo].[LanguagePairs]
    ([OriginalLanguageId]);
GO

-- Creating foreign key on [TranslationLanguageId] in table 'LanguagePairs'
ALTER TABLE [dbo].[LanguagePairs]
ADD CONSTRAINT [FK_LanguageLanguagePair1]
    FOREIGN KEY ([TranslationLanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageLanguagePair1'
CREATE INDEX [IX_FK_LanguageLanguagePair1]
ON [dbo].[LanguagePairs]
    ([TranslationLanguageId]);
GO

-- Creating foreign key on [OriginalFileId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_DocumentFile]
    FOREIGN KEY ([OriginalFileId])
    REFERENCES [dbo].[DocFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocumentFile'
CREATE INDEX [IX_FK_DocumentFile]
ON [dbo].[Documents]
    ([OriginalFileId]);
GO

-- Creating foreign key on [FinalFileId] in table 'Documents'
ALTER TABLE [dbo].[Documents]
ADD CONSTRAINT [FK_DocFinalFile]
    FOREIGN KEY ([FinalFileId])
    REFERENCES [dbo].[DocFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocFinalFile'
CREATE INDEX [IX_FK_DocFinalFile]
ON [dbo].[Documents]
    ([FinalFileId]);
GO

-- Creating foreign key on [DocFileId] in table 'DocStages'
ALTER TABLE [dbo].[DocStages]
ADD CONSTRAINT [FK_DocFileStage]
    FOREIGN KEY ([DocFileId])
    REFERENCES [dbo].[DocFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocFileStage'
CREATE INDEX [IX_FK_DocFileStage]
ON [dbo].[DocStages]
    ([DocFileId]);
GO

-- Creating foreign key on [UserId] in table 'User_Stage'
ALTER TABLE [dbo].[User_Stage]
ADD CONSTRAINT [FK_UserUser_Stage]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserUser_Stage'
CREATE INDEX [IX_FK_UserUser_Stage]
ON [dbo].[User_Stage]
    ([UserId]);
GO

-- Creating foreign key on [DocFileId] in table 'User_Stage'
ALTER TABLE [dbo].[User_Stage]
ADD CONSTRAINT [FK_DocFileUser_Stage]
    FOREIGN KEY ([DocFileId])
    REFERENCES [dbo].[DocFiles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DocFileUser_Stage'
CREATE INDEX [IX_FK_DocFileUser_Stage]
ON [dbo].[User_Stage]
    ([DocFileId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------