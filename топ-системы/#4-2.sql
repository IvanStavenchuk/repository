/*
����� ��������� ���������� ������� 4.1 � ���� ��������� ������������� ExtraValuesView, 
������ �������� �����, ��� �������� NULL � ����������� ���������� ������ 
�� ����� ������������� ������� �������� �� ������ 
'-'. ��������� ������������ ������, ������� ���������� ������ �� ���������� �������������, 
��� ���� � ������� ������ ���� ����������� ��� ������� �������������, 
� �������� NULL �������� �� '-'. 
��� ������������ ������ ������� ���������� ������� ExtraValues ��������� ������
*/

--select sc.* from syscolumns sc
--join sysobjects so on sc.id = so.id and so.name = 'ExtraValuesView' order by colid

declare @stmt nvarchar(max)
set @stmt = N'select ' + char(10)
select @stmt = @stmt + N'[' + sc.name + N'] = isnull([' + sc.name + N'], ''-''),' from syscolumns sc
join sysobjects so on sc.id = so.id and so.name = 'ExtraValuesView'order by colid
set @stmt = @stmt + N' from ExtraValuesView order by [group], [pk]'
select @stmt = replace(@stmt, ', from', ' from')
select @stmt
exec sp_executesql  @stmt
