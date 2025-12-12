// See https://aka.ms/new-console-template for more information
using System;

namespace GestionEstudiantes
{
    /// <summary>
    /// Clase principal que representa a un Estudiante
    /// </summary>
    public class Estudiante
    {
        // Campos privados
        private int id;
        private string nombres;
        private string apellidos;
        private string direccion;
        private string[] telefonos; // Array para almacenar números de teléfono
        
        /// <summary>
        /// Propiedad para el ID del estudiante
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        
        /// <summary>
        /// Propiedad para los nombres del estudiante
        /// </summary>
        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }
        
        /// <summary>
        /// Propiedad para los apellidos del estudiante
        /// </summary>
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }
        
        /// <summary>
        /// Propiedad para la dirección del estudiante
        /// </summary>
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }
        
        /// <summary>
        /// Constructor de la clase Estudiante
        /// </summary>
        public Estudiante(int id, string nombres, string apellidos, string direccion)
        {
            this.id = id;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.telefonos = new string[3]; // Inicializa el array con capacidad para 3 teléfonos
        }
        
        /// <summary>
        /// Método para establecer un número de teléfono en una posición específica del array
        /// </summary>
        /// <param name="indice">Posición en el array (0-2)</param>
        /// <param name="telefono">Número de teléfono a almacenar</param>
        public void EstablecerTelefono(int indice, string telefono)
        {
            if (indice >= 0 && indice < telefonos.Length)
            {
                telefonos[indice] = telefono;
            }
            else
            {
                Console.WriteLine("Índice fuera de rango. Debe estar entre 0 y 2.");
            }
        }
        
        /// <summary>
        /// Método para obtener un número de teléfono del array
        /// </summary>
        /// <param name="indice">Posición en el array (0-2)</param>
        /// <returns>Número de teléfono en la posición indicada</returns>
        public string ObtenerTelefono(int indice)
        {
            if (indice >= 0 && indice < telefonos.Length)
            {
                return telefonos[indice] ?? "No asignado";
            }
            else
            {
                return "Índice no válido";
            }
        }
        
        /// <summary>
        /// Método para mostrar todos los datos del estudiante
        /// </summary>
        public void MostrarInformacion()
        {
            Console.WriteLine("\n=== INFORMACIÓN DEL ESTUDIANTE ===");
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Nombre completo: {nombres} {apellidos}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine("Teléfonos registrados:");
            
            // Recorrer el array de teléfonos usando un bucle for
            for (int i = 0; i < telefonos.Length; i++)
            {
                Console.WriteLine($"  Teléfono {i + 1}: {ObtenerTelefono(i)}");
            }
            Console.WriteLine("=================================\n");
        }
        
        /// <summary>
        /// Método para mostrar todos los teléfonos usando foreach
        /// </summary>
        public void MostrarTelefonos()
        {
            Console.WriteLine("Lista de teléfonos:");
            int contador = 1;
            
            // Recorrer el array usando foreach
            foreach (string telefono in telefonos)
            {
                Console.WriteLine($"  {contador}. {telefono ?? "No asignado"}");
                contador++;
            }
        }
    }
    
    /// <summary>
    /// Clase principal del programa que contiene el método Main
    /// </summary>
    class Program
    {
        /// <summary>
        /// Método principal - Punto de entrada del programa
        /// </summary>
        static void Main(string[] args)
        {
            Console.Title = "Sistema de Gestión de Estudiantes";
            Console.WriteLine("=== SISTEMA DE REGISTRO DE ESTUDIANTES ===\n");
            
            // Crear un objeto Estudiante usando el constructor
            Estudiante estudiante1 = new Estudiante(
                id: 1001,
                nombres: "Juan Carlos",
                apellidos: "Pérez González",
                direccion: "Av. Principal 123, Ciudad"
            );
            
            // Establecer los números de teléfono usando el array
            estudiante1.EstablecerTelefono(0, "0987654321");
            estudiante1.EstablecerTelefono(1, "022345678");
            estudiante1.EstablecerTelefono(2, "0991234567");
            
            // Mostrar la información completa del estudiante
            estudiante1.MostrarInformacion();
            
            // Demostrar acceso individual a los teléfonos
            Console.WriteLine("Acceso individual a teléfonos:");
            Console.WriteLine($"Teléfono 1: {estudiante1.ObtenerTelefono(0)}");
            Console.WriteLine($"Teléfono 3: {estudiante1.ObtenerTelefono(2)}");
            
            // Intentar acceder a un índice fuera de rango
            Console.WriteLine($"\nIntento de acceso a índice inválido: {estudiante1.ObtenerTelefono(5)}");
            
            // Crear un segundo estudiante
            Console.WriteLine("\n\n=== REGISTRO DE SEGUNDO ESTUDIANTE ===");
            Estudiante estudiante2 = new Estudiante(
                id: 1002,
                nombres: "María José",
                apellidos: "Rodríguez López",
                direccion: "Calle Secundaria 456, Pueblo"
            );
            
            // Establecer solo dos teléfonos (dejando uno sin asignar)
            estudiante2.EstablecerTelefono(0, "0978123456");
            estudiante2.EstablecerTelefono(2, "023456789");
            
            estudiante2.MostrarInformacion();
            
            // Mostrar solo los teléfonos usando el método específico
            Console.WriteLine("Mostrando solo teléfonos del segundo estudiante:");
            estudiante2.MostrarTelefonos();
            
            // Demostración de arreglo de objetos
            Console.WriteLine("\n\n=== GESTIÓN DE MÚLTIPLES ESTUDIANTES ===");
            
            // Crear un array de objetos Estudiante
            Estudiante[] listaEstudiantes = new Estudiante[2];
            listaEstudiantes[0] = estudiante1;
            listaEstudiantes[1] = estudiante2;
            
            // Mostrar información de todos los estudiantes usando el array
            Console.WriteLine("Lista completa de estudiantes registrados:");
            for (int i = 0; i < listaEstudiantes.Length; i++)
            {
                Console.WriteLine($"\nEstudiante {i + 1}:");
                Console.WriteLine($"  Nombre: {listaEstudiantes[i].Nombres} {listaEstudiantes[i].Apellidos}");
                Console.WriteLine($"  Teléfono principal: {listaEstudiantes[i].ObtenerTelefono(0)}");
            }
            
            Console.WriteLine("\n\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}