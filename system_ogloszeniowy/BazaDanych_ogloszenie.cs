using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Security.Policy;
using static system_ogloszeniowy.Baza_Logowanie;

namespace system_ogloszeniowy
{
    public class BazaDanych_ogloszenie
    {

        private static string dbName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");


        public static void CreateDatabaseAndTables()
        {
            SQLiteConnection.CreateFile(dbName);

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                string createTablesQuery = @"
                    CREATE TABLE IF NOT EXISTS Firma (
                        id INTEGER PRIMARY KEY,
                        nazwa_firmy TEXT NOT NULL,
                        adres TEXT NOT NULL,
                        lokalizacja_geograficzna TEXT,
                        informacje TEXT,
                        logo_url TEXT
                    );

                   CREATE TABLE IF NOT EXISTS Ogloszenie (
                    id INTEGER PRIMARY KEY,
                    id_firmy INTEGER,
                    id_uzytkownika INTEGER,
                    nazwa TEXT NOT NULL,
                    poziom_stanowiska TEXT,
                    rodzaj_umowy TEXT,
                    wymiar_etatu TEXT,
                    rodzaj_pracy TEXT,
                    widełki_wynagrodzenia TEXT,
                    dni_pracy TEXT,
                    godziny_pracy TEXT,
                    data_waznosci DATE,
                    kategoria TEXT,
                    zakres_obowiazkow TEXT,
                    wymagania_kandydata TEXT,
                    oferowane_benefity TEXT,
                    informacje_o_firmie TEXT,
                    FOREIGN KEY (id_firmy) REFERENCES Firma(id),
                    FOREIGN KEY (id_uzytkownika) REFERENCES OgloszenieUzytkownika(id_uzytkownika)
                );

                    CREATE TABLE IF NOT EXISTS Aplikacja (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        id_ogloszenia INTEGER,
                        status TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id),
                        FOREIGN KEY (id_ogloszenia) REFERENCES Ogloszenie(id)
                    );

                    CREATE TABLE IF NOT EXISTS Doswiadczenie (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        stanowisko TEXT,
                        nazwa_firmy TEXT,
                        lokalizacja TEXT,
                        okres_zatrudnienia_od DATE,
                        okres_zatrudnienia_do DATE,
                        obowiazki TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS Jezyk (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_jezyka TEXT,
                        poziom TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS Kurs (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_szkolenia TEXT,
                        organizator TEXT,
                        data DATE,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS Link (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        adres_url TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS Umiejetnosc (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_umiejetnosci TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS Wyksztalcenie (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_szkoly TEXT,
                        miejscowosc TEXT,
                        poziom_wyksztalcenia TEXT,
                        kierunek TEXT,
                        okres_od DATE,
                        okres_do DATE,
                        FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id)
                    );

                    CREATE TABLE IF NOT EXISTS OgloszenieUzytkownika (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        id_ogloszenia INTEGER,
                        status TEXT,
                        FOREIGN KEY (id_uzytkownika) REFERENCES user(id),
                        FOREIGN KEY (id_ogloszenia) REFERENCES Ogloszenie(id)
                    );
                ";

                try
                {
                    cmd.CommandText = createTablesQuery;
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Poprawnie stworzono tabele");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd z tworzeniem tabel: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DodajOgloszenie(Ogloszenie ogloszenie, Firma firma, int idUzytkownika)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                try
                {

                    string insertFirmaQuery = @"
            INSERT INTO Firma(nazwa_firmy, adres, lokalizacja_geograficzna, informacje, logo_url)
            VALUES (@nazwa_firmy, @adres, @lokalizacja_geograficzna, @informacje, @logo_url);";
                    cmd.CommandText = insertFirmaQuery;
                    cmd.Parameters.AddWithValue("@nazwa_firmy", firma.nazwa_firmy);
                    cmd.Parameters.AddWithValue("@adres", firma.adres_firmy);
                    cmd.Parameters.AddWithValue("@lokalizacja_geograficzna", firma.LokalizacjaGeograficzna);
                    cmd.Parameters.AddWithValue("@informacje", firma.Informacje);
                    cmd.Parameters.AddWithValue("@logo_url", firma.logo_url);
                    cmd.ExecuteNonQuery();


                    string selectLastInsertIdQuery = "SELECT last_insert_rowid();";
                    cmd.CommandText = selectLastInsertIdQuery;
                    int idFirmy = Convert.ToInt32(cmd.ExecuteScalar());
               
                
                    string insertOgloszenieQuery = @"
            INSERT INTO Ogloszenie(id_firmy,id_uzytkownika, nazwa, poziom_stanowiska, rodzaj_umowy, wymiar_etatu,
            rodzaj_pracy, widełki_wynagrodzenia, dni_pracy, godziny_pracy, data_waznosci,
            kategoria, zakres_obowiazkow, wymagania_kandydata, oferowane_benefity, informacje_o_firmie)
            VALUES (@id_firmy,@id_uzytkownika, @nazwa, @poziom_stanowiska, @rodzaj_umowy, @wymiar_etatu,
            @rodzaj_pracy, @widełki_wynagrodzenia, @dni_pracy, @godziny_pracy, @data_waznosci,
            @kategoria, @zakres_obowiazkow, @wymagania_kandydata, @oferowane_benefity, @informacje_o_firmie);";

                    cmd.CommandText = insertOgloszenieQuery;
                    cmd.Parameters.Clear();  // Wyczyszczenie parametrów

                    cmd.Parameters.AddWithValue("@id_firmy", idFirmy);
                    cmd.Parameters.AddWithValue("@id_uzytkownika", idUzytkownika);
                    cmd.Parameters.AddWithValue("@nazwa", ogloszenie.Nazwa);
                    cmd.Parameters.AddWithValue("@poziom_stanowiska", ogloszenie.PoziomStanowiska);
                    cmd.Parameters.AddWithValue("@rodzaj_umowy", ogloszenie.RodzajUmowy);
                    cmd.Parameters.AddWithValue("@wymiar_etatu", ogloszenie.WymiarEtatu);
                    cmd.Parameters.AddWithValue("@rodzaj_pracy", ogloszenie.RodzajPracy);
                    cmd.Parameters.AddWithValue("@widełki_wynagrodzenia", ogloszenie.WidelkiWynagrodzenia);
                    cmd.Parameters.AddWithValue("@dni_pracy", ogloszenie.DniPracy);
                    cmd.Parameters.AddWithValue("@godziny_pracy", ogloszenie.GodzinyPracy);
                    cmd.Parameters.AddWithValue("@data_waznosci", ogloszenie.DataWaznosci);
                    cmd.Parameters.AddWithValue("@kategoria", ogloszenie.Kategoria);
                    cmd.Parameters.AddWithValue("@zakres_obowiazkow", ogloszenie.ZakresObowiazkow);
                    cmd.Parameters.AddWithValue("@wymagania_kandydata", ogloszenie.WymaganiaKandydata);
                    cmd.Parameters.AddWithValue("@oferowane_benefity", ogloszenie.OferowaneBenefity);
                    cmd.Parameters.AddWithValue("@informacje_o_firmie", ogloszenie.InformacjeOFirmie);

                    cmd.ExecuteNonQuery();

                    // Pobierz ID dodanego ogłoszenia
                    string selectLastInsertOgloszenieIdQuery = "SELECT last_insert_rowid();";
                    cmd.CommandText = selectLastInsertOgloszenieIdQuery;
                    int idOgloszenia = Convert.ToInt32(cmd.ExecuteScalar());

                    MessageBox.Show("Pomyślnie zostało dodane ogłoszenie do bazy danych", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd dodawania ogłoszenia: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public void DodajOgloszenieUzytkownika(int idUzytkownika, int idOgloszenia, string status)
        {
            // Pobierz id_uzytkownika z sesji
           

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                try
                {
                    string insertOgloszenieUzytkownikaQuery = @"
                INSERT INTO OgloszenieUzytkownika(id_uzytkownika, id_ogloszenia, status)
                VALUES (@id_uzytkownika, @id_ogloszenia, @status);
            ";

                    cmd.CommandText = insertOgloszenieUzytkownikaQuery;
                    cmd.Parameters.AddWithValue("@id_uzytkownika", idUzytkownika);
                    cmd.Parameters.AddWithValue("@id_ogloszenia", idOgloszenia);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pomyślnie przypisano ogłoszenie do użytkownika", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd dodawania ogłoszenia do użytkownika: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        public int PobierzOstatnieIdOgloszenia()
        {
            int ostatnieId = -1;

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                try
                {
                    string selectLastInsertIdQuery = "SELECT last_insert_rowid();";
                    cmd.CommandText = selectLastInsertIdQuery;
                    ostatnieId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Błąd pobierania ostatniego ID ogłoszenia: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return ostatnieId;
        }





        private static OgloszenieUzytkownika GetOgloszenieUzytkownikaFromReader(SQLiteDataReader reader)
        {
            return new OgloszenieUzytkownika
            {
                Id = GetInt32(reader, "id"),
                IdUzytkownika = GetInt32(reader, "id_uzytkownika"),
                IdOgloszenia = GetInt32(reader, "id_ogloszenia"),
                Status = GetString(reader, "status")
            };
        }


        public class OgloszenieUzytkownika
    {
        public int Id { get; set; }
        public int IdUzytkownika { get; set; }
        public int IdOgloszenia { get; set; }
        public string Status { get; set; }
    } 


    public static List<(Ogloszenie, Firma)> PobierzOgloszeniaIFirmy()
        {
            List<(Ogloszenie, Firma)> ogloszeniaFirmy = new List<(Ogloszenie, Firma)>();

            string selectQuery = "SELECT * FROM Ogloszenie INNER JOIN Firma ON Ogloszenie.id_firmy = Firma.id";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ogloszenie ogloszenie = GetOgloszenieFromReader(reader);
                        Firma firma = GetFirmaFromReader(reader);

                        ogloszeniaFirmy.Add((ogloszenie, firma));
                    }
                }
            }

            return ogloszeniaFirmy;
        }

        private static Ogloszenie GetOgloszenieFromReader(SQLiteDataReader reader)
        {
            return new Ogloszenie
            {
                id_firmy = GetInt32(reader, "id_firmy"),
                Nazwa = GetString(reader, "nazwa"),
                PoziomStanowiska = GetString(reader, "poziom_stanowiska"),
                RodzajUmowy = GetString(reader, "rodzaj_umowy"),
       
            };
        }

        private static Firma GetFirmaFromReader(SQLiteDataReader reader)
        {
            return new Firma
            {
                nazwa_firmy = GetString(reader, "nazwa_firmy"),
                adres_firmy = GetString(reader, "adres"),
                LokalizacjaGeograficzna = GetString(reader, "lokalizacja_geograficzna"),
         
            };
        }

        private static string GetString(SQLiteDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetString(reader.GetOrdinal(columnName)) : string.Empty;
        }

        private static int GetInt32(SQLiteDataReader reader, string columnName)
        {
            return !reader.IsDBNull(reader.GetOrdinal(columnName)) ? reader.GetInt32(reader.GetOrdinal(columnName)) : 0;
        }




        public static List<OgloszenieUzytkownika> PobierzOgloszeniaiIFirmy()
        {
            List<OgloszenieUzytkownika> ogloszeniaFirmy = new List<OgloszenieUzytkownika>();

            string selectQuery = "SELECT * FROM OgloszenieUzytkownika " +
                                 "INNER JOIN Ogloszenie ON OgloszenieUzytkownika.id_ogloszenia = Ogloszenie.id " +
                                 "INNER JOIN Firma ON Ogloszenie.id_firmy = Firma.id";

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OgloszenieUzytkownika ogloszenieUzytkownika = GetOgloszenieUzytkownikaFromReader(reader);
                        ogloszeniaFirmy.Add(ogloszenieUzytkownika);
                    }
                }
            }

            return ogloszeniaFirmy;
        }








        public class Ogloszenie
        {
            public int Id { get; set; } 
            public int id_firmy { get; set; }
          
            public string Nazwa { get; set; }
            public string PoziomStanowiska { get; set; }
            public string RodzajUmowy { get; set; }
            public string WymiarEtatu { get; set; }
            public string RodzajPracy { get; set; }
            public string WidelkiWynagrodzenia { get; set; }
            public string DniPracy { get; set; }
            public string GodzinyPracy { get; set; }
            public DateTime DataWaznosci { get; set; }
            public string Kategoria { get; set; }
            public string ZakresObowiazkow { get; set; }
            public string WymaganiaKandydata { get; set; }
            public string OferowaneBenefity { get; set; }
            public string InformacjeOFirmie { get; set; }
        }
        public class Firma
        {
            public string nazwa_firmy { get; set; }
            public string adres_firmy { get; set; }
            public string LokalizacjaGeograficzna { get; set; }
            public string Informacje { get; set; }
            public string logo_url { get; set; }
        }

    }
}
