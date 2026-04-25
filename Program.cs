
class GeneratorLehmera
{
    static void Main()
    {
        //FAKT z notatek: m = 2^L, a % 8 == 5
        const long m = 2147483648; // 2^31
        const long a = 16389;      // 16389 % 8 = 5 (warunek spełniony)
        
        Console.Write("Ile liczb pseudolosowych wygenerowac (zalecane n >= 100)? ");
        if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
        {
            Console.WriteLine("Prosze podac poprawna liczbe dodatnia.");
            return;
        }

        //Ziarno musi byc nieparzyste 
        long x = 3; 

        //Tablica na znormalizowane wyniki
        double[] r = new double[count];

        Console.WriteLine($"\nGenerowanie {count} liczb...");

        //Generowanie i normalizacja
        for (int i = 0; i < count; i++)
        {
            x = (a * x) % m;
            r[i] = (double)x / m;
        }

        double suma = 0;
        foreach (double val in r) suma += val;
        double srednia = suma / count;

        double sumaKwadratowRoznic = 0;
        foreach (double val in r)
        {
            sumaKwadratowRoznic += Math.Pow(val - srednia, 2);
        }
        double wariancja = sumaKwadratowRoznic / (count - 1);

        Console.WriteLine("\n--- WYNIKI TESTOW STATYSTYCZNYCH ---");
        Console.WriteLine($"Srednia:      {srednia:F6}");
        Console.WriteLine($"Wariancja: {wariancja:F6}");
        Console.WriteLine($"Blad sredniej:           {Math.Abs(0.5 - srednia):F6}");

        try
        {
            using (StreamWriter file = new StreamWriter("test_niezaleznosci.csv"))
            {
                file.WriteLine("X;Y");
                for (int i = 0; i < count - 1; i += 2)
                {
                    file.WriteLine($"{r[i]:F6};{r[i+1]:F6}");
                }
            }
            Console.WriteLine("\n--- TEST NIEZALEZNOSCI ---");
            Console.WriteLine("Dane do wykresu 2D zapisano w pliku: test_niezaleznosci.csv");
            Console.WriteLine("Otworz plik w Excelu i stworz wykres punktowy (Scatter Plot).");
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nBlad zapisu do pliku: " + ex.Message);
        }

        Console.WriteLine("\nNacisnij dowolny klawisz, aby zakonczyc...");
        Console.Read();
    }
}