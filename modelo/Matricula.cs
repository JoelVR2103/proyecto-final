using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelo
{
    
    public enum Turno
    {
        Mañana,
        Tarde
    }
    public class Matricula
    {
        public string DniEstudiante { get; set; }
        public string Grado { get; set; }
        public Turno TurnoSeleccionado { get; set; }
        public List<Materia> MateriasSeleccionadas { get; set; }
        public DateTime FechaMatricula { get; set; }
        public bool EsSeleccionAutomatica { get; set; }

        public Matricula()
        {
            MateriasSeleccionadas = new List<Materia>();
            FechaMatricula = DateTime.Now;
        }

        public Matricula(string dniEstudiante, string grado, Turno turno)
        {
            DniEstudiante = dniEstudiante;
            Grado = grado;
            TurnoSeleccionado = turno;
            MateriasSeleccionadas = new List<Materia>();
            FechaMatricula = DateTime.Now;
            EsSeleccionAutomatica = false;
        }

        public Matricula(string dniEstudiante, string grado, Turno turno, List<Materia> materiasSeleccionadas, bool esSeleccionAutomatica)
        {
            DniEstudiante = dniEstudiante;
            Grado = grado;
            TurnoSeleccionado = turno;
            MateriasSeleccionadas = materiasSeleccionadas ?? new List<Materia>();
            FechaMatricula = DateTime.Now;
            EsSeleccionAutomatica = esSeleccionAutomatica;
        }

        public bool AgregarMateria(Materia materia)
        {
            
            if (MateriasSeleccionadas.Count >= 6)
                return false;

            
            if (MateriasSeleccionadas.Any(m => m.Id == materia.Id))
                return false;

            MateriasSeleccionadas.Add(materia);
            return true;
        }

        public void RemoverMateria(int idMateria)
        {
            MateriasSeleccionadas.RemoveAll(m => m.Id == idMateria);
        }

        public int TotalCreditos()
        {
            return MateriasSeleccionadas.Sum(m => m.Creditos);
        }

        public bool EstaCompleta()
        {
            return MateriasSeleccionadas.Count == 6;
        }
    }
} 
