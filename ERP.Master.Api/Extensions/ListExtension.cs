using System;
using System.Web;
using System.Configuration;
using System.Resources;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Extensão do objeto de lista.
/// </summary>
public static class ListExtension
{
    /// <summary>
    /// Retorna uma string com todos os elementos da lista (toString()) separados pelo separador passado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="delimiter"></param>
    /// <returns></returns>
    public static string AsDelimited<T>(this List<T> obj, string delimiter)
    {
        List<string> items = new List<string>();
        foreach (T data in obj)
        {
            items.Add(data.ToString());
        }
        return String.Join(delimiter, items.ToArray());
    }


    /// <summary>
    /// Retorna uma string com a propridade escolhida de cada elemento da lista separados pelo separador passado
    /// </summary>
    /// <typeparam name="T">Tipo do campo (não usar tipo do objeto)</typeparam>
    /// <param name="obj"></param>
    /// <param name="delimiter"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    public static string AsDelimited<T>(this List<T> obj, string delimiter, string property)
    {
        List<string> items = new List<string>();
        foreach (T data in obj)
        {
            object value = data.GetType().GetProperty(property).GetValue(data, null);
            items.Add(value.ToString());
        }
        return String.Join(delimiter, items.ToArray());
    }

}
