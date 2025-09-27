using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace proyecto_final
{
    class Iniciador
    {
        public static void iniciador()
        {
            Console.WriteLine("Hola porfavor ingresa tu dni");
            int dniIngresado = Convert.ToInt32(Console.ReadLine());
            int dni = 74758787;

            string nombres = "Pedro";
            string apellidos = "Perez";


            if (dniIngresado == dni)
            {
                Console.WriteLine("Bienvenido " + nombres + " " + apellidos);
                Console.WriteLine("Quieres actualizar tus datos? (si/no)");
            }
            else
            {
                Console.WriteLine("No estas registrado, porfavor registrate");
                Console.WriteLine("Ingresa tus nombres");
            }
        } 
    }
}
