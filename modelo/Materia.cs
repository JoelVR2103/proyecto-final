using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Creditos { get; set; }

        public Materia() { }

        public Materia(int id, string nombre, string codigo, int creditos)
        {
            Id = id;
            Nombre = nombre;
            Codigo = codigo;
            Creditos = creditos;
        }

        public List<Materia> ObtenerMateriasDisponibles()
        {
            return new List<Materia>
            {
                new Materia(1, "Matemática I", "MAT101", 4),
                new Materia(2, "Física I", "FIS101", 4),
                new Materia(3, "Química General", "QUI101", 3),
                new Materia(4, "Programación I", "PRG101", 4),
                new Materia(5, "Inglés I", "ING101", 2),
                new Materia(6, "Historia Universal", "HIS101", 3),
                new Materia(7, "Filosofía", "FIL101", 2),
                new Materia(8, "Estadística", "EST101", 3),
                new Materia(9, "Comunicación", "COM101", 2),
                new Materia(10, "Metodología de Investigación", "MET101", 3)
            };
        }
        public override string ToString()
        {
            return $"{Id}. {Nombre} ({Codigo}) - {Creditos} créditos";
        }

        
    }
}
