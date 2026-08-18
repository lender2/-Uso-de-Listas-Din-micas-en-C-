using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionPacientes
{
  
    public class GestorPacientes
    {
        private List<Paciente> pacientes;

        public GestorPacientes()
        {
            pacientes = new List<Paciente>();
        }

        public bool RegistrarPaciente(Paciente nuevoPaciente)
        {
            if (ExisteId(nuevoPaciente.Id))
            {
                Console.WriteLine($"\n[ERROR] Ya existe un paciente registrado con el ID '{nuevoPaciente.Id}'.");
                return false;
            }

            pacientes.Add(nuevoPaciente);
            Console.WriteLine("\n[OK] Paciente registrado exitosamente.");
            return true;
        }

        public void ListarPacientes()
        {
            if (pacientes.Count == 0)
            {
                Console.WriteLine("\nNo hay pacientes registrados en el sistema.");
                return;
            }

            Console.WriteLine($"\n--- Listado de pacientes ({pacientes.Count}) ---");
            foreach (Paciente p in pacientes)
            {
                Console.WriteLine(p.ToString());
            }
        }

        public List<Paciente> BuscarPacientes(string criterio)
        {
            string criterioNormalizado = (criterio ?? string.Empty).Trim().ToLower();

            return pacientes.Where(p =>
                p.Id.ToLower() == criterioNormalizado ||
                p.NombreCompleto.ToLower().Contains(criterioNormalizado)
            ).ToList();
        }

        public Paciente BuscarPorId(string id)
        {
            string idNormalizado = (id ?? string.Empty).Trim().ToLower();
            return pacientes.FirstOrDefault(p => p.Id.ToLower() == idNormalizado);
        }

        public bool ActualizarPaciente(string id, string nuevoNombre, int nuevaEdad, string nuevoSexo, string nuevoDiagnostico, DateTime nuevaFecha)
        {
            Paciente paciente = BuscarPorId(id);
            if (paciente == null)
            {
                Console.WriteLine($"\n[ERROR] No se encontró ningún paciente con el ID '{id}'.");
                return false;
            }

            paciente.NombreCompleto = nuevoNombre;
            paciente.Edad = nuevaEdad;
            paciente.Sexo = nuevoSexo;
            paciente.Diagnostico = nuevoDiagnostico;
            paciente.FechaIngreso = nuevaFecha;

            Console.WriteLine("\n[OK] Datos del paciente actualizados correctamente.");
            return true;
        }

        public bool EliminarPaciente(string id)
        {
            Paciente paciente = BuscarPorId(id);
            if (paciente == null)
            {
                Console.WriteLine($"\n[ERROR] No se encontró ningún paciente con el ID '{id}'.");
                return false;
            }

            pacientes.Remove(paciente);
            Console.WriteLine("\n[OK] Paciente eliminado correctamente.");
            return true;
        }

        public bool ExisteId(string id)
        {
            string idNormalizado = (id ?? string.Empty).Trim().ToLower();
            return pacientes.Any(p => p.Id.ToLower() == idNormalizado);
        }

        public int CantidadPacientes()
        {
            return pacientes.Count;
        }
    }
}
