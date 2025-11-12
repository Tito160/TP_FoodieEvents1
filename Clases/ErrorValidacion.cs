using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public class ErrorValidacion : Exception
    {
        public ErrorValidacion(string mensaje) : base(mensaje) { }
    }
}
