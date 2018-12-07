using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    class LINQQueryExpressions
    {
        struct Client
        {
            public int id;
            public string name;
            public string telephoneNumber;
            public string email;

            public Client(int id, string name, string telephoneNumber, string email)
            {
                this.id = id;
                this.name = name;
                this.telephoneNumber = telephoneNumber;
                this.email = email;
            }
        }

        struct Transation
        {
            public int id;
            public int idClient;
            public string date;
            public string time;
            public int paymentAmount;

            public Transation(int id, int idClient, string date, string time, int paymentAmount)
            {
                this.id = id;
                this.idClient = idClient;
                this.date = date;
                this.time = time;
                this.paymentAmount = paymentAmount;
            }
        }

        static void Main()
        {

            Client[] cl;
            Transation[] tr;


            cl = new Client[5];
            tr = new Transation[5];

            cl[0] = new Client(1, "Вася Петров", "+79690524788", "blabla@gmail.com");
            cl[1] = new Client(2, "Вера Кудря", "+79690524789", "blabla1@gmail.com");
            cl[2] = new Client(3, "Лоя Петрова", "+79690524780", "blabla2@gmail.com");
            cl[3] = new Client(4, "Юлия Балабан", "+79690524781", "blabla3@gmail.com");
            cl[4] = new Client(5, "Тюпов Артем", "+79690524782", "blabla4@gmail.com");

            tr[0] = new Transation(1, 2, "18/06/2000", "12:25:47", 500);
            tr[1] = new Transation(2, 1, "18/06/2007", "13:25:47", 600);
            tr[2] = new Transation(3, 3, "18/06/2008", "14:25:47", 700);
            tr[3] = new Transation(4, 1, "18/06/2009", "15:25:47", 800);
            tr[4] = new Transation(5, 5, "18/06/2010", "16:25:47", 900);


            // Простой запрос 
            var custQuery =
                from client in cl
                select client;

            foreach (Client cli in custQuery)
            {
                Console.Write(cli.id + "---" + cli.name + "\n");
            }

            Console.Write("\n\n\n\n");

            //Запрос с where
            var custQuery1 =
                from transact in tr
                where transact.paymentAmount > 600
                select transact;

            foreach (Transation tran in custQuery1)
            {
                Console.Write(tran.id + "---" + tran.paymentAmount + "\n");
            }

            Console.Write("\n\n\n\n");

            //запрос с группировкой
            var custQuery2 =
                from transact in tr
                group transact by transact.date;

            foreach (var tranGr in custQuery2)
            {
                Console.Write(tranGr.Key + "---");
                foreach (Transation transact in tranGr)
                {
                    Console.Write(transact.id + "\n");
                }
            }

            Console.Write("\n\n\n\n");

            //запрос с джойном
            var custQuery3 =
                from transact in tr
                join client in cl on transact.idClient equals client.id
                select new { ClientName = client.name, TransactionID = transact.id};

            foreach (var item in custQuery3)
            {
                Console.WriteLine( item.ClientName + "---" + item.TransactionID + "\n");
            }

            Console.Write("\n\n\n\n");


            //запрос с let и where
            var custQuery4 =
                from client in cl
                let words = client.name.Split(' ')
                from word in words
                select word;

            foreach (var ct in custQuery4)
            {
                Console.Write(ct + "\n");
            }

            Console.Read();
        }
    }
}
