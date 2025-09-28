using modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace controlador
{
    public class ControladorMatricula
    {
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
        public static Matricula BuscarMatriculaPorDni(string dni)
        {
            var usuario = ControladorUsuario.BuscarUsuarioPorDni(dni);
            return usuario?.MatriculaActual;
        }

        public static bool TieneMatricula(string dni)
        {
            return BuscarMatriculaPorDni(dni) != null;
        }

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

    }
}
