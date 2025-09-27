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
                Console.WriteLine($"¡Bienvenido {usuarioExistente.Nombres} {usuarioExistente.Apellidos}!");
                Console.WriteLine("¿Quieres actualizar tus datos? (si/no):");

                string respuesta = Console.ReadLine().ToLower();
                if (respuesta == "si" || respuesta == "s")
                {
                    ActualizarDatosUsuario(usuarioExistente);
                }
                else
                {
                    Console.WriteLine("Continuando con los datos actuales...");
                }
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

            Console.Write("Ingresa tus apellidos: ");
            string apellidos = Console.ReadLine();

            Console.Write("Ingresa tu edad: ");
            int edad;
            while (!int.TryParse(Console.ReadLine(), out edad) || edad <= 0)
            {
                Console.Write("Edad inválida. Ingresa un número válido: ");
            }

            Console.Write("Ingresa tu sexo (Masculino/Femenino): ");
            string sexo = Console.ReadLine();

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

            Console.Write("Ingresa tu grado de estudios: ");
            string grado = Console.ReadLine();

            // Crear y registrar nuevo usuario
            Usuario nuevoUsuario = new Usuario(nombres, apellidos, edad, sexo, dni, telefono, email, direccion, grado);
            ControladorUsuario.RegistrarUsuario(nuevoUsuario);

            Console.WriteLine($"\n¡Bienvenido {nombres} {apellidos}! Tu registro ha sido exitoso.");
        }

        private static void ActualizarDatosUsuario(Usuario usuario)
        {
            Console.WriteLine("\n=== ACTUALIZACIÓN DE DATOS ===");
            Console.WriteLine("Presiona Enter para mantener el valor actual o ingresa el nuevo valor:");

            Console.Write($"Nombres ({usuario.Nombres}): ");
            string nuevosNombres = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevosNombres))
                usuario.Nombres = nuevosNombres;

            Console.Write($"Apellidos ({usuario.Apellidos}): ");
            string nuevosApellidos = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevosApellidos))
                usuario.Apellidos = nuevosApellidos;

            Console.Write($"Edad ({usuario.Edad}): ");
            string nuevaEdadStr = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaEdadStr) && int.TryParse(nuevaEdadStr, out int nuevaEdad))
                usuario.Edad = nuevaEdad;

            Console.Write($"Sexo ({usuario.Sexo}): ");
            string nuevoSexo = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoSexo))
                usuario.Sexo = nuevoSexo;

            Console.Write($"Teléfono ({usuario.Telefono}): ");
            string nuevoTelefono = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoTelefono) && ControladorUsuario.ValidarTelefono(nuevoTelefono))
                usuario.Telefono = nuevoTelefono;

            Console.Write($"Email ({usuario.Email}): ");
            string nuevoEmail = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoEmail) && ControladorUsuario.ValidarEmail(nuevoEmail))
                usuario.Email = nuevoEmail;

            Console.Write($"Dirección ({usuario.Direccion}): ");
            string nuevaDireccion = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaDireccion))
                usuario.Direccion = nuevaDireccion;

            Console.Write($"Grado ({usuario.Grado}): ");
            string nuevoGrado = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoGrado))
                usuario.Grado = nuevoGrado;

            ControladorUsuario.ActualizarUsuario(usuario);
            Console.WriteLine("\n¡Datos actualizados correctamente!");
        }
    }
}
