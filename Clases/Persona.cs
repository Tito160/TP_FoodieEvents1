using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clases
{
    public abstract class Persona : IPersona
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public string Nombre { get; protected set; }
        public string Email { get; protected set; }
        public string Telefono { get; protected set; }

        protected Persona(string nombre, string email, string telefono)
        {
            Validador.Texto(nombre, "Nombre");
            Validador.Email(email);
            Validador.Telefono(telefono);

            Nombre = nombre.Trim();
            Email = email.Trim();
            Telefono = telefono.Trim();
        }

        public virtual string ObtenerContacto() => $"{Nombre} <{Email}> - {Telefono}";
    }
}
