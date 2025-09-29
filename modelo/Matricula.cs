using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    /// <summary>
    /// Enumeración que define los turnos disponibles para la matrícula.
    /// Permite seleccionar entre horarios de mañana y tarde para las clases.
    /// </summary>
    public enum Turno
    {
        /// <summary>
        /// Turno de mañana - Clases en horario matutino
        /// </summary>
        Mañana,

        /// <summary>
        /// Turno de tarde - Clases en horario vespertino
        /// </summary>
        Tarde
    }

    /// <summary>
    /// Clase que representa la matrícula de un estudiante en el sistema universitario.
    /// Contiene toda la información sobre el grado, turno, materias seleccionadas y fecha de matrícula.
    /// Implementa la lógica de negocio para la gestión de materias y validaciones de matrícula.
    /// </summary>
    public class Matricula
    {
        #region Propiedades - Información de la Matrícula

        /// <summary>
        /// DNI del estudiante propietario de esta matrícula.
        /// Debe coincidir con el DNI del usuario registrado en el sistema.
        /// </summary>
        public string DniEstudiante { get; set; }

        /// <summary>
        /// Grado académico seleccionado para la matrícula.
        /// Ejemplos: "Administración", "Ingeniería", "Medicina", etc.
        /// </summary>
        public string Grado { get; set; }

        /// <summary>
        /// Turno seleccionado para las clases (Mañana o Tarde).
        /// Determina el horario en que el estudiante tomará sus materias.
        /// </summary>
        public Turno TurnoSeleccionado { get; set; }

        /// <summary>
        /// Lista de materias seleccionadas por el estudiante.
        /// Máximo 6 materias por matrícula. No puede haber materias duplicadas.
        /// </summary>
        public List<Materia> MateriasSeleccionadas { get; set; }

        /// <summary>
        /// Fecha y hora en que se realizó la matrícula.
        /// Se asigna automáticamente al momento de crear la matrícula.
        /// </summary>
        public DateTime FechaMatricula { get; set; }

        /// <summary>
        /// Indica si la selección de materias fue automática o manual.
        /// true: Selección automática por el sistema
        /// false: Selección manual por el estudiante
        /// </summary>
        public bool EsSeleccionAutomatica { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por defecto sin parámetros.
        /// Inicializa la lista de materias vacía y establece la fecha actual.
        /// Se usa principalmente para deserialización JSON.
        /// </summary>
        public Matricula()
        {
            MateriasSeleccionadas = new List<Materia>();
            FechaMatricula = DateTime.Now;
        }

        /// <summary>
        /// Constructor básico que inicializa una matrícula con información mínima.
        /// La selección se marca como manual por defecto.
        /// </summary>
        /// <param name="dniEstudiante">DNI del estudiante (8 dígitos)</param>
        /// <param name="grado">Grado académico seleccionado</param>
        /// <param name="turno">Turno de clases (Mañana o Tarde)</param>
        public Matricula(string dniEstudiante, string grado, Turno turno)
        {
            DniEstudiante = dniEstudiante;
            Grado = grado;
            TurnoSeleccionado = turno;
            MateriasSeleccionadas = new List<Materia>();
            FechaMatricula = DateTime.Now;
            EsSeleccionAutomatica = false; // Por defecto es selección manual
        }

        /// <summary>
        /// Constructor que inicializa una matrícula con materias predefinidas.
        /// Útil cuando se tiene una lista de materias ya seleccionadas.
        /// </summary>
        /// <param name="dniEstudiante">DNI del estudiante (8 dígitos)</param>
        /// <param name="grado">Grado académico seleccionado</param>
        /// <param name="turno">Turno de clases (Mañana o Tarde)</param>
        /// <param name="materias">Lista de materias preseleccionadas (máximo 6)</param>
        public Matricula(string dniEstudiante, string grado, Turno turno, List<Materia> materias)
        {
            DniEstudiante = dniEstudiante;
            Grado = grado;
            TurnoSeleccionado = turno;
            MateriasSeleccionadas = materias ?? new List<Materia>(); // Protección contra null
            FechaMatricula = DateTime.Now;
            EsSeleccionAutomatica = false; // Por defecto es selección manual
        }

        /// <summary>
        /// Constructor completo que permite especificar todos los parámetros de la matrícula.
        /// Incluye la opción de marcar si la selección fue automática o manual.
        /// </summary>
        /// <param name="dniEstudiante">DNI del estudiante (8 dígitos)</param>
        /// <param name="grado">Grado académico seleccionado</param>
        /// <param name="turno">Turno de clases (Mañana o Tarde)</param>
        /// <param name="materias">Lista de materias seleccionadas (máximo 6)</param>
        /// <param name="esSeleccionAutomatica">true si fue selección automática, false si fue manual</param>
        public Matricula(string dniEstudiante, string grado, Turno turno, List<Materia> materias, bool esSeleccionAutomatica)
        {
            DniEstudiante = dniEstudiante;
            Grado = grado;
            TurnoSeleccionado = turno;
            MateriasSeleccionadas = materias ?? new List<Materia>(); // Protección contra null
            FechaMatricula = DateTime.Now;
            EsSeleccionAutomatica = esSeleccionAutomatica;
        }

        #endregion

        #region Métodos de Gestión de Materias

        /// <summary>
        /// Agrega una nueva materia a la matrícula si es posible.
        /// Valida que no se exceda el límite de 6 materias y que no haya duplicados.
        /// </summary>
        /// <param name="materia">Materia a agregar a la matrícula</param>
        /// <returns>
        /// true: La materia se agregó exitosamente
        /// false: No se pudo agregar (límite excedido o materia duplicada)
        /// </returns>
        public bool AgregarMateria(Materia materia)
        {
            // Validar que no exceda el límite máximo de 6 materias
            if (MateriasSeleccionadas.Count >= 6)
                return false;

            // Validar que no se repita la materia (por ID)
            if (MateriasSeleccionadas.Any(m => m.Id == materia.Id))
                return false;

            // Agregar la materia a la lista
            MateriasSeleccionadas.Add(materia);
            return true;
        }

        /// <summary>
        /// Remueve una materia de la matrícula basándose en su ID.
        /// Permite al estudiante quitar materias que ya no desea cursar.
        /// </summary>
        /// <param name="idMateria">ID único de la materia a remover</param>
        public void RemoverMateria(int idMateria)
        {
            // Remover todas las materias que coincidan con el ID (debería ser solo una)
            MateriasSeleccionadas.RemoveAll(m => m.Id == idMateria);
        }

        #endregion

        #region Métodos de Consulta y Validación

        /// <summary>
        /// Calcula el total de créditos de todas las materias seleccionadas.
        /// Se usa para verificar la carga académica del estudiante.
        /// </summary>
        /// <returns>Suma total de créditos de todas las materias seleccionadas</returns>
        public int TotalCreditos()
        {
            return MateriasSeleccionadas.Sum(m => m.Creditos);
        }

        /// <summary>
        /// Verifica si la matrícula está completa (tiene exactamente 6 materias).
        /// Una matrícula completa permite al estudiante proceder con la inscripción.
        /// </summary>
        /// <returns>
        /// true: La matrícula tiene exactamente 6 materias (completa)
        /// false: La matrícula tiene menos de 6 materias (incompleta)
        /// </returns>
        public bool EstaCompleta()
        {
            return MateriasSeleccionadas.Count == 6;
        }

        #endregion

        #region Métodos de Presentación

        /// <summary>
        /// Genera un resumen completo de la matrícula en formato de texto.
        /// Incluye información del grado, turno, fecha, tipo de selección y lista de materias.
        /// Se usa para mostrar la información completa al usuario.
        /// </summary>
        /// <returns>
        /// String formateado con toda la información de la matrícula:
        /// - Datos generales (grado, turno, fecha, tipo de selección)
        /// - Estadísticas (total de materias y créditos)
        /// - Lista numerada de todas las materias seleccionadas
        /// </returns>
        public string ObtenerResumenMatricula()
        {
            var sb = new StringBuilder();

            // Encabezado del resumen
            sb.AppendLine($"=== RESUMEN DE MATRÍCULA ===");

            // Información general de la matrícula
            sb.AppendLine($"Grado: {Grado}");
            sb.AppendLine($"Turno: {TurnoSeleccionado}");
            sb.AppendLine($"Fecha de Matrícula: {FechaMatricula:dd/MM/yyyy}");
            sb.AppendLine($"Tipo de Selección: {(EsSeleccionAutomatica ? "Automática" : "Manual")}");

            // Estadísticas de la matrícula
            sb.AppendLine($"Total de Materias: {MateriasSeleccionadas.Count}/6");
            sb.AppendLine($"Total de Créditos: {TotalCreditos()}");
            sb.AppendLine();

            // Lista de materias seleccionadas
            sb.AppendLine("MATERIAS SELECCIONADAS:");

            // Enumerar cada materia con su número de orden
            for (int i = 0; i < MateriasSeleccionadas.Count; i++)
            {
                sb.AppendLine($"{i + 1}. {MateriasSeleccionadas[i]}");
            }

            return sb.ToString();
        }

        #endregion
    }
}
