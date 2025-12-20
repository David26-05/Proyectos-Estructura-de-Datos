// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace AgendaTelefonica
{
    // ==================== CLASES PRINCIPALES ====================
    
    /// <summary>
    /// Esta clase representa a un contacto en la agenda telefónica
    /// </summary>
    public class Contacto
    {
        // Propiedades del contacto
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Categoria { get; set; } // Ej: Puede ser Familia, Amigos, Trabajo
        
        /// <summary>
        /// este es el constructor de la clase Contacto
        /// </summary>
        public Contacto(string nombre, string apellido, string telefono, string email, string categoria)
        {
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Email = email;
            Categoria = categoria;
        }
        
        /// <summary>
        /// Muestra la información del contacto formateada
        /// </summary>
        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre} {Apellido}");
            Console.WriteLine($"Teléfono: {Telefono}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Categoría: {Categoria}");
            Console.WriteLine(new string('-', 40));
        }
        
        /// <summary>
        /// esta linea devuelve una representación en cadena del contacto
        /// </summary>
        public override string ToString()
        {
            return $"{Nombre} {Apellido} - {Telefono} - {Categoria}";
        }
    }
    
    /// <summary>
    /// Esta es la clase principal que gestiona la agenda telefónica
    /// </summary>
    public class Agenda
    {
        // Lista para almacenar los contactos (estructura de datos principal)
        private List<Contacto> contactos;
        
        /// <summary>
        /// Constructor de la clase Agenda
        /// </summary>
        public Agenda()
        {
            contactos = new List<Contacto>();
            InicializarDatosEjemplo();
        }
        
        // ==================== MÉTODOS PRINCIPALES ====================
        
        /// <summary>
        /// Agrega un nuevo contacto a la agenda
        /// </summary>
        public void AgregarContacto(Contacto nuevoContacto)
        {
            contactos.Add(nuevoContacto);
            Console.WriteLine($"\n✓ Contacto {nuevoContacto.Nombre} agregado exitosamente.");
        }
        
        /// <summary>
        /// Elimina un contacto por su número de teléfono
        /// </summary>
        public bool EliminarContacto(string telefono)
        {
            for (int i = 0; i < contactos.Count; i++)
            {
                if (contactos[i].Telefono == telefono)
                {
                    string nombreEliminado = contactos[i].Nombre;
                    contactos.RemoveAt(i);
                    Console.WriteLine($"\n✓ Contacto {nombreEliminado} eliminado exitosamente.");
                    return true;
                }
            }
            Console.WriteLine($"\n✗ No se encontró ningún contacto con el teléfono {telefono}.");
            return false;
        }
        
        /// <summary>
        /// Busca contactos por nombre (búsqueda parcial)
        /// </summary>
        public List<Contacto> BuscarPorNombre(string nombre)
        {
            List<Contacto> resultados = new List<Contacto>();
            
            foreach (Contacto contacto in contactos)
            {
                if (contacto.Nombre.ToLower().Contains(nombre.ToLower()) || 
                    contacto.Apellido.ToLower().Contains(nombre.ToLower()))
                {
                    resultados.Add(contacto);
                }
            }
            
            return resultados;
        }
        
        /// <summary>
        /// Busca contactos por categoría
        /// </summary>
        public List<Contacto> BuscarPorCategoria(string categoria)
        {
            List<Contacto> resultados = new List<Contacto>();
            
            foreach (Contacto contacto in contactos)
            {
                if (contacto.Categoria.ToLower() == categoria.ToLower())
                {
                    resultados.Add(contacto);
                }
            }
            
            return resultados;
        }
        
        /// <summary>
        /// Muestra todos los contactos de la agenda
        /// </summary>
        public void MostrarTodosLosContactos()
        {
            if (contactos.Count == 0)
            {
                Console.WriteLine("\nLa agenda está vacía.");
                return;
            }
            
            Console.WriteLine($"\n📋 AGENDA TELEFÓNICA ({contactos.Count} contactos):");
            Console.WriteLine(new string('=', 60));
            
            for (int i = 0; i < contactos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {contactos[i]}");
            }
        }
        
        /// <summary>
        /// Muestra estadísticas de la agenda
        /// </summary>
        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n📊 ESTADÍSTICAS DE LA AGENDA:");
            Console.WriteLine(new string('-', 40));
            Console.WriteLine($"Total de contactos: {contactos.Count}");
            
            // Cuenta los contactos por categoría usando un diccionario
            Dictionary<string, int> conteoPorCategoria = new Dictionary<string, int>();
            
            foreach (Contacto contacto in contactos)
            {
                if (conteoPorCategoria.ContainsKey(contacto.Categoria))
                {
                    conteoPorCategoria[contacto.Categoria]++;
                }
                else
                {
                    conteoPorCategoria[contacto.Categoria] = 1;
                }
            }
            
            Console.WriteLine("\nContactos por categoría:");
            foreach (var categoria in conteoPorCategoria)
            {
                Console.WriteLine($"  {categoria.Key}: {categoria.Value} contactos");
            }
        }
        
        /// <summary>
        /// Inicializa la agenda con datos 
        /// </summary>
        private void InicializarDatosEjemplo()
        {
            // Matriz con datos  (estructura de datos secundaria)
            string[,] datos = {
                {"David", "Garcia", "0994567809", "davidgarciavile16@gmail.com", "Familia"},
                {"María", "Perez", "0987345621", "mariaper23@gmail.com", "Amigos"},
                {"Antonio", "Avilez", "0967321455", "antonioavi@gmail.com", "Trabajo"},
                {"Noemi", "Pazmiño", "0969324156", "noepaz234@gmail.com", "Amigos"},
                {"Abel", "Villalva", "0989921367", "abelvilla361@gmail.com", "Familia"}
            };
            
            // Agregar contactos de ejemplo
            for (int i = 0; i < datos.GetLength(0); i++)
            {
                Contacto contacto = new Contacto(
                    datos[i, 0],
                    datos[i, 1],
                    datos[i, 2],
                    datos[i, 3],
                    datos[i, 4]
                );
                contactos.Add(contacto);
            }
        }
    }
    
    // ==================== PROGRAMA PRINCIPAL ====================
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Agenda Telefónica - Sistema de Gestión";
            
            // Crear una instancia de la agenda
            Agenda miAgenda = new Agenda();
            
            bool salir = false;
            
            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();
                
                switch (opcion)
                {
                    case "1":
                        // Mostrar todos los contactos
                        miAgenda.MostrarTodosLosContactos();
                        break;
                        
                    case "2":
                        // Agregar nuevo contacto
                        AgregarNuevoContacto(miAgenda);
                        break;
                        
                    case "3":
                        // Buscar contacto por nombre
                        BuscarContactoPorNombre(miAgenda);
                        break;
                        
                    case "4":
                        // Buscar por categoría
                        BuscarPorCategoria(miAgenda);
                        break;
                        
                    case "5":
                        // Eliminar contacto
                        EliminarContacto(miAgenda);
                        break;
                        
                    case "6":
                        // Mostrar estadísticas
                        miAgenda.MostrarEstadisticas();
                        break;
                        
                    case "7":
                        // Salir del programa
                        salir = true;
                        Console.WriteLine("\n¡Gracias por usar la Agenda Telefónica!");
                        break;
                        
                    default:
                        Console.WriteLine("\n✗ Opción no válida. Intente nuevamente.");
                        break;
                }
                
                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
        
        /// <summary>
        /// Muestra el menú principal
        /// </summary>
        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       AGENDA TELEFÓNICA - MENÚ         ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Mostrar todos los contactos");
            Console.WriteLine("2. Agregar un contacto nuevo ");
            Console.WriteLine("3. Buscar el contacto por nombre");
            Console.WriteLine("4. Buscar el contactos por categoría");
            Console.WriteLine("5. Eliminar contacto");
            Console.WriteLine("6. Mostrar estadísticas");
            Console.WriteLine("7. Salir");
            Console.WriteLine("========================================");
            Console.Write("Seleccione una opción: ");
        }
        
        /// <summary>
        /// Método para agregar un nuevo contacto
        /// </summary>
        static void AgregarNuevoContacto(Agenda agenda)
        {
            Console.WriteLine("\n📝 AGREGAR NUEVO CONTACTO");
            Console.WriteLine(new string('-', 30));
            
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            
            Console.Write("Teléfono: ");
            string telefono = Console.ReadLine();
            
            Console.Write("Email: ");
            string email = Console.ReadLine();
            
            Console.Write("Categoría (Familia/Amigos/Trabajo/Otro): ");
            string categoria = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(telefono))
            {
                Console.WriteLine("\n✗ Error: Nombre y teléfono son campos obligatorios.");
                return;
            }
            
            Contacto nuevoContacto = new Contacto(nombre, apellido, telefono, email, categoria);
            agenda.AgregarContacto(nuevoContacto);
        }
        
        /// <summary>
        /// Método para buscar contacto por nombre
        /// </summary>
        static void BuscarContactoPorNombre(Agenda agenda)
        {
            Console.Write("\n🔍 Ingrese nombre o apellido a buscar: ");
            string busqueda = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                Console.WriteLine("\n✗ Debe ingresar un término de búsqueda.");
                return;
            }
            
            List<Contacto> resultados = agenda.BuscarPorNombre(busqueda);
            
            if (resultados.Count > 0)
            {
                Console.WriteLine($"\n✅ Se encontraron {resultados.Count} resultado(s):");
                Console.WriteLine(new string('-', 50));
                
                foreach (Contacto contacto in resultados)
                {
                    contacto.MostrarInformacion();
                }
            }
            else
            {
                Console.WriteLine($"\n✗ No se encontraron contactos con '{busqueda}'.");
            }
        }
        
        /// <summary>
        /// Método para buscar contactos por categoría
        /// </summary>
        static void BuscarPorCategoria(Agenda agenda)
        {
            Console.WriteLine("\n🏷️ CATEGORÍAS DISPONIBLES:");
            Console.WriteLine("1. Familia");
            Console.WriteLine("2. Amigos");
            Console.WriteLine("3. Trabajo");
            Console.WriteLine("4. Otro");
            Console.Write("\nSeleccione categoría (1-4) o escriba una: ");
            
            string entrada = Console.ReadLine();
            string categoria = "";
            
            switch (entrada)
            {
                case "1": categoria = "Familia"; break;
                case "2": categoria = "Amigos"; break;
                case "3": categoria = "Trabajo"; break;
                case "4": categoria = "Otro"; break;
                default: categoria = entrada; break;
            }
            
            if (string.IsNullOrWhiteSpace(categoria))
            {
                Console.WriteLine("\n✗ Debe especificar una categoría.");
                return;
            }
            
            List<Contacto> resultados = agenda.BuscarPorCategoria(categoria);
            
            if (resultados.Count > 0)
            {
                Console.WriteLine($"\n✅ Contactos en categoría '{categoria}' ({resultados.Count}):");
                Console.WriteLine(new string('-', 50));
                
                foreach (Contacto contacto in resultados)
                {
                    contacto.MostrarInformacion();
                }
            }
            else
            {
                Console.WriteLine($"\n✗ No hay contactos en la categoría '{categoria}'.");
            }
        }
        
        /// <summary>
        /// Método para eliminar un contacto
        /// </summary>
        static void EliminarContacto(Agenda agenda)
        {
            agenda.MostrarTodosLosContactos();
            
            Console.Write("\n🗑️ Ingrese el teléfono del contacto a eliminar: ");
            string telefono = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(telefono))
            {
                Console.WriteLine("\n✗ Debe ingresar un número de teléfono.");
                return;
            }
            
            Console.Write($"¿Está seguro de eliminar el contacto con teléfono {telefono}? (S/N): ");
            string confirmacion = Console.ReadLine();
            
            if (confirmacion.ToUpper() == "S")
            {
                agenda.EliminarContacto(telefono);
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }
        }
    }
}