using modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlador
{
    /// <summary>
    /// Controlador principal para la gestión de matrículas del sistema universitario.
    /// Proporciona funcionalidades para crear, actualizar, validar y administrar matrículas de estudiantes.
    /// Maneja la selección de materias, turnos, grados académicos y validaciones correspondientes.
    /// </summary>
    public class ControladorMatricula
    {

        /// <summary>
        /// Obtiene la lista de grados académicos disponibles en la universidad.
        /// </summary>
        /// <returns>Lista de strings con los nombres de los grados disponibles</returns>
        public static List<string> ObtenerGradosDisponibles()
        {
            return new List<string>
            {
                "Ingeniería de Sistemas",
                "Ingeniería Industrial",
                "Medicina",
                "Derecho",
                "Administración",
                "Contabilidad",
                "Psicología",
                "Arquitectura"
            };
        }

        /// <summary>
        /// Busca la matrícula asociada a un estudiante específico por su DNI.
        /// </summary>
        /// <param name="dni">Número de DNI del estudiante</param>
        /// <returns>Objeto Matricula del estudiante o null si no tiene matrícula</returns>
        public static Matricula BuscarMatriculaPorDni(string dni)
        {
            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            return usuario?.MatriculaActual;
        }

        /// <summary>
        /// Verifica si un estudiante tiene una matrícula activa.
        /// </summary>
        /// <param name="dni">Número de DNI del estudiante</param>
        /// <returns>True si el estudiante tiene matrícula, false en caso contrario</returns>
        public static bool TieneMatricula(string dni)
        {
            return BuscarMatriculaPorDni(dni) != null;
        }

        /// <summary>
        /// Crea una nueva matrícula para un estudiante con los parámetros básicos.
        /// Valida que el estudiante exista, no tenga matrícula previa y que los datos sean válidos.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="grado">Grado académico seleccionado</param>
        /// <param name="turno">Turno de clases (Mañana, Tarde, Noche)</param>
        /// <returns>True si la matrícula se creó exitosamente, false en caso contrario</returns>
        public static bool CrearMatricula(string dni, string grado, Turno turno)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            // Validar que el grado sea válido
            if (!ValidarGrado(grado))
                return false;

            // Verificar que no tenga matrícula previa
            if (TieneMatricula(dni))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario == null)
                return false;

            var nuevaMatricula = new Matricula(dni, grado, turno);
            usuario.MatriculaActual = nuevaMatricula;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Crea una nueva matrícula usando un objeto Matricula completo.
        /// Realiza validaciones completas de los datos y verifica que el estudiante no tenga matrícula previa.
        /// </summary>
        /// <param name="matricula">Objeto Matricula con todos los datos de la matrícula</param>
        /// <returns>True si la matrícula se creó exitosamente, false en caso contrario</returns>
        public static bool CrearMatricula(Matricula matricula)
        {
            // Validar datos de la matrícula
            var erroresValidacion = ValidarDatosMatricula(matricula);
            if (erroresValidacion != null)
                return false;

            // Verificar que no tenga matrícula previa
            if (TieneMatricula(matricula.DniEstudiante))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(matricula.DniEstudiante);
            if (usuario == null)
                return false;

            usuario.MatriculaActual = matricula;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Agrega una materia específica a la matrícula de un estudiante.
        /// Valida que la materia exista, el estudiante tenga matrícula y no exceda el límite de materias.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="idMateria">ID de la materia a agregar</param>
        /// <returns>True si la materia se agregó exitosamente, false en caso contrario</returns>
        public static bool AgregarMateriaAMatricula(string dni, int idMateria)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            // Validar ID de materia
            if (!ValidarIdMateria(idMateria))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            var materia = Materia.ObtenerMateriasDisponibles().FirstOrDefault(m => m.Id == idMateria);
            if (materia == null)
                return false;

            bool resultado = usuario.MatriculaActual.AgregarMateria(materia);
            if (resultado)
                ControladorUsuario.ActualizarUsuario(usuario);
            return resultado;
        }

        /// <summary>
        /// Remueve una materia específica de la matrícula de un estudiante.
        /// Valida que el estudiante tenga matrícula y que la materia exista.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="idMateria">ID de la materia a remover</param>
        /// <returns>True si la materia se removió exitosamente, false en caso contrario</returns>
        public static bool RemoverMateriaDeMatricula(string dni, int idMateria)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            // Validar ID de materia
            if (!ValidarIdMateria(idMateria))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            usuario.MatriculaActual.RemoverMateria(idMateria);
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Obtiene la lista de materias disponibles para selección por parte de un estudiante.
        /// Excluye las materias que ya están seleccionadas en su matrícula actual.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <returns>Lista de materias disponibles para seleccionar</returns>
        public static List<Materia> ObtenerMateriasDisponiblesParaSeleccion(string dni)
        {
            var matricula = BuscarMatriculaPorDni(dni);
            if (matricula == null)
                return Materia.ObtenerMateriasDisponibles();

            var todasLasMaterias = Materia.ObtenerMateriasDisponibles();
            var materiasSeleccionadas = matricula.MateriasSeleccionadas.Select(m => m.Id).ToList();

            return todasLasMaterias.Where(m => !materiasSeleccionadas.Contains(m.Id)).ToList();
        }

        /// <summary>
        /// Selecciona automáticamente 6 materias aleatorias para un estudiante.
        /// Limpia las materias actuales y selecciona nuevas materias de forma aleatoria.
        /// Marca la matrícula como selección automática.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <returns>True si la selección automática fue exitosa, false en caso contrario</returns>
        public static bool SeleccionarMateriasAutomaticamente(string dni)
        {
            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            var materiasDisponibles = ObtenerMateriasDisponiblesParaSeleccion(dni);
            var random = new Random();

            // Limpiar materias actuales
            usuario.MatriculaActual.MateriasSeleccionadas.Clear();

            // Seleccionar 6 materias aleatoriamente
            var materiasSeleccionadas = materiasDisponibles.OrderBy(x => random.Next()).Take(6).ToList();

            foreach (var materia in materiasSeleccionadas)
            {
                usuario.MatriculaActual.AgregarMateria(materia);
            }

            usuario.MatriculaActual.EsSeleccionAutomatica = true;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Actualiza el turno de clases de un estudiante en su matrícula.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="nuevoTurno">Nuevo turno a asignar (Mañana, Tarde, Noche)</param>
        /// <returns>True si el turno se actualizó exitosamente, false en caso contrario</returns>
        public static bool ActualizarTurno(string dni, Turno nuevoTurno)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            usuario.MatriculaActual.TurnoSeleccionado = nuevoTurno;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Actualiza el grado académico de un estudiante en su matrícula.
        /// Valida que el nuevo grado sea válido y esté disponible.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="nuevoGrado">Nuevo grado académico a asignar</param>
        /// <returns>True si el grado se actualizó exitosamente, false en caso contrario</returns>
        public static bool ActualizarGrado(string dni, string nuevoGrado)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            // Validar que el grado sea válido
            if (!ValidarGrado(nuevoGrado))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            usuario.MatriculaActual.Grado = nuevoGrado;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Actualiza completamente la lista de materias seleccionadas de un estudiante.
        /// Valida que la nueva lista contenga exactamente 6 materias válidas y sin repeticiones.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <param name="nuevasMaterias">Nueva lista de materias a asignar</param>
        /// <returns>True si las materias se actualizaron exitosamente, false en caso contrario</returns>
        public static bool ActualizarMaterias(string dni, List<Materia> nuevasMaterias)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return false;

            // Validar lista de materias
            if (!ValidarListaMaterias(nuevasMaterias))
                return false;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario?.MatriculaActual == null)
                return false;

            usuario.MatriculaActual.MateriasSeleccionadas = nuevasMaterias;
            ControladorUsuario.ActualizarUsuario(usuario);
            return true;
        }

        /// <summary>
        /// Obtiene un resumen completo de la matrícula de un estudiante.
        /// Incluye información sobre grado, turno, materias seleccionadas y créditos totales.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <returns>String con el resumen de la matrícula o mensaje de error si no existe</returns>
        public static string ObtenerResumenMatricula(string dni)
        {
            var matricula = BuscarMatriculaPorDni(dni);
            if (matricula == null)
                return "No se encontró matrícula para este DNI.";

            return matricula.ObtenerResumenMatricula();
        }

        /// <summary>
        /// Valida si una matrícula está completa y cumple con todos los requisitos.
        /// Verifica que tenga exactamente 6 materias seleccionadas.
        /// </summary>
        /// <param name="dni">DNI del estudiante</param>
        /// <returns>True si la matrícula está completa, false en caso contrario</returns>
        public static bool ValidarMatriculaCompleta(string dni)
        {
            var matricula = BuscarMatriculaPorDni(dni);
            return matricula != null && matricula.EstaCompleta();
        }

        /// <summary>
        /// Elimina completamente la matrícula de un estudiante del sistema.
        /// Establece la matrícula actual del usuario como null.
        /// </summary>
        /// <param name="dni">DNI del estudiante cuya matrícula se eliminará</param>
        public static void EliminarMatricula(string dni)
        {
            // Validar entrada de DNI
            if (!ControladorUsuario.ValidarDni(dni))
                return;

            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            if (usuario != null)
            {
                usuario.MatriculaActual = null;
                ControladorUsuario.ActualizarUsuario(usuario);
            }
        }

        // Métodos de validación específicos para matrícula

        /// <summary>
        /// Valida que un grado académico sea válido y esté disponible en la universidad.
        /// Compara contra la lista de grados disponibles del sistema.
        /// </summary>
        /// <param name="grado">Nombre del grado académico a validar</param>
        /// <returns>True si el grado es válido, false en caso contrario</returns>
        public static bool ValidarGrado(string grado)
        {
            if (string.IsNullOrWhiteSpace(grado))
                return false;

            var gradosDisponibles = ObtenerGradosDisponibles();
            return gradosDisponibles.Contains(grado.Trim());
        }

        /// <summary>
        /// Valida que un ID de materia sea válido y corresponda a una materia existente.
        /// Verifica que el ID sea positivo y exista en la lista de materias disponibles.
        /// </summary>
        /// <param name="idMateria">ID de la materia a validar</param>
        /// <returns>True si el ID es válido, false en caso contrario</returns>
        public static bool ValidarIdMateria(int idMateria)
        {
            if (idMateria <= 0)
                return false;

            var materiasDisponibles = Materia.ObtenerMateriasDisponibles();
            return materiasDisponibles.Any(m => m.Id == idMateria);
        }

        /// <summary>
        /// Valida una lista completa de materias para una matrícula.
        /// Verifica que contenga exactamente 6 materias, sin repeticiones y que todas existan.
        /// </summary>
        /// <param name="materias">Lista de materias a validar</param>
        /// <returns>True si la lista es válida, false en caso contrario</returns>
        public static bool ValidarListaMaterias(List<Materia> materias)
        {
            if (materias == null)
                return false;

            // Debe tener exactamente 6 materias
            if (materias.Count != 6)
                return false;

            // Validar que no haya materias repetidas
            var idsUnicos = materias.Select(m => m.Id).Distinct().Count();
            if (idsUnicos != 6)
                return false;

            // Validar que todas las materias existan
            var materiasDisponibles = Materia.ObtenerMateriasDisponibles();
            foreach (var materia in materias)
            {
                if (!materiasDisponibles.Any(m => m.Id == materia.Id))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Realiza una validación completa de todos los datos de una matrícula.
        /// Verifica DNI del estudiante, existencia del usuario, validez del grado,
        /// materias seleccionadas, fechas y otros requisitos del sistema.
        /// </summary>
        /// <param name="matricula">Objeto Matricula con los datos a validar</param>
        /// <returns>String con los errores encontrados separados por saltos de línea, o null si no hay errores</returns>
        public static string ValidarDatosMatricula(Matricula matricula)
        {
            var errores = new List<string>();

            if (matricula == null)
            {
                errores.Add("La matrícula no puede ser nula.");
                return string.Join("\n", errores);
            }

            // Validar DNI del estudiante
            if (!ControladorUsuario.ValidarDni(matricula.DniEstudiante))
                errores.Add("El DNI del estudiante debe tener exactamente 8 dígitos y no puede ser una secuencia repetida.");

            // Validar que el usuario exista
            if (!ControladorUsuario.ExisteUsuario(matricula.DniEstudiante))
                errores.Add("No existe un usuario registrado con este DNI.");

            // Validar grado
            if (!ValidarGrado(matricula.Grado))
                errores.Add("El grado seleccionado no es válido.");

            // Validar materias si están seleccionadas
            if (matricula.MateriasSeleccionadas != null && matricula.MateriasSeleccionadas.Count > 0)
            {
                // Validar que no exceda 6 materias
                if (matricula.MateriasSeleccionadas.Count > 6)
                    errores.Add("No se pueden seleccionar más de 6 materias.");

                // Validar que no haya materias repetidas
                var idsUnicos = matricula.MateriasSeleccionadas.Select(m => m.Id).Distinct().Count();
                if (idsUnicos != matricula.MateriasSeleccionadas.Count)
                    errores.Add("No se pueden seleccionar materias repetidas.");

                // Validar que todas las materias existan
                var materiasDisponibles = Materia.ObtenerMateriasDisponibles();
                foreach (var materia in matricula.MateriasSeleccionadas)
                {
                    if (!materiasDisponibles.Any(m => m.Id == materia.Id))
                    {
                        errores.Add($"La materia con ID {materia.Id} no existe.");
                        break;
                    }
                }
            }

            // Validar fecha de matrícula
            if (matricula.FechaMatricula > DateTime.Now)
                errores.Add("La fecha de matrícula no puede ser futura.");

            if (matricula.FechaMatricula < DateTime.Now.AddYears(-1))
                errores.Add("La fecha de matrícula no puede ser anterior a un año.");

            return errores.Count > 0 ? string.Join("\n", errores) : null;
        }
    }
}
