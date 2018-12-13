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
            solution.disconnectedObjects_task_6_DataSetFromTable();/*
            solution.disconnectedObjects_task_7_FilterSort();
            solution.disconnectedObjects_8_Insert();
            solution.disconnectedObjects_9_Delete();
            solution.disconnectedObjects_10_Xml();*/
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

            string query = @"select id, transactionAmount from tbTransation where Survival = 0";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "NotSurv");
                DataTable table = dataSet.Tables["NotSurv"];

                Console.WriteLine("Passengers who not survived:");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Passenger"]);
                    Console.Write(" ---- {0}\n", row["Passengerid"]);
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

            string query = @"select * from PTS";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "PTS");
                DataTableCollection tables = dataSet.Tables;

                Console.Write("Input part of name of Passenger: ");
                string partOfName = Console.ReadLine();
                Console.WriteLine();

                string filter = "Passenger like '%" + partOfName + "%'";
                string sort = "Passenger asc";
                Console.WriteLine("Passenger who have name like \"" + partOfName + "\":");
                foreach (DataRow row in tables["PTS"].Select(filter, sort))
                {
                    Console.Write("{0} ", row["PassengerId"]);
                    Console.Write("{0} ", row["Passenger"]);
                    Console.Write("{0} ", row["TicketId"]);
                    Console.Write("{0}\n", row["Survival"]);
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

            string dataCommand = @"select * from P where Age > 50";
            string insertQueryString = @"insert into P(Passenger, Sex, Age) values (@name, @sex, @age)";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                Console.WriteLine("Inserting a new Passenger. Input: ");
                Console.Write("- name = ");
                string name = Console.ReadLine();
                Console.Write("- sex = ");
                string sex = Console.ReadLine();
                Console.Write("- age = ");
                int age = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Passengers");
                DataTable table = dataSet.Tables["Passengers"];

                DataRow insertingRow = table.NewRow();
                insertingRow["Passenger"] = name;
                insertingRow["Sex"] = sex;
                insertingRow["age"] = age;

                table.Rows.Add(insertingRow);

                Console.WriteLine("Passengers");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Passenger"]);
                    Console.Write("{0} ", row["Age"]);
                    Console.Write("---- {0}\n", row["Sex"]);
                }

                SqlCommand insertQueryCommand = new SqlCommand(insertQueryString, connection);
                insertQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 85, "Passenger");
                insertQueryCommand.Parameters.Add("@sex", SqlDbType.VarChar, 20, "Sex");
                insertQueryCommand.Parameters.Add("@age", SqlDbType.Int, 4, "Age");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "Passengers");
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

            string dataCommand = @"select * from P where Age > 50";
            string deleteQueryString = @"delete from P where Passenger = @name";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Deleting the passenger. Input: ");
                Console.Write("- name = ");
                string name = Console.ReadLine();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataCommand, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Passengers");
                DataTable table = dataSet.Tables["Passengers"];

                string filter = "Passenger = '" + name + "'";
                foreach (DataRow row in table.Select(filter))
                {
                    row.Delete();
                }

                SqlCommand deleteQueryCommand = new SqlCommand(deleteQueryString, connection);
                deleteQueryCommand.Parameters.Add("@name", SqlDbType.VarChar, 85, "Passenger");

                dataAdapter.DeleteCommand = deleteQueryCommand;
                dataAdapter.Update(dataSet, "Passengers");

                Console.WriteLine("Passengers");
                foreach (DataRow row in table.Rows)
                {
                    Console.Write("{0} ", row["Passenger"]);
                    Console.Write("{0} ", row["Age"]);
                    Console.Write("---- {0}\n", row["Sex"]);
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

            string query = @"select * from PTS";

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "PTS");
                DataTable table = dataSet.Tables["PTS"];

                dataSet.WriteXml("PTS.xml");
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
