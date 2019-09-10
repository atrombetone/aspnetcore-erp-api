using ERP.Master.Api.DAO;
using ERP.Master.Api.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.BO
{
    public class CidadeBO : BaseBO
    {
        private CidadeDAO dao { get; set; }

        public CidadeBO(string connectionString)
        {
            this.dao = new CidadeDAO(connectionString);
        }

        public CidadeVO GetCidade(string idCidade)
        {
            return dao.GetCidade(idCidade);
        }

        public List<CidadeVO> GetCidades(string uf)
        {
            if(Int32.MinValue.TryParse(uf, 0) > 0)
                return dao.GetCidades(Int32.MinValue.TryParse(uf, 0));
            else
                return dao.GetCidades(uf.ToUpper());
        }

        public List<EstadoVO> GetEstados()
        {
            return dao.GetEstados();
        }
    }
}
