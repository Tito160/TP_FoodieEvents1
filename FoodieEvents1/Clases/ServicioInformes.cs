using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ServicioInformes
{
    private List<EventoGastronomico> _eventos;
    private List<IPersona> _personas;
    private List<Reserva> _reservas;

    public ServicioInformes(List<EventoGastronomico> eventos, List<IPersona> personas, List<Reserva> reservas)
    {
        _eventos = eventos;
        _personas = personas;
        _reservas = reservas;
    }

    public string GenerarInformeEventosPopulares()
    {
        var eventos = _eventos
            .OrderByDescending(e => e.Reservas.Count)
            .Take(5);

        var informe = new StringBuilder();
        informe.AppendLine("=== EVENTOS MÁS POPULARES ===");
        
        int posicion = 1;
        foreach (var evento in eventos)
        {
            informe.AppendLine($"{posicion}. {evento.Nombre}");
            informe.AppendLine($"   Reservas: {evento.Reservas.Count}/{evento.CapacidadMaxima}");
            informe.AppendLine($"   Chef: {evento.Chef.NombreCompleto}");
            informe.AppendLine($"   Tipo: {evento.TipoEvento}");
            informe.AppendLine();
            posicion++;
        }

        return informe.ToString();
    }

    public string GenerarInformeChefsExperimentados()
    {
        var chefs = _personas.OfType<Chef>()
            .OrderByDescending(c => c.AniosExperiencia)
            .Take(5);

        var informe = new StringBuilder();
        informe.AppendLine("=== CHEFS CON MÁS EXPERIENCIA ===");
        
        foreach (var chef in chefs)
        {
            informe.AppendLine($"• {chef.NombreCompleto}");
            informe.AppendLine($"  Experiencia: {chef.AniosExperiencia} años");
            informe.AppendLine($"  Especialidad: {chef.EspecialidadCulinaria}");
            informe.AppendLine($"  Eventos organizados: {chef.EventosOrganizados.Count}");
            informe.AppendLine();
        }

        return informe.ToString();
    }

    public string GenerarInformeParticipantesActivos()
    {
        var participantes = _personas.OfType<Participante>()
            .Select(p => new {
                Participante = p,
                ReservasCount = _reservas.Count(r => r.Persona == p)
            })
            .OrderByDescending(x => x.ReservasCount)
            .Take(5);

        var informe = new StringBuilder();
        informe.AppendLine("=== PARTICIPANTES MÁS ACTIVOS ===");
        
        int posicion = 1;
        foreach (var item in participantes)
        {
            informe.AppendLine($"{posicion}. {item.Participante.NombreCompleto}");
            informe.AppendLine($"   Email: {item.Participante.Email}");
            informe.AppendLine($"   Reservas: {item.ReservasCount}");
            informe.AppendLine($"   Restricciones: {item.Participante.RestriccionesAlimentarias}");
            informe.AppendLine();
            posicion++;
        }

        return informe.ToString();
    }
}