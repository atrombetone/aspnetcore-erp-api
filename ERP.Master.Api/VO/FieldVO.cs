using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Master.Api.VO
{
    public class FieldVO
    {
        public string Name { get; set; }

        public FbDbType Type { get; set; }

        public int Length { get; set; }

        public bool IsFieldExist { get; set; }

        public object Value { get; set; }

        public FieldVO() { }

        public FieldVO(string name, object value, FbDbType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Length = 0;
            this.IsFieldExist = false;
        }

        public FieldVO(string name, object value, FbDbType type, int length)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Length = length;
            this.IsFieldExist = false;
        }

        public FieldVO(string name, object valor, FbDbType type, int lenght, bool isFieldExist)
        {
            this.Name = name;
            this.Value = valor;
            this.Type = type;
            this.Length = lenght;
            this.IsFieldExist = isFieldExist;
        }
    }
}
