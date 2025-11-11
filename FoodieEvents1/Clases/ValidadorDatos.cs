using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class ValidadorDatos
{
    public static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ErrorValidacionException("El correo electrónico no puede estar vacío.");

        if (!email.Contains("@") || !email.Contains("."))
            throw new ErrorValidacionException("El formato del correo electrónico no es válido.");
    }

    public static void ValidarTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new ErrorValidacionException("El teléfono no puede estar vacío.");

        if (!long.TryParse(telefono, out _))
            throw new ErrorValidacionException("El teléfono debe contener solo números.");
    }

    public static void ValidarFechasEvento(DateTime fechaInicio, DateTime fechaFin)
    {
        if (fechaInicio >= fechaFin)
            throw new ErrorValidacionException("La fecha de inicio debe ser anterior a la fecha de fin.");
    }

    public static void ValidarCapacidad(int capacidad)
    {
        if (capacidad <= 0)
            throw new ErrorValidacionException("La capacidad debe ser mayor a cero.");
    }

    public static void ValidarPrecio(decimal precio)
    {
        if (precio < 0)
            throw new ErrorValidacionException("El precio no puede ser negativo.");
    }

    public static void ValidarTextoRequerido(string valor, string nombreCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ErrorValidacionException($"{nombreCampo} no puede estar vacío.");
    }
}