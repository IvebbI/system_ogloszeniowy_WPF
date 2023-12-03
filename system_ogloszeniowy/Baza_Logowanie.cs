using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;

namespace system_ogloszeniowy
{
    public class Baza_Logowanie
    {
        private static string DbName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ogloszenie_baza.db");

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();

                // Tabela user
                string createUserTableQuery = "CREATE TABLE IF NOT EXISTS user (id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT UNIQUE, password TEXT, isadmin INTEGER, " +
                                            "imie TEXT, nazwisko TEXT, dateurodzenia TEXT, telefon TEXT, linkdozdjecia TEXT, adres TEXT, stanowiskopracy TEXT, " +
                                            "opispracy TEXT, podsumowaniezawodowe TEXT, githubprofil TEXT)";
                ExecuteNonQuery(createUserTableQuery, connection);

                // Tabela user_session (zmiana nazwy z session na user_session)
                string createUserSessionTableQuery = "CREATE TABLE IF NOT EXISTS user_session (id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT)";
                ExecuteNonQuery(createUserSessionTableQuery, connection);

                connection.Close();
            }
        }



        public static bool RegisterUser(string email, string password, int isAdmin, string imie, string nazwisko,
                                      string dateurodzenia, string telefon, string linkdozdjecia, string adres,
                                      string stanowiskopracy, string opispracy, string podsumowaniezawodowe, string githubprofil)
        {
            // Sprawdzenie, czy użytkownik o podanym adresie e-mail już istnieje
            if (IsEmailAlreadyRegistered(email))
            {
                // Możesz zwrócić informację, że użytkownik o tym adresie e-mail już istnieje
                return false;
            }

            // Jeśli użytkownik o podanym adresie e-mail nie istnieje, dodaj go do bazy danych
            string insertUserQuery = "INSERT INTO user (email, password, isadmin, imie, nazwisko, dateurodzenia, telefon, linkdozdjecia, " +
                                     "adres, stanowiskopracy, opispracy, podsumowaniezawodowe, githubprofil) " +
                                     "VALUES (@email, @password, @isadmin, @imie, @nazwisko, @dateurodzenia, @telefon, @linkdozdjecia, " +
                                     "@adres, @stanowiskopracy, @opispracy, @podsumowaniezawodowe, @githubprofil)";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(insertUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@isadmin", isAdmin);
                    command.Parameters.AddWithValue("@imie", imie);
                    command.Parameters.AddWithValue("@nazwisko", nazwisko);
                    command.Parameters.AddWithValue("@dateurodzenia", dateurodzenia);
                    command.Parameters.AddWithValue("@telefon", telefon);
                    command.Parameters.AddWithValue("@linkdozdjecia", linkdozdjecia);
                    command.Parameters.AddWithValue("@adres", adres);
                    command.Parameters.AddWithValue("@stanowiskopracy", stanowiskopracy);
                    command.Parameters.AddWithValue("@opispracy", opispracy);
                    command.Parameters.AddWithValue("@podsumowaniezawodowe", podsumowaniezawodowe);
                    command.Parameters.AddWithValue("@githubprofil", githubprofil);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    

        public static bool IsEmailAlreadyRegistered(string email)
        {
            string checkEmailQuery = "SELECT COUNT(*) FROM user WHERE email = @email";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(checkEmailQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public class UserData
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public int IsAdmin { get; set; }
            public string Imie { get; set; }
            public string Nazwisko { get; set; }
            public string DateUrodzenia { get; set; }
            public string Telefon { get; set; }
            public string LinkDoZdjecia { get; set; }
            public string Adres { get; set; }
            public string StanowiskoPracy { get; set; }
            public string OpisPracy { get; set; }
            public string PodsumowanieZawodowe { get; set; }
            public string GithubProfil { get; set; }

            // Dodaj inne właściwości użytkownika według potrzeb
        }
        public static class SessionManager
        {
            private static string loggedInUser;
            private static int loggedInUserId;

            public static bool IsUserLoggedIn => !string.IsNullOrEmpty(loggedInUser);

            public static string LoggedInUser => loggedInUser;

            public static void SetLoggedInUser(string email)
            {
                loggedInUser = email;

                // Ustaw także ID zalogowanego użytkownika
                loggedInUserId = GetUserIdByEmail(email);
            }

            public static void ClearLoggedInUser()
            {
                loggedInUser = null;
                loggedInUserId = 0;
            }

            // Metoda ustawiająca identyfikator zalogowanego użytkownika
            public static void SetLoggedInUserId(int userId)
            {
                loggedInUserId = userId;
            }

            // Metoda pobierająca identyfikator zalogowanego użytkownika
            public static int GetLoggedInUserId()
            {
                return loggedInUserId;
            }

            private static int GetUserIdByEmail(string email)
            {
                string selectUserIdQuery = "SELECT id FROM user WHERE email = @email";
                using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(selectUserIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        var result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
        }


        public static void CheckLoggedInUser()
        {
            // Sprawdź, czy użytkownik jest zalogowany, i uzyskaj jego dane
            if (SessionManager.IsUserLoggedIn)
            {
                string userEmail = SessionManager.LoggedInUser;
                Baza_Logowanie.UserData userData = Baza_Logowanie.GetUserData(userEmail);
            }
        }

        public static UserData GetUserData(string email)
        {
            string selectUserDataQuery = "SELECT * FROM user WHERE email = @email";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(selectUserDataQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserData
                            {
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString(),
                                IsAdmin = Convert.ToInt32(reader["isadmin"]),
                                Imie = reader["imie"].ToString(),
                                Nazwisko = reader["nazwisko"].ToString(),
                                DateUrodzenia = reader["dateurodzenia"].ToString(),
                                Telefon = reader["telefon"].ToString(),
                                LinkDoZdjecia = reader["linkdozdjecia"].ToString(),
                                Adres = reader["adres"].ToString(),
                                StanowiskoPracy = reader["stanowiskopracy"].ToString(),
                                OpisPracy = reader["opispracy"].ToString(),
                                PodsumowanieZawodowe = reader["podsumowaniezawodowe"].ToString(),
                                GithubProfil = reader["githubprofil"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }



        public static bool AuthenticateUser(string email, string password)
        {
            string authenticateUserQuery = "SELECT COUNT(*) FROM user WHERE email = @email AND password = @password";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(authenticateUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public static bool SetLoggedInUser(string email)
        {
            string insertSessionQuery = "INSERT INTO session (email) VALUES (@email)";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(insertSessionQuery, connection))
                {
                    command.Parameters.AddWithValue("@email", email);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static string GetLoggedInUser()
        {
            string selectSessionQuery = "SELECT email FROM session LIMIT 1";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(selectSessionQuery, connection))
                {
                    var result = command.ExecuteScalar();
                    return result != null ? result.ToString() : null;
                }
            }
        }

        public static void ClearLoggedInUser()
        {
            string clearSessionQuery = "DELETE FROM session";
            using (var connection = new SQLiteConnection($"Data Source={DbName};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(clearSessionQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }



        private static void ExecuteNonQuery(string query, SQLiteConnection connection)
        {
            using (var command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
