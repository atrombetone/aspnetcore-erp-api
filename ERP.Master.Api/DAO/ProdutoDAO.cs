using ERP.Master.Api.VO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.DAO
{
    public class ProdutoDAO : BaseDAO
    {
        public ProdutoDAO(string connectionString) : base(connectionString) {}

        public ProdutoDAO(FbConnection fbConn, FbTransaction fbTrans) : base(fbConn, fbTrans) {}

        public List<ProdutoVO> GetProdutos(int idEmpresa, int page = 99999999)
        {
            string SQL = @"SELECT FIRST(20) P.* FROM TBL_PRODUTO P
                            WHERE
                                P.ID_EMPRESA = @ID_EMPRESA
                                AND P.ID < @PAGE_ID
                            ORDER BY P.ID";
            List<ProdutoVO> lista = new List<ProdutoVO>();
            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "PAGE_ID", page, FbDbType.Integer);
            FbDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                ProdutoVO vo = new ProdutoVO();
                MapperVO(dr, vo);
                lista.Add(vo);
            }
            return lista;
        }

        private void MapperVO(FbDataReader dr, ProdutoVO vo)
        {
            vo.IdEmpresa = dr.GetInt32("ID");
            vo.Id = dr.GetInt32("ID");
            vo.NmProduto = dr.GetString("NM_PRODUTO");
            vo.CdOrigem = dr.GetInt32("CD_ORIGEM");
            vo.VlCompra = dr.GetDecimal("VL_COMPRA");
            vo.VlVenda = dr.GetDecimal("VL_VENDA");
            vo.IdStatus = dr.GetInt32("ID_STATUS");
            vo.Unidade = GetUnidade(vo.IdEmpresa, dr.GetInt32("ID_UNIDADE"));
            vo.Ncm = GetNcm(vo.IdEmpresa, dr.GetInt32("ID_NCM"));
            vo.Codigos = GetCodigos(vo.IdEmpresa, vo.Id);
        }

        private List<string> GetCodigos(int idEmpresa, int id)
        {
            string SQL = @"SELECT * FROM TBL_PRODUTO_CODIGO P WHERE
                            P.ID_EMPRESA = @ID_EMPRESA
                            AND P.ID_PRODUTO = @ID_PRODUTO";
            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "ID_PRODUTO", id, FbDbType.Integer);
            FbDataReader dr = cmd.ExecuteReader();
            List<string> listaCodigos = new List<string>();
            while (dr.Read())
            {
                listaCodigos.Add(dr.GetString("CD_PRODUTO"));
            }
            return listaCodigos;
        }

        private NcmVO GetNcm(int idEmpresa, int id)
        {
            string SQL = @"SELECT * FROM TBL_NCM P WHERE
                            P.ID_EMPRESA = @ID_EMPRESA
                            AND P.ID = @ID";
            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "ID", id, FbDbType.Integer);
            NcmVO vo = null;

            FbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                vo = new NcmVO()
                {
                    IdEmpresa = idEmpresa,
                    Id = id,
                    CdNcm = dr.GetString("CD_NCM"),
                    PcIpi = dr.GetDecimal("PC_IPI"),
                    PcAliquotaFederal = dr.GetDecimal("PC_ALIQUOTA_FEDERAL"),
                    PcAliquotaEstadual = dr.GetDecimal("PC_ALIQUOTA_ESTADUA"),
                    pcAliquotaMunicipal = dr.GetDecimal("PC_ALIQUOTA_MUNICIPAL")
                };    
            }
            return vo;
        }

        private UnidadeVO GetUnidade(int idEmpresa, int idUnidade)
        {
            string SQL = @"SELECT * FROM TBL_UNIDADE P WHERE
                            P.ID_EMPRESA = @ID_EMPRESA
                            AND P.ID = @ID";
            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_EMPRESA", idEmpresa, FbDbType.Integer);
            AddParam(cmd, "ID", idUnidade, FbDbType.Integer);
            FbDataReader dr = cmd.ExecuteReader();
            UnidadeVO vo = null;

            if (dr.Read())
            {
                vo = new UnidadeVO()
                {
                    IdEmpresa = dr.GetInt32("ID_EMPRESA"),
                    Id = dr.GetInt32("ID"),
                    SgUnidade = dr.GetString("SG_UNIDADE"),
                    DsUnidade = dr.GetString("DS_UNIDADE"),
                    VlFator = dr.GetDecimal("VL_FATOR"),
                    TpOperador = dr.GetString("TP_OPERADOR"),
                    IdUnidadePai = dr.GetInt32("ID_UNIDADE_PAI")
                };
            }
            return vo;
        }
    }
}
