/*
������������ ������, ������������ �������������� �������� [PK], �� ������� [Status] � ������� [Position], 
� ������ ����, ��� ������ ������� ����� ������������ �� ����� ������ �������, 
��� ���� ������ ������������ ������� �� �������� B, ���� �� ���, �� �� �������� A, ���� �� ���, 
�� �� �������� C, � ���� ����� ���, �� ��� ������� ('-'). 
� ���������� ������� �� ������ ���� ��������, ������� ������� �� ����������� � ��������� ���� �������. 
�������� NULL � �������� ����������� ������� �����������.
*/

SELECT pk, position, status
FROM TestObjects t
where	1 = case 
			when t.Status = 'B'  then 1
			when t.Status = 'A'  and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  t2.status = 'B') then 1 
			when t.Status = 'C'  and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  (t2.status = 'B' or t2.status = 'A')) then 1 
			when t.Status = '-'  and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  (t2.status = 'B' or t2.status = 'A' or t2.status = 'C')) then 1 
			else 0
		end
group by pk, position, status
order by pk
