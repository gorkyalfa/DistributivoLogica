using System;
using System.Collections.Generic;
using System.Linq;

namespace Distributivo
{
    public class Repartidor
    {
        public IRepartible Repartible { get; set; }

        public Repartidor(IRepartible repartible)
        {
            Repartible = repartible;
        }

        public void Ejecutar(List<Docente> docentes, List<Curso> cursos)
        {
            Repartible.Repartir(docentes, cursos);
        }


        public double Rmse(List<Docente> docentes)
        {
            return RootMeanSquareError(docentes.Select(
                docente => KeyValuePair.Create<double, double>(
                    (docente.Maximo + docente.Minimo) / 2, docente.Cursos.Sum(curso => curso.Asignatura.Horas)))
                .ToArray());
        }

        public double RootMeanSquareError(KeyValuePair<double, double>[] valores)
        {
            return Math.Sqrt(valores.Average(x => Math.Pow(x.Key - x.Value, 2)));
        }
    }
}