namespace Distributivo
{
    public class Curso
    {
        public int CursoId { get; set; }

        public Asignatura Asignatura { get; set; }

        public string Paralelo { get; set; }
    }
}