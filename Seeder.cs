using System;
using System.Collections.Generic;
using System.Linq;

namespace Distributivo
{
    class Seeder
    {
        public static List<Asignatura> Asignaturas()
        {
            return new List<Asignatura>
            {
                new Asignatura
                {
                    AsignaturaId = 1,
                    Nombre = "Programación",
                    Horas = 10
                },
                new Asignatura
                {
                    AsignaturaId = 2,
                    Nombre = "Tendencias",
                    Horas = 5
                },
                new Asignatura
                {
                    AsignaturaId = 3,
                    Nombre = "Procesos",
                    Horas = 8
                },
                new Asignatura
                {
                    AsignaturaId = 4,
                    Nombre = "Ingeniería",
                    Horas = 8
                },
                new Asignatura
                {
                    AsignaturaId = 5,
                    Nombre = "Base de datos",
                    Horas = 10
                }
            };
        }

        public static List<Docente> Docentes(List<Asignatura> asignaturas)
        {
            return new List<Docente>
            {
                new Docente
                {
                    DocenteId = 1,
                    Nombre = "Pablo",
                    Minimo = 18,
                    Maximo = 22,
                    AsignaturasAfines = asignaturas.Take(3).ToList()
                },
                new Docente
                {
                    DocenteId = 2,
                    Nombre = "Mauricio",
                    Minimo = 18,
                    Maximo = 22,
                    AsignaturasAfines = asignaturas.Skip(1).Take(2).ToList()
                },
                new Docente
                {
                    DocenteId = 3,
                    Nombre = "Maritza",
                    Minimo = 18,
                    Maximo = 22,
                    AsignaturasAfines = asignaturas.Skip(2).Take(3).ToList()
                },
                new Docente
                {
                    DocenteId = 4,
                    Nombre = "Lorena",
                    Minimo = 18,
                    Maximo = 22,
                    AsignaturasAfines = asignaturas.Skip(3).Take(2).ToList()
                }
            };
        }

        public static List<Asignatura> AsignaturasAB(int numeroAsignaturas)
        {
            var asignaturas = new List<Asignatura>();
            var generator = new Random();
            for (int i = 0; i < numeroAsignaturas; i++)
            {
                asignaturas.Add(new Asignatura
                {
                    AsignaturaId = i,
                    Nombre = "A" + i,
                    Horas = (generator.Next(1, 4) + 1) * 2
                });
            };
            return asignaturas;
        }

        public static List<Curso> CursosAB(List<Asignatura> asignaturas)
        {
            var cursos = new List<Curso>();
            foreach (var paralelo in new string[] { "A", "B" })
            {
                for (int i = 0; i < asignaturas.Count; i++)
                {
                    cursos.Add(new Curso
                    {
                        CursoId = i,
                        Asignatura = asignaturas[i],
                        Paralelo = paralelo
                    });
                }
            };

            return cursos;
        }

        public static List<Docente> DocentesAB(List<Asignatura> asignaturas, int numeroDocentes)
        {
            var docentes = new List<Docente>();
            var numeroAsignaturasPorDocente = asignaturas.Count;
            var generador = new Random();
            for (int i=0; i< numeroDocentes; i++)
            {
                docentes.Add(new Docente
                {
                    DocenteId = i,
                    Nombre = "Docente 1",
                    Minimo = 20,
                    Maximo = 24,
                    AsignaturasAfines = asignaturas.Skip(
                        generador.Next(0, numeroAsignaturasPorDocente > 10
                        ? numeroAsignaturasPorDocente - 10
                        : numeroAsignaturasPorDocente)).Take(
                        generador.Next(numeroAsignaturasPorDocente / 4, numeroAsignaturasPorDocente/2)).ToList()
                });
            };

            return docentes;
        }
    }
}