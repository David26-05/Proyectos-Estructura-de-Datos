// See https://aka.ms/new-console-template for more information
using System;

public class Nodo
{
    public int Dato { get; set; }
    public Nodo Siguiente { get; set; }
    
    public Nodo(int dato)
    {
        Dato = dato;
        Siguiente = null;
    }
}

public class Lista
{
    private Nodo cabeza;
    
    public Lista()
    {
        cabeza = null;
    }
    
    // Método para agregar eledotmentos a la lista (para poder probar)
    public void Agregar(int dato)
    {
        Nodo nuevoNodo = new Nodo(dato);
        
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }
    
    // Método de búsqueda que cuenta las ocurrencias
    public int Buscar(int datoBuscado)
    {
        if (cabeza == null)
        {
            Console.WriteLine($"La lista está vacía. El dato {datoBuscado} no fue encontrado.");
            return 0;
        }
        
        int contador = 0;
        Nodo actual = cabeza;
        
        // Recorrer la lista y contar las ocurrencias
        while (actual != null)
        {
            if (actual.Dato == datoBuscado)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }
        
        // Mostrar el resultado
        if (contador > 0)
        {
            Console.WriteLine($"El dato {datoBuscado} se encontró {contador} vez/veces.");
        }
        else
        {
            Console.WriteLine($"El dato {datoBuscado} no fue encontrado.");
        }
        
        return contador;
    }
    
    // Versión alternativa del método sin mensajes (solo retorna el conteo)
    public int ContarOcurrencias(int datoBuscado)
    {
        if (cabeza == null) return 0;
        
        int contador = 0;
        Nodo actual = cabeza;
        
        while (actual != null)
        {
            if (actual.Dato == datoBuscado)
            {
                contador++;
            }
            actual = actual.Siguiente;
        }
        
        return contador;
    }
    
    // Método para mostrar la lista completa
    public void Mostrar()
    {
        if (cabeza == null)
        {
            Console.WriteLine("Lista vacía");
            return;
        }
        
        Nodo actual = cabeza;
        Console.Write("Lista: ");
        while (actual != null)
        {
            Console.Write(actual.Dato + " ");
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }
}

// Programa de prueba
class Program
{
    static void Main(string[] args)
    {
        Lista miLista = new Lista();
        
        // Agregar algunos datos de prueba
        miLista.Agregar(5);
        miLista.Agregar(3);
        miLista.Agregar(5);
        miLista.Agregar(7);
        miLista.Agregar(5);
        miLista.Agregar(9);
        miLista.Agregar(3);
        
        // Mostrar la lista
        miLista.Mostrar();
        Console.WriteLine();
        
        // Buscar elementos
        Console.WriteLine("Búsquedas:");
        Console.WriteLine("----------");
        
        // Caso 1: Dato que existe varias veces
        int resultado1 = miLista.Buscar(5);
        Console.WriteLine($"Resultado retornado: {resultado1}");
        Console.WriteLine();
        
        // Caso 2: Dato que existe una vez
        int resultado2 = miLista.Buscar(7);
        Console.WriteLine($"Resultado retornado: {resultado2}");
        Console.WriteLine();
        
        // Caso 3: Dato que no existe
        int resultado3 = miLista.Buscar(10);
        Console.WriteLine($"Resultado retornado: {resultado3}");
        Console.WriteLine();
        
        // Caso 4: Dato que existe dos veces
        int resultado4 = miLista.Buscar(3);
        Console.WriteLine($"Resultado retornado: {resultado4}");
        Console.WriteLine();
        
        // Usando la versión alternativa sin mensajes
        Console.WriteLine("Usando método alternativo (sin mensajes):");
        Console.WriteLine($"Ocurrencias de 5: {miLista.ContarOcurrencias(5)}");
        Console.WriteLine($"Ocurrencias de 10: {miLista.ContarOcurrencias(10)}");
    }
}
