using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Master.Api.BO;
using ERP.Master.Api.VO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ERP.Master.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : BaseController
    {
        private CidadeBO bo;

        public CidadesController(IConfiguration cfg) : base(cfg)
        {
            bo = new CidadeBO(this.ConnectionString());
        }

        // GET: api/Cidade
        [HttpGet("{id}")]
        public List<CidadeVO> List(string id)
        {
            return bo.GetCidades(id);
        }
    }
}
