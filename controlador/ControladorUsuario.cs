using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;
using Newtonsoft.Json;

namespace controlador
{
    /// <summary>
    /// Controlador principal para la gestión de usuarios del sistema de matrícula universitaria.
    /// Proporciona funcionalidades para el registro, búsqueda, actualización y validación de usuarios.
    /// Utiliza persistencia en archivo JSON para almacenar la información de los usuarios.
    /// </summary>
    public class ControladorUsuario
    {
        /// <summary>
        /// Ruta del archivo JSON donde se almacenan los datos de los usuarios.
        /// Se ubica en la carpeta modelo del proyecto.
        /// </summary>
        private static readonly string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "modelo", "usuarios.json");

        /// <summary>
        /// Carga la lista de usuarios desde el archivo JSON.
        /// Si el archivo no existe o hay errores de lectura, retorna una lista vacía.
        /// </summary>
        /// <returns>Lista de usuarios cargados desde el archivo JSON</returns>
        private static List<Usuario> CargarUsuarios()
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    string json = File.ReadAllText(rutaArchivo);
                    return JsonConvert.DeserializeObject<List<Usuario>>(json) ?? new List<Usuario>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuarios: {ex.Message}");
            }
            return new List<Usuario>();
        }

        /// <summary>
        /// Guarda la lista de usuarios en el archivo JSON.
        /// Crea el directorio si no existe y maneja errores de escritura.
        /// </summary>
        /// <param name="usuarios">Lista de usuarios a guardar en el archivo</param>
        private static void GuardarUsuarios(List<Usuario> usuarios)
        {
            try
            {
                string directorio = Path.GetDirectoryName(rutaArchivo);
                if (!Directory.Exists(directorio))
                {
                    Directory.CreateDirectory(directorio);
                }

                string json = JsonConvert.SerializeObject(usuarios, Formatting.Indented);
                File.WriteAllText(rutaArchivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar usuarios: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca un usuario específico por su número de DNI.
        /// </summary>
        /// <param name="dni">Número de DNI del usuario a buscar</param>
        /// <returns>El usuario encontrado o null si no existe</returns>
        public static Usuario BuscarUsuarioPorDni(string dni)
        {
            var usuarios = CargarUsuarios();
            return usuarios.FirstOrDefault(u => u.Dni == dni);
        }

        /// <summary>
        /// Verifica si existe un usuario registrado con el DNI especificado.
        /// </summary>
        /// <param name="dni">Número de DNI a verificar</param>
        /// <returns>True si el usuario existe, false en caso contrario</returns>
        public static bool ExisteUsuario(string dni)
        {
            return BuscarUsuarioPorDni(dni) != null;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// Agrega el usuario a la lista existente y guarda los cambios en el archivo JSON.
        /// </summary>
        /// <param name="nuevoUsuario">Objeto Usuario con los datos del nuevo usuario a registrar</param>
        public static void RegistrarUsuario(Usuario nuevoUsuario)
        {
            var usuarios = CargarUsuarios();
            usuarios.Add(nuevoUsuario);
            GuardarUsuarios(usuarios);
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente en el sistema.
        /// Busca el usuario por DNI y actualiza todos sus campos con la nueva información.
        /// </summary>
        /// <param name="usuarioActualizado">Objeto Usuario con los datos actualizados</param>
        public static void ActualizarUsuario(Usuario usuarioActualizado)
        {
            var usuarios = CargarUsuarios();
            var usuarioExistente = usuarios.FirstOrDefault(u => u.Dni == usuarioActualizado.Dni);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombres = usuarioActualizado.Nombres;
                usuarioExistente.Apellidos = usuarioActualizado.Apellidos;
                usuarioExistente.Edad = usuarioActualizado.Edad;
                usuarioExistente.Sexo = usuarioActualizado.Sexo;
                usuarioExistente.Telefono = usuarioActualizado.Telefono;
                usuarioExistente.Email = usuarioActualizado.Email;
                usuarioExistente.Direccion = usuarioActualizado.Direccion;
                usuarioExistente.MatriculaActual = usuarioActualizado.MatriculaActual;
                GuardarUsuarios(usuarios);
            }
        }

        /// <summary>
        /// Valida el formato y contenido de un número de DNI peruano.
        /// Verifica que tenga exactamente 8 dígitos y no sea una secuencia repetida.
        /// </summary>
        /// <param name="dni">Número de DNI a validar</param>
        /// <returns>True si el DNI es válido, false en caso contrario</returns>
        public static bool ValidarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni))
                return false;

            // Remover espacios y guiones
            dni = dni.Replace(" ", "").Replace("-", "");

            // Debe tener exactamente 8 dígitos
            if (dni.Length != 8 || !dni.All(char.IsDigit))
                return false;

            // Validar que no sean todos números iguales (00000000, 11111111, etc.)
            if (dni.All(c => c == dni[0]))
                return false;

            return true;
        }

        /// <summary>
        /// Valida el formato de una dirección de correo electrónico.
        /// Verifica estructura básica, presencia de @ y dominio, y caracteres permitidos.
        /// </summary>
        /// <param name="email">Dirección de email a validar</param>
        /// <returns>True si el email es válido, false en caso contrario</returns>
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim().ToLower();

            // Validación básica de formato
            if (!email.Contains("@") || !email.Contains("."))
                return false;

            // No debe empezar o terminar con @ o .
            if (email.StartsWith("@") || email.StartsWith(".") ||
                email.EndsWith("@") || email.EndsWith("."))
                return false;

            // Debe tener al menos un carácter antes del @
            var partes = email.Split('@');
            if (partes.Length != 2 || partes[0].Length == 0 || partes[1].Length == 0)
                return false;

            // La parte del dominio debe tener al menos un punto
            if (!partes[1].Contains("."))
                return false;

            // Validar caracteres permitidos
            var caracteresPermitidos = "abcdefghijklmnopqrstuvwxyz0123456789@.-_";
            if (!email.All(c => caracteresPermitidos.Contains(c)))
                return false;

            return true;
        }

        /// <summary>
        /// Valida el formato de un número telefónico.
        /// Acepta formatos nacionales e internacionales (9-15 dígitos).
        /// Permite el prefijo + para números internacionales.
        /// </summary>
        /// <param name="telefono">Número de teléfono a validar</param>
        /// <returns>True si el teléfono es válido, false en caso contrario</returns>
        public static bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return false;

            // Remover espacios, guiones y paréntesis
            telefono = telefono.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");

            // Debe tener entre 9 y 15 dígitos (formato internacional)
            if (telefono.Length < 9 || telefono.Length > 15)
                return false;

            // Solo debe contener dígitos (puede empezar con +)
            if (telefono.StartsWith("+"))
            {
                telefono = telefono.Substring(1);
                if (telefono.Length < 9 || telefono.Length > 14)
                    return false;
            }

            if (!telefono.All(char.IsDigit))
                return false;

            // Validar que no sean todos números iguales
            if (telefono.All(c => c == telefono[0]))
                return false;

            return true;
        }

        /// <summary>
        /// Valida el formato de los nombres de una persona.
        /// Debe contener entre 2 y 50 caracteres, solo letras, espacios y acentos.
        /// No permite espacios al inicio, final o dobles.
        /// </summary>
        /// <param name="nombres">Nombres a validar</param>
        /// <returns>True si los nombres son válidos, false en caso contrario</returns>
        public static bool ValidarNombres(string nombres)
        {
            if (string.IsNullOrWhiteSpace(nombres))
                return false;

            nombres = nombres.Trim();

            // Debe tener entre 2 y 50 caracteres
            if (nombres.Length < 2 || nombres.Length > 50)
                return false;

            // Solo debe contener letras, espacios y acentos
            var caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzáéíóúüñABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ ";
            if (!nombres.All(c => caracteresPermitidos.Contains(c)))
                return false;

            // No debe empezar o terminar con espacio
            if (nombres.StartsWith(" ") || nombres.EndsWith(" "))
                return false;

            // No debe tener espacios dobles
            if (nombres.Contains("  "))
                return false;

            return true;
        }

        /// <summary>
        /// Valida el formato de los apellidos de una persona.
        /// Debe contener entre 2 y 50 caracteres, solo letras, espacios y acentos.
        /// No permite espacios al inicio, final o dobles.
        /// </summary>
        /// <param name="apellidos">Apellidos a validar</param>
        /// <returns>True si los apellidos son válidos, false en caso contrario</returns>
        public static bool ValidarApellidos(string apellidos)
        {
            if (string.IsNullOrWhiteSpace(apellidos))
                return false;

            apellidos = apellidos.Trim();

            // Debe tener entre 2 y 50 caracteres
            if (apellidos.Length < 2 || apellidos.Length > 50)
                return false;

            // Solo debe contener letras, espacios y acentos
            var caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzáéíóúüñABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ ";
            if (!apellidos.All(c => caracteresPermitidos.Contains(c)))
                return false;

            // No debe empezar o terminar con espacio
            if (apellidos.StartsWith(" ") || apellidos.EndsWith(" "))
                return false;

            // No debe tener espacios dobles
            if (apellidos.Contains("  "))
                return false;

            return true;
        }

        /// <summary>
        /// Valida que la edad esté dentro del rango permitido para estudiantes universitarios.
        /// La edad debe estar entre 16 y 100 años.
        /// </summary>
        /// <param name="edad">Edad a validar</param>
        /// <returns>True si la edad es válida, false en caso contrario</returns>
        public static bool ValidarEdad(int edad)
        {
            // Edad debe estar entre 16 y 100 años
            return edad >= 16 && edad <= 100;
        }

        /// <summary>
        /// Valida el sexo/género del usuario.
        /// Acepta los valores: "M", "F", "MASCULINO" o "FEMENINO" (no sensible a mayúsculas).
        /// </summary>
        /// <param name="sexo">Sexo/género a validar</param>
        /// <returns>True si el sexo es válido, false en caso contrario</returns>
        public static bool ValidarSexo(string sexo)
        {
            if (string.IsNullOrWhiteSpace(sexo))
                return false;

            sexo = sexo.Trim().ToUpper();

            // Solo acepta exactamente M, F, MASCULINO o FEMENINO
            return sexo == "M" || sexo == "F" || sexo == "MASCULINO" || sexo == "FEMENINO";
        }

        /// <summary>
        /// Valida el formato de una dirección física.
        /// Debe contener entre 10 y 200 caracteres y solo caracteres alfanuméricos,
        /// espacios y algunos símbolos especiales (.,#-°).
        /// </summary>
        /// <param name="direccion">Dirección a validar</param>
        /// <returns>True si la dirección es válida, false en caso contrario</returns>
        public static bool ValidarDireccion(string direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion))
                return false;

            direccion = direccion.Trim();

            // Debe tener entre 10 y 200 caracteres
            if (direccion.Length < 10 || direccion.Length > 200)
                return false;

            // Solo debe contener letras, números, espacios y algunos caracteres especiales
            var caracteresPermitidos = "abcdefghijklmnopqrstuvwxyzáéíóúüñABCDEFGHIJKLMNOPQRSTUVWXYZÁÉÍÓÚÜÑ0123456789 .,#-°";
            if (!direccion.All(c => caracteresPermitidos.Contains(c)))
                return false;

            return true;
        }

        /// <summary>
        /// Realiza una validación completa de todos los datos de un usuario.
        /// Verifica nombres, apellidos, edad, sexo, DNI, teléfono, email y dirección.
        /// También verifica que el DNI no esté duplicado en el sistema.
        /// </summary>
        /// <param name="usuario">Usuario con los datos a validar</param>
        /// <returns>String con los errores encontrados separados por saltos de línea, o null si no hay errores</returns>
        public static string ValidarDatosCompletos(Usuario usuario)
        {
            var errores = new List<string>();

            if (!ValidarNombres(usuario.Nombres))
                errores.Add("Los nombres deben tener entre 2 y 50 caracteres y solo contener letras.");

            if (!ValidarApellidos(usuario.Apellidos))
                errores.Add("Los apellidos deben tener entre 2 y 50 caracteres y solo contener letras.");

            if (!ValidarEdad(usuario.Edad))
                errores.Add("La edad debe estar entre 16 y 100 años.");

            if (!ValidarSexo(usuario.Sexo))
                errores.Add("El sexo debe ser 'M' (Masculino) o 'F' (Femenino).");

            if (!ValidarDni(usuario.Dni))
                errores.Add("El DNI debe tener exactamente 8 dígitos y no puede ser una secuencia repetida.");

            if (!ValidarTelefono(usuario.Telefono))
                errores.Add("El teléfono debe tener entre 9 y 15 dígitos.");

            if (!ValidarEmail(usuario.Email))
                errores.Add("El email debe tener un formato válido (ejemplo@dominio.com).");

            if (!ValidarDireccion(usuario.Direccion))
                errores.Add("La dirección debe tener entre 10 y 200 caracteres.");

            // Verificar si el DNI ya existe
            if (ExisteUsuario(usuario.Dni))
                errores.Add("Ya existe un usuario registrado con este DNI.");

            return errores.Count > 0 ? string.Join("\n", errores) : null;
        }
    }
}

