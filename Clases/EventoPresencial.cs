using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_FoodieEvents1;
using TP_FoodieEvents1.Clases;

namespace Clases;

public class EventoPresencial : Evento
{
    public string Ubicacion { get; private set; }

    public EventoPresencial(string nombre, string descripcion, TipoEvento tipo, DateTime inicio, DateTime fin, int capacidad, decimal precio, Chef organizador, string ubicacion)
        : base(nombre, descripcion, tipo, inicio, fin, capacidad, precio, organizador)
    {
        Validador.Texto(ubicacion, "Ubicación");
        Ubicacion = ubicacion.Trim();
    }
}
