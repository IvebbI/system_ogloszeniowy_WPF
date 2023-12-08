using System;
using System.Collections.Generic;
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
using static system_ogloszeniowy.Baza_Logowanie;
using static system_ogloszeniowy.BazaDanych_ogloszenie;

namespace system_ogloszeniowy
{

    public partial class oferty_pracy : Window
    {

        private Baza_Logowanie.UserData userData;


        public oferty_pracy()
        {
            InitializeComponent();
            WyswietlOfertyPracy();
            Baza_Logowanie.CheckLoggedInUser();
        }

        private void WyswietlOfertyPracy()
        {
            List<(Ogloszenie, Firma)> oferty = BazaDanych_ogloszenie.PobierzOgloszeniaIFirmy();

            Grid mainGrid = new Grid();
            MainStackPanel.Children.Add(mainGrid);

            for (int i = 0; i < oferty.Count; i++)
            {
                Ogloszenie ogloszenie = oferty[i].Item1;
                Firma firma = oferty[i].Item2;

                if (i % 3 == 0)
                {
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Border border = new Border()
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1),
                    Width = 250,
                    Margin = new Thickness(10),
                    Height = 200,
                    Background = Brushes.Transparent,
                };

                border.MouseLeftButtonDown += (sender, e) =>
                {
                    MessageBox.Show("Przycisnieto przycisk pozdro");
                    PrzejdzDoStronySzczegolowUzytkownika(userData, ogloszenie, firma);
                };

                Image image = null;
                if (!string.IsNullOrEmpty(firma.logo_url))
                {
                    image = new Image()
                    {
                        Height = 70,
                        Source = new BitmapImage(new Uri(firma.logo_url)),
                        Margin = new Thickness(5),
                    };
                }

                TextBlock nazwaFirmyTextBlock = new TextBlock()
                {
                    Text = firma.nazwa_firmy,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Left,
                    Margin = new Thickness(5),
                };

                TextBlock widełkiWynagrodzeniaTextBlock = new TextBlock()
                {
                    Text = ogloszenie.WidelkiWynagrodzenia,
                    Margin = new Thickness(5),
                };

                TextBlock RodzajpracyTextBlock = new TextBlock()
                {
                    Text = ogloszenie.RodzajPracy,
                    Margin = new Thickness(5),
                };

                TextBlock adresFirmyTextBlock = new TextBlock()
                {
                    Text = "Adres: " + firma.adres_firmy,
                    Margin = new Thickness(5),
                };

                StackPanel infoPanel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                };

                infoPanel.Children.Add(nazwaFirmyTextBlock);
                infoPanel.Children.Add(widełkiWynagrodzeniaTextBlock);
                infoPanel.Children.Add(RodzajpracyTextBlock);
                infoPanel.Children.Add(adresFirmyTextBlock);

                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                };

                if (image != null)
                {
                    stackPanel.Children.Add(image);
                }
                stackPanel.Children.Add(infoPanel);

                border.Child = stackPanel;
                mainGrid.Children.Add(border);
                Grid.SetRow(border, i / 3);
                Grid.SetColumn(border, i % 3);
            }
        }
        


        private void PrzejdzDoStronySzczegolowUzytkownika(Baza_Logowanie.UserData userData, BazaDanych_ogloszenie.Ogloszenie ogloszenie, BazaDanych_ogloszenie.Firma firma)
        {
            SzczegolyOferty szczegoly = new SzczegolyOferty(userData, ogloszenie, firma);
            szczegoly.Show();
            Close();
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
