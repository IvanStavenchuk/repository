/*
3.2
������������ ������, ������������ �������������� �������� [PK], �� ������� [Status] � ������� [Position], 
� ������ ����, ��� ������ ������� ����� ������������ �� ����� ������ �������, ��� ���� ������ ������������ 
������� ��� ������� ('-'), 
���� �� ���, �� �� �������� �, 
���� �� ���, �� �� �������� A, 
���� �� ���, �� �� �������� D. 
���� ��� ������� �� ������� �� ������ ������� � ���������� ��������, 
������ ������������ ����� ��������� ������ � ��������� ��������. 
�������� NULL � �������� ����������� ������� �����������.
*/

SELECT pk, position, status
FROM TestObjects t
where	1 = case 
			when t.Status = 'C'  then 1
			when t.Status = 'A'  and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  t2.status = 'C') then 1 
			when t.Status = 'D'  and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  (t2.status = 'C' or t2.status = 'A')) then 1 
			else 0
		end
		and not exists ( select top 1 * from TestObjects t2 where t2.Position = t.Position and  t2.status = '-')
group by pk, position, status
order by pk