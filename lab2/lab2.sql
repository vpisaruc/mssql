use supermarket 
go

-- ��������� ���������
select distinct *
from tbProduct 
where productPrice < 300;

--��������� beetwen
select distinct *
from tbBonusCard
where issueDate between '2000-01-01' and '2005-12-10';


--��������� like
select distinct *
from tbBonusCard
where cardNumber like '359____________';

--��������� in � ��������� �����������
select distinct *
from tbBonusCard
where idTransaction in
	(
		select id 
		from tbTransaction
		where "type" = 'Card'
	);

--��������� exists � ��������� �����������
select distinct *
from tbTransaction
where "type" = 'Cash' and
exists 
	( 
		select * 
		from tbTransaction
		where cashboxNumber = '1' 
	);

--��������� ��������� � ��������� 
select distinct *
from tbTransaction
where paymentAmount > all
	(
		select paymentAmount
		from tbTransaction
		where cashboxNumber = 3
	);

-- ���������� ������� � ���������� �������
select distinct avg(bonusCount) as averageBonusCount 
from tbBonusCard;

--��������� ���������� � ��������� ��������
select *
from tbTransaction t
order by 
	(
		select clientName 
		from tbClient c
		where t.idClient = c.id
	);

--������� ��������� case
select cardNumber,
	case year(issueDate)
		when year(getdate()) then 'This year'
		when year(getdate()) - 1 then 'Last year year'
		else 'A long time ago'
	end as "When"
from tbBonusCard;
 
	
	
--��������� ��������� case
select productName,
case 
when productPrice < 1000
	then '������' 
	else '������'
end as productPriceAnalyse
from tbProduct;

--�������� ����� ��������� ��������� �������

select productName,
case 
when productPrice < 1000
	then '������' 
	else '������'
end as productPriceAnalyse
into #tbAnalyser
from tbProduct;

--��������� ��������������� ���������� � �������� ����������� ������ � ����������� from

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

-- � 3 ������������

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
			where clientName like '%�%' 
			and exists 
			(
				select id
				from tbClient
				where clientEmail like '%@gmail%'
			) 
		)
	);



