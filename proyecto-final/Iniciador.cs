using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using controlador;
using modelo;

namespace proyecto_final
{
    /// <summary>
    /// Clase principal que maneja la interfaz de usuario y el flujo del sistema de matrícula.
    /// Proporciona todos los menús, validaciones de entrada y coordinación entre controladores.
    /// </summary>
    class Iniciador
    {
        /// <summary>
        /// Método principal que inicia el sistema de matrícula universitaria.
        /// Solicita el DNI del usuario y determina si debe registrarse o acceder como usuario existente.
        /// </summary>
        public static void iniciador()
        {
            Console.WriteLine("=== SISTEMA DE MATRICULA UNIVERSIDAD BUEN ALEGRE===");

            string dniIngresado;

            // Pedir DNI hasta que sea válido
            do
            {
                Console.WriteLine("Hola, por favor ingresa tu DNI:");
                dniIngresado = Console.ReadLine();

                if (!ControladorUsuario.ValidarDni(dniIngresado))
                {
                    Console.WriteLine("DNI inválido. Debe tener 8 dígitos.\n");
                }

            } while (!ControladorUsuario.ValidarDni(dniIngresado));

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

        /// <summary>
        /// Maneja el proceso de registro de un nuevo usuario en el sistema.
        /// Solicita y valida todos los datos personales requeridos, crea el usuario y procede con la matrícula.
        /// </summary>
        /// <param name="dni">DNI del nuevo usuario a registrar</param>
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

        /// <summary>
        /// Muestra el menú principal para usuarios existentes en el sistema.
        /// Permite actualizar datos, gestionar matrícula, ver información completa o salir del sistema.
        /// </summary>
        /// <param name="usuario">Usuario existente que ha iniciado sesión</param>
        private static void MostrarMenuUsuarioExistente(Usuario usuario)
        {
            Console.Clear();
            Console.WriteLine($"¡Bienvenido {usuario.ObtenerNombreCompleto()} a sistema de matriculas Buen Alegre!");
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

        /// <summary>
        /// Permite al usuario actualizar sus datos personales de forma interactiva.
        /// Muestra los valores actuales y permite modificar cada campo individualmente con validación.
        /// </summary>
        /// <param name="usuario">Usuario cuyos datos se van a actualizar</param>
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

        /// <summary>
        /// Gestiona las opciones de matrícula para usuarios existentes.
        /// Si el usuario tiene matrícula, muestra opciones de gestión; si no, inicia el proceso de matrícula.
        /// </summary>
        /// <param name="usuario">Usuario para el cual gestionar la matrícula</param>
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

        /// <summary>
        /// Maneja el proceso completo de matrícula para un usuario.
        /// Incluye selección de grado, turno, materias y creación de la matrícula final.
        /// </summary>
        /// <param name="usuario">Usuario que realizará la matrícula</param>
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

        /// <summary>
        /// Permite al usuario seleccionar su grado académico de una lista de opciones disponibles.
        /// Muestra todos los grados disponibles y valida la selección del usuario.
        /// </summary>
        /// <returns>Nombre del grado académico seleccionado</returns>
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

        /// <summary>
        /// Permite al usuario seleccionar su turno de clases (mañana o tarde).
        /// Valida la entrada y proporciona un valor por defecto en caso de opción inválida.
        /// </summary>
        /// <returns>Turno seleccionado por el usuario</returns>
        private static Turno SeleccionarTurno()
        {
            Console.WriteLine("\n=== SELECCIÓN DE TURNO ===");
            Console.WriteLine("1. Mañana");
            Console.WriteLine("2. Tarde");
            Console.Write("Selecciona tu turno: ");

            string opcion = Console.ReadLine();

            Turno turnoSeleccionado;
            switch (opcion)
            {
                case "1":
                    turnoSeleccionado = Turno.Mañana;
                    break;
                case "2":
                    turnoSeleccionado = Turno.Tarde;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Seleccionando turno de mañana por defecto.");
                    turnoSeleccionado = Turno.Mañana;
                    break;
            }

            Console.WriteLine($"Has seleccionado turno: {turnoSeleccionado}");
            return turnoSeleccionado;
        }

        /// <summary>
        /// Coordina la selección de materias, permitiendo al usuario elegir entre selección manual o automática.
        /// Retorna las materias seleccionadas y un indicador del tipo de selección utilizado.
        /// </summary>
        /// <returns>Tupla con la lista de materias seleccionadas y un booleano indicando si fue selección automática</returns>
        private static (List<Materia> materias, bool esAutomatico) SeleccionarMaterias()
        {
            Console.WriteLine("\n=== SELECCIÓN DE MATERIAS ===");
            Console.WriteLine("¿Cómo deseas seleccionar tus materias?");
            Console.WriteLine("1. Seleccionar manualmente");
            Console.WriteLine("2. Selección automática (aleatoria)");
            Console.Write("Selecciona una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Selección manual activada.");
                    return (SeleccionarMateriasManual(), false);
                case "2":
                    Console.WriteLine("Selección automática activada.");
                    return (SeleccionarMateriasAutomatico(), true);
                default:
                    Console.WriteLine("Opción inválida. Seleccionando automáticamente.");
                    return (SeleccionarMateriasAutomatico(), true);
            }
        }

        /// <summary>
        /// Permite al usuario seleccionar manualmente 6 materias de la lista disponible.
        /// Valida que se seleccionen exactamente 6 materias sin repeticiones y que todas sean válidas.
        /// </summary>
        /// <returns>Lista de 6 materias seleccionadas por el usuario</returns>
        private static List<Materia> SeleccionarMateriasManual()
        {
            var materiasDisponibles = Materia.ObtenerMateriasDisponibles();
            var materiasSeleccionadas = new List<Materia>();

            while (true)
            {
                Console.WriteLine("\nMaterias disponibles:");
                for (int i = 0; i < materiasDisponibles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {materiasDisponibles[i].Nombre} ({materiasDisponibles[i].Creditos} créditos)");
                }

                Console.WriteLine("\nSelecciona 6 materias (ingresa los números separados por comas):");
                Console.Write("Ejemplo: 1,3,5,7,9,10: ");

                string entrada = Console.ReadLine();

                // Validar que no esté vacío
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("\nEntrada vacía. Por favor, ingresa los números de las materias.");
                    continue;
                }

                string[] indices = entrada.Split(',');

                // Validar que sean exactamente 6 números
                if (indices.Length != 6)
                {
                    Console.WriteLine($"\nDebes seleccionar exactamente 6 materias. Has ingresado {indices.Length}. Inténtalo de nuevo.");
                    continue;
                }

                materiasSeleccionadas.Clear();
                bool seleccionValida = true;

                foreach (string indiceStr in indices)
                {
                    if (!int.TryParse(indiceStr.Trim(), out int indice) || indice < 1 || indice > materiasDisponibles.Count)
                    {
                        Console.WriteLine($"\nNúmero inválido: '{indiceStr.Trim()}'. Debe ser un número entre 1 y {materiasDisponibles.Count}.");
                        seleccionValida = false;
                        break;
                    }

                    var materia = materiasDisponibles[indice - 1];
                    if (materiasSeleccionadas.Any(m => m.Id == materia.Id))
                    {
                        Console.WriteLine($"\nMateria repetida: {materia.Nombre}. No puedes seleccionar la misma materia dos veces.");
                        seleccionValida = false;
                        break;
                    }

                    materiasSeleccionadas.Add(materia);
                }

                if (seleccionValida && materiasSeleccionadas.Count == 6)
                {
                    Console.WriteLine("\nMaterias seleccionadas manualmente:");
                    foreach (var materia in materiasSeleccionadas)
                    {
                        Console.WriteLine($"- {materia.Nombre} ({materia.Creditos} créditos)");
                    }
                    return materiasSeleccionadas;
                }

                if (seleccionValida)
                {
                    Console.WriteLine("\nError inesperado en la selección. Inténtalo de nuevo.");
                }
            }
        }

        /// <summary>
        /// Selecciona automáticamente 6 materias aleatorias de la lista de materias disponibles.
        /// Garantiza que no haya repeticiones en la selección.
        /// </summary>
        /// <returns>Lista de 6 materias seleccionadas aleatoriamente</returns>
        private static List<Materia> SeleccionarMateriasAutomatico()
        {
            var materiasDisponibles = Materia.ObtenerMateriasDisponibles();
            var random = new Random();
            var materiasSeleccionadas = new List<Materia>();

            while (materiasSeleccionadas.Count < 6)
            {
                var materiaAleatoria = materiasDisponibles[random.Next(materiasDisponibles.Count)];
                if (!materiasSeleccionadas.Any(m => m.Id == materiaAleatoria.Id))
                {
                    materiasSeleccionadas.Add(materiaAleatoria);
                }
            }

            Console.WriteLine("\nMaterias seleccionadas automáticamente:");
            foreach (var materia in materiasSeleccionadas)
            {
                Console.WriteLine($"- {materia.Nombre} ({materia.Creditos} créditos)");
            }

            return materiasSeleccionadas;
        }

        /// <summary>
        /// Muestra la información completa de la matrícula actual del usuario.
        /// Si no tiene matrícula, informa al usuario y regresa al menú de gestión.
        /// </summary>
        /// <param name="usuario">Usuario cuya matrícula se mostrará</param>
        private static void MostrarMatriculaActual(Usuario usuario)
        {
            if (usuario.TieneMatricula())
            {
                Console.WriteLine("\n=== MATRÍCULA ACTUAL ===");
                Console.WriteLine(usuario.MatriculaActual.ObtenerResumenMatricula());
            }
            else
            {
                Console.WriteLine("\nNo tienes una matrícula registrada.");
            }

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú de matrícula...");
            Console.ReadKey();
            Console.Clear();
            GestionarMatricula(usuario);
        }

        /// <summary>
        /// Permite al usuario actualizar su grado académico en la matrícula existente.
        /// Actualiza tanto en el controlador como en el objeto usuario local.
        /// </summary>
        /// <param name="usuario">Usuario cuyo grado se actualizará</param>
        private static void ActualizarGrado(Usuario usuario)
        {
            string nuevoGrado = SeleccionarGrado();
            ControladorMatricula.ActualizarGrado(usuario.Dni, nuevoGrado);
            usuario.MatriculaActual.Grado = nuevoGrado;
            Console.WriteLine("\n¡Grado actualizado correctamente!");

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú de matrícula...");
            Console.ReadKey();
            Console.Clear();
            GestionarMatricula(usuario);
        }

        /// <summary>
        /// Permite al usuario cambiar su turno de clases en la matrícula existente.
        /// Actualiza tanto en el controlador como en el objeto usuario local.
        /// </summary>
        /// <param name="usuario">Usuario cuyo turno se cambiará</param>
        private static void CambiarTurno(Usuario usuario)
        {
            Turno nuevoTurno = SeleccionarTurno();
            ControladorMatricula.ActualizarTurno(usuario.Dni, nuevoTurno);
            usuario.MatriculaActual.TurnoSeleccionado = nuevoTurno;
            Console.WriteLine("\n¡Turno actualizado correctamente!");

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú de matrícula...");
            Console.ReadKey();
            Console.Clear();
            GestionarMatricula(usuario);
        }

        /// <summary>
        /// Permite al usuario modificar las materias seleccionadas en su matrícula.
        /// Ofrece tanto selección manual como automática y actualiza todos los datos correspondientes.
        /// </summary>
        /// <param name="usuario">Usuario cuyas materias se modificarán</param>
        private static void ModificarMaterias(Usuario usuario)
        {
            var (nuevasMaterias, esSeleccionAutomatica) = SeleccionarMaterias();
            ControladorMatricula.ActualizarMaterias(usuario.Dni, nuevasMaterias);
            usuario.MatriculaActual.MateriasSeleccionadas = nuevasMaterias;
            usuario.MatriculaActual.EsSeleccionAutomatica = esSeleccionAutomatica;

            // Actualizar usuario en JSON
            ControladorUsuario.ActualizarUsuario(usuario);

            Console.WriteLine("\n¡Materias actualizadas correctamente!");

            Console.WriteLine("\nPresiona cualquier tecla para volver al menú de matrícula...");
            Console.ReadKey();
            Console.Clear();
            GestionarMatricula(usuario);
        }

        /// <summary>
        /// Muestra toda la información personal y académica del usuario de forma detallada.
        /// Incluye datos personales y, si existe, información completa de la matrícula.
        /// </summary>
        /// <param name="usuario">Usuario cuya información se mostrará</param>
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
