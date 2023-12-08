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
using static system_ogloszeniowy.Baza_Logowanie;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy mojekonto.xaml
    /// </summary>
    public partial class mojekonto : Window
    {
        public mojekonto()
        {
            InitializeComponent();

            int uzytkownikId = SessionManager.GetLoggedInUserId();

            WyswietlOgloszeniaUzytkownika(uzytkownikId);

        }
        private static string DbName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");
        private void WyswietlOgloszeniaUzytkownika(int idUzytkownika)
        {
            string selectOgloszeniaQuery = "SELECT * FROM Ogloszenie WHERE id_uzytkownika = @idUzytkownika";

            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();

                using (var command = new SQLiteCommand(selectOgloszeniaQuery, connection))
                {
                    command.Parameters.AddWithValue("@idUzytkownika", idUzytkownika);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ogloszenieId = Convert.ToInt32(reader["Id"]);
                            Grid mainGrid = new Grid();
                            MainStackPanel.Children.Add(mainGrid);

                            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                            Border border = new Border()
                            {
                                BorderBrush = Brushes.Black,
                                BorderThickness = new Thickness(1),
                                Width = 350,
                                Margin = new Thickness(10),
                            };


                            TextBlock idogloszenia = new TextBlock()
                            {
                                Text = $"Ogłoszenie o id: {ogloszenieId}",
                                Margin = new Thickness(0, 5, 0, 5),
                            };
                            string nazwaOgloszenia = reader["nazwa"].ToString();
                            string kategoriaOgloszenia = reader["kategoria"].ToString();
                            string inneDaneOgloszenia = "Dodatkowe informacje o ogłoszeniu...";

                            TextBlock nazwaOgloszeniaTextBlock = new TextBlock()
                            {
                                Text = $"Nazwa: {nazwaOgloszenia}",
                                FontWeight = FontWeights.Bold,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                Margin = new Thickness(0, 10, 0, 5),
                            };

                            TextBlock kategoriaOgloszeniaTextBlock = new TextBlock()
                            {
                                Text = $"Kategoria: {kategoriaOgloszenia}",
                                Margin = new Thickness(0, 5, 0, 5),
                            };

                            TextBlock inneDaneOgloszeniaTextBlock = new TextBlock()
                            {
                                Text = inneDaneOgloszenia,
                                Margin = new Thickness(0, 5, 0, 10),
                            };
                            Button zarzadzajbutton = new Button()
                            {
                                Content = "Zarzadzaj Ogloszeniem",
                                Width = 300,
                                Margin = new Thickness(0, 10, 0, 0),
                            };


                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Children.Add(nazwaOgloszeniaTextBlock);
                            stackPanel.Children.Add(idogloszenia);
                            stackPanel.Children.Add(kategoriaOgloszeniaTextBlock);
                            stackPanel.Children.Add(inneDaneOgloszeniaTextBlock);
                            stackPanel.Children.Add(zarzadzajbutton);

                            border.Child = stackPanel;

                            mainGrid.Children.Add(border);
                            border.MouseLeftButtonDown += (sender, e) =>
                            {
                                PrzejdzDoZarzadzania(ogloszenieId);
                            };
                        }
                    }
                }

                connection.Close();
            }
        }
        private void PrzejdzDoZarzadzania(int ogloszenieId)
        {
            BazaDanych_ogloszenie bazadanych = new BazaDanych_ogloszenie();
            ZarzadzanieOgloszeniem zarzadzanie = new ZarzadzanieOgloszeniem(ogloszenieId);
            zarzadzanie.Show();
            Close();
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
