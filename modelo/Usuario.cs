using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    /// <summary>
    /// Clase que representa a un usuario del sistema de matrícula universitaria.
    /// Esta clase almacena toda la información personal del estudiante y su matrícula actual.
    /// Implementa el patrón de modelo de datos para la gestión de usuarios.
    /// </summary>
    public class Usuario
    {
        #region Propiedades - Información Personal del Usuario

        /// <summary>
        /// Nombres del usuario. Debe contener solo letras y espacios, mínimo 2 caracteres.
        /// Ejemplo: "Juan Carlos", "María"
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Apellidos del usuario. Debe contener solo letras y espacios, mínimo 2 caracteres.
        /// Ejemplo: "García López", "Rodríguez"
        /// </summary>
        public string Apellidos { get; set; }

        /// <summary>
        /// Edad del usuario en años. Debe estar entre 16 y 100 años para ser válida.
        /// Se usa para verificar que el estudiante tenga la edad mínima para estudiar.
        /// </summary>
        public int Edad { get; set; }

        /// <summary>
        /// Sexo del usuario. Solo acepta los valores exactos "Masculino" o "Femenino".
        /// Se usa para estadísticas y reportes demográficos del sistema.
        /// </summary>
        public string Sexo { get; set; }

        /// <summary>
        /// Documento Nacional de Identidad del usuario. Debe tener exactamente 8 dígitos.
        /// Es el identificador único del usuario en el sistema. Ejemplo: "12345678"
        /// </summary>
        public string Dni { get; set; }

        /// <summary>
        /// Número de teléfono del usuario. Debe tener al menos 9 dígitos.
        /// Se usa para contactar al estudiante en caso de emergencias o notificaciones.
        /// Ejemplo: "987654321", "01234567890"
        /// </summary>
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección de correo electrónico del usuario. Debe tener formato válido de email.
        /// Se usa para enviar notificaciones, confirmaciones y comunicaciones oficiales.
        /// Ejemplo: "usuario@ejemplo.com"
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Dirección física del usuario. Debe tener al menos 5 caracteres.
        /// Se usa para correspondencia física y verificación de residencia.
        /// Ejemplo: "Av. Principal 123, Lima"
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Matrícula actual del usuario. Puede ser null si el usuario no tiene matrícula activa.
        /// Contiene toda la información sobre el grado, turno, materias y estado de matrícula.
        /// </summary>
        public Matricula MatriculaActual { get; set; }

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor por defecto sin parámetros.
        /// Inicializa un usuario vacío con todas las propiedades en sus valores por defecto.
        /// Se usa principalmente para deserialización JSON y creación de objetos temporales.
        /// </summary>
        public Usuario() { }

        /// <summary>
        /// Constructor parametrizado que inicializa un usuario con todos sus datos personales.
        /// La matrícula se inicializa como null y debe asignarse posteriormente.
        /// </summary>
        /// <param name="nombres">Nombres del usuario (validado externamente)</param>
        /// <param name="apellidos">Apellidos del usuario (validado externamente)</param>
        /// <param name="edad">Edad del usuario en años (validado externamente)</param>
        /// <param name="sexo">Sexo del usuario: "Masculino" o "Femenino" (validado externamente)</param>
        /// <param name="dni">DNI de 8 dígitos del usuario (validado externamente)</param>
        /// <param name="telefono">Teléfono del usuario (validado externamente)</param>
        /// <param name="email">Email válido del usuario (validado externamente)</param>
        /// <param name="direccion">Dirección física del usuario (validado externamente)</param>
        public Usuario(string nombres, string apellidos, int edad, string sexo, string dni, string telefono, string email, string direccion)
        {
            // Asignación directa de propiedades - las validaciones se realizan en el controlador
            Nombres = nombres;
            Apellidos = apellidos;
            Edad = edad;
            Sexo = sexo;
            Dni = dni;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;

            // La matrícula se inicializa como null - se asigna cuando el usuario se matricula
            MatriculaActual = null;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Verifica si el usuario tiene una matrícula activa asignada.
        /// Este método es fundamental para determinar el flujo de la aplicación.
        /// </summary>
        /// <returns>
        /// true: El usuario tiene una matrícula activa (puede gestionar materias, ver resumen, etc.)
        /// false: El usuario no tiene matrícula (debe crear una nueva matrícula)
        /// </returns>
        public bool TieneMatricula()
        {
            return MatriculaActual != null;
        }

        /// <summary>
        /// Obtiene el nombre completo del usuario concatenando nombres y apellidos.
        /// Se usa para mostrar información personalizada en la interfaz de usuario.
        /// </summary>
        /// <returns>
        /// String con el formato "Nombres Apellidos"
        /// Ejemplo: "Juan Carlos García López"
        /// </returns>
        public string ObtenerNombreCompleto()
        {
            return $"{Nombres} {Apellidos}";
        }

        #endregion
    }
}
