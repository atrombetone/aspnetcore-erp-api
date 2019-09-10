using ERP.Master.Api.VO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Master.Api.DAO
{
    public class FBUtil
    {
        public static bool IsProcedureExists(string procName, FbConnection conn, FbTransaction trans)
        {
            string strCMD = @"
                    SELECT rdb$Procedure_name 
                    FROM rdb$procedures 
                    WHERE rdb$Procedure_name = @PROCNAME 
                    AND (rdb$system_flag IS NULL OR rdb$system_flag = 0) ";

            FbCommand cmd = new FbCommand(strCMD, conn, trans);
            cmd.Parameters.Add(new FbParameter("PROCNAME", procName));
            FbDataReader dr = cmd.ExecuteReader();
            return (dr.Read());
        }

        public static bool IsExisteCampoNaTabela(string campo, string tabela, FbConnection conn, FbTransaction trans)
        {
            string strCMD = @"SELECT RDB$FIELD_NAME FROM RDB$RELATION_FIELDS WHERE RDB$RELATION_NAME = '{0}' AND RDB$FIELD_NAME = '{1}'";
            FbCommand cmd = new FbCommand(String.Format(strCMD, tabela, campo), conn, trans);
            FbDataReader dr = cmd.ExecuteReader();
            return (dr.Read());
        }

        public static FbCommand GenerateInsertCommand(List<FieldVO> campos, string tabela, FbConnection fbConnection, FbTransaction fbTransaction)
        {
            string strCampos = "";
            string strParams = "";
            FbCommand cmd = new FbCommand("INSERT INTO {0}({1})VALUES({2})", fbConnection, fbTransaction);
            foreach (FieldVO campo in campos)
            {
                if (campo.IsFieldExist)
                    if (!IsExisteCampoNaTabela(campo.Name, tabela, fbConnection, fbTransaction))
                        continue;

                strCampos += campo.Name + ",";
                strParams += "@" + campo.Name + ",";

                if (campo.Value == null)
                {
                    cmd.Parameters.Add(new FbParameter(campo.Name, null));
                    continue;
                }

                if (campo.Type == FbDbType.VarChar)
                {
                    FbParameter p = new FbParameter(campo.Name, FbDbType.VarChar, campo.Length, campo.Name);
                    p.Value = campo.Value;
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Integer)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = Int32.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Decimal)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = Decimal.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Date)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = DateTime.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = campo.Value.ToString();
                    cmd.Parameters.Add(p);
                }
            }

            cmd.CommandText = String.Format(cmd.CommandText, tabela, strCampos.Substring(0, strCampos.Length - 1), strParams.Substring(0, strParams.Length - 1));

            return cmd;
        }

        public static FbCommand GenerateUpdateCommand(List<FieldVO> campos, string tabela, List<FieldVO> whereFields, FbConnection fBConection, FbTransaction fBTransaction)
        {
            StringBuilder strCampos = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();
            
            FbCommand cmd = new FbCommand("UPDATE {0} SET {1} WHERE {2} 1 = 1 ", fBConection, fBTransaction);
            foreach (FieldVO campo in campos)
            {
                if (campo.IsFieldExist)
                    if (!IsExisteCampoNaTabela(campo.Name, tabela, fBConection, fBTransaction))
                        continue;

                strCampos.Append(string.Format(" {0} = @{0},", campo.Name));

                if (campo.Value == null)
                {
                    cmd.Parameters.Add(new FbParameter(campo.Name, null));
                    continue;
                }

                if (campo.Type == FbDbType.VarChar)
                {
                    FbParameter p = new FbParameter(campo.Name, FbDbType.VarChar, campo.Length, campo.Name);
                    p.Value = campo.Value;
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Integer)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = Int32.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Decimal)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = Decimal.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else if (campo.Type == FbDbType.Date)
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = DateTime.MinValue.TryParse(campo.Value.ToString());
                    cmd.Parameters.Add(p);
                }
                else
                {
                    FbParameter p = new FbParameter(campo.Name, campo.Type, campo.Length);
                    p.Value = campo.Value.ToString();
                    cmd.Parameters.Add(p);
                }
            }
            for (int i = 0; i < whereFields.Count; i++)
            {
                FieldVO campoWhere = whereFields[i];
                FbParameter p = new FbParameter(campoWhere.Name, campoWhere.Type);
                p.Value = campoWhere.Value;
                cmd.Parameters.Add(p);

                strWhere.Append(string.Format(" {0} = @{0}", campoWhere.Name));
                if (i < whereFields.Count)
                    strWhere.Append(" AND ");
            }

            cmd.CommandText = String.Format(cmd.CommandText, tabela, strCampos.ToString().Substring(0, strCampos.Length - 1), strWhere.ToString());

            return cmd;
        }

        public static bool IsTableExists(string tableName, FbConnection fBConection, FbTransaction fBTransaction)
        {
            string SQL = @"SELECT
                               R.RDB$RELATION_NAME
                        FROM
                            RDB$RELATIONS R
                        WHERE
                            R.RDB$RELATION_NAME = upper(@TABLE_NAME)";

            FbCommand cmd = new FbCommand(SQL, fBConection, fBTransaction);
            cmd.Parameters.Add(new FbParameter("TABLE_NAME", tableName));
            FbDataReader dr = cmd.ExecuteReader();
            return (dr.Read());
        }
    }
}
