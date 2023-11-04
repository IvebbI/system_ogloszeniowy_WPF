using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using static system_ogloszeniowy.BazaDanych_ogloszenie;
using System.IO;
using System.Data.SQLite;
using System.Xml.Linq;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy ogloszenie_dodaj.xaml
    /// </summary>
    public partial class ogloszenie_dodaj : Window
    {
        private static string dbName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");
        public ogloszenie_dodaj()
        {
            InitializeComponent();
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);
            }


        }


        public void DodajKilkaFirmIOgloszen(object sender,RoutedEventArgs e)
        {
            
                Firma firma1 = new Firma
            {
                nazwa_firmy = "Nazwa Firmy 1",
                adres_firmy = "Adres Firmy 1",
                LokalizacjaGeograficzna = "Lokalizacja Firmy 1",
                Informacje = "Informacje o Firmie 1",
                logo_url = "https://example.com/logo1.png"
            };

            Firma firma2 = new Firma
            {
                nazwa_firmy = "Nazwa Firmy 2",
                adres_firmy = "Adres Firmy 2",
                LokalizacjaGeograficzna = "Lokalizacja Firmy 2",
                Informacje = "Informacje o Firmie 2",
                logo_url = "https://example.com/logo2.png"
            };

            DodajFirmeDoBazy(firma1);
            DodajFirmeDoBazy(firma2);

            Ogloszenie ogloszenie1 = new Ogloszenie
            {
                id_firmy = 1,
                Nazwa = "Inżynier systemów",
                PoziomStanowiska = "Specjalista",
                RodzajUmowy = "Umowa o pracę",
                WymiarEtatu = "Pełny etat",
                RodzajPracy = "Zdalna",
                WidelkiWynagrodzenia = "8000-10000 PLN",
                DniPracy = "Poniedziałek - Piątek",
                GodzinyPracy = "8:00 - 16:00",
                DataWaznosci = DateTime.Now.AddDays(30), 
                Kategoria = "Informatyka",
                ZakresObowiazkow = "Zarządzanie systemami informatycznymi",
                WymaganiaKandydata = "Doświadczenie w zarządzaniu sieciami",
                OferowaneBenefity = "Opieka medyczna, karta multisport",
                InformacjeOFirmie = "Firma specjalizująca się w rozwiązaniach IT",
            };

            Ogloszenie ogloszenie2 = new Ogloszenie
            {
                id_firmy = 2,
                Nazwa = "Specjalista ds. marketingu",
                PoziomStanowiska = "Specjalista",
                RodzajUmowy = "Umowa o pracę",
                WymiarEtatu = "Pełny etat",
                RodzajPracy = "Stacjonarna",
                WidelkiWynagrodzenia = "7000-9000 PLN",
                DniPracy = "Poniedziałek - Piątek",
                GodzinyPracy = "9:00 - 17:00",
                DataWaznosci = DateTime.Now.AddDays(45), 
                Kategoria = "Marketing",
                ZakresObowiazkow = "Tworzenie strategii marketingowych",
                WymaganiaKandydata = "Znajomość narzędzi analitycznych",
                OferowaneBenefity = "Pakiet benefitów pozapłacowych",
                InformacjeOFirmie = "Agencja marketingowa z wieloletnim doświadczeniem",
            };

            DodajOgloszenieDoBazy(ogloszenie1);
            DodajOgloszenieDoBazy(ogloszenie2);
        }

        private void DodajFirmeDoBazy(Firma firma)
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd dodawania firmy: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void DodajOgloszenieDoBazy(Ogloszenie ogloszenie)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbName};Version=3;"))
            {
                connection.Open();
                SQLiteCommand cmd = new SQLiteCommand(connection);

                try
                {
                    string insertOgloszenieQuery = @"
                INSERT INTO Ogloszenie(id_firmy, nazwa, poziom_stanowiska, rodzaj_umowy, wymiar_etatu,
                rodzaj_pracy, widełki_wynagrodzenia, dni_pracy, godziny_pracy, data_waznosci,
                kategoria, zakres_obowiazkow, wymagania_kandydata, oferowane_benefity, informacje_o_firmie)
                VALUES (@id_firmy, @nazwa, @poziom_stanowiska, @rodzaj_umowy, @wymiar_etatu,
                @rodzaj_pracy, @widełki_wynagrodzenia, @dni_pracy, @godziny_pracy, @data_waznosci,
                @kategoria, @zakres_obowiazkow, @wymagania_kandydata, @oferowane_benefity, @informacje_o_firmie);";

                    cmd.CommandText = insertOgloszenieQuery;
                    cmd.Parameters.AddWithValue("@id_firmy", ogloszenie.id_firmy);
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
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd dodawania ogłoszenia: " + ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę główną
            Glowna glowna = new Glowna();
            glowna.Show();
            Close();
        }
        private void OfertyPracy_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę oferty_pracy
            oferty_pracy oferty = new oferty_pracy();
            oferty.Show();
            Close();
        }
        private void Mojprofil_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę mojprofil
            mojprofil mojprofil = new mojprofil();
            mojprofil.Show();
            Close();

        }
        private void MojeKonto_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę mojekonto
            mojekonto mk = new mojekonto();
            mk.Show();
            Close();
        }
        private void DodajOgloszenie_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę DodajKonto
            ogloszenie_dodaj ogdodaj = new ogloszenie_dodaj();
            ogdodaj.Show();
            Close();
        }
    }
}
