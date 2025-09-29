using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    /// <summary>
    /// Clase que representa una materia académica en el sistema universitario.
    /// Contiene información básica de cada materia como nombre, código y créditos.
    /// Proporciona el catálogo completo de materias disponibles para matrícula.
    /// </summary>
    public class Materia
    {
        #region Propiedades - Información de la Materia

        /// <summary>
        /// Identificador único de la materia.
        /// Se usa para referenciar y gestionar materias de forma única en el sistema.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre completo de la materia.
        /// Ejemplo: "Matemática I", "Programación I", "Historia Universal"
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Código alfanumérico único de la materia.
        /// Formato estándar: 3 letras + 3 números (ej: "MAT101", "PRG101")
        /// Se usa para identificación rápida y referencias académicas.
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Número de créditos académicos que otorga la materia.
        /// Determina el peso académico y la carga de trabajo de la materia.
        /// Rango típico: 2-4 créditos por materia.
        /// </summary>
        public int Creditos { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por defecto sin parámetros.
        /// Inicializa una materia vacía con valores por defecto.
        /// Se usa principalmente para deserialización JSON y creación temporal.
        /// </summary>
        public Materia() { }

        /// <summary>
        /// Constructor parametrizado que inicializa una materia con todos sus datos.
        /// Se usa para crear materias con información completa y validada.
        /// </summary>
        /// <param name="id">Identificador único de la materia</param>
        /// <param name="nombre">Nombre completo de la materia</param>
        /// <param name="codigo">Código alfanumérico único (formato: XXX###)</param>
        /// <param name="creditos">Número de créditos académicos (2-4 típicamente)</param>
        public Materia(int id, string nombre, string codigo, int creditos)
        {
            Id = id;
            Nombre = nombre;
            Codigo = codigo;
            Creditos = creditos;
        }

        #endregion

        #region Métodos Estáticos - Catálogo de Materias

        /// <summary>
        /// Obtiene la lista completa de las 10 materias disponibles en el sistema.
        /// Este es el catálogo oficial de materias que los estudiantes pueden seleccionar.
        /// Cada materia tiene un ID único, nombre descriptivo, código estándar y créditos asignados.
        /// </summary>
        /// <returns>
        /// Lista de 10 materias disponibles:
        /// 1. Matemática I (MAT101) - 4 créditos
        /// 2. Física I (FIS101) - 4 créditos  
        /// 3. Química General (QUI101) - 3 créditos
        /// 4. Programación I (PRG101) - 4 créditos
        /// 5. Inglés I (ING101) - 2 créditos
        /// 6. Historia Universal (HIS101) - 3 créditos
        /// 7. Filosofía (FIL101) - 2 créditos
        /// 8. Estadística (EST101) - 3 créditos
        /// 9. Comunicación (COM101) - 2 créditos
        /// 10. Metodología de Investigación (MET101) - 3 créditos
        /// </returns>
        public static List<Materia> ObtenerMateriasDisponibles()
        {
            return new List<Materia>
            {
                // Materias de Ciencias Exactas (4 créditos cada una)
                new Materia(1, "Matemática I", "MAT101", 4),
                new Materia(2, "Física I", "FIS101", 4),
                new Materia(4, "Programación I", "PRG101", 4),
                
                // Materias de Ciencias Naturales y Aplicadas (3 créditos cada una)
                new Materia(3, "Química General", "QUI101", 3),
                new Materia(6, "Historia Universal", "HIS101", 3),
                new Materia(8, "Estadística", "EST101", 3),
                new Materia(10, "Metodología de Investigación", "MET101", 3),
                
                // Materias de Humanidades y Comunicación (2 créditos cada una)
                new Materia(5, "Inglés I", "ING101", 2),
                new Materia(7, "Filosofía", "FIL101", 2),
                new Materia(9, "Comunicación", "COM101", 2)
            };
        }

        #endregion

        #region Métodos de Presentación

        /// <summary>
        /// Convierte la materia a su representación en texto para mostrar al usuario.
        /// Formato: "ID. Nombre (Código) - X créditos"
        /// Se usa en listas, menús y reportes para mostrar información completa de la materia.
        /// </summary>
        /// <returns>
        /// String formateado con toda la información de la materia.
        /// Ejemplo: "1. Matemática I (MAT101) - 4 créditos"
        /// </returns>
        public override string ToString()
        {
            return $"{Id}. {Nombre} ({Codigo}) - {Creditos} créditos";
        }

        #endregion
    }
}
