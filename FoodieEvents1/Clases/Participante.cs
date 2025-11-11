using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Participante : Persona
{
    private string _documentoIdentidad;
    private string _restriccionesAlimentarias;

    public Participante(string nombreCompleto, string email, string telefono, 
                    string documentoIdentidad, string restriccionesAlimentarias)
        : base(nombreCompleto, email, telefono)
    {
        DocumentoIdentidad = documentoIdentidad;
        RestriccionesAlimentarias = restriccionesAlimentarias;
    }

    public string DocumentoIdentidad 
    { 
        get => _documentoIdentidad;
        private set
        {
            ValidadorDatos.ValidarTextoRequerido(value, "Documento de identidad");
            _documentoIdentidad = value;
        }
    }

    public string RestriccionesAlimentarias 
    { 
        get => _restriccionesAlimentarias;
        private set => _restriccionesAlimentarias = value ?? "";
    }

    public override string Presentarse()
    {
        string presentacion = $"Soy {NombreCompleto}, participante con documento {DocumentoIdentidad}";
        if (!string.IsNullOrEmpty(RestriccionesAlimentarias))
            presentacion += $". Restricciones: {RestriccionesAlimentarias}";
        return presentacion;
    }
}