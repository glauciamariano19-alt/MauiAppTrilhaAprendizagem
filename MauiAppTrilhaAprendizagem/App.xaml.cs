using MauiAppTrilhaAprendizagem.Data;
using MauiAppTrilhaAprendizagem.Views;

namespace MauiAppTrilhaAprendizagem
{
    public partial class App : Application
    {
        static DatabaseContext _database;

        public static DatabaseContext Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new DatabaseContext();
                    
                }
                return _database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}

