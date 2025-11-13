using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IReporte<T>
    {
        IEnumerable<T> Generar();
    }
}
