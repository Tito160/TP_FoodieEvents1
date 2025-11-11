using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class EventoGastronomico
{
    private string _nombre;
    private string _descripcion;
    private TipoEvento _tipoEvento;
    private DateTime _fechaInicio;
    private DateTime _fechaFin;
    private int _capacidadMaxima;
    private decimal _precioPorEntrada;
    private Chef _chef;
    private List<Reserva> _reservas;

    protected EventoGastronomico(string nombre, string descripcion, TipoEvento tipoEvento, 
                            DateTime fechaInicio, DateTime fechaFin, int capacidadMaxima, 
                            decimal precioPorEntrada, Chef chef)
    {
        _reservas = new List<Reserva>();
        Nombre = nombre;
        Descripcion = descripcion;
        TipoEvento = tipoEvento;
        FechaInicio = fechaInicio;
        FechaFin = fechaFin;
        CapacidadMaxima = capacidadMaxima;
        PrecioPorEntrada = precioPorEntrada;
        Chef = chef;
    }

    public string Nombre 
    { 
        get => _nombre;
        protected set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Nombre del evento");
            _nombre = value;
        }
    }

    public string Descripcion 
    { 
        get => _descripcion;
        protected set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Descripción");
            _descripcion = value;
        }
    }

    public TipoEvento TipoEvento { get => _tipoEvento; protected set => _tipoEvento = value; }
    
    public DateTime FechaInicio 
    { 
        get => _fechaInicio; 
        protected set 
        { 
            ValidadorDatos.ValidarFechasEvento(value, _fechaFin); 
            _fechaInicio = value; 
        } 
    }
    
    public DateTime FechaFin 
    { 
        get => _fechaFin; 
        protected set 
        { 
            ValidadorDatos.ValidarFechasEvento(_fechaInicio, value); 
            _fechaFin = value; 
        } 
    }

    public int CapacidadMaxima 
    { 
        get => _capacidadMaxima; 
        protected set 
        { 
            ValidadorDatos.ValidarCapacidad(value); 
            _capacidadMaxima = value; 
        } 
    }

    public decimal PrecioPorEntrada 
    { 
        get => _precioPorEntrada; 
        protected set 
        { 
            ValidadorDatos.ValidarPrecio(value); 
            _precioPorEntrada = value; 
        } 
    }

    public Chef Chef 
    { 
        get => _chef;
        protected set
        {
            if (value == null) 
                throw new ErrorValidacionException("El evento debe tener un chef asignado.");
            
            _chef = value;
            _chef.AgregarEvento(this);
        }
    }

    public int LugaresDisponibles => _capacidadMaxima - _reservas.Count;
    public bool TieneLugaresDisponibles => LugaresDisponibles > 0;
    public IReadOnlyList<Reserva> Reservas => _reservas.AsReadOnly();

    public abstract string ObtenerInformacionUbicacion();
    public abstract bool ValidarDisponibilidad();

    public void AgregarReserva(Reserva reserva)
    {
        if (!ValidarDisponibilidad())
            throw new ErrorValidacionException("No hay disponibilidad para este evento.");
        _reservas.Add(reserva);
    }

    public void RemoverReserva(Reserva reserva)
    {
        _reservas.Remove(reserva);
    }
}