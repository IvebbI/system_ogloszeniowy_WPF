using System.Data.SQLite;
using System;
using System.Windows;
using static system_ogloszeniowy.Baza_Logowanie;
using System.IO;

namespace system_ogloszeniowy
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            CheckLoggedInUser();
        }

        private void CheckLoggedInUser()
        {
            if (SessionManager.IsUserLoggedIn)
            {
                string userEmail = SessionManager.LoggedInUser;
                UserData userData = Baza_Logowanie.GetUserData(userEmail);

            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
   
            string email = emailTextBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Nieprawidłowy format adresu e-mail.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (Baza_Logowanie.AuthenticateUser(email, password))
            {
                SessionManager.SetLoggedInUser(email);
                MessageBox.Show("Zalogowano pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);

                Glowna glowna = new Glowna();
                glowna.SetSession(SessionManager.LoggedInUser);
                glowna.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Logowanie nie powiodło się. Spróbuj ponownie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

    }
}
