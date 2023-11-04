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
using static system_ogloszeniowy.BazaDanych_ogloszenie;

namespace system_ogloszeniowy
{

    public partial class oferty_pracy : Window
    {




        public oferty_pracy()
        {
            InitializeComponent();
            WyswietlOfertyPracy();
        }
        private void WyswietlOfertyPracy()
        {
            List<(Ogloszenie, Firma)> oferty = BazaDanych_ogloszenie.PobierzOgloszeniaIFirmy(); // Pobierz oferty z bazy danych

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
                };

                TextBlock idTextBlock = new TextBlock()
                {
                    Text = "ID: " + ogloszenie.id_firmy,
                    FontWeight = FontWeights.Bold,
                };

                TextBlock nazwaTextBlock = new TextBlock()
                {
                    Text = ogloszenie.Nazwa,
                };

                TextBlock widełkiWynagrodzeniaTextBlock = new TextBlock()
                {
                    Text = ogloszenie.WidelkiWynagrodzenia,
                };

                Image image = null;
                if (!string.IsNullOrEmpty(firma.logo_url))
                {
                    image = new Image()
                    {
                        Height = 70,
                        Source = new BitmapImage(new Uri(firma.logo_url)),
                    };
                }

                TextBlock nazwaFirmyTextBlock = new TextBlock()
                {
                    Text = firma.nazwa_firmy,
                    TextAlignment = TextAlignment.Right,
                };

                TextBlock adresFirmyTextBlock = new TextBlock()
                {
                    Text = firma.adres_firmy,
                };

                StackPanel stackPanel = new StackPanel();
                stackPanel.Children.Add(idTextBlock);
                stackPanel.Children.Add(nazwaTextBlock);
                stackPanel.Children.Add(widełkiWynagrodzeniaTextBlock);
                if (image != null)
                {
                    stackPanel.Children.Add(image);
                }
                stackPanel.Children.Add(nazwaFirmyTextBlock);
                stackPanel.Children.Add(adresFirmyTextBlock);

                border.Child = stackPanel;
                mainGrid.Children.Add(border);
                Grid.SetRow(border, i / 3);
                Grid.SetColumn(border, i % 3);

               
                border.MouseLeftButtonDown += (sender, e) =>
                {
                    PrzejdzDoStronySzczegolow(oferty[i]);
                };
            }
        }



        private void PrzejdzDoStronySzczegolow((Ogloszenie, Firma) oferta)
        {
   
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
