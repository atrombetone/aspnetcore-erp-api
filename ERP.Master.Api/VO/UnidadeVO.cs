namespace ERP.Master.Api.VO
{
    public class UnidadeVO
    {
        public int IdEmpresa { get; set; }
        public int Id { get; set; }
        public string SgUnidade { get; set; }
        public string DsUnidade { get; set; }
        public decimal VlFator { get; set; }
        public string TpOperador { get; set; }
        public int IdUnidadePai  { get; set; }
    }
}