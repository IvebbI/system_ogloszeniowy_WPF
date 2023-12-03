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

namespace system_ogloszeniowy
{
    /// <summary>
    /// Logika interakcji dla klasy Glowna.xaml
    /// </summary>
    public partial class Glowna : Window
    {
        public Glowna()
        {
            InitializeComponent();
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
            mojekonto mk=new mojekonto();
            mk.Show();
            Close();
        }
        private void DodajOgloszenie_Click(object sender, RoutedEventArgs e)
        {
            // Przekierowanie na stronę DodajKonto
           ogloszenie_dodaj ogdodaj=new ogloszenie_dodaj();
            ogdodaj.Show();
            Close();
        }
    }
}
