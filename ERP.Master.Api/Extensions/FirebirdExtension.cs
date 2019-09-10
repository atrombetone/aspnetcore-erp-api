using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

/// <summary>
/// Extensões para acesso ao banco de dados
/// </summary>
public static class FirebirdExtension
{

    #region FB_COMMAND

    /// <summary>
    /// Define o comando sql como texto 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="textCommand"></param>
    /// <returns></returns>
    public static FbCommand DefinirComandoTexto(this FbCommand command, string textCommand)
    {
        command.CommandText = textCommand;
        command.CommandType = System.Data.CommandType.Text;
        return command;
    }

    /// <summary>
    /// Define o comando sql como procedure
    /// </summary>
    /// <param name="procName"></param>
    /// <returns></returns>
    public static FbCommand DefinirComandoProcedure(this FbCommand command, string procName)
    {
        command.CommandType = System.Data.CommandType.StoredProcedure;
        command.CommandText = procName;
        return command;
    }

    /// <summary>
    /// Define o comando como uma transação
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static FbCommand Transacional(this FbCommand command)
    {
        command.Transaction = command.Connection.BeginTransaction();
        return command;
    }

    /// <summary>
    /// Executa (ExecuteNonQuery) e retorna o comando
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static FbCommand Executar(this FbCommand command)
    {
        command.ExecuteNonQuery();
        return command;
    }

    /// <summary>
    /// Executa (ExecuteScalar) e retorna o comando. T é o tipo de objeto de retorno.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static T Executar<T>(this FbCommand command)
    {
        var temp = command.ExecuteScalar();
        if (temp is DBNull) return default(T);
        else if (temp != null) return (T)temp;
        else return Activator.CreateInstance<T>();
    }

    /// <summary>
    /// Executa o comando e retorna um scalar
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static object ExecutarScalar(this FbCommand command)
    {
        var obj = command.ExecuteScalar();
        if (obj is DBNull)
            return null;
        return obj;
    }

    /// <summary>
    /// Executa o comando de forma manual. A forma de execução é passada como parâmetro action.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static FbCommand Executar(this FbCommand command, Action<FbCommand> action)
    {
        action(command);
        return command;
    }

    /// <summary>
    /// Adiciona um parâmetro ao comando
    /// </summary>
    /// <param name="command"></param>
    /// <param name="parameterName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static FbCommand AdicionarParametro(this FbCommand command, string parameterName, object value)
    {
        command.Parameters.Add(parameterName, value);
        return command;
    }

    /// <summary>
    /// Commita a transação
    /// </summary>
    /// <param name="command"></param>
    public static void Commit(this FbCommand command)
    {
        if (command.Transaction != null)
        {
            command.Transaction.CommitRetaining();
            command.Transaction.Dispose();
        }

    }

    /// <summary>
    /// Commita a transação caso a condicao seja satisfeita
    /// </summary>
    /// <param name="command"></param>
    /// <param name="condicao"></param>
    /// <param name="isRollbackCasoContrario"></param>
    public static void Commit(this FbCommand command, Predicate<FbCommand> condicao, bool isRollbackCasoContrario = true)
    {
        if (command.Transaction != null)
        {
            bool isCommit = condicao(command);

            if (isCommit)
                command.Transaction.CommitRetaining();
            else if(isRollbackCasoContrario)
                command.Transaction.RollbackRetaining();

            if(isCommit || isRollbackCasoContrario)
                command.Transaction.Dispose();
        }
    }

    /// <summary>
    /// Executa um rollback na transação
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public static FbCommand Rollback(this FbCommand command, bool isFinalizarTransacao = true)
    {
        if (command.Transaction != null)
        {
            command.Transaction.RollbackRetaining();
            if(isFinalizarTransacao)
                command.Transaction.Dispose();
        }
        return command;
    }

    /// <summary>
    /// Valida e cria os parâmetros no FBCommand.
    /// </summary>
    /// <param name="cmd"></param>
    /// <param name="sql"></param>
    /// <param name="paramss"></param>
    public static void CriarParametros(this FbCommand cmd, string sql, Dictionary<string, object> paramss)
    {
        List<string> parametrosSQL = new List<string>();
        List<string> parametrosPassados = new List<string>();
        StringBuilder sb = new StringBuilder();
        string[] arr = null;

        if (sql.IndexOf('@') > -1)
        {
            sql = sql.Substring(sql.IndexOf('@')+1);
            arr = sql.Split('@');
        }

        //Parametros do SQL
        foreach (string a in arr)
        {
            int idx = 0;
            if (a.IndexOf(',') > -1)
                idx = a.IndexOf(',');
            else if (a.IndexOf(')') > -1)
                idx = a.IndexOf(')');
            else
                idx = a.IndexOf(';');

            string parameter = a.Substring(0, idx).Trim();
            parametrosSQL.Add(parameter.ToUpper());
        }

        //Parametros Passados
        foreach(KeyValuePair<string, object> kv in paramss)
        {
            parametrosPassados.Add(kv.Key.ToUpper());
        }

        List<string> list1 = parametrosPassados.Except(parametrosSQL).ToList();
        List<string> list2 = parametrosSQL.Except(parametrosPassados).ToList();

        //Após validar coloca os parâmetros
        foreach (KeyValuePair<string, object> kv in paramss)
        {
            cmd.AdicionarParametro(kv.Key, kv.Value);
        }
    }

    #endregion

    #region FB_DATA_READER

    /// <summary>
    /// Processa uma ação para mais de um item lido
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="action"></param>
    public static void ProcessarLista(this FbDataReader reader, Action<FbDataReader> action)
    {
        while (reader.Read())
            action(reader);
    }

    /// <summary>
    /// Processa uma ação para apenas um item
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="action"></param>
    public static void ProcessarObjeto(this FbDataReader reader, Action<FbDataReader> action)
    {
        if (reader.HasRows)
        {
            reader.Read();
            action(reader);
        }
    }

    #endregion
}
