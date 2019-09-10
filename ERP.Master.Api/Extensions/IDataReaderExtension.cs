using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

/// <summary>
/// Extensões utilitárias para a classe IDataReader
/// </summary>
public static class IDataReaderExtension
{
    /// <summary>
    /// Faz o retorno de um campo small int de valor 0 ou 1 retornar true ou false.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool GetBoolean(this IDataReader reader, string name)
    {
        int ret = 0;
        if (reader[name] != null)
        {
            Int32.TryParse(reader[name].ToString(), out ret);           
        }
        
        return ret == 1;
    }

    /// <summary>
    /// Faz o retorno de um campo small int de valor 0 ou 1 retornar true ou false.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static bool? GetBoolean(this IDataReader reader, string name, bool? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        bool ret = false;
        Boolean.TryParse(reader[name].ToString(), out ret);
        return ret;
    }

    /// <summary>
    /// Retorna uma data do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(this IDataReader reader, string name)
    {
        DateTime dt = DateTime.MinValue;
        if (reader[name] != null)
        {
            DateTime.TryParse(reader[name].ToString(), out dt);
        }
        return dt;
    }

    /// <summary>
    /// Retorna uma data do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static DateTime? GetDateTime(this IDataReader reader, string name, DateTime? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        DateTime dt = DateTime.MinValue;
        DateTime.TryParse(reader[name].ToString(), out dt);
        if (dt == DateTime.MinValue) return null;

        return dt;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Int16 GetInt16(this IDataReader reader, string name)
    {
        Int16 i = Int16.MinValue;
        if (reader[name] != null)
        {
            Int16.TryParse(reader[name].ToString(), out i);
        }
        return i;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static Int16? GetInt16(this IDataReader reader, string name, Int16? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        Int16 i = Int16.MinValue;
        Int16.TryParse(reader[name].ToString(), out i);
        return i;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Int32 GetInt32(this IDataReader reader, string name)
    {
        Int32 i = Int32.MinValue;
        if (reader[name] != null)
        {
            Int32.TryParse(reader[name].ToString(), out i);
        }
        return i;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static Int32? GetInt32(this IDataReader reader, string name, Int32? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        Int32 i = Int32.MinValue;
        Int32.TryParse(reader[name].ToString(), out i);
        return i;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Int64 GetInt64(this IDataReader reader, string name)
    {
        Int64 i = Int64.MinValue;
        if (reader[name] != null)
        {
            Int64.TryParse(reader[name].ToString(), out i);
        }
        return i;
    }

    /// <summary>
    /// Retorna um int do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static Int64? GetInt64(this IDataReader reader, string name, Int64? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        Int64 i = Int64.MinValue;
        Int64.TryParse(reader[name].ToString(), out i);
        return i;
    }

    /// <summary>
    /// Retorna um double do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Double GetDouble(this IDataReader reader, string name)
    {
        Double d = Double.MinValue;
        if (reader[name] != null)
        {
            Double.TryParse(reader[name].ToString(), out d);
        }
        return d;
    }

    /// <summary>
    /// Retorna um double do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static Double? GetDouble(this IDataReader reader, string name, Double? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        Double d = Double.MinValue;
        Double.TryParse(reader[name].ToString(), out d);
        return d;
    }

    /// <summary>
    /// Retorna um decimal do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Decimal GetDecimal(this IDataReader reader, string name)
    {
        Decimal d = Decimal.MinValue;
        if (reader[name] != null)
        {
            Decimal.TryParse(reader[name].ToString(), out d);
        }
        return d;
    }

    /// <summary>
    /// Retorna um decimal do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <param name="returnNullValue"></param>
    /// <returns></returns>
    public static Decimal? GetDecimal(this IDataReader reader, string name, Decimal? returnNullValue)
    {
        if (reader[name] == null)
        {
            return returnNullValue;
        }

        Decimal d = Decimal.MinValue;
        Decimal.TryParse(reader[name].ToString(), out d);
        return d;
    }

    /// <summary>
    /// Retorna um array de bytes de um campo Blob do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static byte[] GetBinary(this IDataReader reader, string name)
    {
        if (reader[name] != null && !String.IsNullOrEmpty(reader[name].ToString()))
        {
            byte[] content = (byte[])reader[name];
            return content;
        }

        return null;
    }

    /// <summary>
    /// Retorna uma string do reader do BD.
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetString(this IDataReader reader, string name)
    {
        if (reader[name] != null)
        {
            return reader[name].ToString();
        }
        return null;
    }

    /// <summary>
    /// Verifica se a coluna existe no datareader
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static bool HasColumn(this IDataRecord dr, string columnName)
    {
        for (int i = 0; i < dr.FieldCount; i++)
        {
            if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                return true;
        }
        return false;
    }
}