// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

namespace CampañaVacunacionCOVID
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE VACUNACIÓN COVID-19 ===\n");

            // Crear conjuntos de ciudadanos
            var todosLosCiudadanos = GenerarCiudadanos(500);
            var vacunadosPfizer = GenerarCiudadanos(75, "Pfizer");
            var vacunadosAstraZeneca = GenerarCiudadanos(75, "AstraZeneca");

            // Mostrar información de los conjuntos
            MostrarInformacionConjuntos(todosLosCiudadanos, vacunadosPfizer, vacunadosAstraZeneca);

            // Aplicar operaciones de teoría de conjuntos
            ObtenerListadosSolicitados(todosLosCiudadanos, vacunadosPfizer, vacunadosAstraZeneca);

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        static HashSet<string> GenerarCiudadanos(int cantidad, string tipo = "General")
        {
            var ciudadanos = new HashSet<string>();
            for (int i = 1; i <= cantidad; i++)
            {
                ciudadanos.Add($"Ciudadano {i}");
            }
            return ciudadanos;
        }

        static void MostrarInformacionConjuntos(HashSet<string> todos, HashSet<string> pfizer, HashSet<string> astrazeneca)
        {
            Console.WriteLine("=== INFORMACIÓN DE CONJUNTOS ===");
            Console.WriteLine($"Total de ciudadanos registrados: {todos.Count}");
            Console.WriteLine($"Vacunados con Pfizer: {pfizer.Count}");
            Console.WriteLine($"Vacunados con AstraZeneca: {astrazeneca.Count}");
            Console.WriteLine();
        }

        static void ObtenerListadosSolicitados(HashSet<string> todos, HashSet<string> pfizer, HashSet<string> astrazeneca)
        {
            // 1. Ciudadanos que no se han vacunado
            var vacunadosTotales = new HashSet<string>(pfizer);
            vacunadosTotales.UnionWith(astrazeneca);
            
            var noVacunados = new HashSet<string>(todos);
            noVacunados.ExceptWith(vacunadosTotales);

            // 2. Ciudadanos con ambas dosis (intersección)
            var ambasDosis = new HashSet<string>(pfizer);
            ambasDosis.IntersectWith(astrazeneca);

            // 3. Ciudadanos que solo recibieron Pfizer
            var soloPfizer = new HashSet<string>(pfizer);
            soloPfizer.ExceptWith(astrazeneca);

            // 4. Ciudadanos que solo recibieron AstraZeneca
            var soloAstraZeneca = new HashSet<string>(astrazeneca);
            soloAstraZeneca.ExceptWith(pfizer);

            // Mostrar resultados
            MostrarResultados(noVacunados, ambasDosis, soloPfizer, soloAstraZeneca);
        }

        static void MostrarResultados(HashSet<string> noVacunados, HashSet<string> ambasDosis, 
                                     HashSet<string> soloPfizer, HashSet<string> soloAstraZeneca)
        {
            Console.WriteLine("=== RESULTADOS DE LA CAMPAÑA DE VACUNACIÓN ===\n");

            // 1. No vacunados
            Console.WriteLine("1. CIUDADANOS NO VACUNADOS:");
            Console.WriteLine($"   Total: {noVacunados.Count} ciudadanos");
            if (noVacunados.Count > 0)
            {
                var primerosNoVacunados = noVacunados.Take(10).ToList();
                Console.WriteLine("   Primeros 10 no vacunados:");
                foreach (var ciudadano in primerosNoVacunados)
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (noVacunados.Count > 10)
                    Console.WriteLine($"   ... y {noVacunados.Count - 10} más");
            }
            Console.WriteLine();

            // 2. Ambas dosis
            Console.WriteLine("2. CIUDADANOS CON AMBAS DOSIS:");
            Console.WriteLine($"   Total: {ambasDosis.Count} ciudadanos");
            if (ambasDosis.Count > 0)
            {
                Console.WriteLine("   Listado completo:");
                foreach (var ciudadano in ambasDosis.OrderBy(c => c))
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
            }
            Console.WriteLine();

            // 3. Solo Pfizer
            Console.WriteLine("3. CIUDADANOS QUE SOLO RECIBIERON PFIZER:");
            Console.WriteLine($"   Total: {soloPfizer.Count} ciudadanos");
            if (soloPfizer.Count > 0)
            {
                var primerosPfizer = soloPfizer.Take(10).ToList();
                Console.WriteLine("   Primeros 10 (por orden):");
                foreach (var ciudadano in primerosPfizer.OrderBy(c => c))
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (soloPfizer.Count > 10)
                    Console.WriteLine($"   ... y {soloPfizer.Count - 10} más");
            }
            Console.WriteLine();

            // 4. Solo AstraZeneca
            Console.WriteLine("4. CIUDADANOS QUE SOLO RECIBIERON ASTRAZENECA:");
            Console.WriteLine($"   Total: {soloAstraZeneca.Count} ciudadanos");
            if (soloAstraZeneca.Count > 0)
            {
                var primerosAstra = soloAstraZeneca.Take(10).ToList();
                Console.WriteLine("   Primeros 10 (por orden):");
                foreach (var ciudadano in primerosAstra.OrderBy(c => c))
                {
                    Console.WriteLine($"   - {ciudadano}");
                }
                if (soloAstraZeneca.Count > 10)
                    Console.WriteLine($"   ... y {soloAstraZeneca.Count - 10} más");
            }

            // Resumen estadístico
            MostrarResumenEstadistico(noVacunados.Count, ambasDosis.Count, soloPfizer.Count, soloAstraZeneca.Count);
        }

        static void MostrarResumenEstadistico(int noVacunados, int ambasDosis, int soloPfizer, int soloAstraZeneca)
        {
            Console.WriteLine("\n=== RESUMEN ESTADÍSTICO ===");
            Console.WriteLine($"Total ciudadanos: {noVacunados + ambasDosis + soloPfizer + soloAstraZeneca}");
            Console.WriteLine($"No vacunados: {noVacunados} ({CalcularPorcentaje(noVacunados, 500):F1}%)");
            Console.WriteLine($"Ambas dosis: {ambasDosis} ({CalcularPorcentaje(ambasDosis, 500):F1}%)");
            Console.WriteLine($"Solo Pfizer: {soloPfizer} ({CalcularPorcentaje(soloPfizer, 500):F1}%)");
            Console.WriteLine($"Solo AstraZeneca: {soloAstraZeneca} ({CalcularPorcentaje(soloAstraZeneca, 500):F1}%)");
            
            int totalVacunados = ambasDosis + soloPfizer + soloAstraZeneca;
            Console.WriteLine($"\nCobertura de vacunación: {totalVacunados} ({CalcularPorcentaje(totalVacunados, 500):F1}%)");
        }

        static double CalcularPorcentaje(int valor, int total)
        {
            return (double)valor / total * 100;
        }
    }
}
