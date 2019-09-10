using ERP.Master.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.VO
{
    public class PessoaVO
    {
        public int IdEmpresa { get; set; }
        public int Id { get; set; }
        public string NmRazaoSocial { get; set; }
        public string NmFantasia { get; set; }
        public string NuCep { get; set; }
        public string DsLogradouro { get; set; }
        public string NuLogradouro { get; set; }
        public string DsComplemento { get; set; }
        public string NmBairro { get; set; }
        public string IdCidade { get; set; }
        public string NmCidade { get; set; }
        public string SgUf { get; set; }
        public string NuFone { get; set; }
        public string NuCpfCnpj { get; set; }
        public string NuRgIe { get; set; }
        public int IdStatus { get; set; }
        public DateTime DtInclusao { get; set; }
        public string DsEMail { get; set; }
        public string DsHome { get; set; }
        public string DsObservacao { get; set; }
        public List<string> ClPessoa { get; set; }
        public TipoContribuinte TpContribuinte { get; set; }
        public TipoPessoa TpPessoa { get; set; }
    }
}
