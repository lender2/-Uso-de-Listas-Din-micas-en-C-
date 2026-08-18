using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionPacientes
{
    /// <summary>
    /// Clase encargada de toda la lógica de negocio (CRUD) sobre la lista dinámica
    /// de pacientes. La List&lt;Paciente&gt; funciona como base de datos temporal en
    /// memoria mientras el programa está en ejecución.
    /// </summary>
    public class GestorPacientes
    {
        private List<Paciente> pacientes;

        public GestorPacientes()
        {
            pacientes = new List<Paciente>();
        }

        // ==================== CREAR (ALTA) ====================

        /// <summary>
        /// Registra un nuevo paciente en la lista, validando que el ID no esté duplicado.
        /// </summary>
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

        // ==================== LEER (CONSULTA) ====================

        /// <summary>
        /// Muestra en consola todos los pacientes registrados.
        /// </summary>
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

        /// <summary>
        /// Busca pacientes cuyo ID coincida exactamente o cuyo nombre contenga
        /// el criterio ingresado (sin distinguir mayúsculas/minúsculas).
        /// </summary>
        public List<Paciente> BuscarPacientes(string criterio)
        {
            string criterioNormalizado = (criterio ?? string.Empty).Trim().ToLower();

            return pacientes.Where(p =>
                p.Id.ToLower() == criterioNormalizado ||
                p.NombreCompleto.ToLower().Contains(criterioNormalizado)
            ).ToList();
        }

        /// <summary>
        /// Busca un único paciente por coincidencia exacta de ID.
        /// Se usa como paso previo obligatorio antes de actualizar o eliminar.
        /// </summary>
        public Paciente BuscarPorId(string id)
        {
            string idNormalizado = (id ?? string.Empty).Trim().ToLower();
            return pacientes.FirstOrDefault(p => p.Id.ToLower() == idNormalizado);
        }

        // ==================== ACTUALIZAR (MODIFICAR) ====================

        /// <summary>
        /// Actualiza los datos de un paciente ya existente, localizándolo primero por ID.
        /// </summary>
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

        // ==================== ELIMINAR (BAJA) ====================

        /// <summary>
        /// Elimina un paciente de la lista a partir de su ID.
        /// La confirmación previa se solicita en Program.cs antes de llamar a este método.
        /// </summary>
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

        // ==================== VALIDACIONES / UTILIDADES ====================

        /// <summary>
        /// Indica si ya existe un paciente registrado con el ID dado.
        /// </summary>
        public bool ExisteId(string id)
        {
            string idNormalizado = (id ?? string.Empty).Trim().ToLower();
            return pacientes.Any(p => p.Id.ToLower() == idNormalizado);
        }

        /// <summary>
        /// Cantidad total de pacientes registrados actualmente.
        /// </summary>
        public int CantidadPacientes()
        {
            return pacientes.Count;
        }
    }
}
