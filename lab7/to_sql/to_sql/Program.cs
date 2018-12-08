using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace to_sql
{
    class Program
    {
        static void Main(string[] args)
        {
            linqClassesDataContext supermarket = new linqClassesDataContext();
            
            //выборка всех данных из таблицы
            var list = supermarket.GetTable<tbClient>();

            foreach (var item in list)
            {
                Console.WriteLine(item.clientName);
            }

            Console.Write("\n\n\n");
            Console.ReadKey();

            //выбор бонускарт, где бонусов меньше 300
            var custQuery = supermarket.GetTable<tbBonusCard>().Where(x => x.bonusCount < 300);

            foreach (var item in custQuery)
            {
                Console.Write(item.id + " " + item.cardNumber + "\n");
            }

            Console.Write("\n\n\n");
            Console.ReadKey();

            
            //многотабличный запрос
            var custQuery3 =
                from transact in supermarket.tbTransaction
                join client in supermarket.tbClient on transact.idClient equals client.id
                select new { ClientName = client.clientName, TransactionID = transact.id };

            foreach (var item in custQuery3)
            {
                Console.WriteLine(item.ClientName + "---" + item.TransactionID + "\n");
            }

            Console.Write("\n\n\n");
            Console.ReadKey();

            //добавление
            Console.Write("Нажми, чтобы добавить\n");

            tbClient newClient = new tbClient();

            newClient.id = 201;
            newClient.clientName = "Федя";
            newClient.clientEmail = "fedea@gma.dacd";
            newClient.clientTelephoneNumber = "+79695287941";

            supermarket.tbClient.InsertOnSubmit(newClient);
            supermarket.SubmitChanges();
            Console.ReadKey();

            //изменение
            Console.Write("Нажми, чтобы изменить\n");

            supermarket.GetTable<tbClient>().Where(x => x.id == 201).First().clientName = "НЕФЕДЯ";
         
            supermarket.SubmitChanges();
            Console.ReadKey();

            //удаление
            Console.Write("Нажми, чтобы удалить\n");

            var custQuery4 = supermarket.GetTable<tbClient>().Where(x => x.id == 201);

            if (custQuery4.Count() > 0)
            {
                supermarket.tbClient.DeleteOnSubmit(custQuery4.First());
                supermarket.SubmitChanges();
            }

            Console.Write("Нажми, чтобы выполнить хранимку\n");

            Console.ReadKey();

            //Процедура поиска по ид
            var dataProc = supermarket.GetClient();

            foreach (var item in dataProc)
            {
                Console.WriteLine(item.id + "---" + item.clientName + "\n");
            }

            Console.Read();



        }
    }
}
