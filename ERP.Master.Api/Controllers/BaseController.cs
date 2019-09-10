using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ERP.Master.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IConfiguration _cfg { get; set; }

        protected int ID_EMPRESA { get { return 1; } }

        public BaseController(IConfiguration cfg)
        {
            this._cfg = cfg;
        }

        protected string ConnectionString()
        {
            return this._cfg["ConnectionStrings:DefaultConnection"];
        }
    }
}