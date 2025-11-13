using System;

namespace TP_FoodieEvents1.Interfaz;

public interface IPersona
{
    Guid Id { get; }
    string Nombre { get; }
    string Email { get; }
    string Telefono { get; }
    string ObtenerContacto();
}
