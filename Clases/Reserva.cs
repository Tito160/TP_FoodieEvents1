using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public class Reserva
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public IPersona Asistente { get; private set; } // acepta cualquier IPersona
        public DateTime FechaReserva { get; private set; }
        public MetodoPago MetodoPago { get; private set; }
        public EstadoReserva Estado { get; private set; }

        public Reserva(IPersona asistente, DateTime fechaReserva, MetodoPago metodo, bool confirmarPago)
        {
            Validador.NoNulo(asistente, "Asistente");
            Validador.FechaReserva(fechaReserva);

            Asistente = asistente;
            FechaReserva = fechaReserva;
            MetodoPago = metodo;
            Estado = confirmarPago ? EstadoReserva.Confirmada : EstadoReserva.Pendiente;
        }

        public void ConfirmarPago()
        {
            if (Estado == EstadoReserva.Cancelada)
                throw new ErrorValidacion("No se puede confirmar una reserva cancelada.");
            Estado = EstadoReserva.Confirmada;
        }

        public void Cancelar()
        {
            Estado = EstadoReserva.Cancelada;
        }
    }
}
