using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Servicios
{
    public List<EventoGastronomico> Eventos { get; } = new List<EventoGastronomico>();
    public List<IPersona> Personas { get; } = new List<IPersona>();
    public List<Reserva> Reservas { get; } = new List<Reserva>();

    public void AgregarPersona(IPersona persona) => Personas.Add(persona);
    public void AgregarEvento(EventoGastronomico evento) => Eventos.Add(evento);
    
    public Reserva CrearReserva(IPersona persona, EventoGastronomico evento, MetodoPago metodoPago)
    {
        var reserva = new Reserva(persona, evento, metodoPago);
        Reservas.Add(reserva);
        return reserva;
    }

    public void EliminarEvento(EventoGastronomico evento)
    {
        // COMPOSICIÓN: Eliminar reservas asociadas
        var reservasAEliminar = Reservas.Where(r => r.Evento == evento).ToList();
        foreach (var reserva in reservasAEliminar)
        {
            Reservas.Remove(reserva);
        }
        
        // COMPOSICIÓN: Remover evento del chef
        evento.Chef.RemoverEvento(evento);
        
        Eventos.Remove(evento);
    }

    public List<EventoGastronomico> ObtenerEventosDisponibles() => 
        Eventos.Where(e => e.ValidarDisponibilidad()).ToList();

    public List<Chef> ObtenerChefs() => Personas.OfType<Chef>().ToList();
    public List<Participante> ObtenerParticipantes() => Personas.OfType<Participante>().ToList();
    public List<InvitadoEspecial> ObtenerInvitadosEspeciales() => Personas.OfType<InvitadoEspecial>().ToList();
}