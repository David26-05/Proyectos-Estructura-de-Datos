// See https://aka.ms/new-console-template for more information
using System;
namespace EstacionamientoSistemas
{
    // Clase que representa un vehículo
    public class Vehiculo
    {
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public decimal Precio { get; set; }
        
        public Vehiculo(string placa, string marca, string modelo, int año, decimal precio)
        {
            Placa = placa;
            Marca = marca;
            Modelo = modelo;
            Año = año;
            Precio = precio;
        }
        
        public void MostrarInformacion()
        {
            Console.WriteLine($"Placa: {Placa}");
            Console.WriteLine($"Marca: {Marca}");
            Console.WriteLine($"Modelo: {Modelo}");
            Console.WriteLine($"Año: {Año}");
            Console.WriteLine($"Precio: ${Precio:N2}");
            Console.WriteLine("-----------------------------------");
        }
    }
    // Clase Nodo para la lista enlazada
    public class NodoVehiculo
    {
        public Vehiculo Vehiculo { get; set; }
        public NodoVehiculo Siguiente { get; set; }
        
        public NodoVehiculo(Vehiculo vehiculo)
        {
            Vehiculo = vehiculo;
            Siguiente = null;
        }
    }

    // Clase que gestiona la lista enlazada de vehículos
    public class RegistroEstacionamiento
    {
        private NodoVehiculo cabeza;
        private int contador;
        
        public RegistroEstacionamiento()
        {
            cabeza = null;
            contador = 0;
        }
        
        // a. Agregar vehículo
        public void AgregarVehiculo(string placa, string marca, string modelo, int año, decimal precio)
        {
            // Verificar si la placa ya existe
            if (BuscarVehiculoPorPlaca(placa) != null)
            {
                Console.WriteLine($"Error: Ya existe un vehículo con la placa {placa}");
                return;
            }
            
            Vehiculo nuevoVehiculo = new Vehiculo(placa, marca, modelo, año, precio);
            NodoVehiculo nuevoNodo = new NodoVehiculo(nuevoVehiculo);
            
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                NodoVehiculo actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoNodo;
            }
            
            contador++;
            Console.WriteLine($"\n✓ Vehículo con placa {placa} agregado exitosamente.");
            Console.WriteLine($"Total de vehículos registrados: {contador}\n");
        }
        
        // b. Buscar vehículo por placa
        public Vehiculo BuscarVehiculoPorPlaca(string placa)
        {
            if (cabeza == null)
            {
                return null;
            }
            
            NodoVehiculo actual = cabeza;
            while (actual != null)
            {
                if (actual.Vehiculo.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
                {
                    return actual.Vehiculo;
                }
                actual = actual.Siguiente;
            }
            
            return null;
        }
        
        public void MostrarBusquedaPorPlaca(string placa)
        {
            Vehiculo encontrado = BuscarVehiculoPorPlaca(placa);
            
            if (encontrado != null)
            {
                Console.WriteLine("\n═══════════════════════════════════");
                Console.WriteLine("        VEHÍCULO ENCONTRADO");
                Console.WriteLine("═══════════════════════════════════");
                encontrado.MostrarInformacion();
            }
            else
            {
                Console.WriteLine($"\n✗ No se encontró ningún vehículo con la placa {placa}\n");
            }
        }
        
        // c. Ver vehículos por año
        public void MostrarVehiculosPorAño(int año)
        {
            if (cabeza == null)
            {
                Console.WriteLine($"\nNo hay vehículos registrados en el estacionamiento.\n");
                return;
            }
            
            NodoVehiculo actual = cabeza;
            int encontrados = 0;
            
            Console.WriteLine($"\n═══════════════════════════════════");
            Console.WriteLine($"    VEHÍCULOS DEL AÑO {año}");
            Console.WriteLine($"═══════════════════════════════════");
            
            while (actual != null)
            {
                if (actual.Vehiculo.Año == año)
                {
                    actual.Vehiculo.MostrarInformacion();
                    encontrados++;
                }
                actual = actual.Siguiente;
            }
            
            if (encontrados == 0)
            {
                Console.WriteLine($"No se encontraron vehículos del año {año}\n");
            }
            else
            {
                Console.WriteLine($"Total encontrados: {encontrados}\n");
            }
        }
        
        // d. Ver todos los vehículos registrados
        public void MostrarTodosLosVehiculos()
        {
            if (cabeza == null)
            {
                Console.WriteLine("\nNo hay vehículos registrados en el estacionamiento.\n");
                return;
            }
            
            NodoVehiculo actual = cabeza;
            int numero = 1;
            
            Console.WriteLine("\n══════════════════════════════════════════════════");
            Console.WriteLine("       REGISTRO COMPLETO DE VEHÍCULOS");
            Console.WriteLine("       Área de Ingeniería de Sistemas");
            Console.WriteLine("══════════════════════════════════════════════════\n");
            
            while (actual != null)
            {
                Console.WriteLine($"Vehículo #{numero}:");
                actual.Vehiculo.MostrarInformacion();
                actual = actual.Siguiente;
                numero++;
            }
            
            Console.WriteLine($"\nTOTAL DE VEHÍCULOS REGISTRADOS: {contador}\n");
        }
        
        // e. Eliminar vehículo registrado
        public bool EliminarVehiculo(string placa)
        {
            if (cabeza == null)
            {
                Console.WriteLine($"\nNo hay vehículos registrados.\n");
                return false;
            }
            
            // Caso especial: el primer nodo
            if (cabeza.Vehiculo.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"\n✓ Vehículo con placa {placa} eliminado exitosamente.");
                cabeza = cabeza.Siguiente;
                contador--;
                Console.WriteLine($"Total de vehículos restantes: {contador}\n");
                return true;
            }
            
            // Buscar el nodo a eliminar
            NodoVehiculo actual = cabeza;
            NodoVehiculo anterior = null;
            
            while (actual != null && !actual.Vehiculo.Placa.Equals(placa, StringComparison.OrdinalIgnoreCase))
            {
                anterior = actual;
                actual = actual.Siguiente;
            }
            
            // Si no se encontró el vehículo
            if (actual == null)
            {
                Console.WriteLine($"\n✗ No se encontró ningún vehículo con la placa {placa}\n");
                return false;
            }
            
            // Eliminar el nodo
            anterior.Siguiente = actual.Siguiente;
            contador--;
            
            Console.WriteLine($"\n✓ Vehículo con placa {placa} eliminado exitosamente.");
            Console.WriteLine($"Total de vehículos restantes: {contador}\n");
            return true;
        }
        
        // Método auxiliar para obtener la cantidad de vehículos
        public int ObtenerCantidadVehiculos()
        {
            return contador;
        }
    }

    // Programa principal con menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            RegistroEstacionamiento registro = new RegistroEstacionamiento();
            bool salir = false;
            
            // Agregar algunos datos de ejemplo
            registro.AgregarVehiculo("ABC123", "Toyota", "Corolla", 2020, 25000);
            registro.AgregarVehiculo("XYZ789", "Honda", "Civic", 2021, 27000);
            registro.AgregarVehiculo("DEF456", "Ford", "Focus", 2020, 22000);
            registro.AgregarVehiculo("GHI789", "Chevrolet", "Spark", 2019, 15000);
            registro.AgregarVehiculo("JKL012", "Toyota", "RAV4", 2021, 35000);
            
            Console.WriteLine("══════════════════════════════════════════════════");
            Console.WriteLine("    SISTEMA DE REGISTRO DE ESTACIONAMIENTO");
            Console.WriteLine("    Área de Ingeniería de Sistemas - Universidad");
            Console.WriteLine("══════════════════════════════════════════════════\n");
            
            while (!salir)
            {
                MostrarMenu();
                Console.Write("\nSeleccione una opción: ");
                
                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    Console.Clear();
                    Console.WriteLine("══════════════════════════════════════════════════");
                    Console.WriteLine("    SISTEMA DE REGISTRO DE ESTACIONAMIENTO");
                    Console.WriteLine("══════════════════════════════════════════════════\n");
                    
                    switch (opcion)
                    {
                        case 1:
                            AgregarNuevoVehiculo(registro);
                            break;
                        case 2:
                            BuscarVehiculo(registro);
                            break;
                        case 3:
                            MostrarPorAño(registro);
                            break;
                        case 4:
                            registro.MostrarTodosLosVehiculos();
                            break;
                        case 5:
                            EliminarVehiculo(registro);
                            break;
                        case 6:
                            Console.WriteLine($"\nTotal de vehículos registrados: {registro.ObtenerCantidadVehiculos()}\n");
                            break;
                        case 7:
                            salir = true;
                            Console.WriteLine("\n¡Gracias por usar el sistema de registro!");
                            Console.WriteLine("Saliendo del programa...\n");
                            break;
                        default:
                            Console.WriteLine("\n✗ Opción no válida. Intente nuevamente.\n");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n✗ Entrada no válida. Por favor ingrese un número.\n");
                }
            }
        }
        
        static void MostrarMenu()
        {
            Console.WriteLine("\n──────────────────────────────────────────");
            Console.WriteLine("                MENÚ PRINCIPAL");
            Console.WriteLine("──────────────────────────────────────────");
            Console.WriteLine("1. Agregar vehículo");
            Console.WriteLine("2. Buscar vehículo por placa");
            Console.WriteLine("3. Ver vehículos por año");
            Console.WriteLine("4. Ver todos los vehículos registrados");
            Console.WriteLine("5. Eliminar vehículo registrado");
            Console.WriteLine("6. Ver cantidad total de vehículos");
            Console.WriteLine("7. Salir");
            Console.WriteLine("──────────────────────────────────────────");
        }
        
        static void AgregarNuevoVehiculo(RegistroEstacionamiento registro)
        {
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("        AGREGAR NUEVO VEHÍCULO");
            Console.WriteLine("═══════════════════════════════════\n");
            
            Console.Write("Placa: ");
            string placa = Console.ReadLine();
            
            Console.Write("Marca: ");
            string marca = Console.ReadLine();
            
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine();
            
            Console.Write("Año: ");
            if (int.TryParse(Console.ReadLine(), out int año))
            {
                Console.Write("Precio: $");
                if (decimal.TryParse(Console.ReadLine(), out decimal precio))
                {
                    registro.AgregarVehiculo(placa, marca, modelo, año, precio);
                }
                else
                {
                    Console.WriteLine("\n✗ Precio no válido.\n");
                }
            }
            else
            {
                Console.WriteLine("\n✗ Año no válido.\n");
            }
        }
        
        static void BuscarVehiculo(RegistroEstacionamiento registro)
        {
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("        BUSCAR VEHÍCULO");
            Console.WriteLine("═══════════════════════════════════\n");
            
            Console.Write("Ingrese la placa a buscar: ");
            string placa = Console.ReadLine();
            
            registro.MostrarBusquedaPorPlaca(placa);
        }
        
        static void MostrarPorAño(RegistroEstacionamiento registro)
        {
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("        VEHÍCULOS POR AÑO");
            Console.WriteLine("═══════════════════════════════════\n");
            
            Console.Write("Ingrese el año a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int año))
            {
                registro.MostrarVehiculosPorAño(año);
            }
            else
            {
                Console.WriteLine("\n✗ Año no válido.\n");
            }
        }
        
        static void EliminarVehiculo(RegistroEstacionamiento registro)
        {
            Console.WriteLine("\n═══════════════════════════════════");
            Console.WriteLine("        ELIMINAR VEHÍCULO");
            Console.WriteLine("═══════════════════════════════════\n");
            
            Console.Write("Ingrese la placa del vehículo a eliminar: ");
            string placa = Console.ReadLine();
            
            registro.EliminarVehiculo(placa);
        }
    }
}
