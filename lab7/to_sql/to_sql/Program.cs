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

            var list = supermarket.GetTable<tbClient>();

            foreach (var item in list)
            {
                Console.WriteLine(item.clientName);
            }

            Console.Read();
        }
    }
}
