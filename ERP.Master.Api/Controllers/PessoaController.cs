using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class PessoaController : BaseController
    {
        private PersonBO bo { get; set; }

        public PessoaController(IConfiguration cfg) : base(cfg)
        {
            bo = new PersonBO(this.ConnectionString());
        }

        [HttpGet]
        public List<List<PessoaVO>> Get()
        {
            return bo.ListAll(this.ID_EMPRESA);
        }

        [HttpGet("{id}")]
        public PessoaVO Get(int id)
        {
            return bo.GetPessoa(this.ID_EMPRESA, id);
        }

        [HttpPost]
        public void Post([FromBody] PessoaVO vo)
        {
            vo.IdEmpresa = this.ID_EMPRESA;
            bo.Save(vo);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PessoaVO vo)
        {
            vo.IdEmpresa = this.ID_EMPRESA;
            vo.Id = id;
            bo.Save(vo);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            bo.Delete(this.ID_EMPRESA, id);
        }
    }
}
