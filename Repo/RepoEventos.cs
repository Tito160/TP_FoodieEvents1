using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repo
{
    public class RepoEventos
    {
        private readonly ConcurrentDictionary<Guid, Evento> _almacen = new();

        public void Agregar(Evento evento) => _almacen[evento.Id] = evento;
        public Evento Obtener(Guid id) => _almacen.TryGetValue(id, out var e) ? e : null;
        public IEnumerable<Evento> ObtenerTodos() => _almacen.Values.ToList();
        public void Eliminar(Guid id)
        {
            if (_almacen.TryRemove(id, out var evento))
            {
                evento.EliminarReservas();
            }
        }
    }
}
