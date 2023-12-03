using System.Windows;

namespace system_ogloszeniowy
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
          
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordBox.Password;
            string confirmPassword = confirmPasswordBox.Password;

            string imie = imieTextBox.Text;
            string nazwisko = nazwiskoTextBox.Text;
            string dateurodzenia = dateurodzeniaTextBox.Text;
            string telefon = telefonTextBox.Text;
            string linkdozdjecia = linkdozdjeciaTextBox.Text;
            string adres = adresTextBox.Text;
            string stanowiskopracy = stanowiskopracyTextBox.Text;
            string opispracy = opispracyTextBox.Text;
            string podsumowaniezawodowe = podsumowaniezawodoweTextBox.Text;
            string githubprofil = githubprofilTextBox.Text;

            // Walidacja pól (możesz dostosować zgodnie z potrzebami)
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(imie) || string.IsNullOrWhiteSpace(nazwisko) || string.IsNullOrWhiteSpace(dateurodzenia) ||
                string.IsNullOrWhiteSpace(telefon) || string.IsNullOrWhiteSpace(linkdozdjecia) || string.IsNullOrWhiteSpace(adres) ||
                string.IsNullOrWhiteSpace(stanowiskopracy) || string.IsNullOrWhiteSpace(opispracy) || string.IsNullOrWhiteSpace(podsumowaniezawodowe) ||
                string.IsNullOrWhiteSpace(githubprofil))
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja adresu e-mail (możesz dostosować zgodnie z potrzebami)
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Nieprawidłowy format adresu e-mail.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Walidacja hasła
            if (password != confirmPassword)
            {
                MessageBox.Show("Hasła nie pasują do siebie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Sprawdzenie, czy użytkownik o podanym adresie e-mail już istnieje
            if (Baza_Logowanie.IsEmailAlreadyRegistered(email))
            {
                MessageBox.Show("Użytkownik o podanym adresie e-mail już istnieje.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Dodanie do bazy danych
            if (Baza_Logowanie.RegisterUser(email, password, 0, imie, nazwisko, dateurodzenia, telefon, linkdozdjecia,
                                            adres, stanowiskopracy, opispracy, podsumowaniezawodowe, githubprofil))
            {
                MessageBox.Show("Rejestracja zakończona pomyślnie.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Rejestracja nie powiodła się. Spróbuj ponownie.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private bool IsValidEmail(string email)
        {
            // Prosta walidacja adresu e-mail za pomocą regex (możesz dostosować zgodnie z potrzebami)
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
    }
}
