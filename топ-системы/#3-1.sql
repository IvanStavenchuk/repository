/*
Сформировать запрос, возвращающий идентификаторы объектов [PK], их статусы [Status] и позиции [Position], 
с учётом того, для каждой позиции может возвращаться не более одного объекта, 
при этом должны возвращаться объекты со статусом B, если их нет, то со статусом A, если их нет, 
то со статусом C, и если таких нет, то без статуса ('-'). 
В результате запроса не должно быть объектов, статусы которых не перечислены в указанном выше условии. 
Значения NULL в колонках результатов запроса недопустимы.
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
