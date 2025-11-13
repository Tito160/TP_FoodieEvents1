using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_FoodieEvents1.Clases;

namespace Clases;

public class Chef : Persona
{
    public string Especialidad { get; private set; }
    public string Nacionalidad { get; private set; }
    public int AnosExperiencia { get; private set; }

    public Chef(string nombre, string email, string telefono, string especialidad, string nacionalidad, int anos)
        : base(nombre, email, telefono)
    {
        Validador.Texto(especialidad, "Especialidad");
        Validador.Texto(nacionalidad, "Nacionalidad");
        Validador.NumeroNoNegativo(anos, "Años de experiencia");

        Especialidad = especialidad.Trim();
        Nacionalidad = nacionalidad.Trim();
        AnosExperiencia = anos;
    }

    public string MostrarTrayectoria() => $"{Nombre} - {Especialidad} ({AnosExperiencia} años)";
}
