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
            var usuarioExistente = BuscarUsuarioPorDni(usuarioActualizado.Dni);
            if (usuarioExistente != null)
            {
                usuarioExistente.Nombres = usuarioActualizado.Nombres;
                usuarioExistente.Apellidos = usuarioActualizado.Apellidos;
                usuarioExistente.Edad = usuarioActualizado.Edad;
                usuarioExistente.Sexo = usuarioActualizado.Sexo;
                usuarioExistente.Telefono = usuarioActualizado.Telefono;
                usuarioExistente.Email = usuarioActualizado.Email;
                usuarioExistente.Direccion = usuarioActualizado.Direccion;
                usuarioExistente.Grado = usuarioActualizado.Grado;
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
    }
}
