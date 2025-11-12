using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interfaz
{
    public interface IPersona
    {
        Guid Id { get; }
        string Nombre { get; }
        string Email { get; }
        string Telefono { get; }
        string ObtenerContacto();
    }
}
