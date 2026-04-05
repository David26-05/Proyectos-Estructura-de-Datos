// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VuelosBaratos
{
    // Clase que representa un vuelo
    public class Vuelo
    {
        public string Origen { get; set; }
        public string Destino { get; set; }
        public double Precio { get; set; }
        public string Aerolinea { get; set; }
        public int DuracionMinutos { get; set; }
        public DateTime FechaSalida { get; set; }

        public override string ToString()
        {
            return $"{Origen} -> {Destino} | {Aerolinea} | ${Precio:F2} | {DuracionMinutos} min | {FechaSalida:dd/MM/yyyy}";
        }
    }

    // Grafo para representar rutas de vuelos
    public class GrafoVuelos
    {
        private Dictionary<string, List<Vuelo>> _adyacencias;

        public GrafoVuelos()
        {
            _adyacencias = new Dictionary<string, List<Vuelo>>();
        }

        public void AgregarVuelo(Vuelo vuelo)
        {
            if (!_adyacencias.ContainsKey(vuelo.Origen))
                _adyacencias[vuelo.Origen] = new List<Vuelo>();
            
            _adyacencias[vuelo.Origen].Add(vuelo);
        }

        public List<Vuelo> ObtenerVuelosDesde(string origen)
        {
            return _adyacencias.ContainsKey(origen) ? _adyacencias[origen] : new List<Vuelo>();
        }

        public HashSet<string> ObtenerTodosLosAeropuertos()
        {
            var aeropuertos = new HashSet<string>();
            foreach (var origen in _adyacencias.Keys)
            {
                aeropuertos.Add(origen);
                foreach (var vuelo in _adyacencias[origen])
                    aeropuertos.Add(vuelo.Destino);
            }
            return aeropuertos;
        }

        // Algoritmo de Dijkstra para encontrar la ruta más barata
        public (double costoTotal, List<string> ruta) RutaMasBarata(string origen, string destino)
        {
            var distancias = new Dictionary<string, double>();
            var previos = new Dictionary<string, string>();
            var noVisitados = new HashSet<string>();

            foreach (var aeropuerto in ObtenerTodosLosAeropuertos())
            {
                distancias[aeropuerto] = double.MaxValue;
                noVisitados.Add(aeropuerto);
            }
            distancias[origen] = 0;

            while (noVisitados.Count > 0)
            {
                var actual = noVisitados.OrderBy(v => distancias[v]).First();
                if (distancias[actual] == double.MaxValue) break;
                
                noVisitados.Remove(actual);
                if (actual == destino) break;

                foreach (var vuelo in ObtenerVuelosDesde(actual))
                {
                    var alternativa = distancias[actual] + vuelo.Precio;
                    if (alternativa < distancias[vuelo.Destino])
                    {
                        distancias[vuelo.Destino] = alternativa;
                        previos[vuelo.Destino] = actual;
                    }
                }
            }

            // Reconstruir ruta
            var ruta = new List<string>();
            if (distancias[destino] == double.MaxValue)
                return (double.MaxValue, ruta);

            var temp = destino;
            while (temp != null)
            {
                ruta.Insert(0, temp);
                previos.TryGetValue(temp, out temp);
            }
            return (distancias[destino], ruta);
        }

        // Obtener vuelos directos ordenados por precio
        public List<Vuelo> VuelosDirectosMasBaratos(string origen, string destino)
        {
            return _adyacencias.ContainsKey(origen) 
                ? _adyacencias[origen].Where(v => v.Destino == destino).OrderBy(v => v.Precio).ToList()
                : new List<Vuelo>();
        }
    }

    // Base de datos ficticia
    public class BaseDatosVuelos
    {
        private List<Vuelo> _vuelos;
        private GrafoVuelos _grafo;

        public BaseDatosVuelos()
        {
            _vuelos = new List<Vuelo>();
            _grafo = new GrafoVuelos();
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            // Datos de ejemplo
            _vuelos.AddRange(new[]
            {
                new Vuelo { Origen = "GUAYAQUIL", Destino = "QUITO", Precio = 160, Aerolinea = "Avianca", DuracionMinutos = 60, FechaSalida = DateTime.Now.AddDays(1) },
                new Vuelo { Origen = "QUITO", Destino = "GUAYAQUIL", Precio = 150, Aerolinea = "Sky Ecuador", DuracionMinutos = 60, FechaSalida = DateTime.Now.AddDays(1) },
                new Vuelo { Origen = "ECU", Destino = "ESTADOS UNIDOS", Precio = 1500, Aerolinea = "American Airlines", DuracionMinutos = 120, FechaSalida = DateTime.Now.AddDays(2) },
                new Vuelo { Origen = "ECU", Destino = "EUROPA", Precio = 2100, Aerolinea = "Iberia", DuracionMinutos = 170, FechaSalida = DateTime.Now.AddDays(3) },
                new Vuelo { Origen = "GUAYAQUIL", Destino = "GALAPAGOS", Precio = 1100, Aerolinea = "Sky Ecuador", DuracionMinutos = 120, FechaSalida = DateTime.Now.AddDays(1) },
                new Vuelo { Origen = "GALAPAGOS", Destino = "GUAYAQUIL", Precio = 1200, Aerolinea = "Sky Ecuador", DuracionMinutos = 140, FechaSalida = DateTime.Now.AddDays(2) },
                new Vuelo { Origen = "ECU", Destino = "BRACILIA", Precio = 1100, Aerolinea = "LATAM Airlines", DuracionMinutos = 480, FechaSalida = DateTime.Now.AddDays(1) },
                new Vuelo { Origen = "MEX", Destino = "TIJ", Precio = 2800, Aerolinea = "Volaris", DuracionMinutos = 210, FechaSalida = DateTime.Now.AddDays(4) },
                new Vuelo { Origen = "CUN", Destino = "TIJ", Precio = 2500, Aerolinea = "Viva", DuracionMinutos = 240, FechaSalida = DateTime.Now.AddDays(2) },
            });

            foreach (var vuelo in _vuelos)
                _grafo.AgregarVuelo(vuelo);
        }

        public void CargarDesdeArchivo(string rutaArchivo)
        {
            if (File.Exists(rutaArchivo))
            {
                var lineas = File.ReadAllLines(rutaArchivo);
                foreach (var linea in lineas.Skip(1)) // Saltar encabezado
                {
                    var partes = linea.Split(',');
                    if (partes.Length >= 6)
                    {
                        var vuelo = new Vuelo
                        {
                            Origen = partes[0],
                            Destino = partes[1],
                            Precio = double.Parse(partes[2]),
                            Aerolinea = partes[3],
                            DuracionMinutos = int.Parse(partes[4]),
                            FechaSalida = DateTime.Parse(partes[5])
                        };
                        _vuelos.Add(vuelo);
                        _grafo.AgregarVuelo(vuelo);
                    }
                }
            }
        }

        public void GuardarEnArchivo(string rutaArchivo)
        {
            using (var writer = new StreamWriter(rutaArchivo))
            {
                writer.WriteLine("Origen,Destino,Precio,Aerolinea,Duracion,FechaSalida");
                foreach (var v in _vuelos)
                {
                    writer.WriteLine($"{v.Origen},{v.Destino},{v.Precio},{v.Aerolinea},{v.DuracionMinutos},{v.FechaSalida:yyyy-MM-dd}");
                }
            }
        }

        public List<Vuelo> BuscarVuelos(string origen, string destino, double? precioMax = null, string aerolinea = null)
        {
            var query = _vuelos.AsQueryable();
            
            if (!string.IsNullOrEmpty(origen))
                query = query.Where(v => v.Origen.Equals(origen, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrEmpty(destino))
                query = query.Where(v => v.Destino.Equals(destino, StringComparison.OrdinalIgnoreCase));
            if (precioMax.HasValue)
                query = query.Where(v => v.Precio <= precioMax.Value);
            if (!string.IsNullOrEmpty(aerolinea))
                query = query.Where(v => v.Aerolinea.Equals(aerolinea, StringComparison.OrdinalIgnoreCase));

            return query.ToList();
        }

        public (double costo, List<string> ruta) EncontrarRutaMasBarata(string origen, string destino)
        {
            return _grafo.RutaMasBarata(origen, destino);
        }

        public List<Vuelo> ObtenerTodosLosVuelos() => _vuelos.ToList();
        
        public List<string> ObtenerAeropuertos() => _grafo.ObtenerTodosLosAeropuertos().ToList();
    }

    // Interfaz de Reportería
    public class ReporteriaVuelos
    {
        public static void MostrarVuelos(List<Vuelo> vuelos, string titulo = "Lista de Vuelos")
        {
            Console.WriteLine($"\n=== {titulo} ===");
            Console.WriteLine(new string('-', 80));
            if (vuelos.Count == 0)
            {
                Console.WriteLine("No se encontraron vuelos.");
                return;
            }
            foreach (var vuelo in vuelos)
                Console.WriteLine(vuelo);
            Console.WriteLine($"Total: {vuelos.Count} vuelos");
        }

        public static void MostrarEstadisticas(BaseDatosVuelos db)
        {
            var vuelos = db.ObtenerTodosLosVuelos();
            Console.WriteLine("\n=== ESTADÍSTICAS ===");
            Console.WriteLine($"Total de vuelos: {vuelos.Count}");
            Console.WriteLine($"Aeropuertos disponibles: {string.Join(", ", db.ObtenerAeropuertos())}");
            Console.WriteLine($"Precio promedio: ${vuelos.Average(v => v.Precio):F2}");
            Console.WriteLine($"Precio mínimo: ${vuelos.Min(v => v.Precio):F2}");
            Console.WriteLine($"Precio máximo: ${vuelos.Max(v => v.Precio):F2}");
            
            Console.WriteLine("\nAerolíneas:");
            foreach (var grupo in vuelos.GroupBy(v => v.Aerolinea))
                Console.WriteLine($"  {grupo.Key}: {grupo.Count()} vuelos, precio promedio ${grupo.Average(v => v.Precio):F2}");
        }
    }

    // Programa principal
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SISTEMA DE BÚSQUEDA DE VUELOS BARATOS ===\n");
            
            var db = new BaseDatosVuelos();
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
                Console.WriteLine("1. Ver todos los vuelos");
                Console.WriteLine("2. Buscar vuelos directos");
                Console.WriteLine("3. Encontrar ruta más barata (con escalas)");
                Console.WriteLine("4. Ver estadísticas");
                Console.WriteLine("5. Guardar datos en archivo");
                Console.WriteLine("6. Cargar datos desde archivo");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ReporteriaVuelos.MostrarVuelos(db.ObtenerTodosLosVuelos(), "Todos los vuelos disponibles");
                        break;
                    
                    case "2":
                        Console.Write("Origen (código, ej: MEX): ");
                        var orig = Console.ReadLine().ToUpper();
                        Console.Write("Destino: ");
                        var dest = Console.ReadLine().ToUpper();
                        var resultados = db.BuscarVuelos(orig, dest);
                        ReporteriaVuelos.MostrarVuelos(resultados, $"Vuelos de {orig} a {dest}");
                        break;
                    
                    case "3":
                        Console.Write("Origen: ");
                        var o = Console.ReadLine().ToUpper();
                        Console.Write("Destino: ");
                        var d = Console.ReadLine().ToUpper();
                        
                        var (costo, ruta) = db.EncontrarRutaMasBarata(o, d);
                        if (costo == double.MaxValue)
                        {
                            Console.WriteLine($"No existe ruta disponible de {o} a {d}");
                        }
                        else
                        {
                            Console.WriteLine($"\nRuta más barata de {o} a {d}:");
                            Console.WriteLine($"Ruta: {string.Join(" -> ", ruta)}");
                            Console.WriteLine($"Costo total: ${costo:F2}");
                            
                            // Mostrar vuelos alternativos directos
                            var directos = db.BuscarVuelos(o, d);
                            if (directos.Any())
                            {
                                Console.WriteLine($"\nVuelos directos disponibles (más barato: ${directos.Min(v => v.Precio):F2})");
                            }
                        }
                        break;
                    
                    case "4":
                        ReporteriaVuelos.MostrarEstadisticas(db);
                        break;
                    
                    case "5":
                        Console.Write("Nombre del archivo (ej: vuelos.txt): ");
                        var archivoGuardar = Console.ReadLine();
                        db.GuardarEnArchivo(archivoGuardar);
                        Console.WriteLine($"Datos guardados en {archivoGuardar}");
                        break;
                    
                    case "6":
                        Console.Write("Nombre del archivo a cargar: ");
                        var archivoCargar = Console.ReadLine();
                        if (File.Exists(archivoCargar))
                        {
                            db = new BaseDatosVuelos();
                            db.CargarDesdeArchivo(archivoCargar);
                            Console.WriteLine("Datos cargados exitosamente");
                        }
                        else
                        {
                            Console.WriteLine("Archivo no encontrado");
                        }
                        break;
                    
                    case "7":
                        continuar = false;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    
                    default:
                        Console.WriteLine("Opción no válida");
                        break;
                }
            }
        }
    }
}
