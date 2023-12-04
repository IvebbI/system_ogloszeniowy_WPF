using System;
using System.Data.SQLite;
using System.Windows;
using static system_ogloszeniowy.Baza_Logowanie;

namespace system_ogloszeniowy
{
    public partial class EdytujProfil : Window
    {
        private int idUzytkownika;

        public EdytujProfil()
        {
            InitializeComponent();
            Loaded += EdytujProfil_Loaded;

        }

        private void EdytujProfil_Loaded(object sender, RoutedEventArgs e)
        {
            Baza_Logowanie.CheckLoggedInUser();
            if (SessionManager.IsUserLoggedIn)
            {
                idUzytkownika = SessionManager.GetLoggedInUserId();
                WyswietlProfilUzytkownika(idUzytkownika);
            }
            else
            {
                MessageBox.Show("Użytkownik nie jest zalogowany.");
                Close(); // Zamknij okno edycji profilu, jeśli użytkownik nie jest zalogowany
            }
        }

        private void WyswietlProfilUzytkownika(int idUzytkownika)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=ogloszenie_baza.db;Version=3;"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM user WHERE id = {idUzytkownika}", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            emailtxt.Text = reader["email"].ToString();
                            haslotxt.Password = reader["password"].ToString();
                            imietxt.Text = reader["imie"].ToString();
                            nazwiskotxt.Text = reader["nazwisko"].ToString();
                            dataurodzeniatxt.Text = reader["dateurodzenia"].ToString();
                            telefontxt.Text = reader["telefon"].ToString();
                            linkdozdjeciatxt.Text = reader["linkdozdjecia"].ToString();
                            adrestxt.Text = reader["adres"].ToString();
                            stanowiskopracytxt.Text = reader["stanowiskopracy"].ToString();
                            opispracytxt.Text = reader["opispracy"].ToString();
                            podsumowaniezawodowe.Text = reader["podsumowaniezawodowe"].ToString();
                            githubprofil.Text = reader["githubprofil"].ToString();
                        }
                    }
                }
            }
        }

        private void ZapiszZmiany_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=ogloszenie_baza.db;Version=3;"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"UPDATE user SET email = @email, password = @password, imie = @imie, " +
                                          "nazwisko = @nazwisko, dateurodzenia = @dateurodzenia, telefon = @telefon, " +
                                          "linkdozdjecia = @linkdozdjecia, adres = @adres, stanowiskopracy = @stanowiskopracy, " +
                                          "opispracy = @opispracy, podsumowaniezawodowe = @podsumowaniezawodowe, " +
                                          "githubprofil = @githubprofil WHERE id = @idUzytkownika";
                    command.Parameters.AddWithValue("@idUzytkownika", idUzytkownika);
                    command.Parameters.AddWithValue("@email", emailtxt.Text);
                    command.Parameters.AddWithValue("@password", haslotxt.Password);
                    command.Parameters.AddWithValue("@imie", imietxt.Text);
                    command.Parameters.AddWithValue("@nazwisko", nazwiskotxt.Text);
                    command.Parameters.AddWithValue("@dateurodzenia", dataurodzeniatxt.Text);
                    command.Parameters.AddWithValue("@telefon", telefontxt.Text);
                    command.Parameters.AddWithValue("@linkdozdjecia", linkdozdjeciatxt.Text);
                    command.Parameters.AddWithValue("@adres", adrestxt.Text);
                    command.Parameters.AddWithValue("@stanowiskopracy", stanowiskopracytxt.Text);
                    command.Parameters.AddWithValue("@opispracy", opispracytxt.Text);
                    command.Parameters.AddWithValue("@podsumowaniezawodowe", podsumowaniezawodowe.Text);
                    command.Parameters.AddWithValue("@githubprofil", githubprofil.Text);

                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Zmiany zostały zapisane.");
        }


        private void Anuluj_Click(object sender, RoutedEventArgs e)
        {
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
