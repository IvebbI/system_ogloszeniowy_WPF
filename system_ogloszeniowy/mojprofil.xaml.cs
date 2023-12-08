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
using static system_ogloszeniowy.BazaDanych_ogloszenie;
using System.IO;
using Path = System.IO.Path;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy mojprofil.xaml
    /// </summary>
    public partial class mojprofil : Window
    { 
        public mojprofil()
        {
            InitializeComponent();
            Baza_Logowanie.CheckLoggedInUser();
            if (SessionManager.IsUserLoggedIn)
            {
                WyswietlProfilUzytkownika();
                int idUzytkownika = SessionManager.GetLoggedInUserId();
                WyswietlOgloszeniaUzytkownika(idUzytkownika);
            }
            else
            {
                MessageBox.Show("Użytkownik nie jest zalogowany.");
            }
        }
        private static string DbName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");


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

                            TextBlock headerTextBlock = new TextBlock()
                            {
                                Text = "Ogłoszenie",
                                FontWeight = FontWeights.Bold,
                                FontSize = 24,
                                HorizontalAlignment = HorizontalAlignment.Center,
                                Margin = new Thickness(0, 20, 0, 10),
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

                            StackPanel stackPanel = new StackPanel();
                            stackPanel.Children.Add(headerTextBlock);
                            stackPanel.Children.Add(nazwaOgloszeniaTextBlock);
                            stackPanel.Children.Add(kategoriaOgloszeniaTextBlock);
                            stackPanel.Children.Add(inneDaneOgloszeniaTextBlock);

                            border.Child = stackPanel;

                            mainGrid.Children.Add(border);

                            mainGrid.MouseLeftButtonDown += (sender, e) =>
                            {
                            };
                        }
                    }
                }

                connection.Close();
            }
        }

        private void Przycisk_Click(object sender, RoutedEventArgs e)
        {
            EdytujProfil nowaStrona = new EdytujProfil();
            nowaStrona.Show();
            Close();
        }



        private void WyswietlProfilUzytkownika()
        {
            if (SessionManager.IsUserLoggedIn)
            {
                string userEmail = SessionManager.LoggedInUser;
                Baza_Logowanie.UserData userData = Baza_Logowanie.GetUserData(userEmail);

                if (userData != null)
                {
                    Grid mainGrid = new Grid();
                    MainStackPanel.Children.Add(mainGrid);

                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    Button edytujProfilButton = new Button()
                    {
                        Content = "Edytuj profil",
                        Width=300,
                        Margin = new Thickness(0, 10, 0, 0), 
                    };

                    edytujProfilButton.Click += Przycisk_Click;
                    TextBlock headerTextBlock = new TextBlock()
                    {
                        Text = "Mój Profil",
                        FontWeight = FontWeights.Bold,
                        FontSize = 24, 
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 20, 0, 10), 
                    };

                    Border border = new Border()
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Width = 350,  
                        Margin = new Thickness(10),
                    };

                    Image image = null;
                    if (!string.IsNullOrEmpty(userData.LinkDoZdjecia))
                    {
                        image = new Image()
                        {
                            Height = 200, 
                            Width = 200,  
                            Source = new BitmapImage(new Uri(userData.LinkDoZdjecia)),
                            Margin = new Thickness(0, 10, 0, 10),  
                        };
                    }

                    TextBlock imieNazwiskoTextBlock = new TextBlock()
                    {
                        Text = $"{userData.Imie} {userData.Nazwisko}",
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontSize = 18,  
                        Margin = new Thickness(0, 10, 0, 5), 
                    };

                    TextBlock emailTextBlock = new TextBlock()
                    {
                        Text = $"Email: {userData.Email}",
                        Margin = new Thickness(0, 5, 0, 5), 
                    };

                    TextBlock dateUrodzeniaTextBlock = new TextBlock()
                    {
                        Text = $"Data urodzenia: {userData.DateUrodzenia}",
                        Margin = new Thickness(0, 5, 0, 5), 
                    };

                    TextBlock telefonTextBlock = new TextBlock()
                    {
                        Text = $"Telefon: {userData.Telefon}",
                        Margin = new Thickness(0, 5, 0, 5), 
                    };

                    // Dodaj dodatkowe informacje
                    TextBlock adresTextBlock = new TextBlock()
                    {
                        Text = $"Adres: {userData.Adres}",
                        Margin = new Thickness(0, 5, 0, 5), 
                    };

                    TextBlock stanowiskoPracyTextBlock = new TextBlock()
                    {
                        Text = $"Stanowisko pracy: {userData.StanowiskoPracy}",
                        Margin = new Thickness(0, 5, 0, 5), 
                    };

                    TextBlock opisPracyTextBlock = new TextBlock()
                    {
                        Text = $"Opis pracy: {userData.OpisPracy}",
                        Margin = new Thickness(0, 5, 0, 5),  
                    };

                    TextBlock podsumowanieZawodoweTextBlock = new TextBlock()
                    {
                        Text = $"Podsumowanie zawodowe: {userData.PodsumowanieZawodowe}",
                        Margin = new Thickness(0, 5, 0, 5),  
                    };

                    TextBlock githubProfilTextBlock = new TextBlock()
                    {
                        Text = $"Profil GitHub: {userData.GithubProfil}",
                        Margin = new Thickness(0, 5, 0, 10), 
                    };
           


                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Children.Add(headerTextBlock);
                    stackPanel.Children.Add(image);
                    stackPanel.Children.Add(imieNazwiskoTextBlock);
                    stackPanel.Children.Add(emailTextBlock);
                    stackPanel.Children.Add(dateUrodzeniaTextBlock);
                    stackPanel.Children.Add(telefonTextBlock);
                    stackPanel.Children.Add(adresTextBlock);
                    stackPanel.Children.Add(stanowiskoPracyTextBlock);
                    stackPanel.Children.Add(opisPracyTextBlock);
                    stackPanel.Children.Add(podsumowanieZawodoweTextBlock);
                    stackPanel.Children.Add(githubProfilTextBlock);
                    stackPanel.Children.Add(edytujProfilButton);
                    border.Child = stackPanel;
                    mainGrid.Children.Add(border);

                    border.MouseLeftButtonDown += (sender, e) =>
                    {
                        PrzejdzDoStronySzczegolowUzytkownika(userData);
                    };
                }
                else
                {
                    MessageBox.Show("Nie udało się pobrać danych użytkownika.");
                }
            }
        }




        private void PrzejdzDoStronySzczegolowUzytkownika(Baza_Logowanie.UserData userData)
        {
            SzczegolyOferty szczegoly = new SzczegolyOferty();
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
