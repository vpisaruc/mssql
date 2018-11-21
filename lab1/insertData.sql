USE supermarket 
GO


delete from tbProduct;
delete from tbClient;
delete from tbTransaction;
delete from tbOrder;
delete from tbBonusCard;


BULK INSERT tbProduct
FROM 'D:\university\database\mssql\lab1\products_table.txt'
   WITH 
      (
		 CODEPAGE = '1251',
         FIELDTERMINATOR ='\t',
         ROWTERMINATOR ='\n'
      );

BULK INSERT tbClient
FROM 'D:\university\database\mssql\lab1\clients_table.txt'
   WITH 
      (
		 CODEPAGE = '1251',
         FIELDTERMINATOR ='\t',
         ROWTERMINATOR ='\n'
      );


BULK INSERT tbTransaction
FROM 'D:\university\database\mssql\lab1\transactions_table.txt'
   WITH 
      (
		 CODEPAGE = '1251',
         FIELDTERMINATOR ='\t',
         ROWTERMINATOR ='\n'
      );

BULK INSERT tbOrder
FROM 'D:\university\database\mssql\lab1\orders_table.txt'
   WITH 
      (
		 CODEPAGE = '1251',
         FIELDTERMINATOR ='\t',
         ROWTERMINATOR ='\n'
      );

BULK INSERT tbBonusCard
FROM 'D:\university\database\mssql\lab1\bonuscards_table.txt'
   WITH 
      (
		 CODEPAGE = '1251',
         FIELDTERMINATOR ='\t',
         ROWTERMINATOR ='\n'
      );
