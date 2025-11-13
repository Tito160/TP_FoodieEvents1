using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_FoodieEvents1;
using TP_FoodieEvents1.Clases;

namespace Clases;

public class EventoVirtual : Evento
{
    public string EnlaceStreaming { get; private set; }

    public EventoVirtual(string nombre, string descripcion, TipoEvento tipo, DateTime inicio, DateTime fin, int capacidad, decimal precio, Chef organizador, string enlace)
        : base(nombre, descripcion, tipo, inicio, fin, capacidad, precio, organizador)
    {
        Validador.Texto(enlace, "Enlace streaming");
        EnlaceStreaming = enlace.Trim();
    }
}
