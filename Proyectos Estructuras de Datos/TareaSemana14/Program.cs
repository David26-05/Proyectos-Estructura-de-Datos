// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;

namespace ArbolBinarioBusqueda
{
    // Clase Nodo que representa cada elemento del árbol
    public class Nodo
    {
        public int Valor { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    // Clase del Árbol Binario de Búsqueda
    public class ArbolBinarioBusqueda
    {
        private Nodo raiz;

        public ArbolBinarioBusqueda()
        {
            raiz = null;
        }

        // 1. Insertar valor
        public void Insertar(int valor)
        {
            raiz = InsertarRecursivo(raiz, valor);
            Console.WriteLine($"✅ Valor {valor} insertado correctamente.");
        }

        private Nodo InsertarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
            {
                return new Nodo(valor);
            }

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = InsertarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                Console.WriteLine($"⚠️ El valor {valor} ya existe en el árbol.");
            }

            return nodo;
        }

        // 2. Buscar valor
        public bool Buscar(int valor)
        {
            return BuscarRecursivo(raiz, valor);
        }

        private bool BuscarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return false;

            if (valor == nodo.Valor)
                return true;

            if (valor < nodo.Valor)
                return BuscarRecursivo(nodo.Izquierdo, valor);
            else
                return BuscarRecursivo(nodo.Derecho, valor);
        }

        // 3. Eliminar valor
        public void Eliminar(int valor)
        {
            if (!Buscar(valor))
            {
                Console.WriteLine($"❌ El valor {valor} no existe en el árbol.");
                return;
            }

            raiz = EliminarRecursivo(raiz, valor);
            Console.WriteLine($"🗑️ Valor {valor} eliminado correctamente.");
        }

        private Nodo EliminarRecursivo(Nodo nodo, int valor)
        {
            if (nodo == null)
                return null;

            if (valor < nodo.Valor)
            {
                nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, valor);
            }
            else if (valor > nodo.Valor)
            {
                nodo.Derecho = EliminarRecursivo(nodo.Derecho, valor);
            }
            else
            {
                // Caso 1: Nodo hoja (sin hijos)
                if (nodo.Izquierdo == null && nodo.Derecho == null)
                {
                    return null;
                }
                // Caso 2: Un solo hijo
                else if (nodo.Izquierdo == null)
                {
                    return nodo.Derecho;
                }
                else if (nodo.Derecho == null)
                {
                    return nodo.Izquierdo;
                }
                // Caso 3: Dos hijos
                else
                {
                    // Encontrar el sucesor inorden (mínimo del subárbol derecho)
                    Nodo sucesor = ObtenerMinimoNodo(nodo.Derecho);
                    nodo.Valor = sucesor.Valor;
                    nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Valor);
                }
            }
            return nodo;
        }

        private Nodo ObtenerMinimoNodo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual;
        }

        // 4. Recorridos
        public void RecorridoPreorden()
        {
            Console.Write("Recorrido Preorden: ");
            PreordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PreordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.Valor + " ");
                PreordenRecursivo(nodo.Izquierdo);
                PreordenRecursivo(nodo.Derecho);
            }
        }

        public void RecorridoInorden()
        {
            Console.Write("Recorrido Inorden: ");
            InordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                InordenRecursivo(nodo.Izquierdo);
                Console.Write(nodo.Valor + " ");
                InordenRecursivo(nodo.Derecho);
            }
        }

        public void RecorridoPostorden()
        {
            Console.Write("Recorrido Postorden: ");
            PostordenRecursivo(raiz);
            Console.WriteLine();
        }

        private void PostordenRecursivo(Nodo nodo)
        {
            if (nodo != null)
            {
                PostordenRecursivo(nodo.Izquierdo);
                PostordenRecursivo(nodo.Derecho);
                Console.Write(nodo.Valor + " ");
            }
        }

        // 5. Valor mínimo
        public int? ObtenerMinimo()
        {
            if (raiz == null)
            {
                Console.WriteLine("❌ El árbol está vacío.");
                return null;
            }

            Nodo actual = raiz;
            while (actual.Izquierdo != null)
            {
                actual = actual.Izquierdo;
            }
            return actual.Valor;
        }

        // 6. Valor máximo
        public int? ObtenerMaximo()
        {
            if (raiz == null)
            {
                Console.WriteLine("❌ El árbol está vacío.");
                return null;
            }

            Nodo actual = raiz;
            while (actual.Derecho != null)
            {
                actual = actual.Derecho;
            }
            return actual.Valor;
        }

        // 7. Altura del árbol
        public int ObtenerAltura()
        {
            return CalcularAltura(raiz);
        }

        private int CalcularAltura(Nodo nodo)
        {
            if (nodo == null)
                return 0;

            int alturaIzquierda = CalcularAltura(nodo.Izquierdo);
            int alturaDerecha = CalcularAltura(nodo.Derecho);

            return Math.Max(alturaIzquierda, alturaDerecha) + 1;
        }

        // 8. Limpiar árbol
        public void Limpiar()
        {
            raiz = null;
            Console.WriteLine("🧹 Árbol limpiado completamente.");
        }

        // Verificar si el árbol está vacío
        public bool EstaVacio()
        {
            return raiz == null;
        }
    }

    // Clase principal con el menú interactivo
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioBusqueda arbol = new ArbolBinarioBusqueda();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ÁRBOL BINARIO DE BÚSQUEDA (BST) ===\n");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Mostrar recorridos");
                Console.WriteLine("5. Mostrar mínimo, máximo y altura");
                Console.WriteLine("6. Limpiar árbol");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("❌ Opción inválida. Presione Enter para continuar...");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine();

                switch (opcion)
                {
                    case 1:
                        InsertarValor(arbol);
                        break;

                    case 2:
                        BuscarValor(arbol);
                        break;

                    case 3:
                        EliminarValor(arbol);
                        break;

                    case 4:
                        MostrarRecorridos(arbol);
                        break;

                    case 5:
                        MostrarInfoArbol(arbol);
                        break;

                    case 6:
                        arbol.Limpiar();
                        break;

                    case 0:
                        Console.WriteLine("👋 ¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("❌ Opción no válida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione Enter para continuar...");
                    Console.ReadLine();
                }

            } while (opcion != 0);
        }

        static void InsertarValor(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a insertar: ");
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                arbol.Insertar(valor);
            }
            else
            {
                Console.WriteLine("❌ Valor inválido.");
            }
        }

        static void BuscarValor(ArbolBinarioBusqueda arbol)
        {
            Console.Write("Ingrese el valor a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                bool encontrado = arbol.Buscar(valor);
                if (encontrado)
                    Console.WriteLine($"✅ El valor {valor} SÍ existe en el árbol.");
                else
                    Console.WriteLine($"❌ El valor {valor} NO existe en el árbol.");
            }
            else
            {
                Console.WriteLine("❌ Valor inválido.");
            }
        }

        static void EliminarValor(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("❌ El árbol está vacío. No se puede eliminar.");
                return;
            }

            Console.Write("Ingrese el valor a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int valor))
            {
                arbol.Eliminar(valor);
            }
            else
            {
                Console.WriteLine("❌ Valor inválido.");
            }
        }

        static void MostrarRecorridos(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("❌ El árbol está vacío.");
                return;
            }

            Console.WriteLine("=== RECORRIDOS DEL ÁRBOL ===\n");
            arbol.RecorridoPreorden();
            arbol.RecorridoInorden();
            arbol.RecorridoPostorden();
        }

        static void MostrarInfoArbol(ArbolBinarioBusqueda arbol)
        {
            if (arbol.EstaVacio())
            {
                Console.WriteLine("❌ El árbol está vacío.");
                return;
            }

            Console.WriteLine("=== INFORMACIÓN DEL ÁRBOL ===\n");
            
            int? minimo = arbol.ObtenerMinimo();
            if (minimo.HasValue)
                Console.WriteLine($"📉 Valor mínimo: {minimo.Value}");

            int? maximo = arbol.ObtenerMaximo();
            if (maximo.HasValue)
                Console.WriteLine($"📈 Valor máximo: {maximo.Value}");

            int altura = arbol.ObtenerAltura();
            Console.WriteLine($"📏 Altura del árbol: {altura}");
        }
    }
}
