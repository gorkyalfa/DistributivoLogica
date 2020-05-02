using System.Collections.Generic;

namespace Distributivo
{
    public interface IRepartible
    {
        void Repartir(List<Docente> docentes, List<Curso> cursos);
    }
}