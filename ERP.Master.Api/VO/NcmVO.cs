namespace ERP.Master.Api.VO
{
    public class NcmVO
    {
        public int IdEmpresa { get; set; }
        public int Id { get; set; }
        public string CdNcm { get; set; }
        public decimal PcIpi { get; set; }
        public decimal PcAliquotaFederal { get; set; }
        public decimal PcAliquotaEstadual { get; set; }
        public decimal pcAliquotaMunicipal { get; set; }
    }
}