using ERP.Master.Api.Enums;
using ERP.Master.Api.VO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;

namespace ERP.Master.Api.DAO
{
    public class PersonDAO : BaseDAO
    {
        public PersonDAO(string connectionString) : base(connectionString) { }

        public PersonDAO(FbConnection fbConn, FbTransaction fbTrans) : base(fbConn, fbTrans) { }

        public List<PessoaVO> ListUltimos10(int idEmpresa, ClassePessoa classe)
        {
            #region SQL
            string SQL = @"SELECT FIRST(10) E.* FROM TBL_ENTIDADE E 
                            WHERE E.ID_EMPRESA = @ID_EMPRESA 
                            AND UPPER(E.CL_PERSON) LIKE '%{0}%' ORDER BY E.ID DESC";
            #endregion

            List<PessoaVO> lista = new List<PessoaVO>();
            FbCommand cmd = this.GetCommand(string.Format(SQL, classe.ToString().ToUpper()));
            this.AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            FbDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                PessoaVO vo = new PessoaVO();
                MapperVO(dr, vo);
                lista.Add(vo);
            }

            return lista;
        }

        private void MapperVO(FbDataReader dr, PessoaVO vo)
        {
            vo.IdEmpresa = dr.GetInt32("ID_EMPRESA");
            vo.Id = dr.GetInt32("ID");
            vo.NmRazaoSocial = dr.GetString("NM_RAZAO_SOCIAL");
            vo.NmFantasia = dr.GetString("NM_FANTASIA");
            vo.TpPessoa = dr.GetInt32("TP_FJ") == 1 ?  Enums.TipoPessoa.JURIDICA : Enums.TipoPessoa.FISICA;
            vo.NuCpfCnpj = dr.GetString("NU_CNPJ");
            vo.NuRgIe = dr.GetString("NU_INSC");
            vo.DsLogradouro = dr.GetString("DS_LOGRADOURO");
            vo.NuLogradouro = dr.GetString("NU_LOGRADOURO");
            vo.DsComplemento = dr.GetString("DS_COMPLEMENTO");
            vo.NmBairro = dr.GetString("NM_BAIRRO");
            vo.IdCidade = dr.GetString("ID_CIDADE");
            vo.NuCep  = dr.GetString("NU_CEP");
            vo.NuFone = dr.GetString("NU_FONE");
            vo.DsEMail = dr.GetString("DS_EMAIL");
            vo.DsHome = dr.GetString("DS_HOME");
            vo.ClPessoa = new List<string>(dr.GetString("CL_PERSON").Split("|"));
            vo.IdStatus = dr.GetInt32("ID_STATUS");
            vo.DtInclusao = dr.GetDateTime("DT_CADASTRO");
            vo.DsObservacao = dr.GetString("DS_OBSERVACAO");
        }

        private List<FieldVO> GetFields(PessoaVO vo)
        {
            List<FieldVO> fields = new List<FieldVO>();

            fields.Add(new FieldVO("NM_RAZAO_SOCIAL", vo.NmRazaoSocial, FbDbType.VarChar, 40));
            fields.Add(new FieldVO("NM_FANTASIA", vo.NmFantasia, FbDbType.VarChar, 40));
            fields.Add(new FieldVO("NU_CNPJ", vo.NuCpfCnpj, FbDbType.VarChar, 120));
            fields.Add(new FieldVO("NU_INSC", vo.NuRgIe, FbDbType.VarChar, 18));
            fields.Add(new FieldVO("TP_FJ", (int)vo.TpPessoa, FbDbType.Integer));
            fields.Add(new FieldVO("DS_LOGRADOURO", vo.DsLogradouro, FbDbType.VarChar, 60));
            fields.Add(new FieldVO("NU_LOGRADOURO", vo.NuLogradouro, FbDbType.VarChar, 10));
            fields.Add(new FieldVO("DS_COMPLEMENTO", vo.DsComplemento, FbDbType.VarChar, 30));
            fields.Add(new FieldVO("NM_BAIRRO", vo.NmBairro, FbDbType.VarChar, 30));
            fields.Add(new FieldVO("ID_CIDADE", vo.IdCidade, FbDbType.Integer));
            fields.Add(new FieldVO("NU_CEP", vo.NuCep, FbDbType.VarChar, 10));
            fields.Add(new FieldVO("NU_FONE", vo.NuFone, FbDbType.VarChar, 50));
            fields.Add(new FieldVO("DS_EMAIL", vo.DsEMail, FbDbType.VarChar, 100));
            fields.Add(new FieldVO("DS_HOME", vo.DsHome, FbDbType.VarChar, 100));
            string classe = "";
            foreach (string s in vo.ClPessoa)
                classe += s + "|";

            fields.Add(new FieldVO("CL_PERSON", classe.Substring(0, classe.Length - 1), FbDbType.VarChar, 100));
            fields.Add(new FieldVO("DS_OBSERVACAO", vo.DsObservacao, FbDbType.Text));

            return fields;
        }

        public PessoaVO GetPessoa(int idEmpresa, int id)
        {
            string SQL = "SELECT * FROM TBL_ENTIDADE WHERE ID_EMPRESA = @ID_EMPRESA AND ID = @ID";

            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "ID", id, FbDbType.Integer);
            PessoaVO vo = null;
            FbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vo = new PessoaVO();
                MapperVO(dr, vo);
            }
            
            return vo;
        }

        public void Save(PessoaVO vo)
        {
            FbCommand cmd = null;
            List<FieldVO> fields = GetFields(vo);
            List<FieldVO> whereFields = new List<FieldVO>();
            string tableName = "TBL_ENTIDADE";

            if (vo.Id == 0)
            {
                vo.Id = GenID("gen_tbl_entidade_id");
                fields.Add(new FieldVO("ID", vo.Id, FbDbType.Integer));
                fields.Add(new FieldVO("ID_EMPRESA", vo.IdEmpresa, FbDbType.Integer));
                cmd = FBUtil.GenerateInsertCommand(fields, tableName, this.FBConection, this.FBTransaction);
            }
            else if (!RecordExist("TBL_ENTIDADE", "ID", vo.IdEmpresa, vo.Id))
                cmd = FBUtil.GenerateInsertCommand(fields, tableName, this.FBConection, this.FBTransaction);
            else
            {
                whereFields.Add(new FieldVO("ID_EMPRESA", vo.IdEmpresa, FbDbType.Integer));
                whereFields.Add(new FieldVO("ID", vo.Id, FbDbType.Integer));
                cmd = FBUtil.GenerateUpdateCommand(fields, tableName, whereFields, this.FBConection, this.FBTransaction);
            }
            cmd.ExecuteNonQuery();
        }


        public void Delete(int idEmpresa, int id)
        {
            string SQL = "DELETE FROM TBL_ENTIDADE WHERE ID_EMPRESA = @ID_EMPRESA AND ID = @ID";

            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "ID", id, FbDbType.Integer);
            cmd.ExecuteNonQuery();
        }
    }

        
    }
