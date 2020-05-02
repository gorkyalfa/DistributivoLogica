using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Distributivo
{
    public class Duracion : IComparer<Curso>
    {
        public int Compare([AllowNull] Curso x, [AllowNull] Curso y)
        {
            return x.Asignatura.Horas - y.Asignatura.Horas;
        }
    }
}