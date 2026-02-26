// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

namespace TraductorInglesEspanol
{
    class Program
    {
        static Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        
        static void Main(string[] args)
        {
            InicializarDiccionario();
            
            bool salir = false;
            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        TraducirFrase();
                        break;
                    case "2":
                        AgregarPalabras();
                        break;
                    case "0":
                        salir = true;
                        Console.WriteLine("¡Gracias por usar el traductor! Presione cualquier tecla para salir...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.\n");
                        break;
                }
            }
        }
        
        static void InicializarDiccionario()
        {
            // Lista base de palabras sugeridas (inglés -> español)
            diccionario.Add("time", "tiempo");
            diccionario.Add("person", "persona");
            diccionario.Add("year", "año");
            diccionario.Add("way", "camino");
            diccionario.Add("day", "día");
            diccionario.Add("thing", "cosa");
            diccionario.Add("man", "hombre");
            diccionario.Add("world", "mundo");
            diccionario.Add("life", "vida");
            diccionario.Add("hand", "mano");
            diccionario.Add("part", "parte");
            diccionario.Add("child", "niño/a");
            diccionario.Add("eye", "ojo");
            diccionario.Add("woman", "mujer");
            diccionario.Add("place", "lugar");
            diccionario.Add("work", "trabajo");
            diccionario.Add("week", "semana");
            diccionario.Add("case", "caso");
            diccionario.Add("point", "punto");
            diccionario.Add("government", "gobierno");
            diccionario.Add("company", "empresa");
        }
        
        static void MostrarMenu()
        {
            Console.WriteLine("\n==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.WriteLine("================================================");
            Console.Write("Seleccione una opción: ");
        }
        
        static void TraducirFrase()
        {
            Console.Write("\nIngrese la frase a traducir (inglés o español): ");
            string frase = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("No ingresó ninguna frase.\n");
                return;
            }
            
            // Dividir la frase en palabras, manteniendo signos de puntuación
            string[] palabras = frase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> resultado = new List<string>();
            
            foreach (string palabra in palabras)
            {
                // Limpiar la palabra de signos de puntuación para buscar en el diccionario
                string palabraLimpia = new string(palabra.Where(c => !char.IsPunctuation(c)).ToArray());
                string signosPuntuacion = new string(palabra.Where(char.IsPunctuation).ToArray());
                
                // Verificar si la palabra limpia está en el diccionario (inglés -> español o español -> inglés)
                string traduccion = BuscarTraduccion(palabraLimpia);
                
                if (traduccion != null)
                {
                    // Agregar la traducción con los signos de puntuación originales
                    resultado.Add(traduccion + signosPuntuacion);
                }
                else
                {
                    // Mantener la palabra original si no se encuentra traducción
                    resultado.Add(palabra);
                }
            }
            
            Console.WriteLine("\nFrase traducida:");
            Console.WriteLine(string.Join(" ", resultado) + "\n");
        }
        
        static string BuscarTraduccion(string palabra)
        {
            // Primero intentamos buscar como inglés -> español
            if (diccionario.ContainsKey(palabra))
            {
                return diccionario[palabra];
            }
            
            // Si no se encuentra, buscamos como español -> inglés
            // Esto es ineficiente pero funciona para un diccionario pequeño
            foreach (var par in diccionario)
            {
                // Verificar si la palabra en español coincide (considerando múltiples traducciones separadas por /)
                string[] traduccionesEspanol = par.Value.Split('/');
                foreach (string trad in traduccionesEspanol)
                {
                    if (trad.Trim().Equals(palabra, StringComparison.OrdinalIgnoreCase))
                    {
                        return par.Key;
                    }
                }
            }
            
            return null; // No se encontró traducción
        }
        
        static void AgregarPalabras()
        {
            Console.WriteLine("\n--- Agregar nuevas palabras al diccionario ---");
            Console.Write("Ingrese la palabra en INGLÉS: ");
            string palabraIngles = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(palabraIngles))
            {
                Console.WriteLine("No ingresó ninguna palabra.\n");
                return;
            }
            
            Console.Write("Ingrese la traducción en ESPAÑOL: ");
            string palabraEspanol = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrWhiteSpace(palabraEspanol))
            {
                Console.WriteLine("No ingresó ninguna traducción.\n");
                return;
            }
            
            // Verificar si la palabra ya existe
            if (diccionario.ContainsKey(palabraIngles))
            {
                Console.WriteLine($"\nLa palabra '{palabraIngles}' ya existe en el diccionario.");
                Console.Write("¿Desea actualizar su traducción? (s/n): ");
                string respuesta = Console.ReadLine()?.ToLower();
                
                if (respuesta == "s" || respuesta == "si")
                {
                    diccionario[palabraIngles] = palabraEspanol;
                    Console.WriteLine("Palabra actualizada correctamente.\n");
                }
                else
                {
                    Console.WriteLine("Operación cancelada.\n");
                }
            }
            else
            {
                diccionario.Add(palabraIngles, palabraEspanol);
                Console.WriteLine($"Palabra '{palabraIngles}' agregada correctamente al diccionario.\n");
            }
            
            // Mostrar el diccionario actualizado
            MostrarDiccionario();
        }
        
        static void MostrarDiccionario()
        {
            Console.WriteLine("\n--- Diccionario actual ({0} palabras) ---", diccionario.Count);
            Console.WriteLine("{0,-15} | {1,-15}", "INGLÉS", "ESPAÑOL");
            Console.WriteLine(new string('-', 35));
            
            foreach (var par in diccionario.OrderBy(p => p.Key))
            {
                Console.WriteLine("{0,-15} | {1,-15}", par.Key, par.Value);
            }
            Console.WriteLine();
        }
    }
}
