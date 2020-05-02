using System.Collections.Generic;
using System.Linq;

namespace Distributivo
{
    public class Simple : IRepartible
    {
        // Genera soluciones no optimas

        /// <summary>
        /// Reparte los cursos a los docentes, el algoritmo no encuentra soluciones optimas.
        /// Tampoco cumple con las restricciones de que todos los cursos se dicten por un docente.
        /// Los cursos que se encunetren en el parámetro cursos no fueron asignados.
        /// Los docentes no siempre cuentan con las horas mínimas.  
        /// </summary>
        /// <param name="docentes">Docentes que impartiran cursos</param>
        /// <param name="cursos">Cursos para ser repartidos</param>
        public void Repartir(List<Docente> docentes, List<Curso> cursos)
        {
            foreach (var docente in docentes)
            {
                var cursosAfines = cursos.Where(curso => docente.AsignaturasAfines.IndexOf(curso.Asignatura) >= 0).ToList();

                var posicionActual = 0;
                while (posicionActual < cursosAfines.Count())
                {
                    if (HorasAsignadasSonMenorIgualMaximo(docente, cursosAfines, posicionActual))
                    {
                        docente.Cursos.Add(cursosAfines[posicionActual]);

                        cursos.Remove(cursosAfines[posicionActual]);
                        cursosAfines.Remove(cursosAfines[posicionActual]);

                        if (docente.Cursos.Sum(curso => curso.Asignatura.Horas) >= docente.Minimo)
                            break;
                        
                        continue; // Debido al remove
                    }
                    posicionActual++;
                }
            }
        }

        private bool HorasAsignadasSonMenorIgualMaximo(Docente docente, List<Curso> cursosAfines, int posicionActual)
        {
            return docente.Cursos.Sum(curso => curso.Asignatura.Horas)
             + cursosAfines[posicionActual].Asignatura.Horas <= docente.Maximo;
        }
    }
}