using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace system_ogloszeniowy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //odkomentuj ten kod ponizej tylko najpierw zdebuguj z tą linią nastepnie zakomentuj i odpal   Baza_Logowanie.InitializeDatabase(); 
            //BazaDanych_ogloszenie.CreateDatabaseAndTables();
            Baza_Logowanie.InitializeDatabase();
           
        }

    }
}
