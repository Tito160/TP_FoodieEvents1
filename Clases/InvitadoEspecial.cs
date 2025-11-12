using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public class InvitadoEspecial : Persona
    {
        public string TipoInvitado { get; private set; } // crítico, influencer, etc.
        public bool AccesoGratuito { get; private set; }

        public InvitadoEspecial(string nombre, string email, string telefono, string tipoInvitado, bool accesoGratuito)
            : base(nombre, email, telefono)
        {
            Validador.Texto(tipoInvitado, "Tipo de invitado");
            TipoInvitado = tipoInvitado.Trim();
            AccesoGratuito = accesoGratuito;
        }

        public override string ObtenerContacto() => $"{Nombre} ({TipoInvitado}) - {Email}";
    }
}
