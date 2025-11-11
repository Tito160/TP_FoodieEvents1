using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventoPresencial : EventoGastronomico
{
    private string _ubicacion;

    public EventoPresencial(string nombre, string descripcion, TipoEvento tipoEvento, 
                        DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima, 
                        decimal precioPorEntrada, Chef chef, string ubicacion)
        : base(nombre, descripcion, tipoEvento, fechaInicio, fechaFin, capacidadMaxima, precioPorEntrada, chef)
    {
        Ubicacion = ubicacion;
    }

    public string Ubicacion 
    { 
        get => _ubicacion;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Ubicación");
            _ubicacion = value;
        }
    }

    public override string ObtenerInformacionUbicacion()
    {
        return $"📍 Evento presencial en: {Ubicacion}";
    }

    public override bool ValidarDisponibilidad()
    {
        return TieneLugaresDisponibles && FechaInicio > DateTime.Now;
    }
}