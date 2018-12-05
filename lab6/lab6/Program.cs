using System;
using System.Xml;
using System.IO;

namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Открытие документа, находящегося в файле.
            XmlDocument myDocument = new XmlDocument();
            FileStream myFile = new FileStream("D:/university/database/mssql/lab6/task.xml", FileMode.Open);
            myDocument.Load(myFile);

            // 2. Поиск информации, содержащейся в документе: 
            Console.Write("Имена клиентов:\r\n");
            XmlNodeList names = myDocument.GetElementsByTagName("clientName");
            for (int i = 0; i < names.Count; i++)
                Console.Write(names[i].ChildNodes[0].Value + "\r\n");

            Console.Write("\n\nУ этого клиента ID=6:\r\n");
            XmlElement id = myDocument.GetElementById("6");
            Console.Write(id.ChildNodes[1].ChildNodes[0].Value + "\r\n");

            Console.Write("\n\nИмя клиентов у корых номер 8-(909)-318-98-15:\r\n");
            XmlNodeList cli = myDocument.SelectNodes("//Client/clientName/text()[../../clientTelephoneNumber/text()='8-(909)-318-98-15']");
            for (int i = 0; i < cli.Count; i++)
                Console.Write(cli[i].Value + "\r\n");

            Console.Write("\n\nТелефон первого клиента:\r\n");
            XmlNode cliOne = myDocument.SelectSingleNode("//Client/clientTelephoneNumber/text()[../../clientName/text()='Амина Гуляева']");
            Console.Write(cliOne.Value + "\r\n");


            // 3. Доступ к содержимому узлов:
            Console.Write("\n\n" + myDocument.DocumentElement.ChildNodes[0].Value + "\r\n");

            Console.Write("\n\nИнформация о клиентах: \n");
            XmlNodeList pass = myDocument.GetElementsByTagName("Client");
            for (int i = 0; i < pass.Count; i++)
                Console.Write(pass[i].ChildNodes[0].InnerText + "---------" + pass[i].ChildNodes[3].Value + "\r\n");

            XmlProcessingInstruction myPI = (XmlProcessingInstruction)myDocument.DocumentElement.ChildNodes[0];
            Console.Write("\n\nИнструкция: \nНазвание: " + myPI.Name + "\r\n");
            Console.Write("Дата: " + myPI.Data + "\r\n");

            Console.Write("\n\nID Клиентов: \n");
            for (int i = 0; i < pass.Count; i++)
                Console.Write(pass[i].ChildNodes[0].InnerText + " : " + pass[i].Attributes[0].Value + "\r\n");

            //4. Внесение изменений в документ:
            XmlElement pcElement = (XmlElement)myDocument.GetElementsByTagName("clientTelephoneNumber")[1];
            pass[1].RemoveChild(pcElement);
            Console.Write("Удаление телефона у первого клиента" + "\r\n");
            myDocument.Save("D:/university/database/mssql/lab6/delete.xml");

            XmlNodeList ageValues = myDocument.SelectNodes("//Client/clientTelephoneNumber/text()");
            for (int i = 0; i < ageValues.Count; i++)
                ageValues[i].Value = "Мобильный телефон: " + ageValues[i].Value;
            Console.Write("Изманение форматки телефонов" + "\r\n");
            myDocument.Save("D:/university/database/mssql/lab6/chage.xml");

            XmlElement ClientElement = myDocument.CreateElement("Client");
            XmlElement NameElement = myDocument.CreateElement("clientName");
            XmlElement TelephoneElement = myDocument.CreateElement("clientTelephoneNumber");
            XmlElement EmailElement = myDocument.CreateElement("clientEmail");

            XmlText NameText = myDocument.CreateTextNode("Тюпов Артем");
            XmlText TelephoneText = myDocument.CreateTextNode("+7-(696)-575-43-20");
            XmlText EmailText = myDocument.CreateTextNode("tiup@gmail.com");

            NameElement.AppendChild(NameText);
            TelephoneElement.AppendChild(TelephoneText);
            EmailElement.AppendChild(EmailText);


            ClientElement.AppendChild(NameElement);
            ClientElement.AppendChild(TelephoneElement);
            ClientElement.AppendChild(EmailElement);

            myDocument.DocumentElement.AppendChild(ClientElement);
            myDocument.Save("D:/university/database/mssql/lab6/new.xml");

            XmlDocument newDocument = new XmlDocument();
            FileStream newFile = new FileStream("D:/university/database/mssql/lab6/new.xml", FileMode.Open);
            newDocument.Load(newFile);

            XmlElement newElement = (XmlElement)myDocument.GetElementsByTagName("Client")[7];
            newElement.SetAttribute("ClientId", "31");
            myDocument.Save("D:/university/database/mssql/lab6/addattr.xml");
            

            newFile.Close();
            myFile.Close();
            Console.Read();
        }
    }
}
