using System;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Collections;
using System.Text;

public class UserDefinedFunctions
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
    //ispraviti
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined, MaxByteSize = 8000)]
    public struct MoreThen : IBinarySerialize
    {
        private int count;

        public void Init()
        {
            count = 0;
        }

        public void Accumulate(SqlInt32 Value, SqlInt32 Value2)
        {
            if (Value > Value2)
                count++;
        }

        public void Merge(MoreThen Group)
        {
            count += Group.count;
        }

        public SqlInt32 Terminate()
        {
            return new SqlInt32(count);
        }

        #region IBinarySerialize Members

        public void Read(System.IO.BinaryReader r)
        {
            count = r.ReadInt32();
        }

        public void Write(System.IO.BinaryWriter w)
        {
            w.Write(count);
        }

        #endregion
    }

    //Определяемую пользователем табличную функцию CLR
    //+table
    [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillRow",
    TableDefinition = "intpart int")]
    public static IEnumerable CalculateAverageBonusCount()
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
                    yield return new AvgCnt(result);
                }
            }
        }

    }

    public static void FillRow(object row, out SqlInt32 word)
    {
        // Разбор строки на отдельные столбцы. 
        word = ((AvgCnt)row).word;
    }

    public class AvgCnt
    {
        public SqlInt32 word;

        public AvgCnt(SqlInt32 c)
        {
            word = c;
        }
    }

    // Хранимую процедуру CLR
    //++
    [Microsoft.SqlServer.Server.SqlProcedure()]
    public static void procedureCLR(SqlInt32 CashBoxNumber)
    {
        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();

            using (SqlCommand selectCashbox = new SqlCommand(
                "SELECT * FROM tbTransaction " +
                "WHERE cashboxNumber = @number", connection))
            {
                SqlParameter modifycashBoxNumber = selectCashbox.Parameters.Add(
                    "@number", SqlDbType.Int);
                modifycashBoxNumber.Value = CashBoxNumber;
            }

        }
    }

    //CLR тригер
    //++
    [Microsoft.SqlServer.Server.SqlTrigger (Name="Trigger", Target="tbClient", Event="FOR DELETE")]
    public static void deleteMe()
    {
        SqlTriggerContext triggerContext = SqlContext.TriggerContext;

        if (triggerContext.TriggerAction == TriggerAction.Delete)
            SqlContext.Pipe.Send("You can't delete while deleteMe active! Sorry, not today..!..");
    }

}

//start sql
//Определяемый пользователем тип данных CLR
[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native, IsByteOrdered = true)]
public struct Telephone : INullable
{
    private bool is_Null;
    private Int64 _countryCode;
    private Int64 _telephoneNumber;

    public bool IsNull
    {
        get
        {
            return (is_Null);
        }
    }

    public static Telephone Null
    {
        get
        {
            Telephone pt = new Telephone();
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
            builder.Append(_countryCode);
            builder.Append("-");
            builder.Append(_telephoneNumber);
            return builder.ToString();
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Telephone Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        Telephone pt = new Telephone();
        string[] xy = s.Value.Split("-".ToCharArray());
        pt._countryCode = Int64.Parse(xy[0]);
        pt._telephoneNumber = Int64.Parse(xy[1]);

        return pt;
    }

    public Int64 countryCode
    {
        get
        {
            return this._countryCode;
        }

        set
        {
            _countryCode = value;
        }
    }

    public Int64 telephoneNumber
    {
        get
        {
            return this._telephoneNumber;
        }

        set
        {
            _telephoneNumber = value;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public void telephoneSize(Telephone pFrom)
    {
        if (pFrom._countryCode != 7)
        {
            SqlContext.Pipe.Send("Это не русский номер");
        }
    }

}



