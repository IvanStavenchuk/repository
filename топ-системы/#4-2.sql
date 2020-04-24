/*
После успешного выполнения задания 4.1 в базе появилось представление ExtraValuesView, 
однако заказчик решил, что значения NULL в результатах выполнения чтения 
из этого представления следует заменить на строку 
'-'. Требуется сформировать запрос, который возвращает данные из указанного представления, 
при этом в запросе должны быть перечислены все колонки представления, 
а значения NULL заменены на '-'. 
Для формирования списка колонок содержимое таблицы ExtraValues применять нельзя
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
