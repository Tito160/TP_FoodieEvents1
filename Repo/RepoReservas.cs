using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo
{
    public class RepoReservas
    {
        private readonly ConcurrentDictionary<Guid, Reserva> _almacen = new();

        public void Agregar(Reserva reserva) => _almacen[reserva.Id] = reserva;
        public Reserva Obtener(Guid id) => _almacen.TryGetValue(id, out var r) ? r : null;
        public IEnumerable<Reserva> ObtenerTodos() => _almacen.Values.ToList();
        public void Eliminar(Guid id) => _almacen.TryRemove(id, out _);
    }
}
