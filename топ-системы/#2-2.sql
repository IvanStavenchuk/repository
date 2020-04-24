/*
Далее необходимо преобразовать временные группы в постоянные 
(указав значение колонки Temporary равным 0 и задав какое-то название группы вместо пустой строки).
*/
begin tran

update a1
set a1.Temporary = 0, a1.Name = case when a1.Name is null or a1.Name = '' then t.NewGroupName end
from AccessGroups a1
inner join
(
	select a.*, NewGroupName = 'Группа' + convert(nvarchar(max),ROW_NUMBER() OVER(ORDER BY pk ASC))  from AccessGroups a
)t on t.PK = a1.PK
where a1.Temporary = 1

/*отладка*/
--select * from AccessGroups
--rollback

if @@ERROR != 0
	rollback

commit
