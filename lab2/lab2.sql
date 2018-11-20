use supermarket 
go

--1 Предикант сравнения
select distinct *
from tbProduct 
where productPrice < 300;

--2 предикант beetwen
select distinct *
from tbBonusCard
where issueDate between '2000-01-01' and '2005-12-10';


--3 предикант like
select distinct *
from tbBonusCard
where cardNumber like '359____________';

--4 предикант in с вложенным подзапросом
select distinct *
from tbBonusCard
where idTransaction in
	(
		select id 
		from tbTransaction
		where "type" = 'Card'
	);

--5 предикант exists с вложенным подзапросом
select distinct *
from tbTransaction
where "type" = 'Cash' and
exists 
	( 
		select * 
		from tbTransaction
		where cashboxNumber = '1' 
	);

--6 предикант сравнения с квантором 
select distinct *
from tbTransaction
where paymentAmount > all
	(
		select paymentAmount
		from tbTransaction
		where cashboxNumber = 3
	);

--7 агрегатные функции в выражениях стобцов
select distinct avg(bonusCount) as averageBonusCount 
from tbBonusCard;

--8 скалярные подзапросы в выражения столбцов
select *
from tbTransaction t
order by 
	(
		select clientName 
		from tbClient c
		where t.idClient = c.id
	);

--9 простое выражение case
select cardNumber,
	case year(issueDate)
		when year(getdate()) then 'This year'
		when year(getdate()) - 1 then 'Last year year'
		else 'A long time ago'
	end as "When"
from tbBonusCard;
 
	
	
--10 поисковое выражение case
select productName,
case 
when productPrice < 1000
	then 'Дешего' 
	else 'Дорого'
end as productPriceAnalyse
from tbProduct;

--11 создание новой временной локальной таблицы

select productName,
case 
when productPrice < 1000
	then 'Дешего' 
	else 'Дорого'
end as productPriceAnalyse
into #tbAnalyser
from tbProduct;

--12 вложенные кореллированные подзапросы в качестве производных таблиц и предложении from

with tmpCountSales as
(
select  p.id,
        p.productName,
		sum(1)            cnt
   from tbOrder       o,
        tbProduct     p
   where p.id = o.idProduct
   group by p.id, p.productName
) 
select tcs.*
from tmpCountSales   tcs
where tcs.cnt = (select max(cts1.cnt) from tmpCountSales cts1)
order by tcs.cnt desc, tcs.productName;

--13 с 3 вложенностью

select id, cardNumber 
from tbBonusCard 
where idTransaction in
	(
		select id
		from tbTransaction
		where "date" between '2000-01-01' and '2018-12-10' 
		and idClient in
		(
			select id 
			from tbClient
			where clientName like '%а%' 
			and exists 
			(
				select id
				from tbClient
				where clientEmail like '%@gmail%'
			) 
		)
	);


--14 group by без having

select  p.id,
        p.productName,
		sum(1)            cnt
   from tbOrder       o,
        tbProduct     p
   where p.id = o.idProduct
   group by p.id, p.productName

--15 group by и having
select id, 
	   avg(paymentAmount) as averageAmount
	   from tbTransaction
	   group by id
	   having avg(paymentAmount) >
		(
			select avg(paymentAmount) as MPrice
			from tbTransaction
		);

--16 Однострочная инструкция INSERT, выполняющая вставку в таблицу одной строки значений
insert tbClient(id, clientName, clientTelephoneNumber, clientEmail)
values (201, 'Генрих Восьмой', '+7-(909)-263-85-18', 'lipomas1889@gmail.com');


--17 Многострочная инструкция INSERT, выполняющая вставку в таблицу результирующего набораданных вложенного подзапроса
insert tbBonusCard (id, idTransaction, idClient, cardNumber, issueDate, bonusCount)
select 
	(
		select max(id) + 1 from tbBonusCard
	), id, idClient, 358031067351234, "date", 256
	from tbTransaction where id = 19;

--18 Простая инструкция UPDATE
update tbProduct 
set productPrice = productPrice * 1.2
where id = 128;

--19 Инструкция UPDATE со скалярным подзапросом в предложении SET
update tbTransaction
set "type" = 
		(
			select "type"
			from tbTransaction
			where id = 1
		)
where id = 37;

--20 Простая инструкция DELETE.
delete tbClient
where id = 201;

--21 Инструкция DELETE с вложенным коррелированным подзапросом в предложении WHERE
delete from tbBonusCard
where id in
	(
		select id 
		from tbBonusCard
		where bonusCount = 256
	);   

--22 Инструкция SELECT, использующая простое обобщенное табличное выражение
with tmpCountSales as
(
select  p.id,
        p.productName,
		sum(1)            cnt
   from tbOrder       o,
        tbProduct     p
   where p.id = o.idProduct
   group by p.id, p.productName
) 
select tcs.*
from tmpCountSales   tcs
where tcs.cnt = (select max(cts1.cnt) from tmpCountSales cts1)
order by tcs.cnt desc, tcs.productName;

--23 Инструкция SELECT, использующая рекурсивное обобщенное табличное выражение
with tmpChilds (id,idRef,productName,cost, amount) as 
(
select   id, 
         cast(null as integer)    idRef,
         productName,
	     productPrice             cost,
		 cast(1   as integer)     amount
   from  tbProduct
   where id between 6 and 10
union all
select   id, 
         id + 5                   idRef,
         productName,
	     productPrice             cost,
		 id                       amount
   from  tbProduct
   where id <= 5
),
tmpData(id, productName, cost, amount) as
(
select   id, productName, cost, amount
   from  tmpChilds  
   where idRef is null
union all
select   b.id,
         b.productName,
		 cast(a.amount * a.cost as integer)   cost,
         b.amount
   from  tmpData   a,
         tmpChilds b
   where a.id = b.idRef
)
select * from tmpData;




--24 Оконные функции. Использование конструкций MIN/MAX/AVG OVER()
select c."clientName",
	   b.id,
	   b.cardNumber,
	   min(bonusCount) over(PARTITION by b.id, b.cardNumber) as minimalBonusCount,
	   max(bonusCount) over(PARTITION by b.id, b.cardNumber) as maximalBonusCount
from tbBonusCard b left outer join tbClient c on c.id = idClient;

--25 Оконные фнкции для устранения дублей
WITH tmp AS
	(
		SELECT *
			,ROW_NUMBER() OVER(PARTITION BY idProduct ORDER BY idTransaction) AS rn
		FROM tbOrder
	)
DELETE FROM tmp 
WHERE rn > 1

select * from tbOrder;