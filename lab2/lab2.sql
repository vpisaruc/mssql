use supermarket 
go

-- Предикант сравнения
select distinct *
from tbProduct 
where productPrice < 300;

--предикант beetwen
select distinct *
from tbBonusCard
where issueDate between '2000-01-01' and '2005-12-10';


--предикант like
select distinct *
from tbBonusCard
where cardNumber like '359____________';

--предикант in с вложенным подзапросом
select distinct *
from tbBonusCard
where idTransaction in
	(
		select id 
		from tbTransaction
		where "type" = 'Card'
	);

--предикант exists с вложенным подзапросом
select distinct *
from tbTransaction
where "type" = 'Cash' and
exists 
	( 
		select * 
		from tbTransaction
		where cashboxNumber = '1' 
	);

--предикант сравнения с квантором 
select distinct *
from tbTransaction
where paymentAmount > all
	(
		select paymentAmount
		from tbTransaction
		where cashboxNumber = 3
	);

-- агрегатные функции в выражениях стобцов
select distinct avg(bonusCount) as averageBonusCount 
from tbBonusCard;

--скалярные подзапросы в выражения столбцов
select *
from tbTransaction t
order by 
	(
		select clientName 
		from tbClient c
		where t.idClient = c.id
	);

--простое выражение case
select cardNumber,
	case year(issueDate)
		when year(getdate()) then 'This year'
		when year(getdate()) - 1 then 'Last year year'
		else 'A long time ago'
	end as "When"
from tbBonusCard;
 
	
	
--поисковое выражение case
select productName,
case 
when productPrice < 1000
	then 'Дешего' 
	else 'Дорого'
end as productPriceAnalyse
from tbProduct;

--создание новой временной локальной таблицы