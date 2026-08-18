# Sistema de Gestión de Pacientes

**Caso Práctico – Algoritmos Computacionales | Universidad O&M**
**Tema:** Uso de listas dinámicas (`List<T>`) en C#
**Modalidad:** Trabajo en pareja

## Integrantes

| Nombre completo | Matrícula |
|---|---|
| Lender Manuel Sanchez Nonanci | 25-SISN-2-047 |
| David Eduardo Olivo Encarnacion | 25-SISN-2-021 |

**Docente:** Gamalier Reyes del Carmen

## Descripción breve

Aplicación de consola en C# que simula un sistema de gestión de pacientes para una clínica. Permite **registrar, listar, buscar, actualizar y eliminar** pacientes, almacenando toda la información en una lista dinámica (`List<Paciente>`) en memoria mientras el programa está en ejecución.

El programa está organizado de forma orientada a objetos en tres archivos:

- **`Paciente.cs`** – clase modelo con los atributos de cada paciente.
- **`GestorPacientes.cs`** – clase con toda la lógica CRUD sobre la lista de pacientes (validaciones, búsqueda, alta, baja y modificación).
- **`Program.cs`** – menú principal, lectura de datos por consola y validación de entrada del usuario.

## Datos de entrada

Al registrar o actualizar un paciente, el sistema solicita:

- **ID (cédula):** identificador único del paciente.
- **Nombre completo**
- **Edad:** número entero entre 1 y 120.
- **Sexo:** `M` o `F`.
- **Diagnóstico**
- **Fecha de ingreso:** formato `dd/MM/aaaa`.

## Datos que procesa

- Valida que ningún campo obligatorio quede vacío.
- Valida que el ID no esté duplicado al registrar un nuevo paciente.
- Valida que la edad sea un número entero dentro de un rango válido (1–120).
- Valida que la fecha de ingreso tenga un formato correcto (`dd/MM/aaaa`).
- Busca pacientes por coincidencia exacta de ID o coincidencia parcial de nombre.
- Localiza al paciente por ID antes de permitir su actualización o eliminación.
- Solicita confirmación (S/N) antes de eliminar un paciente.
- Después de cada operación, pregunta si se desea repetir o volver al menú principal.

## Datos de salida

- Listado completo de pacientes registrados.
- Resultados de búsqueda por ID o nombre.
- Mensajes `[OK]` de confirmación ante operaciones exitosas (registro, actualización, eliminación).
- Mensajes `[ERROR]` / `[AVISO]` claros cuando el ID no existe, está duplicado, o los datos ingresados no son válidos.

## Cómo ejecutar el proyecto

1. Instalar el [SDK de .NET](https://dotnet.microsoft.com/download) (versión 8.0 o superior).
2. Abrir una terminal en la carpeta del proyecto (donde está `SistemaGestionPacientes.csproj`).
3. Ejecutar:
   ```
   dotnet run
   ```
4. Seguir las opciones del menú en pantalla.

## Capturas de pantalla

_(Agregar aquí las capturas de pantalla de la ejecución de cada opción del menú: registrar, listar, buscar, actualizar, eliminar y salir del sistema.)_

## Estructura del proyecto

```
SistemaGestionPacientes/
├── Paciente.cs
├── GestorPacientes.cs
├── Program.cs
├── SistemaGestionPacientes.csproj
└── README.md
```
