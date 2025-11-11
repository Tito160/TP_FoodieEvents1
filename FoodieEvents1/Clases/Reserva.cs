using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Reserva
{
    private IPersona _persona;
    private EventoGastronomico _evento;
    private DateTime _fechaReserva;
    private bool _estaPagado;
    private MetodoPago _metodoPago;
    private EstadoReserva _estado;

    public Reserva(IPersona persona, EventoGastronomico evento, MetodoPago metodoPago)
    {
        if (!evento.ValidarDisponibilidad())
            throw new ErrorValidacionException("No hay disponibilidad para este evento.");

        Persona = persona;
        Evento = evento;
        FechaReserva = DateTime.Now;
        MetodoPago = metodoPago;
        _estado = EstadoReserva.Confirmada;
        _estaPagado = persona is InvitadoEspecial;

        evento.AgregarReserva(this);
    }

    public IPersona Persona 
    { 
        get => _persona;
        private set
        {
            if (value == null) 
                throw new ErrorValidacionException("La reserva debe tener una persona.");
            _persona = value;
        }
    }

    public EventoGastronomico Evento 
    { 
        get => _evento;
        private set
        {
            if (value == null) 
                throw new ErrorValidacionException("La reserva debe tener un evento.");
            _evento = value;
        }
    }

    public DateTime FechaReserva { get => _fechaReserva; private set => _fechaReserva = value; }
    public bool EstaPagado { get => _estaPagado; private set => _estaPagado = value; }
    public MetodoPago MetodoPago { get => _metodoPago; set => _metodoPago = value; }
    public EstadoReserva Estado { get => _estado; set => _estado = value; }

    public void Cancelar()
    {
        _estado = EstadoReserva.Cancelada;
        Evento.RemoverReserva(this);
    }

    public void MarcarComoPagado()
    {
        if (Persona is InvitadoEspecial)
            throw new ErrorValidacionException("Los invitados especiales no requieren pago.");
        
        EstaPagado = true;
    }
}