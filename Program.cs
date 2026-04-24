using System;

class GeneratorLehmera
{
    static void Main()
    {
        //parametry
        const long m = 2147483647; //modulo: 2^31 - 1 - liczba pierwsza Mersenne'a
        const long a = 16807;      //mnoznik: MINSTD- Minimum Standard, generator Parka-Millera
        
        //pobranie danych
        Console.Write("Ile liczb pseudolosowych wygenerowac? ");
        if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
        {
            Console.WriteLine("Prosze podac poprawna liczbe dodatnia.");
            return;
        }

        //ustawiamy ziarno

        //ta opcja jest zeby kazde uruchomienie bylo unikalne, bierze ilosc tickow systemu
        //long x = DateTime.Now.Ticks % m;
        //if (x == 0) x = 1;

        //powtarzalna wersja
        long x = 3;

        Console.WriteLine($"\nGenerowanie {count} liczb (m={m}, a={a}, ziarno={x}):\n");

        //generowanie liczb
        for (int i = 0; i < count; i++)
        {
            //wzor X1= (a* X0) mod m
            x = (a * x) % m;

            //wynik
            Console.WriteLine($"{i + 1}: {x}");
        }
    }
}