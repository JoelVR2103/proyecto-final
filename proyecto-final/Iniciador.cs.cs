using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controlador;
using modelo;

namespace proyecto_final
{
    internal class Iniciador
    {
        public static void iniciador()
        {
            Console.WriteLine("=== SISTEMA DE MATRICULA ===");
            Console.WriteLine("Hola, por favor ingresa tu DNI:");

            string dniIngresado = Console.ReadLine();

            // Validar formato de DNI
            if (!ControladorUsuario.ValidarDni(dniIngresado))
            {
                Console.WriteLine("DNI inválido. Debe tener 8 dígitos.");
                return;
            }

            // Verificar si el usuario existe
            if (ControladorUsuario.ExisteUsuario(dniIngresado))
            {
                Usuario usuarioExistente = ControladorUsuario.BuscarUsuarioPorDni(dniIngresado);
                Console.WriteLine($"¡Bienvenido {usuarioExistente.ObtenerNombreCompleto()}!");
                MostrarMenuUsuarioExistente(usuarioExistente);
            }
            else
            {
                Console.WriteLine("No estás registrado, por favor regístrate.");
                RegistrarNuevoUsuario(dniIngresado);
            }
        }

        private static void RegistrarNuevoUsuario(string dni)
        {
            Console.WriteLine("\n=== REGISTRO DE NUEVO USUARIO ===");

            Console.Write("Ingresa tus nombres: ");
            string nombres = Console.ReadLine();
            while (!ControladorUsuario.ValidarNombres(nombres))
            {
                Console.Write("Nombres inválidos. Solo se permiten letras y espacios (mínimo 2 caracteres): ");
                nombres = Console.ReadLine();
            }

            Console.Write("Ingresa tus apellidos: ");
            string apellidos = Console.ReadLine();
            while (!ControladorUsuario.ValidarApellidos(apellidos))
            {
                Console.Write("Apellidos inválidos. Solo se permiten letras y espacios (mínimo 2 caracteres): ");
                apellidos = Console.ReadLine();
            }

            Console.Write("Ingresa tu edad: ");
            int edad;
            while (!int.TryParse(Console.ReadLine(), out edad) || !ControladorUsuario.ValidarEdad(edad))
            {
                Console.Write("Edad inválida. Debe ser un número entre 16 y 100 años: ");
            }

            Console.Write("Ingresa tu sexo (Masculino/Femenino): ");
            string sexo = Console.ReadLine();
            while (!ControladorUsuario.ValidarSexo(sexo))
            {
                Console.Write("Sexo inválido. Ingresa 'Masculino' o 'Femenino': ");
                sexo = Console.ReadLine();
            }

            Console.Write("Ingresa tu teléfono: ");
            string telefono = Console.ReadLine();
            while (!ControladorUsuario.ValidarTelefono(telefono))
            {
                Console.Write("Teléfono inválido. Debe tener al menos 9 dígitos: ");
                telefono = Console.ReadLine();
            }

            Console.Write("Ingresa tu email: ");
            string email = Console.ReadLine();
            while (!ControladorUsuario.ValidarEmail(email))
            {
                Console.Write("Email inválido. Ingresa un email válido: ");
                email = Console.ReadLine();
            }

            Console.Write("Ingresa tu dirección: ");
            string direccion = Console.ReadLine();
            while (!ControladorUsuario.ValidarDireccion(direccion))
            {
                Console.Write("Dirección inválida. Debe tener al menos 5 caracteres: ");
                direccion = Console.ReadLine();
            }

            // Crear y registrar nuevo usuario (sin grado)
            Usuario nuevoUsuario = new Usuario(nombres, apellidos, edad, sexo, dni, telefono, email, direccion);
            ControladorUsuario.RegistrarUsuario(nuevoUsuario);

            Console.WriteLine($"\n¡Bienvenido {nombres} {apellidos}! Tu registro ha sido exitoso.");
            Console.WriteLine("Ahora procederemos con tu matrícula...");

            // Iniciar proceso de matrícula
            ProcesarMatricula(nuevoUsuario);
        }

        private static void MostrarMenuUsuarioExistente(Usuario usuario)
        {
            Console.Clear();
            Console.WriteLine($"¡Bienvenido {usuario.ObtenerNombreCompleto()}!");
            Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
            Console.WriteLine("1. Actualizar datos personales");
            Console.WriteLine("2. Gestionar matrícula");
            Console.WriteLine("3. Ver información completa");
            Console.WriteLine("4. Salir");
            Console.Write("Selecciona una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ActualizarDatosPersonales(usuario);
                    break;
                case "2":
                    GestionarMatricula(usuario);
                    break;
                case "3":
                    MostrarInformacionCompleta(usuario);
                    break;
                case "4":
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    MostrarMenuUsuarioExistente(usuario);
                    break;
            }
        }

        private static void ActualizarDatosPersonales(Usuario usuario)
        {
            Console.WriteLine("\n=== ACTUALIZACIÓN DE DATOS PERSONALES ===");
            Console.WriteLine("Presiona Enter para mantener el valor actual o ingresa el nuevo valor:");

            Console.Write($"Nombres ({usuario.Nombres}): ");
            string nuevosNombres = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevosNombres))
            {
                while (!ControladorUsuario.ValidarNombres(nuevosNombres))
                {
                    Console.Write("Nombres inválidos. Solo se permiten letras y espacios (mínimo 2 caracteres): ");
                    nuevosNombres = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevosNombres)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevosNombres))
                    usuario.Nombres = nuevosNombres;
            }

            Console.Write($"Apellidos ({usuario.Apellidos}): ");
            string nuevosApellidos = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevosApellidos))
            {
                while (!ControladorUsuario.ValidarApellidos(nuevosApellidos))
                {
                    Console.Write("Apellidos inválidos. Solo se permiten letras y espacios (mínimo 2 caracteres): ");
                    nuevosApellidos = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevosApellidos)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevosApellidos))
                    usuario.Apellidos = nuevosApellidos;
            }

            Console.Write($"Edad ({usuario.Edad}): ");
            string nuevaEdadStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaEdadStr))
            {
                int nuevaEdad;
                while (!int.TryParse(nuevaEdadStr, out nuevaEdad) || !ControladorUsuario.ValidarEdad(nuevaEdad))
                {
                    Console.Write("Edad inválida. Debe ser un número entre 16 y 100 años (Enter para cancelar): ");
                    nuevaEdadStr = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevaEdadStr)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevaEdadStr))
                    usuario.Edad = nuevaEdad;
            }

            Console.Write($"Sexo ({usuario.Sexo}): ");
            string nuevoSexo = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoSexo))
            {
                while (!ControladorUsuario.ValidarSexo(nuevoSexo))
                {
                    Console.Write("Sexo inválido. Ingresa 'Masculino' o 'Femenino' (Enter para cancelar): ");
                    nuevoSexo = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevoSexo)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevoSexo))
                    usuario.Sexo = nuevoSexo;
            }

            Console.Write($"Teléfono ({usuario.Telefono}): ");
            string nuevoTelefono = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoTelefono))
            {
                while (!ControladorUsuario.ValidarTelefono(nuevoTelefono))
                {
                    Console.Write("Teléfono inválido. Debe tener al menos 9 dígitos (Enter para cancelar): ");
                    nuevoTelefono = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevoTelefono)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevoTelefono))
                    usuario.Telefono = nuevoTelefono;
            }

            Console.Write($"Email ({usuario.Email}): ");
            string nuevoEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoEmail))
            {
                while (!ControladorUsuario.ValidarEmail(nuevoEmail))
                {
                    Console.Write("Email inválido. Ingresa un email válido (Enter para cancelar): ");
                    nuevoEmail = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevoEmail)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevoEmail))
                    usuario.Email = nuevoEmail;
            }

            Console.Write($"Dirección ({usuario.Direccion}): ");
            string nuevaDireccion = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaDireccion))
            {
                while (!ControladorUsuario.ValidarDireccion(nuevaDireccion))
                {
                    Console.Write("Dirección inválida. Debe tener al menos 5 caracteres (Enter para cancelar): ");
                    nuevaDireccion = Console.ReadLine();
                    if (string.IsNullOrEmpty(nuevaDireccion)) break; // Permitir cancelar
                }
                if (!string.IsNullOrEmpty(nuevaDireccion))
                    usuario.Direccion = nuevaDireccion;
            }

            ControladorUsuario.ActualizarUsuario(usuario);
            Console.WriteLine("\n¡Datos personales actualizados correctamente!");

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
            Console.ReadKey();
            Console.Clear();
            MostrarMenuUsuarioExistente(usuario);
        }

        private static void GestionarMatricula(Usuario usuario)
        {
            Console.Clear();
            if (usuario.TieneMatricula())
            {
                Console.WriteLine("\n=== GESTIÓN DE MATRÍCULA ===");
                Console.WriteLine("1. Ver matrícula actual");
                Console.WriteLine("2. Actualizar grado");
                Console.WriteLine("3. Cambiar turno");
                Console.WriteLine("4. Modificar materias");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Selecciona una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MostrarMatriculaActual(usuario);
                        break;
                    case "2":
                        ActualizarGrado(usuario);
                        break;
                    case "3":
                        CambiarTurno(usuario);
                        break;
                    case "4":
                        ModificarMaterias(usuario);
                        break;
                    case "5":
                        MostrarMenuUsuarioExistente(usuario);
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        GestionarMatricula(usuario);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nNo tienes una matrícula registrada. Procederemos a crear una nueva.");
                ProcesarMatricula(usuario);
            }
        }

        private static void ProcesarMatricula(Usuario usuario)
        {
            Console.WriteLine("\n=== PROCESO DE MATRÍCULA ===");

            // Seleccionar grado
            string grado = SeleccionarGrado();

            // Seleccionar turno
            Turno turno = SeleccionarTurno();

            // Seleccionar materias
            var (materiasSeleccionadas, esSeleccionAutomatica) = SeleccionarMaterias();

            // Crear matrícula
            Matricula nuevaMatricula = new Matricula(usuario.Dni, grado, turno, materiasSeleccionadas, esSeleccionAutomatica);

            // Asignar matrícula al usuario
            usuario.MatriculaActual = nuevaMatricula;

            // Registrar en el controlador
            ControladorMatricula.CrearMatricula(nuevaMatricula);

            // Actualizar usuario en JSON
            ControladorUsuario.ActualizarUsuario(usuario);

            Console.WriteLine("\n¡Matrícula completada exitosamente!");
            Console.WriteLine(nuevaMatricula.ObtenerResumenMatricula());

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
            Console.ReadKey();
            Console.Clear();
            MostrarMenuUsuarioExistente(usuario);
        }

        private static string SeleccionarGrado()
        {
            Console.WriteLine("\n=== SELECCIÓN DE GRADO ===");
            var grados = ControladorMatricula.ObtenerGradosDisponibles();

            for (int i = 0; i < grados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {grados[i]}");
            }

            Console.Write("Selecciona tu grado: ");
            int opcion;
            while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > grados.Count)
            {
                Console.Write("Opción inválida. Selecciona un grado válido: ");
            }

            string gradoSeleccionado = grados[opcion - 1];
            Console.WriteLine($"Has seleccionado: {gradoSeleccionado}");
            return gradoSeleccionado;
        }

        private static void MostrarInformacionCompleta(Usuario usuario)
        {
            Console.WriteLine("\n=== INFORMACIÓN COMPLETA ===");
            Console.WriteLine($"Nombre: {usuario.ObtenerNombreCompleto()}");
            Console.WriteLine($"DNI: {usuario.Dni}");
            Console.WriteLine($"Edad: {usuario.Edad}");
            Console.WriteLine($"Sexo: {usuario.Sexo}");
            Console.WriteLine($"Teléfono: {usuario.Telefono}");
            Console.WriteLine($"Email: {usuario.Email}");
            Console.WriteLine($"Dirección: {usuario.Direccion}");

            if (usuario.TieneMatricula())
            {
                Console.WriteLine("\n=== INFORMACIÓN DE MATRÍCULA ===");
                Console.WriteLine(usuario.MatriculaActual.ObtenerResumenMatricula());
            }
            else
            {
                Console.WriteLine("\nNo tienes una matrícula registrada.");
            }

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú principal...");
            Console.ReadKey();
            Console.Clear();
            MostrarMenuUsuarioExistente(usuario);
        }
    }
}
