namespace Kurz_C_2_ukol_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Book> knihovna = new List<Book>();
            string vstup;
            Console.WriteLine("\nZadej příkaz (ADD, LIST, STATS, FIND, END) :");

            while ((vstup = Console.ReadLine()) != null)
            {
                var casti = vstup.Split(';');
                var prikaz = casti[0].ToUpper();

                switch (prikaz)
                {
                    case "ADD":
                        if (casti.Length != 5)
                        {
                            Console.WriteLine("Neplatný formát příkazu ADD. Použij: ADD;název;autor;datum;strany");
                            break;
                        }

                        string title = casti[1];
                        string author = casti[2];

                        if (!DateTime.TryParseExact(casti[3], "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime publishedDate))
                        {
                            Console.WriteLine("Neplatný formát datumu. Použij formát YYYY-MM-DD.");
                            break;
                        }

                        if (!int.TryParse(casti[4], out int pages))
                        {
                            Console.WriteLine("Počet stran musí být celé číslo.");
                            break;
                        }

                        bool existuje = knihovna.Any(k => k.Title.Equals(title, StringComparison.OrdinalIgnoreCase)
                        &&
                        k.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

                        if (existuje)
                        {
                            Console.WriteLine("Tato kniha již v knihovně existuje.");
                            break;
                        }

                        try
                        {
                            Book novaKniha = new Book(title, author, publishedDate, pages);
                            knihovna.Add(novaKniha);
                            Console.WriteLine("Kniha úspěšně přidána.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Chyba při vytváření knihy: {ex.Message}");
                        }

                        break;



                    case "LIST":
                        {
                            if (knihovna.Count == 0)
                            {
                                Console.WriteLine("Knihovna je prázdná.");
                                break;
                            }

                            var serazeneKnihy = knihovna.OrderBy(k => k.PublishedDate);

                            foreach (var kniha in serazeneKnihy)
                            {
                                Console.WriteLine(kniha);
                            }
                            break;
                        }
                    case "STATS":
                        {
                            if (knihovna.Count == 0)
                            {
                                Console.WriteLine("Statistiku nelze spočítat – knihovna je prázdná.");
                                break;
                            }

                            double prumer = knihovna.Select(k => k.Pages).Average();
                            Console.WriteLine($"Průměrný počet stran: {Math.Round(prumer)}");

                            var pocetKnihPodleAutora = knihovna.GroupBy(k => k.Author).Select(skupina => new { Autor = skupina.Key, Pocet = skupina.Count() });
                            
                            foreach (var zaznam in pocetKnihPodleAutora)
                            {
                                Console.WriteLine($"{zaznam.Autor}: {zaznam.Pocet}");
                            }

                            var unikatniSlova = knihovna.SelectMany(k => k.Title.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                            .Select(slovo => slovo.ToLower().Trim(',', '.', ':', ';', '-', '!'))
                            .Where(slovo => !string.IsNullOrWhiteSpace(slovo))
                            .Distinct();

                            Console.WriteLine($"Počet unikátních slov v názvech knih: {unikatniSlova.Count()}");
                            break;
                        }

                    case "FIND":
                        {
                            if (casti.Length != 2)
                            {
                                Console.WriteLine("Použij formát: FIND;klíčové_slovo");
                                break;
                            }

                            string hledaneSlovo = casti[1].ToLower();

                            var vysledky = knihovna
                                .Where(k => k.Title.ToLower().Contains(hledaneSlovo))
                                .ToList();

                            if (vysledky.Count == 0)
                            {
                                Console.WriteLine($"Žádné knihy neodpovídají hledání pro \"{hledaneSlovo}\".");
                                break;
                            }

                            Console.WriteLine($"Výsledky hledání pro \"{hledaneSlovo}\":");
                            foreach (var kniha in vysledky)
                            {
                                Console.WriteLine($" - {kniha.Title}");
                            }

                            break;
                        }

                    case "END":
                        return;

                    default:
                        Console.WriteLine("Neznámý příkaz.");
                        break;

                }
            }
            
        }
    }
}
