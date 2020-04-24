USE [TEST_DEST]
GO
IF EXISTS(SELEct top 1 * from sysobjects where name = 'AccessGroupCommands')
	drop table AccessGroupCommands

IF EXISTS(SELEct top 1 * from sysobjects where name = 'Objects')
	drop table dbo.[Objects]

IF EXISTS(SELEct top 1 * from sysobjects where name = 'AccessGroups')
	drop table AccessGroups

CREATE TABLE [dbo].[AccessGroups](
	[PK] [int] IDENTITY(1000,50) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Temporary] [int] NOT NULL,
	[SystemType] [int] NOT NULL,
 CONSTRAINT [PK_AccessGroups] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AccessGroups] ADD  CONSTRAINT [DF_AccessGroups_Guid]  DEFAULT (newid()) FOR [Guid]
ALTER TABLE [dbo].[AccessGroups] ADD  CONSTRAINT [DF_AccessGroups_Name]  DEFAULT ('') FOR [Name]
ALTER TABLE [dbo].[AccessGroups] ADD  CONSTRAINT [DF_AccessGroups_TypeID]  DEFAULT ((0)) FOR [TypeID]
ALTER TABLE [dbo].[AccessGroups] ADD  CONSTRAINT [DF_AccessGroups_Temporary]  DEFAULT ((0)) FOR [Temporary]
ALTER TABLE [dbo].[AccessGroups] ADD  CONSTRAINT [DF_AccessGroups_SystemType]  DEFAULT ((0)) FOR [SystemType]

CREATE NONCLUSTERED INDEX [IX_AccessGroups_TypeID] ON [dbo].[AccessGroups](
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX [UK_AccessGroups_Guid] ON [dbo].[AccessGroups](
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- [TEST_DEST].[AccessGroupCommands]

USE [TEST_DEST]
GO


CREATE TABLE [dbo].[AccessGroupCommands](
	[CommandID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[Enable] [int] NOT NULL,
) ON [PRIMARY]
GO


-- [TEST_DEST].[Objects]

USE [TEST_DEST]
GO

CREATE TABLE [dbo].[Objects](
	[PK] [int] NOT NULL,
	[ObjectID] [int] NOT NULL,
	[AccessGroupID] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
) ON [PRIMARY]
GO



declare 
	@PK int,
	@Guid uniqueidentifier,
	@Name nvarchar(510),
	@TypeID int,
	@Temporary int,
	@SystemType int,
	@newAccessGroupsId int
declare 
	AccessGroupsCursor cursor
	for select * from TESTS_SOURCE.dbo.AccessGroups;

	open AccessGroupsCursor;
	fetch next from AccessGroupsCursor into @PK, @Guid,@Name, @TypeID, @Temporary, @SystemType;
	while @@FETCH_STATUS = 0
	begin
		insert into AccessGroups (Guid,Name,TypeID,Temporary,SystemType) values (@Guid,@Name, @TypeID, @Temporary, @SystemType)
		set @newAccessGroupsId = @@IDENTITY

		insert into AccessGroupCommands (CommandID,GroupID,[Enable])
		select CommandID,[GroupID] = @newAccessGroupsId,[Enable] from TESTS_SOURCE.dbo.AccessGroupCommands where GroupID = @PK

		insert into [Objects] (PK,ObjectID,AccessGroupID,Name,Guid)
		select PK,ObjectID,[AccessGroupID] = @newAccessGroupsId,Name,Guid from TESTS_SOURCE.dbo.[Objects] where AccessGroupID = @PK

		fetch next from AccessGroupsCursor into @PK,@Guid,@Name, @TypeID, @Temporary, @SystemType
	end
	CLOSE AccessGroupsCursor;
	DEALLOCATE AccessGroupsCursor;
-- Данные

-- [TEST_DEST].[AccessGroups]

USE [TEST_DEST]
GO

CREATE NONCLUSTERED INDEX [IX_AccGroupCommands_CommandID] ON [dbo].[AccessGroupCommands](
	[CommandID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_AccGroupCommands_GroupID] ON [dbo].[AccessGroupCommands](
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_AccGroupCommands_Optimize1] ON [dbo].[AccessGroupCommands](
	[CommandID] ASC,
	[GroupID] ASC,
	[Enable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE [dbo].[AccessGroupCommands] ADD  CONSTRAINT [DF_AccGrpCommands_CommandID]  DEFAULT ((0)) FOR [CommandID]
ALTER TABLE [dbo].[AccessGroupCommands] ADD  CONSTRAINT [DF_AccGrpCommands_GroupID]  DEFAULT ((0)) FOR [GroupID]
ALTER TABLE [dbo].[AccessGroupCommands] ADD  CONSTRAINT [DF_AccGrpCommands_Enable]  DEFAULT ((0)) FOR [Enable]
ALTER TABLE [dbo].[AccessGroupCommands]  WITH CHECK ADD  CONSTRAINT [FK_AccGrpCommands_AccGroups] FOREIGN KEY([GroupID]) REFERENCES [dbo].[AccessGroups] ([PK]) ON DELETE CASCADE
ALTER TABLE [dbo].[AccessGroupCommands] CHECK CONSTRAINT [FK_AccGrpCommands_AccGroups]
ALTER TABLE [dbo].[AccessGroupCommands] ADD  CONSTRAINT [PK_AccessGroupCommands] PRIMARY KEY CLUSTERED 
(
	[CommandID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Objects] ADD  DEFAULT ((0)) FOR [ObjectID]
ALTER TABLE [dbo].[Objects] ADD  DEFAULT ((0)) FOR [AccessGroupID]
ALTER TABLE [dbo].[Objects] ADD  DEFAULT (N'') FOR [Name]
ALTER TABLE [dbo].[Objects] ADD  CONSTRAINT [DF_Objects_Guid]  DEFAULT (newid()) FOR [Guid]
ALTER TABLE [dbo].[Objects]  WITH CHECK ADD  CONSTRAINT [FK_Objects_AccGroups] FOREIGN KEY([AccessGroupID]) REFERENCES [dbo].[AccessGroups] ([PK]) ON DELETE SET DEFAULT ON UPDATE CASCADE
ALTER TABLE [dbo].[Objects] ADD  CONSTRAINT [PK_Objects] PRIMARY KEY CLUSTERED 
(
	[PK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

ALTER TABLE [dbo].[Objects] CHECK CONSTRAINT [FK_Objects_AccGroups]
GO
