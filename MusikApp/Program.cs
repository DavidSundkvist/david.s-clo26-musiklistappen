using Microsoft.Extensions.Configuration;
using MusikApp;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");
Console.WriteLine($"Databassökväg: {Path.GetFullPath("musik.db")}");
// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");
DbHandler db = new DbHandler(connectionString);
// Tips på ordning (se UPPGIFT.md):
//   1. Skriv ut huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".
bool kör = true;
while (kör)
{
    Console.WriteLine("=== Min musikapp ===");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Lägg till låt");
    Console.WriteLine("3. Ta bort låt");
    Console.WriteLine("4. Avsluta");

    Console.Write("Välj: ");
    string? val = Console.ReadLine();

    switch (val)
    {

        case "1":
            List<Song> songs = db.GetAllSongs();

            foreach (Song song in songs)
            {
                Console.WriteLine($"{song.Id}. {song.Title} - {song.Duration} sekunder");
            }
            break;


        case "2":
            Console.Write("Ange låtens titel: ");
            string title = Console.ReadLine() ?? "";

            Console.Write("Ange låtens längd i sekunder: ");
            int duration = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Ange albumets ID: ");
            int albumId = int.Parse(Console.ReadLine() ?? "0");

            Song newSong = new Song
            {
                Title = title,
                Duration = duration,
                AlbumId = albumId
            };

            db.AddSong(newSong);

            Console.WriteLine("Låten har lagts till!");



            break;



        case "3":
            {
                Console.Write("Ange ID på låten du vill ta bort: ");

                if (!int.TryParse(Console.ReadLine(), out int songId))
                {
                    Console.WriteLine("Felaktigt ID!");
                    break;
                }

                Console.Write($"Är du säker på att du vill ta bort låt {songId}? (j/n): ");
                string answer = Console.ReadLine() ?? "";

                if (answer.ToLower() == "j")
                {
                    bool deleted = db.DeleteSong(songId);

                    if (deleted)
                    {
                        Console.WriteLine("Låten har tagits bort!");
                    }
                    else
                    {
                        Console.WriteLine("Ingen låt med det ID-numret hittades.");
                    }
                }
                else
                {
                    Console.WriteLine("Borttagningen avbröts.");
                }

                break;
            }

        case "4":
            Console.WriteLine("Hejdå!");
            kör = false;
            break;

        default:
            Console.WriteLine("Ogiltigt val. Försök igen.");
            break;
    }
}