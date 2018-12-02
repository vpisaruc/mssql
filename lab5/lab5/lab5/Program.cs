using System;
using System.Xml;
using System.Xml.Schema;

namespace ValidateCS
{
    class Class1
    {
        static void Main(string[] args)
        {
            // Create a cache of schemas, and add two schemas
            XmlSchemaCollection sc = new XmlSchemaCollection();
            sc.Add("", "D:/university/database/mssql/lab5/task1XSDGENERATOR.xsd");
            //sc.Add("", "../../../doctors.xsd");

            // Create a validating reader object
            XmlTextReader tr = new XmlTextReader("D:/university/database/mssql/lab5/task1.xml");
            XmlValidatingReader vr = new XmlValidatingReader(tr);

            // Specify the type of validation required
            vr.ValidationType = ValidationType.Schema;

            // Tell the validating reader to use the schema collection
            vr.Schemas.Add(sc);

            // Register a validation event handler method
            vr.ValidationEventHandler += new ValidationEventHandler(MyHandler);

            // Read and validate the XML document
            try
            {
                int num = 0;
                while (vr.Read())
                {

                    if (vr.NodeType == XmlNodeType.Element &&
                       vr.LocalName == "C")
                    {
                        num++;
                        vr.MoveToElement();


                    }

                }

                Console.WriteLine("Number of Clients: " + num + "\n");
            }
            catch (XmlException ex)
            {
                Console.WriteLine("XMLException occurred: " + ex.Message);
            }
            finally
            {
                vr.Close();
            }
            Console.Read();
        }

        // Validation event handler method
        public static void MyHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation Error: " + e.Message);
        }
        
    }
}