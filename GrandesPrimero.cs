using System.Collections.Generic;
using System.Linq;

namespace Distributivo
{
    public class GrandesPrimero : IRepartible
    {
        // Genera soluciones no optimas

        /// <summary>
        /// Reparte los cursos a los docentes, el algoritmo no encuentra soluciones optimas.
        /// Tampoco cumple con las restricciones de que todos los cursos se dicten por un docente.
        /// Los cursos que se encunetren en el parámetro cursos no fueron asignados.
        /// Los docentes no siempre cuentan con las horas mínimas.
        /// 
        /// Las materias se ordenan de mayor a menor.
        /// Se asigna un materia x docente y se va avanzando hasta que cada docente llega cerca del nivel deseado.
        /// </summary>
        /// <param name="docentes">Docentes que impartiran cursos</param>
        /// <param name="cursos">Cursos para ser repartidos</param>
        public void Repartir(List<Docente> docentes, List<Curso> cursos)
        {
            cursos.Sort(new Duracion());
            var numeroCursos = cursos.Count;
            var intento = 1;
            var numeroIntentos = numeroCursos * 2; // Número escogido al azar, no sabemos que sería un buen valor

            while (intento < numeroIntentos)
            {
                while (cursos.Any())
                {
                    foreach (var docente in docentes.Where(docente => !HorasAsignadasMayorIgualQueMinimo(docente)))
                    {
                        var cursosAfines = ObtenerCursosAfines(cursos, docente);

                        var posicionActual = 0;
                        while (posicionActual < cursosAfines.Count())
                        {
                            if (HorasAsignadasSonMenorIgualMaximo(docente, cursosAfines[posicionActual]))
                            {
                                docente.Cursos.Add(cursosAfines[posicionActual]);

                                cursos.Remove(cursosAfines[posicionActual]);
                                cursosAfines.Remove(cursosAfines[posicionActual]);

                                break;
                            }
                            posicionActual++;
                        }
                    }

                    if (SeAsignaronCursos(cursos, numeroCursos))
                        numeroCursos = cursos.Count;
                    else
                        break;
                }

                if (!cursos.Any())
                    return;

                if (!ReubicarNoAsignados(docentes, cursos))
                    return;

                intento++;
            }
        }

        private bool ReubicarNoAsignados(List<Docente> docentes, List<Curso> cursos)
        {
            if (cursos.Any())
            {
                for (var indiceCurso = 0; indiceCurso < cursos.Count; indiceCurso++)
                {
                    var docenteEncontrado = docentes.FirstOrDefault(
                        docente =>
                        docente.AsignaturasAfines.Contains(cursos[indiceCurso].Asignatura));

                    if (docenteEncontrado != null)
                    {
                        var totalHoras = docenteEncontrado.Cursos.Sum(curso => curso.Asignatura.Horas);
                        var cursoParaIntercambiar = docenteEncontrado.Cursos.FirstOrDefault(
                            curso =>
                            docenteEncontrado.Minimo <= totalHoras - curso.Asignatura.Horas + cursos[indiceCurso].Asignatura.Horas
                            && docenteEncontrado.Maximo >= totalHoras - curso.Asignatura.Horas + cursos[indiceCurso].Asignatura.Horas
                            && curso.Asignatura.AsignaturaId != cursos[indiceCurso].Asignatura.AsignaturaId);

                        docenteEncontrado.Cursos.Add(cursos[indiceCurso]);
                        cursos.Remove(cursos[indiceCurso]);
                        docenteEncontrado.Cursos.Remove(cursoParaIntercambiar);
                        cursos.Add(cursoParaIntercambiar);

                        return true;
                    }
                }
            }

            return false;
        }

        private static bool SeAsignaronCursos(List<Curso> cursos, int numeroCursos)
        {
            return numeroCursos != cursos.Count;
        }

        private static bool HorasAsignadasMayorIgualQueMinimo(Docente docente)
        {
            return docente.Cursos.Sum(curso => curso.Asignatura.Horas) >= docente.Minimo;
        }

        private static List<Curso> ObtenerCursosAfines(List<Curso> cursos, Docente docente)
        {
            return cursos.Where(curso => docente.AsignaturasAfines.IndexOf(curso.Asignatura) >= 0).ToList();
        }

        private bool HorasAsignadasSonMenorIgualMaximo(Docente docente, Curso cursoAfin)
        {
            return docente.Cursos.Sum(curso => curso.Asignatura.Horas) + cursoAfin.Asignatura.Horas <= docente.Maximo;
        }
    }
}