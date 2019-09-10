using FirebirdSql.Data.FirebirdClient;
using System;

namespace ERP.Master.Api.DAO
{
    public class BaseDAO : IDisposable
    {
        private string _connectionString { get; set; }

        public void Dispose()
        {
            try
            {
                this.FBConection.Close();
            }
            catch { }
        }

        public BaseDAO(string connectionString)
        {
            this._connectionString = connectionString;
            _fbConnection = new FbConnection(connectionString);
            _fbConnection.Open();
        }

        public BaseDAO(FbConnection fbConn, FbTransaction fbTrans)
        {
            _fbConnection = fbConn;
            _fbTransaction = fbTrans;
        }

        public FbConnection FBConection
        {
            get
            {
                if (_fbConnection == null)
                {
                    _fbConnection = new FbConnection(this._connectionString);
                    _fbConnection.Open();
                }

                return _fbConnection;
            }
        }
        private FbConnection _fbConnection;

        public FbTransaction FBTransaction
        {
            get
            {
                if (_fbTransaction == null)
                {
                    _fbTransaction = this.FBConection.BeginTransaction();
                }

                return _fbTransaction;
            }
        }
        private FbTransaction _fbTransaction;

        public FbCommand GetCommand(string SQL)
        {
            return new FbCommand(SQL, this.FBConection, this.FBTransaction);
        }

        protected bool RecordExist(string tableName, string fieldKey, int idCompany, int id)
        {
            string SQL = "SELECT COUNT(*) AS REC_COUNT FROM " + tableName + " WHERE ID_EMPRESA = @ID_EMPRESA AND " + fieldKey + " = @" + fieldKey;
            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idCompany, FbDbType.Integer);
            AddParam(cmd, fieldKey, id, FbDbType.Integer);
            FbDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                return dr.GetInt32("REC_COUNT") > 0;
            }
            return false;
        }

        protected int GenID(string genID)
        {
            string scmd = String.Format("SELECT GEN_ID({0}, 1) FROM RDB$DATABASE", genID);
            FbCommand cmd = new FbCommand(scmd, this.FBConection, this.FBTransaction);
            object o = cmd.ExecuteScalar();
            return o.ToString().ToInt32();
        }

        public void AddParam(FbCommand cmd, string campo, object valor, FbDbType fbDbType, int size = 0)
        {
            if (fbDbType == FbDbType.VarChar)
            {
                FbParameter p = new FbParameter(campo, FbDbType.VarChar, size, campo);
                p.Value = valor;
                cmd.Parameters.Add(p);
            }
            else if (fbDbType == FbDbType.Integer)
            {
                try
                {
                    int? val = (valor == null) ? null : (int?)Int32.Parse(valor.ToString());
                    FbParameter p = new FbParameter(campo, fbDbType);
                    p.Value = val;
                    cmd.Parameters.Add(p);
                }
                catch (Exception ex)
                {
                    throw new Exception("O valor '" + valor + "' não é válido para o campo '" + campo + "'\n\nComando:\n" + cmd.CommandText, ex);
                }
            }
            else if (fbDbType == FbDbType.Decimal)
            {
                try
                {
                    decimal? val = (valor == null) ? null : (decimal?)Decimal.Parse(valor.ToString());
                    FbParameter p = new FbParameter(campo, fbDbType);
                    p.Value = val;
                    cmd.Parameters.Add(p);
                }
                catch (Exception ex)
                {
                    throw new Exception("O valor '" + valor + "' não é válido para o campo '" + campo + "'\n\nComando:\n" + cmd.CommandText, ex);
                }
            }
            else
            {
                FbParameter p = new FbParameter(campo, fbDbType);
                p.Value = valor;
                cmd.Parameters.Add(p);
            }
        }

    }
}
