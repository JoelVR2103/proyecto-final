using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final
{
    /// <summary>
    /// Clase principal del programa que contiene el punto de entrada de la aplicación.
    /// Se encarga de inicializar el sistema de matrícula universitaria.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación del sistema de matrícula universitaria.
        /// Inicia el flujo del programa llamando al método iniciador que maneja toda la lógica de interfaz.
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados en esta aplicación)</param>
        static void Main(string[] args)
        {
            // Inicializar el sistema de matrícula universitaria
            Iniciador.iniciador();

            /* 
             * DOCUMENTACIÓN DEL FLUJO DEL SISTEMA:
             * 
             * MÓDULO DE LUCINDA - Gestión de Usuarios:
             * - Solicitud de DNI al usuario
             * - Si existe: dar bienvenida y preguntar si quiere actualizar datos
             * - Si no existe: proceso de registro completo
             * - Registro incluye: nombres, apellidos, edad, sexo, dni, telefono, email, direccion, grado
             * - Una vez registrado: dar bienvenida y proceder con matrícula
             * 
             * MÓDULO DE ANDREE - Sistema de Matrícula:
             * - Selección del grado de matrícula
             * - Elección del turno (mañana o tarde)
             * - Dos opciones para elegir materias:
             *   1. Manual: usuario elige sus materias
             *   2. Automático: sistema elige por el usuario
             * - Validación: máximo 6 materias, no repetir cursos
             * - Si es automático: sistema elige 6 cursos aleatoriamente
             * 
             * MÓDULO DE YACSON - Confirmación y Resumen:
             * - Mostrar resumen completo de la matrícula
             * - Preguntar si está correcto
             * - Si hay error: opción de volver a elegir materias
             * - Si está bien: confirmar matrícula
             * - Mensaje de confirmación final
             * - Imprimir datos completos de la matrícula
             * 
             * ARQUITECTURA - JOEL:
             * - Patrón Modelo-Vista-Controlador (MVC)
             * - Distribución de tareas entre módulos
             * - Funciones organizadas en controladores
             * - Modelo de datos con persistencia en JSON
             * - Base de datos JSON para no perder información entre sesiones
             */
        }
    }
}
