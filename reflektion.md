# Reflektion — Musiklistappen

**Namn:** [David Sundkvist]
**Datum:** [2026-11-01]

---

## Vad var svårast?'

Det svåraste var att få C#-programmet att fungera tillsammans med min SQLite-databas. I början hade jag problem med att programmet inte hittade rätt databasfil. Jag hade bland annat råkat få fel filändelse och behövde kontrollera var musik.db låg.

Jag hade också problem när jag skulle ta bort låtar eftersom de var kopplade till andra tabeller genom foreign keys. Jag behövde ändra min kod så att kopplingarna i song_genres togs bort innan själva låten.

Det var även lite svårt att hålla reda på var olika metoder skulle ligga i DbHandler.cs. När jag gjorde min JOIN-funktion råkade jag lägga den på fel plats och fick två metoder med samma namn.


---

## Vad lärde du dig?

Jag har fått en bättre förståelse för hur C# och SQL kan användas tillsammans i ett program.

Jag lärde mig hur man hämtar information från en databas med SELECT, lägger till information med INSERT och tar bort information med DELETE.

Jag har också lärt mig mer om foreign keys och varför man inte alltid kan ta bort en rad direkt om den har kopplingar till andra tabeller.

En annan sak jag lärde mig var hur INNER JOIN fungerar och hur man kan hämta information från två tabeller samtidig

---

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Jag använde ADO.NET i min DbHandler.cs för att kommunicera med SQLite-databasen.

Jag började med att skapa en anslutning med SqliteConnection och öppna den med connection.Open(). Sedan skapade jag ett kommando med CreateCommand(), skrev min SQL-fråga och körde kommandot.

När jag skulle visa låtar använde jag ExecuteReader() och reader.Read() för att läsa resultatet. När jag lade till eller tog bort låtar använde jag ExecuteNonQuery().

Det som kändes mest naturligt var själva SQL-frågorna eftersom jag hade arbetat med SQL tidigare. Det som var mer förvirrande var att förstå skillnaden mellan ExecuteReader() och ExecuteNonQuery() och hur resultaten skulle omvandlas till Song-objekt.

Efter att ha testat funktionerna flera gånger började jag förstå hur stegen hänger ihop.

---

## Varför ska man använda parameteriserade queries?

Vad kan hända om man klistrar ihop SQL med strängar i stället? (Tänk på Bobby Tables.)

Man använder parameteriserade queries för att göra SQL-frågor säkrare och undvika SQL-injektion.

Om man bygger SQL genom att sätta ihop strängar kan någon skriva in SQL-kod istället för vanlig text. Det kan göra att databasen utför något annat än vad programmeraren tänkt sig, till exempel att data tas bort.

Med parametrar som @title och @id behandlas användarens inmatning som värden istället för som en del av SQL-kommandot.

Jag använde AddWithValue() för att skicka in värden på ett säkrare sätt.


---

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

Jag skulle vilja lägga till en sökfunktion så att man kan söka efter en låt genom att skriva en del av titeln.

Jag skulle också förbättra hur man lägger till nya låtar. Just nu måste användaren veta vilket album-ID som ska användas. Det hade varit enklare att visa en lista över album som användaren kan välja mellan.

En annan förbättring hade varit bättre felhantering, till exempel att använda TryParse() så att programmet inte kraschar om någon skriver bokstäver istället för siffror.

Jag skulle även planera kodens struktur bättre från början för att undvika att behöva flytta metoder och ändra saker i efterhand.

---

## (VG) Varför delade du upp koden som du gjorde?

Jag valde att dela upp koden i olika filer för att göra programmet mer organiserat och lättare att förstå.

I Program.cs har jag huvudmenyn och det som användaren kan göra. I DbHandler.cs har jag samlat all SQL-kod och metoderna som kommunicerar med databasen. Jag har även Song.cs som beskriver vilka egenskaper en låt har, till exempel titel, längd och ID.

Om jag hade lagt all kod i Program.cs hade filen blivit väldigt lång och svårare att läsa och felsöka. Nu vet jag var jag ska leta beroende på vad jag vill ändra.

Jag märkte själv hur viktigt det var att hålla ordning på metoderna när jag råkade lägga min JOIN-metod på fel plats och fick två metoder med samma namn.

Om jag skulle bygga ut programmet med fler funktioner blir det också enklare när databaskoden och menyn är separerade.
