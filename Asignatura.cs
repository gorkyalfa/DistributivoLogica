using System.Collections.Generic;

namespace Distributivo
{
    public class Asignatura
    {
        public Asignatura()
        {
            Cursos = new List<Curso>();
        }

        public int AsignaturaId { get; set; }

        public string Nombre { get; set; }

        public int Horas { get; set; }

        public List<Curso> Cursos { get; set; }
    }
}