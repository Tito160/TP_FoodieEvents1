using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;

public class Chef : Persona
{
    private string _especialidadCulinaria;
    private string _nacionalidad;
    private int _aniosExperiencia;
    private List<EventoGastronomico> _eventosOrganizados;

    public Chef(string nombreCompleto, string email, string telefono, 
            string especialidadCulinaria, string nacionalidad, int aniosExperiencia)
        : base(nombreCompleto, email, telefono)
    {
        _eventosOrganizados = new List<EventoGastronomico>();
        EspecialidadCulinaria = especialidadCulinaria;
        Nacionalidad = nacionalidad;
        AniosExperiencia = aniosExperiencia;
    }

    public string EspecialidadCulinaria 
    { 
        get => _especialidadCulinaria;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Especialidad culinaria");
            _especialidadCulinaria = value;
        }
    }

    public string Nacionalidad 
    { 
        get => _nacionalidad;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Nacionalidad");
            _nacionalidad = value;
        }
    }

    public int AniosExperiencia 
    { 
        get => _aniosExperiencia;
        private set
        {
            if (value < 0)
                throw new ErrorValidacionException("Los años de experiencia no pueden ser negativos.");
            _aniosExperiencia = value;
        }
    }

    public IReadOnlyList<EventoGastronomico> EventosOrganizados => _eventosOrganizados.AsReadOnly();

    public void AgregarEvento(EventoGastronomico evento)
    {
        if (evento != null && !_eventosOrganizados.Contains(evento))
            _eventosOrganizados.Add(evento);
    }

    public void RemoverEvento(EventoGastronomico evento)
    {
        _eventosOrganizados.Remove(evento);
    }

    public override string Presentarse()
    {
        return $"Soy {NombreCompleto}, chef especializado en {EspecialidadCulinaria} con {AniosExperiencia} años de experiencia.";
    }

    public override string ObtenerInformacionContacto()
    {
        return base.ObtenerInformacionContacto() + $" - Especialidad: {EspecialidadCulinaria}";
    }
}