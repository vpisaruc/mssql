using System;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;

public class HelloWorldProc
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void HelloWorld(out string text)
    {
        SqlContext.Pipe.Send("Hello world!" + Environment.NewLine);
        text = "Hello world!";
    }

}


public partial class UserDefinedFunctions
{
    [SqlFunctionAttribute()]
    public static SqlInt32 ScalarFunc1(SqlDateTime beginingDate, SqlDateTime endDate)
    {
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();
            using (SqlCommand selectPaymentAmount = new SqlCommand(
             "SELECT " +
             "[paymentAmount]" +
             "FROM [tbTransaction] " +
             "WHERE \"date\" BETWEEN @beginingDate and @endDate;",
             connection))
            {
                SqlParameter modifiedBeginingDate = selectPaymentAmount.Parameters.Add(
                    "@beginingDate", SqlDbType.DateTime);

                SqlParameter modifiedEndDate = selectPaymentAmount.Parameters.Add(
                    "@endDate", SqlDbType.DateTime);

                modifiedBeginingDate.Value = beginingDate;
                modifiedEndDate.Value = endDate;

                using (SqlDataReader amountReader = selectPaymentAmount.ExecuteReader())
                {
                    SqlInt32 result = 0;
                    while (amountReader.Read())
                    {
                        result += amountReader.GetSqlInt32(1);

                    }
                    return result;
                }
            }
        }
    }
}