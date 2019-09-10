using ERP.Master.Api.VO;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.DAO
{
    public class CidadeDAO : BaseDAO
    {
        public CidadeDAO(string connectionString) : base(connectionString) { }

        public CidadeDAO(FbConnection fbConn, FbTransaction fbTrans) : base(fbConn, fbTrans) { }
    
        public CidadeVO GetCidade(string idCidade)
        {
            string SQL = "SELECT * FROM TBL_CIDADE WHERE ID = @ID";

            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID", idCidade, FbDbType.VarChar, 9);
            FbDataReader dr = cmd.ExecuteReader();
            CidadeVO vo = null;
            if (dr.Read())
            {
                vo = new CidadeVO();
                MapperVO(dr, vo);
            }
            return vo;
        }

        public List<CidadeVO> GetCidades(string uf)
        {
            string SQL = "SELECT * FROM TBL_CIDADE WHERE SG_UF = @SG_UF ORDER BY NM_CIDADE";

            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "SG_UF", uf, FbDbType.VarChar, 2);
            FbDataReader dr = cmd.ExecuteReader();
            List<CidadeVO> lista = new List<CidadeVO>();
            while(dr.Read())
            {
                CidadeVO vo = new CidadeVO();
                MapperVO(dr, vo);
                lista.Add(vo);

            }
            return lista;
        }

        public List<CidadeVO> GetCidades(int idUf)
        {
            string SQL = "SELECT * FROM TBL_CIDADE WHERE ID_UF = @ID_UF ORDER BY NM_CIDADE";

            FbCommand cmd = GetCommand(SQL);
            AddParam(cmd, "ID_UF", idUf.ToString("00"), FbDbType.VarChar, 2);
            FbDataReader dr = cmd.ExecuteReader();
            List<CidadeVO> lista = new List<CidadeVO>();
            while (dr.Read())
            {
                CidadeVO vo = new CidadeVO();
                MapperVO(dr, vo);
                lista.Add(vo);

            }
            return lista;
        }

        public List<EstadoVO> GetEstados()
        {
            string SQL = @"SELECT DISTINCT
                            C.ID_UF AS ID,
                            C.SG_UF,
                            C.NM_ESTADO
                        FROM
                            TBL_CIDADE C
                        ORDER BY C.SG_UF";
            FbCommand cmd = GetCommand(SQL);
            FbDataReader dr = cmd.ExecuteReader();
            List<EstadoVO> lista = new List<EstadoVO>();
            while (dr.Read())
            {
                EstadoVO vo = new EstadoVO();
                MapperVO(dr, vo);
                lista.Add(vo);

            }
            return lista;
        }

        private void MapperVO(FbDataReader dr, CidadeVO vo)
        {
            vo.Id = dr.GetString("ID");
            vo.IduF = dr.GetString("ID_UF");
            vo.SgUF = dr.GetString("SG_UF");
            vo.NmCidade = dr.GetString("NM_CIDADE").Capitalize();
        }

        private void MapperVO(FbDataReader dr, EstadoVO vo)
        {
            vo.Id = dr.GetString("ID");
            vo.SgUF = dr.GetString("SG_UF");
            vo.NmEstado = dr.GetString("NM_ESTADO").Capitalize();
        }
    }
}
