// See https://aka.ms/new-console-template for more information
using System;

namespace PilasEjercicios
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EJERCICIO 1: VERIFICACIÓN DE PARÉNTESIS BALANCEADOS ===");
            
            string expresion1 = "{7 + (8 * 5) - [(9 - 7) + (4 + 1)]}";
            Console.WriteLine($"Expresión: {expresion1}");
            Console.WriteLine($"Resultado: {BalanceadorParentesis.VerificarBalance(expresion1)}");
            
            Console.WriteLine("\n--- Otras pruebas ---");
            
            string expresion2 = "((a + b) * (c - d)";
            Console.WriteLine($"\nExpresión: {expresion2}");
            Console.WriteLine($"Resultado: {BalanceadorParentesis.VerificarBalance(expresion2)}");
            
            string expresion3 = "[{a + b} * (c + d)]";
            Console.WriteLine($"\nExpresión: {expresion3}");
            Console.WriteLine($"Resultado: {BalanceadorParentesis.VerificarBalance(expresion3)}");
            
            Console.WriteLine("\n\n=== EJERCICIO 2: TORRES DE HANOI ===");
            
            // Resolver con 3 discos (puedes cambiar este número)
            int numeroDiscos = 3;
            TorresHanoi hanoi = new TorresHanoi(numeroDiscos);
            hanoi.Resolver();
            
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }

//verificacion de Parentesis Balanceados
 class BalanceadorParentesis
    {
        public static string VerificarBalance(string expresion)
        {
            Stack<char> pila = new Stack<char>();
            
            foreach (char caracter in expresion)
            {
                // Si es un símbolo de apertura, lo apilamos
                if (caracter == '(' || caracter == '[' || caracter == '{')
                {
                    pila.Push(caracter);
                }
                // Si es un símbolo de cierre, verificamos
                else if (caracter == ')' || caracter == ']' || caracter == '}')
                {
                    // Si la pila está vacía, hay un cierre sin apertura
                    if (pila.Count == 0)
                    {
                        return "Fórmula NO balanceada: Cierre sin apertura.";
                    }
                    
                    char tope = pila.Pop();
                    
                    // Verificamos que el cierre corresponda con la apertura
                    if (!EsParCorrespondiente(tope, caracter))
                    {
                        return $"Fórmula NO balanceada: '{tope}' no coincide con '{caracter}'.";
                    }
                }
            }
            
            // Al final, la pila debe estar vacía si todo está balanceado
            return pila.Count == 0 
                ? "Fórmula balanceada." 
                : "Fórmula NO balanceada: Símbolos de apertura sin cerrar.";
        }
        
        private static bool EsParCorrespondiente(char apertura, char cierre)
        {
            return (apertura == '(' && cierre == ')') ||
                   (apertura == '[' && cierre == ']') ||
                   (apertura == '{' && cierre == '}');
        }
    }

// Resolucion de las torres de Hanoi con pilas
class TorresHanoi
    {
        private Stack<int> torreA;
        private Stack<int> torreB;
        private Stack<int> torreC;
        private int totalDiscos;
        private int movimientos;
        
        public TorresHanoi(int discos)
        {
            totalDiscos = discos;
            movimientos = 0;
            
            // Inicializar las torres como pilas
            torreA = new Stack<int>();
            torreB = new Stack<int>();
            torreC = new Stack<int>();
            
            // Colocar discos en la torre A (el disco más grande en la base)
            for (int i = discos; i >= 1; i--)
            {
                torreA.Push(i);
            }
        }
        
        public void Resolver()
        {
            Console.WriteLine("=== INICIO DEL JUEGO DE LAS TORRES DE HANOI ===");
            Console.WriteLine($"Número de discos: {totalDiscos}");
            Console.WriteLine("Torre A: Inicio, Torre B: Auxiliar, Torre C: Destino");
            Console.WriteLine();
            
            MostrarEstadoTorres();
            Console.WriteLine();
            
            // Resolver recursivamente
            MoverDiscos(totalDiscos, 'A', 'C', 'B');
            
            Console.WriteLine($"\n=== FIN DEL JUEGO ===");
            Console.WriteLine($"Total de movimientos realizados: {movimientos}");
            Console.WriteLine($"Movimientos mínimos teóricos: {Math.Pow(2, totalDiscos) - 1}");
        }
        
        private void MoverDiscos(int n, char origen, char destino, char auxiliar)
        {
            if (n > 0)
            {
                // Mover n-1 discos de origen a auxiliar
                MoverDiscos(n - 1, origen, auxiliar, destino);
                
                // Mover el disco n de origen a destino
                MoverDisco(origen, destino);
                
                // Mostrar estado actual
                MostrarEstadoTorres();
                Console.WriteLine();
                
                // Mover n-1 discos de auxiliar a destino
                MoverDiscos(n - 1, auxiliar, destino, origen);
            }
        }
        
        private void MoverDisco(char origen, char destino)
        {
            int disco;
            
            // Obtener el disco de la torre de origen
            switch (origen)
            {
                case 'A': disco = torreA.Pop(); break;
                case 'B': disco = torreB.Pop(); break;
                case 'C': disco = torreC.Pop(); break;
                default: throw new ArgumentException("Torre inválida");
            }
            
            // Colocar el disco en la torre de destino
            switch (destino)
            {
                case 'A': 
                    if (torreA.Count > 0 && torreA.Peek() <= disco)
                        throw new InvalidOperationException("Movimiento inválido: disco más grande sobre uno más pequeño");
                    torreA.Push(disco);
                    break;
                case 'B':
                    if (torreB.Count > 0 && torreB.Peek() <= disco)
                        throw new InvalidOperationException("Movimiento inválido: disco más grande sobre uno más pequeño");
                    torreB.Push(disco);
                    break;
                case 'C':
                    if (torreC.Count > 0 && torreC.Peek() <= disco)
                        throw new InvalidOperationException("Movimiento inválido: disco más grande sobre uno más pequeño");
                    torreC.Push(disco);
                    break;
            }
            
            movimientos++;
            Console.WriteLine($"Movimiento {movimientos}: Mover disco {disco} de torre {origen} a torre {destino}");
        }
        
        private void MostrarEstadoTorres()
        {
            Console.WriteLine("Estado actual de las torres:");
            Console.WriteLine($"Torre A: {ObtenerContenidoTorre(torreA)}");
            Console.WriteLine($"Torre B: {ObtenerContenidoTorre(torreB)}");
            Console.WriteLine($"Torre C: {ObtenerContenidoTorre(torreC)}");
        }
        
        private string ObtenerContenidoTorre(Stack<int> torre)
        {
            if (torre.Count == 0) return "Vacía";
            
            // Convertir la pila a array para mostrar en orden correcto
            int[] discos = torre.ToArray();
            Array.Reverse(discos); // Los discos se muestran de mayor a menor
            
            return string.Join(", ", discos);
        }
    }
}