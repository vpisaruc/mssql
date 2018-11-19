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