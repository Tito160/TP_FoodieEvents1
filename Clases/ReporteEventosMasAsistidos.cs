using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enum;
using Interfaz;
using Repo;

namespace Clases;

public class ReporteEventosMasAsistidos : IReporte<(Guid EventoId, string NombreEvento, int Asistentes)>
{
    private readonly RepoEventos _repoEventos;

    public ReporteEventosMasAsistidos(RepoEventos repoEventos)
    {
        _repoEventos = repoEventos;
    }

    public IEnumerable<(Guid EventoId, string NombreEvento, int Asistentes)> Generar()
    {
        return _repoEventos.ObtenerTodos()
            .Select(e => (e.Id, e.Nombre, e.Reservas.Count(r => r.Estado == EstadoReserva.Confirmada)))
            .OrderByDescending(t => t.Item3)
            .ToList();
    }
}
