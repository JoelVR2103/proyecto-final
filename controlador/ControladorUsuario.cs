using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using modelo;

namespace controlador
{
    public class ControladorUsuario
    {
        // Lista de usuarios ficticios para simular base de datos
        private static List<Usuario> usuariosRegistrados = new List<Usuario>
        {
            new Usuario("Pedro", "Perez", 25, "Masculino", "74758787", "987654321", "pedro@email.com", "Av. Principal 123", "Ingeniería"),
            new Usuario("Maria", "Garcia", 22, "Femenino", "12345678", "912345678", "maria@email.com", "Jr. Secundario 456", "Medicina"),
            new Usuario("Juan", "Lopez", 24, "Masculino", "87654321", "923456789", "juan@email.com", "Calle Tercera 789", "Derecho")
        };

        public static Usuario BuscarUsuarioPorDni(string dni)
        {
            return usuariosRegistrados.FirstOrDefault(u => u.Dni == dni);
        }

        public static bool ExisteUsuario(string dni)
        {
            return BuscarUsuarioPorDni(dni) != null;
        }

        public static void RegistrarUsuario(Usuario nuevoUsuario)
        {
            usuariosRegistrados.Add(nuevoUsuario);
        }

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

        public static bool ValidarDni(string dni)
        {
            return !string.IsNullOrEmpty(dni) && dni.Length == 8 && dni.All(char.IsDigit);
        }

        public static bool ValidarEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }

        public static bool ValidarTelefono(string telefono)
        {
            return !string.IsNullOrEmpty(telefono) && telefono.Length >= 9 && telefono.All(char.IsDigit);
        }

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

        public static bool ValidarEdad(int edad)
        {
            // Edad debe estar entre 16 y 100 años
            return edad >= 16 && edad <= 100;
        }

        public static bool ValidarSexo(string sexo)
        {
            if (string.IsNullOrWhiteSpace(sexo))
                return false;

            sexo = sexo.Trim().ToUpper();

            // Solo acepta exactamente M, F, MASCULINO o FEMENINO
            return sexo == "M" || sexo == "F" || sexo == "MASCULINO" || sexo == "FEMENINO";
        }

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
    }
}
