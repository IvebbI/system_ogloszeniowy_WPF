using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using static system_ogloszeniowy.Baza_Logowanie;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy SzczegolyOferty.xaml
    /// </summary>
    public partial class SzczegolyOferty : Window
    {
        private Baza_Logowanie.UserData userData;
        private BazaDanych_ogloszenie.Ogloszenie ogloszenie;
        private BazaDanych_ogloszenie.Firma firma;

        public SzczegolyOferty(Baza_Logowanie.UserData userData, BazaDanych_ogloszenie.Ogloszenie ogloszenie, BazaDanych_ogloszenie.Firma firma)
        {
            InitializeComponent();
            this.userData = userData;
            this.ogloszenie = ogloszenie;
            this.firma = firma;

            WyswietlSzczegolyOferty();
        }

        private void WyswietlSzczegolyOferty()
        {
            Grid mainGrid = new Grid();
            MainStackPanel.Children.Add(mainGrid);

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            TextBlock nazwaTextBlock = new TextBlock()
            {
                Text = $"Nazwa Firmy która ogłosiła ofertę: {firma.nazwa_firmy}",
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 10, 0, 10),
            };
            TextBlock opisTextBlock = new TextBlock()
            {
                Text = $"Adres firmy: {firma.adres_firmy}",
                Margin = new Thickness(0, 0, 0, 10),
            };

            TextBlock lokalizacjageograficzna = new TextBlock()
            {
                Text = $"Lokalizacja Geograficzna: {firma.LokalizacjaGeograficzna}",
                Margin = new Thickness(0, 0, 0, 10),
            };

            TextBlock firmainformacje = new TextBlock()
            {
                Text = $"Informacje o firmie: {firma.Informacje}",
                Margin = new Thickness(0, 0, 0, 10),
            };

            Image image = null;
            if (!string.IsNullOrEmpty(firma.logo_url))
            {
                image = new Image()
                {
                    Height = 100,
                    Source = new BitmapImage(new Uri(firma.logo_url)),
                    Margin = new Thickness(0, 0, 0, 10),
                };
            }

            TextBlock ogloszenienazwa = new TextBlock()
            {
                Text = $"Nazwa: {ogloszenie.Nazwa}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock poziom_stanowiska = new TextBlock()
            {
                Text = $"Poziom Stanowiska: {ogloszenie.PoziomStanowiska}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock rodzajumowy = new TextBlock()
            {
                Text = $"Rodzaj umowy: {ogloszenie.RodzajUmowy}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock wymiaretatu = new TextBlock()
            {
                Text = $"Wymiar etatu: {ogloszenie.WymiarEtatu}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock rodzajpracy = new TextBlock()
            {
                Text = $"Rodzaj pracy: {ogloszenie.RodzajPracy}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock widelkiwynagrodzenia = new TextBlock()
            {
                Text = $"Wynagrodzenie: {ogloszenie.WidelkiWynagrodzenia}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock dnipracy = new TextBlock()
            {
                Text = $"Dni pracy: {ogloszenie.DniPracy}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock godzinypracy = new TextBlock()
            {
                Text = $"Godziny pracy: {ogloszenie.GodzinyPracy}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock datawaznosci = new TextBlock()
            {
                Text = $"Data waznosci: {ogloszenie.DataWaznosci}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock kategoria = new TextBlock()
            {
                Text = $"Kategoria: {ogloszenie.Kategoria}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock zakresobowiazkow = new TextBlock()
            {
                Text = $"Zakres obowiazkow: {ogloszenie.ZakresObowiazkow}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock wymaganiakandydata = new TextBlock()
            {
                Text = $"Wymagania kandydata: {ogloszenie.WymaganiaKandydata}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock oferowanebenefity = new TextBlock()
            {
                Text = $"Oferowane benefity: {ogloszenie.OferowaneBenefity}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            TextBlock informacjeofirmie = new TextBlock()
            {
                Text = $"Informacje o firmie: {firma.Informacje}",
                Margin = new Thickness(0, 0, 0, 10),
            };
            Button aplikujButton = new Button()
            {
                Content = "Aplikuj",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 10, 0, 0),
            };
            aplikujButton.Click += Aplikuj_Click;



            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(nazwaTextBlock);
            stackPanel.Children.Add(opisTextBlock);
            stackPanel.Children.Add(lokalizacjageograficzna);
            stackPanel.Children.Add(firmainformacje);
            stackPanel.Children.Add(ogloszenienazwa);
            stackPanel.Children.Add(poziom_stanowiska);
            stackPanel.Children.Add(rodzajumowy);
            stackPanel.Children.Add(wymiaretatu);
            stackPanel.Children.Add(rodzajpracy);
            stackPanel.Children.Add(widelkiwynagrodzenia);
            stackPanel.Children.Add(dnipracy);
            stackPanel.Children.Add(godzinypracy);
            stackPanel.Children.Add(datawaznosci);
            stackPanel.Children.Add(kategoria);
            stackPanel.Children.Add(zakresobowiazkow);
            stackPanel.Children.Add(wymaganiakandydata);
            stackPanel.Children.Add(oferowanebenefity);
            stackPanel.Children.Add(informacjeofirmie);
            stackPanel.Children.Add(aplikujButton);
            if (image != null)
            {
                stackPanel.Children.Add(image);
            }

            mainGrid.Children.Add(stackPanel);
        }
        private void Aplikuj_Click(object sender, RoutedEventArgs e)
        {

            BazaDanych_ogloszenie bazadanych = new BazaDanych_ogloszenie();
            int uzytkownikId = SessionManager.GetLoggedInUserId();
            int ogloszenieId = bazadanych.PobierzOstatnieIdOgloszenia();
            DodajAplikacje(uzytkownikId, ogloszenieId);
       

        }
        private static string DbName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");
        private void DodajAplikacje(int uzytkownikId, int ogloszenieId)
        {


            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();

                string insertQuery = "INSERT INTO OgloszeniaUdzial (id_ogloszenia, uzytkownik_id) VALUES (@idOgloszenia, @idUzytkownika)";
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@idOgloszenia", ogloszenieId);
                    cmd.Parameters.AddWithValue("@idUzytkownika", uzytkownikId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public SzczegolyOferty()
        {
            InitializeComponent();
        }
        private void StronaGlowna_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę główną
            Glowna glowna = new Glowna();
            glowna.Show();
            Close();
        }
        public void SetSession(string userEmail)
        {
            SessionManager.SetLoggedInUser(userEmail);
            Baza_Logowanie.CheckLoggedInUser();
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