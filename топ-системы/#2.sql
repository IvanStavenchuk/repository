begin transaction 

/*промежуточные данные для повторного использования*/
IF OBJECT_ID(N'tempdb..#temp_results') IS NOT NULL
	drop table #temp_results
select 
	GroupID, 
		(SELECT ''+convert(nvarchar(max),[CommandID])+','
		FROM [TEST_DEST].[dbo].[AccessGroupCommands] TT 
		WHERE TT.groupid=T.groupid 
		FOR XML PATH(''))
	CommandIDs
into #temp_results
FROM [TEST_DEST].[dbo].[AccessGroupCommands] T
group by groupid

/*определение какие группы остаются, а какие подменяются */
IF OBJECT_ID(N'tempdb..#from_to') IS NOT NULL
	drop table #from_to
select 
	--t4.CommandIDs, 
	t4.GroupID [toGroupID], 
	t5.GroupID [fromGroupID] 
	--,t5.CommandIDs 
into #from_to
from (
	select CommandIDs, min(GroupID) GroupID 
	from #temp_results t3
	group by CommandIDs
	)t4
left join #temp_results t5 
	on t4.CommandIDs = t5.CommandIDs 
	and t4.GroupID != t5.GroupID
where 
	t5.GroupID is not null

select * from #from_to

/*разрываем связи перед удалением*/
update o
set o.AccessGroupID  = t.toGroupID
from [TEST_DEST].[dbo].[objects] o
inner join #from_to t on t.fromGroupID = o.AccessGroupID

/*удаление из ссылающихся таблиц*/
delete a from [TEST_DEST].[dbo].AccessGroupCommands a
inner join #from_to t on t.fromGroupID = a.GroupID

/*удаление из источника*/
delete a from [TEST_DEST].[dbo].AccessGroups a
inner join #from_to t on t.fromGroupID = a.PK

if @@ERROR != 0
	rollback

commit
