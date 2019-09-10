using ERP.Master.Api.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.Exceptions
{
    public class BusinessException : Exception
    {
        public enum ExceptionCode
        {
            [EnumValue("Código da Empresa não informado")]
            ID_EMPRESA_NAO_INFORMADO = 0,
            [EnumValue("O Endereço não é válido")]
            ENDERECO_INVALIDO = 1,
            [EnumValue("CNPJ Obrigatório para pessoa Juridica")]
            CNPJ_OBRIGATORIO_PJ = 2
        }

        private ExceptionCode Code
        {
            get
            {
                return this._code;
            }
        }
        private ExceptionCode _code;

        public BusinessException(ExceptionCode code, string message): base(message)
        {
            this._code = code;
        }
    }
}
