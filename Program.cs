using System;
using System.Globalization;

namespace SistemaGestionPacientes
{
    /// <summary>
    /// Punto de entrada del programa. Contiene el menú principal, la lectura
    /// y validación de datos por consola, y el flujo de cada opción.
    /// Toda la lógica de negocio (CRUD) vive en GestorPacientes, no aquí.
    /// </summary>
    class Program
    {
        private static readonly GestorPacientes gestor = new GestorPacientes();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ProcesoRegistrar();
                        break;
                    case "2":
                        ProcesoListar();
                        break;
                    case "3":
                        ProcesoBuscar();
                        break;
                    case "4":
                        ProcesoActualizar();
                        break;
                    case "5":
                        ProcesoEliminar();
                        break;
                    case "6":
                        salir = true;
                        Console.WriteLine("\nGracias por usar el Sistema de Gestión de Pacientes. ¡Hasta pronto!");
                        break;
                    default:
                        Console.WriteLine("\n[AVISO] Opción inválida. Por favor seleccione una opción del 1 al 6.");
                        break;
                }
            }
        }

        /// <summary>
        /// Imprime el menú principal del sistema.
        /// </summary>
        static void MostrarMenu()
        {
            Console.WriteLine("\n=========================================");
            Console.WriteLine("   SISTEMA DE GESTIÓN DE PACIENTES");
            Console.WriteLine("=========================================");
            Console.WriteLine("1. Registrar nuevo paciente");
            Console.WriteLine("2. Listar todos los pacientes");
            Console.WriteLine("3. Buscar paciente por ID o nombre");
            Console.WriteLine("4. Actualizar datos de un paciente");
            Console.WriteLine("5. Eliminar un paciente");
            Console.WriteLine("6. Salir del sistema");
            Console.WriteLine("=========================================");
            Console.Write("Seleccione una opción: ");
        }

        // ==================== OPCIÓN 1: REGISTRAR ====================
        static void ProcesoRegistrar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- Registrar nuevo paciente ---");

                string id = LeerTextoNoVacio("ID (cédula): ");
                string nombre = LeerTextoNoVacio("Nombre completo: ");
                int edad = LeerEdad("Edad: ");
                string sexo = LeerSexo("Sexo (M/F): ");
                string diagnostico = LeerTextoNoVacio("Diagnóstico: ");
                DateTime fecha = LeerFecha("Fecha de ingreso (dd/MM/aaaa): ");

                Paciente nuevo = new Paciente(id, nombre, edad, sexo, diagnostico, fecha);
                gestor.RegistrarPaciente(nuevo);

                continuar = PreguntarRepetir("¿Desea registrar otro paciente? (S/N): ");
            }
        }

        // ==================== OPCIÓN 2: LISTAR ====================
        static void ProcesoListar()
        {
            gestor.ListarPacientes();
            Console.WriteLine("\nPresione ENTER para volver al menú principal...");
            Console.ReadLine();
        }

        // ==================== OPCIÓN 3: BUSCAR ====================
        static void ProcesoBuscar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- Buscar paciente por ID o nombre ---");
                Console.Write("Ingrese el ID o nombre a buscar: ");
                string criterio = Console.ReadLine();

                var resultados = gestor.BuscarPacientes(criterio);
                if (resultados.Count == 0)
                {
                    Console.WriteLine("\n[AVISO] No se encontraron pacientes que coincidan con la búsqueda.");
                }
                else
                {
                    Console.WriteLine($"\n--- Resultados encontrados ({resultados.Count}) ---");
                    foreach (Paciente p in resultados)
                    {
                        Console.WriteLine(p.ToString());
                    }
                }

                continuar = PreguntarRepetir("¿Desea realizar otra búsqueda? (S/N): ");
            }
        }

        // ==================== OPCIÓN 4: ACTUALIZAR ====================
        static void ProcesoActualizar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- Actualizar datos de un paciente ---");
                Console.Write("Ingrese el ID del paciente a actualizar: ");
                string id = Console.ReadLine();

                Paciente existente = gestor.BuscarPorId(id);
                if (existente == null)
                {
                    Console.WriteLine($"\n[ERROR] No se encontró ningún paciente con el ID '{id}'.");
                }
                else
                {
                    Console.WriteLine($"\nDatos actuales -> {existente}");
                    Console.WriteLine("Ingrese los nuevos datos a continuación:");

                    string nombre = LeerTextoNoVacio("Nombre completo: ");
                    int edad = LeerEdad("Edad: ");
                    string sexo = LeerSexo("Sexo (M/F): ");
                    string diagnostico = LeerTextoNoVacio("Diagnóstico: ");
                    DateTime fecha = LeerFecha("Fecha de ingreso (dd/MM/aaaa): ");

                    gestor.ActualizarPaciente(id, nombre, edad, sexo, diagnostico, fecha);
                }

                continuar = PreguntarRepetir("¿Desea actualizar otro paciente? (S/N): ");
            }
        }

        // ==================== OPCIÓN 5: ELIMINAR ====================
        static void ProcesoEliminar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("\n--- Eliminar un paciente ---");
                Console.Write("Ingrese el ID del paciente a eliminar: ");
                string id = Console.ReadLine();

                Paciente existente = gestor.BuscarPorId(id);
                if (existente == null)
                {
                    Console.WriteLine($"\n[ERROR] No se encontró ningún paciente con el ID '{id}'.");
                }
                else
                {
                    Console.WriteLine($"\nPaciente encontrado -> {existente}");
                    Console.Write("¿Está seguro de que desea eliminar este paciente? (S/N): ");
                    string confirmacion = Console.ReadLine();

                    if ((confirmacion ?? string.Empty).Trim().ToUpper() == "S")
                    {
                        gestor.EliminarPaciente(id);
                    }
                    else
                    {
                        Console.WriteLine("\nOperación cancelada. El paciente no fue eliminado.");
                    }
                }

                continuar = PreguntarRepetir("¿Desea eliminar otro paciente? (S/N): ");
            }
        }

        // ==================== LECTURA Y VALIDACIÓN DE DATOS ====================

        /// <summary>
        /// Solicita un texto por consola y no continúa hasta que el campo no esté vacío.
        /// </summary>
        static string LeerTextoNoVacio(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(texto))
                {
                    Console.WriteLine("[AVISO] Este campo es obligatorio y no puede estar vacío. Intente de nuevo.");
                }
            } while (string.IsNullOrWhiteSpace(texto));

            return texto.Trim();
        }

        /// <summary>
        /// Solicita la edad y valida que sea un número entero dentro de un rango razonable.
        /// </summary>
        static int LeerEdad(string mensaje)
        {
            int edad;
            bool valido;
            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                valido = int.TryParse(entrada, out edad) && edad > 0 && edad <= 120;
                if (!valido)
                {
                    Console.WriteLine("[AVISO] Ingrese una edad válida (número entero entre 1 y 120).");
                }
            } while (!valido);

            return edad;
        }

        /// <summary>
        /// Solicita el sexo del paciente y valida que sea 'M' o 'F'.
        /// </summary>
        static string LeerSexo(string mensaje)
        {
            string sexo;
            bool valido;
            do
            {
                Console.Write(mensaje);
                sexo = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
                valido = sexo == "M" || sexo == "F";
                if (!valido)
                {
                    Console.WriteLine("[AVISO] Ingrese 'M' para masculino o 'F' para femenino.");
                }
            } while (!valido);

            return sexo;
        }

        /// <summary>
        /// Solicita la fecha de ingreso en formato dd/MM/aaaa y valida que sea una fecha real.
        /// </summary>
        static DateTime LeerFecha(string mensaje)
        {
            DateTime fecha;
            bool valido;
            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                valido = DateTime.TryParseExact(entrada, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
                if (!valido)
                {
                    Console.WriteLine("[AVISO] Ingrese una fecha válida en formato dd/MM/aaaa (ejemplo: 15/08/2026).");
                }
            } while (!valido);

            return fecha;
        }

        /// <summary>
        /// Pregunta al usuario si desea repetir la operación actual.
        /// Devuelve true únicamente si responde 'S'.
        /// </summary>
        static bool PreguntarRepetir(string mensaje)
        {
            Console.Write("\n" + mensaje);
            string respuesta = (Console.ReadLine() ?? string.Empty).Trim().ToUpper();
            return respuesta == "S";
        }
    }
}
