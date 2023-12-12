using System;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using static system_ogloszeniowy.BazaDanych_ogloszenie;
using Path = System.IO.Path;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy ZarzadzanieOgloszeniem.xaml
    /// </summary>
    public partial class ZarzadzanieOgloszeniem : Window
    {
        private int ogloszenieId;

        public ZarzadzanieOgloszeniem(int ogloszenieId)
        {
            InitializeComponent();
            this.ogloszenieId = ogloszenieId;
            MessageBox.Show(ogloszenieId.ToString());
            WyswietlFirmyIOgloszenia();
            
        }
        

        private static string DbName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");

        private void WyswietlFirmyIOgloszenia()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
                {
                    connection.Open();

                    string selectQuery = "SELECT Ogloszenie.*, Firma.* " +
                                         "FROM Ogloszenie " +
                                         "JOIN Firma ON Ogloszenie.id_firmy = Firma.id " +
                                         "WHERE Ogloszenie.id = @ogloszenieId";

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ogloszenieId", ogloszenieId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nazwaFirmy = GetStringOrNull(reader, "nazwa_firmy");
                                string adresFirmy = GetStringOrNull(reader, "adres");
                                string lokalizacjaGeograficzna = GetStringOrNull(reader, "lokalizacja_geograficzna");
                                string informacjeOFirmie = GetStringOrNull(reader, "Informacje");
                                string logoUrl = GetStringOrNull(reader, "logo_url");
                                string ogloszenienazwa = GetStringOrNull(reader, "nazwa");
                                string poziomstanowiska = GetStringOrNull(reader, "poziom_stanowiska");
                                string rodzaj_umowy = GetStringOrNull(reader, "rodzaj_umowy");
                                string wymiar_etatu = GetStringOrNull(reader, "wymiar_etatu");
                                string rodzaj_pracy = GetStringOrNull(reader, "rodzaj_pracy");
                                string widelki_wynagrodzenia = GetStringOrNull(reader, "widełki_wynagrodzenia");
                                string dnipracy = GetStringOrNull(reader, "dni_pracy");
                                string godzinypracy = GetStringOrNull(reader, "godziny_pracy");
                                string datawaznosci = GetStringOrNull(reader, "data_waznosci");
                                string kategoria = GetStringOrNull(reader, "kategoria");
                                string zakresobowiazkow = GetStringOrNull(reader, "zakres_obowiazkow");
                                string wymaganiakandydata = GetStringOrNull(reader, "wymagania_kandydata");
                                string oferowanebenefity = GetStringOrNull(reader, "oferowane_benefity");
                                string informacjeofirmie = GetStringOrNull(reader, "informacje_o_firmie");
                                string informacjefirma = GetStringOrNull(reader, "informacje");

                                TextBox nazwaFirmyTextBox = new TextBox()
                                {
                                    Text = nazwaFirmy,
                                    FontWeight = FontWeights.Bold,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width=300,
                                    Height=100,
                                };

                                TextBox adresFirmyTextBox = new TextBox()
                                {
                                    Text = adresFirmy,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };

                                TextBox lokalizacjaGeograficznaTextBox = new TextBox()
                                {
                                    Text = lokalizacjaGeograficzna,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };

                                TextBox informacjeOFirmieTextBox = new TextBox()
                                {
                                    Text = informacjeOFirmie,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };

                                TextBox image = new TextBox()
                                {
                                    Text = logoUrl.ToString(),
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                         

                                TextBox _ogloszenienazwa = new TextBox()
                                {
                                    Text = ogloszenienazwa,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _poziom_stanowiska = new TextBox()
                                {
                                    Text = poziomstanowiska,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _rodzajumowy = new TextBox()
                                {
                                    Text = rodzaj_umowy,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _wymiaretatu = new TextBox()
                                {
                                    Text = wymiar_etatu,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _rodzajpracy = new TextBox()
                                {
                                    Text = rodzaj_pracy,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _widelkiwynagrodzenia = new TextBox()
                                {
                                    Text = widelki_wynagrodzenia,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _dnipracy = new TextBox()
                                {
                                    Text = dnipracy,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _godzinypracy = new TextBox()
                                {
                                    Text = godzinypracy,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _datawaznosci = new TextBox()
                                {
                                    Text = datawaznosci,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _kategoria = new TextBox()
                                {
                                    Text = kategoria,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _zakresobowiazkow = new TextBox()
                                {
                                    Text = zakresobowiazkow,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _wymaganiakandydata = new TextBox()
                                {
                                    Text = wymaganiakandydata,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _oferowanebenefity = new TextBox()
                                {
                                    Text = oferowanebenefity,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                TextBox _informacjeofirmie = new TextBox()
                                {
                                    Text = informacjeofirmie,
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 300,
                                    Height = 100,
                                };
                                Button PrzyciskEdytuj = new Button()
                                {
                                    Content = "Edytuj Ogłoszenie",
                                    Margin = new Thickness(10, 10, 0, 10),
                                    Width = 150,
                                    Height = 50,
                                };

                                PrzyciskEdytuj.Click += (sender, e) =>
                                {
                                    try
                                    {
                                        using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
                                        {
                                            connection.Open();

                                            string selectFirmaIdQuery = "SELECT id_firmy FROM Ogloszenie WHERE id = @ogloszenieId";

                                            int firmaId;

                                            using (var commandSelectFirmaId = new SQLiteCommand(selectFirmaIdQuery, connection))
                                            {
                                                commandSelectFirmaId.Parameters.AddWithValue("@ogloszenieId", ogloszenieId);

                                                var result = commandSelectFirmaId.ExecuteScalar();

                                                if (result != null && int.TryParse(result.ToString(), out firmaId))
                                                {

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Nie udało się uzyskać identyfikatora firmy.");
                                                    return;
                                                }
                                            }

                                            string updateOgloszenieQuery = "UPDATE Ogloszenie " +
                                                                           "SET poziom_stanowiska = @poziomstanowiska," +
                                                                           "rodzaj_umowy = @rodzajumowy, " +
                                                                           "wymiar_etatu = @wymiaretatu, " +
                                                                           "rodzaj_pracy = @rodzajpracy, " +
                                                                           "widełki_wynagrodzenia = @widelkiwynagrodzenia, " +
                                                                           "dni_pracy = @dnipracy, " +
                                                                           "godziny_pracy = @godzinypracy, " +
                                                                           "data_waznosci = @datawaznosci, " +
                                                                           "kategoria = @kategoria, " +
                                                                           "zakres_obowiazkow = @zakresobowiazkow, " +
                                                                           "wymagania_kandydata = @wymaganiakandydata, " +
                                                                           "oferowane_benefity = @oferowanebenefity, " +
                                                                           "informacje_o_firmie = @informacjeofirmie " +
                                                                           "WHERE id = @ogloszenieId";

                                            using (var commandOgloszenie = new SQLiteCommand(updateOgloszenieQuery, connection))
                                            {
                                                commandOgloszenie.Parameters.AddWithValue("@poziomstanowiska", _poziom_stanowiska.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@rodzajumowy", _rodzajumowy.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@wymiaretatu", _wymiaretatu.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@rodzajpracy", _rodzajpracy.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@widelkiwynagrodzenia", _widelkiwynagrodzenia.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@dnipracy", _dnipracy.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@godzinypracy", _godzinypracy.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@datawaznosci", _datawaznosci.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@kategoria", _kategoria.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@zakresobowiazkow", _zakresobowiazkow.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@wymaganiakandydata", _wymaganiakandydata.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@oferowanebenefity", _oferowanebenefity.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@informacjeofirmie", _informacjeofirmie.Text);
                                                commandOgloszenie.Parameters.AddWithValue("@ogloszenieId", ogloszenieId);

                                                int rowsAffectedOgloszenie = commandOgloszenie.ExecuteNonQuery();

                                            
                                            }

                                     
                                            string updateFirmaQuery = "UPDATE Firma " +
                                                "SET nazwa_firmy = @nazwaFirmy, " +
                                                "adres = @adresFirmy, " +
                                                "lokalizacja_geograficzna = @lokalizacjaGeograficzna, " +
                                                "Informacje = @informacjeOFirmie, " +
                                                "logo_url = @logoUrl " +
                                                "WHERE id = @firmaId";

                                            using (var commandFirma = new SQLiteCommand(updateFirmaQuery, connection))
                                            {
                                                commandFirma.Parameters.AddWithValue("@nazwaFirmy", nazwaFirmyTextBox.Text);
                                                commandFirma.Parameters.AddWithValue("@adresFirmy", adresFirmyTextBox.Text);
                                                commandFirma.Parameters.AddWithValue("@lokalizacjaGeograficzna", lokalizacjaGeograficznaTextBox.Text);
                                                commandFirma.Parameters.AddWithValue("@informacjeOFirmie", informacjeOFirmieTextBox.Text);
                                                commandFirma.Parameters.AddWithValue("@logoUrl", image.Text);
                                                commandFirma.Parameters.AddWithValue("@firmaId", firmaId);

                                                int rowsAffectedFirma = commandFirma.ExecuteNonQuery();
                                                MessageBox.Show("Pomyślnie zaktualizowano ogłoszenie!");
                                              
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"Wystąpił błąd: {ex.Message}");
                                    }
                                };


                                if (image != null)
                                {
                                    MainStackPanel.Children.Add(image);
                                }
                                MainStackPanel.Children.Add(nazwaFirmyTextBox);
                                MainStackPanel.Children.Add(adresFirmyTextBox);
                                MainStackPanel.Children.Add(lokalizacjaGeograficznaTextBox);
                                MainStackPanel.Children.Add(informacjeOFirmieTextBox);
                                MainStackPanel.Children.Add(_ogloszenienazwa);
                                MainStackPanel.Children.Add(_poziom_stanowiska);
                                MainStackPanel.Children.Add(_rodzajumowy);
                                MainStackPanel.Children.Add(_wymiaretatu);
                                MainStackPanel.Children.Add(_rodzajpracy);
                                MainStackPanel.Children.Add(_widelkiwynagrodzenia);
                                MainStackPanel.Children.Add(_dnipracy);
                                MainStackPanel.Children.Add(_godzinypracy);
                                MainStackPanel.Children.Add(_datawaznosci);
                                MainStackPanel.Children.Add(_kategoria);
                                MainStackPanel.Children.Add(_zakresobowiazkow);
                                MainStackPanel.Children.Add(_wymaganiakandydata);
                                MainStackPanel.Children.Add(_oferowanebenefity);
                                MainStackPanel.Children.Add(_informacjeofirmie);
                                MainStackPanel.Children.Add(PrzyciskEdytuj);
                            }
                            else
                            {
                                MessageBox.Show("Nie znaleziono ogłoszenia o podanym identyfikatorze.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
      
        
        private string GetStringOrNull(SQLiteDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? null : reader.GetString(columnIndex);
        }

        private void ZarzadzajClicked(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę główną
            Glowna glowna = new Glowna();
            glowna.Show();
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
