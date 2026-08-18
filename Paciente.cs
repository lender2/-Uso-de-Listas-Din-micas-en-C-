using System;

namespace SistemaGestionPacientes
{
    /// <summary>
    /// Clase modelo que representa a un paciente del centro de salud.
    /// Solo contiene los datos del paciente (sin lógica de negocio);
    /// la lógica CRUD vive en GestorPacientes.
    /// </summary>
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

        /// <summary>
        /// Representación legible del paciente, usada al listar
        /// y al mostrar resultados de búsqueda en consola.
        /// </summary>
        public override string ToString()
        {
            return $"ID: {Id} | Nombre: {NombreCompleto} | Edad: {Edad} | Sexo: {Sexo} | " +
                   $"Diagnóstico: {Diagnostico} | Fecha de ingreso: {FechaIngreso:dd/MM/yyyy}";
        }
    }
}
