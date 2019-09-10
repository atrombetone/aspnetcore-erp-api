using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.VO
{
    public class ProdutoVO
    {
        public int IdEmpresa { get; set; }
        public int Id { get; set; }
        public string NmProduto { get; set; }
        public int CdOrigem { get; set; }
        public decimal VlCompra { get; set; }
        public decimal VlVenda { get; set; }
        public UnidadeVO Unidade  { get; set; }
        public NcmVO Ncm { get; set; }
        public int IdStatus { get; set; }
        public List<ProdutoVO> LsInsumos { get; set; }
        public List<string> Codigos { get; set; }
    }
}
