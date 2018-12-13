using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    class Tasks
    {
        private readonly string connectionString = @"Data Source = VICTORSPC; database = supermarket; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static void Main(string[] args)
        {
            Tasks solution = new Tasks();

            solution.connectedObjects_task_1_ConnectionString();
            solution.connectedObjects_task_2_SimpleScalarSelection();
            solution.connectedObjects_task_3_SqlCommand_SqlDataReader();
            solution.connectedObjects_task_4_SqlCommandWithParameters();
            solution.connectedObjects_task_5_SqlCommand_StoredProcedure();
            solution.disconnectedObjects_task_6_DataSetFromTable();
            solution.disconnectedObjects_task_7_FilterSort();
            solution.disconnectedObjects_8_Insert();
            solution.disconnectedObjects_9_Delete();
            solution.disconnectedObjects_10_Xml();
        }

        public void connectedObjects_task_1_ConnectionString()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 1, "[Connected] Shows connection info.");

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");
                Console.WriteLine("Connection properties:");
                Console.WriteLine("\tConnection string: {0}", connection.ConnectionString);
                Console.WriteLine("\tDatabase:          {0}", connection.Database);
                Console.WriteLine("\tData Source:       {0}", connection.DataSource);
                Console.WriteLine("\tServer version:    {0}", connection.ServerVersion);
                Console.WriteLine("\tConnection state:  {0}", connection.State);
                Console.WriteLine("\tWorkstation id:    {0}", connection.WorkstationId);
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the connection creating. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_2_SimpleScalarSelection()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 2, "[Connected] Simple scalar query.");

            string queryString = @"select count(*) from tbBonusCard";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand scalarQueryCommand = new SqlCommand(queryString, connection);
            Console.WriteLine("Sql command \"{0}\" has been created.", queryString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");
                Console.WriteLine("-------->>> The count of BonusCards is {0}", scalarQueryCommand.ExecuteScalar());
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_3_SqlCommand_SqlDataReader()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 3, "[Connected] DataReader for query.");

            string queryString = @"select id, date, paymentAmount, type, cashboxNumber from tbTransaction where id = 50";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand dataQueryCommand = new SqlCommand(queryString, connection);
            Console.WriteLine("Sql command \"{0}\" has been created.", queryString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");
                SqlDataReader dataReader = dataQueryCommand.ExecuteReader();

                Console.WriteLine("-------->>> Transaction with 50 id: ");
                while (dataReader.Read())
                {
                    Console.WriteLine("\t{0} {1} {2} {3} {4}", dataReader.GetValue(0), dataReader.GetValue(1), dataReader.GetValue(2), dataReader.GetValue(3), dataReader.GetValue(4));
                }
                Console.WriteLine("-------->>> <<<-------");
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_4_SqlCommandWithParameters()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 4, "[Connected] SqlCommand (Insert, Delete).");

            string countQueryString = @"select count(*) from tbClient go";
            string insertQueryString = @"insert into tbClient(id, clientName, clientTelephoneNumber, clientEmail) values (@id, @name, @telephone, @email)";
            string deleteQueryString = @"delete from tbClient where id = @id";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand countQueryCommand = new SqlCommand(countQueryString, connection);
            SqlCommand insertQueryCommand = new SqlCommand(insertQueryString, connection);
            SqlCommand deleteQueryCommand = new SqlCommand(deleteQueryString, connection);

            //parameters
            insertQueryCommand.Parameters.Add("@id", SqlDbType.Int);
            insertQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 200);
            insertQueryCommand.Parameters.Add("@telephone", SqlDbType.VarChar, 200);
            insertQueryCommand.Parameters.Add("@email", SqlDbType.VarChar, 200);
            deleteQueryCommand.Parameters.Add("@id", SqlDbType.Int);

            Console.WriteLine("Sql commands: \n1) \"{0}\"\n\n2) \"{1}\"\n\n3) \"{2}\"\n\nhas been created.\n", countQueryString, insertQueryString, deleteQueryString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.\n");
                Console.WriteLine("Current count of clients: {0}\n", countQueryCommand.ExecuteScalar());
                Console.WriteLine("Inserting a new musician. Input: ");
                Console.Write("- id = ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("- name = ");
                string name = Console.ReadLine();
                Console.Write("- telephone = ");
                string telephone = Console.ReadLine();
                Console.Write("- email = ");
                string email = Console.ReadLine();


                insertQueryCommand.Parameters["@id"].Value = id;
                insertQueryCommand.Parameters["@name"].Value = name;
                insertQueryCommand.Parameters["@telephone"].Value = telephone;
                insertQueryCommand.Parameters["@email"].Value = email;
                deleteQueryCommand.Parameters["@id"].Value = id;

                Console.WriteLine("\nInsert command: {0}", insertQueryCommand.CommandText);
                insertQueryCommand.ExecuteNonQuery();
                Console.WriteLine("------>>> New count of Passengers: {0}", countQueryCommand.ExecuteScalar());

                Console.WriteLine("Delete command: {0}", deleteQueryCommand.CommandText);
                deleteQueryCommand.ExecuteNonQuery();
                Console.WriteLine("------>>> New count of Passegers: {0}", countQueryCommand.ExecuteScalar());
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void connectedObjects_task_5_SqlCommand_StoredProcedure()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 5, "[Connected] Stored procedure.");

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand storedProcedureCommand = connection.CreateCommand();
            storedProcedureCommand.CommandType = CommandType.StoredProcedure;
            storedProcedureCommand.CommandText = "selectCashbox";

            Console.WriteLine("Sql command \"{0}\" has been created.", storedProcedureCommand.CommandText);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.\n");

                

                var outputParamet = storedProcedureCommand.Parameters.Add("@transactionAmount", SqlDbType.Int);
                outputParamet.Direction = ParameterDirection.Output;

                var returnParameter = storedProcedureCommand.Parameters.Add("@retVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                storedProcedureCommand.ExecuteNonQuery();
                var result = returnParameter.Value;

                Console.WriteLine("------>>>Average transaction amount on 1st cashbox: {0}", result);
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_task_6_DataSetFromTable()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 6, "[Disconnected] DataSet from the table.");

            string query = @"select id, paymentAmount from tbTransaction where paymentAmount < 4000";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "lowThen");
                DataTable table = dataSet.Tables["lowThen"];

                Console.WriteLine("Transactions with paymentAmount < 4000:");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["id"]);
                    Console.Write(" ---- {0}\n", row["paymentAmount"]);
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_task_7_FilterSort()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 7, "[Disconnected] Filter and sort.");

            string query = @"select * from tbClient";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "tbClient");
                DataTableCollection tables = dataSet.Tables;

                Console.Write("Input part of name of Client: ");
                string partOfName = Console.ReadLine();
                Console.WriteLine();

                string filter = "clientName like '%" + partOfName + "%'";
                string sort = "clientName asc";
                Console.WriteLine("Client who have name like \"" + partOfName + "\":");
                foreach (DataRow row in tables["tbClient"].Select(filter, sort))
                {
                    Console.Write("{0} ", row["id"]);
                    Console.Write("{0} ", row["clientName"]);
                    Console.Write("{0} ", row["clientTelephoneNumber"]);
                    Console.Write("{0}\n", row["clientEmail"]);
                }
                Console.WriteLine();
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_8_Insert()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 8, "[Disconnected] Insert.");

            string dataCommand = @"select * from tbClient";
            string insertQueryString = @"insert into tbClient(id, clientName, clientTelephoneNumber, clientEmail) values (@id, @name, @telephone, @email)";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                Console.WriteLine("Inserting a new Passenger. Input: ");
                Console.Write("- id = ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("- name = ");
                string name = Console.ReadLine();
                Console.Write("- telephone number = ");
                string telephone = Console.ReadLine();
                Console.Write("- email = ");
                string email  = Console.ReadLine();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Clients");
                DataTable table = dataSet.Tables["Clients"];

                DataRow insertingRow = table.NewRow();
                insertingRow["id"] = id;
                insertingRow["clientName"] = name;
                insertingRow["clientTelephoneNumber"] = telephone;
                insertingRow["clientEmail"] = email;

                table.Rows.Add(insertingRow);

                Console.WriteLine("Clients");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["id"]);
                    Console.Write("{0} ", row["clientName"]);
                    Console.Write("{0} ", row["clientTelephoneNumber"]);
                    Console.Write("{0}\n", row["clientEmail"]);
                }

                SqlCommand insertQueryCommand = new SqlCommand(insertQueryString, connection);
                insertQueryCommand.Parameters.Add("@id", SqlDbType.Int, 5 ,"id");
                insertQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 200, "clientName");
                insertQueryCommand.Parameters.Add("@telephone", SqlDbType.VarChar, 200, "clientTelephoneNumber");
                insertQueryCommand.Parameters.Add("@email", SqlDbType.VarChar, 200, "clientEmail");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "Clients");
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_9_Delete()
        {
            Console.WriteLine("".PadLeft(79, '-'));
            Console.WriteLine("Task #{0}: {1}", 9, "[Disconnected] Delete.");

            string dataCommand = @"select * from tbClient";
            string deleteQueryString = @"delete from tbClient where id = @id";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Deleting the passenger. Input: ");
                Console.Write("- id = ");
                int id = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Clients");
                DataTable table = dataSet.Tables["Clients"];

                string filter = "id = '" + id + "'";
                foreach (DataRow row in table.Select(filter))
                {
                    row.Delete();
                }

                SqlCommand deleteQueryCommand = new SqlCommand(deleteQueryString, connection);
                deleteQueryCommand.Parameters.Add("@id", SqlDbType.Int, 5, "id");

                dataAdapter.DeleteCommand = deleteQueryCommand;
                dataAdapter.Update(dataSet, "Clients");

                Console.WriteLine("Clients");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["id"]);
                    Console.Write("{0} ", row["clientName"]);
                    Console.Write("{0} ", row["clientTelephoneNumber"]);
                    Console.Write("{0}\n", row["clientEmail"]);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql command execution. Message: " + e.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Bad input! " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }

        public void disconnectedObjects_10_Xml()
        {
            Console.WriteLine("".PadLeft(80, '-'));
            Console.WriteLine("Task #{0}: {1}", 10, "WriteXml.");

            string query = @"select * from tbClient";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "tbClient");
                DataTable table = dataSet.Tables["tbClient"];

                dataSet.WriteXml("tbClient.xml");
                Console.WriteLine("Check the PTS.xml file.");
            }
            catch (SqlException e)
            {
                Console.WriteLine("There is a problem during the sql query execution. Message: " + e.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadLine();
        }
    }
}
