using System;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using System.Text;

public partial class UserDefinedFunctions
{
    //Определяемую пользователем скалярную функцию CLR
    [SqlFunctionAttribute(DataAccess = DataAccessKind.Read)]
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
                        result += amountReader.GetSqlInt32(0);
                    }
                    return result;
                }
            }
        }
    }

    //Пользовательскую агрегатную функцию CLR
    [SqlFunctionAttribute(DataAccess = DataAccessKind.Read)]
    public static SqlInt32 transactionCnt(SqlDateTime beginingDate, SqlDateTime endDate)
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
                        result += 1;

                    }
                    return result;
                }
            }
        }
    }

    //Определяемую пользователем табличную функцию CLR
    [SqlFunctionAttribute(DataAccess = DataAccessKind.Read)]
    public static SqlInt32 calculateAverageBonusCount()
    {
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();

            using (SqlCommand selectPaymentAmount = new SqlCommand(
             "SELECT " +
             "[bonusCount]" +
             "FROM [tbBonusCard] ;",
             connection))
            {
                using (SqlDataReader amountReader = selectPaymentAmount.ExecuteReader())
                {
                    SqlInt32 cnt = 0;
                    SqlInt32 totalBonus = 0;
                    SqlInt32 result;
                    while (amountReader.Read())
                    {
                        cnt += 1;
                        totalBonus += amountReader.GetSqlInt32(0);
                    }
                    result = totalBonus / cnt;
                    return result;
                }
            }
        }
    }

    // Хранимую процедуру CLR
    [Microsoft.SqlServer.Server.SqlProcedure()]
    public static void procedureCLR(SqlInt32 CashBoxNumber)
    {
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();

            using (SqlCommand selectCashbox = new SqlCommand(
                "SELECT * FROM Purchasing.Vendor " +
                "WHERE cashboxNumber = @number", connection))
            {
                SqlParameter modifycashBoxNumber = selectCashbox.Parameters.Add(
                    "@number", SqlDbType.Int);
                modifycashBoxNumber.Value = CashBoxNumber;
            }

        }
    }

    //CLR тригер
    [Microsoft.SqlServer.Server.SqlTrigger (Name="Trigger", Target="tbClient", Event="FOR DELETE")]
    public static void deleteMe()
    {
        SqlTriggerContext triggerContext = SqlContext.TriggerContext;

        if (triggerContext.TriggerAction == TriggerAction.Delete)
            SqlContext.Pipe.Send("You can't delete while deleteMe active! Sorry, not today..!..");
    }

}

//Определяемый пользователем тип данных CLR
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, IsByteOrdered = true)]
public struct Point : INullable
{
    private bool is_Null;
    private Int32 _x;
    private Int32 _y;

    public bool IsNull
    {
        get
        {
            return (is_Null);
        }
    }

    public static Point Null
    {
        get
        {
            Point pt = new Point();
            pt.is_Null = true;
            return pt;
        }
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_x);
            builder.Append(", ");
            builder.Append(_y);
            return builder.ToString();
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Point Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        Point pt = new Point();
        string[] xy = s.Value.Split(",".ToCharArray());
        pt.x = Int32.Parse(xy[0]);
        pt.y = Int32.Parse(xy[1]);

        return pt;
    }

    public Int32 x
    {
        get
        {
            return this._x;
        }

        set
        {
            _x = value;
        }
    }

    public Int32 y
    {
        get
        {
            return this._y;
        }

        set
        {
            _y = value;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public Double Distance()
    {
        return DistanceFromXY(0, 0);
    }

    [SqlMethod(OnNullCall = false)]
    public Double DistanceFrom(Point pFrom)
    {
        return DistanceFromXY(pFrom.x, pFrom.y);
    }

    [SqlMethod(OnNullCall = false)]
    public Double DistanceFromXY(Int32 iX, Int32 iY)
    {
        return Math.Sqrt(Math.Pow(iX - _x, 2.0) + Math.Pow(iY - _y, 2.0));
    }
}