using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public static class Validador
    {
        public static void Texto(string valor, string campo)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ErrorValidacion($"{campo} no puede estar vacío.");
        }

        public static void Email(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo) || !Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ErrorValidacion("Correo electrónico no válido.");
        }

        public static void Telefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono, @"^[0-9]{7,15}$"))
                throw new ErrorValidacion("Teléfono no válido. Debe contener 7 a 15 dígitos.");
        }

        public static void FechasEvento(DateTime inicio, DateTime fin)
        {
            if (inicio == default || fin == default) throw new ErrorValidacion("Fechas inválidas.");
            if (inicio >= fin) throw new ErrorValidacion("La fecha de inicio debe ser anterior a la fecha de fin.");
        }

        public static void NumeroPositivo(int valor, string campo)
        {
            if (valor <= 0) throw new ErrorValidacion($"{campo} debe ser mayor que cero.");
        }

        public static void NumeroNoNegativo(decimal valor, string campo)
        {
            if (valor < 0) throw new ErrorValidacion($"{campo} no puede ser negativo.");
        }

        public static void NumeroNoNegativo(int valor, string campo)
        {
            if (valor < 0) throw new ErrorValidacion($"{campo} no puede ser negativo.");
        }

        public static void NoNulo(object obj, string campo)
        {
            if (obj == null) throw new ErrorValidacion($"{campo} no puede ser nulo.");
        }

        public static void FechaReserva(DateTime fecha)
        {
            if (fecha == default) throw new ErrorValidacion("Fecha de reserva inválida.");
            if (fecha < DateTime.Today) throw new ErrorValidacion("La fecha de reserva no puede ser anterior a hoy.");
        }
    }
}
