using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_FoodieEvents1.Interfaz;

namespace Repo;

public class RepoPersonas
{
    private readonly ConcurrentDictionary<Guid, IPersona> _almacen = new();

    public void Agregar(IPersona persona) => _almacen[persona.Id] = persona;
    public IPersona Obtener(Guid id) => _almacen.TryGetValue(id, out var p) ? p : null;
    public IEnumerable<IPersona> ObtenerTodos() => _almacen.Values.ToList();
    public IPersona BuscarPorEmail(string email) => _almacen.Values.FirstOrDefault(p => p.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
}
