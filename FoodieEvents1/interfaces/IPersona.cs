using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IPersona
{
    string NombreCompleto { get; }
    string Email { get; }
    string Telefono { get; }
    string ObtenerInformacionContacto();
    string Presentarse();
}