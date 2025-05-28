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

                    case "END":
                        return;

                    default:
                        Console.WriteLine("Neznámý příkaz.");
                        break;

                        //}
                        //if (vstup.ToUpper() == "END")
                        //    break;

                        //if (vstup.ToUpper() == "END")
                }
            }
            
        }
    }
}
