/*
3.2
Сформировать запрос, возвращающий идентификаторы объектов [PK], их статусы [Status] и позиции [Position], 
с учётом того, для каждой позиции может возвращаться не более одного объекта, при этом должны возвращаться 
объекты без статуса ('-'), 
если их нет, то со статусом С, 
если их нет, то со статусом A, 
если их нет, то со статусом D. 
Если для позиции не найдено ни одного объекта с подходящим статусом, 
должен возвращаться любой найденный объект с указанной позицией. 
Значения NULL в колонках результатов запроса недопустимы.
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