using System;

namespace SistemaGestionPacientes
{
    public class Paciente
    {
        public string Id { get; set; }
        public string NombreCompleto { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Diagnostico { get; set; }
        public DateTime FechaIngreso { get; set; }

        public Paciente(string id, string nombreCompleto, int edad, string sexo, string diagnostico, DateTime fechaIngreso)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Edad = edad;
            Sexo = sexo;
            Diagnostico = diagnostico;
            FechaIngreso = fechaIngreso;
        }
        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {NombreCompleto} | Edad: {Edad} | Sexo: {Sexo} | " +
                   $"Diagnóstico: {Diagnostico} | Fecha de ingreso: {FechaIngreso:dd/MM/yyyy}";
        }
    }
}
