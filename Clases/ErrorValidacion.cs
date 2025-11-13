using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP_FoodieEvents1.Clases
{
    public class ErrorValidacion : Exception
    {
        public ErrorValidacion(string mensaje) : base(mensaje) { }
    }
}
