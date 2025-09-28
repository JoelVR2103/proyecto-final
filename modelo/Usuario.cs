using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Usuario
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string Dni { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public Matricula MatriculaActual { get; set; }

        public Usuario() { }

        public Usuario(string nombres, string apellidos, int edad, string sexo, string dni, string telefono, string email, string direccion)
        {
            Nombres = nombres;
            Apellidos = apellidos;
            Edad = edad;
            Sexo = sexo;
            Dni = dni;
            Telefono = telefono;
            Email = email;
            Direccion = direccion;
            MatriculaActual = null;
        }

        public bool TieneMatricula()
        {
            return MatriculaActual != null;
        }

        public string ObtenerNombreCompleto()
        {
            return $"{Nombres} {Apellidos}";
        }
    }
}
