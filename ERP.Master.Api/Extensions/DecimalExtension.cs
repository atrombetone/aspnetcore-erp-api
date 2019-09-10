using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Configuration;

/// <summary>
/// Extensões utilitárias para a classe Decimal.
/// </summary>
public static class DecimalExtension
{        
    /// <summary>
    /// Tenta fazer o parse de uma string para decimal.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    /// <param name="valDefault"></param>
    /// <returns></returns>
    public static Decimal TryParse(this Decimal i, object obj, Decimal valDefault = 0)
    {        
        if (obj == null) return valDefault;

        string s = obj.ToString();
        try
        {
            i = Decimal.Parse(s);
        }
        catch
        {
            i = valDefault;
        }

        return i;
    }


    /// <summary>
    /// Converte o decimal para dinheiro arredondado e colocando quantidade de casas decimais de acordo com a configuração.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="casasDecimais">Número de casas decimais desejado</param>
    /// <returns></returns>
    public static Decimal FormatarValor(this Decimal d, int casasDecimais)
    {      
        string dstr = Decimal.Round(d, 2).ToString("0." + new string('0', casasDecimais));
        return Decimal.Parse(dstr);
    }

    /// <summary>
    /// Converte o decimal para quantidade(unidade) arredondado e colocando quantidade de casas decimais de acordo com a configuração.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="casasDecimais">Número de casas decimais desejado, qualquer número menor que 0 faz o método pegar o valor da configuração</param>
    /// <returns></returns>
    public static Decimal FormatarQtdade(this Decimal d, int casasDecimais)
    {       
        string dstr = Decimal.Round(d, 2).ToString("0." + new string('0', casasDecimais));
        return Decimal.Parse(dstr);
    }

    /// <summary>
    /// Arredona um número com a quantidade de casas decimais passadas.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="quantidadeCasasDecimais"></param>
    /// <returns></returns>
    public static Decimal Arredondar(this Decimal d, int quantidadeCasasDecimais)
    {
        string dstr = Decimal.Round(d, quantidadeCasasDecimais).ToString("N" + quantidadeCasasDecimais);
        return Decimal.Parse(dstr);
    }

    /// <summary>
    /// Arredona um número com a quantidade de casas decimais passadas
    /// e retorna uma string formatada
    /// </summary>
    /// <param name="d"></param>
    /// <param name="quantidadeCasasDecimais"></param>
    /// <returns>String Formatada com "N" + qtdadeCasasDecimais</returns>
    public static String ArredondarFmt(this Decimal d, int quantidadeCasasDecimais)
    {
        return Decimal.Round(d, quantidadeCasasDecimais).ToString("N" + quantidadeCasasDecimais);
    }
}