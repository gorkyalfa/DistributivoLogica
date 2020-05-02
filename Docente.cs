using System.Collections.Generic;

namespace Distributivo
{
    public class Docente
    {
        public Docente()
        {
            AsignaturasAfines = new List<Asignatura>();
            Cursos = new List<Curso>();
        }
        public int DocenteId { get; set; }
        public string Nombre { get; set; }
        public int Minimo { get; set; }
        public int Maximo { get; set; }
        public List<Asignatura> AsignaturasAfines { get; set; }
        public List<Curso> Cursos { get; set; }
    }
}