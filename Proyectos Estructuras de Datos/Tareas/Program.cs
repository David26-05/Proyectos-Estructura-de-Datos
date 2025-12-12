// See https://aka.ms/new-console-template for more information
using System;

namespace FigurasGeometricas
{
    /// <summary>
    /// Clase que representa un Círculo
    /// </summary>
    public class Circulo
    {
        // Radio es una propiedad privada que encapsula el dato primitivo double
        // Se utiliza para almacenar el radio del círculo en unidades de longitud
        private double radio;

        /// <summary>
        /// Constructor de la clase Círculo
        /// </summary>
        /// <param name="radio">Radio del círculo (debe ser mayor a 0)</param>
        public Circulo(double radio)
        {
            // Validamos que el radio sea mayor a 0
            if (radio <= 0)
            {
                throw new ArgumentException("El radio debe ser mayor a 0", nameof(radio));
            }
            this.radio = radio;
        }

        /// <summary>
        /// Propiedad pública para acceder y modificar el radio del círculo
        /// </summary>
        public double Radio
        {
            get { return radio; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("El radio debe ser mayor a 0", nameof(value));
                }
                radio = value;
            }
        }

        /// <summary>
        /// CalcularArea es un método que devuelve un valor double
        /// Se utiliza para calcular el área de un círculo
        /// La fórmula utilizada es: π * radio²
        /// </summary>
        /// <returns>Área del círculo en unidades cuadradas</returns>
        public double CalcularArea()
        {
            return Math.PI * radio * radio;
        }

        /// <summary>
        /// CalcularPerimetro es un método que devuelve un valor double
        /// Se utiliza para calcular el perímetro (circunferencia) de un círculo
        /// La fórmula utilizada es: 2 * π * radio
        /// </summary>
        /// <returns>Perímetro (circunferencia) del círculo en unidades de longitud</returns>
        public double CalcularPerimetro()
        {
            return 2 * Math.PI * radio;
        }

        /// <summary>
        /// Método para mostrar información del círculo
        /// </summary>
        /// <returns>Cadena con información del círculo</returns>
        public override string ToString()
        {
            return $"Círculo - Radio: {radio:F2}, Área: {CalcularArea():F2}, Perímetro: {CalcularPerimetro():F2}";
        }
    }

    /// <summary>
    /// Clase que representa un Rectángulo
    /// </summary>
    public class Rectangulo
    {
        // Base y altura son propiedades privadas que encapsulan datos primitivos double
        // Se utilizan para almacenar las dimensiones del rectángulo
        private double baseRectangulo;
        private double altura;

        /// <summary>
        /// Constructor de la clase Rectángulo
        /// </summary>
        /// <param name="baseRectangulo">Base del rectángulo (debe ser mayor a 0)</param>
        /// <param name="altura">Altura del rectángulo (debe ser mayor a 0)</param>
        public Rectangulo(double baseRectangulo, double altura)
        {
            // Validamos que ambas dimensiones sean mayores a 0
            if (baseRectangulo <= 0)
            {
                throw new ArgumentException("La base debe ser mayor a 0", nameof(baseRectangulo));
            }
            if (altura <= 0)
            {
                throw new ArgumentException("La altura debe ser mayor a 0", nameof(altura));
            }

            this.baseRectangulo = baseRectangulo;
            this.altura = altura;
        }

        /// <summary>
        /// Propiedad pública para acceder y modificar la base del rectángulo
        /// </summary>
        public double BaseRectangulo
        {
            get { return baseRectangulo; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("La base debe ser mayor a 0", nameof(value));
                }
                baseRectangulo = value;
            }
        }

        /// <summary>
        /// Propiedad pública para acceder y modificar la altura del rectángulo
        /// </summary>
        public double Altura
        {
            get { return altura; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("La altura debe ser mayor a 0", nameof(value));
                }
                altura = value;
            }
        }

        /// <summary>
        /// CalcularArea es un método que devuelve un valor double
        /// Se utiliza para calcular el área de un rectángulo
        /// La fórmula utilizada es: base * altura
        /// </summary>
        /// <returns>Área del rectángulo en unidades cuadradas</returns>
        public double CalcularArea()
        {
            return baseRectangulo * altura;
        }

        /// <summary>
        /// CalcularPerimetro es un método que devuelve un valor double
        /// Se utiliza para calcular el perímetro de un rectángulo
        /// La fórmula utilizada es: 2 * (base + altura)
        /// </summary>
        /// <returns>Perímetro del rectángulo en unidades de longitud</returns>
        public double CalcularPerimetro()
        {
            return 2 * (baseRectangulo + altura);
        }

        /// <summary>
        /// Método para mostrar información del rectángulo
        /// </summary>
        /// <returns>Cadena con información del rectángulo</returns>
        public override string ToString()
        {
            return $"Rectángulo - Base: {baseRectangulo:F2}, Altura: {altura:F2}, " +
                   $"Área: {CalcularArea():F2}, Perímetro: {CalcularPerimetro():F2}";
        }
    }

    /// <summary>
    /// Clase de demostración para probar las figuras geométricas
    /// </summary>
    public class ProgramaDemo
    {
        /// <summary>
        /// Método principal que demuestra el uso de las clases Circulo y Rectangulo
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("=== DEMOSTRACIÓN DE FIGURAS GEOMÉTRICAS ===\n");

            try
            {
                // Crear un círculo con radio 5
                // Instanciamos un objeto de la clase Circulo con radio 5 unidades
                Circulo miCirculo = new Circulo(5.0);
                
                // Mostrar información del círculo
                // El método ToString() muestra el radio, área y perímetro del círculo
                Console.WriteLine(miCirculo.ToString());
                
                // Calcular y mostrar el área del círculo de forma individual
                // Llamamos al método CalcularArea() que retorna el área calculada
                double areaCirculo = miCirculo.CalcularArea();
                Console.WriteLine($"Área del círculo calculada individualmente: {areaCirculo:F2}");
                
                // Calcular y mostrar el perímetro del círculo de forma individual
                // Llamamos al método CalcularPerimetro() que retorna el perímetro calculado
                double perimetroCirculo = miCirculo.CalcularPerimetro();
                Console.WriteLine($"Perímetro del círculo calculado individualmente: {perimetroCirculo:F2}\n");

                // Modificar el radio del círculo usando la propiedad
                // La propiedad Radio valida que el nuevo valor sea mayor a 0
                Console.WriteLine("Modificando el radio del círculo a 7.5...");
                miCirculo.Radio = 7.5;
                Console.WriteLine(miCirculo.ToString() + "\n");

                // Crear un rectángulo con base 4 y altura 6
                // Instanciamos un objeto de la clase Rectangulo con dimensiones 4x6 unidades
                Rectangulo miRectangulo = new Rectangulo(4.0, 6.0);
                
                // Mostrar información del rectángulo
                // El método ToString() muestra la base, altura, área y perímetro del rectángulo
                Console.WriteLine(miRectangulo.ToString());
                
                // Calcular y mostrar el área del rectángulo de forma individual
                // Llamamos al método CalcularArea() que retorna el área calculada
                double areaRectangulo = miRectangulo.CalcularArea();
                Console.WriteLine($"Área del rectángulo calculada individualmente: {areaRectangulo:F2}");
                
                // Calcular y mostrar el perímetro del rectángulo de forma individual
                // Llamamos al método CalcularPerimetro() que retorna el perímetro calculado
                double perimetroRectangulo = miRectangulo.CalcularPerimetro();
                Console.WriteLine($"Perímetro del rectángulo calculado individualmente: {perimetroRectangulo:F2}\n");

                // Modificar las dimensiones del rectángulo usando las propiedades
                // Las propiedades BaseRectangulo y Altura validan que los nuevos valores sean mayores a 0
                Console.WriteLine("Modificando las dimensiones del rectángulo a base 8.5 y altura 3.2...");
                miRectangulo.BaseRectangulo = 8.5;
                miRectangulo.Altura = 3.2;
                Console.WriteLine(miRectangulo.ToString() + "\n");

                // Demostración de validación de datos
                Console.WriteLine("=== DEMOSTRACIÓN DE VALIDACIÓN DE DATOS ===");
                
                try
                {
                    // Intentar crear un círculo con radio negativo (esto lanzará una excepción)
                    // El constructor valida que el radio sea mayor a 0
                    Console.WriteLine("Intentando crear un círculo con radio -2...");
                    Circulo circuloInvalido = new Circulo(-2.0);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error al crear círculo: {ex.Message}");
                }

                try
                {
                    // Intentar crear un rectángulo con base 0 (esto lanzará una excepción)
                    // El constructor valida que ambas dimensiones sean mayores a 0
                    Console.WriteLine("\nIntentando crear un rectángulo con base 0 y altura 5...");
                    Rectangulo rectanguloInvalido = new Rectangulo(0, 5.0);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error al crear rectángulo: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

            Console.WriteLine("\n=== FIN DE LA DEMOSTRACIÓN ===");
            Console.ReadKey();
        }
    }
}