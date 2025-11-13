using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo;

public class RepoReservas
{
    private readonly ConcurrentDictionary<Guid, RepoReservas> _almacen = new();

    public void Agregar(RepoReservas reserva) => _almacen[reserva.Id] = reserva;
    public RepoReservas Obtener(Guid id) => _almacen.TryGetValue(id, out var r) ? r : null;
    public IEnumerable<RepoReservas> ObtenerTodos() => _almacen.Values.ToList();
    public void Eliminar(Guid id) => _almacen.TryRemove(id, out _);
}
