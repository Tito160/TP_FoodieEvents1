using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InvitadoEspecial : Persona
{
    private string _tipoInvitado;
    private string _organizacion;

    public InvitadoEspecial(string nombreCompleto, string email, string telefono, 
                        string tipoInvitado, string organizacion)
        : base(nombreCompleto, email, telefono)
    {
        TipoInvitado = tipoInvitado;
        Organizacion = organizacion;
    }

    public string TipoInvitado 
    { 
        get => _tipoInvitado;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Tipo de invitado");
            _tipoInvitado = value;
        }
    }

    public string Organizacion 
    { 
        get => _organizacion;
        private set => _organizacion = value ?? "";
    }

    public bool TieneAccesoGratuito => true;

    public override string Presentarse()
    {
        return $"Soy {NombreCompleto}, {TipoInvitado} de {Organizacion}. Acceso: Gratuito";
    }

    public override string ObtenerInformacionContacto()
    {
        return base.ObtenerInformacionContacto() + $" - Tipo: {TipoInvitado}";
    }
}