using System;
using System.Data.SQLite;

namespace system_ogloszeniowy
{
    public class BazaDanych_ogloszenie
    {
        public static void CreateDatabaseAndTables()
        {
            string dbName = "ogloszenie_baza.db"; // Nazwa twojej bazy danych SQLite

            SQLiteConnection.CreateFile(dbName);

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                string createTablesQuery = @"
                    -- CREATE TABLE queries...
                    CREATE TABLE IF NOT EXISTS Aplikacja (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        id_ogloszenia INTEGER,
                        status TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Doswiadczenie (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        stanowisko TEXT,
                        nazwa_firmy TEXT,
                        lokalizacja TEXT,
                        okres_zatrudnienia_od DATE,
                        okres_zatrudnienia_do DATE,
                        obowiazki TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Firma (
                        id INTEGER PRIMARY KEY,
                        nazwa_firmy TEXT NOT NULL,
                        adres TEXT NOT NULL,
                        lokalizacja_geograficzna TEXT,
                        informacje TEXT,
                        logo_url TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Jezyk (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_jezyka TEXT,
                        poziom TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Kurs (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_szkolenia TEXT,
                        organizator TEXT,
                        data DATE
                    );

                    CREATE TABLE IF NOT EXISTS Link (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        adres_url TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Ogloszenie (
                        id INTEGER PRIMARY KEY,
                        id_firmy INTEGER,
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
                        informacje_o_firmie TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Umiejetnosc (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_umiejetnosci TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Uzytkownik (
                        id INTEGER PRIMARY KEY,
                        imie TEXT,
                        nazwisko TEXT,
                        data_urodzenia DATE,
                        adres_email TEXT,
                        numer_telefonu TEXT,
                        zdjecie_profilowe BLOB,
                        miejsce_zamieszkania TEXT
                    );

                    CREATE TABLE IF NOT EXISTS Wyksztalcenie (
                        id INTEGER PRIMARY KEY,
                        id_uzytkownika INTEGER,
                        nazwa_szkoly TEXT,
                        miejscowosc TEXT,
                        poziom_wyksztalcenia TEXT,
                        kierunek TEXT,
                        okres_od DATE,
                        okres_do DATE
                    );

                    -- ALTER TABLE queries...

                    ALTER TABLE Aplikacja
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id),
                        ADD FOREIGN KEY (id_ogloszenia) REFERENCES Ogloszenie(id);

                    ALTER TABLE Doswiadczenie
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);

                    ALTER TABLE Firma
                        ADD PRIMARY KEY (id);

                    ALTER TABLE Jezyk
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);

                    ALTER TABLE Kurs
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);

                    ALTER TABLE Link
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);

                    ALTER TABLE Ogloszenie
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_firmy) REFERENCES Firma(id);

                    ALTER TABLE Umiejetnosc
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);

                    ALTER TABLE Uzytkownik
                        ADD PRIMARY KEY (id),
                        ADD UNIQUE (adres_email);

                    ALTER TABLE Wyksztalcenie
                        ADD PRIMARY KEY (id),
                        ADD FOREIGN KEY (id_uzytkownika) REFERENCES Uzytkownik(id);
                ";

                cmd.CommandText = createTablesQuery;
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
