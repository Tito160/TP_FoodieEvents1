using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EventoVirtual : EventoGastronomico
{
    private string _plataforma;
    private string _enlace;

    public EventoVirtual(string nombre, string descripcion, TipoEvento tipoEvento, 
                        DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima, 
                        decimal precioPorEntrada, Chef chef, string plataforma, string enlace)
        : base(nombre, descripcion, tipoEvento, fechaInicio, fechaFin, capacidadMaxima, precioPorEntrada, chef)
    {
        Plataforma = plataforma;
        Enlace = enlace;
    }

    public string Plataforma 
    { 
        get => _plataforma;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Plataforma");
            _plataforma = value;
        }
    }

    public string Enlace { get => _enlace; private set => _enlace = value ?? ""; }

    public override string ObtenerInformacionUbicacion()
    {
        return $"🌐 Evento virtual en: {Plataforma}. Enlace: {Enlace}";
    }

    public override bool ValidarDisponibilidad()
    {
        return TieneLugaresDisponibles && FechaInicio > DateTime.Now;
    }
}