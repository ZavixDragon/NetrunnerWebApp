using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects
{
    public class StringObject
    {
        public string Value { get; set; }

        public StringObject(string value)
        {
            Value = value;
        }
    }
}
