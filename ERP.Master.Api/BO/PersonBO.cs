using ERP.Master.Api.DAO;
using ERP.Master.Api.Exceptions;
using ERP.Master.Api.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.BO
{
    public class PersonBO : BaseBO
    {
        private void Validate(PessoaVO vo)
        {
            ValidarEndereco(vo);
            ValidarTipoPessoal(vo);

            
        }

        private void ValidarTipoPessoal(PessoaVO vo)
        {
            if (vo.TpPessoa == Enums.TipoPessoa.JURIDICA && string.IsNullOrEmpty(vo.NuCpfCnpj))
                throw new BusinessException(BusinessException.ExceptionCode.CNPJ_OBRIGATORIO_PJ, "CNPJ não Informado");
        }

        private void ValidarEndereco(PessoaVO vo)
        {
            if(string.IsNullOrEmpty(vo.DsLogradouro)
                || string.IsNullOrEmpty(vo.NuLogradouro)
                || string.IsNullOrEmpty(vo.NuCep))
                throw new BusinessException(BusinessException.ExceptionCode.ENDERECO_INVALIDO, "Endereço inválido");
        }

        private PersonDAO dao { get; set; }
        private CidadeDAO cidadeDao { get; set; }

        public PersonBO(string connectionString)
        {
            this.dao = new PersonDAO(connectionString);
            this.cidadeDao = new CidadeDAO(dao.FBConection, dao.FBTransaction);
        }

        public List<List<PessoaVO>> ListAll(int idEmpresa)
        {
            if (idEmpresa < 1)
                throw new BusinessException(BusinessException.ExceptionCode.ID_EMPRESA_NAO_INFORMADO, "Código da empresa não foi setado.");
            List<List<PessoaVO>> lista = new List<List<PessoaVO>>();
            lista.Add(dao.ListUltimos10(idEmpresa, Enums.ClassePessoa.Cliente));
            lista.Add(dao.ListUltimos10(idEmpresa, Enums.ClassePessoa.Fornecedor));
            lista.Add(dao.ListUltimos10(idEmpresa, Enums.ClassePessoa.Vendedor));
            return lista;
        }

        public PessoaVO GetPessoa(int idEmpresa, int id)
        {
            if (idEmpresa < 1)
                throw new BusinessException(BusinessException.ExceptionCode.ID_EMPRESA_NAO_INFORMADO, "Código da empresa não foi setado.");

            PessoaVO vo = dao.GetPessoa(idEmpresa, id);

            if(vo == null)
            {
                vo = new PessoaVO();
                return vo;
            }

            CidadeVO cidade = cidadeDao.GetCidade(vo.IdCidade);
            if(cidade != null)
            {
                vo.NmCidade = cidade.NmCidade;
                vo.SgUf = cidade.SgUF;
            }
            return vo;
        }

        public void Save(PessoaVO vo)
        {
            //Validate(vo);
            dao.Save(vo);
            this.dao.FBTransaction.CommitRetaining();
        }

        public void Delete(int idEmpresa, int id)
        {
            dao.Delete(idEmpresa, id);
            this.dao.FBTransaction.CommitRetaining();
        }
    }
}
