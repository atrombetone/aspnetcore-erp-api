using System;
using System.Web;
using System.Configuration;
using System.Resources;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Extensões utilitárias para a classe String.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Remove todos os caracteres de formatação de uma string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string UnFormat(this String s)
    {
        if (s != null)
            return s.Replace("-", "").Replace("/", "").Replace(".", "").Replace(",", "").Replace("(", "").Replace(")", "").Replace(" ", "");
        
        return null;
    }

    /// <summary>
    /// Remove todos os caracteres de formatação de uma string.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Truncate(this String s, int length)
    {
        try
        {
            string aux = s + new String(' ', length);
            return aux.Substring(0, length).TrimEnd();
        }
        catch
        {
            return null;
        }
    }
    


    /// <summary>
    /// Pega o texto do resource
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string GetResource(this String s)
    {
        try
        {
            ResourceManager resmgr = new ResourceManager("JRS.Common.Resources.Integra",
                              Assembly.GetExecutingAssembly());            
            string saux = resmgr.GetString(s, null);

            if (!String.IsNullOrEmpty(saux))
                return saux;
            else
                return s;
        }
        catch {
            return s;
        }
    }

    /// <summary>
    /// Pega o texto do resource e formata de acordo com parâmetros "{0}".
    /// </summary>
    /// <param name="s"></param>
    /// <param name="paramss"></param>
    /// <returns></returns>
    public static string GetResource(this String s, params string[] paramss)
    {        
        //Pega o texto de resource de cada parâmetro passado.
        string par = "";
        for (int i = 0; i < paramss.Length; i++)
        {
            par += paramss[i].GetResource() + "|";
        }

        //Pega a mensagem.
        string msg = s.GetResource();

        try
        {
            //Retorna a mensagem e os parâmetros formatados.
            return String.Format(msg, par.Split('|'));
        }
        catch
        {
            //Se der erro retorna apenas a chave do resource passada.
            return s;
        }
    }

    /// <summary>
    /// Extrai a primeira parte da string utilizando como separador o traço com comando Split('-').
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ExtractCode(this String s)
    {
        try
        {
            string[] arr = s.Split('-');
            return arr[0].Trim();
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// Formata CPF.
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns>000.000.000-00</returns>
    public static string FormatCPF(this string cpf)
    {
        long lCpf = Int64.MinValue;
        Int64.TryParse(cpf.UnFormat(), out lCpf);

        return String.Format(@"{0:000\.000\.000\-00}", lCpf);
    }

    /// <summary>
    /// Formata Data.
    /// </summary>
    /// <param name="sDate"></param>
    /// <returns>yyy/mm/dd</returns>
    public static string FormatDate(this string sDate)
    {
        long lDate = Int64.MinValue;
        Int64.TryParse(sDate, out lDate);

        return String.Format(@"{0:0000/00/00}", lDate);
    }

    /// <summary>
    /// Formata CNPJ.
    /// </summary>
    /// <param name="sCNPJ"></param>
    /// <returns>00.000.0000-00</returns>
    public static string FormatCNPJ(this string sCNPJ)
    {
        long lCNPJ = Int64.MinValue;
        Int64.TryParse(sCNPJ.UnFormat(), out lCNPJ);

        return String.Format(@"{0:00\.000\.000\/0000\-00}", lCNPJ);
    }

    /// <summary>
    /// Formata CEP.
    /// </summary>
    /// <param name="sCEP"></param>
    /// <returns>00000-000</returns>
    public static string FormatCEP(this string sCEP)
    {
        long lCEP = Int64.MinValue;
        Int64.TryParse(sCEP.UnFormat(), out lCEP);

        return String.Format(@"{0:00000\-000}", lCEP);
    }
    
    /// <summary>
    /// Pega o texto do resource
    /// </summary>
    /// <param name="s"></param>
    /// <param name="resFile"></param>
    /// <returns></returns>
    public static string GetResourceCustomFile(this String s, string resFile = "JRS.Common.Resources.Integra")
    {
        try
        {
            ResourceManager resmgr = new ResourceManager(resFile,
                              Assembly.GetExecutingAssembly());
            string saux = resmgr.GetString(s, null);

            if (!String.IsNullOrEmpty(saux))
                return saux;
            else
                return s;
        }
        catch
        {
            return s;
        }
    }

    /// <summary>
    /// Pega o texto do resource e formata de acordo com parâmetros "{0}".
    /// </summary>
    /// <param name="s"></param>
    /// <param name="resFile"></param>
    /// <param name="paramss"></param>
    /// <returns></returns>
    public static string GetResourceCustomFile(this String s, string resFile = "JRS.Common.Resources.Integra", params string[] paramss)
    {
        //Pega o texto de resource de cada parâmetro passado.
        string par = "";
        for (int i = 0; i < paramss.Length; i++)
        {
            par += paramss[i].GetResource() + "|";
        }

        //Pega a mensagem.
        string msg = s.GetResourceCustomFile(resFile);

        try
        {
            //Retorna a mensagem e os parâmetros formatados.
            return String.Format(msg, par.Split('|'));
        }
        catch
        {
            //Se der erro retorna apenas a chave do resource passada.
            return s;
        }
    }

    /// <summary>
    /// Capitaliza a string pra uma melhor apresentação.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string Capitalize(this String s)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
    }

    /// <summary>
    /// Remove todas as ocorrências de não-números da string.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string SomenteNumeros(this String s)
    {
        string ret = String.Empty;
        int i = 0;
        while (i < s.Length)
        {
            if(Regex.IsMatch(s[i].ToString(), "[0-9]"))
                ret += s[i];
            i++;
        }
        return ret;
    }

    /// <summary>
    /// Tenta converter uma string para boolean
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool ToBoolean(this String s)
    {
        if (String.IsNullOrEmpty(s)) return false;

        if (s.ToUpper() == "S" || s.ToUpper() == "TRUE" || s == "1")
            return true;
        else
            return false;
    }

    /// <summary>
    /// Tenta converter uma string para int32
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this String s)
    {
        return Int32.MinValue.TryParse(s);
    }

    /// <summary>
    /// Tenta converter uma string para int64
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ToInt64(this String s)
    {
        return Int64.MinValue.TryParse(s);
    }

    /// <summary>
    /// Tenta converter uma string para DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this String s)
    {
        return DateTime.MinValue.TryParse(s);
    }
    
    /// <summary>
    /// COnverte as datas para o locale do internacional antes de para serializar
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ret"></param>
    private static void ConverterPropriedadesData<T>(T ret)
    {
        PropertyInfo[] props = ret.GetType().GetProperties();
        foreach (var prop in props)
        {
            object dt;
            try
            {
                dt = prop.GetValue(ret, null);
            }
            catch { continue; }
            if (dt != null && dt.GetType() == typeof(DateTime))
            {
                prop.SetValue(ret, ((DateTime)dt).AddHours(-3), null);
            }
        }
    }

    /// <summary>
    /// Retorna a quantidade de repetições do carácter na string
    /// </summary>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static int CountChar(this String s, char c)
    {
        List<int> list = new List<int>();
        int i = 0;
        while ((i = s.IndexOf(c, i)) >= 0)
            list.Add(i++);
        return list.Count;
    }

    /// <summary>
    /// Verifica se a string é um número.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNumber(this String value)
    {        
        foreach(Char c in value.ToCharArray())
            if (!Char.IsDigit(c))
                return false;

        return true;
    }

}
