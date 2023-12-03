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
using System.Globalization;
using static system_ogloszeniowy.Baza_Logowanie;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy ogloszenie_dodaj.xaml
    /// </summary>
    public partial class ogloszenie_dodaj : Window
    {

        private int idZalogowanegoUzytkownika;
        public ogloszenie_dodaj()
        {
            InitializeComponent();
        }



        private void DodajOgloszeniee_Click(object sender, RoutedEventArgs e)
        {
            // Tutaj dodaj kod do pobierania danych z formularza

            Ogloszenie noweOgloszenie = new Ogloszenie
            {
                Nazwa = NazwaTextBox.Text,
                PoziomStanowiska = PoziomStanowiskaTextBox.Text,
                RodzajUmowy = RodzajUmowyTextBox.Text,
                WymiarEtatu = WymiarEtatuTextBox.Text,
                RodzajPracy = RodzajPracyTextBox.Text,
                WidelkiWynagrodzenia = WidelkiWynagrodzeniaTextBox.Text,
                DniPracy = DniPracyTextBox.Text,
                GodzinyPracy = GodzinyPracyTextBox.Text,
                DataWaznosci = DateTime.ParseExact(DataWaznosciTextBox.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Kategoria = KategoriaTextBox.Text,
                ZakresObowiazkow = ZakresObowiazkowTextBox.Text,
                WymaganiaKandydata = WymaganiaKandydataTextBox.Text,
                OferowaneBenefity = OferowaneBenefityTextBox.Text,
                InformacjeOFirmie = InformacjeOFirmieTextBox.Text
            };

            Firma nowaFirma = new Firma
            {
                nazwa_firmy = NazwaTextBox.Text,
                adres_firmy = AdresFirmyTextBox.Text,
                LokalizacjaGeograficzna = LokalizacjaGeograficznaTextBox.Text,
                Informacje = InformacjeTextBox.Text,
                logo_url = LogoUrlTextBox.Text
            };
            int idUzytkownika = SessionManager.GetLoggedInUserId();
            BazaDanych_ogloszenie bazadanych = new BazaDanych_ogloszenie();
            bazadanych.DodajOgloszenie(noweOgloszenie, nowaFirma, idUzytkownika);

            int idOgloszenia = bazadanych.PobierzOstatnieIdOgloszenia();

      
            bazadanych.DodajOgloszenieUzytkownika(idUzytkownika, idOgloszenia, "Aktywne");
            MessageBox.Show("Pomyślnie dodano ogłoszenie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
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
