/*
����� ���������� ������������� ��������� ������ � ���������� 
(������ �������� ������� Temporary ������ 0 � ����� �����-�� �������� ������ ������ ������ ������).
*/
begin tran

update a1
set a1.Temporary = 0, a1.Name = case when a1.Name is null or a1.Name = '' then t.NewGroupName end
from AccessGroups a1
inner join
(
	select a.*, NewGroupName = '������' + convert(nvarchar(max),ROW_NUMBER() OVER(ORDER BY pk ASC))  from AccessGroups a
)t on t.PK = a1.PK
where a1.Temporary = 1

/*�������*/
--select * from AccessGroups
--rollback

if @@ERROR != 0
	rollback

commit
