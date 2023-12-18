using MistApp.Models;
using MistApp.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistApp.Services
{
    public class NavService
    {
        private static NavService _instance;
        private MainWindow _mainWindow;
        private Game _gameToNav;

        public static NavService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavService();
                }
                return _instance;
            }
        }

        public MainWindow curMainWindow
        {
            get => _mainWindow;
            set => _mainWindow = value;
        }

        public Game GameToNav
        {
            get => _gameToNav;
            set => _gameToNav = value;
        }
    }
}
