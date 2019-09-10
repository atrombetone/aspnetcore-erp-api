using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ERP.Master.Api.Attributes
{
    /// <summary>
    /// Classe que implementa um LivreAttribute (Annotation) para propriedades de uma classe.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumValueAttribute : System.Attribute
    {
        /// <summary>
        /// Indentifica um campo e seu tipo.
        /// </summary>
        /// <param name="value">Tipo do campo (entrada, saída ou ambos)</param>
        public EnumValueAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Valor do Enum.
        /// </summary>
        public string Value { get; set; }
    }
}
