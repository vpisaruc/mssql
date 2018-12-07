using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace to_xml
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadXML();
            Console.Write("\n\n\n");
            ReadFirstXML();
            Console.Write("\n\n\n");
            UpdateXML();
            Console.Write("\n\n\n");
            WriteXML();
            Console.ReadLine();
        }

        // ПРИМЕР 1. ЧТЕНИЕ ДАННЫХ ИЗ ДОКУМЕНТА XML
        static void ReadXML()
        {
            XDocument xdoc = XDocument.Load(@"D:/university/database/mssql/lab6/task.xml");
            var query = from people in xdoc.Descendants("Client")
                        select people.Value;
            Console.WriteLine("Найдено  {0} персонажей", query.Count());
            Console.WriteLine();
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }

        // ПРИМЕР 2. ЧТЕНИЕ ОДНОГО ЭЛЕМЕНТА ИЗ ДОКУМЕНТА XML
        static void ReadFirstXML()
        {
            XDocument xdoc = XDocument.Load(@"D:/university/database/mssql/lab6/task.xml");
            Console.WriteLine(xdoc.Element("gmailUsers").Element("Client").Element("clientName").Value);
        }
        
        // ПРИМЕР 3. МОДИФИКАЦИЯ ДАННЫХ В ДОКУМЕНТЕ XML
        static void UpdateXML()
        {
            XDocument xdoc = XDocument.Load(@"D:/university/database/mssql/lab6/task.xml");
            xdoc.Element("gmailUsers").Element("Client").Element("clientTelephoneNumber").SetValue("1237847234");
            Console.WriteLine(xdoc.Element("gmailUsers").Element("Client").Element("clientTelephoneNumber").Value);
        }

        // ПРИМЕР 4. ЗАПИСЬ ДАННЫХ В ДОКУМЕНТ XML
        static void WriteXML()
        {
            XDocument xdoc = XDocument.Load(@"D:/university/database/mssql/lab6/task.xml");
            XElement xe = new XElement("Client", "Нечто");
            xdoc.Element("gmailUsers").Add(xe);
            var query = from people in xdoc.Descendants("Client")
                        select people.Value;
            Console.WriteLine("Найдено {0} персонажей", query.Count());
            Console.WriteLine();
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}
