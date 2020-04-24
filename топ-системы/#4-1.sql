
/*
а) Требуется сформировать представление ExtraValuesView (VIEW), 
которое позволит одним запросом получить первичный ключ 
значения параметра, 
номер группы, 
идентификатор объекта 
и все значения параметров в отдельной колонке каждое. 
Имя каждой колонки, в которой хранится значение параметра, должно совпадать с идентификатором параметра, 
но начинаться со строки Parameter_, например Parameter_70. 
Не допускается явно (вручную) указывать имена всех требуемых колонок, 
т.к. список значений параметров – переменный (см. выше). 
Подсказка: динамический SQL.


б) Требуется сформировать запрос, читающий данные из этого представления, упорядоченные по группе и по идентификатору объекта.
*/
IF OBJECT_ID('ExtraValuesView') IS NOT NULL
	drop view ExtraValuesView
go

declare @i int,
		@stmt nvarchar(max)

		set @stmt = N'

create view ExtraValuesView 
as
select 
	[PK],
	[Group],
	[Object]
'

declare params_cursor cursor for 
	select distinct Parameter from ExtraValues

open params_cursor
fetch next from params_cursor into @i
while @@FETCH_STATUS = 0 
begin
	set @stmt = @stmt + char(9)  + N',[Parameter_'+ cast(@i as nvarchar(12)) +N'] = case [Parameter] when '+ cast(@i as nvarchar(12)) +N' then [Value] end ' + char(10)  
	fetch next from params_cursor into @i
end
close params_cursor
deallocate params_cursor
set @stmt = @stmt + N' from ExtraValues '+ CHAR(10)
select @stmt
exec sp_executesql @stmt

select * from ExtraValuesView order by [group],PK