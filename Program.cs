using System;
using System.Linq;

namespace Distributivo
{
    class Program
    {
        static void Main(string[] args)
        {
            var asignaturas = Seeder.AsignaturasAB(30);
            var docentes = Seeder.DocentesAB(asignaturas, 15);
            var cursos = Seeder.CursosAB(asignaturas);

            Console.WriteLine($"Distribuir {cursos.Sum(curso => curso.Asignatura.Horas)} horas");
            Console.WriteLine($"Capacidad máxima {docentes.Sum(docente => docente.Maximo)} horas");
            Console.Write($"Seleccione el algoritmo. 1 -> Simple, 2 -> GrandesPrimero: ");

            IRepartible algoritmo;
            var key = Console.ReadLine();
            if (key == "1")
                algoritmo = new Simple();
            else
                algoritmo = new GrandesPrimero();

            Repartidor repartidor = new Repartidor(algoritmo);

            Console.WriteLine("\nDistribuyendo...");

            repartidor.Ejecutar(docentes, cursos);

            foreach (var docente in docentes)
            {
                Console.Write($"\nDocente: {docente.Nombre}, {docente.Cursos.Sum(curso => curso.Asignatura.Horas)} horas");
                foreach (var asignatura in docente.AsignaturasAfines)
                {
                    Console.Write($", {asignatura.Nombre}");
                }

                Console.WriteLine($"\n|{"Curso",-25}|{"Paralelo",-8}|{"Horas",5}|");
                foreach (var curso in docente.Cursos)
                {
                    Console.WriteLine($"|{curso.Asignatura.Nombre,-25}|{curso.Paralelo,-8}|{curso.Asignatura.Horas,5}|");
                }
            }

            Console.WriteLine("\nCursos sin asignar");
            foreach (var curso in cursos)
            {
                Console.WriteLine($"{curso.Asignatura.Nombre}, Paralelo: {curso.Paralelo}, {curso.Asignatura.Horas} horas");
            }

            Console.WriteLine("Listo");
            Console.ReadKey();
        }
    }
}