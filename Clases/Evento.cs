using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public abstract class Evento
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Nombre { get; protected set; }
        public string Descripcion { get; protected set; }
        public TipoEvento Tipo { get; protected set; }          // tipo de evento
        public DateTime FechaInicio { get; protected set; }
        public DateTime FechaFin { get; protected set; }
        public int Capacidad { get; protected set; }
        public decimal Precio { get; protected set; }
        public Chef Organizador { get; protected set; }

        // encapsular reservas internamente
        private readonly List<Reserva> _reservas = new();
        public IReadOnlyCollection<Reserva> Reservas => _reservas.AsReadOnly();

        protected Evento(string nombre, string descripcion, TipoEvento tipo, DateTime inicio, DateTime fin, int capacidad, decimal precio, Chef organizador)
        {
            Validador.Texto(nombre, "Nombre del evento");
            Validador.Texto(descripcion, "Descripción");
            Validador.FechasEvento(inicio, fin);
            Validador.NumeroPositivo(capacidad, "Capacidad");
            Validador.NumeroNoNegativo(precio, "Precio");
            Validador.NoNulo(organizador, "Chef organizador");

            Nombre = nombre.Trim();
            Descripcion = descripcion.Trim();
            Tipo = tipo;
            FechaInicio = inicio;
            FechaFin = fin;
            Capacidad = capacidad;
            Precio = precio;
            Organizador = organizador;
        }

        // agregar reserva: único punto de entrada para insertar reservas
        public virtual void AgregarReserva(Reserva reserva)
        {
            Validador.NoNulo(reserva, "Reserva");
            var confirmadas = _reservas.Count(r => r.Estado == EstadoReserva.Confirmada);
            if (reserva.Estado == EstadoReserva.Confirmada && confirmadas >= Capacidad)
                throw new ErrorValidacion("No hay cupos disponibles.");
            _reservas.Add(reserva);
        }

        public virtual void EliminarReservas()
        {
            _reservas.Clear();
        }
    }
}
