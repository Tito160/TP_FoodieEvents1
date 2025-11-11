using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class Persona : IPersona
{
    private string _nombreCompleto;
    private string _email;
    private string _telefono;

    protected Persona(string nombreCompleto, string email, string telefono)
    {
        NombreCompleto = nombreCompleto;
        Email = email;
        Telefono = telefono;
    }

    public string NombreCompleto 
    { 
        get => _nombreCompleto;
        protected set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Nombre completo");
            _nombreCompleto = value;
        }
    }

    public string Email 
    { 
        get => _email;
        protected set
        {
            ValidadorDatos.ValidarEmail(value);
            _email = value;
        }
    }

    public string Telefono 
    { 
        get => _telefono;
        protected set
        {
            ValidadorDatos.ValidarTelefono(value);
            _telefono = value;
        }
    }

    public virtual string ObtenerInformacionContacto()
    {
        return $"Contacto: {NombreCompleto} - {Email} - {Telefono}";
    }

    public abstract string Presentarse();
}