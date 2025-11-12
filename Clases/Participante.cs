using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public class Participante : Persona
    {
        public string Documento { get; private set; }
        public string Restricciones { get; private set; }

        public Participante(string nombre, string email, string telefono, string documento, string restricciones)
            : base(nombre, email, telefono)
        {
            Validador.Texto(documento, "Documento");
            Documento = documento.Trim();
            Restricciones = restricciones?.Trim() ?? string.Empty;
        }
    }
}
