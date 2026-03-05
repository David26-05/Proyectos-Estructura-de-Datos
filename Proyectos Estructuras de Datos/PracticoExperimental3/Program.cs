// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;

namespace TorneoFutbol
{
    // Clase Jugador
    public class Jugador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Posicion { get; set; }
        public int NumeroCamiseta { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, {Nombre} {Apellido}, Edad: {Edad}, Posición: {Posicion}, #: {NumeroCamiseta}";
        }
    }

    // Clase Equipo
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Estadio { get; set; }
        public int AñoFundacion { get; set; }
        public HashSet<Jugador> Jugadores { get; set; }

        public Equipo()
        {
            Jugadores = new HashSet<Jugador>();
        }

        public override string ToString()
        {
            return $"ID: {Id}, {Nombre}, Ciudad: {Ciudad}, Estadio: {Estadio}, Fundado: {AñoFundacion}, Jugadores: {Jugadores.Count}";
        }
    }

    // Clase principal del Torneo
    public class TorneoFutbolManager
    {
        // Uso de Map (Dictionary) para almacenar equipos por ID
        private Dictionary<int, Equipo> equipos;
        
        // Uso de Map (Dictionary) para almacenar jugadores por ID
        private Dictionary<int, Jugador> jugadores;
        
        // Uso de Set (HashSet) para mantener emails únicos
        private HashSet<string> emailsRegistrados;
        
        // Contadores para IDs autoincrementales
        private int nextEquipoId = 1;
        private int nextJugadorId = 1;

        public TorneoFutbolManager()
        {
            equipos = new Dictionary<int, Equipo>();
            jugadores = new Dictionary<int, Jugador>();
            emailsRegistrados = new HashSet<string>();
        }

        // Métodos para Equipos
        public void AgregarEquipo(string nombre, string ciudad, string estadio, int añoFundacion)
        {
            var equipo = new Equipo
            {
                Id = nextEquipoId++,
                Nombre = nombre,
                Ciudad = ciudad,
                Estadio = estadio,
                AñoFundacion = añoFundacion
            };
            
            equipos.Add(equipo.Id, equipo);
            Console.WriteLine($"Equipo agregado exitosamente. ID: {equipo.Id}");
        }

        public void EliminarEquipo(int equipoId)
        {
            if (equipos.ContainsKey(equipoId))
            {
                // Remover jugadores del equipo
                var equipo = equipos[equipoId];
                foreach (var jugador in equipo.Jugadores.ToList())
                {
                    RemoverJugadorDeEquipo(jugador.Id);
                }
                
                equipos.Remove(equipoId);
                Console.WriteLine($"Equipo con ID {equipoId} eliminado exitosamente.");
            }
            else
            {
                Console.WriteLine($"No se encontró equipo con ID {equipoId}");
            }
        }

        // Métodos para Jugadores
        public void AgregarJugador(string nombre, string apellido, int edad, 
                                   string posicion, int numeroCamiseta, string email)
        {
            // Verificar email único
            if (emailsRegistrados.Contains(email))
            {
                Console.WriteLine("Error: El email ya está registrado.");
                return;
            }

            var jugador = new Jugador
            {
                Id = nextJugadorId++,
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad,
                Posicion = posicion,
                NumeroCamiseta = numeroCamiseta,
                Email = email
            };
            
            jugadores.Add(jugador.Id, jugador);
            emailsRegistrados.Add(email);
            Console.WriteLine($"Jugador agregado exitosamente. ID: {jugador.Id}");
        }

        public void AsignarJugadorAEquipo(int jugadorId, int equipoId)
        {
            if (!jugadores.ContainsKey(jugadorId))
            {
                Console.WriteLine($"No se encontró jugador con ID {jugadorId}");
                return;
            }

            if (!equipos.ContainsKey(equipoId))
            {
                Console.WriteLine($"No se encontró equipo con ID {equipoId}");
                return;
            }

            var jugador = jugadores[jugadorId];
            var equipo = equipos[equipoId];

            // Verificar si el jugador ya está en algún equipo
            foreach (var eq in equipos.Values)
            {
                if (eq.Jugadores.Contains(jugador))
                {
                    Console.WriteLine($"El jugador ya está asignado al equipo {eq.Nombre}");
                    return;
                }
            }

            // Verificar número de camiseta único en el equipo
            if (equipo.Jugadores.Any(j => j.NumeroCamiseta == jugador.NumeroCamiseta))
            {
                Console.WriteLine($"Error: El número {jugador.NumeroCamiseta} ya está ocupado en este equipo.");
                return;
            }

            equipo.Jugadores.Add(jugador);
            Console.WriteLine($"Jugador {jugador.Nombre} asignado al equipo {equipo.Nombre}");
        }

        public void RemoverJugadorDeEquipo(int jugadorId)
        {
            if (!jugadores.ContainsKey(jugadorId))
            {
                Console.WriteLine($"No se encontró jugador con ID {jugadorId}");
                return;
            }

            var jugador = jugadores[jugadorId];
            
            foreach (var equipo in equipos.Values)
            {
                if (equipo.Jugadores.Remove(jugador))
                {
                    Console.WriteLine($"Jugador removido del equipo {equipo.Nombre}");
                    return;
                }
            }
            
            Console.WriteLine("El jugador no estaba asignado a ningún equipo");
        }

        // Métodos de Reportería (Visualización y Consultas)
        public void MostrarTodosLosEquipos()
        {
            Console.WriteLine("\n=== LISTA DE EQUIPOS ===");
            if (equipos.Count == 0)
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            foreach (var equipo in equipos.Values)
            {
                Console.WriteLine(equipo);
            }
        }

        public void MostrarTodosLosJugadores()
        {
            Console.WriteLine("\n=== LISTA DE JUGADORES ===");
            if (jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores registrados.");
                return;
            }

            foreach (var jugador in jugadores.Values)
            {
                string equipoAsignado = ObtenerEquipoDelJugador(jugador.Id);
                Console.WriteLine($"{jugador} - Equipo: {equipoAsignado}");
            }
        }

        public void MostrarEquipoConJugadores(int equipoId)
        {
            if (!equipos.ContainsKey(equipoId))
            {
                Console.WriteLine($"No se encontró equipo con ID {equipoId}");
                return;
            }

            var equipo = equipos[equipoId];
            Console.WriteLine($"\n=== EQUIPO: {equipo.Nombre} ===");
            Console.WriteLine($"Ciudad: {equipo.Ciudad}, Estadio: {equipo.Estadio}");
            Console.WriteLine($"Año Fundación: {equipo.AñoFundacion}");
            Console.WriteLine("\nJUGADORES:");
            
            if (equipo.Jugadores.Count == 0)
            {
                Console.WriteLine("No hay jugadores en este equipo.");
                return;
            }

            foreach (var jugador in equipo.Jugadores.OrderBy(j => j.NumeroCamiseta))
            {
                Console.WriteLine($"  #{jugador.NumeroCamiseta} - {jugador.Nombre} {jugador.Apellido} ({jugador.Posicion})");
            }
        }

        public void BuscarJugadoresPorPosicion(string posicion)
        {
            Console.WriteLine($"\n=== JUGADORES EN POSICIÓN: {posicion} ===");
            var jugadoresFiltrados = jugadores.Values
                .Where(j => j.Posicion.Equals(posicion, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (jugadoresFiltrados.Count == 0)
            {
                Console.WriteLine($"No se encontraron jugadores en la posición {posicion}");
                return;
            }

            foreach (var jugador in jugadoresFiltrados)
            {
                string equipo = ObtenerEquipoDelJugador(jugador.Id);
                Console.WriteLine($"{jugador} - Equipo: {equipo}");
            }
        }

        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n=== ESTADÍSTICAS DEL TORNEO ===");
            Console.WriteLine($"Total de Equipos: {equipos.Count}");
            Console.WriteLine($"Total de Jugadores: {jugadores.Count}");
            
            int jugadoresAsignados = 0;
            foreach (var equipo in equipos.Values)
            {
                jugadoresAsignados += equipo.Jugadores.Count;
            }
            Console.WriteLine($"Jugadores asignados a equipos: {jugadoresAsignados}");
            Console.WriteLine($"Jugadores sin equipo: {jugadores.Count - jugadoresAsignados}");
            
            // Mostrar distribución por posiciones
            var posiciones = jugadores.Values
                .GroupBy(j => j.Posicion)
                .Select(g => new { Posicion = g.Key, Cantidad = g.Count() });
            
            Console.WriteLine("\nDistribución por posiciones:");
            foreach (var pos in posiciones)
            {
                Console.WriteLine($"  {pos.Posicion}: {pos.Cantidad} jugadores");
            }
        }

        private string ObtenerEquipoDelJugador(int jugadorId)
        {
            if (!jugadores.ContainsKey(jugadorId))
                return "No existe";

            var jugador = jugadores[jugadorId];
            foreach (var equipo in equipos.Values)
            {
                if (equipo.Jugadores.Contains(jugador))
                    return equipo.Nombre;
            }
            return "Sin equipo";
        }

        public void MostrarEquiposPorCiudad()
        {
            Console.WriteLine("\n=== EQUIPOS POR CIUDAD ===");
            var equiposPorCiudad = equipos.Values
                .GroupBy(e => e.Ciudad)
                .OrderBy(g => g.Key);

            foreach (var grupo in equiposPorCiudad)
            {
                Console.WriteLine($"\n{grupo.Key}:");
                foreach (var equipo in grupo)
                {
                    Console.WriteLine($"  - {equipo.Nombre}");
                }
            }
        }

        public void MostrarJugadoresPorEdad()
        {
            Console.WriteLine("\n=== JUGADORES POR RANGO DE EDAD ===");
            var juveniles = jugadores.Values.Where(j => j.Edad < 20);
            var adultos = jugadores.Values.Where(j => j.Edad >= 20 && j.Edad < 30);
            var veteranos = jugadores.Values.Where(j => j.Edad >= 30);

            Console.WriteLine($"Juveniles (<20): {juveniles.Count()}");
            Console.WriteLine($"Adultos (20-29): {adultos.Count()}");
            Console.WriteLine($"Veteranos (30+): {veteranos.Count()}");
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            TorneoFutbolManager torneo = new TorneoFutbolManager();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n=== SISTEMA DE REGISTRO TORNEO DE FÚTBOL ===");
                Console.WriteLine("1. Agregar Equipo");
                Console.WriteLine("2. Agregar Jugador");
                Console.WriteLine("3. Asignar Jugador a Equipo");
                Console.WriteLine("4. Remover Jugador de Equipo");
                Console.WriteLine("5. Eliminar Equipo");
                Console.WriteLine("6. Mostrar Todos los Equipos");
                Console.WriteLine("7. Mostrar Todos los Jugadores");
                Console.WriteLine("8. Mostrar Equipo con sus Jugadores");
                Console.WriteLine("9. Buscar Jugadores por Posición");
                Console.WriteLine("10. Mostrar Estadísticas del Torneo");
                Console.WriteLine("11. Mostrar Equipos por Ciudad");
                Console.WriteLine("12. Mostrar Jugadores por Edad");
                Console.WriteLine("13. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarEquipoMenu(torneo);
                        break;
                    case "2":
                        AgregarJugadorMenu(torneo);
                        break;
                    case "3":
                        AsignarJugadorMenu(torneo);
                        break;
                    case "4":
                        RemoverJugadorMenu(torneo);
                        break;
                    case "5":
                        EliminarEquipoMenu(torneo);
                        break;
                    case "6":
                        torneo.MostrarTodosLosEquipos();
                        break;
                    case "7":
                        torneo.MostrarTodosLosJugadores();
                        break;
                    case "8":
                        MostrarEquipoDetalleMenu(torneo);
                        break;
                    case "9":
                        BuscarPorPosicionMenu(torneo);
                        break;
                    case "10":
                        torneo.MostrarEstadisticas();
                        break;
                    case "11":
                        torneo.MostrarEquiposPorCiudad();
                        break;
                    case "12":
                        torneo.MostrarJugadoresPorEdad();
                        break;
                    case "13":
                        salir = true;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void AgregarEquipoMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVO EQUIPO ===");
            
            Console.Write("Nombre del equipo: ");
            string nombre = Console.ReadLine();
            
            Console.Write("Ciudad: ");
            string ciudad = Console.ReadLine();
            
            Console.Write("Estadio: ");
            string estadio = Console.ReadLine();
            
            Console.Write("Año de fundación: ");
            if (int.TryParse(Console.ReadLine(), out int año))
            {
                torneo.AgregarEquipo(nombre, ciudad, estadio, año);
            }
            else
            {
                Console.WriteLine("Año inválido.");
            }
        }

        static void AgregarJugadorMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR NUEVO JUGADOR ===");
            
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            
            Console.Write("Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("Edad inválida.");
                return;
            }
            
            Console.Write("Posición (Arquero/Defensa/Mediocampista/Delantero): ");
            string posicion = Console.ReadLine();
            
            Console.Write("Número de camiseta: ");
            if (!int.TryParse(Console.ReadLine(), out int numero))
            {
                Console.WriteLine("Número inválido.");
                return;
            }
            
            Console.Write("Email: ");
            string email = Console.ReadLine();
            
            torneo.AgregarJugador(nombre, apellido, edad, posicion, numero, email);
        }

        static void AsignarJugadorMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== ASIGNAR JUGADOR A EQUIPO ===");
            
            Console.Write("ID del jugador: ");
            if (!int.TryParse(Console.ReadLine(), out int jugadorId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            Console.Write("ID del equipo: ");
            if (!int.TryParse(Console.ReadLine(), out int equipoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            torneo.AsignarJugadorAEquipo(jugadorId, equipoId);
        }

        static void RemoverJugadorMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== REMOVER JUGADOR DE EQUIPO ===");
            
            Console.Write("ID del jugador: ");
            if (!int.TryParse(Console.ReadLine(), out int jugadorId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            torneo.RemoverJugadorDeEquipo(jugadorId);
        }

        static void EliminarEquipoMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== ELIMINAR EQUIPO ===");
            
            Console.Write("ID del equipo a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int equipoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            torneo.EliminarEquipo(equipoId);
        }

        static void MostrarEquipoDetalleMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== MOSTRAR EQUIPO CON JUGADORES ===");
            
            Console.Write("ID del equipo: ");
            if (!int.TryParse(Console.ReadLine(), out int equipoId))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            
            torneo.MostrarEquipoConJugadores(equipoId);
        }

        static void BuscarPorPosicionMenu(TorneoFutbolManager torneo)
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR JUGADORES POR POSICIÓN ===");
            
            Console.Write("Posición a buscar: ");
            string posicion = Console.ReadLine();
            
            torneo.BuscarJugadoresPorPosicion(posicion);
        }
    }
}
