﻿using System;
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
    public class EstadosController : BaseController
    {
        private CidadeBO bo;

        public EstadosController(IConfiguration cfg) : base(cfg)
        {
            bo = new CidadeBO(this.ConnectionString());
        }

        [HttpGet]
        public List<EstadoVO> List()
        {
            return bo.GetEstados();
        }
    }
}
