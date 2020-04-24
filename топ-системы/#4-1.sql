
/*
�) ��������� ������������ ������������� ExtraValuesView (VIEW), 
������� �������� ����� �������� �������� ��������� ���� 
�������� ���������, 
����� ������, 
������������� ������� 
� ��� �������� ���������� � ��������� ������� ������. 
��� ������ �������, � ������� �������� �������� ���������, ������ ��������� � ��������������� ���������, 
�� ���������� �� ������ Parameter_, �������� Parameter_70. 
�� ����������� ���� (�������) ��������� ����� ���� ��������� �������, 
�.�. ������ �������� ���������� � ���������� (��. ����). 
���������: ������������ SQL.


�) ��������� ������������ ������, �������� ������ �� ����� �������������, ������������� �� ������ � �� �������������� �������.
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